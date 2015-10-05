using Microsoft.Office.Interop.Outlook;
using Fonlow.SyncML.Common;

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
