using Common.Notify.Tools.Wechat;
using Common.Notify.Tools.Wechat.DTO.Message.MessageDto;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Common.Notify.Test
{
    [TestClass]
    public class WechatHelperTest
    {
        [TestMethod]
        public void GetToken()
        {
            string token = WechatHelper.GetToken("corpid", "corpsecret");
            System.Console.WriteLine(token);
        }


        [TestMethod]
        public void GetDepartment()
        {
            Tools.Wechat.DTO.DepartmentOutDto depart = WechatHelper.GetDepartment("corpid", "corpsecret");
            System.Console.WriteLine(JsonConvert.SerializeObject(depart));
        }


        [TestMethod]
        public void GetDepartUser()
        {
            Tools.Wechat.DTO.DepartmentUserSimple depart = WechatHelper.GetDepartmentUser("corpid", "corpsecret", 1);
            System.Console.WriteLine(JsonConvert.SerializeObject(depart));
        }


        [TestMethod]
        public void GetDepartUserDetail()
        {
            Tools.Wechat.DTO.DepartmentUserDetail depart = WechatHelper.GetDepartmentUserDetail("corpid", "corpsecret", 1);
            System.Console.WriteLine(JsonConvert.SerializeObject(depart));
        }

        [TestMethod]
        public void SendMessage()
        {
            TextInDto dto = new TextInDto()
            {
                agentid = 123654,
                duplicate_check_interval = 1800,
                touser = "xxx",
                toparty = "",
                msgtype = "text",
                text = new Text
                {
                    content = "你的快递已到，请携带工卡前往邮件中心领取。\n出发前可查看<a href=\"http://work.weixin.qq.com\">邮件中心视频实况</a>，聪明避开排队。"
                }

            };
            WechatHelper.SendMessage("corpid", "corpsecret", dto);
        }
        
         [TestMethod]
        public void GetUserPhone()
        {
            var depart = WechatHelper.GetUserIdByPhone("xx", "V_-ewHd17-xx", "xx");
            System.Console.WriteLine(JsonConvert.SerializeObject(depart));
        }
    }
}
