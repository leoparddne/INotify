using Common.Notify.Tools.Wechat.DTO.Message.MessageDto;

namespace Common.Notify.Tools.Wechat.DTO.Message.MessageDto.Template
{
    public class TemplateVoteInDto : BaseMessageInDto
    {
        public string msgtype { get; set; } = "template_card";
        public Template_CardVote template_card { get; set; }
        public int enable_id_trans { get; set; }
    }

    public class Template_CardVote
    {
        public string card_type { get; set; } = "vote_interaction";
        public Source source { get; set; }
        public Main_Title main_title { get; set; }
        public string task_id { get; set; }
        public Checkbox checkbox { get; set; }
        public Submit_Button submit_button { get; set; }
    }

    public class Checkbox
    {
        public string question_key { get; set; }
        public Option_List[] option_list { get; set; }
        public int mode { get; set; }
    }


    public class Submit_Button
    {
        public string text { get; set; }
        public string key { get; set; }
    }

}
