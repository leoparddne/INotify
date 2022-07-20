using Common.Notify.Tools.DingTalk.DTO;
using System;
using System.Threading.Tasks;

namespace Common.Notify.Tools.DingTalk.Util
{
    public static class DingTalkHttpUtil
    {
        public static readonly string baseUrl = "https://oapi.dingtalk.com/topapi/";
        public static T Get<T>(string url)
        {
            Task<CommonOutDto<T>> result = HttpHelper.GetAsync<CommonOutDto<T>>($"{baseUrl}{url}");
            result.Wait();

            return GetData(result.Result);
        }

        public static T Post<T>(string url, object bodyData)
        {
            Task<CommonOutDto<T>> result = HttpHelper.PostAsync<CommonOutDto<T>>($"{baseUrl}{url}", bodyData);
            result.Wait();

            return GetData(result.Result);
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
