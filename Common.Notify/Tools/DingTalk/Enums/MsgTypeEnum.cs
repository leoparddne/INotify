using System.ComponentModel;

namespace Common.Notify.Tools.DingTalk.Enums
{
    public enum MsgTypeEnum
    {
        /// <summary>
        /// 文本消息
        /// </summary>
        [Description("text")]
        TEXT,

        /// <summary>
        /// 图片消息
        /// </summary>
        [Description("image")]
        Image,

        /// <summary>
        /// 语音消息
        /// </summary>
        [Description("voice")]
        Voice,

        /// <summary>
        /// 文件消息
        /// </summary>
        [Description("file")]
        File,

        /// <summary>
        /// 链接消息
        /// </summary>
        [Description("link")]
        Link,

        /// <summary>
        /// OA消息
        /// </summary>
        [Description("oa")]
        OA,

        /// <summary>
        /// markdown消息
        /// </summary>
        [Description("markdown")]
        Markdown,

        /// <summary>
        /// 卡片消息
        /// </summary>
        [Description("action_card")]
        ActionCard,
    }
}
