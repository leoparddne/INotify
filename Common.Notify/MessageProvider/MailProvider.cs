using Common.Notify.Config;
using Common.Notify.DTO;
using Common.Notify.Tools.Mail;

namespace Common.Notify.MessageProvider
{
    public class MailProvider : BaseProvider
    {
        public void SendMessage(BaseConfig config, object message)
        {
            if (config is not MailConfig mailConfig)
            {
                throw new System.Exception("Error Mail Config");
            }

            if (message is not MailInDto sendData)
            {
                throw new System.Exception("Error Mail MessageData");
            }

            MailHelper.SendMail(mailConfig, sendData.Subject, sendData.Content, sendData.MailTo);
        }

        public void SendMessage(BaseConfig config, SendTextInDto dto)
        {
            if (config is not MailConfig mailConfig)
            {
                throw new System.Exception("Error Mail Config");
            }


            MailHelper.SendMail(mailConfig, dto.Subject, dto.Content, dto.SendTo);
        }
    }
}
