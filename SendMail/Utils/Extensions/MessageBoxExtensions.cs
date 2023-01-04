using System.Windows.Forms;

namespace SendMail.Utils.Extensions
{
    public static class MessageBoxExtensions
    {
        public static void Show(this MessageBox messageBox,string text, MessageBoxIcon icon)
        {
            messageBox.Show(text, icon);
        }
    }
}
