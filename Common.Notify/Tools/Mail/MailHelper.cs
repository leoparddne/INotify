using Common.Notify.Config;
using MimeKit;
using System;
using System.Collections.Generic;

namespace Common.Notify.Tools.Mail
{
    public static class MailHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config">服务器配置</param>
        /// <param name="subject">主题</param>
        /// <param name="content">正文</param>
        /// <param name="mailTo">接收邮箱</param>
        /// <returns></returns>
        public static void SendMail(MailConfig config, string subject, string content, List<string> mailTo)
        {
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress(config.MailFrom, config.MailFrom));

            if (mailTo == null || mailTo.Count <= 0)
            {
                throw new Exception("maill to null user");
            }

            foreach (string item in mailTo)
            {
                message.To.Add(new MailboxAddress(item, item));
            }

            message.Subject = string.Format(subject);
            message.Body = new TextPart("html")
            {
                Text = content
            };
            using (MailKit.Net.Smtp.SmtpClient client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect(config.MailHost, config.MailPort, true);
                client.Authenticate(config.MailFrom, config.MailPwd);
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
