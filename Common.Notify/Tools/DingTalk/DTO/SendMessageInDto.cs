using Common.Notify.Tools.DingTalk.Enums;
using System.Collections.Generic;

namespace Common.Notify.Tools.DingTalk.DTO
{
    /// <summary>
    /// https://open.dingtalk.com/document/orgapp-server/message-types-and-data-format
    /// </summary>
    public class SendMessageInDto
    {
        /// <summary>
        /// 消息内容，最长不超过2048个字节
        /// </summary>
        public Msg msg { get; set; }

        /// <summary>
        /// 是否发送给企业全部用户。
        /// </summary>
        public bool to_all_user { get; set; } = false;

        /// <summary>
        /// 发送消息时使用的微应用的AgentID。
        /// </summary>
        public string agent_id { get; set; }

        /// <summary>
        /// 接收者的部门id列表，最大列表长度20。
        /// 接收者是部门ID时，包括子部门下的所有用户。
        /// 多个部门使用英文逗号分隔
        /// </summary>
        public string dept_id_list { get; set; }

        /// <summary>
        /// 接收者的userid列表，最大用户列表长度100。
        /// 多个用户使用英文逗号分隔
        /// </summary>
        public string userid_list { get; set; }
    }

    public class Msg
    {
        public Voice voice { get; set; }
        public Image image { get; set; }
        public Oa oa { get; set; }
        public File file { get; set; }
        public Action_Card action_card { get; set; }
        public Link link { get; set; }
        public Markdown markdown { get; set; }
        public Text text { get; set; }
        public string msgtype { get; set; }

        public void SetMsgType(MsgTypeEnum type)
        {
            msgtype = type.GetDesc();
        }
    }

    public class Voice
    {
        public string duration { get; set; }
        public string media_id { get; set; }
    }

    public class Image
    {
        public string media_id { get; set; }
    }

    public class Oa
    {
        public Head head { get; set; }
        public string pc_message_url { get; set; }
        public Status_Bar status_bar { get; set; }
        public Body body { get; set; }
        public string message_url { get; set; }
    }

    public class Head
    {
        public string bgcolor { get; set; }
        public string text { get; set; }
    }

    public class Status_Bar
    {
        public string status_value { get; set; }
        public string status_bg { get; set; }
    }

    public class Body
    {
        public string file_count { get; set; }
        public string image { get; set; }
        public Form form { get; set; }
        public string author { get; set; }
        public Rich rich { get; set; }
        public string title { get; set; }
        public string content { get; set; }
    }

    public class Form
    {
        public string value { get; set; }
        public string key { get; set; }
    }

    public class Rich
    {
        public string unit { get; set; }
        public string num { get; set; }
    }

    public class File
    {
        public string media_id { get; set; }
    }

    public class Action_Card
    {
        public List<Btn_Json_List> btn_json_list { get; set; }
        public string single_url { get; set; }
        public string btn_orientation { get; set; }
        public string single_title { get; set; }
        public string markdown { get; set; }
        public string title { get; set; }
    }

    public class Btn_Json_List
    {
        public string action_url { get; set; }
        public string title { get; set; }
    }

    public class Link
    {
        public string picUrl { get; set; }
        public string messageUrl { get; set; }
        public string text { get; set; }
        public string title { get; set; }
    }

    public class Markdown
    {
        public string text { get; set; }
        public string title { get; set; }
    }

    public class Text
    {
        public string content { get; set; }
    }

}
