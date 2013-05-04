using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.IO;

namespace Fonlow.SyncML.Common
{
    /// <summary>
    /// Access messages persisted as Code=Message.
    /// </summary>
    public class MessagesInPairs
    {
        /// <summary>
        /// Return message according to code.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string GetMessage(string code)
        {
            return statusCodeMessages[code];
        }

        /// <summary>
        /// Load message from a string with code:message pairs.
        /// </summary>
        /// <param name="textOfStatusMessagePairs">Text in format code:message</param>
        /// <returns></returns>
        public void LoadMessages(string textOfStatusMessagePairs)
        {
            statusCodeMessages = new NameValueCollection();
            if (!String.IsNullOrEmpty(textOfStatusMessagePairs))
            {
                string[] lines = textOfStatusMessagePairs.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string s in lines)
                {
                    string[] nv = s.Split(new char[] { '=' });
                    if (!String.IsNullOrEmpty(s.Trim()))
                        statusCodeMessages.Add(nv[0], nv[1]);
                }
            }
        }
        /// <summary>
        /// Buffer to store messages. NameValueCollection use hash index, good enough.
        /// </summary>
        private NameValueCollection statusCodeMessages;
       
    }
}
