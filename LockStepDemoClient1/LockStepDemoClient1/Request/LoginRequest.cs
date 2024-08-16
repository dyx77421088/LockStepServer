using Commit.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace LockStepDemoClient1.Request
{
    internal class LoginRequest
    {
        // 登陆请求
        public static bool Login(User user)
        {
            using (TcpClient client = new TcpClient("localhost", NetConfig.TCP_PORT))
            {
                NetworkStream stream = client.GetStream();

                // 发送请求
                string request = "你好，服务器！";
                byte[] requestData = Encoding.UTF8.GetBytes(request);
                stream.Write(requestData, 0, requestData.Length);
                Console.WriteLine("请求已发送: " + request);

                // 接收响应
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine("收到响应: " + response);
            }
            return true;
        }
    }
}
