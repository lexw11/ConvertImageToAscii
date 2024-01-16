// Following tutorial by Thinathayalan Ganesan 
// https://www.c-sharpcorner.com/article/generating-ascii-art-from-an-image-using-C-Sharp/

using System.Drawing;
using System.Text;

namespace ConvertImageToAscii
{ 
    internal class Program
    {
        static void Main()
        {
            Program program = new Program();
            program.btnConvertToAscii_Click();
        }

        //private void btnConvertToAscii_Click(object sender, EventArgs e)
        private void btnConvertToAscii_Click()
        {
            //btnConvertToAscii.Enabled = false;
            // Load the Image from the specified path
            const string imagePath = @"C:\Users\wrigh\Downloads\Mike_Wazowski.png";
            Bitmap image = new Bitmap(imagePath);
            //Bitmap image = new Bitmap(imageURL, true);
            // Resize the image
            // I've use a trackBar to emulate Zoom In / zoom Out feature
            // This value sets the WIDTH, number of characters, of the text image
            image = GetReSizedImage(image, 150);
            // Convert the resized image into ASCII
            string _Content = ConvertToAscii(image);
            // Enclose the final string between <pre> tags to preserve its formatting
            // and load it in the browser control
            //browserMain.DocumentText = "<pre>" + _Content + "</pre>";
            Console.WriteLine(_Content);
            //btnConvertToAscii.Enabled = true;
        }

        private Bitmap GetReSizedImage(Bitmap inputBitmap, int asciiWidth)
        {
            int asciiHeight = 0;
            // Calculate the new Height of the image from its width
            asciiHeight = (int)Math.Ceiling((double)inputBitmap.Height * asciiWidth / inputBitmap.Width);
            // Create a new Bitmap and define its resolution
            Bitmap result = new Bitmap(asciiWidth, asciiHeight);
            Graphics g = Graphics.FromImage((Image)result);
            // The interpolation mode produces high quality images
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.DrawImage(inputBitmap, 0, 0, asciiWidth, asciiHeight);
            g.Dispose();
            return result;
        }

        private string ConvertToAscii(Bitmap image)
        {
            Boolean toggle = false;
            StringBuilder sb = new StringBuilder();
            for (int h = 0; h < image.Height; h++)
            {
                for (int w = 0; w < image.Width; w++)
                {
                    Color pixelColor = image.GetPixel(w, h);
                    // Average out the RGB components to find the Gray Color
                    int red = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
                    int green = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
                    int blue = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
                    Color grayColor = Color.FromArgb(red, green, blue);
                    // Use the toggle flag to minimize height-wise stretch
                    if (!toggle)
                    {
                        int index = (grayColor.R * 10) / 255;
                        sb.Append(_AsciiChars[index]);
                    }
                }
                if (!toggle)
                {
                    //sb.Append("<BR>");
                    sb.Append("\n");
                    toggle = true;
                }
                else
                {
                    toggle = false;
                }
            }
            return sb.ToString();
        }

        //private string[] _AsciiChars = { "#", "#", "@", "%", "=", "+", "*", ":", "-", ".", "&nbsp;" };
        private string[] _AsciiChars = { "#", "#", "@", "%", "=", "+", "*", ":", "-", ".", " " };

        //private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    saveFileDialog1.Filter = "Text File (*.txt)|.txt|HTML (*.htm)|.htm";
        //    DialogResult diag = saveFileDialog1.ShowDialog();
        //    if (diag == DialogResult.OK)
        //    {
        //        if (saveFileDialog1.FilterIndex == 1)
        //        {
        //            _Content = _Content.Replace("&nbsp;", " ").Replace("<BR>", "\n");
        //        }
        //        else
        //        {
        //            _Content = "<pre>" + _Content + "</pre>";
        //        }
        //        StreamWriter sw = new StreamWriter(saveFileDialog1.FileName);
        //        sw.Write(_Content);
        //        sw.Flush();
        //        sw.Close();
        //    }
        //}
    }
}

