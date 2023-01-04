using System;
using System.Collections.Generic;
using MsOutlook = Microsoft.Office.Interop.Outlook;

namespace SendMail.Utils
{
    public class OutlookEmailHelper
    {
        public bool SendMailWithOutlook(string to,string subject, string body, List<string> attachments)
        {
            try
            {
                // create the outlook application.
                MsOutlook.Application outlookApp = new MsOutlook.Application();
                if (outlookApp == null)
                    return false;

                // create a new mail item.
                MsOutlook.MailItem mail = (MsOutlook.MailItem)outlookApp.CreateItem(MsOutlook.OlItemType.olMailItem);

                // set html body. 
                // add the body of the email
                string htmlBody = $"<p>{body}</p>";
                mail.HTMLBody = htmlBody;

                //Add attachments.
                if (attachments != null)
                {
                    foreach (string file in attachments)
                    {
                        //attach the file
                        MsOutlook.Attachment oAttach = mail.Attachments.Add(file);
                    }
                }

                mail.Subject = subject;
                mail.To = to;

                mail.Send();

                mail = null;
                outlookApp = null;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}


