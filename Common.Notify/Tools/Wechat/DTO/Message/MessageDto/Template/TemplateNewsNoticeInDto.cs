using Common.Notify.Tools.Wechat.DTO.Message.MessageDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Notify.Tools.Wechat.DTO.Message.MessageDto.Template
{
    /// <summary>
    /// 图文
    /// </summary>
    public class TemplateNewsNoticeInDto : BaseMessageInDto
    {
        public string msgtype { get; set; } = "template_card";
        public Template_CardNews template_card { get; set; }
        public int enable_id_trans { get; set; }
    }

    public class Template_CardNews
    {
        public string card_type { get; set; } = "news_notice";
        public Source source { get; set; }
        public Action_Menu action_menu { get; set; }
        public string task_id { get; set; }
        public Main_Title main_title { get; set; }
        public Quote_Area quote_area { get; set; }
        public Image_Text_Area image_text_area { get; set; }
        public Card_Image card_image { get; set; }
        public Vertical_Content_List[] vertical_content_list { get; set; }
        public Horizontal_Content_List[] horizontal_content_list { get; set; }
        public Jump_List[] jump_list { get; set; }
        public Card_Action card_action { get; set; }
    }

    public class Image_Text_Area
    {
        public int type { get; set; }
        public string url { get; set; }
        public string title { get; set; }
        public string desc { get; set; }
        public string image_url { get; set; }
    }

    public class Card_Image
    {
        public string url { get; set; }
        public float aspect_ratio { get; set; }
    }


    public class Vertical_Content_List
    {
        public string title { get; set; }
        public string desc { get; set; }
    }

}
