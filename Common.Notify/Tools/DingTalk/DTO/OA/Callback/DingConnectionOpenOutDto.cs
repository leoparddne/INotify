namespace Common.Notify.Tools.DingTalk.DTO.OA.Callback
{
    public class DingConnectionOpenOutDto : DingOACommonOutDto
    {
        /// <summary>
        /// 钉钉 Stream 服务端的地址信息，对应下个步骤中建立WebSocket 连接中服务器的 Host。

        /// </summary>
        public string endpoint { get; set; }

        /// <summary>
        /// 服务端颁发的身份信息，客户端需凭此信息和钉钉服务端建连连接。对应下个步骤中建立 WebSocket 连接中的 ticket 参数。ticket的有效期为90秒且只能用来建立一条连接，请勿存储ticket或重复使用。
        /// </summary>
        public string ticket { get; set; }
    }
}
