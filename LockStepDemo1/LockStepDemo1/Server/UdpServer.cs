using Commit.Config;
using LockStepDemo1.Server.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
/*
    UDP的服务端
 */
namespace LockStepDemo1.Server
{
    internal class UdpServer
    {
        private static int port = NetConfig.UDP_PORT;
        private static UdpClient udpServer = new UdpClient(port);
        // 添加连入的用户的信息（用于广播）  key表示的是id
        private static Dictionary<int, ClientInfo> activeClients = new Dictionary<int, ClientInfo>(); 
        // 启动upd的server
        public static void Start()
        {
            Console.WriteLine("🚀 UDP 聊天服务器已启动，等待消息...");

            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, port);

            const uint IOC_IN = 0x80000000;
            int IOC_VENDOR = 0x18000000;
            int SIO_UDP_CONNRESET = (int)(IOC_IN | IOC_VENDOR | 12);

            //因为我使用的是UdpClient, 所以先get出Socket（Client）来。
            udpServer.Client.IOControl((int)SIO_UDP_CONNRESET, new byte[] { Convert.ToByte(false) }, null);
            ReceiveMessages();
        }
        /// <summary>
        /// 循环接收信息
        /// </summary>
        private static void ReceiveMessages()
        {
            // 假设这是接收消息的循环
            IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Any, 0);
            while (true)
            {
                try
                {
                    byte[] receivedData = udpServer.Receive(ref clientEndPoint);
                    //BaseRequest requset = 
                    // 处理接收到的数据...
                    HandleReceivedData(receivedData, clientEndPoint);

                    // 更新活动客户端列表
                    if (!activeClients.ContainsKey(clientEndPoint))
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
                if (br.RequestData == RequestData.RdUser)
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
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
    }
}
