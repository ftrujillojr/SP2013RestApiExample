using Newtonsoft.Json;
using Microsoft.SharePoint.Client; 
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SP2013Example
{
    class SP2013REST
    {
        private string baseSPSite = null;
        private string endpoint = null;

        public SP2013REST(string baseSPSite, string endpoint)
        {
            this.baseSPSite = baseSPSite;
            this.endpoint = baseSPSite + endpoint;
        }

        public String executeGet()
        {
            // SP2013 REST API endpoint
            HttpWebRequest httpWebRequest = this.getHttpWebRequestWithNTLMCredentials(this.endpoint);
            // HEADERS
            httpWebRequest.Method = "GET";
            httpWebRequest.Accept = "application/json;odata=verbose";
            httpWebRequest.Timeout = 60 * 1000; // GetResponse() timeout.
            // RESPONSE
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            String jsonString = readResponse(httpWebResponse);
            return jsonString;
        }

        public String executePost(string jsonPayload)
        {
            // SP2013 REST API endpoint
            HttpWebRequest httpWebRequest = this.getHttpWebRequestWithNTLMCredentials(this.endpoint);

            // HEADERS
            httpWebRequest.Method = "POST";
            httpWebRequest.Accept = "application/json;odata=verbose";
            httpWebRequest.ContentLength = jsonPayload.Length;
            httpWebRequest.ContentType = "application/json;odata=verbose";
            httpWebRequest.Timeout = 60 * 1000; // GetResponse() timeout.
            httpWebRequest.Headers.Add("X-RequestDigest", this.getFormDigestValue());

            if (httpWebRequest.ContentLength > 0)
            {
                // Encode String jsonPayload to Byte[]
                Byte[] jsonPostData = System.Text.Encoding.ASCII.GetBytes(jsonPayload);

                // Get Stream for data json payload
                Stream stream = httpWebRequest.GetRequestStream();
                stream.Write(jsonPostData, 0, jsonPostData.Length);
                stream.Close();
            }

            // RESPONSE
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            String jsonString = readResponse(httpWebResponse);

            return jsonString;
        }


        private String getFormDigestValue()
        {
            // SP2013 REST API endpoint
            HttpWebRequest contextRequest = this.getHttpWebRequestWithNTLMCredentials(this.baseSPSite + "/_api/contextinfo");
            // HEADERS
            contextRequest.Method = "POST";
            contextRequest.Accept = "application/json;odata=verbose";
            contextRequest.ContentLength = 0;  // MUST 
            // RESPONSE
            HttpWebResponse contextResponse = (HttpWebResponse)contextRequest.GetResponse();
            String contextJson = readResponse(contextResponse);
            // Deserialize JSON to Custom Object
            SP2013_ContextInfo.ContextInfo contextInfo = JsonConvert.DeserializeObject<SP2013_ContextInfo.ContextInfo>(contextJson);
            String formDigestValue = contextInfo.d.GetContextWebInformation.FormDigestValue;
            return formDigestValue;
        }

        private String readResponse(HttpWebResponse httpWebResponse)
        {
            Stream postStream = httpWebResponse.GetResponseStream();
            StreamReader postReader = new StreamReader(postStream);
            String results = postReader.ReadToEnd();
            postReader.Close();
            postStream.Close();
            return results;
        }

        private HttpWebRequest getHttpWebRequestWithNTLMCredentials(string restUrl)
        {
            CredentialCache credCache = new CredentialCache();
            credCache.Add(new Uri(restUrl), "NTLM", CredentialCache.DefaultNetworkCredentials);
            HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(restUrl);
            httpWebRequest.Credentials = credCache;
            return httpWebRequest;
        }
    } // class SP2013REST


} // namespace SP2013Example

