namespace Common.Notify.Tools.Wechat.DTO.Message.MessageDto
{
    public class VoiceInDto : BaseMessageInDto
    {
        public string msgtype { get; set; } = "voice";
        public Voice voice { get; set; }
    }

    public class Voice
    {
        public string media_id { get; set; }
    }

}
