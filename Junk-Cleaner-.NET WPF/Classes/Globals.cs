using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Junk_Cleaner_.NET_WPF
{
    public static class Globals
    {

        private static readonly string[] SizeSuffixes =
                  { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

        public static string sizeFix(long length, int decimalPlaces)
        {
            try
            {
                if (length < 0) { return "-" + sizeFix((-length), decimalPlaces); }
                int i = 0;
                decimal dValue = (decimal)length;
                while (Math.Round(dValue, decimalPlaces) >= 1000) { dValue /= 1024; i++; }
                return string.Format("{0:n" + decimalPlaces + "} {1}", dValue, SizeSuffixes[i]);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
