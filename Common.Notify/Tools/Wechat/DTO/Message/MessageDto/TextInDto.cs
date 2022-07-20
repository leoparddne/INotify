namespace Common.Notify.Tools.Wechat.DTO.Message.MessageDto
{
    public class TextInDto : BaseMessageInDto
    {
        public string msgtype { get; set; } = "text";
        public Text text { get; set; }
        public int safe { get; set; }
        public int enable_id_trans { get; set; }
    }

    public class Text
    {
        public string content { get; set; }
    }

}
