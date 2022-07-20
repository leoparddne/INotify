using Common.Notify.Config;
using Common.Notify.DTO;
using Common.Notify.Tools.Wechat;
using Common.Notify.Tools.Wechat.DTO.Message.MessageDto;
using System;

namespace Common.Notify.MessageProvider
{
    public class WechatProvider : BaseProvider
    {
        public void SendMessage(BaseConfig config, object message)
        {
            if (config is not WechatConfig wechatConfig)
            {
                throw new Exception("Error DingTalk Config");
            }

            if (message is not BaseMessageInDto wechatMessage)
            {
                throw new Exception("Error DingTalk MessageData");
            }

            WechatHelper.SendMessage(wechatConfig.Corpid, wechatConfig.Corpsecret, wechatMessage);
        }

        public void SendMessage(BaseConfig config, SendTextInDto dto)
        {
            if (config is not WechatConfig wechatConfig)
            {
                throw new Exception("Error DingTalk Config");
            }
            int agentId = Convert.ToInt32(wechatConfig.AgengId);
            WechatHelper.SendMessage(wechatConfig.Corpid, wechatConfig.Corpsecret, new TextInDto
            {
                agentid = agentId,
                touser = string.Join(',', dto.SendTo),
                msgtype = "text",
                duplicate_check_interval = 1800,
                text = new Text
                {
                    content = dto.Content
                }
            });
        }
    }
}
