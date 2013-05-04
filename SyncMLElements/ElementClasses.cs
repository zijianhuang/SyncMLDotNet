using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Xml.Linq;


/*This file containt classes mapped to SyncML elements.
All Common Use Elements are implemented.
 All Message Container Elements are implemented.
 All Data Description Elements are implemented.
 All Protocol Management Elements (Status) are implemented
 Most Protocol Command Elements are implemented except the following:
 --Copy, Exec, Get, Search--
 --Field, Filter, FilterType, Record, SourceParent, Target+, TargetParent, Correlator, Alert+,
 Atomic+, Move, Put, Sequence+, Sync+ --
 
Parts of the documents are referred from SyncML Representation Protocol (http://www.syncml.org/docs/syncml_represent_v11_20020215.pdf)
as the most classes are directly mapped to SyncML elements.*/

namespace Fonlow.SyncML.Elements
{

    /// <summary>
    /// Specifies a SyncML message-unique command identifier.
    /// </summary>
    ///<ParentElements>
    ///Add, Alert, Atomic, Copy, Delete, Exec, Get, Map, Put, Replace,
    ///Results, Search, Sequence, Status, Sync
    ///</ParentElements>
    ///<ContentModel>(#PCDATA)</ContentModel>
    public class SyncMLCmdID : SyncMLSimpleElement
    {
        public SyncMLCmdID():base(ElementNames.CmdID)
        {

        }
    }

    /// <summary>
    /// Specifies the SyncML command to add data to a data collection.
    /// </summary>
    ///<ParentElements> Atomic, Sequence, Sync, SyncBody.</ParentElements>
    ///<ContentModel>(CmdID, NoResp?, Cred?, Meta?, Item+)</ContentModel>
    public class SyncMLAdd : SyncMLUpdateBase
    {

        public static SyncMLAdd Create(XElement xmlData)
        {
            if (xmlData == null)
                return null;
            
            SyncMLAdd r = new SyncMLAdd();
            XNamespace ns = xmlData.Name.Namespace;
            r.CmdID = SyncMLSimpleElementFactory.Create<SyncMLCmdID>(xmlData.Element(ns+ElementNames.CmdID));
            r.NoResp = SyncMLSimpleElementFactory.Create<SyncMLNoResp>(xmlData.Element(ns+ElementNames.NoResp));
            r.Cred = SyncMLCred.Create(xmlData.Element(ns+ElementNames.Cred));
            r.Meta = SyncMLSimpleElementFactory.Create<SyncMLMeta>(xmlData.Element(ns+ElementNames.Meta));

            IEnumerable<XElement> items = xmlData.Elements(ns+ElementNames.Item);
            if (items!= null)
            {
                r.itemCollection = new Collection<SyncMLItem>();
                foreach (XElement element in items)
                {
                    r.ItemCollection.Add(SyncMLItem.Create(element));
                }
            }

            return r;
        }

        public static SyncMLAdd Create()
        {
            SyncMLAdd r = new SyncMLAdd();
            return r;
        }

        private SyncMLAdd():base(ElementNames.Add)
        {

        }

        public override XElement Xml
        {
            get
            {
                Element.RemoveAll();
                Element.Add(CmdID.Xml);
                if (noResp != null)
                    Element.Add(NoResp.Xml);
                if (cred != null)
                    Element.Add(Cred.Xml);
                if (meta != null)
                    Element.Add(Meta.Xml);
                foreach (SyncMLItem item in ItemCollection)
                {
                    Element.Add(item.Xml);
                }
                return Element;
            }
        }

    }

    /// <summary>
    /// Specifies the SyncML command to replace data.
    /// </summary>
    ///<ParentElements> Atomic, Sequence, Sync, SyncBody.</ParentElements>
    ///<ContentModel>(CmdID, NoResp?, Cred?, Meta?, Item+)</ContentModel>
    public class SyncMLReplace : SyncMLUpdateBase
    {

        public static SyncMLReplace Create(XElement xmlData)
        {
            if (xmlData == null)
                return null;

            SyncMLReplace r = new SyncMLReplace();
            XNamespace ns = xmlData.Name.Namespace;
            r.CmdID = SyncMLSimpleElementFactory.Create<SyncMLCmdID>(xmlData.Element(ns + ElementNames.CmdID));
            r.NoResp = SyncMLSimpleElementFactory.Create<SyncMLNoResp>(xmlData.Element(ns+ElementNames.NoResp));
            r.Cred = SyncMLCred.Create(xmlData.Element(ns+ElementNames.Cred));
            r.Meta = SyncMLSimpleElementFactory.Create<SyncMLMeta>(xmlData.Element(ns+ElementNames.Meta));

            IEnumerable<XElement> items = xmlData.Elements(ns+ElementNames.Item);
            if (items  != null)
            {
                r.itemCollection = new Collection<SyncMLItem>();
                foreach (XElement element in items)
                {
                    r.ItemCollection.Add(SyncMLItem.Create(element));
                }
            }

            return r;
        }

        public static SyncMLReplace Create()
        {
            SyncMLReplace r = new SyncMLReplace();
            return r;
        }

        private SyncMLReplace():base(ElementNames.Replace)
        {

        }

        public override XElement Xml
        {
            get
            {
                Element.RemoveAll();
                Element.Add(CmdID.Xml);
                if (noResp != null)
                    Element.Add(NoResp.Xml);
                if (cred != null)
                    Element.Add(Cred.Xml);
                if (meta != null)
                    Element.Add(Meta.Xml);
                foreach (SyncMLItem item in ItemCollection)
                {
                    Element.Add(item.Xml);
                }
                return Element;
            }
        }

    }

    /// <summary>
    /// Specifies the SyncML command to delete data from a data collection.
    /// </summary>
    ///<ParentElements> Atomic, Sequence, Sync, SyncBody</ParentElements>
    ///<ContentModel>(CmdID, NoResp?, Archive?, SftDel?, Cred?, Meta?, Item+)</ContentModel>
    public class SyncMLDelete : SyncMLUpdateBase
    {
        public static SyncMLDelete Create(XElement xmlData)
        {
            if (xmlData == null)
                return null;

            SyncMLDelete r = new SyncMLDelete();
            XNamespace ns = xmlData.Name.Namespace;
            r.CmdID = SyncMLSimpleElementFactory.Create<SyncMLCmdID>(xmlData.Element(ns + ElementNames.CmdID));
            r.NoResp = SyncMLSimpleElementFactory.Create<SyncMLNoResp>(xmlData.Element(ns+ElementNames.NoResp));
            r.Archive = SyncMLSimpleElementFactory.Create<SyncMLArchive>(xmlData.Element(ns+ElementNames.Archive));
            r.SftDel = SyncMLSimpleElementFactory.Create<SyncMLSftDel>(xmlData.Element(ns+ElementNames.SftDel));
            r.Cred = SyncMLCred.Create(xmlData.Element(ns+ElementNames.Cred));
            r.Meta = SyncMLSimpleElementFactory.Create<SyncMLMeta>(xmlData.Element(ns+ElementNames.Meta));

            IEnumerable<XElement> items = xmlData.Elements(ns+ElementNames.Item);
            if (items!= null)
            {
                r.itemCollection = new Collection<SyncMLItem>();
                foreach (XElement item in items)
                {
                    r.ItemCollection.Add(SyncMLItem.Create(item));

                }
            }

            return r;
        }

        public static SyncMLDelete Create()
        {
            SyncMLDelete r = new SyncMLDelete();
            return r;
        }

        private SyncMLDelete():base(ElementNames.Delete)
        {

        }

        private SyncMLArchive archive;

        public SyncMLArchive Archive
        {
            get { return archive; }
            set { archive = value; }
        }

        private SyncMLSftDel sftDel;

        public SyncMLSftDel SftDel
        {
            get { return sftDel; }
            set { sftDel = value; }
        }

        public override XElement Xml
        {
            get
            {
                Element.RemoveAll();
                Element.Add(CmdID.Xml);
                if (noResp != null)
                    Element.Add(NoResp.Xml);
                if (archive != null)
                    Element.Add(Archive.Xml);
                if (sftDel != null)
                    Element.Add(SftDel.Xml);
                if (cred != null)
                    Element.Add(Cred.Xml);
                if (meta != null)
                    Element.Add(Meta.Xml);

                foreach (SyncMLItem item in ItemCollection)
                {
                    Element.Add(item.Xml);
                }
                return Element;
            }
        }


    }

    /// <summary>
    /// Base class for SyncMLAdd and SyncMLReplace as well as SyncMLDelete
    /// </summary>
    public abstract class SyncMLUpdateBase : SyncMLCommand
    {
        protected SyncMLUpdateBase(XName elementName)
            : base(elementName)
        { }

        protected SyncMLUpdateBase(XNamespace sp, string localName):base(sp, localName)
        { }

        internal SyncMLNoResp noResp;

        public SyncMLNoResp NoResp
        {
            get { if (noResp == null) noResp = new SyncMLNoResp(); return noResp; }
            set { noResp = value; }
        }

        internal SyncMLCred cred;

        public SyncMLCred Cred
        {
            get { if (cred == null) cred = SyncMLCred.Create(); return cred; }
            set { cred = value; }
        }

        internal SyncMLMeta meta;

        public SyncMLMeta Meta
        {
            get { if (meta == null) meta = new SyncMLMeta(); return meta; }
            set { meta = value; }
        }

        internal Collection<SyncMLItem> itemCollection = new Collection<SyncMLItem>();

        public Collection<SyncMLItem> ItemCollection
        {
            get { return itemCollection; }
        }


    }

    public class SyncMLNoResp : SyncMLSimpleElement
    {
        public SyncMLNoResp():base(ElementNames.NoResp)
        {

        }
    }

    public class SyncMLNoResults : SyncMLSimpleElement
    {
        public SyncMLNoResults()
            : base(ElementNames.NoResults)
        {

        }
    }

    /// <summary>
    /// Specifies an authentication credential for the originator.
    /// </summary>
    ///<ParentElements> Add, Alert, Copy, Delete, Exec, Get, Put, Map, Replace, Search,Status, Sync, SyncHdr</ParentElements>
    ///<ContentModel>(Meta?, Data)</ContentModel>
    public class SyncMLCred : SyncMLComplexElement
    {
        public static SyncMLCred Create(XElement xmlData)
        {
            if (xmlData == null)
                return null;

            SyncMLCred r = new SyncMLCred();
            XNamespace ns = xmlData.Name.Namespace;
            r.Meta = SyncMLSimpleElementFactory.Create<SyncMLMeta>(xmlData.Element(ns + ElementNames.Meta));
            r.Data =SyncMLSimpleElementFactory.Create<SyncMLData>(xmlData.Element(ns+ElementNames.Data));
            return r;
        }

        public static SyncMLCred Create()
        {
            SyncMLCred r = new SyncMLCred();
            r.Data = new SyncMLData();
            return r;
        }



        private SyncMLCred():base(ElementNames.Cred)
        {

        }


        public override XElement Xml
        {
            get
            {
                Element.RemoveAll();
                if (meta != null)
                    Element.Add(Meta.Xml);
                Element.Add(Data.Xml);

                return Element;
            }
        }


        SyncMLMeta meta;
        public SyncMLMeta Meta
        {
            get
            {
                if (meta == null) meta = new SyncMLMeta();
                return meta;
            }
            set
            {
                meta = value;
            }
        }

        SyncMLData data;
        public SyncMLData Data
        {
            get
            {
                return data;
            }
            set
            {
                data = value; ThrowExceptionIfNull(value, "Cred/Data");
            }
        }

    }


    /// <summary>
    /// Specifies a container for item data.
    /// </summary>
    ///<ParentElements> Add, Alert, Copy, Delete, Exec, Get, Put, Replace, Results,Status</ParentElements>
    ///<ContentModel>(Target?, Source?, Meta?, Data?)</ContentModel>
    public class SyncMLItem : SyncMLComplexElement
    {
        public static SyncMLItem Create(XElement xmlData)
        {
            if (xmlData == null)
                return null;

            SyncMLItem r = new SyncMLItem();
            XNamespace ns = xmlData.Name.Namespace;
            r.Target = SyncMLTarget.Create(xmlData.Element(ns + ElementNames.Target));
            r.Source = SyncMLSource.Create(xmlData.Element(ns+ElementNames.Source));
            r.Meta = SyncMLSimpleElementFactory.Create<SyncMLMeta>(xmlData.Element(ns+ElementNames.Meta));
            r.Data = SyncMLSimpleElementFactory.Create<SyncMLData>(xmlData.Element(ns+ElementNames.Data));

            return r;
        }

        public static SyncMLItem Create()
        {
            SyncMLItem r = new SyncMLItem();
            return r;
        }

        private SyncMLItem():base(ElementNames.Item)
        {

        }

        public override XElement Xml
        {
            get
            {
                Element.RemoveAll();
                if (target != null)
                    Element.Add(Target.Xml);
                if (source != null)
                    Element.Add(Source.Xml);
                if (meta != null)
                    Element.Add(Meta.Xml);
                if (data != null)
                    Element.Add(Data.Xml);
                return Element;
            }
        }

        SyncMLTarget target;

        public SyncMLTarget Target
        {
            get { if (target == null) target = SyncMLTarget.Create(); return target; }
            set { target = value; }
        }
        SyncMLSource source;

        public SyncMLSource Source
        {
            get { if (source == null) source = SyncMLSource.Create(); return source; }
            set { source = value; }
        }
        SyncMLMeta meta;

        public SyncMLMeta Meta
        {
            get { if (meta == null) meta = new SyncMLMeta(); return meta; }
            set { meta = value; }
        }
        SyncMLData data;

        public SyncMLData Data
        {
            get { if (data == null) data = new SyncMLData(); return data; }
            set { data = value; }
        }
    }

    /// <summary>
    /// Specifies source routing or mapping information.
    /// </summary>
    /// <ParentElements>Parent Elements: Item, Map, MapItem, Search, Sync, SyncHdr</ParentElements>
    /// <ContentModel>(LocURI, LocName?)</ContentModel>
    public class SyncMLSource : SyncMLComplexElement
    {
        private SyncMLSource()
            : base(ElementNames.Source)
        { }

        public static SyncMLSource Create(XElement xmlData)
        {
            if (xmlData == null)
                return null;

            SyncMLSource r = new SyncMLSource();
            XNamespace ns = xmlData.Name.Namespace;
            r.LocURI = SyncMLSimpleElementFactory.Create<SyncMLLocURI>(xmlData.Element(ns + ElementNames.LocURI));
            r.LocName = SyncMLSimpleElementFactory.Create<SyncMLLocName>(xmlData.Element(ns+ElementNames.LocName));
            return r;
        }

        public static SyncMLSource Create()
        {
            SyncMLSource r = new SyncMLSource();
            r.LocURI = new SyncMLLocURI();
            return r;
        }

        public override XElement Xml
        {
            get
            {
                Element.RemoveAll();
                Element.Add(LocURI.Xml);
                if (locName != null)
                    Element.Add(LocName.Xml);

                return Element;
            }
        }

        SyncMLLocURI locURI;

        public SyncMLLocURI LocURI
        {
            get { return locURI; }
            set { locURI = value; ThrowExceptionIfNull(value, "Target/LocURI"); }
        }


        SyncMLLocName locName;

        public SyncMLLocName LocName
        {
            get { if (locName == null) locName = new SyncMLLocName(); return locName; }
            set { locName = value; }
        }
    }

    /// <summary>
    /// Usage: Specifies target routing or mapping information.
    /// </summary>
    /// <ParentElements>Parent Elements: Item, Map, MapItem, Search, Sync, SyncHdr</ParentElements>
    /// <ContentModel>(LocURI, LocName?, Filter?)</ContentModel>
    public class SyncMLTarget : SyncMLComplexElement
    {
        private SyncMLTarget()
            : base(ElementNames.Target)
        { }

        public static SyncMLTarget Create(XElement xmlData)
        {
            if (xmlData == null)
                return null;

            SyncMLTarget r = new SyncMLTarget();
            XNamespace ns = xmlData.Name.Namespace;
            r.LocURI = SyncMLSimpleElementFactory.Create<SyncMLLocURI>(xmlData.Element(ns + ElementNames.LocURI));
            r.LocName = SyncMLSimpleElementFactory.Create<SyncMLLocName>(xmlData.Element(ns+ElementNames.LocName));
            return r;
        }

        public static SyncMLTarget Create()
        {
            SyncMLTarget r = new SyncMLTarget();
            r.LocURI = new SyncMLLocURI();
            return r;
        }

        public override XElement Xml
        {
            get
            {
                Element.RemoveAll();
                Element.Add(LocURI.Xml);
                if (locName != null)
                    Element.Add(LocName.Xml);
                return Element;
            }
        }

        SyncMLLocURI locURI;

        public SyncMLLocURI LocURI
        {
            get { return locURI; }
            set { locURI = value; ThrowExceptionIfNull(value, "Target/LocURI"); }
        }

        SyncMLLocName locName;

        public SyncMLLocName LocName
        {
            get { if (locName == null) locName = new SyncMLLocName(); return locName; }
            set { locName = value; }
        }
    }

    /// <summary>
    /// Specifies the target or source specific address.
    /// </summary>
    /// <ParentElements>Target, Source</ParentElements>
    public class SyncMLLocURI : SyncMLSimpleElement
    {
        public SyncMLLocURI()
            : base(ElementNames.LocURI)
        {

        }
    }

    /// <summary>
    ///Specifies the display name for the target or source address.
    /// </summary>
    /// <ParentElements>Target, Source</ParentElements>
    public class SyncMLLocName : SyncMLSimpleElement
    {
        public SyncMLLocName()
            : base(ElementNames.LocName)
        {

        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <ParentElements> Map</ParentElements>
    ///<ContentModel>(Target, Source)</ContentModel>
    public class SyncMLMapItem : SyncMLComplexElement
    {
        public static SyncMLMapItem Create(XElement xmlData)
        {
            if (xmlData == null)
                return null;

            SyncMLMapItem r = new SyncMLMapItem();
            XNamespace ns = xmlData.Name.Namespace;
            r.Target = SyncMLTarget.Create(xmlData.Element(ns + ElementNames.Target));
            r.Source = SyncMLSource.Create(xmlData.Element(ns+ElementNames.Source));
            return r;
        }

        public static SyncMLMapItem Create()
        {
            SyncMLMapItem r = new SyncMLMapItem();
            r.Target = SyncMLTarget.Create();
            r.Source = SyncMLSource.Create();
            return r;
        }

        private SyncMLMapItem():base(ElementNames.MapItem)
        {

        }

        public override XElement Xml
        {
            get
            {
                Element.RemoveAll();
                Element.Add(Target.Xml);
                Element.Add(Source.Xml);
                return Element;
            }
        }
        SyncMLTarget target;

        public SyncMLTarget Target
        {
            get { return target; }
            set { target = value; }
        }
        SyncMLSource source;

        public SyncMLSource Source
        {
            get { return source; }
            set { source = value; }
        }
    }

    /// <summary>
    /// Specifies the SyncML command used to update identifier maps.
    /// </summary>
    /// Usage: 
    ///<ParentElements> Atomic, Sequence, SyncBody</ParentElements>
    ///<ContentModel>(CmdID, Target, Source, Cred?, Meta?, MapItem+)</ContentModel>
    public class SyncMLMap : SyncMLCommand
    {
        public static SyncMLMap Create(XElement xmlData)
        {
            if (xmlData == null)
                return null;

            SyncMLMap r = new SyncMLMap();
            XNamespace ns = xmlData.Name.Namespace;
            r.CmdID = SyncMLSimpleElementFactory.Create<SyncMLCmdID>(xmlData.Element(ns + ElementNames.CmdID));
            r.Target = SyncMLTarget.Create(xmlData.Element(ns+ElementNames.Target));
            r.Source = SyncMLSource.Create(xmlData.Element(ns+ElementNames.Source));
            r.Cred = SyncMLCred.Create(xmlData.Element(ns+ElementNames.Cred));
            r.Meta = SyncMLSimpleElementFactory.Create<SyncMLMeta>(xmlData.Element(ns+ElementNames.Meta));

            IEnumerable<XElement> items = xmlData.Elements(ns+ElementNames.MapItem);
            if (items!=null)
            {
                r.mapItemCollection = new Collection<SyncMLMapItem>();
                foreach (XElement item in items)
                    r.MapItemCollection.Add(SyncMLMapItem.Create(item));
            }

            return r;

        }

        public static SyncMLMap Create()
        {
            SyncMLMap r = new SyncMLMap();
            r.Target = SyncMLTarget.Create();
            r.Source = SyncMLSource.Create();
            r.mapItemCollection = new Collection<SyncMLMapItem>();
            return r;
        }

        private SyncMLMap():base(ElementNames.Map)
        {

        }

        public override XElement Xml
        {
            get
            {
                Element.RemoveAll();
                Element.Add(CmdID.Xml, Target.Xml, Source.Xml);
                if (cred != null)
                    Element.Add(Cred.Xml);
                if (meta != null)
                    Element.Add(Meta.Xml);

                foreach (SyncMLMapItem item in MapItemCollection)
                {
                    Element.Add(item.Xml);
                }

                return Element;

            }
        }

        SyncMLTarget target;

        public SyncMLTarget Target
        {
            get { return target; }
            set { target = value; ThrowExceptionIfNull(value, "Map/Target"); }
        }
        SyncMLSource source;

        public SyncMLSource Source
        {
            get { return source; }
            set { source = value; ThrowExceptionIfNull(value, "Map/Source"); }
        }
        SyncMLCred cred;

        public SyncMLCred Cred
        {
            get { if (cred == null) cred = SyncMLCred.Create(); return cred; }
            set { cred = value; }
        }

        SyncMLMeta meta;

        public SyncMLMeta Meta
        {
            get { if (meta == null) meta = new SyncMLMeta(); return meta; }
            set { meta = value; }
        }
        Collection<SyncMLMapItem> mapItemCollection;

        public Collection<SyncMLMapItem> MapItemCollection
        {
            get { return mapItemCollection; }
        }
    }

    /// <summary>
    /// Specifies meta-information about the parent element type.
    /// </summary>
    ///<ParentElements> Add, Atomic, Chal, Copy, Cred, Delete, Get, Item, Map, Put,Replace, Results, Search, Sequence, Sync</ParentElements>
    ///<ContentModel>(#PCDATA)</ContentModel>
    public class SyncMLMeta : SyncMLSimpleElement
    {
        public SyncMLMeta():base(ElementNames.Meta)
        {

        }

    }

    public class SyncMLCorrelator : SyncMLSimpleElement
    {
        public SyncMLCorrelator()
            : base(ElementNames.Correlator)
        {

        }
    }
    /// <summary>
    /// Specifies discrete SyncML data.
    /// </summary>
    ///<ParentElements> Alert, Cred, Item, Status, Search</ParentElements>
    ///<ContentModel>(#PCDATA)</ContentModel>
    public class SyncMLData : SyncMLSimpleElement
    {
        public SyncMLData():base(ElementNames.Data)
        {

        }
    }

    /// <summary>
    /// Specifies an authentication challenge. The receiver of the challenge specifies
    /// </summary>
    ///<ParentElements>Status</ParentElements> 
    ///<ContentModel>(Meta)</ContentModel>
    public class SyncMLChal : SyncMLComplexElement
    {
        public static SyncMLChal Create(XElement xmlData)
        {
            if (xmlData == null)
                return null;

            SyncMLChal r = new SyncMLChal();
            XNamespace ns = xmlData.Name.Namespace;
            r.Meta = SyncMLSimpleElementFactory.Create<SyncMLMeta>(xmlData.Element(ns + ElementNames.Meta));
            return r;
        }

        public static SyncMLChal Create()
        {
            SyncMLChal r = new SyncMLChal();
            r.Meta = new SyncMLMeta();
            return r;
        }

        private SyncMLChal():base(ElementNames.Chal)
        {

        }

        public override XElement Xml
        {
            get
            {
                Element.RemoveAll();
                if (meta != null)
                     Element.Add(meta.Xml);
                return Element; }
        }

        private SyncMLMeta meta;

        public SyncMLMeta Meta
        {
            get { if (meta == null) meta = new SyncMLMeta(); return meta; }
            set { meta = value; ThrowExceptionIfNull(value, "Chal/Meta"); }
        }


    }

    /// <summary>
    /// Specifies the name of the SyncML command referenced by a Status element type.
    /// </summary>
    ///<ParentElements> Status</ParentElements>
    ///<Restrictions> The value MUST be one of Add, Alert, Atomic, Copy, Delete, Exec,Get, Map, Put, 
    ///Replace, Results, Search, Sequence, Status, Sync.</Restrictions>
    ///<ContentModel>(#PCDATA)</ContentModel>
    public class SyncMLCmd : SyncMLSimpleElement
    {
        public SyncMLCmd()
            : base(ElementNames.Cmd)
        {

        }
    }

    public class SyncMLFinal : SyncMLSimpleElement
    {
        public SyncMLFinal()
            : base(ElementNames.Final)
        {

        }
    }

    public class SyncMLLang : SyncMLSimpleElement
    {
        public SyncMLLang()
            : base(ElementNames.Lang)
        {

        }
    }

    public class SyncMLMoreData : SyncMLSimpleElement
    {
        public SyncMLMoreData()
            : base(ElementNames.MoreData)
        {

        }
    }

    public class SyncMLMsgID : SyncMLSimpleElement
    {
        public SyncMLMsgID()
            : base(ElementNames.MsgID)
        {

        }

    }

    public class SyncMLMsgRef : SyncMLSimpleElement
    {
        public SyncMLMsgRef()
            : base(ElementNames.MsgRef)
        {

        }
    }

    /// <summary>
    /// Specifies the CmdID referenced by a Status element type.
    /// </summary>
    ///<ParentElements> Results, Status</ParentElements>
    public class SyncMLCmdRef : SyncMLSimpleElement
    {
        public SyncMLCmdRef()
            : base(ElementNames.CmdRef)
        {

        }
    }

    public class SyncMLNumberOfChanges : SyncMLSimpleElement
    {
        public SyncMLNumberOfChanges()
            : base(ElementNames.NumberOfChanges)
        {

        }
    }

    public class SyncMLRespURI : SyncMLSimpleElement
    {
        public SyncMLRespURI()
            : base(ElementNames.RespURI)
        {

        }
    }

    public class SyncMLSessionID : SyncMLSimpleElement
    {
        public SyncMLSessionID()
            : base(ElementNames.SessionID)
        {

        }
    }

    public class SyncMLSftDel : SyncMLSimpleElement
    {
        public SyncMLSftDel()
            : base(ElementNames.SftDel)
        {

        }
    }

    public class SyncMLArchive : SyncMLSimpleElement
    {
        public SyncMLArchive()
            : base(ElementNames.Archive)
        {

        }
    }

    public class SyncMLSourceRef : SyncMLSimpleElement
    {
        public SyncMLSourceRef()
            : base(ElementNames.SourceRef)
        {

        }
    }

    public class SyncMLTargetRef : SyncMLSimpleElement
    {
        public SyncMLTargetRef()
            : base(ElementNames.TargetRef)
        {

        }
    }


    public class SyncMLVerDTD : SyncMLSimpleElement
    {
        public SyncMLVerDTD()
            : base(ElementNames.VerDTD)
        {

        }
    }

    public class SyncMLVerProto : SyncMLSimpleElement
    {
        public SyncMLVerProto()
            : base(ElementNames.VerProto)
        {

        }
    }

    /// <summary>
    /// Specifies the SyncML command to transfer data items to a recipient network device or database.
    ///<ParentElements> SyncBody</ParentElements>
    ///<ContentModel>(CmdID, NoResp?, Lang?, Cred?, Meta?, Item+)</ContentModel>
    /// </summary>
    public class SyncMLPut : SyncMLCommand
    {
        private SyncMLPut():base(ElementNames.Put)
        {
        }

        public static SyncMLPut Create(XElement xmlData)
        {
            if (xmlData == null)
                return null;

            SyncMLPut r = new SyncMLPut();
            XNamespace ns = xmlData.Name.Namespace;
            r.CmdID = SyncMLSimpleElementFactory.Create<SyncMLCmdID>(xmlData.Element(ns + ElementNames.CmdID));
            r.NoResp = SyncMLSimpleElementFactory.Create<SyncMLNoResp>(xmlData.Element(ns+ElementNames.NoResp));
            r.Lang = SyncMLSimpleElementFactory.Create<SyncMLLang>(xmlData.Element(ns+ElementNames.Lang));
            r.Cred = SyncMLCred.Create(xmlData.Element(ns+ElementNames.Cred));
            r.Meta = SyncMLSimpleElementFactory.Create<SyncMLMeta>(xmlData.Element(ns+ElementNames.Meta));

            IEnumerable<XElement> items = xmlData.Elements(ns+ElementNames.Item);
            if (items!= null)
            {
                r.itemCollection = new Collection<SyncMLItem>();
                foreach (XElement item in items)
                    r.ItemCollection.Add(SyncMLItem.Create(item));
            }

            return r;
        }

        public static SyncMLPut Create()
        {
            SyncMLPut r = new SyncMLPut();
            r.itemCollection = new Collection<SyncMLItem>();
            return r;

        }

        public override XElement Xml
        {
            get
            {
                Element.RemoveAll();
                Element.Add(CmdID.Xml);
                if (noResp != null)
                    Element.Add(noResp.Xml);
                if (lang != null)
                    Element.Add(lang.Xml);
                if (cred != null)
                    Element.Add(cred.Xml);
                if (meta != null)
                    Element.Add(meta.Xml);

                foreach (SyncMLItem item in ItemCollection)
                    Element.Add(item.Xml);
                return Element;
            }
        }

        private SyncMLNoResp noResp;

        public SyncMLNoResp NoResp
        {
            get { if (noResp == null) noResp = new SyncMLNoResp(); return noResp; }
            set { noResp = value; }
        }

        private SyncMLLang lang;

        public SyncMLLang Lang
        {
            get { if (lang == null) lang = new SyncMLLang(); return lang; }
            set { lang = value; }
        }

        private SyncMLCred cred;

        public SyncMLCred Cred
        {
            get { if (cred == null) cred = SyncMLCred.Create(); return cred; }
            set { cred = value; }
        }

        private SyncMLMeta meta;

        public SyncMLMeta Meta
        {
            get { if (meta == null) meta = new SyncMLMeta(); return meta; }
            set { meta = value; }
        }

        private Collection<SyncMLItem> itemCollection;

        public Collection<SyncMLItem> ItemCollection
        {
            get { return itemCollection; }
        }

    }


    /// <summary>
    /// Specifies the SyncML command to retrieve data from the recipient.
    /// </summary>
    ///<ParentElements> SyncBody, Sequence, Atomic</ParentElements>
    ///<ContentModel>(CmdID, NoResp?, Lang?, Cred?, Meta?, Item+)</ContentModel>
    public class SyncMLGet : SyncMLCommand
    {
        private SyncMLGet():base(ElementNames.Get)
        {
        }

        public static SyncMLGet Create(XElement xmlData)
        {
            if (xmlData == null)
                return null;

            SyncMLGet r = new SyncMLGet();
            XNamespace ns = xmlData.Name.Namespace;
            r.CmdID = SyncMLSimpleElementFactory.Create<SyncMLCmdID>(xmlData.Element(ns + ElementNames.CmdID));
            r.NoResp = SyncMLSimpleElementFactory.Create<SyncMLNoResp>(xmlData.Element(ns+ElementNames.NoResp));
            r.Lang = SyncMLSimpleElementFactory.Create<SyncMLLang>(xmlData.Element(ns+ElementNames.Lang));
            r.Cred = SyncMLCred.Create(xmlData.Element(ns+ElementNames.Cred));
            r.Meta = SyncMLSimpleElementFactory.Create<SyncMLMeta>(xmlData.Element(ns+ElementNames.Meta));

            IEnumerable<XElement> items = xmlData.Elements(ns+ElementNames.Item);
            if (items!=null)
            {
                r.itemCollection = new Collection<SyncMLItem>();
                foreach (XElement item in items)
                    r.ItemCollection.Add(SyncMLItem.Create(item));
            }

            return r;
        }

        public static SyncMLGet Create()
        {
            SyncMLGet r = new SyncMLGet();
            r.itemCollection = new Collection<SyncMLItem>();
            return r;

        }

        public override XElement Xml
        {
            get
            {
                Element.RemoveAll();
                Element.Add(CmdID.Xml);
                if (noResp != null)
                    Element.Add(noResp.Xml);
                if (lang != null)
                    Element.Add(lang.Xml);
                if (cred != null)
                    Element.Add(cred.Xml);
                if (meta != null)
                    Element.Add(meta.Xml);

                foreach (SyncMLItem item in ItemCollection)
                    Element.Add(item.Xml);

                return Element;
            }
        }

        private SyncMLNoResp noResp;

        public SyncMLNoResp NoResp
        {
            get { if (noResp == null) noResp = new SyncMLNoResp(); return noResp; }
            set { noResp = value; }
        }

        private SyncMLLang lang;

        public SyncMLLang Lang
        {
            get { if (lang == null) lang = new SyncMLLang(); return lang; }
            set { lang = value; }
        }

        private SyncMLCred cred;

        public SyncMLCred Cred
        {
            get { if (cred == null) cred = SyncMLCred.Create(); return cred; }
            set { cred = value; }
        }

        private SyncMLMeta meta;

        public SyncMLMeta Meta
        {
            get { if (meta == null) meta = new SyncMLMeta(); return meta; }
            set { meta = value; }
        }

        private Collection<SyncMLItem> itemCollection;

        public Collection<SyncMLItem> ItemCollection
        {
            get { return itemCollection; }
        }


    }
    /// <summary>
    /// Specifies the container for the revisioning, routing information in the SyncML message.
    /// </summary>
    ///<ParentElements> SyncML</ParentElements>
    ///<ContentModel>(VerDTD, VerProto, SessionID, MsgID, Target, Source, RespURI?, NoResp?,Cred?, Meta?)</ContentModel>
    public class SyncMLHdr : SyncMLComplexElement
    {
        public static SyncMLHdr Create()
        {
            SyncMLHdr r = new SyncMLHdr();
            r.VerDTD = new SyncMLVerDTD();
            r.VerProto = new SyncMLVerProto();
            r.SessionID = new SyncMLSessionID();
            r.MsgID = new SyncMLMsgID();
            r.Target = SyncMLTarget.Create();
            r.Source = SyncMLSource.Create();
            return r;
        }

        private SyncMLHdr():base(ElementNames.SyncHdr)
        {

        }

        public static SyncMLHdr Create(XElement xmlData)
        {
            if (xmlData == null)
                return null;

            SyncMLHdr r = new SyncMLHdr();
            XNamespace ns = xmlData.Name.Namespace;
            r.VerDTD = SyncMLSimpleElementFactory.Create<SyncMLVerDTD>(xmlData.Element(ns+ElementNames.VerDTD));
            r.VerProto = SyncMLSimpleElementFactory.Create<SyncMLVerProto>(xmlData.Element(ns+ElementNames.VerProto));
            r.SessionID = SyncMLSimpleElementFactory.Create<SyncMLSessionID>(xmlData.Element(ns+ElementNames.SessionID));
            r.MsgID = SyncMLSimpleElementFactory.Create<SyncMLMsgID>(xmlData.Element(ns+ElementNames.MsgID));
            r.RespURI = SyncMLSimpleElementFactory.Create<SyncMLRespURI>(xmlData.Element(ns+ElementNames.RespURI));
            r.NoResp = SyncMLSimpleElementFactory.Create<SyncMLNoResp>(xmlData.Element(ns+ElementNames.NoResp));
            r.Cred = SyncMLCred.Create(xmlData.Element(ns+ElementNames.Cred));
            r.Meta = SyncMLSimpleElementFactory.Create<SyncMLMeta>(xmlData.Element(ns+ElementNames.Meta));

            r.Source = SyncMLSource.Create(xmlData.Element(ns+ElementNames.Source));
            r.Target = SyncMLTarget.Create(xmlData.Element(ns+ElementNames.Target));

            return r;
        }

        public override XElement Xml
        {
            get
            {
                Element.RemoveAll();
                Element.Add(VerDTD.Xml, VerProto.Xml, SessionID.Xml, MsgID.Xml, Target.Xml, Source.Xml);

                //for optional sub-elements
                if (respURI != null)
                    Element.Add(RespURI.Xml);
                if (noResp != null)
                    Element.Add(NoResp.Xml);
                if (cred != null)
                    Element.Add(Cred.Xml);
                if (meta != null)
                    Element.Add(Meta.Xml);

                return Element;
            }
        }
        private SyncMLVerDTD verDTD;

        public SyncMLVerDTD VerDTD
        {
            get { return verDTD; }
            set { verDTD = value; ThrowExceptionIfNull(value, ElementNames.VerDTD); }
        }
        private SyncMLVerProto verProto;

        public SyncMLVerProto VerProto
        {
            get { return verProto; }
            set { verProto = value; ThrowExceptionIfNull(value, ElementNames.VerProto); }
        }
        private SyncMLSessionID sessionID;

        public SyncMLSessionID SessionID
        {
            get { return sessionID; }
            set { sessionID = value; ThrowExceptionIfNull(value, ElementNames.SessionID); }
        }
        private SyncMLMsgID msgID;

        public SyncMLMsgID MsgID
        {
            get { return msgID; }
            set { msgID = value; ThrowExceptionIfNull(value, ElementNames.MsgID); }
        }
        private SyncMLTarget target;

        public SyncMLTarget Target
        {
            get { return target; }
            set { target = value; ThrowExceptionIfNull(value, ElementNames.Target); }
        }

        private SyncMLSource source;

        public SyncMLSource Source
        {
            get { return source; }
            set { source = value; ThrowExceptionIfNull(value, ElementNames.Source); }
        }

        private SyncMLRespURI respURI;

        public SyncMLRespURI RespURI
        {
            get { if (respURI == null) respURI = new SyncMLRespURI(); return respURI; }
            set { respURI = value; }
        }

        private SyncMLNoResp noResp;

        public SyncMLNoResp NoResp
        {
            get { if (noResp == null) noResp = new SyncMLNoResp(); return noResp; }
            set { noResp = value; }
        }

        private SyncMLCred cred;

        public SyncMLCred Cred
        {
            get { if (cred == null) cred = SyncMLCred.Create(); return cred; }
            set { cred = value; }
        }

        private SyncMLMeta meta;

        public SyncMLMeta Meta
        {
            get { if (meta == null) meta = new SyncMLMeta(); return meta; }
            set { meta = value; }
        }


    }

    /// <summary>
    /// Specifies the container for the body or contents of the SyncML message.
    /// </summary>
    /// Usage: 
    ///<ParentElements> SyncML</ParentElements>
    ///<ContentModel>((Alert | Atomic | Copy | Exec | Get | Map | Put | Results | Search |Sequence | Status | Sync | Add | Replace | Delete)+, Final?)</ContentModel>
    public class SyncMLBody : SyncMLComplexElement
    {
        public static SyncMLBody Create(XElement xmlData)
        {
            if (xmlData == null)
                return null;

            SyncMLBody r = new SyncMLBody();
            IEnumerable<XElement> nodes = xmlData.Elements();
            if (nodes!=null)
            {
                r.commands = new Collection<SyncMLComplexElement>();
                foreach (XElement node in nodes)
                {
                    switch (node.Name.LocalName)
                    {
                        case "Add": r.Commands.Add(SyncMLAdd.Create(node)); break;
                        case "Replace": r.Commands.Add(SyncMLReplace.Create(node)); break;
                        case "Delete": r.Commands.Add(SyncMLDelete.Create(node)); break;
                        case "Sync": r.Commands.Add(SyncMLSync.Create(node)); break;
                        case "Status": r.Commands.Add(SyncMLStatus.Create(node)); break;
                        case "Map": r.Commands.Add(SyncMLMap.Create(node)); break;
                        case "Results": r.Commands.Add(SyncMLResults.Create(node)); break;
                        case "Alert": r.Commands.Add(SyncMLAlert.Create(node)); break;
                        case "Get": r.Commands.Add(SyncMLGet.Create(node)); break;
                        case "Final": r.Final = SyncMLSimpleElementFactory.Create<SyncMLFinal>(node); break;
                        default:
                            Trace.TraceInformation("Not support this command: "+ node.Name.ToString());
                            break;
                    }
                }
            }
            return r;

        }

        public static SyncMLBody Create()
        {
            SyncMLBody r = new SyncMLBody();
            r.commands = new Collection<SyncMLComplexElement>();
            return r;
        }

        private SyncMLBody():base(ElementNames.SyncBody)
        {
        }

        private Collection<SyncMLComplexElement> commands;

        public Collection<SyncMLComplexElement> Commands
        {
            get { return commands; }
        }

        private SyncMLFinal final;

        public SyncMLFinal Final
        {
            get { if (final == null) final = new SyncMLFinal(); return final; }
            set { final = value; }
        }

        public void MarkFinal()
        {
            if (final == null) 
                final = new SyncMLFinal();
        }

        public bool HasFinal { get { return final != null; } }

        public override XElement Xml
        {
            get
            {
                Element.RemoveAll();
                foreach (SyncMLComplexElement item in Commands)
                {
                    Element.Add(item.Xml);
                }
                if (final != null)
                    Element.Add(Final.Xml);
                return Element;
            }
        }

    }


    /// <summary>
    /// Specifies the request status code for a corresponding SyncML command.
    /// </summary>
    /// <ParentElements> SyncBody</ParentElements>
    /// <ContentModel>(CmdID, MsgRef, CmdRef, Cmd, TargetRef*, SourceRef*, Cred?, Chal?, Data,Item*)</ContentModel>
    public class SyncMLStatus : SyncMLCommand
    {

        public static SyncMLStatus Create()
        {
            SyncMLStatus r = new SyncMLStatus();
            r.MsgRef = new SyncMLMsgRef();
            r.CmdRef = new SyncMLCmdRef();
            r.Cmd = new SyncMLCmd();
            r.targetRefCollection = new Collection<SyncMLTargetRef>();
            r.sourceRefCollection = new Collection<SyncMLSourceRef>();
            r.itemCollection = new Collection<SyncMLItem>();
            r.Data = new SyncMLData();
            return r;
        }

        private SyncMLStatus():base(ElementNames.Status)
        {

        }
        public static SyncMLStatus Create(XElement xmlData)
        {
            if (xmlData == null)
                return null;

            SyncMLStatus r = new SyncMLStatus();
            XNamespace ns = xmlData.Name.Namespace;
            r.CmdID = SyncMLSimpleElementFactory.Create<SyncMLCmdID>(xmlData.Element(ns + ElementNames.CmdID));
            r.MsgRef = SyncMLSimpleElementFactory.Create<SyncMLMsgRef>(xmlData.Element(ns+ElementNames.MsgRef));
            r.CmdRef = SyncMLSimpleElementFactory.Create<SyncMLCmdRef>(xmlData.Element(ns+ElementNames.CmdRef));
            r.Cmd = SyncMLSimpleElementFactory.Create<SyncMLCmd>(xmlData.Element(ns+ElementNames.Cmd));
            r.Cred = SyncMLCred.Create(xmlData.Element(ns+ElementNames.Cred));
            r.Chal = SyncMLChal.Create(xmlData.Element(ns+ElementNames.Chal));
            r.Data = SyncMLSimpleElementFactory.Create<SyncMLData>(xmlData.Element(ns+ElementNames.Data));

            IEnumerable<XElement> itemsTarget = xmlData.Elements(ns+ElementNames.TargetRef);
            if (itemsTarget != null)
            {
                r.targetRefCollection = new Collection<SyncMLTargetRef>();
                foreach (XElement item in itemsTarget)
                {
                    r.TargetRefCollection.Add(SyncMLSimpleElementFactory.Create<SyncMLTargetRef>(item));
                }
            }

            IEnumerable<XElement> itemsSource = xmlData.Elements(ns+ElementNames.SourceRef);
            if (itemsSource != null)
            {
                r.sourceRefCollection = new Collection<SyncMLSourceRef>();
                foreach (XElement item in itemsSource)
                {
                    r.SourceRefCollection.Add(SyncMLSimpleElementFactory.Create<SyncMLSourceRef>(item));
                }
            }

            IEnumerable<XElement> items = xmlData.Elements(ns+ElementNames.Item);
            if (items != null)
            {
                r.itemCollection = new Collection<SyncMLItem>();
                foreach (XElement item in items)
                {
                    r.ItemCollection.Add(SyncMLItem.Create(item));
                }
            }

            return r;

        }

        public override XElement Xml
        {
            get
            {
                Element.RemoveAll();
                Element.Add(CmdID.Xml, MsgRef.Xml, CmdRef.Xml, Cmd.Xml);
                foreach (SyncMLTargetRef targetRef in TargetRefCollection)
                    Element.Add(targetRef.Xml);

                foreach (SyncMLSourceRef sourceRef in SourceRefCollection)
                    Element.Add(sourceRef.Xml);

                if (cred != null)
                    Element.Add(Cred.Xml);
                if (chal != null)
                    Element.Add(Chal.Xml);
                Element.Add(Data.Xml);

                foreach (SyncMLItem item in ItemCollection)
                    Element.Add(item.Xml);

                return Element;

            }
        }

        private SyncMLMsgRef msgRef;

        public SyncMLMsgRef MsgRef
        {
            get { return msgRef; }
            set { msgRef = value; ThrowExceptionIfNull(value, "Status/MsgRef"); }
        }

        private SyncMLCmdRef cmdRef;

        public SyncMLCmdRef CmdRef
        {
            get { return cmdRef; }
            set { cmdRef = value; ThrowExceptionIfNull(value, "Status/CmdRef"); }
        }


        private SyncMLCmd cmd;

        public SyncMLCmd Cmd
        {
            get { return cmd; }
            set { cmd = value; ThrowExceptionIfNull(value, "Status/Cmd"); }
        }

        private Collection<SyncMLTargetRef> targetRefCollection;

        public Collection<SyncMLTargetRef> TargetRefCollection
        {
            get { return targetRefCollection; }
        }

        private Collection<SyncMLSourceRef> sourceRefCollection;

        public Collection<SyncMLSourceRef> SourceRefCollection
        {
            get { return sourceRefCollection; }
        }

        private SyncMLCred cred;

        public SyncMLCred Cred
        {
            get { if (cred == null) cred = SyncMLCred.Create(); return cred; }
            set { cred = value; }
        }

        private SyncMLChal chal;

        public SyncMLChal Chal
        {
            get { if (chal == null) chal = SyncMLChal.Create(); return chal; }
            set { chal = value; }
        }

        private SyncMLData data;

        public SyncMLData Data
        {
            get { return data; }
            set { data = value; ThrowExceptionIfNull(value, "Status/Data"); }
        }

        private Collection<SyncMLItem> itemCollection;

        public Collection<SyncMLItem> ItemCollection
        {
            get { return itemCollection; }
        }


    }

    /// <summary>
    /// Specifies the SyncML command for sending custom content information to the recipient. 
    /// The command provides a mechanism for communicating content information,such as state 
    /// information or notifications to an application on the recipient device. In addition, 
    /// this command provides a "standard way to specify non-standard" extended commands, beyond 
    /// those defined in this specification.
    /// 
    /// Alert types (Alert code values) are defined in http://www.syncml.org/docs/OMA-TS-DS_DataSyncRep-V1_2_1-20070810-A page 81
    /// </summary>
    /// <ParentElements> Atomic, Sequence, SyncBody</ParentElements>
    ///<ContentModel>(CmdID, NoResp?, Cred?, Data?, Correlator?, Item*)</ContentModel>
    ///<Restriction>http://www.syncml.org/docs/syncml_sync_represent_v11_20020215.pdf page 35
    ///</Restriction>
    public class SyncMLAlert : SyncMLCommand
    {
        public static SyncMLAlert Create(XElement xmlData)
        {
            if (xmlData == null)
                return null;

            SyncMLAlert r = new SyncMLAlert();
            XNamespace ns = xmlData.Name.Namespace;
            r.CmdID = SyncMLSimpleElementFactory.Create<SyncMLCmdID>(xmlData.Element(ns + ElementNames.CmdID));
            r.NoResp = SyncMLSimpleElementFactory.Create<SyncMLNoResp>(xmlData.Element(ns+ElementNames.NoResp));
            r.Cred = SyncMLCred.Create(xmlData.Element(ns+ElementNames.Cred));
            r.Data = SyncMLSimpleElementFactory.Create<SyncMLData>(xmlData.Element(ns+ElementNames.Data));

            IEnumerable<XElement> items = xmlData.Elements(ns+ElementNames.Item);
            if (items != null)
            {
                r.itemCollection = new Collection<SyncMLItem>();
                foreach (XElement item in items)
                {
                    r.ItemCollection.Add(SyncMLItem.Create(item));
                }
            }

            return r;
        }

        public static SyncMLAlert Create()
        {
            SyncMLAlert r = new SyncMLAlert();
            r.itemCollection = new Collection<SyncMLItem>();
            return r;
        }

        private SyncMLAlert():base(ElementNames.Alert)
        {

        }

        public override XElement Xml
        {
            get
            {
                Element.RemoveAll();
                Element.Add(CmdID.Xml);
                if (noResp != null)
                    Element.Add(NoResp.Xml);
                if (cred != null)
                    Element.Add(Cred.Xml);
                if (data != null)
                    Element.Add(Data.Xml);
                if (correlator != null)
                    Element.Add(correlator.Xml);
                foreach (SyncMLItem item in ItemCollection)
                    Element.Add(item.Xml);

                return Element;
            }
        }

        private SyncMLCorrelator correlator;
        /// <summary>
        /// The Correlator element type MUST be identical to the Correlator value of an Exec command if the alert is sent as an
        /// asynchronous response to that Exec command.
        /// </summary>
        public SyncMLCorrelator Correlator
        {
            get { if (correlator == null) correlator = new SyncMLCorrelator(); return correlator; }
            set { correlator = value; }
        }


        private SyncMLNoResp noResp;
        /// <summary>
        /// If specified, the optional NoResp element type indicates that a response status code MUST
        /// NOT be returned for the command.
        /// </summary>
        public SyncMLNoResp NoResp
        {
            get { if (noResp == null) noResp = new SyncMLNoResp(); return noResp; }
            set { noResp = value; }
        }


        private SyncMLCred cred;
        /// <summary>
        /// The optional Cred element type specifies the authentication credential to be used for the
        /// command. If not present, the default authentication credential is taken from the SyncHdr
        /// element type. If a Cred element type is not present in any of these other element types,
        /// then the command is specified without an authentication credential.
        /// </summary>
        public SyncMLCred Cred
        {
            get { if (cred == null) cred = SyncMLCred.Create(); return cred; }
            set { cred = value; }
        }

        private SyncMLData data;
        /// <summary>
        /// The optional Data element type specifies the type of alert.
        /// </summary>
        public SyncMLData Data
        {
            get { if (data == null) data = new SyncMLData(); return data; }
            set { data = value; }
        }


        private Collection<SyncMLItem> itemCollection;
        /// <summary>
        /// Optionally, one or more Item element types MUST be specified. The Item element type
        /// specifies parameters for the Alert command. The Target and Source specified within
        /// the Item element type MUST be an absolute URI.
        /// </summary>
        public Collection<SyncMLItem> ItemCollection
        {
            get { return itemCollection; }
        }


    }


    /// <summary>
    /// Specifies the SyncML command that is used to return the results of a Search or Get command.
    /// </summary>
    ///<ParentElements> SyncBody</ParentElements>  
    ///<ContentModel>(CmdID, MsgRef?, CmdRef, Meta?, TargetRef?, SourceRef?, Item+)</ContentModel>
    public class SyncMLResults : SyncMLCommand
    {
        public static SyncMLResults Create()
        {
            SyncMLResults r = new SyncMLResults();
            r.CmdRef = new SyncMLCmdRef();
            r.itemCollection = new Collection<SyncMLItem>();
            return r;
        }

        private SyncMLResults():base(ElementNames.Results)
        {

        }

        public static SyncMLResults Create(XElement xmlData)
        {
            if (xmlData == null)
                return null;

            SyncMLResults r = new SyncMLResults();
            XNamespace ns = xmlData.Name.Namespace;
            r.CmdID = SyncMLSimpleElementFactory.Create<SyncMLCmdID>(xmlData.Element(ns + ElementNames.CmdID));
            r.MsgRef = SyncMLSimpleElementFactory.Create<SyncMLMsgRef>(xmlData.Element(ns+ElementNames.MsgRef));
            r.CmdRef = SyncMLSimpleElementFactory.Create<SyncMLCmdRef>(xmlData.Element(ns+ElementNames.CmdRef));
            r.Meta = SyncMLSimpleElementFactory.Create<SyncMLMeta>(xmlData.Element(ns+ElementNames.Meta));
            r.TargetRef = SyncMLSimpleElementFactory.Create<SyncMLTargetRef>(xmlData.Element(ns+ElementNames.TargetRef));
            r.SourceRef = SyncMLSimpleElementFactory.Create<SyncMLSourceRef>(xmlData.Element(ns+ElementNames.SourceRef));

            IEnumerable<XElement>  items = xmlData.Elements(ns+ElementNames.Item);
            if (items!= null)
            {
                r.itemCollection = new Collection<SyncMLItem>();
                foreach (XElement item in items)
                {
                    r.ItemCollection.Add(SyncMLItem.Create(item));
                }
            }

            return r;
        }

        public override XElement Xml
        {
            get
            {
                Element.RemoveAll();
                Element.Add(CmdID.Xml);
                if (msgRef != null)
                    Element.Add(MsgRef.Xml);
                if (cmdRef != null)
                    Element.Add(CmdRef.Xml);
                if (meta != null)
                    Element.Add(Meta.Xml);
                if (sourceRef != null)
                    Element.Add(SourceRef.Xml);
                if (targetRef != null)
                    Element.Add(TargetRef.Xml);
                foreach (SyncMLItem item in ItemCollection)
                    Element.Add(item.Xml);
                return Element;
            }
        }


        private SyncMLMsgRef msgRef;

        public SyncMLMsgRef MsgRef
        {
            get { if (msgRef == null) msgRef = new SyncMLMsgRef(); return msgRef; }
            set { msgRef = value; }
        }

        private SyncMLCmdRef cmdRef;

        public SyncMLCmdRef CmdRef
        {
            get { return cmdRef; }
            set { cmdRef = value; }
        }


        private SyncMLMeta meta;

        public SyncMLMeta Meta
        {
            get { if (meta == null) meta = new SyncMLMeta(); return meta; }
            set { meta = value; }
        }

        private SyncMLTargetRef targetRef;

        public SyncMLTargetRef TargetRef
        {
            get { if (targetRef == null) targetRef = new SyncMLTargetRef(); return targetRef; }
            set { targetRef = value; }
        }

        private SyncMLSourceRef sourceRef;

        public SyncMLSourceRef SourceRef
        {
            get { if (sourceRef == null) sourceRef = new SyncMLSourceRef(); return sourceRef; }
            set { sourceRef = value; }
        }


        private Collection<SyncMLItem> itemCollection;

        public Collection<SyncMLItem> ItemCollection
        {
            get { return itemCollection; }
        }
    }

    /// <summary>
    /// Specifies the SyncML command to order the processing of a set of SyncML commands.
    /// </summary>
    ///<ParentElements> Atomic, Sync, SyncBody</ParentElements>
    ///<ContentModel>(CmdID, NoResp?, Meta?, (Add | Replace | Delete | Copy | Atomic | Map |Sync | Get | Alert | Exec)+)</ContentModel>
    public class SyncMLSequence : SyncMLCommand
    {
        public static SyncMLSequence Create()
        {
            SyncMLSequence r = new SyncMLSequence();
            r.commands = new Collection<SyncMLCommand>();
            return r;
        }

        private SyncMLSequence():base(ElementNames.Sequence)
        {

        }

        public static SyncMLSequence Create(XElement xmlData)
        {
            if (xmlData == null)
                return null;

            SyncMLSequence r = new SyncMLSequence();
            XNamespace ns = xmlData.Name.Namespace;
            r.CmdID = SyncMLSimpleElementFactory.Create<SyncMLCmdID>(xmlData.Element(ns + ElementNames.CmdID));
            r.NoResp = SyncMLSimpleElementFactory.Create<SyncMLNoResp>(xmlData.Element(ns+ElementNames.NoResp));
            r.Meta = SyncMLSimpleElementFactory.Create<SyncMLMeta>(xmlData.Element(ns+ElementNames.Meta));

            IEnumerable<XElement> nodes = xmlData.Elements();
            if (nodes!=null)
            {
                r.commands = new Collection<SyncMLCommand>();
                foreach (XElement node in nodes)
                {
                    switch (node.Name.LocalName)
                    {
                        case ElementNames.Add: r.Commands.Add(SyncMLAdd.Create(node)); break;
                        case ElementNames.Replace: r.Commands.Add(SyncMLReplace.Create(node)); break;
                        case ElementNames.Delete: r.Commands.Add(SyncMLDelete.Create(node)); break;
                        case ElementNames.Sync: r.Commands.Add(SyncMLSync.Create(node)); break;
                        case ElementNames.Map: r.Commands.Add(SyncMLMap.Create(node)); break;
                        case ElementNames.Alert: r.Commands.Add(SyncMLAlert.Create(node)); break;
                        case ElementNames.CmdID: break;//handled
                        case ElementNames.NoResp: break; //handled
                        case ElementNames.Meta: break;//handled
                        default: 
                            Debug.Assert(false, "Invalid element: "+node.Name.ToString());
                            break;
                    }
                }
            }

            return r;
        }



        public override XElement Xml
        {
            get
            {
                Element.RemoveAll();
                Element.Add(CmdID.Xml);
                if (noResp != null)
                    Element.Add(NoResp.Xml);
                if (meta != null)
                    Element.Add(Meta.Xml);
                foreach (SyncMLCommand command in Commands)
                    Element.Add(command.Xml);
                return Element;
            }
        }


        private SyncMLNoResp noResp;

        public SyncMLNoResp NoResp
        {
            get { if (noResp == null) noResp = new SyncMLNoResp(); return noResp; }
            set { noResp = value; }
        }


        private SyncMLMeta meta;

        public SyncMLMeta Meta
        {
            get
            {
                if (meta == null) meta = new SyncMLMeta();
                return meta;
            }
            set { meta = value; }
        }



        private Collection<SyncMLCommand> commands;

        public Collection<SyncMLCommand> Commands
        {
            get { return commands; }
        }
    }


    /// <summary>
    /// Specifies the SyncML command that indicates a data synchronization operation.
    /// </summary>
    ///<ParentElements> Atomic, Sequence, SyncBody</ParentElements>
    ///<ContentModel>(CmdID, NoResp?, Cred?, Target?, Source?, Meta?, NumberOfChanges?, (Add | Atomic | Copy |Delete | Sequence | Replace)*)</ContentModel>
    public class SyncMLSync : SyncMLCommand
    {
        public static SyncMLSync Create()
        {
            SyncMLSync r = new SyncMLSync();
            r.commands = new Collection<SyncMLCommand>();
            return r;
        }

        private SyncMLSync():base(ElementNames.Sync)
        {

        }
        public static SyncMLSync Create(XElement xmlData)
        {
            if (xmlData == null)
                return null;

            SyncMLSync r = new SyncMLSync();
            XNamespace ns = xmlData.Name.Namespace;
            r.CmdID = SyncMLSimpleElementFactory.Create<SyncMLCmdID>(xmlData.Element(ns + ElementNames.CmdID));
            r.NoResp = SyncMLSimpleElementFactory.Create<SyncMLNoResp>(xmlData.Element(ns+ElementNames.NoResp));
            r.Meta = SyncMLSimpleElementFactory.Create<SyncMLMeta>(xmlData.Element(ns+ElementNames.Meta));
            r.NumberOfChanges = SyncMLSimpleElementFactory.Create<SyncMLNumberOfChanges>(xmlData.Element(ns+ElementNames.NumberOfChanges));

            r.Cred = SyncMLCred.Create(xmlData.Element(ns+ElementNames.Cred));
            r.Target = SyncMLTarget.Create(xmlData.Element(ns+ElementNames.Target));
            r.Source = SyncMLSource.Create(xmlData.Element(ns+ElementNames.Source));

            IEnumerable<XElement> nodes = xmlData.Elements();
            if (nodes!=null)
            {
                r.commands = new Collection<SyncMLCommand>();
                foreach (XElement node in nodes)
                {
                    switch (node.Name.LocalName)
                    {
                        case "Add": r.Commands.Add(SyncMLAdd.Create(node)); break;
                        case "Replace": r.Commands.Add(SyncMLReplace.Create(node)); break;
                        case "Delete": r.Commands.Add(SyncMLDelete.Create(node)); break;
                        case "Sequence": r.Commands.Add(SyncMLSequence.Create(node)); break;
                        default:
                            //do nothing for non SyncMLCommand
                            break;
                    }
                }
            }

            return r;
        }



        public override XElement Xml
        {
            get
            {
                Element.RemoveAll();
                Element.Add(CmdID.Xml);
                if (noResp != null)
                    Element.Add(NoResp.Xml);
                if (cred != null)
                    Element.Add(Cred.Xml);
                if (target != null)
                    Element.Add(Target.Xml);
                if (source != null)
                    Element.Add(Source.Xml);
                if (meta != null)
                    Element.Add(Meta.Xml);
                if (numberOfChanges != null)
                    Element.Add(NumberOfChanges.Xml);
                foreach (SyncMLCommand command in Commands)
                    Element.Add(command.Xml);

                return Element;
            }
        }


        private SyncMLNoResp noResp;

        public SyncMLNoResp NoResp
        {
            get { if (noResp == null) noResp = new SyncMLNoResp(); return noResp; }
            set { noResp = value; }
        }


        private SyncMLNumberOfChanges numberOfChanges;
        public SyncMLNumberOfChanges NumberOfChanges
        {
            get { if (numberOfChanges == null) numberOfChanges = new SyncMLNumberOfChanges(); return numberOfChanges; }
            set { numberOfChanges = value; }
        }

        private SyncMLMeta meta;

        public SyncMLMeta Meta
        {
            get { if (meta == null) meta = new SyncMLMeta(); return meta; }
            set { meta = value; }
        }

        private SyncMLCred cred;

        public SyncMLCred Cred
        {
            get { if (cred == null) cred = SyncMLCred.Create(); return cred; }
            set { cred = value; }
        }

        private SyncMLSource source;

        public SyncMLSource Source
        {
            get { if (source == null) source = SyncMLSource.Create(); return source; }
            set { source = value; }
        }

        private SyncMLTarget target;

        public SyncMLTarget Target
        {
            get { if (target == null) target = SyncMLTarget.Create(); return target; }
            set { target = value; }
        }


        private Collection<SyncMLCommand> commands;

        public Collection<SyncMLCommand> Commands
        {
            get { return commands; }
        }
    }

    /// <summary>
    /// Specifies the SyncML command to request that the subordinate commands be executed as a set or not at all.
    /// </summary>
    ///<ParentElements> Sequence, Sync, SyncBody</ParentElements>
    ///<ContentModel>(CmdID, NoResp?, Meta?, (Add | Delete | Copy | Atomic | Map | Replace |Sequence | Sync | Get | Exec | Alert)+)
    ///              (CmdID, NoResp?, Meta?, (Add | Delete | Copy | Atomic | Map | Move |Replace | Sequence | Sync | Get | Exec | Alert)+)</ContentModel>
    public class SyncMLAtomic : SyncMLCommand
    {
        public static SyncMLAtomic Create()
        {
            SyncMLAtomic r = new SyncMLAtomic();
            r.commands = new Collection<SyncMLCommand>();
            return r;
        }

        private SyncMLAtomic():base(ElementNames.Atomic)
        {

        }
        public static SyncMLAtomic Create(XElement xmlData)
        {
            if (xmlData == null)
                return null;

            SyncMLAtomic r = new SyncMLAtomic();
            XNamespace ns = xmlData.Name.Namespace;
            r.CmdID = SyncMLSimpleElementFactory.Create<SyncMLCmdID>(xmlData.Element(ns + ElementNames.CmdID));
            r.NoResp = SyncMLSimpleElementFactory.Create<SyncMLNoResp>(xmlData.Element(ns+ElementNames.NoResp));
            r.Meta = SyncMLSimpleElementFactory.Create<SyncMLMeta>(xmlData.Element(ns+ElementNames.Meta));

            IEnumerable<XElement> nodes = xmlData.Elements();
            if (nodes!=null)
            {
                r.commands = new Collection<SyncMLCommand>();
                foreach (XElement node in nodes)
                {
                    switch (node.Name.LocalName)
                    {
                        case ElementNames.Add: r.Commands.Add(SyncMLAdd.Create(node)); break;
                        case ElementNames.Replace: r.Commands.Add(SyncMLReplace.Create(node)); break;
                        case ElementNames.Delete: r.Commands.Add(SyncMLDelete.Create(node)); break;
                        case ElementNames.Sequence: r.Commands.Add(SyncMLSequence.Create(node)); break;
                        case ElementNames.Atomic: r.Commands.Add(SyncMLAtomic.Create(node)); break;
                        case ElementNames.Map: r.Commands.Add(SyncMLMap.Create(node)); break;
                        case ElementNames.Sync: r.Commands.Add(SyncMLSync.Create(node)); break;
                        case ElementNames.Alert: r.Commands.Add(SyncMLAlert.Create(node)); break;
                        case ElementNames.CmdID: break;//handled
                        case ElementNames.NoResp: break; //handled
                        case ElementNames.Meta: break;//handled
                        default:
                            Debug.Assert(false, "Invalid element: "+node.Name.ToString());
                            break;
                    }
                }
            }

            return r;
        }



        public override XElement Xml
        {
            get
            {
                Element.RemoveAll();
                Element.Add(CmdID.Xml);
                if (noResp != null)
                    Element.Add(NoResp.Xml);
                if (meta != null)
                    Element.Add(Meta.Xml);
                foreach (SyncMLCommand command in Commands)
                    Element.Add(command.Xml);
                return Element;
            }
        }


        private SyncMLNoResp noResp;

        public SyncMLNoResp NoResp
        {
            get { if (noResp == null) noResp = new SyncMLNoResp(); return noResp; }
            set { noResp = value; }
        }


        private SyncMLMeta meta;

        public SyncMLMeta Meta
        {
            get { if (meta == null) meta = new SyncMLMeta(); return meta; }
            set { meta = value; }
        }


        private Collection<SyncMLCommand> commands;

        public Collection<SyncMLCommand> Commands
        {
            get { return commands; }
        }
    }

    /// <summary>
    /// Specifies the container for a SyncML Message.
    /// </summary>
    ///<ParentElements> None. This is the root or document element.</ParentElements>
    ///<ContentModel>(SyncHdr, SyncBody)</ContentModel>
    public class SyncMLSyncML : SyncMLComplexElement
    {
        private int nextCmdID;

        public SyncMLCmdID NextCmdID
        {
            get { nextCmdID++; return SyncMLSimpleElementFactory.Create<SyncMLCmdID>(nextCmdID.ToString()); }
        }

        /// <summary>
        /// Generate a SyncMLSyncML object throught the XML data.
        /// </summary>
        /// <param name="xmlData">XML data.</param>
        /// <returns>Return null if fail.</returns>
        public static SyncMLSyncML Create(XElement xmlData)
        {
            if (xmlData == null)
                return null;

            SyncMLSyncML r = new SyncMLSyncML();
            XNamespace ns = xmlData.Name.Namespace;
            r.Hdr = SyncMLHdr.Create(xmlData.Element(ns+ElementNames.SyncHdr));
            r.Body = SyncMLBody.Create(xmlData.Element(ns+ElementNames.SyncBody));

            if ((r.Hdr == null) || (r.Body == null))
                return null;

            return r;

        }

        private SyncMLSyncML()
            : base(ElementNames.SyncML)
        {
        }

        public static SyncMLSyncML Create()
        {
            SyncMLSyncML r = new SyncMLSyncML();
            r.Hdr = SyncMLHdr.Create();
            r.Body = SyncMLBody.Create();
            return r;

        }

    /*    public static SyncMLSyncML Create(string xmlString)
        {
            if (String.IsNullOrEmpty(xmlString))
                return null;

            return Create(new SyncMLNavigator(xmlString));
        }*/

        public override XElement Xml
        {
            get
            {
                Element.RemoveAll();
                Element.Add(Hdr.Xml);
                Element.Add(Body.Xml);
                return Element;
            }
        }


        private SyncMLHdr hdr;

        public SyncMLHdr Hdr
        {
            get { return hdr; }
            set { hdr = value; }
        }

        private SyncMLBody body;

        public SyncMLBody Body
        {
            get { return body; }
            set { body = value; }
        }

    }
}
