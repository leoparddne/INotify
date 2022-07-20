using Common.Notify.Tools.DingTalk;
using Common.Notify.Tools.DingTalk.DTO;
using Common.Notify.Tools.DingTalk.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Common.Notify.Test
{
    [TestClass]
    public class DingTalkHelperTest
    {
        [TestMethod]
        public void GetAccessToken()
        {
            string token = DingTalkHelper.GetAccessToken("appKey", "apSecret");
            System.Console.WriteLine(token);
        }


        [TestMethod]
        public void TestSend()
        {
            string token = DingTalkHelper.GetAccessToken("appKey", "apSecret");
            System.Console.WriteLine(token);
        }



        [TestMethod]
        public void QueryAllDepartment()
        {
            System.Collections.Generic.List<DepartmentItemOutDto> result = DingTalkHelper.QueryAllDepartment("appKey", "apSecret");

            System.Console.WriteLine(JsonConvert.SerializeObject(result));
        }


        [TestMethod]
        public void ListUser()
        {
            UserResultOutDto result = DingTalkHelper.ListUser("deptId", 0, 10, "appKey", "apSecret");

            System.Console.WriteLine(JsonConvert.SerializeObject(result));
        }

        [TestMethod]
        public void SendMessage()
        {
            SendMessageInDto dto = new SendMessageInDto()
            {

                agent_id = "agent_id",
                userid_list = "userid_list",
            };
            Action_Card card = new Action_Card
            {
                title = "是透出到会话列表和通知的文案",
                markdown = "支持markdown格式的正文内容",
                btn_orientation = "1",
                btn_json_list = new System.Collections.Generic.List<Btn_Json_List>
                    {
                        new   Btn_Json_List{
                                title= "一个按钮",
                            action_url= "https=//www.taobao.com"
                        },
                        new   Btn_Json_List{
                                title= "两个按钮",
                            action_url= "https=//www.tmall.com"
                        }
                    }
            };
            Msg msg = new Msg
            {
                action_card = card
            };
            dto.msg = msg;
            dto.msg.SetMsgType(MsgTypeEnum.ActionCard);

            DingTalkHelper.SendMessage(dto, "appKey", "apSecret");
        }
        
        [TestMethod]
        public void GetUserByPhone()
        {
            var result = DingTalkHelper.GetUserByPhone("appKey", "apSecret", "13812071740",false);

            System.Console.WriteLine(JsonConvert.SerializeObject(result));
        }
    }
}
