using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Controls;

namespace Junk_Cleaner_.NET_WPF
{
    public class ErazedElements
    {

    }
    public  class MicrosoftEdge
    {
        public bool blnInternetCache { get; set; }
        public bool blnInternetHistory { get; set; }
        public bool blnCookies { get; set; }
        public bool blnDownloadHistory { get; set; }
        public bool blnLastDownloadLocation { get; set; }
        public bool blnSession { get; set; }
        public bool blnRecentlyTypedURLs { get; set; }
        public bool blnSavedFormInformation { get; set; }
        public bool blnSavedPasswords { get; set; }
    }

    public class SystemElement
    {
        public string strImagePath { get; set; }
        public bool blnRecycleBin { get; set; }
        public bool blnTemporaryFiles { get; set; }
        public bool blnClipboard { get; set; }
        public bool blnMemoryDumps { get; set; }
        public bool blnWindowsLogFiles { get; set; }
        public bool blnWindowsErrorReporting { get; set; }
        public bool blnDnsCache { get; set; }
        public bool blnSavedFormInformation { get; set; }
        public bool blnSavedPasswords { get; set; }
    }
    
}
