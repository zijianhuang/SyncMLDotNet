using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.ComponentModel;
using System.IO;
using System.Diagnostics;

namespace Fonlow.SyncML.Common
{
    /// <summary>
    /// Send web request through POST
    /// </summary>
    public class SyncServerConnection
    {

        private HttpWebRequest request;
        protected HttpWebRequest Request { get { return request; } }

        private SyncServerConnection() { }

        public SyncServerConnection(string uriLink, WebProxy proxy)
        {
            Uri uri = new Uri(uriLink);
            request = (HttpWebRequest)WebRequest.Create(uri);
            request.Timeout = 300000; // 5 minutes rather default 100 seconds, in case the server needs time to return large volume of data.
            if (proxy != null)
                request.Proxy = proxy;

            request.ServicePoint.Expect100Continue = false; // some stupid ISP handle Expect100Continue badly, so need to turn it off.
            SetRequestType();// virtual. I think this is safe. Just different way to initialize httpWReq in the base class.
        }

        private CookieCollection cookies;

        public CookieCollection Cookies
        {
            get { return cookies; }
        }


        private void SetRequestType()
        {
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded"; // without this the server will think this is WebClient.UploadString
        }


        /// <summary>
        /// Post data and get reponse text.
        /// </summary>
        /// <param name="data">Data to post.</param>
        /// <returns>Text to receive from sponse.</returns>
        public virtual string GetResponseText(string data)
        {
            request.CookieContainer = new CookieContainer();
            if (cookies != null)
                request.CookieContainer.Add(cookies);

            Debug.WriteLine("Send Header:" + request.Headers.ToString());
         //   Debug.WriteLine("Sending data: " + data);

            byte[] postData = Encoding.UTF8.GetBytes(data);
            request.ContentLength = postData.Length;

            try
            {
                using (Stream postStream = request.GetRequestStream())
                {
                    postStream.Write(postData, 0, postData.Length);
                }

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    cookies = response.Cookies;
                    // Get the stream associated with the response.
                    Stream receiveStream = response.GetResponseStream();

                    Debug.WriteLine("Response status code:" + response.StatusCode);
                    Debug.WriteLine("Response headers:" + response.Headers.ToString());

                    // Pipes the stream to a higher level stream reader with the required encoding format.
                    StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                    string r = readStream.ReadToEnd();
                    return r;
                }
            }
            catch (WebException ex)
            {
                Debug.WriteLine("Something wrong in server response:");
                if (ex.Response != null)
                {
                    Debug.WriteLine("Headers: " + ex.Response.Headers.ToString());
                    Debug.WriteLine("Uri: " + ex.Response.ResponseUri);
                    Debug.WriteLine("The response is " + (ex.Response.IsFromCache ? String.Empty : " not ") + "from cache.");
                }
                return ex.Message;
            }

        }

    }
}
