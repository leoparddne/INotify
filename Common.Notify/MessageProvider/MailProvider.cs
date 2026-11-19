using Common.Notify.Config;
using Common.Notify.DTO;
using Common.Notify.Tools.Mail;

namespace Common.Notify.MessageProvider
{
    public class MailProvider : BaseProvider
    {
        public override void SendMessage(BaseConfig config, SendTextInDto dto)
        {
            if (config is not MailConfig mailConfig)
            {
                throw new System.Exception("Error Mail Config");
            }

            MailUtil.SendMail(mailConfig, dto.Subject, dto.Content, dto.SendTo, config.Timeout, dto.AttachFileList);
        }
    }
}
