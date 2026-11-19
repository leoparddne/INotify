namespace Common.Notify.Tools.DingTalk.DTO.OA
{
    public class TemplateSchema
    {
        public string creatorUserId { get; set; }
        public string appUuid { get; set; }
        public string formCode { get; set; }
        public string formUuid { get; set; }
        public string name { get; set; }
        public string memo { get; set; }
        public string ownerIdType { get; set; }
        public Schemacontent schemaContent { get; set; }
        public string icon { get; set; }
        public int appType { get; set; }
        public string bizType { get; set; }
        public int engineType { get; set; }
        public string status { get; set; }
        public int listOrder { get; set; }
        public string customSetting { get; set; }
        public string procType { get; set; }
        public string visibleRange { get; set; }
        public string gmtCreate { get; set; }
        public string gmtModified { get; set; }
    }

    public class Schemacontent
    {
        public string title { get; set; }
        public string icon { get; set; }
        public Item[] items { get; set; }
    }

    public class Item
    {
        public string componentName { get; set; }
        public Props props { get; set; }
        public Child[] children { get; set; }
    }

    public class Props
    {
        public string id { get; set; }
        public string label { get; set; }
        public string bizAlias { get; set; }
        public bool required { get; set; }
        public string placeholder { get; set; }
        public string[] options { get; set; }
        public int appId { get; set; }
        public string durationLabel { get; set; }
        public int pushToCalendar { get; set; }
        public string align { get; set; }
        public Statfield[] statField { get; set; }
        public bool hideLabel { get; set; }
        public Objoption[] objOptions { get; set; }
        public string format { get; set; }
        public bool pushToAttendance { get; set; }
        public bool labelEditableFreeze { get; set; }
        public Push push { get; set; }
        public string commonBizType { get; set; }
        public bool requiredEditableFreeze { get; set; }
        public string unit { get; set; }
        public bool extract { get; set; }
        public string link { get; set; }
        public bool payEnable { get; set; }
        public bool hidden { get; set; }
        public string bizType { get; set; }
        public bool staffStatusEnabled { get; set; }
        public string actionName { get; set; }
        public string attendTypeLabel { get; set; }
        public Childfieldvisible childFieldVisible { get; set; }
        public string notPrint { get; set; }
        public bool verticalPrint { get; set; }
        public bool duration { get; set; }
        public Holidayoption[] holidayOptions { get; set; }
        public bool useCalendar { get; set; }
        public bool hiddenInApprovalDetail { get; set; }
        public bool disabled { get; set; }
        public bool asyncCondition { get; set; }
        public Behaviorlinkage[] behaviorLinkage { get; set; }
        public bool showAttendOptions { get; set; }
        public string notUpper { get; set; }
        public string fieldsInfo { get; set; }
        public bool eSign { get; set; }
        public string mainTitle { get; set; }
        public string formula { get; set; }
        public int choice { get; set; }
    }

    public class Push
    {
        public int pushSwitch { get; set; }
        public string pushTag { get; set; }
        public int attendanceRule { get; set; }
    }

    public class Childfieldvisible
    {
        public bool key { get; set; }
    }

    public class Statfield
    {
        public string id { get; set; }
        public string label { get; set; }
        public bool upper { get; set; }
        public string unit { get; set; }
    }

    public class Objoption
    {
        public string value { get; set; }
    }

    public class Holidayoption
    {
        public string key { get; set; }
    }

    public class Behaviorlinkage
    {
        public string value { get; set; }
        public Target[] targets { get; set; }
    }

    public class Target
    {
        public string fieldId { get; set; }
        public string behavior { get; set; }
    }

    public class Child
    {
        public string componentName { get; set; }
        public Props1 props { get; set; }
    }

    public class Props1
    {
        public string id { get; set; }
        public string label { get; set; }
        public string bizAlias { get; set; }
        public bool required { get; set; }
    }

}
