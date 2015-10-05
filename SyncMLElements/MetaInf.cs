using System.Xml.Linq;

namespace Fonlow.SyncML.Elements
{
    public class MetaLast : SyncMLSimpleElement
    {
        public MetaLast() : base(ElementNames.SyncMLMetInf, ElementNames.Last)
        {

        }
    }

    public class MetaNext : SyncMLSimpleElement
    {
        public MetaNext() : base(ElementNames.SyncMLMetInf, ElementNames.Next)
        {

        }
    }

    /// <summary>
    /// Specifies the synchronization state information (i.e., sync anchor) for the currentsynchronization session.
    /// </summary>
    /// <ParentElements>MetInf</ParentElements> 
    /// <ContentModel>(Last?, Next)</ContentModel>
    public class MetaAnchor : SyncMLComplexElement
    {
        private MetaAnchor() : base(ElementNames.SyncMLMetInf, ElementNames.Anchor) { }

        public static MetaAnchor Create()
        {
            MetaAnchor r = new MetaAnchor();
            r.Next = new MetaNext();
            return r;
        }
        /// <summary>
        /// Create next and optional last.
        /// </summary>
        public static MetaAnchor Create(string next, string last)
        {
            MetaAnchor r = new MetaAnchor();
            if (last != null)
                r.Last = SyncMLSimpleElementFactory.Create<MetaLast>(last);
            r.Next = SyncMLSimpleElementFactory.Create<MetaNext>(next);
            return r;
        }

        public static MetaAnchor Create(XElement xmlData)
        {
            if (xmlData == null)
                return null;

            MetaAnchor r = new MetaAnchor();
            r.Last = SyncMLSimpleElementFactory.Create<MetaLast>(ElementReader.Element(xmlData, ElementNames.Last));
            r.Next = SyncMLSimpleElementFactory.Create<MetaNext>(ElementReader.Element(xmlData, ElementNames.Next));
            return r;
        }

        private MetaLast last;

        public MetaLast Last
        {
            get { if (last == null) last = new MetaLast(); return last; }
            set { last = value; }
        }

        private MetaNext next;

        public MetaNext Next
        {
            get { return next; }
            set { next = value; ThrowExceptionIfNull(value, "Anchor/Next"); }
        }

        public override XElement Xml
        {
            get
            {
                Element.RemoveAll();
                if (last != null)
                    Element.Add(last.Xml);
                Element.Add(Next.Xml);
                return Element;
            }
        }
    }

    /// <summary>
    /// EMI might not need to be supported.
    /// Specifies the non-standard, experimental meta information (EMI) extensions supported by the device. The extensions
    /// are specified in terms of the XML element type name and the value.
    /// </summary>
    public class MetaEMI : SyncMLSimpleElement
    {
        public MetaEMI() : base(ElementNames.SyncMLMetInf, ElementNames.EMI)
        {

        }
    }

    public class MetaFormat : SyncMLSimpleElement
    {
        public MetaFormat() : base(ElementNames.SyncMLMetInf, ElementNames.Format)
        {

        }
    }

    public class MetaFieldLevel : SyncMLSimpleElement
    {
        public MetaFieldLevel() : base(ElementNames.SyncMLMetInf, ElementNames.FieldLevel)
        {

        }
    }

    public class MetaFreeID : SyncMLSimpleElement
    {
        public MetaFreeID() : base(ElementNames.SyncMLMetInf, ElementNames.FreeID)
        {

        }
    }

    public class MetaFreeMem : SyncMLSimpleElement
    {
        public MetaFreeMem() : base(ElementNames.SyncMLMetInf, ElementNames.FreeMem)
        {

        }
    }

    /// <summary>
    /// Restrictions: The content information for this element type SHOULD BE 
    /// one of draft, final, delete, undelete, read, unread.
    /// When this meta-information is specified repetitively in a hierarchically of element types
    /// (e.g., in a SyncML collection, as well as the items in the collection), then the metainformation
    /// specified in the lowest level element type takes precedence.
    /// This element type is used to set the meta-information characteristics of a data object, such
    /// as the draft/final, delete/undelete, read/unread marks on a folder item or mail item.
    /// 
    /// At the moment, the class does not check assigned content.
    /// </summary>
    public class MetaMark : SyncMLSimpleElement
    {
        public MetaMark() : base(ElementNames.SyncMLMetInf, ElementNames.Mark)
        {

        }
    }

    public class MetaMaxMsgSize : SyncMLSimpleElement
    {
        public MetaMaxMsgSize() : base(ElementNames.SyncMLMetInf, ElementNames.MaxMsgSize)
        {

        }
    }

    public class MetaMaxObjSize : SyncMLSimpleElement
    {
        public MetaMaxObjSize() : base(ElementNames.SyncMLMetInf, ElementNames.MaxObjSize)
        {

        }
    }

    public class MetaNextNonce : SyncMLSimpleElement
    {
        public MetaNextNonce() : base(ElementNames.SyncMLMetInf, ElementNames.NextNonce)
        {

        }
    }

    public class MetaSharedMem : SyncMLSimpleElement
    {
        public MetaSharedMem() : base(ElementNames.SyncMLMetInf, ElementNames.SharedMem)
        {

        }
    }

    public class MetaSize : SyncMLSimpleElement
    {
        public MetaSize() : base(ElementNames.SyncMLMetInf, ElementNames.Size)
        {

        }
    }

    public class MetaType : SyncMLSimpleElement
    {
        public MetaType() : base(ElementNames.SyncMLMetInf, ElementNames.Type)
        {

        }
    }

    public class MetaVersion : SyncMLSimpleElement
    {
        public MetaVersion() : base(ElementNames.SyncMLMetInf, ElementNames.Version)
        {

        }
    }

    /// <summary>
    /// Specifies the maximum free memory and item identifier for a source (e.g., a datastore or a device.
    /// </summary>
    ///<ContentModel>(SharedMem?, FreeMem, FreeID)</ContentModel>
    public class MetaMem : SyncMLComplexElement
    {
        private MetaMem() : base(ElementNames.SyncMLMetInf, ElementNames.Mem) { }

        public static MetaMem Create(XElement xmlData)
        {
            if (xmlData == null)
                return null;

            MetaMem r = new MetaMem();
            r.FreeMem = SyncMLSimpleElementFactory.Create<MetaFreeMem>(ElementReader.Element(xmlData, ElementNames.FreeMem));
            r.FreeID = SyncMLSimpleElementFactory.Create<MetaFreeID>(ElementReader.Element(xmlData, ElementNames.FreeID));
            r.SharedMem = SyncMLSimpleElementFactory.Create<MetaSharedMem>(ElementReader.Element(xmlData, ElementNames.SharedMem));
            return r;
        }

        public static MetaMem Create()
        {
            MetaMem r = new MetaMem();
            r.FreeMem = new MetaFreeMem();
            r.FreeID = new MetaFreeID();
            return r;
        }


        private MetaSharedMem sharedMem;

        public MetaSharedMem SharedMem
        {
            get { if (sharedMem == null) sharedMem = new MetaSharedMem(); return sharedMem; }
            set { sharedMem = value; }
        }

        private MetaFreeMem freeMem;

        public MetaFreeMem FreeMem
        {
            get { return freeMem; }
            set { freeMem = value; ThrowExceptionIfNull(value, "Mem/FreeMem"); }
        }

        private MetaFreeID freeID;

        public MetaFreeID FreeID
        {
            get { return freeID; }
            set { freeID = value; ThrowExceptionIfNull(value, "Mem/FreeID"); }
        }

        public override XElement Xml
        {
            get
            {
                Element.RemoveAll();
                if (sharedMem != null)
                    Element.Add(sharedMem.Xml);

                Element.Add(FreeMem.Xml);
                Element.Add(FreeID.Xml);

                return Element;
            }
        }

    }

    /// <summary>
    /// Retrieve meta elements from a Meta container.
    /// SyncMLMeta may hold meta data of sync commands or device info, so it is not elegant to design SyncMLMeta as SyncMLComplexElement.
    /// The parser assume the data is about sync commands only, no device info.
    /// </summary>
    public class MetaParser
    {
        XElement navMeta;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="metaXml">Meta data including the Meta tag.</param>
        public MetaParser(XElement metaXml)
        {
            navMeta = metaXml;
        }

        XNamespace metaNamespace = ElementNames.SyncMLMetInf;

        public MetaFormat GetMetaFormat()
        {
            return SyncMLSimpleElementFactory.Create<MetaFormat>(navMeta.Element(metaNamespace + ElementNames.Format));
        }

        public MetaType GetMetaType()
        {
            return SyncMLSimpleElementFactory.Create<MetaType>(navMeta.Element(metaNamespace + ElementNames.Type));
        }

        public MetaAnchor GetMetaAnchor()
        {
            XElement n = navMeta.Element(metaNamespace + ElementNames.Anchor);
            if (n != null)
                return MetaAnchor.Create(n);
            else
                return null;
        }


        public MetaFieldLevel GetMetaFieldLevel()
        {
            return SyncMLSimpleElementFactory.Create<MetaFieldLevel>(navMeta.Element(metaNamespace + ElementNames.FieldLevel));
        }

        public MetaMark GetMetaMark()
        {
            return SyncMLSimpleElementFactory.Create<MetaMark>(navMeta.Element(metaNamespace + ElementNames.Mark));
        }

        public MetaMaxMsgSize GetMetaMaxMsgSize()
        {
            return SyncMLSimpleElementFactory.Create<MetaMaxMsgSize>(navMeta.Element(metaNamespace + ElementNames.MaxMsgSize));
        }

        public MetaMaxObjSize GetMetaMaxObjSize()
        {
            return SyncMLSimpleElementFactory.Create<MetaMaxObjSize>(navMeta.Element(metaNamespace + ElementNames.MaxObjSize));
        }

        public MetaMem GetMetaMem()
        {
            XElement n = navMeta.Element(metaNamespace + ElementNames.Mem);
            if (n != null)
                return MetaMem.Create(n);
            else
                return null;
        }

        public MetaNextNonce GetMetaNextNonce()
        {
            return SyncMLSimpleElementFactory.Create<MetaNextNonce>(navMeta.Element(metaNamespace + ElementNames.NextNonce));
        }

        public MetaSize GetMetaSize()
        {
            return SyncMLSimpleElementFactory.Create<MetaSize>(navMeta.Element(metaNamespace + ElementNames.Size));
        }

        public MetaVersion GetMetaVersion()
        {
            return SyncMLSimpleElementFactory.Create<MetaVersion>(navMeta.Element(metaNamespace + ElementNames.Version));
        }


    }

}
