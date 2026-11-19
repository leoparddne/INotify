using Common.Notify.Config;
using Common.Notify.DTO;
using Common.Notify.DTO.Contact;
using Common.Notify.Tools.Feishu;
using Common.Toolkit.Extention;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Notify.MessageProvider
{
    public class FeishuProvider : BaseProvider
    {
        public override void SendMessage(BaseConfig config, SendTextInDto dto)
        {
            if (config is not FeishuConfig feishuConfig)
            {
                throw new Exception("Error Feishu Config");
            }


            FeishuHelper.BatchSendMsg(feishuConfig, dto.SendTo, "text", dto.Content, config.Timeout);
        }


        public override List<DepartmentOutDto> GetDepartmentList(BaseConfig config)
        {
            if (config is not FeishuConfig feishuConfig)
            {
                throw new Exception("Error Feishu Config");
            }

            var data = FeishuHelper.GetDepartmentList(feishuConfig, config.Timeout);
            if (data.IsNullOrEmpty())
            {
                return null;
            }

            return data.Select(f => new DepartmentOutDto
            {
                Id = f.department_id,
                ParentId = f.parent_department_id,
                Name = f.name?.default_value
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
            if (config is not FeishuConfig feishuConfig)
            {
                throw new Exception("Error Feishu Config");
            }


            List<UserOutDto> result = new List<UserOutDto>();

            foreach (string deptId in departIds)
            {
                var data = FeishuHelper.GetUserListByDepartment(feishuConfig, deptId, config.Timeout);
                if (data.IsNullOrEmpty())
                {
                    continue;
                }

                result.AddRange(data.Select(f => new UserOutDto
                {
                    UserId = f.user_id,
                    Name = f.name
                }));
            }

            return result;
        }
    }
}
