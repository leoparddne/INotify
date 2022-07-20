namespace Common.Notify.Config
{
    public class DingTalkConfig : BaseConfig
    {
        /// <summary>
        /// app id
        /// </summary>
        public string AgentId { get; set; }

        /// <summary>
        /// 已创建的企业内部应用的AppKey
        /// </summary>
        public string AppKey { get; set; }

        /// <summary>
        /// 已创建的企业内部应用的AppSecret
        /// </summary>
        public string AppSecret { get; set; }
    }
}
