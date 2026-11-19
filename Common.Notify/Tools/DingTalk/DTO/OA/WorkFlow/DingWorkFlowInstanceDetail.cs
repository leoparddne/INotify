using System.Collections.Generic;

namespace Common.Notify.Tools.DingTalk.DTO.OA.WorkFlow
{
    public class DingWorkFlowInstanceDetail : DingOACommonOutDto
    {
        public Result result { get; set; }
        public string success { get; set; }
    }

    public class Result
    {
        public string title { get; set; }
        public string finishTime { get; set; }
        public string originatorUserId { get; set; }
        public string originatorDeptId { get; set; }
        public string originatorDeptName { get; set; }
        public string status { get; set; }
        public string[] approverUserIds { get; set; }
        public string[] ccUserIds { get; set; }
        public string result { get; set; }
        public string businessId { get; set; }
        public List<Operationrecord> operationRecords { get; set; }
        public List<DingWorkFlowDetailTask> tasks { get; set; }
        public string bizAction { get; set; }
        public string bizData { get; set; }
        public string[] attachedProcessInstanceIds { get; set; }
        public string mainProcessInstanceId { get; set; }
        public List<Formcomponentvalue> formComponentValues { get; set; }
        public string createTime { get; set; }
    }

    public class Operationrecord
    {
        public string userId { get; set; }
        public string date { get; set; }
        public string type { get; set; }
        public string result { get; set; }
        public string remark { get; set; }
        public List<Attachment> attachments { get; set; }
        public List<string> ccUserIds { get; set; }
    }


    public class DingWorkFlowDetailTask
    {
        public long taskId { get; set; }
        public string userId { get; set; }
        public string status { get; set; }
        public string result { get; set; }
        public string createTime { get; set; }
        public string finishTime { get; set; }
        public string mobileUrl { get; set; }
        public string pcUrl { get; set; }
        public string processInstanceId { get; set; }
        public string activityId { get; set; }
    }

}
