using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Notify.Tools.Wechat.DTO.Message.MessageDto.Template
{
    public class TemplateButtonInDto : BaseMessageInDto
    {
        public string msgtype { get; set; } = "template_card";
        public Template_Card_Btuuton template_card { get; set; }
        public int enable_id_trans { get; set; }
    }

    public class Template_Card_Btuuton
    {
        public string card_type { get; set; } = "button_interaction";
        public Source source { get; set; }
        public Action_Menu action_menu { get; set; }
        public Main_Title main_title { get; set; }
        public Quote_Area quote_area { get; set; }
        public string sub_title_text { get; set; }
        public Horizontal_Content_List[] horizontal_content_list { get; set; }
        public Card_Action card_action { get; set; }
        public string task_id { get; set; }
        public Button_Selection button_selection { get; set; }
        public Button_List[] button_list { get; set; }
    }

    public class Button_Selection
    {
        public string question_key { get; set; }
        public string title { get; set; }
        public Option_List[] option_list { get; set; }
        public string selected_id { get; set; }
    }

    public class Option_List
    {
        public string id { get; set; }
        public string text { get; set; }
    }


    public class Button_List
    {
        public string text { get; set; }
        public int style { get; set; }
        public string key { get; set; }
    }

}
