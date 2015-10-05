using Microsoft.Office.Interop.Outlook;
using Fonlow.SyncML.Common;

namespace Fonlow.SyncML.OutlookSync
{
    /// <summary>
    /// Apply SyncML commands to Outlook
    /// </summary>
    public class SyncMLSifCToOutlook : SyncMLXToOutlook<ContactItem>
    {
        public SyncMLSifCToOutlook(Application app)
            : base(new OutlookContacts(app), new OutlookContactsWithSifC(app))
        {
        }

        protected override string GetTextFromContent(bool isBase64, string contentType, string content)
        {
            if (contentType == "text/x-s4j-sifc")
            {
                return Utility.ConvertFromBase64(content);
            }
            else if ((contentType == "text/x-vcard") || (contentType == "text/vcard"))
            {
                if (isBase64)
                    return Utility.ConvertFromBase64(content);
                else
                    return content;
            }
            else
                return null;
        }

    }

    public class SyncMLVCardToOutlook : SyncMLXToOutlook<ContactItem>
    {
        public SyncMLVCardToOutlook(Application app)
            : base(new OutlookContacts(app), new OutlookContactsWithVCard(app))
        {
        }

        protected override string GetTextFromContent(bool isBase64, string contentType, string content)
        {
            if (contentType == "text/x-s4j-sifc")
            {
                return Utility.ConvertFromBase64(content);
            }
            else if ((contentType == "text/x-vcard") || (contentType == "text/vcard"))
            {
                if (isBase64)
                    return Utility.ConvertFromBase64(content);
                else
                    return content;
            }
            else
                return null;
        }

    }
}
