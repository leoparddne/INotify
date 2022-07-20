using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Notify.Tools.Wechat.DTO.Message.MessageDto
{
    public class TextCardInDto : BaseMessageInDto
    {
        public string msgtype { get; set; } = "textcard";
        public Textcard textcard { get; set; }
        public int enable_id_trans { get; set; }
    }

    public class Textcard
    {
        public string title { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string btntxt { get; set; }
    }

}
