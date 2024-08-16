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
        public static byte[] DeSerializeBaseRequest(BaseRequest baseReqeust)
        {
            return baseReqeust.ToByteArray();
        }
    }
}
