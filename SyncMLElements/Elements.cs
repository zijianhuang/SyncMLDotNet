using System;
using System.Collections.Generic;
using System.Text;

/// This file defined SyncML elements. 
/// Naming Convention:
///     1. Use SyncML element names as surfix.
namespace Fonlow.SyncMLElements
{
    public interface ISyncMLSimpleElement
    {
        string Content { get;set;}
        string XML { get;}
    }

    /// <summary>
    /// Value will return the text representation of XML data insdie
    /// </summary>
    public interface ISyncMLComplexElement
    {
      //  string Content { get;set;}
        string XML { get;}
    }

    /// <summary>
    /// Parent class of SyncML commands
    /// </summary>
    public interface ISyncMLCommand : ISyncMLComplexElement
    {
        ISyncMLCmdID CmdID { get;set;}
    }

    /// <summary>
    /// Parent class of SyncML Data Description elements
    /// </summary>
    public interface ISyncMLDataDescription : ISyncMLComplexElement
    {

    }

    /// <summary>
    /// Specifies the SyncML command to add data to a data collection.
    /// </summary>
    ///<ParentElements> Atomic, Sequence, Sync, SyncBody.</ParentElements>
    ///<ContentModel>(CmdID, NoResp?, Cred?, Meta?, Item+)</ContentModel>
    public interface ISyncMLAdd : ISyncMLComplexElement
    {
        ISyncMLNoResp NoResp { get;set;}
        ISyncMLCred Cred { get;set;}
        ISyncMLMeta Meta { get;set;}
        ICollection<ISyncMLItem> ItemCollection { get;set;}

    }

    public interface ISyncMLNoResp : ISyncMLSimpleElement
    {

    }

    public interface ISyncMLNoResults : ISyncMLSimpleElement
    {

    }

    /// <summary>
    /// Specifies a SyncML message-unique command identifier.
    /// </summary>
    ///<ParentElements>
    ///Add, Alert, Atomic, Copy, Delete, Exec, Get, Map, Put, Replace,
    ///Results, Search, Sequence, Status, Sync
    ///</ParentElements>
    ///<ContentModel>(#PCDATA)</ContentModel>
    public interface ISyncMLCmdID : ISyncMLSimpleElement
    {

    }

    /// <summary>
    /// Specifies an authentication credential for the originator.
    /// </summary>
    ///<ParentElements> Add, Alert, Copy, Delete, Exec, Get, Put, Map, Replace, Search,Status, Sync, SyncHdr</ParentElements>
    ///<ContentModel>(Meta?, Data)</ContentModel>
    public interface ISyncMLCred : ISyncMLComplexElement
    {
        /// <summary>
        /// The returned value might be null.
        /// </summary>
        ISyncMLMeta Meta { get; set;}
        ISyncMLData Data { get;set;}
    }

    /// <summary>
    /// Specifies meta-information about the parent element type.
    /// </summary>
    ///<ParentElements> Add, Atomic, Chal, Copy, Cred, Delete, Get, Item, Map, Put,Replace, Results, Search, Sequence, Sync</ParentElements>
    ///<ContentModel>(#PCDATA)</ContentModel>
    public interface ISyncMLMeta : ISyncMLDataDescription
    {

    }

    /// <summary>
    /// Specifies discrete SyncML data.
    /// </summary>
    ///<ParentElements> Alert, Cred, Item, Status, Search</ParentElements>
    ///<ContentModel>(#PCDATA)</ContentModel>
    public interface ISyncMLData : ISyncMLDataDescription
    {

    }


    /// <summary>
    /// Specifies a container for item data.
    /// </summary>
    ///<ParentElements> Add, Alert, Copy, Delete, Exec, Get, Put, Replace, Results,Status</ParentElements>
    ///<ContentModel>(Target?, Source?, Meta?, Data?)</ContentModel>
    public interface ISyncMLItem : ISyncMLDataDescription
    {
        ISyncMLTarget Target { get;set;}
        ISyncMLSource Source { get;set;}
        ISyncMLMeta Meta { get;set;}
        ISyncMLData Data { get;set;}
    }

    /// <summary>
    /// Specifies source routing or mapping information.
    /// </summary>
    /// <ParentElements>Parent Elements: Item, Map, MapItem, Search, Sync, SyncHdr</ParentElements>
    /// <ContentModel>(LocURI, LocName?)</ContentModel>
    public interface ISyncMLSource : ISyncMLComplexElement
    {
        ISyncMLLocURI LocURI { get;set;}
        /// <summary>
        /// Might be null
        /// </summary>
        ISyncMLLocName LocName { get;set;}

    }

    /// <summary>
    /// Usage: Specifies target routing or mapping information.
    /// </summary>
    /// <ParentElements>Parent Elements: Item, Map, MapItem, Search, Sync, SyncHdr</ParentElements>
    /// <ContentModel>(LocURI, LocName?)</ContentModel>
    public interface ISyncMLTarget : ISyncMLComplexElement
    {
        ISyncMLLocURI LocURI { get;set;}
        /// <summary>
        /// Might be null
        /// </summary>
        ISyncMLLocName LocName { get;set;}
    }

    /// <summary>
    /// Specifies the target or source specific address.
    /// </summary>
    /// <ParentElements>Target, Source</ParentElements>
    public interface ISyncMLLocURI : ISyncMLSimpleElement
    {

    }

    /// <summary>
    ///Specifies the display name for the target or source address.
    /// </summary>
    /// <ParentElements>Target, Source</ParentElements>
    public interface ISyncMLLocName : ISyncMLSimpleElement
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <ParentElements> Map</ParentElements>
///<ContentModel>(Target, Source)</ContentModel>
    public interface ISyncMLMapItem : ISyncMLComplexElement
    {
        ISyncMLTarget Target { get;set;}
        ISyncMLSource Source { get;set;}
    }

    /// <summary>
    /// Specifies the SyncML command used to update identifier maps.
    /// </summary>
    /// Usage: 
///<ParentElements> Atomic, Sequence, SyncBody</ParentElements>
///<ContentModel>(CmdID, Target, Source, Cred?, Meta?, MapItem+)</ContentModel>
    public interface ISyncMLMap : ISyncMLCommand
    {
        ISyncMLTarget Target { get;set;}
        ISyncMLSource Source { get;set;}
        ISyncMLCred Cred { get;set;}
        ISyncMLMeta Meta {get;set;}
        ICollection<ISyncMLMapItem> MapItemCollection { get;set;}
    }

}
