using Microsoft.Office.Interop.Outlook;
using Fonlow.SyncML.Common;

namespace Fonlow.SyncML.OutlookSync
{
    /// <summary>
    /// Apply SyncML commands to Outlook
    /// </summary>
    public class SyncMLSifTToOutlook : SyncMLXToOutlook<TaskItem>
    {
        public SyncMLSifTToOutlook(Application app)
            : base(new OutlookTasks(app), new OutlookTasksWithSifT(app))
        {
        }

        protected override string GetTextFromContent(bool isBase64, string contentType, string content)
        {
            if (contentType == "text/x-s4j-sift")
            {
                return Utility.ConvertFromBase64(content);
            }
            else if ((contentType == "text/x-vcalendar") || (contentType == "text/vcalendar"))
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

    public class SyncMLTodoToOutlook : SyncMLXToOutlook<TaskItem>
    {
        public SyncMLTodoToOutlook(Application app)
            : base(new OutlookTasks(app), new OutlookTasksWithICal(app))
        {
        }

        protected override string GetTextFromContent(bool isBase64, string contentType, string content)
        {
            if (contentType == "text/x-s4j-sift")
            {
                return Utility.ConvertFromBase64(content);
            }
            else if ((contentType == "text/x-vcalendar") || (contentType == "text/vcalendar"))
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
