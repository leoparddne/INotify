using Common.Notify.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Common.Notify.Test
{
    [TestClass]
    public class MessageHelperTest
    {
        [TestMethod]
        public void SendDing()
        {
            DingTalkConfig config = new DingTalkConfig
            {
                AgentId = "AgengId",
                AppKey = "AppKey",
                AppSecret = "AppSecret"
            };
            MessageHelper.SendText(Enums.ConfigTypeEnum.DingTalk, config, new DTO.SendTextInDto
            {
                Content = "test---",
                SendTo = new List<string> { "xxx" }
            });
        }

        [TestMethod]
        public void SendWechat()
        {
            WechatConfig config = new WechatConfig
            {
                AgengId = "AgengId",
                Corpid = "Corpid",
                Corpsecret = "Corpsecret"
            };
            MessageHelper.SendText(Enums.ConfigTypeEnum.WeChat, config, new DTO.SendTextInDto
            {
                Content = "test---",
                SendTo = new List<string> { "xx" }
            });
        }

        [TestMethod]
        public void SendMail()
        {
            MailConfig config = new MailConfig
            {
                MailHost = "smtp.exmail.qq.com",
                MailPort = 465,
                MailFrom = "test@qq.com.cn",
                MailPwd = "test"
            };
            MessageHelper.SendText(Enums.ConfigTypeEnum.EMail, config, new DTO.SendTextInDto
            {
                Subject = "test",
                Content = "test---",
                SendTo = new List<string> { "xx@xx.com.cn", "xxx@xx.com.cn" }
            });
        }
    }
}
