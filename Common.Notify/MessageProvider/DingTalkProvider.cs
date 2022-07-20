using Common.Notify.Config;
using Common.Notify.DTO;
using Common.Notify.Tools.DingTalk;
using Common.Notify.Tools.DingTalk.DTO;

namespace Common.Notify.MessageProvider
{
    public class DingTalkProvider : BaseProvider
    {

        public void SendMessage(BaseConfig config, object message)
        {
            if (config is not DingTalkConfig dingTalkConfig)
            {
                throw new System.Exception("Error DingTalk Config");
            }

            if (message is not SendMessageInDto sendData)
            {
                throw new System.Exception("Error DingTalk MessageData");
            }

            DingTalkHelper.SendMessage(sendData, dingTalkConfig.AppKey, dingTalkConfig.AppSecret);
        }

        public void SendMessage(BaseConfig config, SendTextInDto dto)
        {
            if (config is not DingTalkConfig dingTalkConfig)
            {
                throw new System.Exception("Error DingTalk Config");
            }

            DingTalkHelper.SendMessage(new SendMessageInDto
            {
                agent_id = dingTalkConfig.AgentId,
                msg = new Msg
                {
                    text = new Text
                    {
                        content = dto.Content
                    },
                    msgtype = "text"
                },

                to_all_user = false,
                userid_list = string.Join(',', dto.SendTo)
            }, dingTalkConfig.AppKey, dingTalkConfig.AppSecret);
        }

    }
}
