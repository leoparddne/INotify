using Common.Notify.Tools.Wechat.DTO;
using Common.Notify.Tools.Wechat.DTO.Message;
using Common.Notify.Tools.Wechat.DTO.Message.MessageDto;
using Common.Notify.Tools.Wechat.Util;

namespace Common.Notify.Tools.Wechat
{
    public static class WechatHelper
    {
        public static string GetToken(string corpid, string corpsecret)
        {
            AccessTokenOutDto result = WechatHttpHelper.Get<AccessTokenOutDto>($"gettoken?corpid={corpid}&corpsecret={corpsecret}");

            return result.access_token;
        }

        /// <summary>
        /// 获取部门
        /// </summary>
        /// <param name="corpid"></param>
        /// <param name="corpsecret"></param>
        /// <returns></returns>
        public static DepartmentOutDto GetDepartment(string corpid, string corpsecret)
        {
            var token = GetToken(corpid, corpsecret);

            var result = WechatHttpHelper.Get<DepartmentOutDto>($"department/list?access_token={token}");

            return result;
        }

        public static DepartmentUserSimple GetDepartmentUser(string corpid, string corpsecret, int departmentId)
        {
            var token = GetToken(corpid, corpsecret);

            var departmentUser = WechatHttpHelper.Get<DepartmentUserSimple>($"user/simplelist?access_token={token}&department_id={departmentId}");

            return departmentUser;
        }


        public static DepartmentUserDetail GetDepartmentUserDetail(string corpid, string corpsecret, int departmentId)
        {
            var token = GetToken(corpid, corpsecret);

            var result = WechatHttpHelper.Get<DepartmentUserDetail>($"user/list?access_token={token}&department_id={departmentId}");


            return result;
        }

        /// <summary>
        /// https://developer.work.weixin.qq.com/document/path/90236
        /// </summary>
        /// <param name="corpid"></param>
        /// <param name="corpsecret"></param>

        public static SendMessageOutDto SendMessage(string corpid, string corpsecret, BaseMessageInDto message)
        {
            var token = GetToken(corpid, corpsecret);

            var result = WechatHttpHelper.Post<SendMessageOutDto>($"message/send?access_token={token}", message);

            return result;
        }
        
        /// <summary>
        /// 根据手机号获取userid
        /// </summary>
        /// <param name="corpid"></param>
        /// <param name="corpsecret"></param>
        /// <param name="mobile"></param>
        public static UserOutDto GetUserIdByPhone(string corpid, string corpsecret, string mobile)
        {
            var token = GetToken(corpid, corpsecret);
            var result = WechatHttpHelper.Post<UserOutDto>($"user/getuserid?access_token={token}", new
            {
                mobile= mobile
            });

            return result;
        }
    }
}
