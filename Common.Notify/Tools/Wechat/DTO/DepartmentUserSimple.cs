using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Notify.Tools.Wechat.DTO
{
    public class DepartmentUserSimple : WechatCommonOutDto
    {
        public List<UserItemSimple> userlist { get; set; }
    }



    public class UserItemSimple
    {
        public string name { get; set; }
        public int[] department { get; set; }
        public string userid { get; set; }
    }

}
