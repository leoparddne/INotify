namespace Common.Notify.Tools.Wechat.DTO.Message.MessageDto
{
    public class MpNewsInDto : BaseMessageInDto
    {
        public string msgtype { get; set; } = "mpnews";
        public Mpnews mpnews { get; set; }
        public int safe { get; set; }
        public int enable_id_trans { get; set; }
    }

    public class Mpnews
    {
        public Article[] articles { get; set; }
    }

    public class Article
    {
        public string title { get; set; }
        public string thumb_media_id { get; set; }
        public string author { get; set; }
        public string content_source_url { get; set; }
        public string content { get; set; }
        public string digest { get; set; }
    }

}
