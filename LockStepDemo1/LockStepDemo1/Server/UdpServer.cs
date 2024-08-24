using Commit.Config;
using Commit.Utils;
using LockStepDemo1.Server.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Xml.Linq;
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
        private static User NewUser(string name, string password, int id)
        {
            return new User()
            {
                Name = name,
                Password = password,
                Id = id
            };
        }
        // 启动upd的server
        public static void Start()
        {
            Init();
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
                    BaseRequest requset = ProtoBufUtils.DeSerializeBaseRequest(receivedData);
                    // 处理接收到的数据...
                    HandleReceivedData(requset, clientEndPoint);
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

        private static void HandleReceivedData(BaseRequest request, IPEndPoint client)
        {
            // 处理消息逻辑...
            if (request.RequestType == RequestType.RtLogin) // 登陆请求
            {
                if (request.RequestData == RequestData.RdUser)
                {
                    HandleLogin(request, client);
                }
            }
            else if (request.RequestType == RequestType.RtMessage) // 消息的请求
            {
                // 发送消息
                HandleBroadcastMessage(request, client);
            }
        }
        /// <summary>
        /// 广播发送的消息
        /// </summary>
        /// <param name="message">消息的内容</param>
        /// <param name="sourceClient">谁在发送</param>
        private static void HandleBroadcastMessage(BaseRequest request, IPEndPoint sourceClient)
        {
            Console.WriteLine("\n\n开始发消息了：");
            Msg msg = request.Msg;
            byte[] message = ProtoBufUtils.SerializeBaseRequest(request);
            Console.WriteLine("我在这里是发");
            foreach (int id in activeClients.Keys)
            {
                if (id != request.UserId) // 这个用户发送消息给其它的所有的用户
                {
                    try
                    {
                        Console.WriteLine($"消息发送给 {activeClients[id].User.Name}");
                        udpServer.Send(message, message.Length, activeClients[id].Point);
                    }
                    catch (SocketException s)
                    {
                        //Console.WriteLine($"❌ 发送消息到 {client} 失败: {ex.Message}");
                        // 从活动客户端列表中移除该客户端
                        Console.WriteLine($"客户端 {activeClients[id].User.Name} 已移除");
                        activeClients.Remove(id);
                    }
                    catch (ArgumentNullException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }


        /// <summary>
        /// 处理登陆的请求
        /// </summary>
        /// <param name="requst">数据源</param>
        /// <param name="client">upd请求</param>
        private static void HandleLogin(BaseRequest request,  IPEndPoint client)
        {
            BaseRequest baseRequest = new BaseRequest()
            {
                RequestType = RequestType.RtLogin,
                RequestData = RequestData.RdStatus,
                Status = new Status()
                {
                   St = StatusType.StError,
                   Msg = "用户名或密码错误"
                }
            };
            
            bool isOk = false;
            User user = request.User;
            foreach (User u in users)
            {
                if (u.Name == user.Name && u.Password == user.Password)
                {
                    isOk = true;
                    user = u; // 查询到的用户
                    break;
                }
            }
            if (isOk) // 登陆成功
            {
                baseRequest.Status.St = StatusType.StSuccess;
                baseRequest.Status.Msg = "登陆成功！！";
                baseRequest.UserId = user.Id;
                // 更新活动客户端列表
                if (!activeClients.ContainsKey(user.Id))
                {
                    activeClients.Add(user.Id, new ClientInfo(user, client));
                    Console.WriteLine($"新客户端: {client}");
                }
            }
            byte[] message = ProtoBufUtils.SerializeBaseRequest(baseRequest);
            udpServer.Send(message, message.Length, client);
        }
    }
}
