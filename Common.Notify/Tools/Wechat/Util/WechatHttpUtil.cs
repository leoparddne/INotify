using Common.Notify.Tools.Wechat.DTO;
using Common.Toolkit.Helper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Notify.Tools.Wechat.Util
{
    public static class WechatHttpUtil
    {
        public static readonly string baseUrl = "https://qyapi.weixin.qq.com/cgi-bin/";

        /// <summary>
        /// 共享的 httphelper 实例，避免多次创建对象
        /// </summary>
        public static HttpHelper httpHelperEx = new();

        public static T Get<T>(string url, Dictionary<string, object> parameter = null, Dictionary<string, string> Headers = null, int timeout = 10) where T : WechatCommonOutDto
        {
            httpHelperEx.SetTimeout(timeout);
            Task<T> result = httpHelperEx.GetAsync<T>($"{baseUrl}{url}", Headers);
            result.Wait();

            return GetData(result.Result);
        }

        public static T Post<T>(string url, object bodyData, Dictionary<string, string> Headers = null, int timeout = 10) where T : WechatCommonOutDto
        {
            httpHelperEx.SetTimeout(timeout);
            Task<T> result = httpHelperEx.PostAsync<T>($"{baseUrl}{url}", bodyData, Headers);
            result.Wait();

            return GetData(result.Result);
        }

        public static T GetData<T>(T data) where T : WechatCommonOutDto
        {
            if (data.errcode == 0)
            {
                return data;
            }

            throw new Exception(data.errmsg);
        }
    }
}
