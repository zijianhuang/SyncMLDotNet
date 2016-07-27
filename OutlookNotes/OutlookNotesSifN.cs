using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Microsoft.Office.Interop.Outlook;
using Fonlow.SyncML.Common;

namespace Fonlow.SyncML.OutlookSync
{
    public class OutlookNotesWithSifN : OutlookItemsWithSyncContent<NoteItem>
    {
        public OutlookNotesWithSifN(Application app)
            : base(app.GetNamespace("MAPI").GetDefaultFolder(OlDefaultFolders.olFolderNotes) as Folder)
        {
        }

        protected override NoteItem AddItemToItemsFolder()
        {
            return ItemsFolder.Items.Add(OlItemType.olNoteItem) as NoteItem;
        }

        protected override string GetEntryId(NoteItem item)
        {
            return item.EntryID;
        }

        protected override void SaveItem(NoteItem item)
        {
            item.Save();
        }

        const string isoFormat = "yyyyMMddTHHmmssZ";

        public override string ReadItemToText(NoteItem item)
        {
            XElement element = new XElement("note",
                new XElement("SIFVersion", "1.0"),
                     DataUtility.CreateElement("Categories", item.Categories),
            DataUtility.CreateElement("Body", item.Body),
            DataUtility.CreateElement("Color", ((int)item.Color).ToString()),
            DataUtility.CreateElement("Height", item.Height.ToString()),
            DataUtility.CreateElement("Width", item.Width.ToString()),
            DataUtility.CreateElement("Left", item.Left.ToString()),
            DataUtility.CreateElement("Top", item.Top.ToString())

                     );

            return element.ToString(SaveOptions.DisableFormatting);

        }

        protected override void WriteMetaToItem(string meta, NoteItem item)
        {
            XElement sif = XElement.Parse(meta);
            item.Body = DataUtility.GetXElementValueSafely(sif, "Body");
            item.Categories = DataUtility.GetXElementValueSafely(sif, "Categories");
            item.Color = (OlNoteColor)DataUtility.ParseInt(DataUtility.GetXElementValueSafely(sif, "Color"));
            item.Height = DataUtility.ParseInt(DataUtility.GetXElementValueSafely(sif, "Height"));
            item.Width = DataUtility.ParseInt(sif, "Width");
            item.Top = DataUtility.ParseInt(sif, "Top");
            item.Left = DataUtility.ParseInt(sif, "Left");

        }
    }

}
