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

        private string _AsciiChars = " .-:*+=%@##";
        //private string _AsciiChars = "##@%=+*:-. ";

        private void btnConvertToAscii_Click()
        {
            //const string imagePath = @"C:\Users\wrigh\Downloads\Mike_Wazowski.png";
            const string imagePath = @"C:\Users\wrigh\Downloads\pupper.jpg";

            Bitmap image = new Bitmap(imagePath, true);

            image = GetReSizedImage(image, 150);

            string _Content = ConvertToAscii(image);

            Console.WriteLine(_Content);
        }

        private Bitmap GetReSizedImage(Bitmap inputBitmap, int asciiWidth)
        {
            int asciiHeight = 0;

            asciiHeight = (int)Math.Ceiling((double)inputBitmap.Height * asciiWidth / inputBitmap.Width);

            Bitmap result = new Bitmap(asciiWidth, asciiHeight);

            Graphics g = Graphics.FromImage((Image)result);

            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.DrawImage(inputBitmap, 0, 0, asciiWidth, asciiHeight);
            g.Dispose();
            return result;
        }

        private string ConvertToAscii(Bitmap image)
        {
            Boolean toggle = false;
            StringBuilder stringBuilder = new StringBuilder();
            for (int row = 0; row < image.Height; row++)
            {
                for (int column = 0; column < image.Width; column++)
                {
                    Color pixelColor = image.GetPixel(column, row);

                    int rgbAverage = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
                    //Color grayColor = Color.FromArgb(rgbAverage, rgbAverage, rgbAverage);

                    if (!toggle)
                    {
                        int index = Convert.ToInt32(rgbAverage * (_AsciiChars.Length - 1) / 255);
                        stringBuilder.Append(_AsciiChars[index]);
                    }
                }
                if (!toggle)
                {
                    stringBuilder.Append("\n");
                    toggle = true;
                }
                else
                {
                    toggle = false;
                }
            }
            return stringBuilder.ToString();
        }

    }
}

