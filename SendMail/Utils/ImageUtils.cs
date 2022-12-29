using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendMail.Utils
{
    public static class ImageUtils
    {
        public static Image GetFileExtensionImg(string path, Size size)
        {
            string fileExtension = Path.GetExtension(path);
            Image fileExtensionImg = null;
            if (!string.IsNullOrWhiteSpace(fileExtension))
            {
                if (fileExtension.Contains("pdf")) fileExtensionImg = Properties.Resources.pdf;
                else if (fileExtension.Contains("docx")) fileExtensionImg = Properties.Resources.docx;
                else if (fileExtension.Contains("txt")) fileExtensionImg = Properties.Resources.txt;
                else fileExtensionImg = Properties.Resources.file;
            }

            fileExtensionImg = ResizeImage(fileExtensionImg, size);

            return fileExtensionImg;
        }

        public static Image ResizeImage(Image imgToResize, Size size)
        {
            //Get the image current width  
            int sourceWidth = imgToResize.Width;
            //Get the image current height  
            int sourceHeight = imgToResize.Height;
            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;
            //Calulate  width with new desired size  
            nPercentW = ((float)size.Width / (float)sourceWidth);
            //Calculate height with new desired size  
            nPercentH = ((float)size.Height / (float)sourceHeight);
            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;
            //New Width  
            int destWidth = (int)(sourceWidth * nPercent);
            //New Height  
            int destHeight = (int)(sourceHeight * nPercent);
            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((System.Drawing.Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            // Draw image with new width and height  
            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();
            return (Image)b;
        }
    }
}
