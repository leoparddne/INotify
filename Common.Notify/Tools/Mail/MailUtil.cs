using Common.Notify.Config;
using Common.Notify.DTO.Contact;
using Common.Toolkit.Extention;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;

namespace Common.Notify.Tools.Mail
{
    public static class MailUtil
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config">服务器配置</param>
        /// <param name="subject">主题</param>
        /// <param name="content">正文</param>
        /// <param name="mailTo">接收邮箱</param>
        /// <returns></returns>
        public static void SendMail(MailConfig config, string subject, string content, List<string> mailTo, int timeout = 10, List<AttachFileConfig> attachFileList = null)
        {
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress(config.MailFrom, config.MailFrom));

            if (mailTo.IsNullOrEmpty())
            {
                throw new Exception("maill to null user");
            }

            foreach (string item in mailTo)
            {
                message.To.Add(new MailboxAddress(item, item));
            }

            message.Subject = string.Format(subject);
            using (MailKit.Net.Smtp.SmtpClient client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Timeout = timeout * 1000;
                client.Connect(config.MailHost, config.MailPort, config.SecurityType);
                Multipart multipart = new Multipart("mixed");

                if (!attachFileList.IsNullOrEmpty())
                {
                    foreach (var attachFile in attachFileList)
                    {
                        var fileInfo = new FileInfo(attachFile.FilePath);
                        using (var fileStream = File.OpenRead(attachFile.FilePath))
                        {
                            var memoryStream = new MemoryStream();
                            fileStream.CopyTo(memoryStream);
                            memoryStream.Position = 0; // 重置位置

                            MimePart attachment = new MimePart()
                            {
                                Content = new MimeContent(memoryStream, ContentEncoding.Default),
                                ContentDisposition = new MimeKit.ContentDisposition(MimeKit.ContentDisposition.Attachment),
                                ContentTransferEncoding = ContentEncoding.Base64,
                                FileName = attachFile.FileName
                            };
                            multipart.Add(attachment);
                        }
                    }
                }

                var textBody = new TextPart("html")
                {
                    Text = content
                };

                multipart.Add(textBody);
                message.Body = multipart;

                client.Authenticate(config.MailFrom, config.MailPwd);
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
