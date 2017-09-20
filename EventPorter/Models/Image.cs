using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventPorter.Models
{
    public class Image
    {
        public int ID { get; set; }
        public string FilePath { get; set; }
        public string ContentType { get; set; }

        public static bool IsImage(HttpPostedFileBase file)
        {
            if (file.ContentType.Contains("image"))
            {
                return true;
            }

            string[] formats = new string[] { ".jpg", ".png", ".gif", ".jpeg" }; // add more if u like...

            // linq from Henrik Stenbæk
            return formats.Any(item => file.FileName.EndsWith(item, StringComparison.OrdinalIgnoreCase));
        }
    }
}