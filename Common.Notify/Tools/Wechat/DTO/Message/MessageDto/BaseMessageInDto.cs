using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Notify.Tools.Wechat.DTO.Message.MessageDto
{
    public class BaseMessageInDto
    {
        public string touser { get; set; }
        public string toparty { get; set; }
        public string totag { get; set; }

        /// <summary>
        /// 企业应用id-小程序通知消息时不需要此字段
        /// </summary>
        public int agentid { get; set; }

        /// <summary>
        /// 表示是否开启重复消息检查，0表示否，1表示是，默认0
        /// </summary>
        public int enable_duplicate_check { get; set; }

        /// <summary>
        /// 表示是否重复消息检查的时间间隔，默认1800s，最大不超过4小时
        /// </summary>
        public int duplicate_check_interval { get; set; }
    }
}
