using Common.Notify.DTO;
using Common.Notify.DTO.Contact;
using Common.Notify.WebAPI.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Common.Notify.WebAPI.Controllers
{
    public class ContactController : BaesController
    {
        private readonly NotifyOption notifyOption;

        public ContactController(IOptions<NotifyOption> notifyOption)
        {
            this.notifyOption = notifyOption.Value;
        }

        /// <summary>
        /// 获取部门列表
        /// </summary>
        [HttpPost]
        public List<DepartmentOutDto> GetDepartmentList(MessageConfigInDto dto)
        {
            dto.Timeout = notifyOption.Timeout;
            var departmentList = ContactHelper.GetDepartmentList(dto);

            return departmentList;
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        [HttpPost]
        public List<UserOutDto> GetUserList(GetDepartmentInDto dto)
        {
            dto.Timeout = notifyOption.Timeout;

            var user = ContactHelper.GetUserListByDepartment(dto, dto.Id);

            return user;
        }
    }
}
