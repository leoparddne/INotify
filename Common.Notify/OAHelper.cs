using Common.Notify.Config;
using Common.Notify.Enums;
using Common.Notify.OA;
using System;

namespace Common.Notify
{
    public static class OAHelper
    {
        /// <summary>
        /// 获取模板基础信息
        /// </summary>
        /// <param name="type"></param>
        /// <param name="config"></param>
        /// <param name="message"></param>
        /// <exception cref="NotImplementedException"></exception>
        public static object GetTemplate(ConfigTypeEnum type, BaseConfig config, object message)
        {
            OABaseProvider provider = null;
            switch (type)
            {
                case ConfigTypeEnum.WeChat:
                    throw new NotImplementedException();
                    break;
                case ConfigTypeEnum.DingTalk:
                    provider = new DingTalkOAProvider();
                    break;
                case ConfigTypeEnum.EMail:
                    throw new NotImplementedException();
                    break;
                default:
                    throw new NotImplementedException();
                    break;
            }

            return provider.GetTemplate(config, message);
        }

        /// <summary>
        /// 开始流程
        /// </summary>
        /// <param name="type"></param>
        /// <param name="config"></param>
        /// <param name="message"></param>
        /// <exception cref="NotImplementedException"></exception>
        public static object StartProcess(ConfigTypeEnum type, BaseConfig config, object parameter)
        {
            OABaseProvider provider = null;
            switch (type)
            {
                case ConfigTypeEnum.WeChat:
                    throw new NotImplementedException();
                    break;
                case ConfigTypeEnum.DingTalk:
                    provider = new DingTalkOAProvider();
                    break;
                case ConfigTypeEnum.EMail:
                    throw new NotImplementedException();
                    break;
                default:
                    throw new NotImplementedException();
                    break;
            }

            return provider.StartProcess(config, parameter);
        }


        /// <summary>
        /// 获取实例详情
        /// </summary>
        /// <param name="type"></param>
        /// <param name="config"></param>
        /// <param name="message"></param>
        /// <exception cref="NotImplementedException"></exception>
        public static object GetDingInstanceDetail(ConfigTypeEnum type, BaseConfig config, object parameter)
        {
            OABaseProvider provider = null;
            switch (type)
            {
                case ConfigTypeEnum.WeChat:
                    throw new NotImplementedException();
                    break;
                case ConfigTypeEnum.DingTalk:
                    provider = new DingTalkOAProvider();
                    break;
                case ConfigTypeEnum.EMail:
                    throw new NotImplementedException();
                    break;
                default:
                    throw new NotImplementedException();
                    break;
            }

            return provider.GetDingInstanceDetail(config, parameter);
        }


        /// <summary>
        /// 审批通过
        /// </summary>
        /// <param name="type"></param>
        /// <param name="config"></param>
        /// <param name="message"></param>
        /// <exception cref="NotImplementedException"></exception>
        public static object AgreeProcess(ConfigTypeEnum type, BaseConfig config, object parameter)
        {
            OABaseProvider provider = null;
            switch (type)
            {
                case ConfigTypeEnum.WeChat:
                    throw new NotImplementedException();
                    break;
                case ConfigTypeEnum.DingTalk:
                    provider = new DingTalkOAProvider();
                    break;
                case ConfigTypeEnum.EMail:
                    throw new NotImplementedException();
                    break;
                default:
                    throw new NotImplementedException();
                    break;
            }

            return provider.AccectProcess(config, parameter);
        }

        /// <summary>
        /// 审批通过
        /// </summary>
        /// <param name="type"></param>
        /// <param name="config"></param>
        /// <param name="message"></param>
        /// <exception cref="NotImplementedException"></exception>
        public static object RefuseProcess(ConfigTypeEnum type, BaseConfig config, object parameter)
        {
            OABaseProvider provider = null;
            switch (type)
            {
                case ConfigTypeEnum.WeChat:
                    throw new NotImplementedException();
                    break;
                case ConfigTypeEnum.DingTalk:
                    provider = new DingTalkOAProvider();
                    break;
                case ConfigTypeEnum.EMail:
                    throw new NotImplementedException();
                    break;
                default:
                    throw new NotImplementedException();
                    break;
            }

            return provider.RefuseProcess(config, parameter);
        }

        /// <summary>
        /// 审批流程回调
        /// </summary>
        /// <param name="type"></param>
        /// <param name="config"></param>
        /// <param name="parameter"></param>
        /// <param name="callbackAction"></param>
        /// <exception cref="NotImplementedException"></exception>
        public static void Callback(ConfigTypeEnum type, BaseConfig config, object parameter, Action<object> callbackAction)
        {
            OABaseProvider provider = null;
            switch (type)
            {
                case ConfigTypeEnum.WeChat:
                    throw new NotImplementedException();
                    break;
                case ConfigTypeEnum.DingTalk:
                    provider = new DingTalkOAProvider();
                    break;
                case ConfigTypeEnum.EMail:
                    throw new NotImplementedException();
                    break;
                default:
                    throw new NotImplementedException();
                    break;
            }

            provider.Callback(config, parameter, callbackAction);
        }
    }
}
