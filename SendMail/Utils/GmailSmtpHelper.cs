using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SendMail.Utils
{
    public class GmailSmtpHelper
    {
        private readonly string _username;
        private readonly string _password;

        public GmailSmtpHelper(string username, string password)
        {
            _username = username;
            _password = password;
        }

        public void SendMail(string to, string subject, string body)
        {
            using(var client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(_username, _password);

                var msgObj = CreateMessage(to, subject, body);

                client.Send(msgObj);
            }
        }

        private MailMessage CreateMessage(string to, string subject,string body)
        {
            var msgObj = new MailMessage();
            msgObj.To.Add(to);
            msgObj.From = new MailAddress(_username);
            msgObj.Subject = subject;
            msgObj.Body = body;
            return msgObj;
        }

        
    }
}
