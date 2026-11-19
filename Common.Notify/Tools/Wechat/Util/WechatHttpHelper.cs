using Common.Notify.Tools.Wechat.DTO;
using System;
using System.Threading.Tasks;

namespace Common.Notify.Tools.Wechat.Util
{
    public static class WechatHttpHelper
    {
        public static readonly string baseUrl = "https://qyapi.weixin.qq.com/cgi-bin/";
        public static T Get<T>(string url, int timeout = 10000) where T : WechatCommonOutDto
        {
            Task<T> result = HttpHelper.GetAsync<T>($"{baseUrl}{url}", null, null, timeout);
            result.Wait();

            return GetData(result.Result);
        }

        public static T Post<T>(string url, object bodyData, int timeout = 10000) where T : WechatCommonOutDto
        {
            Task<T> result = HttpHelper.PostAsync<T>($"{baseUrl}{url}", bodyData, null, timeout);
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
