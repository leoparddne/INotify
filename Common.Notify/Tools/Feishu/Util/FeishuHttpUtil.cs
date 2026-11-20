using Common.Notify.Tools.Feishu.DTO;
using Common.Toolkit.Helper;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Notify.Tools.Feishu.Util
{
    public static class FeishuHttpUtil
    {
        public static readonly string baseUrl = "https://open.feishu.cn/open-apis/";

        /// <summary>
        /// 共享的 httphelper 实例，避免多次创建对象
        /// </summary>
        public static HttpHelper httpHelperEx = new();

        public static T Get<T>(string url, Dictionary<string, object> parameter = null, Dictionary<string, string> Headers = null, int timeout = 10) where T : FeishuCommonOutDto
        {
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(timeout));
            Task<T> result = httpHelperEx.GetAsync<T>($"{baseUrl}{url}", Headers, cts.Token);
            result.Wait();

            return GetData(result.Result);
        }

        public static T Post<T>(string url, object bodyData, Dictionary<string, string> Headers = null, int timeout = 10) where T : FeishuCommonOutDto
        {
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(timeout));
            Task<T> result = httpHelperEx.PostAsync<T>($"{baseUrl}{url}", bodyData, Headers, cts.Token);
            result.Wait();

            return GetData(result.Result);
        }

        public static T GetData<T>(T data) where T : FeishuCommonOutDto
        {
            if (data.code == 0)
            {
                return data;
            }

            throw new Exception(data.msg);
        }
    }
}
