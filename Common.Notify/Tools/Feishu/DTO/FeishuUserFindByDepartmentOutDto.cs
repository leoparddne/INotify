using System.Collections.Generic;

namespace Common.Notify.Tools.Feishu.DTO
{
    /// <summary>
    /// 部门下的用户列表（contact/v3/users/find_by_department）
    /// </summary>
    public class FeishuUserFindByDepartmentOutDto : FeishuCommonOutDto
    {
        public FeishuUserFindByDepartmentData data { get; set; }
    }

    public class FeishuUserFindByDepartmentData
    {
        public List<FeishuUserItem> items { get; set; }
        public string page_token { get; set; }
        public bool has_more { get; set; }
    }

    public class FeishuUserItem
    {
        public string union_id { get; set; }
        public string user_id { get; set; }
        public string open_id { get; set; }
        public string name { get; set; }
        public string en_name { get; set; }
    }
}
