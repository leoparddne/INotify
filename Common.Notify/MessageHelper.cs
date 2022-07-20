using Common.Notify.Config;
using Common.Notify.DTO;
using Common.Notify.Enums;
using Common.Notify.MessageProvider;

namespace Common.Notify
{
    /// <summary>
    /// 
    /// </summary>
    public static class MessageHelper
    {
        /// <summary>
        /// 支持扩展的格式-根据实际情况传递具体的发送参数即可
        /// </summary>
        /// <param name="type"></param>
        /// <param name="config"></param>
        /// <param name="message"></param>
        /// <exception cref="System.NotImplementedException"></exception>
        public static void Send(ConfigTypeEnum type, BaseConfig config, object message)
        {
            BaseProvider provider = null;
            switch (type)
            {
                case ConfigTypeEnum.WeChat:
                    provider = new WechatProvider();
                    break;
                case ConfigTypeEnum.DingTalk:
                    provider = new DingTalkProvider();
                    break;
                case ConfigTypeEnum.EMail:
                    provider = new MailProvider();
                    break;
                case ConfigTypeEnum.SMS:
                    throw new System.NotImplementedException();
                    break;
                default:
                    break;
            }

            provider.SendMessage(config, message);
        }

        /// <summary>
        /// 发送基础文本格式数据
        /// </summary>
        /// <param name="type"></param>
        /// <param name="config"></param>
        /// <param name="message"></param>
        /// <exception cref="System.NotImplementedException"></exception>
        public static void SendText(ConfigTypeEnum type, BaseConfig config, SendTextInDto dto)
        {
            BaseProvider provider = null;
            switch (type)
            {
                case ConfigTypeEnum.WeChat:
                    provider = new WechatProvider();
                    break;
                case ConfigTypeEnum.DingTalk:
                    provider = new DingTalkProvider();
                    break;
                case ConfigTypeEnum.EMail:
                    provider = new MailProvider();
                    break;
                case ConfigTypeEnum.SMS:
                    throw new System.NotImplementedException();
                    break;
                default:
                    break;
            }

            provider.SendMessage(config, dto);
        }
    }
}
