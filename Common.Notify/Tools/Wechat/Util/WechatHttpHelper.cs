using Common.Notify.Tools.Wechat.DTO;
using System;
using System.Threading.Tasks;

namespace Common.Notify.Tools.Wechat.Util
{
    public static class WechatHttpHelper
    {
        public static readonly string baseUrl = "https://qyapi.weixin.qq.com/cgi-bin/";
        public static T Get<T>(string url) where T : WechatCommonOutDto
        {
            Task<T> result = HttpHelper.GetAsync<T>($"{baseUrl}{url}");
            result.Wait();

            return GetData(result.Result);
        }

        public static T Post<T>(string url, object bodyData) where T : WechatCommonOutDto
        {
            Task<T> result = HttpHelper.PostAsync<T>($"{baseUrl}{url}", bodyData);
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
