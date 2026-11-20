using Common.Notify.Config;
using Common.Notify.DTO;
using Common.Notify.DTO.Contact;
using Common.Notify.Tools.Wechat;
using Common.Notify.Tools.Wechat.DTO;
using Common.Notify.Tools.Wechat.DTO.Message.MessageDto;
using Common.Toolkit.Extention;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Notify.MessageProvider
{
    public class WechatProvider : BaseProvider
    {
        public override void SendMessage(BaseConfig config, SendTextInDto dto)
        {
            if (config is not WechatConfig wechatConfig)
            {
                throw new Exception("Error DingTalk Config");
            }
            WechatHelper.SendMessage(wechatConfig.Corpid, wechatConfig.Corpsecret, new TextInDto
            {
                agentid = wechatConfig.AgengId.ToInt(-1),
                touser = string.Join('|', dto.SendTo),
                msgtype = "text",
                duplicate_check_interval = 1800,
                text = new Text
                {
                    content = dto.Content
                }
            }, config.Timeout);
        }


        public override List<DepartmentOutDto> GetDepartmentList(BaseConfig config)
        {
            if (config is not WechatConfig wechatConfig)
            {
                throw new Exception("Error DingTalk Config");
            }

            Tools.Wechat.DTO.WeChatDepartmentOutDto data = WechatHelper.GetDepartment(wechatConfig.Corpid, wechatConfig.Corpsecret, config.Timeout);
            if (data.department.IsNullOrEmpty())
            {
                return null;
            }

            return data.department.Select(f => new DepartmentOutDto
            {
                Id = f.id.ToString(),
                ParentId = f.parentid.ToString(),
                Name = f.name
            }).ToList();
        }

        /// <summary>
        /// 获取部门下的所有用户
        /// </summary>
        /// <param name="config"></param>
        /// <param name="departIds"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override List<UserOutDto> GetUserListByDepartment(BaseConfig config, List<string> departIds)
        {
            if (config is not WechatConfig wechatConfig)
            {
                throw new Exception("Error DingTalk Config");
            }


            List<UserOutDto> result = new List<UserOutDto>();

            foreach (string deptId in departIds)
            {
                if (!int.TryParse(deptId, out int deptIdValue))
                {
                    throw new Exception($"Error deptId:{deptId}");
                }
                DepartmentUserSimple data = WechatHelper.GetDepartmentUser(wechatConfig.Corpid, wechatConfig.Corpsecret, deptIdValue, config.Timeout);

                foreach (UserItemSimple item in data.userlist)
                {
                    result.Add(new UserOutDto
                    {
                        UserId = item.userid,
                        Name = item.name
                    });
                }
            }

            return result;
        }
    }
}
