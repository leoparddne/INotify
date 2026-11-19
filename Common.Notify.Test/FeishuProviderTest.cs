using Common.Notify.Config;
using Common.Notify.DTO;
using Common.Notify.MessageProvider;
using Common.Notify.Tools.Feishu;
using Newtonsoft.Json;

namespace Common.Notify.Test
{
    [TestClass]
    public sealed class FeishuProviderTest
    {
        private static readonly FeishuConfig Config = new FeishuConfig
        {
            APPID = "appId",
            APPSecret = "appSecret"
        };

        /// <summary>
        /// 测试组织内用户的 user_id
        /// </summary>
        private static readonly List<string> TestUserIds = new List<string> { "9d92fged" };

        /// <summary>
        /// 获取 tenant_access_token
        /// </summary>
        [TestMethod]
        public void GetToken()
        {
            var token = FeishuHelper.GetToken(Config.APPID, Config.APPSecret, Config.Timeout);

            Assert.IsFalse(string.IsNullOrEmpty(token), "获取 token 失败");
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

            Assert.IsTrue(list == null || list.Count >= 0);
            Console.WriteLine(JsonConvert.SerializeObject(list));

        }

        /// <summary>
        /// 获取部门下的用户
        /// </summary>
        [TestMethod]
        public void GetUserListByDepartment()
        {
            var feishu = new FeishuProvider();

            var departments = feishu.GetDepartmentList(Config);
            var departIds = departments?.Select(d => d.Id.ToString()).ToList() ?? new List<string>();

            var users = feishu.GetUserListByDepartment(Config, departIds);

            Assert.IsNotNull(users);
            Console.WriteLine(JsonConvert.SerializeObject(users));
        }
    }
}
