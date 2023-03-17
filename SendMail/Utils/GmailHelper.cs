using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SendMail.Utils
{
    public class GmailHelper
    {
        public GmailHelper(string[] mailList, string subject, string message)
        {
            MailList = mailList;
            Subject = subject;
            Message = message;
        }

        public GmailHelper(string[] mailList, string[] ccMailList, string subject, string message)
        {
            MailList = mailList;
            CcMailList = ccMailList;
            Subject = subject;
            Message = message;
        }

        public string[] MailList { get; }
        public string[] CcMailList { get; }
        public string Subject { get; }
        public string Message { get; }

        public Message CreateEmailWithAttachment(List<string> attachments,string to)
        {
            MailMessage mail = new MailMessage();
            mail.Subject = this.Subject;
            mail.Body = this.Message;
            mail.IsBodyHtml = true;

            //foreach (string add in this.MailList)
            //{
            //    if (string.IsNullOrEmpty(add))
            //        continue;
            //    mail.To.Add(new MailAddress(add));
            //}

            mail.To.Add(to);
            
            if (CcMailList != null && CcMailList.Count() > 0)
            {
                foreach (string add in CcMailList)
                {
                    if (string.IsNullOrEmpty(add))
                        continue;

                    mail.CC.Add(new MailAddress(add));
                }
            }

            foreach (string path in attachments)
            {
                //var bytes = File.ReadAllBytes(path);
                //string mimeType = MimeMapping.GetMimeMapping(path);
                Attachment attachment = new Attachment(path);//bytes, mimeType, Path.GetFileName(path), true);
                mail.Attachments.Add(attachment);
            }

            MimeMessage mimeMessage = MimeMessage.CreateFromMailMessage(mail);

            Message message = new Message();
            message.Raw = Base64UrlEncode(mimeMessage.ToString());
            return message;
        }

        public Message CreateSimpleEmail(string to)
        {
            string message = $"To: {to}\r\nSubject: {this.Subject}\r\nContent-Type: text/html;charset=utf-8\r\n\r\n<h1>{this.Message}</h1>";
            //call your gmail service
            var msg = new Google.Apis.Gmail.v1.Data.Message();
            msg.Raw = Base64UrlEncode(message.ToString());
            return msg;
        }

        private string Base64UrlEncode(string input)
        {
            var data = Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(data).Replace("+", "-").Replace("/", "_").Replace("=", "");
        }

        private async Task<UserCredential> GetGmailCredential()
        {
            string[] scopes = new string[] { GmailService.Scope.GmailSend };
            UserCredential credential;
            //read your credentials file
            using (FileStream stream = new FileStream(System.Windows.Forms.Application.StartupPath + "\\credential3.json", FileMode.Open, FileAccess.Read))
            {
                //string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                //path = Path.Combine(path, ".credentials\\token.json");
                string credPath = "token.json";

                //credential = GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.FromStream(stream).Secrets,
                //                                                scopes, "user", CancellationToken.None,
                //                                                new FileDataStore(credPath, true)).Result;

                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.FromStream(stream).Secrets,
                                                                scopes, "user", CancellationToken.None);

                return credential;
            }
        }

        public async Task SendMail(Message message, string applicationName)
        {
            var credential = await GetGmailCredential();
            var service = new GmailService(new BaseClientService.Initializer() { HttpClientInitializer = credential, ApplicationName = applicationName });
            service.Users.Messages.Send(message, "me").Execute();            
        }
    }
}
