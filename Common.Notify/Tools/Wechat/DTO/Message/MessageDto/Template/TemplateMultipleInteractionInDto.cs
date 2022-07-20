using Common.Notify.Tools.Wechat.DTO.Message.MessageDto;

namespace Common.Notify.Tools.Wechat.DTO.Message.MessageDto.Template
{
    public class TemplateMultipleInteractionInDto : BaseMessageInDto
    {
        public string msgtype { get; set; } = "template_card";
        public Template_CardMultiple template_card { get; set; }
        public int enable_id_trans { get; set; }
    }

    public class Template_CardMultiple
    {
        public string card_type { get; set; } = "multiple_interaction";
        public Source source { get; set; }
        public Main_Title main_title { get; set; }
        public string task_id { get; set; }
        public Select_List[] select_list { get; set; }
        public Submit_Button submit_button { get; set; }
    }


    public class Select_List
    {
        public string question_key { get; set; }
        public string title { get; set; }
        public string selected_id { get; set; }
        public Option_List[] option_list { get; set; }
    }
}
