using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using Google.Protobuf;
//using OutputNamespace; // 替换为实际产生的命名空间

namespace LockStepDemoClient1
{
    public class User
    {
        public string name;
        public string password;
        public int port; // 模拟端口
        public User(string name, string password, int port)
        {
            this.name = name;
            this.password = password;
            this.port = port;
        }
        public User() { }
    }
    internal class Program
    {
        private const int port = 12345;
        private static UdpClient udpClient = new UdpClient();
        //private static UdpClient udpClient = new UdpClient(new Random().Next(12346, 12370));
        private static IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port); // 请替换为实际的服务器 IP
        private static List<User> users = new List<User>();
        private static void Init()
        {
            users.Add(new User("张三", "123", 12347));
            users.Add(new User("里斯", "123", 12348));
            users.Add(new User("王五", "123", 12349));
            users.Add(new User("张柳", "123", 12350));
            users.Add(new User("力气", "123", 12351));
            users.Add(new User("Admin", "123", 12352));
        }
        private static User Login()
        {
            Init();
            User user = new User();
            while (user.port == 0)
            {
                Console.WriteLine("请登录:\nuserName:");
                string userName = Console.ReadLine();
                Console.WriteLine("password:");
                string password = Console.ReadLine();
                foreach (var item in users)
                {
                    if (item.name == userName && item.password == password)
                    {
                        user = item;
                        break;
                    }
                }
            }
            return user;
        }
        //static void Main()
        //{
        //    User user = Login();
        //    //udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, 0)); // 也可以指定具体的端口
        //    udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, user.port)); // 指定具体的端口

        //    byte[] data2 = Encoding.UTF8.GetBytes($"username:{user.name},password:{user.password}");
        //    udpClient.Send(data2, data2.Length, serverEndPoint);

        //    Thread receiveThread = new Thread(ReceiveMessages);
        //    receiveThread.Start();

        //    Console.WriteLine("💬 UDP 聊天客户端已启动，输入消息发送（输入 'exit' 退出）:");

        //    while (true)
        //    {
        //        string message = Console.ReadLine();
        //        if (message.ToLower() == "exit") break;

        //        byte[] data = Encoding.UTF8.GetBytes(message);
        //        udpClient.Send(data, data.Length, serverEndPoint);
        //        Console.WriteLine($"发送的消息: {message}");
        //    }

        //    // 清理资源
        //    udpClient.Close();
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