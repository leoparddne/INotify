using Common.Notify.DTO.Contact;
using System.Collections.Generic;

namespace Common.Notify.DTO.Message
{
    public class CommonSendMessageInDto : MessageConfigInDto
    {
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 对应平台userid/邮箱地址
        /// </summary>
        public List<string> UsersIds { get; set; }

        public List<string> GroupIds { get; set; }

        /// <summary>
        /// 主题-发送邮件时使用
        /// </summary>
        public string Subject { get; set; }

        public List<AttachFileConfig> AttachFileList { get; set; }

        public string BizID { get; set; }

        public string RedirectPath { get; set; }
    }
}
