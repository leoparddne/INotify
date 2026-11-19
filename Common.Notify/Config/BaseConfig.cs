namespace Common.Notify.Config
{
    public class BaseConfig
    {
        /// <summary>
        /// 超时时长（秒），各 provider 内部按所用库的要求转换为毫秒
        /// </summary>
        public int Timeout { get; set; } = 50;
    }
}
