using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using Microsoft.Identity.Client;
using System.Security;
using Microsoft.Graph;
using System.Net.Http.Headers;
using Message = Microsoft.Graph.Message;

namespace SendMail
{
    public partial class Form1 : Form
    {
        string clientId = ConfigurationManager.AppSettings["clientId"];
        string tenantId = ConfigurationManager.AppSettings["TenantId"];
        string sendersEmail = ConfigurationManager.AppSettings["SendersEmail"];
        string password = ConfigurationManager.AppSettings["Password"];

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            txtClientId.Text = clientId;
            txtTenantId.Text = tenantId;
            txtSendersEmail.Text = sendersEmail;
            txtPassword.Text = password;
        }

        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            SendEmailGraphAPI();
        }

        private async void SendEmailGraphAPI()
        {
            var pcaoptions = new PublicClientApplicationOptions
            {
                ClientId = clientId,
                TenantId = tenantId,
                RedirectUri = "https://login.microsoftonline.com/common/oauth2/nativeclient"
            };

            var pca = PublicClientApplicationBuilder
                .CreateWithApplicationOptions(pcaoptions)
                .WithAuthority(AzureCloudInstance.AzurePublic, tenantId).Build();

            //var ewsScope = new string[] { "https://graph.microsoft.com/.default" };
            var scope = new string[] { "user.read" };

            var securePassword = new SecureString();
            foreach (char c in password)
            {
                securePassword.AppendChar(c);
            }

            AuthenticationResult authResult = null;
            var app = App.PublicClientApp;
            var accounts = await app.GetAccountsAsync();
            var firstAccount = accounts.FirstOrDefault();
            try
            {
               authResult = await app.AcquireTokenSilent(scope,firstAccount).ExecuteAsync();
            }
            catch 
            {
                authResult = await app.AcquireTokenInteractive(scope).ExecuteAsync();
            }

            GraphServiceClient graphServiceClient =
                new GraphServiceClient(new DelegateAuthenticationProvider(async (requestMessage) =>
                {
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authResult.AccessToken);
                }));

            char[] separator = { ';' };
            string[] strList = txtTo.Text.Split(separator);
            List<Recipient> toMailList = new List<Recipient>();

            for (int i = 0; i < strList.Length; i++)
            {
                var toMail = new Recipient 
                {
                    EmailAddress = new EmailAddress
                    {
                        Address = strList[i]
                    }
                };

                toMailList.Add(toMail);
            }

            var message = new Message
            {
                Subject = txtSubject.Text,
                Body = new ItemBody
                {
                    ContentType = BodyType.Text,
                    Content = txtMessage.Text
                },
                ToRecipients = toMailList
            };

            try
            {
                await graphServiceClient.Me.SendMail(message, true).Request().PostAsync();
            }
            finally
            {
                MessageBox.Show("Email Sent Successfully!", "Success");
            }
        }
    }

    public partial  class App
    {
        static App()
        {
            _clientApp = PublicClientApplicationBuilder.Create(ClientId)
            //.WithAuthority(AzureCloudInstance.AzurePublic, TenantId)
            .WithRedirectUri("https://login.microsoftonline.com/common/oauth2/nativeclient")
            .Build();
        }

        private static string ClientId => ConfigurationManager.AppSettings["ClientId"];
        private static string TenantId => ConfigurationManager.AppSettings["TenantId"];
        private static IPublicClientApplication _clientApp;
        public static IPublicClientApplication PublicClientApp { get { return _clientApp; } }
    }
}
