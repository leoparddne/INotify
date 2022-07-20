namespace Common.Notify.Tools.Wechat.DTO.Message
{
    public class SendMessageOutDto : WechatCommonOutDto
    {
        public string invalidparty { get; set; }
        public string invalidtag { get; set; }
        public string msgid { get; set; }
    }

}
