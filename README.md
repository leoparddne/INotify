# INotify
统一消息推送,钉钉、微信、邮件


统一调用在MessageHelperTest测试类中

!!!请注意调整对应的key

### MessageHelper类
SendText方法用来发送基础的文本内容

Send方法可以发送自定义的结构,通过object类型将具体的sdk需要的结构传递到底层进行发送

```

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
                MailPwd = "test",
				//安全类型,默认为自动,如果服务器ssl验证失败可以修改此处类型并调整端口为非ssl端口(默认25)
				//需要注意office365部分场景下需要使用StartTls
                SecurityType = MailKit.Security.SecureSocketOptions.Auto
            };
            MessageHelper.SendText(Enums.ConfigTypeEnum.EMail, config, new DTO.SendTextInDto
            {
                Subject = "test",
                Content = "test---",
                SendTo = new List<string> { "xx@xx.com.cn", "xxx@xx.com.cn" }
            });
        }
```