using Common.Notify.Config;
using Common.Notify.Enums;

namespace Common.Notify.DTO
{
    //TODO - 后续拆分为多个配置类
    public class MessageConfigInDto
    {
        public ConfigTypeEnum ConfigType { get; set; }

        public DingTalkConfig DingTalkConfig { get; set; }
        public WechatConfig WechatConfig { get; set; }
        public FeishuConfig FeishuConfig { get; set; }
        public MailConfig MailConfig { get; set; }

        /// <summary>
        /// 超时时长
        /// </summary>
        public int Timeout { get; set; }
    }
}
