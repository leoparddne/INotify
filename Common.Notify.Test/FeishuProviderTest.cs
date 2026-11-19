using Common.Notify.Config;
using Common.Notify.DTO;
using Common.Notify.MessageProvider;
using Common.Notify.Tools.Feishu;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Linq;

namespace Common.Notify.Test
{
    [TestClass]
    public class FeishuProviderTest
    {
        private static readonly FeishuConfig Config = new FeishuConfig
        {
            APPID = "appId",
            APPSecret = "appSecret"
        };

        /// <summary>
        /// 测试组织内用户的 user_id
        /// </summary>
        private static readonly System.Collections.Generic.List<string> TestUserIds = new System.Collections.Generic.List<string> { "userId" };

        /// <summary>
        /// 获取 tenant_access_token
        /// </summary>
        [TestMethod]
        public void GetToken()
        {
            var token = FeishuHelper.GetToken(Config.APPID, Config.APPSecret);

            System.Console.WriteLine(token);
        }

        /// <summary>
        /// 群发文本消息（批量发送给多个用户）
        /// </summary>
        [TestMethod]
        public void SendMessage()
        {
            var feishu = new FeishuProvider();

            feishu.SendMessage(Config, new SendTextInDto
            {
                SendTo = TestUserIds,
                Content = "群发测试消息"
            });
        }

        /// <summary>
        /// 获取部门列表
        /// </summary>
        [TestMethod]
        public void GetDepartmentList()
        {
            var feishu = new FeishuProvider();

            var list = feishu.GetDepartmentList(Config);

            System.Console.WriteLine(JsonConvert.SerializeObject(list));
        }

        /// <summary>
        /// 获取部门下的用户
        /// </summary>
        [TestMethod]
        public void GetUserListByDepartment()
        {
            var feishu = new FeishuProvider();

            var departments = feishu.GetDepartmentList(Config);
            var departIds = departments?.Select(d => d.Id).ToList() ?? new System.Collections.Generic.List<string>();

            var users = feishu.GetUserListByDepartment(Config, departIds);

            System.Console.WriteLine(JsonConvert.SerializeObject(users));
        }
    }
}
