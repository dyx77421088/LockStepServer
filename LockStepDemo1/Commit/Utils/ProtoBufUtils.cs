using Google.Protobuf;
using System.Text.RegularExpressions;

namespace Commit.Utils
{
    /// <summary>
    /// 序列化相关的工具类
    /// </summary>
    public class ProtoBufUtils
    {
        public static BaseRequest DeSerializeBaseRequest(byte[] data)
        {
            return BaseRequest.Parser.ParseFrom(data);
        }
        public static byte[] SerializeBaseRequest(BaseRequest baseRequest)
        {
            return baseRequest.ToByteArray();
        }

        public static byte[] SerializeBaseRequest(User user, RequestType rt, RequestData rd)
        {
            BaseRequest baseRequest = new BaseRequest()
            {
                RequestType = rt,
                RequestData = rd,
                User = user,
            };
            return baseRequest.ToByteArray();
        }
        /// <summary>
        /// 这种没有歧义的就不用RequestData了，也就是说RequestType为RequestType.RtMatch，那么它的类型一定是match
        /// </summary>
        /// <param name="match">匹配</param>
        /// <returns></returns>
        public static byte[] SerializeBaseRequest(Matching match)
        {
            BaseRequest baseRequest = new BaseRequest()
            {
                RequestType = RequestType.RtMatch,
                Matching = match,
            };
            return baseRequest.ToByteArray();
        }
    }
}
