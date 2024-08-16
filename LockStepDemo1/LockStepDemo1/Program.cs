﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using Google.Protobuf;
using Commit.Config;

namespace LockStepDemo1
{

    internal class Program
    {
        private static int port = NetConfig.UDP_PORT;
        private static List<IPEndPoint> clients = new List<IPEndPoint>();
        private static List<IPEndPoint> activeClients = new List<IPEndPoint>();
        private static List<string> names = new List<string>();
        private static UdpClient udpServer = new UdpClient(port);
        private static List<User> users = new List<User>();
        private static void Init()
        {
            users.Add(NewUser("张三", "123", 12347));
            users.Add(NewUser("里斯", "123", 12348));
            users.Add(NewUser("王五", "123", 12349));
            users.Add(NewUser("张柳", "123", 12350));
            users.Add(NewUser("力气", "123", 12351));
            users.Add(NewUser("Admin", "123", 12352));
        }
        private static User NewUser(string name, string password, int port)
        {
            return new User()
            {
                Name = name,
                Password = password,
                Prot = port
            };
        }

        static void Main()
        {
            Init(); // 初始化user
            Console.WriteLine("🚀 UDP 聊天服务器已启动，等待消息...");

            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, port);

            const uint IOC_IN = 0x80000000;
            int IOC_VENDOR = 0x18000000;
            int SIO_UDP_CONNRESET = (int)(IOC_IN | IOC_VENDOR | 12);

            //因为我使用的是UdpClient, 所以先get出Socket（Client）来。
            udpServer.Client.IOControl((int)SIO_UDP_CONNRESET, new byte[] { Convert.ToByte(false) }, null);
            ReceiveMessages();
        }

        static void Main3()
        {
            // 创建一个新的 Person 对象并赋值
            User person = new User
            {
                Name = "Alice",
                Id = 123,
                Password = "asfsa",
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
            BaseRequest br = BaseRequest.Parser.ParseFrom(receivedData);
            if (br.RequestType == RequestType.RtLogin) // 登陆请求
            {
                if(br.RequestData == RequestData.RdUser)
                {
                    bool isOk = false;
                    foreach (User user in users)
                    {
                        if (user.Name == br.User.Name && user.Password == br.User.Password)
                        {
                            isOk = true;
                            break;
                        }
                    }

                }
            }
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
