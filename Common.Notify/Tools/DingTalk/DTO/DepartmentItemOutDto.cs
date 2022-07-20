using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Notify.Tools.DingTalk.DTO
{
    public class DepartmentItemOutDto
    {
        public bool auto_add_user { get; set; }
        public bool create_dept_group { get; set; }
        public int dept_id { get; set; }
        public string name { get; set; }
        public int parent_id { get; set; }
    }


}
