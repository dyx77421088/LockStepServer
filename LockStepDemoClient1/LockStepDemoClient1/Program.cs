
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

        private static User Login()
        {
            //Init();
            User user = new User();
            while (user.Prot == 0)
            {
                Console.WriteLine("请登录:\nuserName:");
                string userName = Console.ReadLine();
                Console.WriteLine("password:");
                string password = Console.ReadLine();
                foreach (var item in users)
                {
                    if (item.Name == userName && item.Password == password)
                    {
                        user = item;
                        break;
                    }
                }
            }
            return user;
        }
        static void Main2()
        {
            
            User user = Login();
            //udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, 0)); // 也可以指定具体的端口
            udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, user.Prot)); // 指定具体的端口

            byte[] data2 = Encoding.UTF8.GetBytes($"username:{user.Name},password:{user.Password}");
            udpClient.Send(data2, data2.Length, serverEndPoint);

            Thread receiveThread = new Thread(ReceiveMessages);
            receiveThread.Start();

            Console.WriteLine("💬 UDP 聊天客户端已启动，输入消息发送（输入 'exit' 退出）:");

            while (true)
            {
                string message = Console.ReadLine();
                if (message.ToLower() == "exit") break;

                byte[] data = Encoding.UTF8.GetBytes(message);
                udpClient.Send(data, data.Length, serverEndPoint);
                Console.WriteLine($"发送的消息: {message}");
            }

            // 清理资源
            udpClient.Close();
        }

        static void Main()
        {
            BaseRequest request = new BaseRequest()
            {
                RequestType = RequestType.RtLogin,
                RequestData = RequestData.RdUser,
                User = new User
                {
                    Name = "Alice",
                    Id = 123,
                    Password = "asfsa",
                }
            };
            // 序列化为二进制数据
            byte[] data;
            data = request.ToByteArray();
            // 将数据写入文件（可选）
            //File.WriteAllBytes("person.bin", data);

            Console.WriteLine(data.Length);
            BaseRequest newPerson = BaseRequest.Parser.ParseFrom(data);
            // 输出结果
            Console.WriteLine($"Name: {newPerson.User.Name}, ID: {newPerson.User.Id}");
            Console.WriteLine("password: " + string.Join(", ", newPerson.User.Password));
            Console.WriteLine("RequestType: " + string.Join(", ", newPerson.RequestType));
            Console.WriteLine("RequestData: " + string.Join(", ", newPerson.RequestData));
        }

        private static void ReceiveMessages()
        {
            IPEndPoint listenEndPoint = new IPEndPoint(IPAddress.Any, port);

            while (true)
            {
                try
                {
                    byte[] receivedData = udpClient.Receive(ref listenEndPoint);
                    string receivedMessage = Encoding.UTF8.GetString(receivedData);
                    Console.WriteLine($"📥 收到消息: {receivedMessage}");
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