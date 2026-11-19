namespace Common.Notify.Config
{
    public class BaseConfig
    {
        /// <summary>
        /// 超时时长（秒），在各类最终发送请求的帮助类中转换为毫秒
        /// </summary>
        public int Timeout { get; set; } = 10;
    }
}
