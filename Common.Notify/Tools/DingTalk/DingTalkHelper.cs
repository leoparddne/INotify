using Common.Notify.Tools.DingTalk.DTO;
using Common.Notify.Tools.DingTalk.Util;
using System.Collections.Generic;

namespace Common.Notify.Tools.DingTalk
{
    public static class DingTalkHelper
    {
        /// <summary>
        /// 使用 Token 初始化账号Client
        /// </summary>
        /// <returns></returns>
        public static AlibabaCloud.SDK.Dingtalkoauth2_1_0.Client CreateClient()
        {
            AlibabaCloud.OpenApiClient.Models.Config config = new()
            {
                Protocol = "https",
                RegionId = "central"
            };

            return new AlibabaCloud.SDK.Dingtalkoauth2_1_0.Client(config);
        }

        /// <summary>
        /// 获取访问token
        /// </summary>
        /// <returns></returns>
        public static string GetAccessToken(string appKey, string appSecret)
        {
            AlibabaCloud.SDK.Dingtalkoauth2_1_0.Client client = CreateClient();
            AlibabaCloud.SDK.Dingtalkoauth2_1_0.Models.GetAccessTokenRequest getAccessTokenRequest = new()
            {
                AppKey = appKey,
                AppSecret = appSecret
            };

            AlibabaCloud.SDK.Dingtalkoauth2_1_0.Models.GetAccessTokenResponse token = client.GetAccessToken(getAccessTokenRequest);

            return token.Body.AccessToken;
        }


        public static List<DepartmentItemOutDto> QueryAllDepartment(string appKey, string appSecret)
        {
            string token = GetAccessToken(appKey, appSecret);

            #region 旧代码 
            //AlibabaCloud.OpenApiClient.Models.Config config = new AlibabaCloud.OpenApiClient.Models.Config();
            //config.Protocol = "https";
            //config.RegionId = "central";
            //config.SecurityToken = token;
            //AlibabaCloud.SDK.Dingtalkindustry_1_0.Client industry = new(config);

            //QueryAllDepartmentRequest request = new ();
            //AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            //QueryAllDepartmentHeaders headers = new QueryAllDepartmentHeaders();
            //headers.XAcsDingtalkAccessToken = token;


            //industry.QueryAllDepartmentWithOptions(request, headers, runtime);


            #endregion

            return DingTalkHttpUtil.Get<List<DepartmentItemOutDto>>($"v2/department/listsub?access_token={token}");
        }


        public static UserResultOutDto ListUser(string deptId, int cursor, int size, string appKey, string appSecret)
        {
            string token = GetAccessToken(appKey, appSecret);

            return DingTalkHttpUtil.Post<UserResultOutDto>($"v2/user/list?access_token={token}",
                new
                {
                    dept_id = deptId,
                    cursor,
                    size
                });
        }

        public static void SendMessage(SendMessageInDto dto, string appKey, string appSecret)
        {
            string token = GetAccessToken(appKey, appSecret);

            DingTalkHttpUtil.Post<UserResultOutDto>($"message/corpconversation/asyncsend_v2?access_token={token}",
              dto);
        }
        
            /// <summary>
        /// 根据手机号获取uerid
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="appSecret"></param>
        /// <param name="phone"></param>
        /// <param name="support_exclusive_account_search">是否支持通过手机号搜索专属帐号。</param>
        public static UserPhone GetUserByPhone(string appKey, string appSecret,string phone,bool support_exclusive_account_search)
        {
            string token = GetAccessToken(appKey, appSecret);

            return DingTalkHttpUtil.Post<UserPhone>($"v2/user/getbymobile?access_token={token}",
              new
              {
                  mobile = phone,
                  support_exclusive_account_search= support_exclusive_account_search
              });
        }
    }
}
