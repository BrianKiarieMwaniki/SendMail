using SendMail.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SendMail
{
    public partial class GmailApp : Form
    {
        List<string> attachments = new List<string>();   
        string applicationName = "GmailAPIDemoClient";

        public GmailApp()
        {
            InitializeComponent();
        }

        private void btnSendEmail_Click(object sender, EventArgs e)
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
                            gmailHelper.SendMail(msg, applicationName);
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
                            gmailHelper.SendMail(msg, applicationName);
                        }
                    }
                }
            }           
            finally
            {
                MessageBox.Show("Your email has been successfully sent!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
