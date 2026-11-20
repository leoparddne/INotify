# Common.Notify

统一消息推送：钉钉、企业微信、飞书、邮件。

支持直接调用指定通道，也支持通过 `MessageHelper.SendText(CommonSendMessageInDto)` 统一调用。

!!! 请注意调整对应的 key

## 工程结构

- `Common.Toolkit`：公共封装类库（HTTP 请求、对象映射、异常校验、扩展方法）
- `Common.Notify`：消息推送与通讯录、OA 核心库
- `Common.Notify.WebAPI`：对外 HTTP 接口
- `Common.Notify.Test`：测试示例

## 通道类型

`ConfigTypeEnum`：`WeChat`（企业微信）、`DingTalk`（钉钉）、`EMail`（邮件）、`Feishu`（飞书）、`SMS`（短信，未实现）。

## 配置类

所有配置继承自 `BaseConfig`，公共字段 `Timeout`（秒，默认 10）。

- `DingTalkConfig`：`AgentId`、`AppKey`、`AppSecret`
- `WechatConfig`：`Corpid`、`Corpsecret`、`AgengId`
- `FeishuConfig`：`APPID`、`APPSecret`
- `MailConfig`：`MailFrom`、`MailPwd`、`MailHost`、`MailPort`、`SecurityType`（默认 `Auto`）

## 核心入口

- `MessageHelper`：`SendText(type, config, dto)` 指定通道发送；`SendText(dto)` 通用发送。
- `ContactHelper`：`GetDepartmentList(dto)` 获取部门；`GetUserListByDepartment(dto)` 获取部门用户。
- `NotifyConfigHelper`：`GetConfig`、`ParseConfig`、`ParseSendConfig` 配置解析。
- `OAHelper`：OA 审批（仅钉钉）。

## 关键 DTO

- `SendTextInDto`：`Subject`、`SendTo`、`Content`、`BizID`、`RedirectPath`、`AttachFileList`。
- `CommonSendMessageInDto`：继承 `MessageConfigInDto`，额外含 `Content`、`UsersIds`、`GroupIds`、`Subject`、`AttachFileList`、`BizID`、`RedirectPath`。
- `AttachFileConfig`：附件配置，外部只需传 `FileID`。
- `MessageConfigInDto`：`ConfigType`、各通道配置对象、`Timeout`。

## 使用示例

直接调用具体通道：

```csharp
// 钉钉
var dingConfig = new DingTalkConfig
{
    AgentId = "AgentId",
    AppKey = "AppKey",
    AppSecret = "AppSecret"
};
MessageHelper.SendText(ConfigTypeEnum.DingTalk, dingConfig, new SendTextInDto
{
    Content = "test---",
    SendTo = new List<string> { "xxx" }
});

// 企业微信
var wechatConfig = new WechatConfig
{
    AgengId = "AgengId",
    Corpid = "Corpid",
    Corpsecret = "Corpsecret"
};
MessageHelper.SendText(ConfigTypeEnum.WeChat, wechatConfig, new SendTextInDto
{
    Content = "test---",
    SendTo = new List<string> { "xx" }
});

// 邮件
var mailConfig = new MailConfig
{
    MailHost = "smtp.exmail.qq.com",
    MailPort = 465,
    MailFrom = "test@qq.com.cn",
    MailPwd = "test",
    // 安全类型，默认自动；服务器 SSL 验证失败可改用非 SSL 端口（默认 25）
    SecurityType = MailKit.Security.SecureSocketOptions.Auto
};
MessageHelper.SendText(ConfigTypeEnum.EMail, mailConfig, new SendTextInDto
{
    Subject = "test",
    Content = "test---",
    SendTo = new List<string> { "xx@xx.com.cn", "xxx@xx.com.cn" }
});

// 飞书
var feishuConfig = new FeishuConfig
{
    APPID = "appId",
    APPSecret = "appSecret"
};
MessageHelper.SendText(ConfigTypeEnum.Feishu, feishuConfig, new SendTextInDto
{
    Content = "test---",
    SendTo = new List<string> { "user_id" }
});
```

统一调用：

```csharp
MessageHelper.SendText(new CommonSendMessageInDto
{
    ConfigType = ConfigTypeEnum.DingTalk,
    DingTalkConfig = config,
    Subject = "test",
    Content = "test---",
    UsersIds = new List<string> { "xxx" },  // 平台 userid / 邮箱地址
    GroupIds = new List<string> { "1" }     // 部门 id，仅企业微信、钉钉支持
});
```

邮件（含附件）：

```csharp
MessageHelper.SendText(ConfigTypeEnum.EMail, config, new SendTextInDto
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

配置解析：

```csharp
var dto = NotifyConfigHelper.ParseSendConfig(ConfigTypeEnum.DingTalk, configJson, timeout: 10);
dto.Content = "test---";
dto.UsersIds = new List<string> { "xxx" };
MessageHelper.SendText(dto);
```

## WebAPI

`Common.Notify.WebAPI` 基于 `Common.Notify` 暴露 HTTP 接口，无授权，路由前缀 `api/notify/{controller}/{action}`。

### 接口

| 方法 | 路由 | 说明 |
| --- | --- | --- |
| POST | `api/notify/Notify/Send` | 发送消息（JSON body，`CommonSendMessageInDto`） |
| POST | `api/notify/Notify/SendFile` | 发送带附件消息（`multipart/form-data`，附件为上传文件） |
| POST | `api/notify/Contact/GetDepartmentList` | 获取部门列表 |
| POST | `api/notify/Contact/GetUserList` | 获取部门下的用户 |

### 配置（appsettings.json）

| 配置项 | 说明 |
| --- | --- |
| `APIName` | 接口名称，用于 Swagger |
| `SwaggerFile` | Swagger 注释文件，多个用逗号分隔 |
| `Notify:Timeout` | 请求超时（秒），默认 10 |

Swagger 地址：`/swagger`。

### 返回结构

统一返回 `APIResponseModel<T>`：`Code`（0 成功 / 1 失败）、`Message`、`Data`。

## 测试

`Common.Notify.Test` 中的 `FeishuProviderTest` 演示了飞书通道的调用方式。
