# Common.Notify
统一消息推送：钉钉、企业微信、飞书、邮件

支持发送基础文本，也支持通过 `SendText` 传递自定义结构由底层 SDK 发送。

!!!请注意调整对应的 key

### 依赖工程

- `Common.Toolkit`：公共封装类库（HTTP 请求、对象映射、异常校验、扩展方法）

### 通道类型

`ConfigTypeEnum` 目前支持的通道：

| 枚举值 | 说明 |
| --- | --- |
| `WeChat` | 企业微信 |
| `DingTalk` | 钉钉 |
| `EMail` | 邮件 |
| `Feishu` | 飞书 |
| `SMS` | 短信（未实现） |

### 配置类

所有配置继承自 `BaseConfig`，公共字段 `Timeout`（超时时长，秒，默认 10）。

- `DingTalkConfig`：`AgentId`、`AppKey`、`AppSecret`
- `WechatConfig`：`Corpid`、`Corpsecret`、`AgengId`
- `FeishuConfig`：`APPID`、`APPSecret`
- `MailConfig`：`MailFrom`、`MailPwd`、`MailHost`、`MailPort`、`SecurityType`（`MailKit.Security.SecureSocketOptions`，默认 `Auto`）

### 核心入口类

- `MessageHelper`：消息发送入口。
  - `SendText(ConfigTypeEnum type, BaseConfig config, SendTextInDto dto)`：按指定通道发送基础文本。
  - `SendText(CommonSendMessageInDto dto)`：通用调用入口，内部自动解析配置、按部门获取用户并发送。
- `ContactHelper`：通讯录查询。
  - `GetDepartmentList(MessageConfigInDto dto)`：获取企业下所有部门。
  - `GetUserListByDepartment(GetDepartmentInDto dto)`：获取部门下的所有用户。
- `NotifyConfigHelper`：配置解析。
  - `GetConfig(MessageConfigInDto dto)`：根据 `ConfigType` 取出对应的配置对象。
  - `ParseConfig(ConfigTypeEnum configType, string configStr, int timeout = 10, bool skipTimeout = false)`：从 JSON 字符串解析配置。
  - `ParseSendConfig(...)`：解析配置并组装为 `CommonSendMessageInDto`。
- `OAHelper`：OA 审批相关（目前仅支持钉钉），含 `GetTemplate`、`StartProcess`、`GetDingInstanceDetail`、`AgreeProcess`、`RefuseProcess`、`Callback`。

### 关键 DTO

- `SendTextInDto`：`Subject`（邮件主题）、`SendTo`（发送目标）、`Content`（正文）、`BizID`、`RedirectPath`、`AttachFileList`。
- `CommonSendMessageInDto`：继承 `MessageConfigInDto`，额外包含 `Content`、`UsersIds`（平台 userid/邮箱地址）、`GroupIds`（部门 id）、`Subject`、`AttachFileList`、`BizID`、`RedirectPath`。
- `AttachFileConfig`：附件配置，外部只需传 `FileID`，`FilePath`/`FileName` 为内部使用。
- `MessageConfigInDto`：`ConfigType` 及各通道配置对象（`DingTalkConfig`、`WechatConfig`、`FeishuConfig`、`MailConfig`）和 `Timeout`。

### 测试

`Common.Notify.Test` 中的 `FeishuProviderTest` 演示了飞书通道的调用方式。


### 使用示例
* 直接调用具体通道
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

* 统一调用

通过 `CommonSendMessageInDto` 指定通道与配置，`MessageHelper` 会自动解析配置、按 `GroupIds` 获取部门用户，合并 `UsersIds` 后统一发送。

```
   [TestMethod]
        public void SendByCommon()
        {
            MessageHelper.SendText(new CommonSendMessageInDto
            {
                ConfigType = Enums.ConfigTypeEnum.DingTalk,
                DingTalkConfig = new DingTalkConfig
                {
                    AgentId = "AgengId",
                    AppKey = "AppKey",
                    AppSecret = "AppSecret"
                },
                Subject = "test",
                Content = "test---",
                // 平台 userid / 邮箱地址
                UsersIds = new List<string> { "xxx" },
                // 部门 id，仅企业微信、钉钉会按部门拉取用户
                GroupIds = new List<string> { "1" }
            });
        }
```

* 飞书通道

```
   [TestMethod]
        public void SendFeishu()
        {
            FeishuConfig config = new FeishuConfig
            {
                APPID = "appId",
                APPSecret = "appSecret"
            };
            MessageHelper.SendText(Enums.ConfigTypeEnum.Feishu, config, new DTO.SendTextInDto
            {
                Content = "test---",
                SendTo = new List<string> { "user_id" }
            });
        }
```

* 带附件的邮件

```
   MessageHelper.SendText(Enums.ConfigTypeEnum.EMail, config, new DTO.SendTextInDto
   {
       Subject = "test",
       Content = "test---",
       SendTo = new List<string> { "xx@xx.com.cn" },
       AttachFileList = new List<AttachFileConfig>
       {
           new AttachFileConfig { FileID = "文件ID" }
       }
   });
```

### 通讯录

* 获取部门列表

```
   var departments = ContactHelper.GetDepartmentList(new MessageConfigInDto
   {
       ConfigType = Enums.ConfigTypeEnum.Feishu,
       FeishuConfig = config
   });
```

* 获取部门下的用户

```
   var users = ContactHelper.GetUserListByDepartment(new GetDepartmentInDto
   {
       ConfigType = Enums.ConfigTypeEnum.Feishu,
       FeishuConfig = config,
       Id = new List<string> { "部门id" }
   });
```

### 配置解析

从 JSON 字符串解析配置并组装为发送 DTO：

```
   var dto = NotifyConfigHelper.ParseSendConfig(Enums.ConfigTypeEnum.DingTalk, configJson, timeout: 10);
   dto.Content = "test---";
   dto.UsersIds = new List<string> { "xxx" };
   MessageHelper.SendText(dto);
```

### OA 审批（仅钉钉）

```
   // 获取审批模板
   OAHelper.GetTemplate(Enums.ConfigTypeEnum.DingTalk, config, parameter);

   // 发起流程
   OAHelper.StartProcess(Enums.ConfigTypeEnum.DingTalk, config, parameter);

   // 审批通过 / 拒绝
   OAHelper.AgreeProcess(Enums.ConfigTypeEnum.DingTalk, config, parameter);
   OAHelper.RefuseProcess(Enums.ConfigTypeEnum.DingTalk, config, parameter);
```