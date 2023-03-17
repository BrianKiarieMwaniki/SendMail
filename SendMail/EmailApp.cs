using SendMail.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SendMail
{
    public partial class EmailApp : Form
    {
        List<string> attachments = new List<string>();
        string clientId = ConfigurationManager.AppSettings["clientId"];
        string applicationName = "GmailAPIDemoClient";
        //string applicationName = "FinCreditEmailSetup";
        private bool _isPlatformGmail = false;
        private bool _isPlatformMicrosoft = false;
        private bool _isPlatformOutlook = false;
        private bool _isGmailSmtp => radioButtonGmailSMTP.Checked;
        
        public EmailApp()
        {
            InitializeComponent();
        }

        private void EmailApp_Load(object sender, EventArgs e)
        {
            radioBtnGmail.CheckedChanged += RadioBtnGmail_Checked;
            radioBtnMicrosoft.CheckedChanged += RadioBtnMicrosoft_Checked;
            radioBtnOutlook.CheckedChanged += RadioBtnOutlook_Checked;
        }

        private void RadioBtnOutlook_Checked(object sender, EventArgs e)
        {
            _isPlatformOutlook = true;
            _isPlatformMicrosoft = false;
            _isPlatformGmail = false;
        }

        private void RadioBtnMicrosoft_Checked(object sender, EventArgs e)
        {
            _isPlatformMicrosoft = true;
            _isPlatformGmail = false;            
            _isPlatformOutlook = false;
        }

        private void RadioBtnGmail_Checked(object sender, EventArgs e)
        {            
            _isPlatformGmail = true;            
            _isPlatformMicrosoft = false;
            _isPlatformOutlook = false;
        }

        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            if (_isPlatformGmail)
                SendGmailEmail();
            else if (_isPlatformMicrosoft)
                SendMicrosoftEmail();
            else if (_isPlatformOutlook)
                SendOutlookEmail();
            else if(_isGmailSmtp)
            {
                SendGmailSmtpEmail();
            }
            else
                MessageBox.Show("You must select a platform to use to send your emails!🙁");
        }

        private void SendGmailSmtpEmail()
        {
            string[] mailList = txtTo.Text.Split(';');
            string subject = txtSubject.Text;
            string message = txtMessage.Text;

            using (var form = new Credentials())
            {
                var result = form.ShowDialog();

                if(result == DialogResult.OK)
                {
                    var username = form.Username;
                    var password = form.Password;

                    if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password)) return;

                    var smtpClient = new GmailSmtpHelper(username, password);

                    if (mailList != null && mailList.Count() > 0)
                    {
                        foreach (var to in mailList)
                        {
                            smtpClient.SendMail(to, subject, message);
                        }
                    }
;                }
            }
        }

        private async void SendGmailEmail()
        {
            string[] mailList = txtTo.Text.Split(';');
            string subject = txtSubject.Text;
            string message = txtMessage.Text;
            Google.Apis.Gmail.v1.Data.Message messageToSend = new Google.Apis.Gmail.v1.Data.Message();
            var gmailHelper = new GmailHelper(mailList, subject, message);
            try
            {
                if (attachments.Count > 0)
                {
                    if (mailList != null && mailList.Count() > 0)
                    {
                        foreach (var to in mailList)
                        {
                            var msg = gmailHelper.CreateEmailWithAttachment(attachments, to);
                            await gmailHelper.SendMail(msg, applicationName);
                        }
                    }
                }
                else
                {
                    if (mailList != null && mailList.Count() > 0)
                    {
                        foreach (var to in mailList)
                        {
                            var msg = gmailHelper.CreateSimpleEmail(to);
                           await gmailHelper.SendMail(msg, applicationName);
                        }
                    }
                }
                MessageBox.Show("Your email has been successfully sent!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private async void SendMicrosoftEmail()
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
                    if(attachments.Any())
                    {
                         await graphHelper.SendEmail(accessToken, message, attachments);
                    }
                    else
                    {
                        await graphHelper.SendEmail(accessToken, message);
                    }
                }
            }
            finally
            {
                MessageBox.Show("Emails Sent Successfull");
            }

        }

        private void SendOutlookEmail()
        {
            var isSentSuccessfully = false;
            string[] recepients = txtTo.Text?.Split(';');
            try
            {
                var outlookHelper = new OutlookEmailHelper();
                if(recepients.Count() > 0)
                {
                    foreach (var to in recepients)
                    {
                        isSentSuccessfully = outlookHelper.SendMailWithOutlook(txtTo.Text, txtSubject.Text, txtMessage.Text, attachments);
                    }
                }
            }
            catch 
            {
            }
            finally
            {
                if (isSentSuccessfully) MessageBox.Show("Outlook Email Sent Successfully!👍");
            }
        }

        private void btnAddAttachment_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;
            openFileDialog.Multiselect = true;
            openFileDialog.ShowDialog();
            string[] filePaths = openFileDialog.FileNames;
            string paths = string.Empty;
            if (filePaths.Count() > 0)
            {
                foreach (var path in filePaths)
                {
                    paths += $"{path}; ";
                    attachments.Add(path);
                    flpAttachments.Controls.Add(CreateAttachmentView(path));
                }
            }
        }

        private Button CreateAttachmentView(string path)
        {
            Button attachBtn = new Button();
            attachBtn.BackColor = Color.Transparent;            
            attachBtn.Image = ImageUtils.GetFileExtensionImg(path, new Size(35,35));
            attachBtn.Height = 50;
            attachBtn.Width = 50;
            attachBtn.Click += (sender, e) => { HandleAttachBtnClick(sender, e, path); };

            var badge = new Label();
            badge.Height = 15;
            badge.Width = 15;
            badge.BackColor = Color.Transparent;
            badge.Anchor = AnchorStyles.Top;
            badge.Anchor = AnchorStyles.Right;
            badge.Location = new Point()
            {
                X = badge.Width + 15,
                Y = attachBtn.Location.Y
            };
            badge.Image = ImageUtils.ResizeImage(Properties.Resources.delete,new Size(14,14));
            badge.Cursor = Cursors.Hand;
            badge.Click += (sender, e) => { HandleAttachBtnBadgeClick(sender, e, path); };
            attachBtn.Controls.Add(badge);
            this.PerformLayout();
            return attachBtn;
        }

        private void HandleAttachBtnBadgeClick(object sender, EventArgs e, string path)
        {
            var control = sender as Label;
            var parentControl = control.Parent;
            flpAttachments.Controls.Remove(parentControl);
            flpAttachments.Refresh();

            attachments.Remove(path);
        }

        private void HandleAttachBtnClick(object sender, EventArgs e, string path)
        {
            var process = new Process();
            process.StartInfo = new ProcessStartInfo() { UseShellExecute = true, FileName = path };
            process.Start();
        }

      
    }
}
