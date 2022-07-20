using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Notify.Tools
{
    public static class HttpHelper
    {
        #region Get
        public static async Task<TResult> GetAsync<TResult>(string url, Dictionary<string, object> parameter = null, Dictionary<string, string> Headers = null, int timeout = 10000)
        {
            var result = await GetAsync(url, parameter, Headers, timeout);
            return JsonConvert.DeserializeObject<TResult>(result);
        }
        public static async Task<string> GetAsync(string url, Dictionary<string, object> parameter = null, Dictionary<string, string> Headers = null, int timeout = 10000)
        {
            RestClient client;
            RestRequest request;
            GetInit(url, parameter, Headers, timeout, out client, out request);

            RestResponse response = await client.ExecuteAsync(request);
            return response.Content;
        }

        private static void GetInit(string url, Dictionary<string, object> parameter, Dictionary<string, string> Headers, int timeout, out RestClient client, out RestRequest request)
        {
            client = new RestClient();
            request = new RestRequest(url, Method.Get);
            //request.AddHeader("Content-Type", "application/json");
            request.Timeout = timeout;

            GenerateHeader(request, Headers);

            if (parameter != null)
            {
                foreach (KeyValuePair<string, object> item in parameter)
                {
                    Parameter para = Parameter.CreateParameter(item.Key, item.Value, ParameterType.GetOrPost);
                    request.AddParameter(para);
                }
            }
        }
        #endregion

        #region Post
        public static async Task<TResult> PostAsync<TResult>(string url, object obj = null, Dictionary<string, string> Headers = null, int timeout = 10000)
        {
            var result = await PostAsync(url, obj, Headers, timeout);
            return JsonConvert.DeserializeObject<TResult>(result);
        }
        public static async Task<string> PostAsync(string url, object obj = null, Dictionary<string, string> Headers = null, int timeout = 10000)
        {
            RestClient client;
            RestRequest request;
            PostInit(url, obj, Headers, timeout, out client, out request);

            RestResponse response = await client.ExecuteAsync(request);
            return response.Content;
        }

        private static void PostInit(string url, object obj, Dictionary<string, string> Headers, int timeout, out RestClient client, out RestRequest request)
        {
            client = new RestClient();
            request = new RestRequest(url, Method.Post);
            //request.AddHeader("Content-Type", "application/json");
            request.Timeout = timeout;

            GenerateHeader(request, Headers);

            request.AddJsonBody(obj);
        }

        /// <summary>
        /// 将header添加到请求中
        /// </summary>
        private static void GenerateHeader(RestRequest request, Dictionary<string, string> Headers)
        {
            if (Headers != null && Headers.Count > 0)
            {
                request.AddOrUpdateHeaders(Headers);
            }
        }
        #endregion
    }
}
