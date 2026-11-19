using Common.Notify.Config;
using Common.Notify.Tools.DingTalk.DTO;
using Common.Notify.Tools.DingTalk.DTO.OA;
using Common.Notify.Tools.DingTalk.DTO.OA.Callback;
using Common.Notify.Tools.DingTalk.DTO.OA.WorkFlow;
using Common.Notify.Tools.DingTalk.Util;
using Common.Toolkit.Extention;
using Common.Toolkit.Helper;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Common.Notify.Tools.DingTalk
{
    public static class DingTalkHelper
    {
        private const string EventType = "EVENT";
        private const string SystemType = "SYSTEM";
        private const string PingTopic = "ping";
        private const string DisconnectTopic = "disconnect";
        private const string DisconnectReason = "Disconnect by dingtalk stream server, Ok for reconnect.";
        private static ConcurrentDictionary<string, (string, DateTime)> TokenDic = new();

        /// <summary>
        /// 获取访问token
        /// </summary>
        /// <returns></returns>
        public static string GetAccessToken(string appKey, string appSecret, int timeout = 10)
        {
            if (TokenDic.ContainsKey(appKey))
            {
                var targetTime = TokenDic[appKey].Item2;

                var lastTime = targetTime.AddMinutes(-5);


                if (DateTime.Now < lastTime)
                {
                    return TokenDic[appKey].Item1;
                }
            }

            var expireTime = DateTime.Now;
            TokenOutDto data = DingTalkHttpUtil.GetRaw<TokenOutDto>($"https://oapi.dingtalk.com/gettoken?appkey={appKey}&appsecret={appSecret}", timeout);

            ExceptionHelper.CheckException(data == null || string.IsNullOrEmpty(data.access_token), "无法获取访问权限");
            expireTime = expireTime.AddSeconds(data.expires_in);

            TokenDic.AddOrUpdate(appKey, f =>
            {
                return (data.access_token, expireTime);
            }, (f, v) =>
            {
                return (data.access_token, expireTime);
            });

            return data.access_token;
        }


        public static object AttendanceList(List<string> userIdList, DateTime workDayFrom, DateTime workDayTo, string appKey, string appSecret, int timeout = 10)
        {
            string token = GetAccessToken(appKey, appSecret, timeout);

            var result = DingTalkHttpUtil.PostRaw<object>($"attendance/list?access_token={token}",
                new
                {
                    userIdList,
                    workDateFrom = workDayFrom.ToString("yyyy-MM-dd  00:00:00"),
                    workDateTo = workDayTo.ToString("yyyy-MM-dd  00:00:00"),
                    offset = 0,
                    limit = 10
                }, timeout);

            return result;
        }

        public static List<DepartmentItemOutDto> QueryAllDepartment(string appKey, string appSecret, int timeout = 10)
        {
            string token = GetAccessToken(appKey, appSecret, timeout);

            #region 旧代码 
            //AlibabaCloud.OpenApiClient.Models.Config config = new AlibabaCloud.OpenApiClient.Models.Config();
            //config.Protocol = "https";
            //config.RegionId = "central";
            //config.SecurityToken = token;
            //AlibabaCloud.SDK.Dingtalkindustry_1_0.Client industry = new(config);

            //QueryAllDepartmentRequest request = new ();
            //AlibabaCloud.TeaUtil.Models.RuntimeOptions runtime = new AlibabaCloud.TeaUtil.Models.RuntimeOptions();
            //QueryAllDepartmentHeaders headers = new QueryAllDepartmentHeaders();
            //headers.XAcsDingtalkAccessToken = token;


            //industry.QueryAllDepartmentWithOptions(request, headers, runtime);


            #endregion
            //var rootDepartment= DingTalkHttpUtil.Get<List<DepartmentItemOutDto>>($"v2/department/listsub?access_token={token}");

            //if (!rootDepartment.IsNullOrEmpty())
            //{
            //    //获取子级
            //    foreach (var item in rootDepartment)
            //    {
            //        var child=GetChild(token, item.dept_id);
            //        if (child.IsNullOrEmpty())
            //        {
            //            break;
            //        }
            //        rootDepartment.AddRange(child);
            //    }
            //}

            //return rootDepartment;
            return GetChild(token, 1, timeout);

        }

        private static List<DepartmentItemOutDto> GetChild(string token, int deptID, int timeout = 10)
        {
            string finalDeptID = deptID == 0 ? "" : deptID.ToString();
            var result = new List<DepartmentItemOutDto>();
            var subDepartment = DingTalkHttpUtil.Get<List<DepartmentItemOutDto>>($"topapi/v2/department/listsub?access_token={token}&dept_id={finalDeptID}", timeout);
            if (subDepartment.IsNullOrEmpty())
            {
                return null;
            }
            result.AddRange(subDepartment);

            foreach (var item in subDepartment)
            {
                var data = GetChild(token, item.dept_id, timeout);
                if (data.IsNullOrEmpty())
                {
                    continue;
                }
                result.AddRange(data);
            }
            return result;
        }

        public static UserResultOutDto ListUser(string deptId, int cursor, int size, string appKey, string appSecret, int timeout = 10)
        {
            string token = GetAccessToken(appKey, appSecret, timeout);

            return DingTalkHttpUtil.Post<UserResultOutDto>($"topapi/v2/user/list?access_token={token}",
                new
                {
                    dept_id = deptId,
                    cursor,
                    size
                }, timeout);
        }

        public static void SendMessage(SendMessageInDto dto, string appKey, string appSecret, int timeout = 10)
        {
            string token = GetAccessToken(appKey, appSecret, timeout);

            var result = DingTalkHttpUtil.Post<UserResultOutDto>($"topapi/message/corpconversation/asyncsend_v2?access_token={token}",
              dto, timeout);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="appSecret"></param>
        /// <param name="phone"></param>
        /// <param name="support_exclusive_account_search">是否支持通过手机号搜索专属帐号。</param>
        public static UserPhone GetUserByPhone(string appKey, string appSecret, string phone, bool support_exclusive_account_search, int timeout = 10)
        {
            string token = GetAccessToken(appKey, appSecret, timeout);

            return DingTalkHttpUtil.Post<UserPhone>($"topapi/v2/user/getbymobile?access_token={token}",
              new
              {
                  mobile = phone,
                  support_exclusive_account_search
              }, timeout);
        }

        public static TemplateSchema GetTemplate(string processCode, DingTalkConfig config)
        {
            string token = GetAccessToken(config.AppKey, config.AppSecret, config.Timeout);

            return DingTalkHttpUtil.Get<TemplateSchema>($"v1.0/workflow/forms/schemas/processCodes?processCode={processCode}", config.Timeout, DingTalkHttpUtil.baseUrl_api_ding, new Dictionary<string, string>
            {
                { "x-acs-dingtalk-access-token",token}
            });
        }

        /// <summary>
        /// 获取实例详情
        /// </summary>
        /// <param name="instanceID"></param>
        /// <param name="dingTalkConfig"></param>
        /// <returns></returns>
        public static object GetDingInstanceDetail(string instanceID, DingTalkConfig dingTalkConfig)
        {
            string token = GetAccessToken(dingTalkConfig.AppKey, dingTalkConfig.AppSecret, dingTalkConfig.Timeout);

            var result = DingTalkHttpUtil.GetRaw<DingWorkFlowInstanceDetail>($"{DingTalkHttpUtil.baseUrl_api_ding}/v1.0/workflow/processInstances?processInstanceId={instanceID}",
                dingTalkConfig.Timeout, new Dictionary<string, string>
            {
                { "x-acs-dingtalk-access-token",token}
            });

            DingTalkHttpUtil.CheckOAData(result);

            return result;
        }

        /// <summary>
        /// 发起流程实例
        /// </summary>
        public static object ProcessInstance(DingWorkFlowInstanceInDto parameter, DingTalkConfig config)
        {
            string token = GetAccessToken(config.AppKey, config.AppSecret, config.Timeout);

            var result = DingTalkHttpUtil.PostRaw<DingWorkFlowInstanceOutDto>($"v1.0/workflow/processInstances",
                parameter,
                config.Timeout, DingTalkHttpUtil.baseUrl_api_ding, new Dictionary<string, string>
            {
                { "x-acs-dingtalk-access-token",token}
            });

            DingTalkHttpUtil.CheckOAData(result);

            return result;
        }


        /// <summary>
        /// 发起流程实例
        /// </summary>
        public static object AccectProcess(DingAgreeProcessInDto parameter, DingTalkConfig config)
        {
            string token = GetAccessToken(config.AppKey, config.AppSecret, config.Timeout);

            var result = DingTalkHttpUtil.PostRaw<DingWorkFlowInstanceOutDto>($"v1.0/workflow/processInstances/execute",
                parameter,
                config.Timeout, DingTalkHttpUtil.baseUrl_api_ding, new Dictionary<string, string>
            {
                { "x-acs-dingtalk-access-token",token}
            });

            DingTalkHttpUtil.CheckOAData(result);

            return result;
        }



        /// <summary>
        /// 注册连接凭证
        /// </summary>
        public static object ConnectionsOpen(DingConnectionsOpenInDto parameter, DingTalkConfig config)
        {
            //获取连接信息
            var result = DingTalkHttpUtil.PostRaw<DingConnectionOpenOutDto>($"/v1.0/gateway/connections/open",
                parameter,
                config.Timeout, DingTalkHttpUtil.baseUrl_api_ding, null);

            DingTalkHttpUtil.CheckOAData(result);

            var connectHost = $"{result!.endpoint}?ticket={result.ticket}";

            string host = result.endpoint;

            CancellationTokenSource _cts = new();
            ClientWebSocket _wsClient = new();

            var uri = new Uri(connectHost);
            _wsClient.ConnectAsync(uri, _cts.Token).Wait();

            var x = _wsClient.State;

            StartAsync(_wsClient, _cts.Token);

            Thread.Sleep(1000 * 60 * 5);





            return result;
        }

        public static async Task StartAsync(ClientWebSocket _wsClient, CancellationToken ct)
        {

            var buffer = new Memory<byte>(new byte[4096]);
            while (!ct.IsCancellationRequested)
            {
                try
                {
                    var res = await _wsClient!.ReceiveAsync(buffer, ct);
                    var eventData = Encoding.UTF8.GetString(buffer.Span[..res.Count]);
                    var eventJson = JsonDocument.Parse(eventData);
                    eventJson.RootElement.TryGetProperty("headers", out var headers);
                    eventJson.RootElement.TryGetProperty("type", out var type);
                    switch (type.GetString())
                    {
                        case not null and SystemType when headers.TryGetProperty("topic", out var topic) && topic.GetString() == PingTopic:
                            {
                                await ResponsePingAsync(eventJson, _wsClient, ct);
                                break;
                            }
                        case not null and SystemType when headers.TryGetProperty("topic", out var topic) && topic.GetString() == DisconnectTopic:
                            {
                                await _wsClient.CloseAsync(WebSocketCloseStatus.NormalClosure, DisconnectReason, ct);
                                await Task.Delay(10000, ct);
                                //await ConnectWebSocketAsync(ct);
                                break;
                            }
                        case not null and EventType:
                            {
                                await ResponsePushAsync(eventJson, _wsClient, ct);
                                //if (OnReceivedAsync is not null)
                                //{
                                //    _ = OnReceivedAsync(eventJson);
                                //}
                                //if (OnReceived is not null)
                                //{
                                //    OnReceived(eventJson);
                                //}
                                break;
                            }
                        default:

                            break;
                    }
                }
                catch
                {
                    //await ConnectWebSocketAsync(ct);
                    await Task.Delay(10000, ct);
                }
            }
        }


        private static async Task ResponsePushAsync(JsonDocument eventJson, ClientWebSocket _wsClient, CancellationToken ct = default)
        {
            var messageId = eventJson.RootElement.GetProperty("headers").GetProperty("messageId").GetString();
            var data = eventJson.RootElement.GetProperty("data").GetString();
            var pong = new
            {
                code = 200,
                headers = new
                {
                    contentType = Application.Json,
                    messageId,
                },
                message = "OK",
                data
            };

            //var pongJson = JsonSerializer.Serialize(pong, _dingtalkJsonOptions);
            //方式1
            var pongJson = JsonSerializer.Serialize(pong);
            var pongBytes = Encoding.UTF8.GetBytes(pongJson);

            await _wsClient!.SendAsync(pongBytes, WebSocketMessageType.Text, true, ct);
        }

        private static async Task ResponsePingAsync(JsonDocument eventJson, ClientWebSocket _wsClient, CancellationToken ct = default)
        {
            var messageId = eventJson.RootElement.GetProperty("headers").GetProperty("messageId").GetString();
            var data = eventJson.RootElement.GetProperty("data").GetString();
            var pong = new
            {
                code = 200,
                headers = new
                {
                    contentType = Application.Json,
                    messageId,
                },
                message = "OK",
                data
            };

            //var pongJson = JsonSerializer.Serialize(pong, _dingtalkJsonOptions);
            //方式1
            var pongJson = JsonSerializer.Serialize(pong);
            var pongBytes = Encoding.UTF8.GetBytes(pongJson);

            var px = Encoding.UTF8.GetString(pongBytes);

            ////方式2
            //var res = await CreateReplyMessage(messageId, data);
            //var resx = Encoding.UTF8.GetString(res);

            //await _wsClient!.SendAsync(pongBytes, WebSocketMessageType.Binary, true, ct);
            await _wsClient!.SendAsync(pongBytes, WebSocketMessageType.Text, true, ct);
            //await _wsClient!.SendAsync(res, WebSocketMessageType.Text, true, ct);
        }

        //public static async Task<byte[]> CreateReplyMessage(string messageId, string data)
        //{
        //    using var stream = new MemoryStream();
        //    using var writer = new Utf8JsonWriter(stream);

        //    writer.WriteStartObject();
        //    writer.WriteNumber("code", 200);

        //    writer.WritePropertyName("headers");
        //    writer.WriteStartObject();
        //    writer.WriteString("contentType", "application/json");
        //    writer.WriteString("messageId", messageId);
        //    writer.WriteEndObject();

        //    writer.WriteString("message", "OK");
        //    writer.WriteString("data", data);

        //    writer.WriteEndObject();
        //    await writer.FlushAsync();
        //    stream.Seek(0, SeekOrigin.Begin);

        //    return stream.ToArray();
        //}
    }
}
