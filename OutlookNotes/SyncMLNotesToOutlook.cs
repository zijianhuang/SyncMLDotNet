using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Microsoft.Office.Interop.Outlook;
using Fonlow.SyncML.Common;
using Fonlow.SyncML.Elements;
using System.Xml.Xsl;
using System.Collections.Specialized;

namespace Fonlow.SyncML.OutlookSync
{
    /// <summary>
    /// Apply SyncML commands to Outlook
    /// </summary>
    public class SyncMLNotesToOutlook : SyncMLXToOutlook<NoteItem>
    {
        public SyncMLNotesToOutlook(Application app)
            : base(new OutlookNotes(app), new OutlookNotesWithSifN(app))
        {
        }

        protected override string GetTextFromContent(bool isBase64, string contentType, string content)
        {
            if (contentType == "text/x-s4j-sifn")
            {
                return Utility.ConvertFromBase64(content);
            }
            else
                return null;
        }

    }
}
