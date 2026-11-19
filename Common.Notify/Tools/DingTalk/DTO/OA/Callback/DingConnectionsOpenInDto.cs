using System.Collections.Generic;

namespace Common.Notify.Tools.DingTalk.DTO.OA.Callback
{
    public class DingConnectionsOpenInDto
    {
        /// <summary>
        /// 企业自建：Appkey
        /// </summary>
        public string clientId { get; set; }

        /// <summary>
        /// 企业自建：AppSecret
        /// </summary>
        public string clientSecret { get; set; }

        /// <summary>
        /// 订阅信息，平台将根据此信息判断是否通过stream方式推送给当前客户端
        /// </summary>
        public List<DingTopicOption> subscriptions { get; set; }

        /// <summary>
        /// 标记客户端类型，格式为name-sdk-lang/version,例如java版本的sdk可命名为dingtalk-sdk-java/1.0.0
        /// </summary>
        public string ua { get; set; }

        /// <summary>
        /// 运行SDK服务的当前本地IP，方便定位排查连接问题
        /// </summary>
        public string localIp { get; set; }
    }


    public class DingTopicOption
    {
        /// <summary>
        /// 订阅事件类型
        /// EVENT : 事件类型
        /// CALLBACK : 回调类型
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 订阅的具体业务topic，
        /// 事件类型统一填写*，
        /// 回调类型根据平台提供的topic填写，例如机器人回调为/v1.0/im/bot/messages/get, 卡片回调为/v1.0/card/instances/callback
        /// </summary>
        public string topic { get; set; }
    }

}
