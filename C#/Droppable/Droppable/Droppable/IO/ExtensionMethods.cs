using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Droppable.IO
{
    public static class ExtensionMethods
    {
        public static string[] Explode(this string str, string delimiter)
        {
            return str.Split(new string[] { delimiter }, StringSplitOptions.None);
        }

        public static string FillSpaces(this string str, string filler = "+")
        {
            return str.Replace(" ", filler);
        }

        public static long DownloadDataGetLength(this WebClient wb, string url)
        {
            using (wb)
            {
                try
                {
                    return wb.DownloadData(url).LongLength;
                }
                catch (WebException)
                {
                    return 0;
                }
            }
        }

        public static string DownloadStringDispose(this WebClient wb, string url)
        {
            using (wb)
            {
                try
                {
                    return wb.DownloadString(url);
                }
                catch (WebException)
                {
                    return string.Empty;
                }
            }
        }
    }
}
