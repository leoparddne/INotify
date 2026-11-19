using Common.Notify.Tools.Feishu.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Notify.Tools.Feishu.Util
{
    public static class FeishuHttpHelper
    {
        public static readonly string baseUrl = "https://open.feishu.cn/open-apis/";

        public static T Get<T>(string url, Dictionary<string, object> parameter = null, Dictionary<string, string> Headers = null, int timeout = 10000) where T : FeishuCommonOutDto
        {
            Task<T> result = HttpHelper.GetAsync<T>($"{baseUrl}{url}", parameter, Headers, timeout);
            result.Wait();

            return GetData(result.Result);
        }

        public static T Post<T>(string url, object bodyData, Dictionary<string, string> Headers = null, int timeout = 10000) where T : FeishuCommonOutDto
        {
            Task<T> result = HttpHelper.PostAsync<T>($"{baseUrl}{url}", bodyData, Headers, timeout);
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
