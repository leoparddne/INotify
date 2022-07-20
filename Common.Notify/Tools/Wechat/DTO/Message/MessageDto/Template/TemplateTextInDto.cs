using Common.Notify.Tools.Wechat.DTO.Message.MessageDto;

namespace Common.Notify.Tools.Wechat.DTO.Message.MessageDto.Template
{
    /// <summary>
    /// 文本通知
    /// </summary>
    public class TemplateTextInDto : BaseMessageInDto
    {
        public string msgtype { get; set; } = "template_card";
        public Template_Card template_card { get; set; }
        public int enable_id_trans { get; set; }
    }

    public class Template_Card
    {
        public string card_type { get; set; } = "text_notice";
        public Source source { get; set; }
        public Action_Menu action_menu { get; set; }
        public string task_id { get; set; }
        public Main_Title main_title { get; set; }
        public Quote_Area quote_area { get; set; }
        public Emphasis_Content emphasis_content { get; set; }
        public string sub_title_text { get; set; }
        public Horizontal_Content_List[] horizontal_content_list { get; set; }
        public Jump_List[] jump_list { get; set; }
        public Card_Action card_action { get; set; }
    }

    public class Source
    {
        public string icon_url { get; set; }
        public string desc { get; set; }
        public int desc_color { get; set; }
    }

    public class Action_Menu
    {
        public string desc { get; set; }
        public Action_List[] action_list { get; set; }
    }

    public class Action_List
    {
        public string text { get; set; }
        public string key { get; set; }
    }

    public class Main_Title
    {
        public string title { get; set; }
        public string desc { get; set; }
    }

    public class Quote_Area
    {
        public int type { get; set; }
        public string url { get; set; }
        public string title { get; set; }
        public string quote_text { get; set; }
    }

    public class Emphasis_Content
    {
        public string title { get; set; }
        public string desc { get; set; }
    }

    public class Card_Action
    {
        public int type { get; set; }
        public string url { get; set; }
        public string appid { get; set; }
        public string pagepath { get; set; }
    }

    public class Horizontal_Content_List
    {
        public string keyname { get; set; }
        public string value { get; set; }
        public int type { get; set; }
        public string url { get; set; }
        public string media_id { get; set; }
        public string userid { get; set; }
    }

    public class Jump_List
    {
        public int type { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public string appid { get; set; }
        public string pagepath { get; set; }
    }

}
