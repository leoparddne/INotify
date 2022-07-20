namespace Common.Notify.Tools.Wechat.DTO.Message.MessageDto
{
    public class FileInDto : BaseMessageInDto
    {
        public string msgtype { get; set; } = "file";
        public File file { get; set; }
        public int safe { get; set; }
    }

    public class File
    {
        public string media_id { get; set; }
    }

}
