using Common.Notify.DTO.Contact;
using System.Collections.Generic;

namespace Common.Notify.Tools.Mail
{
    public class MailInDto
    {
        public string Subject { get; set; }
        public string Content { get; set; }
        public List<string> MailTo { get; set; }
        public List<AttachFileConfig> AttachFileList { get; set; }
    }
}
