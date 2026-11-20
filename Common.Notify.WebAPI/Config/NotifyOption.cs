namespace Common.Notify.WebAPI.Config
{
    /// <summary>
    /// 通知服务配置
    /// </summary>
    public class NotifyOption
    {
        /// <summary>
        /// 请求超时时间（秒）
        /// </summary>
        public int Timeout { get; set; } = 10;
    }
}
