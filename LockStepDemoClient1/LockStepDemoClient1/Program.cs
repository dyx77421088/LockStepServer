
using Commit.Utils;
using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace LockStepDemoClient1
{
    internal class Program
    {
        private const int port = 12345;
        private static UdpClient udpClient = new UdpClient();
        //private static UdpClient udpClient = new UdpClient(new Random().Next(12346, 12370));
        private static IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port); // 请替换为实际的服务器 IP
        private static List<User> users = new List<User>();

        // 登陆！
        private static void Login()
        {
            User user = new User();
            Console.WriteLine("请登录:\nuserName:");
            string userName = Console.ReadLine();
            Console.WriteLine("password:");
            string password = Console.ReadLine();
            user.Name = userName;
            user.Password = password;
            byte[] message = ProtoBufUtils.DeSerializeBaseRequest(user, RequestType.RtLogin, RequestData.RdUser);
            clientUser = user;
            udpClient.Send(message, message.Length, serverEndPoint);
        }
        static void Main()
        {
            
            
            udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, 0)); // 0：自己分配端口号 也可以指定具体的端口

            Login(); // 登陆
            Thread receiveThread = new Thread(ReceiveMessages);
            receiveThread.Start();

            Console.WriteLine("💬 UDP 聊天客户端已启动，输入消息发送（输入 'exit' 退出）:");


        }
        static User clientUser;
        private static void ReceiveMessages()
        {
            IPEndPoint listenEndPoint = new IPEndPoint(IPAddress.Any, port);

            while (true)
            {
                try
                {
                    byte[] receivedData = udpClient.Receive(ref listenEndPoint);
                    BaseRequest baseRequest = ProtoBufUtils.SerializeBaseRequest(receivedData);
                    if (baseRequest.RequestType == RequestType.RtLogin )
                    {
                        if (baseRequest.RequestData == RequestData.RdStatus)
                        {
                            if (baseRequest.Status.St == StatusType.StError)
                            {
                                Console.WriteLine(baseRequest.Status.Msg);
                                Login(); // 登陆失败，需要再次登陆
                            }
                            else if (baseRequest.Status.St == StatusType.StSuccess)
                            {
                                Console.WriteLine(baseRequest.Status.Msg);
                                // 循环发送消息
                                while (true)
                                {
                                    string str = Console.ReadLine();
                                    if (str.ToLower() == "exit") break;

                                    BaseRequest br = new BaseRequest()
                                    {
                                        RequestType = RequestType.RtMessage,
                                        RequestData = RequestData.RdMessage,
                                        Msg = new Msg()
                                        {
                                            Msg_ = str
                                        }
                                    };
                                    byte[] msg = ProtoBufUtils.DeSerializeBaseRequest(br);
                                    udpClient.Send(msg, msg.Length, serverEndPoint);
                                    //Console.WriteLine($"发送的消息: {str}");
                                }

                                //清理资源
                                udpClient.Close();
                            }
                        }

                    }
                    else if (baseRequest.RequestType == RequestType.RtMessage)
                    {
                        if (baseRequest.RequestData == RequestData.RdMessage)
                        {
                            Console.WriteLine("\n收到一条消息:");
                            Console.WriteLine("发送者:" +  baseRequest.Msg.User.Name);
                            Console.WriteLine("内容:" +  baseRequest.Msg.Msg_);
                        }
                    }
                }
                catch (ObjectDisposedException)
                {
                    Console.WriteLine("UdpClient已关闭。");
                    break; // 可以选择停止接收
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"异常: {ex.Message}");
                }
            }
        }
    }
}