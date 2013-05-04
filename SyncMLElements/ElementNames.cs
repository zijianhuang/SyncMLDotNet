using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
namespace Fonlow.SyncML.Elements
{
    /// <summary>
    /// Contain strings of SyncML elements.
    /// </summary>
    internal static class ElementNames
    {
        #region Sync

        public const string Archive = "Archive";
        public const string Chal = "Chal";
        public const string Cmd = "Cmd";
        public const string CmdID = "CmdID";
        public const string CmdRef = "CmdRef";
        public const string Cred = "Cred";
        public const string Field = "Field";
        public const string Filter = "Filter";
        public const string FilterType = "FilterType";
        public const string Final = "Final";
        public const string Lang = "Lang";
        public const string LocName = "LocName";
        public const string LocURI = "LocURI";
        public const string MoreData = "MoreData";
        public const string MsgID = "MsgID";
        public const string MsgRef = "MsgRef";
        public const string NoResp = "NoResp";
        public const string NoResults = "NoResults";
        public const string NumberOfChanges = "NumberOfChanges";
        public const string Record = "Record";
        public const string RespURI = "RespURI";
        public const string SessionID = "SessionID";
        public const string SftDel = "SftDel";
        public const string Source = "Source";
        public const string SourceParent = "SourceParent";
        public const string SourceRef = "SourceRef";
        public const string Target = "Target";
        public const string TargetParent = "TargetParent";
        public const string TargetRef = "TargetRef";
        public const string VerDTD = "VerDTD";
        public const string VerProto = "VerProto";
        public const string SyncML = "SyncML";
        public const string SyncHdr = "SyncHdr";
        public const string SyncBody = "SyncBody";
        public const string Data = "Data";
        public const string Item = "Item";
        public const string Meta = "Meta";
        public const string Correlator = "Correlator";
        public const string Status = "Status";
        public const string Add = "Add";
        public const string Alert = "Alert";
        public const string Atomic = "Atomic";
        public const string Copy = "Copy";
        public const string Delete = "Delete";
        public const string Exec = "Exec";
        public const string Get = "Get";
        public const string Map = "Map";
        public const string MapItem = "MapItem";
        public const string Move = "Move";
        public const string Put = "Put";
        public const string Replace = "Replace";
        public const string Results = "Results";
        public const string Search = "Search";
        public const string Sequence = "Sequence";
        public const string Sync = "Sync";

        #endregion 

        #region Device

        public const string CTCap = "CTCap";
        public const string CTType = "CTType";
        public const string DataStore = "DataStore";
        public const string DataType = "DataType";
        public const string DevID = "DevID";
        public const string DevInf = "DevInf";
        public const string DevTyp = "DevTyp";
        public const string DisplayName = "DisplayName";
        public const string DSMem = "DSMem";
        public const string Ext = "Ext";
        public const string FieldLevel = "FieldLevel";
        public const string Filter_Rx = "Filter-Rx";
        public const string FilterCap = "FilterCap";
        public const string FilterKeyword = "FilterKeyword";
        public const string FwV = "FwV";
        public const string HwV = "HwV";
        public const string Man = "Man";
        public const string MaxGUIDSize = "MaxGUIDSize";
        public const string MaxID = "MaxID";
        public const string MaxMem = "MaxMem";
        public const string MaxOccur = "MaxOccur";
        public const string MaxSize = "MaxSize";
        public const string Mod = "Mod";
        public const string NoTruncate = "NoTruncate";
        public const string OEM = "OEM";
        public const string ParamName = "ParamName";
        public const string Property = "Property";
        public const string PropName = "PropName";
        public const string PropParam = "PropParam";
        public const string Rx = "Rx";
        public const string Rx_Pref = "Rx-Pref";
        public const string SharedMem = "SharedMem";
   //     public const string SourceRef = "SourceRef";
        public const string SupportHierarchicalSync = "SupportHierarchicalSync";
        public const string SupportLargeObjs = "SupportLargeObjs";
        public const string SupportNumberOfChanges = "SupportNumberOfChanges";
        public const string SwV = "SwV";
        public const string SyncCap = "SyncCap";
        public const string SyncType = "SyncType";
        public const string Tx = "Tx";
        public const string Tx_Pref = "Tx-Pref";
        public const string UTC = "UTC";
        public const string ValEnum = "ValEnum";
        public const string VerCT = "VerCT";
 //       public const string VerDTD = "VerDTD";
        public const string XNam = "XNam";
        public const string XVal = "XVal";

        #endregion

        #region Meta

        public const string Anchor = "Anchor";
        public const string EMI = "EMI";
  //      public const string FieldLevel = "FieldLevel";
        public const string Format = "Format";
        public const string FreeID = "FreeID";
        public const string FreeMem = "FreeMem";
        public const string Last = "Last";
        public const string Mark = "Mark";
        public const string MaxMsgSize = "MaxMsgSize";
        public const string MaxObjSize = "MaxObjSize";
        public const string Mem = "Mem";
        public const string MetInf = "MetInf";
        public const string Next = "Next";
        public const string NextNonce = "NextNonce";
        public const string ShareMem = "ShareMem";
        public const string Size = "Size";
        public const string Type = "Type";
        public const string Version = "Version";

        #endregion

        public const string SyncMLMetInf = "syncml:metinf";
        public const string SyncMLDevInf = "syncml:devinf";
        public const string SyncMLVersion = "";// "SYNCML:SYNCML1.2";
    }
}
