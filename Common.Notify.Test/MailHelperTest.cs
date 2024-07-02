using Common.Notify.Tools.Mail;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Common.Notify.Test
{
    [TestClass]
    public class MailHelperTest
    {
        [TestMethod]
        public void SendMail()
        {
            MailHelper.SendMail(new Config.MailConfig
            {
                MailHost = "smtp.exmail.qq.com",
                MailPort = 465,
                MailFrom = "test@qq.com.cn",
                MailPwd = "test",
                SecurityType = MailKit.Security.SecureSocketOptions.Auto

            }, "sub", "content", new List<string>
            {
                "test@163.com"
            });
        }
    }
}
