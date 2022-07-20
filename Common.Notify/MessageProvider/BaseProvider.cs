using Common.Notify.Config;
using Common.Notify.DTO;

namespace Common.Notify.MessageProvider
{
    public interface BaseProvider
    {

        /// <summary>
        /// 根据平台发送自定义数据
        /// </summary>
        /// <param name="config"></param>
        /// <param name="message"></param>
        public void SendMessage(BaseConfig config, object message);

        /// <summary>
        /// 发送基础的文本通知数据
        /// </summary>
        /// <param name="config"></param>
        /// <param name="dto"></param>
        public void SendMessage(BaseConfig config, SendTextInDto dto);
    }
}
