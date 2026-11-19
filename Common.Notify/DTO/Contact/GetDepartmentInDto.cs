using System.Collections.Generic;

namespace Common.Notify.DTO.Contact
{
    public class GetDepartmentInDto : MessageConfigInDto
    {
        /// <summary>
        /// 部门id
        /// </summary>
        public List<string> Id { get; set; }
    }
}
