using Common.Notify.Config;
using Common.Notify.DTO;
using Common.Notify.DTO.Contact;
using Common.Notify.Enums;
using Common.Notify.MessageProvider;
using Common.Toolkit.Extention;
using System.Collections.Generic;

namespace Common.Notify
{
    /// <summary>
    /// 
    /// </summary>
    public static class ContactHelper
    {
        /// <summary>
        /// 获取企业下所有部门
        /// </summary>
        /// <param name="dto"></param>
        public static List<DepartmentOutDto> GetDepartmentList(MessageConfigInDto dto)
        {
            BaseProvider provider = null;
            BaseConfig config = null;
            switch (dto.ConfigType)
            {
                case ConfigTypeEnum.WeChat:
                    provider = new WechatProvider();
                    config = dto.WechatConfig;
                    break;
                case ConfigTypeEnum.DingTalk:
                    provider = new DingTalkProvider();
                    config = dto.DingTalkConfig;
                    break;
                case ConfigTypeEnum.Feishu:
                    provider = new FeishuProvider();
                    config = dto.FeishuConfig;
                    break;
                default:
                    throw new System.NotImplementedException();
                    break;
            }

            return provider.GetDepartmentList(config);
        }

        /// <summary>
        /// 获取部门下的所有用户
        /// </summary>
        /// <param name="type"></param>
        /// <param name="config"></param>
        /// <param name="message"></param>
        public static List<UserOutDto> GetUserListByDepartment(MessageConfigInDto dto, List<string> departIds)
        {
            if (departIds.IsNullOrEmpty())
            {
                return default;
            }
            BaseConfig config = null;
            BaseProvider provider = null;
            switch (dto.ConfigType)
            {
                case ConfigTypeEnum.WeChat:
                    provider = new WechatProvider();
                    config = dto.WechatConfig;
                    break;
                case ConfigTypeEnum.DingTalk:
                    provider = new DingTalkProvider();
                    config = dto.DingTalkConfig;
                    break;
                case ConfigTypeEnum.Feishu:
                    provider = new FeishuProvider();
                    config = dto.FeishuConfig;
                    break;
                default:
                    throw new System.NotImplementedException();
                    break;
            }
            return provider.GetUserListByDepartment(config, departIds);
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static List<UserOutDto> GetUserListByDepartment(GetDepartmentInDto dto)
        {
            List<UserOutDto> user = GetUserListByDepartment(dto, dto.Id);

            return user;
        }
    }
}
