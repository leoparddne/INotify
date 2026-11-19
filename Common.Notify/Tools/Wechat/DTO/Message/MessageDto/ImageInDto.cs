namespace Common.Notify.Tools.Wechat.DTO.Message.MessageDto
{
    public class ImageInDto : BaseMessageInDto
    {
        public string msgtype { get; set; } = "image";
        public Image image { get; set; }
        public int safe { get; set; }

    }

    public class Image
    {
        public string media_id { get; set; }
    }

}
