using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Notify.Tools.Mail
{
    public class MailInDto
    {
        public string Subject { get; internal set; }
        public string Content { get; internal set; }
        public List<string> MailTo { get; internal set; }
    }
}
