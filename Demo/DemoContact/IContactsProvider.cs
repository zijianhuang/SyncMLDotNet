using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoContact
{
    public interface IContactsProvider
    {
        /// <summary>
        /// For change log xml from client address book
        /// </summary>
        /// <param name="lastAnchor">Last anchor.</param>
        /// <returns>Change log XML</returns>
        string GetChangeLogXml(DateTime lastAnchor);

        /// <summary>
        /// Apply change log to client address book, and return localID/serverID pair
        /// </summary>
        /// <param name="xml">Change log XML to update local address book</param>
        /// <returns>Each pair is in localID=ServerID format, separated by line break.</returns>
        string ApplyChangeLogXml(string xml);

        /// <summary>
        /// Obtain sync permission from local address book.
        /// </summary>
        /// <param name="message">The message from sync program to local address book</param>
        /// <returns>True if local address book allow to sync.</returns>
        bool BeginSync(string message);

        /// <summary>
        /// Notify local address book that the sync is finished.
        /// </summary>
        /// <param name="message"></param>
        void EndSync(string message);

        string GetContactName(string localId);

        /// <summary>
        /// Get error message from the provider.
        /// </summary>
        string ErrorMessage
        {
            get;
        }

        string DeletedNames
        { get; }

        string AddedNames { get; }

        string UpdatedNames { get; }

        bool DeleteAllContacts();
    }
}
