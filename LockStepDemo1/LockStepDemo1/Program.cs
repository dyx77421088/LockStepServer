using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using Microsoft.Win32;
using Google.Protobuf;

namespace LockStepDemo1
{

    internal class Program
    {
        private const int port = 12345;
        private static List<IPEndPoint> clients = new List<IPEndPoint>();
        private static List<IPEndPoint> activeClients = new List<IPEndPoint>();
        private static List<string> names = new List<string>();
        private static UdpClient udpServer = new UdpClient(port);

        //static void Main()
        //{
        //    Console.WriteLine("🚀 UDP 聊天服务器已启动，等待消息...");

        //    IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, port);

        //    const uint IOC_IN = 0x80000000;
        //    int IOC_VENDOR = 0x18000000;
        //    int SIO_UDP_CONNRESET = (int)(IOC_IN | IOC_VENDOR | 12);

        //    //因为我使用的是UdpClient, 所以先get出Socket（Client）来。
        //    udpServer.Client.IOControl((int)SIO_UDP_CONNRESET, new byte[] { Convert.ToByte(false) }, null);
        //    ReceiveMessages();
        //}

        static void Main()
        {
            // 创建一个新的 Person 对象并赋值
            Person person = new Person
            {
                Name = "Alice",
                Id = 123,
                Phone = { "555-1234", "555-5678" }
            };

            // 序列化为二进制数据
            byte[] data;
            data = person.ToByteArray();
            // 将数据写入文件（可选）
            //File.WriteAllBytes("person.bin", data);

            Console.WriteLine(data.Length);
            // 反序列化
            //Person newPerson;
            //using (var stream = new MemoryStream(data))
            //{
            //    newPerson = Person.Parser.ParseFrom(stream);
            //}
            Person newPerson = Person.Parser.ParseFrom(data);
            // 输出结果
            Console.WriteLine($"Name: {newPerson.Name}, ID: {newPerson.Id}");
            Console.WriteLine("Phones: " + string.Join(", ", newPerson.Phone));
        }

        private static void ReceiveMessages()
        {
            // 假设这是接收消息的循环
            IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Any, 0);
            while (true)
            {
                try
                {
                    byte[] receivedData = udpServer.Receive(ref clientEndPoint);

                    // 处理接收到的数据...
                    HandleReceivedData(receivedData, clientEndPoint);

                    // 更新活动客户端列表
                    if (!activeClients.Contains(clientEndPoint))
                    {
                        activeClients.Add(clientEndPoint);
                        Console.WriteLine($"新客户端: {clientEndPoint}");
                    }
                }
                catch (SocketException ex)
                {
                    Console.WriteLine($"SocketException: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                }
            }
        }

        private static void HandleReceivedData(byte[] receivedData, IPEndPoint client)
        {
            // 处理消息逻辑...
            // 使用 UTF8 编码将字节数组转换为字符串
            string str = Encoding.UTF8.GetString(receivedData);
            if (str.Contains("username:") && str.Contains(",password"))
            {
                // 分割字符串
                string[] parts = str.Split(',');

                // 提取用户名和密码
                string username = parts[0].Split(':')[1];
                string password = parts[1].Split(':')[1];
                names.Add(username);
            }
            else
            {
                // 发送消息
                BroadcastMessage(receivedData, client);
            }
            
        }

        private static void BroadcastMessage(byte[] message, IPEndPoint sourceClient)
        {
            Console.WriteLine("\n\n开始发消息了：");
            for (int i = 0; i < activeClients.Count; ++i)
            {
                IPEndPoint client = activeClients[i];
                string name = names[i];
                if (!client.Equals(sourceClient))
                {
                    try
                    {
                        udpServer.Send(message, message.Length, client);
                        Console.WriteLine($"消息发送给 {name}");
                    }
                    catch (SocketException s)
                    {
                        //Console.WriteLine($"❌ 发送消息到 {client} 失败: {ex.Message}");
                        // 从活动客户端列表中移除该客户端
                        activeClients.Remove(client);
                        Console.WriteLine($"客户端 {client} 已移除");
                    }
                    catch (ArgumentNullException ex)
                    {
                        Console.WriteLine(ex.Message );
                    }
                }
            }
        }
    }
}
