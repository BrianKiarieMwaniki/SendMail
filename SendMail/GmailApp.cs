using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SendMail
{
    public partial class GmailApp : Form
    {
        string[] scopes = new string[] { GmailService.Scope.GmailSend };
        string applicationName = "GmailAPIDemoClient";

        public GmailApp()
        {
            InitializeComponent();
        }

        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            UserCredential credential;
            //read your credentials file
            using (FileStream stream = new FileStream(Application.StartupPath + @"/credentials.json", FileMode.Open, FileAccess.Read))
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                path = Path.Combine(path, ".credentials/gmail-dotnet-quickstart.json");
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.Load(stream).Secrets, scopes, "user", CancellationToken.None, new FileDataStore(path, true)).Result;
            }

            string message = $"To: {txtTo.Text}\r\nSubject: {txtSubject.Text}\r\nContent-Type: text/html;charset=utf-8\r\n\r\n<h1>{txtMessage.Text}</h1>";
            //call your gmail service
            var service = new GmailService(new BaseClientService.Initializer() { HttpClientInitializer = credential, ApplicationName = applicationName });
            var msg = new Google.Apis.Gmail.v1.Data.Message();
            msg.Raw = Base64UrlEncode(message.ToString());
            service.Users.Messages.Send(msg, "me").Execute();
            MessageBox.Show("Your email has been successfully sent !", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private string Base64UrlEncode(string input)
        {
            var data = Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(data).Replace("+", "-").Replace("/", "_").Replace("=", "");
        }
    }
}
