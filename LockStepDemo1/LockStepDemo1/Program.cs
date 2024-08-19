using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using Google.Protobuf;
using Commit.Config;
using LockStepDemo1.Server;

namespace LockStepDemo1
{

    internal class Program
    {
        static void Main()
        {
            UdpServer.Start();
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

    }
}
