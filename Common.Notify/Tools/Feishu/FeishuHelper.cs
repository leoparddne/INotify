using Common.Notify.Config;
using Common.Notify.Tools.Feishu.DTO;
using Common.Notify.Tools.Feishu.Util;
using System.Collections.Generic;

namespace Common.Notify.Tools.Feishu
{
    public static class FeishuHelper
    {
        public static string GetToken(string app_id, string app_secret, int timeout = 10)
        {
            var result = FeishuHttpUtil.Post<FeishuTokenOutDto>($"auth/v3/tenant_access_token/internal", new
            {
                app_id,
                app_secret
            }, null, timeout);

            return result.tenant_access_token;
        }


        public static void SendMsg(string token, string receive_id, string msg_type, string content, string uuid, int timeout = 10)
        {
            var result = FeishuHttpUtil.Post<FeishuTokenOutDto>($"im/v1/messages?receive_id_type=user_id", new
            {
                receive_id,
                msg_type,
                content,
                //uuid = uuid

            }, new Dictionary<string, string>
            {
                { "Authorization",$"Bearer {token}"}
            }, timeout);
        }


        public static string ReplyMsg(string token, string messageID, string msg_type, string content, string uuid, int timeout = 10)
        {
            var result = FeishuHttpUtil.Post<FeishuTokenOutDto>($"im/v1/messages/{messageID}/reply", new
            {
                content,
                msg_type,
                //uuid = uuid

            }, new Dictionary<string, string>
            {
                { "Authorization",$"Bearer {token}"}
            }, timeout);

            return result.tenant_access_token;
        }

        public static void BatchSendMsg(FeishuConfig feishuConfig, List<string> receive_id, string msg_type, string content, int timeout)
        {
            var token = FeishuHelper.GetToken(feishuConfig.APPID, feishuConfig.APPSecret, feishuConfig.Timeout);

            var result = FeishuHttpUtil.Post<FeishuTokenOutDto>($"message/v4/batch_send/", new
            {
                user_ids = receive_id,
                msg_type,
                //batch_send 要求 content 为 JSON 对象，而非字符串
                content = new { text = content },

            }, new Dictionary<string, string>
            {
                { "Authorization",$"Bearer {token}"}
            }, timeout);
        }

        /// <summary>
        /// 获取企业下所有部门（递归获取子部门）
        /// 文档：https://open.feishu.cn/document/directory-v1/department/filter
        /// </summary>
        public static List<FeishuDepartmentItem> GetDepartmentList(FeishuConfig feishuConfig, int timeout = 10)
        {
            var token = GetToken(feishuConfig.APPID, feishuConfig.APPSecret, timeout);

            var result = new List<FeishuDepartmentItem>();
            //根部门 id 为 0，从根部门开始逐层获取子部门
            result.AddRange(GetChildDepartmentList(token, "0", timeout));

            return result;
        }

        /// <summary>
        /// 按父部门 id 分页获取子部门
        /// </summary>
        private static List<FeishuDepartmentItem> GetChildDepartmentList(string token, string parentDepartmentId, int timeout)
        {
            var result = new List<FeishuDepartmentItem>();
            string pageToken = null;

            do
            {
                var data = FeishuHttpUtil.Post<FeishuDepartmentFilterOutDto>($"directory/v1/departments/filter?department_id_type=open_department_id", new
                {
                    filter = new
                    {
                        conditions = new[]
                        {
                            new { field = "parent_department_id", @operator = "eq", value = $"\"{parentDepartmentId}\"" }
                        }
                    },
                    required_fields = new[]
                    {
                        "department_id",
                        "parent_department_id",
                        "name"
                    },
                    page_request = new
                    {
                        page_size = 100,
                        page_token = pageToken
                    }
                }, new Dictionary<string, string>
                {
                    { "Authorization",$"Bearer {token}"}
                }, timeout);

                if (data.data?.departments != null)
                {
                    foreach (var item in data.data.departments)
                    {
                        result.Add(item);

                        //递归获取子部门
                        result.AddRange(GetChildDepartmentList(token, item.department_id, timeout));
                    }
                }

                pageToken = data.data?.page_response?.has_more == true ? data.data.page_response.page_token : null;
            } while (!string.IsNullOrEmpty(pageToken));

            return result;
        }

        /// <summary>
        /// 获取部门下的用户列表
        /// 文档：https://open.feishu.cn/document/server-docs/contact-v3/user/find_by_department
        /// </summary>
        public static List<FeishuUserItem> GetUserListByDepartment(FeishuConfig feishuConfig, string departmentId, int timeout = 10)
        {
            var token = GetToken(feishuConfig.APPID, feishuConfig.APPSecret, timeout);

            var result = new List<FeishuUserItem>();
            string pageToken = null;

            do
            {
                var url = $"contact/v3/users/find_by_department?department_id={departmentId}&department_id_type=open_department_id&user_id_type=user_id&page_size=50";
                if (!string.IsNullOrEmpty(pageToken))
                {
                    url += $"&page_token={pageToken}";
                }

                var data = FeishuHttpUtil.Get<FeishuUserFindByDepartmentOutDto>(url, null, new Dictionary<string, string>
                {
                    { "Authorization",$"Bearer {token}"}
                }, timeout);

                if (data.data?.items != null)
                {
                    result.AddRange(data.data.items);
                }

                pageToken = data.data?.has_more == true ? data.data.page_token : null;
            } while (!string.IsNullOrEmpty(pageToken));

            return result;
        }
    }
}
