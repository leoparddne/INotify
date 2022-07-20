using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Notify.Tools.Wechat.DTO
{
    public class DepartmentUserDetail : WechatCommonOutDto
    {
        public List<UserItemDetail> userlist { get; set; }
    }

    public class UserItemDetail
    {
        public string name { get; set; }
        public int[] department { get; set; }
        public string position { get; set; }
        public int status { get; set; }
        public int enable { get; set; }
        public int isleader { get; set; }
        public Extattr extattr { get; set; }
        public int hide_mobile { get; set; }
        public string telephone { get; set; }
        public int[] order { get; set; }
        public int main_department { get; set; }
        public string alias { get; set; }
        public int[] is_leader_in_dept { get; set; }
        public string userid { get; set; }
        public object[] direct_leader { get; set; }
    }

    public class Extattr
    {
        public object[] attrs { get; set; }
    }

}
