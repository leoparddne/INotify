using System.Collections.Generic;

namespace Common.Notify.DTO
{
    public class SendTextInDto
    {
        /// <summary>
        /// 主题-发送邮件时使用
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// 发送的目标
        /// </summary>
        public List<string> SendTo { get; set; }

        /// <summary>
        /// 发送的文本
        /// </summary>
        public string Content { get; set; }
    }
}
