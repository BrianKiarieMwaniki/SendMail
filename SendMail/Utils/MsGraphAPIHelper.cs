using Microsoft.Graph;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SendMail.Utils
{
    public class MsGraphAPIHelper
    {
        private readonly string _clientId;

        public MsGraphAPIHelper(string clientId)
        {
            _clientId = clientId;
        }
        public async Task<string> GetAccessToken(string[] scopes)
        {
            AuthenticationResult authResult = null;
            App application = new App(_clientId);
            var app = application.PublicClientApp;
            var accounts = await app.GetAccountsAsync();
            var firstAccount = accounts.FirstOrDefault();
            try
            {
                authResult = await app.AcquireTokenSilent(scopes, firstAccount).ExecuteAsync();
            }
            catch
            {
                authResult = await app.AcquireTokenInteractive(scopes).ExecuteAsync();
            }

            if (authResult == null) return string.Empty;

            return authResult.AccessToken;            
        }

        public async Task<string> SendEmail(string accessToken,Message message)
        {
            string result = string.Empty;
            GraphServiceClient graphServiceClient =
                new GraphServiceClient(new DelegateAuthenticationProvider(async (requestMessage) =>
                {
                   await  Task.FromResult(requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken));
                }));

            try
            {
                await graphServiceClient.Me.SendMail(message, true).Request().PostAsync();
            }
            catch(Exception ex)
            {
               result = ex.Message;
            }
            finally
            {
                result = "201";
            }

            return result;
        }

        public async Task<string> SendEmail(string accessToken,Message message, List<string> attachments)
        {
            string result = string.Empty;
            GraphServiceClient graphServiceClient =
                new GraphServiceClient(new DelegateAuthenticationProvider(async (requestMessage) =>
                {
                   await  Task.FromResult(requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken));
                }));

            try
            {
                if(attachments.Any())
                {
                    message.Attachments = CreateAttachments(attachments);
                }
                await graphServiceClient.Me.SendMail(message, true).Request().PostAsync();
            }
            catch(Exception ex)
            {
               result = ex.Message;
            }
            finally
            {
                result = "201";
            }

            return result;
        }

        public Message CreateSimpleMail(string to, string subject, string body)
        {
            var message = new Message()
            {
                Subject = subject,
                Body = new ItemBody { ContentType = BodyType.Text, Content = body },
                ToRecipients = new List<Recipient>()
                {
                    new Recipient
                    {
                        EmailAddress = new EmailAddress
                        {
                            Address = to
                        }
                    }
                }
            };

            return message;
        }

        private MessageAttachmentsCollectionPage CreateAttachments(List<string> attachments)
        {
            var attachmentsToReturn = new MessageAttachmentsCollectionPage();
            foreach (var attachment in attachments)
            {
                var fileAttachment = new FileAttachment()
                {
                    Name = Path.GetFileName(attachment),
                    ContentType = GetContentType(attachment),
                    ContentBytes = System.IO.File.ReadAllBytes(attachment)
                };

                attachmentsToReturn.Add(fileAttachment);
            }

            return attachmentsToReturn;
        }

        private string GetAttachmentAsBase64String(string path)
        {
            try
            {
                var bytes = System.IO.File.ReadAllBytes(path);
                return Convert.ToBase64String(bytes);
            }
            catch
            {
                return string.Empty;
            }

        }

        private string GetContentType(string path)
        {
            string contentType = string.Empty;
            string fileExtension = Path.GetExtension(path);
            switch(fileExtension.ToLower())
            {
                case "txt":
                    return "text/plain";
                case "pdf":
                    return "text/plain";
                case "jpeg":
                    return "image/jpeg";
                default:
                    return "text/plain";
            }
        }
    }

    public partial class App
    {
        public App(string clientId)
        {            
            _clientApp = PublicClientApplicationBuilder.Create(clientId)
            .WithRedirectUri("https://login.microsoftonline.com/common/oauth2/nativeclient")
            .Build();
        }

        private IPublicClientApplication _clientApp;

        public IPublicClientApplication PublicClientApp { get { return _clientApp; } }
    }
}
