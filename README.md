# Common.Notify
统一消息推送：钉钉、企业微信、飞书、邮件

支持发送基础文本，也支持通过 `SendText` 传递自定义结构由底层 SDK 发送。

!!!请注意调整对应的 key

### 依赖工程

- `Common.Toolkit`：公共封装类库（HTTP 请求、对象映射、异常校验、扩展方法）

### MessageHelper 类

- `SendText(ConfigTypeEnum type, BaseConfig config, SendTextInDto dto)`：按指定通道发送基础文本。
- `SendText(CommonSendMessageInDto dto)`：通用调用入口，内部自动解析配置、按部门获取用户并发送。

### 测试

`Common.Notify.Test` 中的 `FeishuProviderTest` 演示了飞书通道的调用方式。

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
