using System;
using System.Collections.Generic;
using System.Text;
using Fonlow.SyncML.Common;
using System.Net;

namespace Fonlow.SyncML
{
    /// <summary>
    /// Connection to SyncML server.
    /// </summary>
    internal class SyncMLConnection : SyncServerConnection
    {
        protected SyncMLConnection(string uriLink, WebProxy proxy)
            : base(uriLink, proxy)
        {
            SetRequestType();
        }
        private void SetRequestType()
        {
            Request.Method = "POST";
            Request.ContentType = "application/vnd.syncml+xml; charset=UTF-8"; // the http binding document (1.1)of syncml has error, -xml?
            Request.Accept = "text/html, image/gif, image/jpeg, *; q=.2, */*; q=.2";
            Request.UserAgent = "SyncML Client for Open Contacts 1.0";

        }

        public static SyncMLConnection Create(string uriLink, WebProxy proxy)
        {
            if (String.IsNullOrEmpty(uriLink))
                return null;

            try
            {
                SyncMLConnection connection = new SyncMLConnection(uriLink, proxy);
                return connection;
            }
            catch (UriFormatException)
            {
                return null;
            }
            catch (InvalidCastException)
            {
                return null;
            }
        }
    }

    /// <summary>
    /// Provide a pool of connections different URI sharing the same URL but different sessions.
    /// The first URI generally has no session ID, and the following ones have with session ID returned from
    /// the response.
    /// </summary>
    public class SyncMLConnections
    {
        /// <summary>
        /// The value may be changed with different 
        /// </summary>
        public string Uri
        {
            get;
            set;
        }

        /// <summary>
        /// Make URI has the original uri defined in the constructor.
        /// </summary>
        public void ResetUri()
        {
            Uri = originalUri;
        }

        readonly string originalUri;

        public SyncMLConnections(string uriLink, WebProxy proxy)
        {
            Uri = uriLink;
            originalUri = uriLink;
            this.proxy = proxy;
        }

        private WebProxy proxy;
        protected WebProxy Proxy
        {
            get { return proxy; }
        }

        /// <summary>
        /// Post message to server and get response.
        /// </summary>
        /// <param name="data">Message to send.</param>
        /// <returns>Response from the server. Null if connection failed.</returns>
        public string GetResponseText(string data)
        {
            SyncMLConnection connection = SyncMLConnection.Create(Uri, Proxy);
            if (connection != null)
                return connection.GetResponseText(data);
            else
                return null;
        }

    }
}
