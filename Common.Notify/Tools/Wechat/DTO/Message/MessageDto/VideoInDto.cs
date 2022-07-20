namespace Common.Notify.Tools.Wechat.DTO.Message.MessageDto
{
    public class VideoInDto : BaseMessageInDto
    {
        public string msgtype { get; set; } = "video";
        public Video video { get; set; }
        public int safe { get; set; }
    }

    public class Video
    {
        public string media_id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
    }


}
