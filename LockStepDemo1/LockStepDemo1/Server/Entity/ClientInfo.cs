using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
/*
    客户端信息
 */
namespace LockStepDemo1.Server.Entity
{
    internal class ClientInfo
    {
        public User User { get; set; }
        public IPEndPoint Point { get; set; }
        public ClientInfo(User user, IPEndPoint point)
        {
            User = user;
            Point = point;
        }
    }
}
