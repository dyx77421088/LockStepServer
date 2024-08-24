using Google.Protobuf;

namespace Commit.Utils
{
    /// <summary>
    /// 序列化相关的工具类
    /// </summary>
    public class ProtoBufUtils
    {
        public static BaseRequest SerializeBaseRequest(byte[] data)
        {
            return BaseRequest.Parser.ParseFrom(data);
        }
        public static byte[] DeSerializeBaseRequest(BaseRequest baseRequest)
        {
            return baseRequest.ToByteArray();
        }

        public static byte[] DeSerializeBaseRequest(User user, RequestType rt, RequestData rd)
        {
            BaseRequest baseRequest = new BaseRequest()
            {
                RequestType = rt,
                RequestData = rd,
                User = user,
            };
            return baseRequest.ToByteArray();
        }
    }
}
