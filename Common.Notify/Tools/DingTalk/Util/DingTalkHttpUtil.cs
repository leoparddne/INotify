using Common.Notify.Tools.DingTalk.DTO;
using Common.Notify.Tools.DingTalk.DTO.OA;
using Common.Toolkit.Helper;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Common.Notify.Tools.DingTalk.Util
{
    public static class DingTalkHttpUtil
    {
        /// <summary>
        /// https://oapi.dingtalk.com/
        /// </summary>
        public static readonly string baseUrl = "https://oapi.dingtalk.com/";

        /// <summary>
        /// https://api.dingtalk.com/
        /// </summary>
        public static readonly string baseUrl_api_ding = "https://api.dingtalk.com/";

        /// <summary>
        /// 共享的 httphelper 实例，避免多次创建对象
        /// </summary>
        public static HttpHelper httpHelperEx = new();


        public static string GetBaseUrl(string newBaseURL)
        {
            if (string.IsNullOrEmpty(newBaseURL))
            {
                return baseUrl;
            }

            return newBaseURL;
        }

        public static T Get<T>(string url, int timeout = 10, string baseUrlStr = null, Dictionary<string, string> Headers = null)
        {
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(timeout));
            var result = httpHelperEx.Get<CommonOutDto<T>>($"{GetBaseUrl(baseUrlStr)}{url}", Headers, cts.Token);

            return GetData(result);
        }


        /// <summary>
        /// 传递自定义的url - 不使用内部baseurl
        /// </summary>
        public static T GetRaw<T>(string url, int timeout = 10, Dictionary<string, string> Headers = null)
        {
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(timeout));
            var result = httpHelperEx.Get<T>($"{url}", Headers, cts.Token);

            return result;
        }

        public static T Post<T>(string url, object bodyData, int timeout = 10, string baseUrlStr = null, Dictionary<string, string> Headers = null)
        {
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(timeout));
            var result = httpHelperEx.Post<CommonOutDto<T>>($"{GetBaseUrl(baseUrlStr)}{url}", bodyData, Headers, cts.Token);

            return GetData(result);
        }

        public static T PostRaw<T>(string url, object bodyData, int timeout = 10, string baseUrlStr = null, Dictionary<string, string> Headers = null)
        {
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(timeout));
            var result = httpHelperEx.Post<T>($"{GetBaseUrl(baseUrlStr)}{url}", bodyData, Headers, cts.Token);

            return result;
        }

        public static void CheckOAData(DingOACommonOutDto data)
        {
            if (string.IsNullOrEmpty(data.message))
            {
                return;
            }

            throw new Exception(data.message);
        }

        public static T GetData<T>(CommonOutDto<T> data)
        {
            if (data.errcode == 0)
            {
                return data.result;
            }

            throw new Exception(data.errmsg);
        }
    }
}
