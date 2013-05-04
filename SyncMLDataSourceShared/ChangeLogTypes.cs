using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Collections.Specialized;
using Fonlow.SyncML.Elements;

namespace Fonlow.SyncML.Common
{
    /// <summary>
    /// Extract a number of sync commands from a source which generate 
    /// SyncML sync commands from the change log of local data source.
    /// </summary>
    public interface ISyncCommandsSource
    {
        /// <summary>
        /// Extract a number of sync commands. The implementation should defined how many commands each extraction.
        /// </summary>
        /// <param name="commands">The source returns a collection of sync commands. </param>
        /// <returns>True if there are commands to returned, false if zero command is return.</returns>
        bool ExtractNextCommands(IList<Fonlow.SyncML.Elements.SyncMLCommand> commands);
        /// <summary>
        /// Total number of changes
        /// </summary>
        int NumberOfChanges { get; }

        void PrepareForSlowSync();
    }


    /// <summary>
    /// Interface to local data source that can handle SyncML sync commands.
    /// Many of the properties may be optional throngh returning null or empty string, as these
    /// properties are mostly for visual feedback which might not be critical for operations.
    /// </summary>
    public interface ILocalDataSource
    {
       /* /// <summary>
        /// Implementation shold have the exchange type defined in the constructor.
        /// </summary>
        string ExchangeType { get; }*/

        /// <summary>
        /// Generally the name of the product providing data, like MS Outlook or Open Contacts.
        /// </summary>
        string DataSourceName { get; }

        /// <summary>
        /// Apply syncML sync commands to local data source.
        /// </summary>
        /// <param name="commands">Sync commands from the server.</param>
        /// <param name="lastAnchorTimeStr">Last anchor time string used by local application.</param>
        /// <returns>Local ID/Server ID pair</returns>
        NameValueCollection ApplySyncCommands(IList<SyncMLCommand> commands, string lastAnchorTimeText);

        /// <summary>
        /// Obtain sync permission from local data source.
        /// A simple implementation can be just returning true. The data source might not care about the mesage.
        /// </summary>
        /// <param name="message">The message from sync program to local address book</param>
        /// <returns>True if local address book allow to sync.</returns>
        bool BeginSync(string message);

        /// <summary>
        /// Notify local address book that the sync is finished.
        /// </summary>
        /// <param name="message"></param>
        void EndSync(string message, OperationStatus status);

        string GetItemName(string localId);

        /// <summary>
        /// Get error message from the provider after applying changes from the server.
        /// </summary>
        string ErrorMessage
        {
            get;
        }

        /// <summary>
        /// Deleted names after applying changes from the server.
        /// </summary>
        string NamesOfDeletedItems
        { get; }

        /// <summary>
        /// Added names in local source after applying changes from the server.
        /// </summary>
        string NamesOfAddedItems { get; }

        /// <summary>
        /// Updated names after applying changes from the server.
        /// </summary>
        string NamesOfUpdatedItems { get; }

        /// <summary>
        /// Return sync commands in manner of ISyncCommandsSource, in order to
        /// send modification to the server.
        /// </summary>
        /// <param name="lastAnchor"></param>
        /// <returns></returns>
        ISyncCommandsSource GenerateSyncCommandsSource(DateTime lastAnchorTime);

        /// <summary>
        /// Summary of changes to be sent to the server, after calling GenerateSyncCommandsSource.
        /// </summary>
        string SummaryOfGeneratedSyncCommands { get; }

        bool DeleteAllItems();
    }


}
