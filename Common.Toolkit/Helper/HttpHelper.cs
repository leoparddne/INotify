using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Common.Toolkit.Helper
{
    /// <summary>
    /// HTTP 请求帮助类
    /// </summary>
    public class HttpHelper : IDisposable
    {
        private readonly HttpClient _httpClient;

        private readonly JsonSerializerOptions _jsonOptions;

        public HttpHelper(double timeout = 30.0)
        {
            SocketsHttpHandler handler = new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(2.0),
                PooledConnectionIdleTimeout = TimeSpan.FromMinutes(1.0),
                MaxConnectionsPerServer = 50,
                UseProxy = false,
                ConnectTimeout = TimeSpan.FromSeconds(5.0)
            };
            _httpClient = new HttpClient(handler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        /// <summary>
        /// 设置超时时长（秒）
        /// </summary>
        public void SetTimeout(double second)
        {
            _httpClient.Timeout = TimeSpan.FromSeconds(second);
        }

        public async Task<TResponse> PostAsync<TResponse>(string url, object request, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            using HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = JsonContent.Create(request, null, _jsonOptions)
            };
            AddHeaders(httpRequest, headers);
            using HttpResponseMessage response = await _httpClient.SendAsync(httpRequest, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            response.EnsureSuccessStatusCode();
            if (response.Content.Headers.ContentLength == 0)
            {
                return default;
            }
            return JsonConvert.DeserializeObject<TResponse>(await response.Content.ReadAsStringAsync(cancellationToken));
        }

        public async Task<TResponse> GetAsync<TResponse>(string url, Dictionary<string, string> headers = null, CancellationToken cancellationToken = default)
        {
            using HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, url);
            AddHeaders(httpRequest, headers);
            using HttpResponseMessage response = await _httpClient.SendAsync(httpRequest, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            response.EnsureSuccessStatusCode();
            if (response.Content.Headers.ContentLength == 0)
            {
                return default;
            }
            return JsonConvert.DeserializeObject<TResponse>(await response.Content.ReadAsStringAsync(cancellationToken));
        }

        public TResponse Post<TResponse>(string url, object request, Dictionary<string, string> headers = null)
        {
            return PostAsync<TResponse>(url, request, headers).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public TResponse Get<TResponse>(string url, Dictionary<string, string> headers = null)
        {
            return GetAsync<TResponse>(url, headers).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        private static void AddHeaders(HttpRequestMessage request, Dictionary<string, string> headers)
        {
            if (headers == null)
            {
                return;
            }
            foreach (KeyValuePair<string, string> header in headers)
            {
                request.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }
        }

        public void Dispose()
        {
        }
    }
}
