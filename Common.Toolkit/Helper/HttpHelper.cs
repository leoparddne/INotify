namespace Common.Toolkit.Helper
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;

    public class HttpHelper : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;
        private bool _disposed = false;

        public HttpHelper()
        {
            var handler = new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(2),
                PooledConnectionIdleTimeout = TimeSpan.FromMinutes(1),
                MaxConnectionsPerServer = 50,
                UseProxy = false,
                ConnectTimeout = TimeSpan.FromSeconds(5),
            };

            _httpClient = new HttpClient(handler);

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        /// <summary>
        /// 异步 POST 请求
        /// </summary>
        public async Task<TResponse> PostAsync<TResponse>(
            string url,
            object request,
            Dictionary<string, string> headers = null,
            CancellationToken cancellationToken = default)
        {
            using var httpRequest = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = JsonContent.Create(request, options: _jsonOptions)
            };

            AddHeaders(httpRequest, headers);

            using var response = await _httpClient.SendAsync(
                httpRequest,
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken);

            response.EnsureSuccessStatusCode();

            if (response.Content.Headers.ContentLength == 0)
                return default;

            var json = await response.Content.ReadAsStringAsync(cancellationToken);
            var result = JsonConvert.DeserializeObject<TResponse>(json);
            return result;
        }

        /// <summary>
        /// 异步 GET 请求
        /// </summary>
        public async Task<TResponse> GetAsync<TResponse>(
            string url,
            Dictionary<string, string> headers = null,
            CancellationToken cancellationToken = default)
        {
            using var httpRequest = new HttpRequestMessage(HttpMethod.Get, url);
            AddHeaders(httpRequest, headers);

            using var response = await _httpClient.SendAsync(
                httpRequest,
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken);

            response.EnsureSuccessStatusCode();

            if (response.Content.Headers.ContentLength == 0)
                return default;

            var json = await response.Content.ReadAsStringAsync(cancellationToken);

            var result = JsonConvert.DeserializeObject<TResponse>(json);
            return result;
        }

        /// <summary>
        /// 同步 POST 请求（通过异步方法实现）
        /// </summary>
        public TResponse Post<TResponse>(
            string url,
            object request,
            Dictionary<string, string> headers = null,
            CancellationToken cancellationToken = default)
        {
            return PostAsync<TResponse>(url, request, headers, cancellationToken)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
        }

        /// <summary>
        /// 同步 GET 请求（通过异步方法实现）
        /// </summary>
        public TResponse Get<TResponse>(
            string url,
            Dictionary<string, string> headers = null,
            CancellationToken cancellationToken = default)
        {
            return GetAsync<TResponse>(url, headers, cancellationToken)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
        }

        private static void AddHeaders(HttpRequestMessage request, Dictionary<string, string>? headers)
        {
            if (headers == null) return;

            foreach (var header in headers)
            {
                request.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _httpClient?.Dispose();
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}