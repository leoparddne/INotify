using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Notify.Tools.DingTalk.DTO
{
    public class UserResultOutDto
    {
        public bool has_more { get; set; }
        public List<UserResultItem> list { get; set; }
    }

    public class UserResultItem
    {
        public bool active { get; set; }
        public bool admin { get; set; }
        public string avatar { get; set; }
        public bool boss { get; set; }
        public int[] dept_id_list { get; set; }
        public long dept_order { get; set; }
        public bool exclusive_account { get; set; }
        public bool hide_mobile { get; set; }
        public bool leader { get; set; }
        public string name { get; set; }
        public string title { get; set; }
        public string unionid { get; set; }
        public string userid { get; set; }
        public string job_number { get; set; }
    }

}
