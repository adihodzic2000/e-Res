using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF
{
    public static class Globals
    {
        public static string GetMonthName(int monthNumber)
        {
            switch (monthNumber)
            {
                case 1: return "Januar";
                case 2: return "Februar";
                case 3: return "Mart";
                case 4: return "April";
                case 5: return "Maj";
                case 6: return "Juni";
                case 7: return "Juli";
                case 8: return "August";
                case 9: return "Septembar";
                case 10: return "Oktobar";
                case 11: return "Novembar";
                case 12: return "Decembar";
                default: return "Nothing";
            }
        }
        public static byte[] FromImageToByte(Image image)
        {
            MemoryStream ms = new MemoryStream();
            image.Save(ms, ImageFormat.Jpeg);
            return ms.ToArray();
        }
        public static Image FromByteToImage(byte[] image)
        {
            MemoryStream ms = new MemoryStream(image);
            return Image.FromStream(ms);
        }
        static public string EncodeTo64(string toEncode)
        {

            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);

            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);

            return returnValue;

        }
        static public double diffDays(DateTime dateTime)
        {
            return (dateTime-new DateTime(2020, 1, 1)).TotalDays;
        }
    }
}
