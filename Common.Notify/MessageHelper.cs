using Common.Notify.Config;
using Common.Notify.DTO;
using Common.Notify.DTO.Contact;
using Common.Notify.DTO.Message;
using Common.Notify.Enums;
using Common.Notify.MessageProvider;
using Common.Toolkit.Extention;
using Common.Toolkit.Helper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Notify
{
    /// <summary>
    /// 
    /// </summary>
    public static class MessageHelper
    {
        /// <summary>
        /// 发送基础文本格式数据
        /// </summary>
        /// <param name="type"></param>
        /// <param name="config"></param>
        /// <param name="message"></param>
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
                case ConfigTypeEnum.Feishu:
                    provider = new FeishuProvider();
                    break;
                default:
                    throw new NotImplementedException();
                    break;
            }

            provider.SendMessage(config, dto);
        }

        /// <summary>
        /// 发送基础文本格式数据-基础通用调用
        /// </summary>
        /// <param name="dto"></param>
        public static void SendText(CommonSendMessageInDto dto)
        {
            BaseConfig config = NotifyConfigHelper.GetConfig(dto);

            //获取用户列表
            var data = MapperHelper.AutoMap<CommonSendMessageInDto, GetDepartmentInDto>(dto);
            data.Id = dto.GroupIds;

            List<string> users = new List<string>();
            if (!data.Id.IsNullOrEmpty() && (dto.ConfigType == ConfigTypeEnum.WeChat || dto.ConfigType == ConfigTypeEnum.DingTalk))
            {
                List<UserOutDto> groupUser = ContactHelper.GetUserListByDepartment(data);
                if (!groupUser.IsNullOrEmpty())
                {
                    users.AddRange(groupUser.Select(f => f.UserId));
                }
            }

            if (!dto.UsersIds.IsNullOrEmpty())
            {
                users.AddRange(dto.UsersIds);
            }

            if (users.IsNullOrEmpty())
            {
                return;
            }

            SendText(dto.ConfigType, config, new SendTextInDto
            {
                Subject = dto.Subject,
                Content = dto.Content,
                SendTo = users,
                AttachFileList = dto.AttachFileList,
                BizID = dto.BizID,
                RedirectPath = dto.RedirectPath,
            });
        }
    }
}
