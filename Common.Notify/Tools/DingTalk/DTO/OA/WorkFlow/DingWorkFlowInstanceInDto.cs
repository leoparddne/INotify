using System.Collections.Generic;

namespace Common.Notify.Tools.DingTalk.DTO.OA.WorkFlow
{
    public class DingWorkFlowInstanceInDto
    {
        public List<Approver> approvers { get; set; }
        public string originatorUserId { get; set; }
        public string processCode { get; set; }
        public List<Formcomponentvalue> formComponentValues { get; set; }
    }

    public class Approver
    {
        public List<string> userIds { get; set; }
    }

    public class Formcomponentvalue
    {
        public string id { get; set; }
        public string name { get; set; }
        public string value { get; set; }
        public string extValue { get; set; }
        public string componentType { get; set; }
        public string bizAlias { get; set; }
    }
}
