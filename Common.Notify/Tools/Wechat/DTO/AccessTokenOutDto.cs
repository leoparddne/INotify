namespace Common.Notify.Tools.Wechat.DTO
{
    public class AccessTokenOutDto : WechatCommonOutDto
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
    }
}
