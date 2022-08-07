using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helper
{
    public static class UploadImageHelper
    {
        public static string UploadFile(byte[] base64Image)
        {
            var fileName=Guid.NewGuid().ToString() + ".jpg";
            var filePath=Path.Combine("Uploads/Images", fileName);

            System.IO.File.WriteAllBytes(filePath, base64Image);

            string imageUrl = string.Format("/{0}/{1}", "Uploads/Images", fileName);
            return imageUrl;
        }
    }
}
