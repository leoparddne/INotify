using Common.Notify.Config;
using Common.Notify.DTO;
using Common.Notify.DTO.Contact;
using System;
using System.Collections.Generic;

namespace Common.Notify.MessageProvider
{
    public abstract class BaseProvider
    {
        /// <summary>
        /// 发送基础的文本通知数据
        /// </summary>
        /// <param name="config"></param>
        /// <param name="dto"></param>
        public abstract void SendMessage(BaseConfig config, SendTextInDto dto);


        public virtual List<DepartmentOutDto> GetDepartmentList(BaseConfig config)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取部门下的所有用户
        /// </summary>
        /// <param name="config"></param>
        /// <param name="departIds"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual List<UserOutDto> GetUserListByDepartment(BaseConfig config, List<string> departIds)
        {
            throw new NotImplementedException();
        }
    }
}