using System.Collections.Generic;

namespace Common.Notify.Tools.DingTalk.DTO.OA.WorkFlow
{
    public class DingAgreeProcessInDto
    {
        public string processInstanceId { get; set; }
        public string remark { get; set; }
        public string result { get; set; }
        public string actionerUserId { get; set; }
        public long taskId { get; set; }
        public DingProcessAgressFile file { get; set; }
    }

    public class DingProcessAgressFile
    {
        public string[] photos { get; set; }
        public List<Attachment> attachments { get; set; }
    }

    public class Attachment
    {
        public string spaceId { get; set; }
        public string fileSize { get; set; }
        public string fileId { get; set; }
        public string fileName { get; set; }
        public string fileType { get; set; }
    }


}
