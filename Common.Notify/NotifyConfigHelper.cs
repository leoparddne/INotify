using Common.Notify.Config;
using Common.Notify.DTO;
using Common.Notify.DTO.Message;
using Common.Notify.Enums;
using Newtonsoft.Json;
using System;

namespace Common.Notify
{
    public static class NotifyConfigHelper
    {
        /// <summary>
        /// 获取配置
        /// </summary>
        public static BaseConfig GetConfig(MessageConfigInDto dto)
        {
            BaseConfig config = null;
            switch (dto.ConfigType)
            {
                case Enums.ConfigTypeEnum.WeChat:
                    config = dto.WechatConfig;
                    break;
                case Enums.ConfigTypeEnum.DingTalk:
                    config = dto.DingTalkConfig;
                    break;
                case Enums.ConfigTypeEnum.EMail:
                    config = dto.MailConfig;
                    break;
                case Enums.ConfigTypeEnum.SMS:
                    throw new NotImplementedException();
                    break;
                case Enums.ConfigTypeEnum.Feishu:
                    config = dto.FeishuConfig;
                    break;
                default:
                    break;
            }

            config.Timeout = dto.Timeout;
            return config;
        }

        public static CommonSendMessageInDto ParseSendConfig(ConfigTypeEnum configType, string configStr, int timeout = 10, bool skipTimeout = false)
        {
            var config = ParseConfig(configType, configStr, timeout, skipTimeout);

            var result = new CommonSendMessageInDto();
            result.ConfigType = configType;
            switch (configType)
            {
                case ConfigTypeEnum.WeChat:
                    result.WechatConfig = config as WechatConfig;
                    break;
                case ConfigTypeEnum.DingTalk:
                    result.DingTalkConfig = config as DingTalkConfig;
                    break;
                case ConfigTypeEnum.EMail:
                    result.MailConfig = config as MailConfig;
                    break;
                case ConfigTypeEnum.SMS:
                    break;
                case ConfigTypeEnum.Feishu:
                    result.FeishuConfig = config as FeishuConfig;
                    break;
                default:
                    break;
            }

            return result;
        }
        public static BaseConfig ParseConfig(ConfigTypeEnum configType, string configStr, int timeout = 10, bool skipTimeout = false)
        {
            BaseConfig config = null;
            switch (configType)
            {
                case Enums.ConfigTypeEnum.WeChat:
                    config = JsonConvert.DeserializeObject<WechatConfig>(configStr);
                    break;
                case Enums.ConfigTypeEnum.DingTalk:
                    config = JsonConvert.DeserializeObject<DingTalkConfig>(configStr);
                    break;
                case Enums.ConfigTypeEnum.EMail:
                    config = JsonConvert.DeserializeObject<MailConfig>(configStr);
                    break;
                case Enums.ConfigTypeEnum.SMS:
                    throw new NotImplementedException();
                    break;
                case Enums.ConfigTypeEnum.Feishu:
                    config = JsonConvert.DeserializeObject<FeishuConfig>(configStr);
                    break;
                default:
                    break;
            }

            if (!skipTimeout)
            {
                config.Timeout = timeout;
            }
            return config;
        }
    }
}
