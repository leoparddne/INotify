namespace Common.Notify.DTO.Contact
{
    public class AttachFileConfig
    {
        /// <summary>
        /// 只需要传递文件ID字段，其他字段为内部使用的公共字段
        /// </summary>
        public string FileID { get; set; }

        public string FilePath { get; set; }
        public string FileName { get; set; }
    }
}
