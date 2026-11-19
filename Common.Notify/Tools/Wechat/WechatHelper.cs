using Common.Notify.Tools.Wechat.DTO;
using Common.Notify.Tools.Wechat.DTO.Message;
using Common.Notify.Tools.Wechat.DTO.Message.MessageDto;
using Common.Notify.Tools.Wechat.Util;

namespace Common.Notify.Tools.Wechat
{
    public static class WechatHelper
    {
        public static string GetToken(string corpid, string corpsecret, int timeout = 10)
        {
            AccessTokenOutDto result = WechatHttpUtil.Get<AccessTokenOutDto>($"gettoken?corpid={corpid}&corpsecret={corpsecret}", null, null, timeout);

            return result.access_token;
        }

        /// <summary>
        /// 获取部门
        /// </summary>
        /// <param name="corpid"></param>
        /// <param name="corpsecret"></param>
        /// <returns></returns>
        public static WeChatDepartmentOutDto GetDepartment(string corpid, string corpsecret, int timeout = 10)
        {
            var token = GetToken(corpid, corpsecret);

            var result = WechatHttpUtil.Get<WeChatDepartmentOutDto>($"department/list?access_token={token}", null, null, timeout);

            return result;
        }

        public static DepartmentUserSimple GetDepartmentUser(string corpid, string corpsecret, int departmentId, int timeout = 10)
        {
            var token = GetToken(corpid, corpsecret);

            var departmentUser = WechatHttpUtil.Get<DepartmentUserSimple>($"user/simplelist?access_token={token}&department_id={departmentId}", null, null, timeout);

            return departmentUser;
        }


        public static DepartmentUserDetail GetDepartmentUserDetail(string corpid, string corpsecret, int departmentId, int timeout = 10)
        {
            var token = GetToken(corpid, corpsecret);

            var result = WechatHttpUtil.Get<DepartmentUserDetail>($"user/list?access_token={token}&department_id={departmentId}", null, null, timeout);


            return result;
        }

        /// <summary>
        /// https://developer.work.weixin.qq.com/document/path/90236
        /// </summary>
        /// <param name="corpid"></param>
        /// <param name="corpsecret"></param>

        public static SendMessageOutDto SendMessage(string corpid, string corpsecret, BaseMessageInDto message, int timeout = 10)
        {
            var token = GetToken(corpid, corpsecret);

            var result = WechatHttpUtil.Post<SendMessageOutDto>($"message/send?access_token={token}", message, null, timeout);

            return result;
        }


        /// <summary>
        /// 根据手机号获取userid
        /// </summary>
        /// <param name="corpid"></param>
        /// <param name="corpsecret"></param>
        /// <param name="mobile"></param>
        public static WechatUserOutDto GetUserIdByPhone(string corpid, string corpsecret, string mobile, int timeout = 10)
        {
            var token = GetToken(corpid, corpsecret);
            var result = WechatHttpUtil.Post<WechatUserOutDto>($"user/getuserid?access_token={token}", new
            {
                mobile
            }, null, timeout);

            return result;
        }
    }
}
