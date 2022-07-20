using System.Collections.Generic;

namespace Common.Notify.Tools.Wechat.DTO
{
    public class DepartmentOutDto : WechatCommonOutDto
    {
        public List<DepartmentItem> department { get; set; }
    }


    public class DepartmentItem
    {
        public int id { get; set; }
        public string name { get; set; }
        public int parentid { get; set; }
        public int order { get; set; }
        public object[] department_leader { get; set; }
    }
}
