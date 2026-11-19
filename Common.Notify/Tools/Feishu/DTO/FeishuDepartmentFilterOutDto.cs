using System.Collections.Generic;

namespace Common.Notify.Tools.Feishu.DTO
{
    /// <summary>
    /// 部门列表（directory/v1/departments/filter）
    /// </summary>
    public class FeishuDepartmentFilterOutDto : FeishuCommonOutDto
    {
        public FeishuDepartmentFilterData data { get; set; }
    }

    public class FeishuDepartmentFilterData
    {
        public List<FeishuDepartmentItem> departments { get; set; }
        public FeishuPageResponse page_response { get; set; }
    }

    public class FeishuPageResponse
    {
        public string page_token { get; set; }
        public bool has_more { get; set; }
    }

    public class FeishuDepartmentItem
    {
        /// <summary>
        /// 部门 ID，类型与请求的 department_id_type 一致
        /// </summary>
        public string department_id { get; set; }
        public FeishuI18nText name { get; set; }
        public string parent_department_id { get; set; }
    }

    /// <summary>
    /// 多语言文本字段（如部门名称）
    /// </summary>
    public class FeishuI18nText
    {
        public string default_value { get; set; }
        public Dictionary<string, string> i18n_value { get; set; }
    }
}
