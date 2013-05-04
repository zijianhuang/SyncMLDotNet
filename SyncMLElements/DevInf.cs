using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace Fonlow.SyncML.Elements
{
    public class DevinfCTType : SyncMLSimpleElement
    {
        public DevinfCTType()
            : base(ElementNames.SyncMLDevInf, ElementNames.CTType)
        {

        }
    }

    public class DevinfDataType : SyncMLSimpleElement
    {
        public DevinfDataType()
            : base(ElementNames.SyncMLDevInf, ElementNames.DataType)
        {

        }
    }

    public class DevinfDevID : SyncMLSimpleElement
    {
        public DevinfDevID()
            : base(ElementNames.SyncMLDevInf, ElementNames.DevID)
        {

        }
    }

    public class DevinfDevTyp : SyncMLSimpleElement
    {
        public DevinfDevTyp()
            : base(ElementNames.SyncMLDevInf, ElementNames.DevTyp)
        {

        }

    }
    public class DevinfDisplayName : SyncMLSimpleElement
    {
        public DevinfDisplayName()
            : base(ElementNames.SyncMLDevInf, ElementNames.DisplayName)
        {

        }
    }

    public class DevinfFwV : SyncMLSimpleElement
    {
        public DevinfFwV()
            : base(ElementNames.SyncMLDevInf, ElementNames.FwV)
        {

        }
    }

    public class DevinfHwV : SyncMLSimpleElement
    {
        public DevinfHwV()
            : base(ElementNames.SyncMLDevInf, ElementNames.HwV)
        {

        }
    }

    public class DevinfMan : SyncMLSimpleElement
    {
        public DevinfMan()
            : base(ElementNames.SyncMLDevInf, ElementNames.Man)
        {

        }
    }

    public class DevinfMaxGUIDSize : SyncMLSimpleElement
    {
        public DevinfMaxGUIDSize()
            : base(ElementNames.SyncMLDevInf, ElementNames.MaxGUIDSize)
        {

        }
    }

    public class DevinfMaxID : SyncMLSimpleElement
    {
        public DevinfMaxID()
            : base(ElementNames.SyncMLDevInf, ElementNames.MaxID)
        {

        }
    }

    public class DevinfMaxMem : SyncMLSimpleElement
    {
        public DevinfMaxMem()
            : base(ElementNames.SyncMLDevInf, ElementNames.MaxMem)
        {

        }
    }

    public class DevinfMod : SyncMLSimpleElement
    {
        public DevinfMod()
            : base(ElementNames.SyncMLDevInf, ElementNames.Mod)
        {

        }
    }

    public class DevinfOEM : SyncMLSimpleElement
    {
        public DevinfOEM()
            : base(ElementNames.SyncMLDevInf, ElementNames.OEM)
        {

        }
    }

    public class DevinfParamName : SyncMLSimpleElement
    {
        public DevinfParamName()
            : base(ElementNames.SyncMLDevInf, ElementNames.ParamName)
        {

        }
    }

    public class DevinfPropName : SyncMLSimpleElement
    {
        public DevinfPropName()
            : base(ElementNames.SyncMLDevInf, ElementNames.PropName)
        {

        }
    }

    public class DevinfSharedMem : SyncMLSimpleElement
    {
        public DevinfSharedMem()
            : base(ElementNames.SyncMLDevInf, ElementNames.SharedMem)
        {

        }
    }

    public class DevinfSize : SyncMLSimpleElement
    {
        public DevinfSize()
            : base(ElementNames.SyncMLDevInf, ElementNames.Size)
        {

        }
    }

    public class DevinfSourceRef : SyncMLSimpleElement
    {
        public DevinfSourceRef()
            : base(ElementNames.SyncMLDevInf, ElementNames.SourceRef)
        {

        }
    }

    public class DevinfSupportLargeObjs : SyncMLSimpleElement
    {
        public DevinfSupportLargeObjs()
            : base(ElementNames.SyncMLDevInf, ElementNames.SupportLargeObjs)
        {

        }
    }

    public class DevinfSupportHierarchicalSync : SyncMLSimpleElement
    {
        public DevinfSupportHierarchicalSync()
            : base(ElementNames.SyncMLDevInf, ElementNames.SupportHierarchicalSync)
        {

        }
    }

    public class DevinfSupportNumberOfChanges : SyncMLSimpleElement
    {
        public DevinfSupportNumberOfChanges()
            : base(ElementNames.SyncMLDevInf, ElementNames.SupportNumberOfChanges)
        {

        }
    }

    public class DevinfSwV : SyncMLSimpleElement
    {
        public DevinfSwV()
            : base(ElementNames.SyncMLDevInf, ElementNames.SwV)
        {

        }
    }

    public class DevinfSyncType : SyncMLSimpleElement
    {
        public DevinfSyncType()
            : base(ElementNames.SyncMLDevInf, ElementNames.SyncType)
        {

        }
    }

    public class DevinfUTC : SyncMLSimpleElement
    {
        public DevinfUTC()
            : base(ElementNames.SyncMLDevInf, ElementNames.UTC)
        {

        }
    }

    public class DevinfValEnum : SyncMLSimpleElement
    {
        public DevinfValEnum()
            : base(ElementNames.SyncMLDevInf, ElementNames.ValEnum)
        {

        }
    }

    public class DevinfVerCT : SyncMLSimpleElement
    {
        public DevinfVerCT()
            : base(ElementNames.SyncMLDevInf, ElementNames.VerCT)
        {

        }
    }

    public class DevinfVerDTD : SyncMLSimpleElement
    {
        public DevinfVerDTD()
            : base(ElementNames.SyncMLDevInf, ElementNames.VerDTD)
        {

        }
    }

    public class DevinfXNam : SyncMLSimpleElement
    {
        public DevinfXNam()
            : base(ElementNames.SyncMLDevInf, ElementNames.XNam)
        {

        }
    }

    public class DevinfXVal : SyncMLSimpleElement
    {
        public DevinfXVal()
            : base(ElementNames.SyncMLDevInf, ElementNames.XVal)
        {

        }
    }

    /// <summary>
    /// Specifies the supported filter grammars that can be received by the data store.
    /// </summary>
    ///<ParentElements> Datastore</ParentElements>
    ///<Restrictions> If a device supports filtering for a specific data store, then at least one Filter-Rx element MUST be present
    ///and it MUST support at least the ¡°syncml:filtertype-cgi¡± grammar. The following example shows the minimum requirements
    ///for a device that supports filtering on a specific data store.</Restrictions>
    ///<ContentModel>Filter-Rx (CTType, VerCT)</ContentModel>
    public class DevinfFilter_Rx : SyncMLComplexElement
    {
        private DevinfFilter_Rx() : base(ElementNames.SyncMLDevInf, ElementNames.Filter_Rx) { }

        public static DevinfFilter_Rx Create()
        {
            DevinfFilter_Rx r = new DevinfFilter_Rx();
            r.CTType = new DevinfCTType();
            r.VerCT = new DevinfVerCT();
            return r;
        }

        public static DevinfFilter_Rx Create(XElement xmlData)
        {
            if (xmlData == null)
                return null;

            DevinfFilter_Rx r = new DevinfFilter_Rx();
            r.CTType = SyncMLSimpleElementFactory.Create<DevinfCTType>(ElementReader.Element(xmlData,ElementNames.CTType));
            r.VerCT = SyncMLSimpleElementFactory.Create<DevinfVerCT>(ElementReader.Element(xmlData,ElementNames.VerCT));
            return r;
        }

        private DevinfCTType ctType;

        public DevinfCTType CTType
        {
            get { return ctType; }
            set { ctType = value; ThrowExceptionIfNull(value, "Filter-Rx/CTType"); }
        }

        private DevinfVerCT verCT;

        public DevinfVerCT VerCT
        {
            get { return verCT; }
            set { verCT = value; ThrowExceptionIfNull(value, "Filter-Rx/VerCT"); }
        }

        public override XElement Xml
        {
            get
            {
                Element.RemoveAll();
                Element.Add(CTType.Xml);
                Element.Add(VerCT.Xml);
                return Element;
            }
        }
    }

    /// <summary>
    /// Indicates the filtering capabilities.
    /// </summary>
    ///<ParentElements> Datastore</ParentElements>
    ///<ContentModel>FilterCap (CTType, VerCT, FilterKeyword*, PropName*)</ContentModel>
    public class DevinfFilterCap : SyncMLComplexElement
    {
        private DevinfFilterCap() : base(ElementNames.SyncMLDevInf, ElementNames.FilterCap) { }

        public static DevinfFilterCap Create()
        {
            DevinfFilterCap r = new DevinfFilterCap();
            r.CTType = new DevinfCTType();
            r.VerCT = new DevinfVerCT();
            r.filterKeywordCollection = new Collection<DevinfFilterKeyword>();
            r.propNameCollection = new Collection<DevinfPropName>();
            return r;
        }

        public static DevinfFilterCap Create(XElement xmlData)
        {
            if (xmlData == null)
                return null;

            DevinfFilterCap r = new DevinfFilterCap();
            r.CTType = SyncMLSimpleElementFactory.Create<DevinfCTType>(ElementReader.Element(xmlData,ElementNames.CTType));
            r.VerCT = SyncMLSimpleElementFactory.Create<DevinfVerCT>(ElementReader.Element(xmlData,ElementNames.VerCT));
            r.filterKeywordCollection = new Collection<DevinfFilterKeyword>();
            r.propNameCollection = new Collection<DevinfPropName>();
            IEnumerable<XElement> keywordNodes = ElementReader.Elements(xmlData,ElementNames.FilterKeyword);
            foreach (XElement node in keywordNodes)
            {
                r.FilterKeywordCollection.Add(SyncMLSimpleElementFactory.Create<DevinfFilterKeyword>(node));
            }

            IEnumerable<XElement> propNameNodes = ElementReader.Elements(xmlData,ElementNames.PropName);
            foreach (XElement node in propNameNodes)
            {
                r.PropNameCollection.Add(SyncMLSimpleElementFactory.Create<DevinfPropName>(node));
            }
            return r;
        }

        private DevinfCTType ctType;

        public DevinfCTType CTType
        {
            get { return ctType; }
            set { ctType = value; ThrowExceptionIfNull(value, "Filter-Rx/CTType"); }
        }

        private DevinfVerCT verCT;

        public DevinfVerCT VerCT
        {
            get { return verCT; }
            set { verCT = value; ThrowExceptionIfNull(value, "Filter-Rx/VerCT"); }
        }

        internal Collection<DevinfFilterKeyword> filterKeywordCollection;

        public Collection<DevinfFilterKeyword> FilterKeywordCollection
        {
            get { return filterKeywordCollection; }
        }

        internal Collection<DevinfPropName> propNameCollection;

        public Collection<DevinfPropName> PropNameCollection
        {
            get { return propNameCollection; }
        }


        public override XElement Xml
        {
            get
            {
                Element.RemoveAll();
                Element.Add(CTType.Xml);
                Element.Add(VerCT.Xml);
                foreach (DevinfFilterKeyword item in FilterKeywordCollection)
                {
                    Element.Add(item.Xml);
                }

                foreach (DevinfPropName item in PropNameCollection)
                {
                    Element.Add(item.Xml);
                }
                return Element;
            }
        }
    }

    public class DevinfFilterKeyword : SyncMLSimpleElement
    {
        public DevinfFilterKeyword()
            : base(ElementNames.SyncMLDevInf, ElementNames.FilterKeyword)
        {

        }
    }

    public class DevinfMaxOccur : SyncMLSimpleElement
    {
        public DevinfMaxOccur()
            : base(ElementNames.SyncMLDevInf, ElementNames.MaxOccur)
        {

        }
    }

    public class DevinfMaxSize : SyncMLSimpleElement
    {
        public DevinfMaxSize()
            : base(ElementNames.SyncMLDevInf, ElementNames.MaxSize)
        {

        }
    }

    public class DevinfNoTruncate : SyncMLSimpleElement
    {
        public DevinfNoTruncate()
            : base(ElementNames.SyncMLDevInf, ElementNames.NoTruncate)
        {

        }
    }

    /// <summary>
    /// Specifies the synchronization capabilities of the given local datastore.
    /// </summary>
    ///<ParentElements>DataStore</ParentElements> 
    ///<ContentModel>(SyncType+)</ContentModel>
    public class DevinfSyncCap : SyncMLComplexElement
    {
        private DevinfSyncCap()
            : base(ElementNames.SyncMLDevInf, ElementNames.SyncCap)
        {

        }

        public static DevinfSyncCap Create()
        {
            DevinfSyncCap r = new DevinfSyncCap();
            r.syncTypeCollection = new Collection<DevinfSyncType>();
            return r;
        }

        public static DevinfSyncCap Create(XElement xmlData)
        {
            if (xmlData == null)
                return null;

            DevinfSyncCap r = new DevinfSyncCap();
            r.syncTypeCollection = new Collection<DevinfSyncType>();
            IEnumerable<XElement> nodes = ElementReader.Elements(xmlData,ElementNames.SyncType);
            foreach (XElement node in nodes)
            {
                r.syncTypeCollection.Add(SyncMLSimpleElementFactory.Create<DevinfSyncType>(node));
            }

            return r;
        }

        private Collection<DevinfSyncType> syncTypeCollection;

        public Collection<DevinfSyncType> SyncTypeCollection
        {
            get { return syncTypeCollection; }
        }

        public override XElement Xml
        {
            get
            {
                Element.RemoveAll();
                foreach (DevinfSyncType item in SyncTypeCollection)
                    Element.Add(item.Xml);
                return Element;
            }
        }
    }

    /// <summary>
    /// Specifies the non-standard, experimental extensions supported by the device. The
    /// extensions are specified in terms of the XML element type name and the value.
    /// </summary>
    ///<ContentModel>(XNam, Xval*)</ContentModel>
    public class DevinfExt : SyncMLComplexElement
    {
        private DevinfExt()
            : base(ElementNames.SyncMLDevInf, ElementNames.Ext)
        {

        }

        public static DevinfExt Create()
        {
            DevinfExt r = new DevinfExt();
            r.XNam = new DevinfXNam();
            return r;
        }

        public static DevinfExt Create(XElement xmlData)
        {
            if (xmlData == null)
                return null;

            DevinfExt r = new DevinfExt();
            r.XNam = SyncMLSimpleElementFactory.Create<DevinfXNam>(ElementReader.Element(xmlData,ElementNames.XNam));
            IEnumerable<XElement> nodes = ElementReader.Elements(xmlData,ElementNames.XVal);
            r.xValCollection = new Collection<DevinfXVal>();
            foreach (XElement node in nodes)
            {
                r.XValCollection.Add(SyncMLSimpleElementFactory.Create<DevinfXVal>(node));
            }

            return r;
        }

        private DevinfXNam xNam;

        public DevinfXNam XNam
        {
            get { return xNam; }
            set { xNam = value; }
        }

        private Collection<DevinfXVal> xValCollection;

        public Collection<DevinfXVal> XValCollection
        {
            get { return xValCollection; }
        }


        public override XElement Xml
        {
            get
            {
                Element.RemoveAll();
                Element.Add(XNam.Xml);
                foreach (DevinfXVal item in XValCollection)
                    Element.Add(item.Xml);
                return Element;
            }
        }
    }

    public class DevinfFieldLevel : SyncMLSimpleElement
    {
        public DevinfFieldLevel()
            : base(ElementNames.SyncMLDevInf, ElementNames.FieldLevel)
        {

        }
    }

    /// <summary>
    /// Specifies a supported parameter of a given property
    /// </summary>
    /// Usage: 
    ///<ContentModel>PropParam (ParamName, DataType?, ValEnum*, DisplayName?)</ContentModel>
    public class DevinfPropParam : SyncMLComplexElement
    {

        public override XElement Xml
        {
            get
            {
                Element.RemoveAll();
                Element.Add(ParamName.Xml);
                if (dataType != null)
                    Element.Add(dataType.Xml);

                foreach (DevinfValEnum item in ValEnumCollection)
                    Element.Add(item.Xml);

                if (displayName != null)
                    Element.Add(displayName.Xml);
                return Element;

            }
        }
        private DevinfPropParam() : base(ElementNames.SyncMLDevInf, ElementNames.PropParam) { }

        public static DevinfPropParam Create()
        {
            DevinfPropParam r = new DevinfPropParam();
            r.ParamName = new DevinfParamName();
            r.valEnumCollection = new Collection<DevinfValEnum>();
            return r;
        }

        public static DevinfPropParam Create(XElement xmlData)
        {
            if (xmlData == null)
                return null;

            DevinfPropParam r = new DevinfPropParam();
            r.ParamName = SyncMLSimpleElementFactory.Create<DevinfParamName>(ElementReader.Element(xmlData,ElementNames.ParamName));
            r.DataType = SyncMLSimpleElementFactory.Create<DevinfDataType>(ElementReader.Element(xmlData,ElementNames.DataType));
            r.DisplayName = SyncMLSimpleElementFactory.Create<DevinfDisplayName>(ElementReader.Element(xmlData,ElementNames.DisplayName));
            r.valEnumCollection = new Collection<DevinfValEnum>();
            IEnumerable<XElement> nodes = ElementReader.Elements(xmlData,ElementNames.ValEnum);
            foreach (XElement node in nodes)
            {
                r.ValEnumCollection.Add(SyncMLSimpleElementFactory.Create<DevinfValEnum>(node));
            }

            return r;
        }


        private DevinfParamName paramName;

        public DevinfParamName ParamName
        {
            get { return paramName; }
            set { paramName = value; ThrowExceptionIfNull(value, "PropParam/ParamName"); }
        }

        private DevinfDataType dataType;

        public DevinfDataType DataType
        {
            get { if (dataType == null) dataType = new DevinfDataType(); return dataType; }
            set { dataType = value; }
        }

        private DevinfDisplayName displayName;

        public DevinfDisplayName DisplayName
        {
            get { if (displayName == null) displayName = new DevinfDisplayName(); return displayName; }
            set { displayName = value; }
        }

        private Collection<DevinfValEnum> valEnumCollection;

        public Collection<DevinfValEnum> ValEnumCollection
        {
            get { return valEnumCollection; }
        }

    }

    /// <summary>
    /// Specifies a supported property of a given content type.
    /// </summary>
    ///<ParentElements> CTCap</ParentElements>
    ///<ContentModel>Property (PropName, DataType?, MaxOccur?, MaxSize?, NoTruncate?, ValEnum*,DisplayName?, PropParam*)</ContentModel>
    public class DevinfProperty : SyncMLComplexElement
    {
        public override XElement Xml
        {
            get
            {
                Element.RemoveAll();
                Element.Add(PropName.Xml);
                if (dataType != null)
                    Element.Add(dataType.Xml);

                if (maxOccur != null)
                    Element.Add(maxOccur.Xml);

                if (maxSize != null)
                    Element.Add(maxSize.Xml);

                if (noTruncate != null)
                    Element.Add(noTruncate.Xml);

                foreach (DevinfValEnum item in ValEnumCollection)
                    Element.Add(item.Xml);

                if (displayName != null)
                    Element.Add(displayName.Xml);

                foreach (DevinfPropParam item in PropParamCollection)
                    Element.Add(item.Xml);

                return Element;
            }
        }


        private DevinfProperty() : base(ElementNames.SyncMLDevInf, ElementNames.Property) { }

        public static DevinfProperty Create()
        {
            DevinfProperty r = new DevinfProperty();
            r.PropName = new DevinfPropName();
            r.valEnumCollection = new Collection<DevinfValEnum>();
            r.propParamCollection = new Collection<DevinfPropParam>();
            return r;
        }

        public static DevinfProperty Create(XElement xmlData)
        {
            DevinfProperty r = new DevinfProperty();
            r.propName = SyncMLSimpleElementFactory.Create<DevinfPropName>(ElementReader.Element(xmlData,ElementNames.PropName));
            r.dataType = SyncMLSimpleElementFactory.Create<DevinfDataType>(ElementReader.Element(xmlData,ElementNames.DataType));
            r.maxOccur = SyncMLSimpleElementFactory.Create<DevinfMaxOccur>(ElementReader.Element(xmlData,ElementNames.MaxOccur));
            r.maxSize = SyncMLSimpleElementFactory.Create<DevinfMaxSize>(ElementReader.Element(xmlData,ElementNames.MaxSize));
            r.noTruncate = SyncMLSimpleElementFactory.Create<DevinfNoTruncate>(ElementReader.Element(xmlData,ElementNames.NoTruncate));
            r.displayName = SyncMLSimpleElementFactory.Create<DevinfDisplayName>(ElementReader.Element(xmlData,ElementNames.DisplayName));
            r.valEnumCollection = new Collection<DevinfValEnum>();
            r.propParamCollection = new Collection<DevinfPropParam>();
            IEnumerable<XElement> valEnumNodes = ElementReader.Elements(xmlData,ElementNames.ValEnum);
            foreach (XElement node in valEnumNodes)
            {
                r.ValEnumCollection.Add(SyncMLSimpleElementFactory.Create<DevinfValEnum>(node));
            }
            IEnumerable<XElement> propParamNodes = ElementReader.Elements(xmlData,ElementNames.PropParam);
            foreach (XElement node in propParamNodes)
            {
                r.PropParamCollection.Add(DevinfPropParam.Create(node));
            }
            return r;
        }

        private Collection<DevinfPropParam> propParamCollection;

        public Collection<DevinfPropParam> PropParamCollection
        {
            get { return propParamCollection; }
        }

        private Collection<DevinfValEnum> valEnumCollection;

        public Collection<DevinfValEnum> ValEnumCollection
        {
            get { return valEnumCollection; }
        }

        private DevinfPropName propName;

        public DevinfPropName PropName
        {
            get { return propName; }
            set { propName = value; ThrowExceptionIfNull(value, "Property/PropName"); }
        }

        private DevinfNoTruncate noTruncate;

        public DevinfNoTruncate NoTruncate
        {
            get { if (noTruncate == null) noTruncate = new DevinfNoTruncate(); return noTruncate; }
            set { noTruncate = value; }
        }

        private DevinfDisplayName displayName;

        public DevinfDisplayName DisplayName
        {
            get { if (displayName == null) displayName = new DevinfDisplayName(); return displayName; }
            set { displayName = value; }
        }



        private DevinfMaxOccur maxOccur;

        public DevinfMaxOccur MaxOccur
        {
            get { if (maxOccur == null) maxOccur = new DevinfMaxOccur(); return maxOccur; }
            set { maxOccur = value; }
        }

        private DevinfMaxSize maxSize;

        public DevinfMaxSize MaxSize
        {
            get { if (maxSize == null) maxSize = new DevinfMaxSize(); return maxSize; }
            set { maxSize = value; }
        }



        private DevinfDataType dataType;

        public DevinfDataType DataType
        {
            get { if (dataType == null) dataType = new DevinfDataType(); return dataType; }
            set { dataType = value; }
        }



    }

    /// <summary>
    /// Content Model:(CTType, VerCT)
    /// </summary>
    public class DevinfRx : SyncMLComplexElement
    {
        private DevinfRx()
            : base(ElementNames.SyncMLDevInf, ElementNames.Rx)
        {

        }

        public static DevinfRx Create()
        {
            DevinfRx r = new DevinfRx();
            r.CTType = new DevinfCTType();
            r.VerCT = new DevinfVerCT();
            return r;
        }

        public static DevinfRx Create(XElement xmlData)
        {
            if (xmlData == null)
                return null;

            DevinfRx r = new DevinfRx();
            r.CTType = SyncMLSimpleElementFactory.Create<DevinfCTType>(ElementReader.Element(xmlData,ElementNames.CTType));
            r.VerCT = SyncMLSimpleElementFactory.Create<DevinfVerCT>(ElementReader.Element(xmlData,ElementNames.VerCT));
            return r;
        }

        private DevinfCTType ctType;

        public DevinfCTType CTType
        {
            get { return ctType; }
            set { ctType = value; }
        }

        private DevinfVerCT verCT;

        public DevinfVerCT VerCT
        {
            get { return verCT; }
            set { verCT = value; }
        }


        public override XElement Xml
        {
            get
            {
                Element.RemoveAll();
                Element.Add(CTType.Xml);
                Element.Add(VerCT.Xml);
                return Element;
            }
        }
    }

    /// <summary>
    /// Content Model:(CTType, VerCT)
    /// </summary>
    public class DevinfRx_Pref : SyncMLComplexElement
    {
        private DevinfRx_Pref()
            : base(ElementNames.SyncMLDevInf, ElementNames.Rx_Pref)
        {

        }

        public static DevinfRx_Pref Create()
        {
            DevinfRx_Pref r = new DevinfRx_Pref();
            r.CTType = new DevinfCTType();
            r.VerCT = new DevinfVerCT();
            return r;
        }

        public static DevinfRx_Pref Create(XElement xmlData)
        {
            if (xmlData == null)
                return null;

            DevinfRx_Pref r = new DevinfRx_Pref();
            r.CTType = SyncMLSimpleElementFactory.Create<DevinfCTType>(ElementReader.Element(xmlData,ElementNames.CTType));
            r.VerCT = SyncMLSimpleElementFactory.Create<DevinfVerCT>(ElementReader.Element(xmlData,ElementNames.VerCT));
            return r;
        }

        private DevinfCTType ctType;

        public DevinfCTType CTType
        {
            get { return ctType; }
            set { ctType = value; }
        }

        private DevinfVerCT verCT;

        public DevinfVerCT VerCT
        {
            get { return verCT; }
            set { verCT = value; }
        }


        public override XElement Xml
        {
            get
            {
                Element.RemoveAll();
                Element.Add(CTType.Xml);
                Element.Add(VerCT.Xml);
                return Element;
            }
        }
    }

    /// <summary>
    /// Content Model:(CTType, VerCT)
    /// </summary>
    public class DevinfTx : SyncMLComplexElement
    {
        private DevinfTx()
            : base(ElementNames.SyncMLDevInf, ElementNames.Tx)
        {

        }

        public static DevinfTx Create()
        {
            DevinfTx r = new DevinfTx();
            r.CTType = new DevinfCTType();
            r.VerCT = new DevinfVerCT();
            return r;
        }

        public static DevinfTx Create(XElement xmlData)
        {
            if (xmlData == null)
                return null;

            DevinfTx r = new DevinfTx();
            r.CTType = SyncMLSimpleElementFactory.Create<DevinfCTType>(ElementReader.Element(xmlData,ElementNames.CTType));
            r.VerCT = SyncMLSimpleElementFactory.Create<DevinfVerCT>(ElementReader.Element(xmlData,ElementNames.VerCT));
            return r;
        }

        private DevinfCTType ctType;

        public DevinfCTType CTType
        {
            get { return ctType; }
            set { ctType = value; }
        }

        private DevinfVerCT verCT;

        public DevinfVerCT VerCT
        {
            get { return verCT; }
            set { verCT = value; }
        }


        public override XElement Xml
        {
            get
            {
                Element.RemoveAll();
                Element.Add(CTType.Xml);
                Element.Add(VerCT.Xml);
                return Element;
            }
        }
    }

    /// <summary>
    /// Content Model:(CTType, VerCT)
    /// </summary>
    public class DevinfTx_Pref : SyncMLComplexElement
    {
        private DevinfTx_Pref()
            : base(ElementNames.SyncMLDevInf, ElementNames.Tx_Pref)
        {

        }

        public static DevinfTx_Pref Create()
        {
            DevinfTx_Pref r = new DevinfTx_Pref();
            r.CTType = new DevinfCTType();
            r.VerCT = new DevinfVerCT();
            return r;
        }

        public static DevinfTx_Pref Create(XElement xmlData)
        {
            if (xmlData == null)
                return null;

            DevinfTx_Pref r = new DevinfTx_Pref();
            r.CTType = SyncMLSimpleElementFactory.Create<DevinfCTType>(ElementReader.Element(xmlData,ElementNames.CTType));
            r.VerCT = SyncMLSimpleElementFactory.Create<DevinfVerCT>(ElementReader.Element(xmlData,ElementNames.VerCT));
            return r;
        }

        private DevinfCTType ctType;

        public DevinfCTType CTType
        {
            get { return ctType; }
            set { ctType = value; }
        }

        private DevinfVerCT verCT;

        public DevinfVerCT VerCT
        {
            get { return verCT; }
            set { verCT = value; }
        }


        public override XElement Xml
        {
            get
            {
                Element.RemoveAll();
                Element.Add(CTType.Xml);
                Element.Add(VerCT.Xml);
                return Element;
            }
        }
    }

    /// <summary>
    /// Specifies the maximum memory and item identifier for a given local datastore.
    /// </summary>
    ///<ParentElements> DataStore</ParentElements>
    ///<ContentModel>(SharedMem?, MaxMem?, MaxID?)</ContentModel>
    public class DevinfDSMem : SyncMLComplexElement
    {
        private DevinfDSMem()
            : base(ElementNames.SyncMLDevInf, ElementNames.DSMem)
        {

        }

        public static DevinfDSMem Create()
        {
            DevinfDSMem r = new DevinfDSMem();
            return r;
        }

        public static DevinfDSMem Create(XElement xmlData)
        {
            if (xmlData == null)
                return null;

            DevinfDSMem r = new DevinfDSMem();
            r.SharedMem = SyncMLSimpleElementFactory.Create<DevinfSharedMem>(ElementReader.Element(xmlData,ElementNames.SharedMem));
            r.MaxMem = SyncMLSimpleElementFactory.Create<DevinfMaxMem>(ElementReader.Element(xmlData,ElementNames.MaxMem));
            r.MaxID = SyncMLSimpleElementFactory.Create<DevinfMaxID>(ElementReader.Element(xmlData,ElementNames.MaxID));
            return r;

        }

        private DevinfSharedMem sharedMem;

        public DevinfSharedMem SharedMem
        {
            get { if (sharedMem == null) sharedMem = new DevinfSharedMem(); return sharedMem; }
            set { sharedMem = value; }
        }

        private DevinfMaxMem maxMem;

        public DevinfMaxMem MaxMem
        {
            get { if (maxMem == null) maxMem = new DevinfMaxMem(); return maxMem; }
            set { maxMem = value; }
        }

        private DevinfMaxID maxID;

        public DevinfMaxID MaxID
        {
            get { if (maxID == null) maxID = new DevinfMaxID(); return maxID; }
            set { maxID = value; }
        }


        public override XElement Xml
        {
            get
            {
                Element.RemoveAll();
                if (sharedMem != null)
                    Element.Add(sharedMem.Xml);
                if (maxMem != null)
                    Element.Add(maxMem.Xml);
                if (maxID != null)
                    Element.Add(maxID.Xml);
                return Element;
            }
        }
    }

    /// <summary>
    /// Specifies the properties of a given local datastore.
    /// </summary>
    /// Usage: 
    ///<ContentModel>
    ///(SourceRef, DisplayName?, MaxGUIDSize?, Rx-Pref, Rx*, Tx-Pref,Tx*, CTCap+, DSMem?, SupportHierarchicalSync?, SyncCap, 
    ///Filter-Rx*,FilterCap*)</ContentModel>
    public class DevinfDataStore : SyncMLComplexElement
    {
        public override XElement Xml
        {
            get
            {
                Element.RemoveAll();
                Element.Add(SourceRef.Xml);
                if (displayName != null)
                    Element.Add(displayName.Xml);
                if (maxGUIDSize != null)
                    Element.Add(maxGUIDSize.Xml);
                Element.Add(RxPref.Xml);
                foreach (DevinfRx item in RxCollection)
                    Element.Add(item.Xml);

                Element.Add(Tx_Pref.Xml);
                foreach (DevinfTx item in TxCollection)
                    Element.Add(item.Xml);

                foreach (DevinfCTCap item in CTCapCollection)
                    Element.Add(item.Xml);

                if (dsMem != null)
                    Element.Add(dsMem.Xml);
                if (supportHierarchicalSync != null)
                    Element.Add(supportHierarchicalSync.Xml);
                Element.Add(SyncCap.Xml);
                foreach (DevinfFilter_Rx item in Filter_RxCollection)
                    Element.Add(item.Xml);

                foreach (DevinfFilterCap item in FilterCapCollection)
                    Element.Add(item.Xml);

                return Element;

            }
        }

        private DevinfDataStore() : base(ElementNames.SyncMLDevInf, ElementNames.DataStore) { }

        public static DevinfDataStore Create(XElement xmlData)
        {
            if (xmlData == null)
                return null;
            
            DevinfDataStore r = new DevinfDataStore();
            r.SourceRef = SyncMLSimpleElementFactory.Create<DevinfSourceRef>(ElementReader.Element(xmlData,ElementNames.SourceRef));
            r.DisplayName = SyncMLSimpleElementFactory.Create<DevinfDisplayName>(ElementReader.Element(xmlData,ElementNames.DisplayName));
            r.MaxGUIDSize = SyncMLSimpleElementFactory.Create<DevinfMaxGUIDSize>(ElementReader.Element(xmlData,ElementNames.MaxGUIDSize));
            r.RxPref = DevinfRx_Pref.Create(ElementReader.Element(xmlData,ElementNames.Rx_Pref));
            r.Tx_Pref = DevinfTx_Pref.Create(ElementReader.Element(xmlData,ElementNames.Tx_Pref));
            r.DSMem = DevinfDSMem.Create(ElementReader.Element(xmlData,ElementNames.DSMem));
            r.SupportHierarchicalSync = SyncMLSimpleElementFactory.Create<DevinfSupportHierarchicalSync>(ElementReader.Element(xmlData,"SupportHierarchicalSync"));
            r.SyncCap = DevinfSyncCap.Create(ElementReader.Element(xmlData,ElementNames.SyncCap));

            r.rxCollection = new Collection<DevinfRx>();
            IEnumerable<XElement> rxNodes = ElementReader.Elements(xmlData,ElementNames.Rx);
            foreach (XElement node in rxNodes)
            {
                r.RxCollection.Add(DevinfRx.Create(node));
            }
            r.txCollection = new Collection<DevinfTx>();
            IEnumerable<XElement> txNodes = ElementReader.Elements(xmlData,ElementNames.Tx);
            foreach (XElement node in txNodes)
            {
                r.TxCollection.Add(DevinfTx.Create(node));
            }
            r.ctCapCollection = new Collection<DevinfCTCap>();
            IEnumerable<XElement> ctCapNodes = ElementReader.Elements(xmlData,ElementNames.CTCap);
            foreach (XElement node in ctCapNodes)
            {
                r.CTCapCollection.Add(DevinfCTCap.Create(node));
            }
            r.filter_RxCollection = new Collection<DevinfFilter_Rx>();
            IEnumerable<XElement> filterRxNodes = ElementReader.Elements(xmlData,ElementNames.Filter_Rx);
            foreach (XElement node in filterRxNodes)
            {
                r.Filter_RxCollection.Add(DevinfFilter_Rx.Create(node));
            }
            r.filterCapCollection = new Collection<DevinfFilterCap>();
            IEnumerable<XElement> filterCapNodes = ElementReader.Elements(xmlData,ElementNames.FilterCap);
            foreach (XElement node in filterCapNodes)
            {
                r.FilterCapCollection.Add(DevinfFilterCap.Create(node));
            }
            return r;
        }


        public static DevinfDataStore Create()
        {
            DevinfDataStore r = new DevinfDataStore();
            r.SourceRef = new DevinfSourceRef();
            r.RxPref = DevinfRx_Pref.Create();
            r.Tx_Pref = DevinfTx_Pref.Create();
            r.rxCollection = new Collection<DevinfRx>();
            r.txCollection = new Collection<DevinfTx>();
            r.ctCapCollection = new Collection<DevinfCTCap>();
            r.SyncCap = DevinfSyncCap.Create();
            r.filter_RxCollection = new Collection<DevinfFilter_Rx>();
            r.filterCapCollection = new Collection<DevinfFilterCap>();
            return r;
        }


        internal Collection<DevinfTx> txCollection;

        public Collection<DevinfTx> TxCollection
        {
            get { return txCollection; }
        }

        internal Collection<DevinfCTCap> ctCapCollection;

        public Collection<DevinfCTCap> CTCapCollection
        {
            get { return ctCapCollection; }
        }

        internal Collection<DevinfFilterCap> filterCapCollection;

        public Collection<DevinfFilterCap> FilterCapCollection
        {
            get { return filterCapCollection; }
        }



        private DevinfSupportHierarchicalSync supportHierarchicalSync;

        public DevinfSupportHierarchicalSync SupportHierarchicalSync
        {
            get { if (supportHierarchicalSync == null) supportHierarchicalSync = new DevinfSupportHierarchicalSync(); return supportHierarchicalSync; }
            set { supportHierarchicalSync = value; }
        }

        private DevinfSyncCap syncCap;

        public DevinfSyncCap SyncCap
        {
            get { return syncCap; }
            set { syncCap = value; ThrowExceptionIfNull(value, "DataStore/SyncCap"); }
        }

        internal Collection<DevinfFilter_Rx> filter_RxCollection;

        public Collection<DevinfFilter_Rx> Filter_RxCollection
        {
            get { return filter_RxCollection; }
        }


        private DevinfSourceRef sourceRef;

        public DevinfSourceRef SourceRef
        {
            get { return sourceRef; }
            set { sourceRef = value; ThrowExceptionIfNull(value, "DataStore/SourceRef"); }
        }

        private DevinfDisplayName displayName;

        public DevinfDisplayName DisplayName
        {
            get { if (displayName == null) displayName = new DevinfDisplayName(); return displayName; }
            set { displayName = value; }
        }

        private DevinfDSMem dsMem;

        public DevinfDSMem DSMem
        {
            get { if (dsMem == null) dsMem = DevinfDSMem.Create(); return dsMem; }
            set { dsMem = value; }
        }

        private DevinfTx_Pref tx_Pref;

        public DevinfTx_Pref Tx_Pref
        {
            get { return tx_Pref; }
            set { tx_Pref = value; ThrowExceptionIfNull(value, "DataStore/Tx-Pref"); }
        }

        private DevinfMaxGUIDSize maxGUIDSize;

        public DevinfMaxGUIDSize MaxGUIDSize
        {
            get { if (maxGUIDSize == null) maxGUIDSize = new DevinfMaxGUIDSize(); return maxGUIDSize; }
            set { maxGUIDSize = value; }
        }

        private DevinfRx_Pref rxPref;

        public DevinfRx_Pref RxPref
        {
            get { return rxPref; }
            set { rxPref = value; ThrowExceptionIfNull(value, "DataStore/Rx-Pref"); }
        }

        internal Collection<DevinfRx> rxCollection;

        public Collection<DevinfRx> RxCollection
        {
            get { return rxCollection; }
        }

    }

    /// <summary>
    /// Specifies the root or document element type of the Device Information document.
    /// </summary>
    /// Usage: 
    ///<ContentModel>VerDTD, Man, Mod, OEM?, FwV, SwV, HwV, DevID, DevTyp, UTC?,
    ///SupportLargeObjs?, SupportNumberOfChanges?, DataStore+, Ext*
    ///  </ContentModel>
    public class DevInf : SyncMLComplexElement
    {
        public override XElement Xml
        {
            get
            {
                Element.RemoveAll();
                Element.Add(VerDTD.Xml);
                Element.Add(Man.Xml);
                Element.Add(Mod.Xml);
                if (oem != null)
                    Element.Add(oem.Xml);
                Element.Add(FwV.Xml);
                Element.Add(SwV.Xml);
                Element.Add(HwV.Xml);
                Element.Add(DevID.Xml);
                Element.Add(DevTyp.Xml);
                if (utc != null)
                    Element.Add(utc.Xml);
                if (supportLargeObjs != null)
                    Element.Add(supportLargeObjs.Xml);
                if (supportNumberOfChanges != null)
                    Element.Add(supportNumberOfChanges.Xml);
                foreach (DevinfDataStore item in DataStoreCollection)
                    Element.Add(item.Xml);

                foreach (DevinfExt item in ExtCollection)
                    Element.Add(item.Xml);

                return Element;
            }
        }



        private DevInf() : base(ElementNames.SyncMLDevInf, ElementNames.DevInf) { }

        public static DevInf Create(XElement xmlData)
        {
            if (xmlData == null)
                return null;
            DevInf r = new DevInf();
            r.Man = SyncMLSimpleElementFactory.Create<DevinfMan>(ElementReader.Element(xmlData,ElementNames.Man));
            r.VerDTD = SyncMLSimpleElementFactory.Create<DevinfVerDTD>(ElementReader.Element(xmlData,ElementNames.VerDTD));
            r.Mod = SyncMLSimpleElementFactory.Create<DevinfMod>(ElementReader.Element(xmlData,ElementNames.Mod));
            r.FwV = SyncMLSimpleElementFactory.Create<DevinfFwV>(ElementReader.Element(xmlData,ElementNames.FwV));
            r.SwV = SyncMLSimpleElementFactory.Create<DevinfSwV>(ElementReader.Element(xmlData,ElementNames.SwV));
            r.HwV = SyncMLSimpleElementFactory.Create<DevinfHwV>(ElementReader.Element(xmlData,ElementNames.HwV));
            r.DevID = SyncMLSimpleElementFactory.Create<DevinfDevID>(ElementReader.Element(xmlData,ElementNames.DevID));
            r.DevTyp = SyncMLSimpleElementFactory.Create<DevinfDevTyp>(ElementReader.Element(xmlData,ElementNames.DevTyp));
            r.OEM = SyncMLSimpleElementFactory.Create<DevinfOEM>(ElementReader.Element(xmlData,ElementNames.OEM));
            r.UTC = SyncMLSimpleElementFactory.Create<DevinfUTC>(ElementReader.Element(xmlData,ElementNames.UTC));
            r.SupportLargeObjs = SyncMLSimpleElementFactory.Create<DevinfSupportLargeObjs>(ElementReader.Element(xmlData,ElementNames.SupportLargeObjs));
            r.SupportNumberOfChanges = SyncMLSimpleElementFactory.Create<DevinfSupportNumberOfChanges>(ElementReader.Element(xmlData,ElementNames.SupportNumberOfChanges));
            r.dataStoreCollection = new Collection<DevinfDataStore>();
            r.extCollection = new Collection<DevinfExt>();
            IEnumerable<XElement> dataStoreNodes = ElementReader.Elements(xmlData,ElementNames.DataStore);
            foreach (XElement node in dataStoreNodes)
            {
                r.DataStoreCollection.Add(DevinfDataStore.Create(node));
            }
            IEnumerable<XElement> extNodes = ElementReader.Elements(xmlData, ElementNames.Ext);
            foreach (XElement node in extNodes)
            {
                r.ExtCollection.Add(DevinfExt.Create(node));
            }
            return r;
        }

        public static DevInf Create()
        {
            DevInf r = new DevInf();

            r.VerDTD = new DevinfVerDTD();
            r.Man = new DevinfMan();
            r.Mod = new DevinfMod();
            r.FwV = new DevinfFwV();
            r.SwV = new DevinfSwV();
            r.HwV = new DevinfHwV();
            r.DevID = new DevinfDevID();
            r.DevTyp = new DevinfDevTyp();
            r.dataStoreCollection = new Collection<DevinfDataStore>();
            r.extCollection = new Collection<DevinfExt>();
            return r;
        }

        internal Collection<DevinfDataStore> dataStoreCollection;

        public Collection<DevinfDataStore> DataStoreCollection
        {
            get { return dataStoreCollection; }
        }

        internal Collection<DevinfExt> extCollection;

        public Collection<DevinfExt> ExtCollection
        {
            get { return extCollection; }
        }

        private DevinfVerDTD verDTD;

        public DevinfVerDTD VerDTD
        {
            get { if (verDTD == null) verDTD = new DevinfVerDTD(); return verDTD; }
            set { verDTD = value; }
        }

        private DevinfUTC utc;

        public DevinfUTC UTC
        {
            get { if (utc == null) utc = new DevinfUTC(); return utc; }
            set { utc = value; }
        }

        private DevinfSupportLargeObjs supportLargeObjs;

        public DevinfSupportLargeObjs SupportLargeObjs
        {
            get { if (supportLargeObjs == null) supportLargeObjs = new DevinfSupportLargeObjs(); return supportLargeObjs; }
            set { supportLargeObjs = value; }
        }

        private DevinfSupportNumberOfChanges supportNumberOfChanges;

        public DevinfSupportNumberOfChanges SupportNumberOfChanges
        {
            get { if (supportNumberOfChanges == null) supportNumberOfChanges = new DevinfSupportNumberOfChanges(); return supportNumberOfChanges; }
            set { supportNumberOfChanges = value; }
        }



        private DevinfSwV swv;

        public DevinfSwV SwV
        {
            get { if (swv == null) swv = new DevinfSwV(); return swv; }
            set { swv = value; }
        }

        private DevinfHwV hwV;

        public DevinfHwV HwV
        {
            get { if (hwV == null) hwV = new DevinfHwV(); return hwV; }
            set { hwV = value; }
        }

        private DevinfDevID devID;

        public DevinfDevID DevID
        {
            get { if (devID == null) devID = new DevinfDevID(); return devID; }
            set { devID = value; }
        }

        private DevinfDevTyp devTyp;

        public DevinfDevTyp DevTyp
        {
            get { if (devTyp == null) devTyp = new DevinfDevTyp(); return devTyp; }
            set { devTyp = value; }
        }



        private DevinfMan man;

        public DevinfMan Man
        {
            get { if (man == null) man = new DevinfMan(); return man; }
            set { man = value; }
        }

        private DevinfMod mod;

        public DevinfMod Mod
        {
            get { if (mod == null) mod = new DevinfMod(); return mod; }
            set { mod = value; }
        }

        private DevinfOEM oem;

        public DevinfOEM OEM
        {
            get { if (oem == null) oem = new DevinfOEM(); return oem; }
            set { oem = value; }
        }

        private DevinfFwV fwv;

        public DevinfFwV FwV
        {
            get { if (fwv == null) fwv = new DevinfFwV(); return fwv; }
            set { fwv = value; }
        }


    }

    /// <summary>
    /// Specifies the content type capabilities of the device.
    /// </summary>
    ///<ContentModel>CTCap ( CTType, VerCT, FieldLevel?, Property+)</ContentModel>
    public class DevinfCTCap : SyncMLComplexElement
    {
        private DevinfCTCap() : base(ElementNames.SyncMLDevInf, ElementNames.CTCap) { }

        public static DevinfCTCap Create(XElement xmlData)
        {
            DevinfCTCap r = new DevinfCTCap();
            r.CTType = SyncMLSimpleElementFactory.Create<DevinfCTType>(ElementReader.Element(xmlData,ElementNames.CTType));
            r.VerCT = SyncMLSimpleElementFactory.Create<DevinfVerCT>(ElementReader.Element(xmlData,ElementNames.VerCT));
            r.FieldLevel = SyncMLSimpleElementFactory.Create<DevinfFieldLevel>(ElementReader.Element(xmlData,ElementNames.FieldLevel));
            r.propertyCollection = new Collection<DevinfProperty>();
            IEnumerable<XElement> propertyNodes = ElementReader.Elements(xmlData,ElementNames.Property);
            foreach (XElement node in propertyNodes)
            {
                r.PropertyCollection.Add(DevinfProperty.Create(node));
            }
            return r;
        }

        public static DevinfCTCap Create()
        {
            DevinfCTCap r = new DevinfCTCap();
            r.CTType = new DevinfCTType();
            r.VerCT = new DevinfVerCT();
            r.propertyCollection = new Collection<DevinfProperty>();
            return r;
        }

        public override XElement Xml
        {
            get
            {
                Element.RemoveAll();
                Element.Add(CTType.Xml);
                Element.Add(VerCT.Xml);
                if (fieldLevel != null)
                    Element.Add(fieldLevel.Xml);
                foreach (DevinfProperty item in PropertyCollection)
                    Element.Add(item.Xml);

                return Element;
            }
        }
        private DevinfCTType ctType;

        public DevinfCTType CTType
        {
            get { return ctType; }
            set { ctType = value; ThrowExceptionIfNull(value, "CTCap/CTType"); }
        }

        private DevinfVerCT verCT;

        public DevinfVerCT VerCT
        {
            get { return verCT; }
            set { verCT = value; ThrowExceptionIfNull(value, "CTCap/VerCT"); }
        }

        private DevinfFieldLevel fieldLevel;

        public DevinfFieldLevel FieldLevel
        {
            get { if (fieldLevel == null) fieldLevel = new DevinfFieldLevel(); return fieldLevel; }
            set { fieldLevel = value; }
        }

        internal Collection<DevinfProperty> propertyCollection;

        public Collection<DevinfProperty> PropertyCollection
        {
            get { return propertyCollection; }
        }
    }

    /*      /// <summary>
        /// The 1.1 schema drove me crazy as it was really badly designed, not friendly to XPath.
        /// So I decided to support the 1.2 schema only.
        /// Specifies the content type capabilities of the device.
        /// </summary>
    ///<Restrictions> The content type capabilities of the device SHOULD be defined.</Restrictions>
    ///<ContentModel>CTCap ((CTType, (PropName, (ValEnum+ | (DataType, Size?))?,DisplayName?,
    ///(ParamName, (ValEnum+ | (DataType, Size?))?,DisplayName?)*)+)+)</ContentModel>
      public class DevinfCTCap : SyncMLComplexElement
        {
            private DevinfCTCap()
            {

            }

            public static DevinfCTCap Create()
            {
                DevinfCTCap r = new DevinfCTCap();
                r.CTTypeSetCollection = new Collection<CTTypeSet>();
                return r;
            }

            public static DevinfCTCap Create(XElement xmlData)
            {
                if (xmlData == null)
                    return null;
                DevinfCTCap r = new DevinfCTCap();
                r.CTTypeSetCollection = new Collection<CTTypeSet>();
                XPathNodeIterator ctTypeNodes = DevinfReader.Elements(xmlData,ElementNames.CTType);
                while (ctTypeNodes.MoveNext())
                {
                    XPathNavigator nav1 = ctTypeNodes.Current;
                    XPathNavigator nav2;

                    if (ctTypeNodes.MoveNext())
                        nav2= ctTypeNodes.Current;


                }



                return r;
            
            }

            private void XmlToCTTypeSetCollection(XElement xmlData)
            {

            }


            private Collection<CTTypeSet> ctTypeSetCollection;

            public Collection<CTTypeSet> CTTypeSetCollection
            {
                get { return ctTypeSetCollection; }
                set { ctTypeSetCollection = value; }
            }



            public override XElement XmlText
            {
                get
                {
                    return
              SyncMLElementCollectionToString<CTTypeSet>.Convert(CTTypeSetCollection);
                }
            }

            /// <summary>
            /// CTType, (PropName, (ValEnum+ | (DataType, Size?))?,DisplayName?,(ParamName, (ValEnum+ | (DataType, Size?))?,DisplayName?)*)+
            /// </summary>
            public class CTTypeSet : SyncMLComplexElement
            {
                private CTTypeSet()
                {

                }

                public static CTTypeSet Create()
                {
                    CTTypeSet r = new CTTypeSet();
                    r.CTType = new DevinfCTType();
                    r.PropNameSetCollection = new Collection<PropNameSet>();
                    return r;
                }

                public static CTTypeSet Create(XPathNavigator ctType1, XPathNavigator ctType2)
                {
                    if (ctType1 == null)
                        return null;

                    CTTypeSet r = new CTTypeSet();
                    r.CTType = SyncMLSimpleElementFactory.Create<DevinfCTType>(ctType1);

                    Collection<XPathNavigator> propNameNodes = new Collection<XPathNavigator>();

                    while (ctType1.MoveToNext(XPathNodeType.Element)&&(ctType1.ComparePosition(ctType2)!= XmlNodeOrder.Same))
                    {
                        if (ctType1.LocalName == ElementNames.PropName)
                            propNameNodes.Add(ctType1);
                    }

                    for (int i = 0; i < propNameNodes.Count; i++)
                    {
                        PropNameSet propNameSet = PropNameSet.Create();
                    }

                    return r;

                }

                private DevinfCTType ctType;

                public DevinfCTType CTType
                {
                    get { return ctType; }
                    set { ctType = value; }
                }

                private Collection<PropNameSet> propNameSetCollection;

                public Collection<PropNameSet> PropNameSetCollection
                {
                    get { return propNameSetCollection; }
                    set { propNameSetCollection = value; }
                }

                public override XElement XmlText
                {
                    get { throw new Exception("The method or operation is not implemented."); }
                }

            }

            /// <summary>
            /// PropName, (ValEnum+ | (DataType, Size?))?,DisplayName?,(ParamName, (ValEnum+ | (DataType, Size?))?,DisplayName?)*
            /// </summary>
            public class PropNameSet : SyncMLComplexElement
            {
                private DevinfPropName propName;

                public DevinfPropName PropName
                {
                    get { return propName; }
                    set { propName = value; }
                }

                private ValEnumSet valEnumSet;

                public ValEnumSet _ValEnumSet
                {
                    get { if (valEnumSet == null) _ValEnumSet = ValEnumSet.Create(); return valEnumSet; }
                    set { valEnumSet = value;  }
                }

                private DevinfDisplayName displayName;

                public DevinfDisplayName DisplayName
                {
                    get { if (displayName == null) displayName = new DevinfDisplayName(); return displayName; }
                    set { displayName = value; }
                }

                private Collection<ParamNameSet> paramNameSetCollection;

                public Collection<ParamNameSet> ParamNameSetCollection
                {
                    get { return paramNameSetCollection; }
                    set { paramNameSetCollection = value; }
                }



                public override XElement XmlText
                {
                    get
                    {
                        return
                       PropName.XmlText +
                   _ValEnumSet.XmlText;
                    }
                }

                private PropNameSet()
                {

                }

                public static PropNameSet Create()
                {
                    PropNameSet r = new PropNameSet();
                    r.PropName = new DevinfPropName();
                    r._ValEnumSet = ValEnumSet.Create();
                    return r;
                }

                /// <summary>
                /// PropName, (ValEnum+ | (DataType, Size?))?,DisplayName?,(ParamName, (ValEnum+ | (DataType, Size?))?,DisplayName?)*
                /// </summary>
                /// <param name="nav1"></param>
                /// <param name="nav2"></param>
                /// <returns></returns>
                public static PropNameSet Create(XPathNavigator propName1, XPathNavigator propName2)
                {
                    if (propName1 == null)
                        return null;

                    PropNameSet r = new PropNameSet();
                    r.PropName = SyncMLSimpleElementFactory.Create<DevinfPropName>(propName1);

                    Collection<ValEnumSet> valEnumNodes = new Collection<ValEnumSet>();




                    return null;
                }

            }

            /// <summary>
            /// ValEnum+ | (DataType, Size?)
            /// </summary>
            public class ValEnumSet : SyncMLComplexElement
            {
                private ValEnumSet()
                {

                }

                public static ValEnumSet Create()
                {
                    ValEnumSet r = new ValEnumSet();
                    r.valEnumCollection = new Collection<DevinfValEnum>();
                    return r;
                }

                public static ValEnumSet Create(XPathNavigator xmlData)
                {
                    if (xmlData == null)
                        return null;

                    ValEnumSet r = new ValEnumSet();
                    XPathNodeIterator nodes = xmlData.Select(ElementNames.ValEnum);
                    if (nodes.Count == 0)
                    {
                        r.Set.DataType = SyncMLSimpleElementFactory.Create<DevinfDataType>(new SyncMLNavigator( DevinfReader.Element(xmlData,ElementNames.DataType)));
                        r.Set.Size = SyncMLSimpleElementFactory.Create<DevinfSize>(new SyncMLNavigator( DevinfReader.Element(xmlData,ElementNames.Size)));
                    }
                    else
                    {
                        r.valEnumCollection = new Collection<DevinfValEnum>();
                        while (nodes.MoveNext())
                        {
                            r.valEnumCollection.Add(SyncMLSimpleElementFactory.Create<DevinfValEnum>(new SyncMLNavigator( nodes.Current)));
                        }
                    }
                
                    return r;

                }

                public override XElement XmlText
                {
                    get {
                        if ((valEnumCollection == null) || (valEnumCollection.Count > 0))
                            return SyncMLElementCollectionToString<DevinfValEnum>.Convert(valEnumCollection);
                        else
                            return Set.DataType.XmlText + (Set.size != null ? Set.size.XmlText : String.Empty);
                    }
                }

                private Collection<DevinfValEnum> valEnumCollection;

                public Collection<DevinfValEnum> ValEnumCollection
                {
                    get { return valEnumCollection; }
                    set { valEnumCollection = value; }
                }

                internal DataTypeSet set;

                public DataTypeSet Set
                {
                    get { return set; }
                    set { set = value; ThrowExceptionIfNull(value, "DataTypeSet"); }
                }


            }

            /// <summary>
            /// (DataType, Size?)
            /// </summary>
            public class DataTypeSet : SyncMLComplexElement
            {
                private DataTypeSet()
                {

                }

                public static DataTypeSet Create()
                {
                    DataTypeSet r = new DataTypeSet();
                    r.dataType = new DevinfDataType();
                    return r;
                }

                public static DataTypeSet Create(XElement xmlData)
                {
                    if (xmlData == null)
                        return null;

                    DataTypeSet r = new DataTypeSet();
                    r.DataType = SyncMLSimpleElementFactory.Create<DevinfDataType>(DevinfReader.Element(xmlData,ElementNames.DataType));
                    r.Size = SyncMLSimpleElementFactory.Create<DevinfSize>(DevinfReader.Element(xmlData,ElementNames.Size));
                    return r;

                }

                private DevinfDataType dataType;

                public DevinfDataType DataType
                {
                    get { return dataType; }
                    set { dataType = value; ThrowExceptionIfNull(value, "DAtaTypeSet/DataType"); }
                }

                internal DevinfSize size;

                public DevinfSize Size
                {
                    get { if (size == null) size = new DevinfSize(); return size; }
                    set { size = value; }
                }


                public override XElement XmlText
                {
                    get { return DataType.XmlText + (size != null ? size.XmlText : String.Empty); }
                }


            }

            /// <summary>
            /// ParamName, (ValEnum+ | (DataType, Size?))?,DisplayName?
            /// </summary>
            public class ParamNameSet : SyncMLComplexElement
            {
                public static ParamNameSet Create(XElement xmlData)
                {
                    if (xmlData == null)
                        return null;

                    ParamNameSet r = new ParamNameSet();
                    r.ParamName = SyncMLSimpleElementFactory.Create<DevinfParamName>(DevinfReader.Element(xmlData,ElementNames.ParamName));
                    r.DisplayName = SyncMLSimpleElementFactory.Create<DevinfDisplayName>(DevinfReader.Element(xmlData,ElementNames.DisplayName));
                    return r;

                }

                private ParamNameSet()
                {

                }


                private DevinfParamName paramName;

                public DevinfParamName ParamName
                {
                    get { return paramName; }
                    set { paramName = value; ThrowExceptionIfNull(value, "DataTypeSet/ParamName"); }
                }

                private DevinfDisplayName displayName;

                public DevinfDisplayName DisplayName
                {
                    get { if (displayName == null) displayName = new DevinfDisplayName(); return displayName; }
                    set { displayName = value; }
                }

	
                private ValEnumSet valEnumSet;
                /// <summary>
                /// ValEnum+ | (DataType, Size?)
                /// </summary>
                public ValEnumSet _ValEnumSet
                {
                    get { if (valEnumSet == null) valEnumSet = ValEnumSet.Create(); return valEnumSet; }
                    set { valEnumSet = value;  }
                }


                public override XElement XmlText
                {
                    get
                    {
                        return
                       ParamName.XmlText +
                       (valEnumSet != null ? _ValEnumSet.XmlText : String.Empty) +
                       (displayName != null ? displayName.XmlText : String.Empty);
                   
                    }
                }

                public static ParamNameSet Create()
                {
                    ParamNameSet r = new ParamNameSet();
                    r.ParamName = new DevinfParamName();
                    r._ValEnumSet = ValEnumSet.Create();
                    return r;
                }

                public static ParamNameSet Create(XPathNavigator xmlData)
                {
                    if (xmlData == null)
                        return null;

                    //   PropNameSet r = new PropNameSet();
                    //   r.PropName = SyncMLSimpleElementFactory.Create<DevinfPropName>(xmlData.Element
                    return null;
                }

            }

        }
    */

}
