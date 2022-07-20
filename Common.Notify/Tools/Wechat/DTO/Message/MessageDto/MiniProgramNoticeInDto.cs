using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Notify.Tools.Wechat.DTO.Message.MessageDto
{
    public class MiniProgramNoticeInDto : BaseMessageInDto
    {
        public string msgtype { get; set; } = "miniprogram_notice";
        public Miniprogram_Notice miniprogram_notice { get; set; }
        public int enable_id_trans { get; set; }
    }

    public class Miniprogram_Notice
    {
        public string appid { get; set; }
        public string page { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public bool emphasis_first_item { get; set; }
        public Content_Item[] content_item { get; set; }
    }

    public class Content_Item
    {
        public string key { get; set; }
        public string value { get; set; }
    }

}
