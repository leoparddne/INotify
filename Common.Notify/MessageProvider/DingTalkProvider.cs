using Common.Notify.Config;
using Common.Notify.DTO;
using Common.Notify.DTO.Contact;
using Common.Notify.Tools.DingTalk;
using Common.Notify.Tools.DingTalk.DTO;
using Common.Toolkit.Extention;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Notify.MessageProvider
{
    public class DingTalkProvider : BaseProvider
    {
        public override void SendMessage(BaseConfig config, SendTextInDto dto)
        {
            if (config is not DingTalkConfig dingTalkConfig)
            {
                throw new Exception("Error DingTalk Config");
            }

            var msg = new Msg
            {
                text = new Text
                {
                    content = dto.Content
                },
                msgtype = "text",
                //markdown = new Markdown
                //{
                //    //text
                //    title = dto.Content,
                //    //text = dto.Subject
                //},
                //msgtype = "markdown"
            };
            msg.file = new File
            {
                media_id = ""
            };

            var para = new SendMessageInDto
            {
                agent_id = dingTalkConfig.AgentId,
                msg = msg,
                to_all_user = false,
                userid_list = string.Join(',', dto.SendTo)
            };


            DingTalkHelper.SendMessage(para, dingTalkConfig.AppKey, dingTalkConfig.AppSecret, config.Timeout);
        }

        public override List<DepartmentOutDto> GetDepartmentList(BaseConfig config)
        {
            if (config is not DingTalkConfig dingTalkConfig)
            {
                throw new Exception("Error DingTalk Config");
            }

            var data = DingTalkHelper.QueryAllDepartment(dingTalkConfig.AppKey, dingTalkConfig.AppSecret, config.Timeout);
            if (data.IsNullOrEmpty())
            {
                return null;
            }

            return data.Select(f => new DepartmentOutDto
            {
                Id = f.dept_id.ToString(),
                ParentId = f.parent_id.ToString(),
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
            if (config is not DingTalkConfig dingTalkConfig)
            {
                throw new Exception("Error DingTalk Config");
            }


            var result = new List<UserOutDto>();

            int index = 0;
            int step = 60;
            bool isEnd = false;
            foreach (var deptId in departIds)
            {
                while (!isEnd)
                {
                    var data = DingTalkHelper.ListUser(deptId, index, step, dingTalkConfig.AppKey, dingTalkConfig.AppSecret, config.Timeout);
                    foreach (var item in data.list)
                    {
                        result.Add(new UserOutDto
                        {
                            UserId = item.userid,
                            Name = item.name
                        });
                    }
                    if (!data.has_more)
                    {
                        break;
                    }

                    index += step;
                }
            }

            return result;
        }
    }
}
