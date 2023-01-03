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
using SendMail.Utils;

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
            char[] separator = { ';' };
            string[] strList = txtTo.Text.Split(separator);
            List<Message> messages = new List<Message>();
            var scopes = new string[] { "Mail.ReadWrite", "Mail.Send", "email" };

            try
            {
                var graphHelper = new MsGraphAPIHelper(clientId);
                var accessToken = await graphHelper.GetAccessToken(scopes);
                for (int i = 0; i < strList.Length; i++)
                {
                    var message = graphHelper.CreateSimpleMail(strList[i], txtSubject.Text, txtMessage.Text);
                    await graphHelper.SendEmail(accessToken, message);
                }
            }
            finally
            {
                MessageBox.Show("Emails Sent Successfull");
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
