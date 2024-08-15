using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
    转换指定目录下的所有proto为c#到指定路径下
 */
namespace Utils
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string protoDirectory = @"C:\Users\WIN\Desktop\unityProject\Server\LockStepDemoClient1\Commit\Proto"; // 替换为你的.proto文件目录路径
            string outputDirectory = @"C:\Users\WIN\Desktop\unityProject\Server\LockStepDemoClient1\Commit\Proto\output"; // 替换为输出目录

            // 确保输出目录存在
            Directory.CreateDirectory(outputDirectory);

            // 获取所有 .proto 文件
            string[] protoFiles = Directory.GetFiles(protoDirectory, "*.proto");

            foreach (string protoFile in protoFiles)
            {
                ConvertProtoToCs(protoFile, outputDirectory);
            }

            Console.WriteLine("所有 .proto 文件已成功转换为 C#。");
        }

        static void ConvertProtoToCs(string protoFile, string outputDirectory)
        {
            //protoc--csharp_out =.person.proto

            // 获取 .proto 文件所在目录
            string protoDirectory = Path.GetDirectoryName(protoFile);

            // 运行 protoc 命令
            var processStartInfo = new ProcessStartInfo
            {
                FileName = "protoc",
                Arguments = $"--proto_path=\"{protoDirectory}\" --csharp_out=\"{outputDirectory}\" \"{protoFile}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (var process = Process.Start(processStartInfo))
            {
                process.WaitForExit();

                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                if (process.ExitCode != 0)
                {
                    Console.WriteLine($"转换文件 {protoFile} 时出错: {error}");
                }
                else
                {
                    Console.WriteLine($"成功转换: {protoFile}");
                }
            }
        }
    }
}
