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
        private bool debug = false;
        private HttpWebRequest httpWebRequest = null;

        public SP2013REST(string baseSPSite, string endpoint)
        {
            this.baseSPSite = baseSPSite;
            this.endpoint = baseSPSite + endpoint;
        }

        public SP2013REST(string baseSPSite, string endpoint, bool debug)
        {
            this.baseSPSite = baseSPSite;
            this.endpoint = baseSPSite + endpoint;
            this.debug = debug;
        }

        public String executeGet()
        {
            String jsonString = null;

            try
            {
                // SP2013 REST API endpoint
                this.httpWebRequest = this.getHttpWebRequestWithNTLMCredentials(this.endpoint);

                // HEADERS
                this.httpWebRequest.Method = "GET";
                this.httpWebRequest.Accept = "application/json;odata=verbose";
                this.httpWebRequest.Timeout = 60 * 1000; // GetResponse() timeout.

                if (this.debug)
                {
                    WebHeaderCollection webHeaderCollectionRequest = this.httpWebRequest.Headers;
                    string[] requestKeys = httpWebRequest.Headers.AllKeys;
                    Console.WriteLine();
                    foreach (var k in requestKeys)
                    {
                        Console.WriteLine("\t{0,-45} => {1}", k, webHeaderCollectionRequest.Get(k));
                    }
                }

                // RESPONSE
                HttpWebResponse httpWebResponse = (HttpWebResponse)this.httpWebRequest.GetResponse();
                jsonString = readResponse(httpWebResponse);

                Console.WriteLine("\nHttpWebRequest {0} => {1}", this.httpWebRequest.Method, this.endpoint);

                if (this.debug)
                {
                    WebHeaderCollection webHeaderCollectionResponse = httpWebResponse.Headers;
                    string[] responseKeys = httpWebResponse.Headers.AllKeys;

                    Console.WriteLine("\nHttpResponse => " + this.endpoint);
                    foreach (var k in responseKeys)
                    {
                        Console.WriteLine("\t{0,-45} => {1}", k, webHeaderCollectionResponse.Get(k));
                    }

                    Console.WriteLine(SP2013REST.jsonPretty(jsonString));
                }

            } catch(Exception ex) 
            {
                string msg = String.Format("\nERROR: HttpWebRequest {0} => {1}\n\n{2}", this.httpWebRequest.Method, this.endpoint, ex.Message);
                throw new SP2013Exception(msg);
            }
            return jsonString;
        }

        public String executePost(string jsonPayload, string httpMethodOverride, string etag)
        {
            String jsonString = null;

            try
            {
                // SP2013 REST API endpoint
                this.httpWebRequest = this.getHttpWebRequestWithNTLMCredentials(this.endpoint);

                // HEADERS
                this.httpWebRequest.Method = "POST";
                this.httpWebRequest.Accept = "application/json;odata=verbose";
                this.httpWebRequest.ContentType = "application/json;odata=verbose";
                this.httpWebRequest.Timeout = 60 * 1000; // GetResponse() timeout.

                // http://www.hanselman.com/blog/HTTPPUTOrDELETENotAllowedUseXHTTPMethodOverrideForYourRESTServiceWithASPNETWebAPI.aspx
                if (httpMethodOverride != null && Regex.IsMatch(httpMethodOverride, "PUT|MERGE|DELETE"))
                {
                    this.httpWebRequest.Headers.Add("X-HTTP-Method", httpMethodOverride);
                }

                if (jsonPayload == null)
                {
                    this.httpWebRequest.ContentLength = 0;
                }
                else
                {
                    this.httpWebRequest.ContentLength = jsonPayload.Length;
                    if (jsonPayload.Length > 0)
                    {
                        // Encode String jsonPayload to Byte[]
                        Byte[] jsonPostData = System.Text.Encoding.ASCII.GetBytes(jsonPayload);

                        // Get Stream for data json payload
                        Stream stream = this.httpWebRequest.GetRequestStream();
                        stream.Write(jsonPostData, 0, jsonPostData.Length);
                        stream.Close();
                    }
                }

                if (etag == null)
                {
                    this.httpWebRequest.Headers.Add("If-Match", "*");  // update or delete reguardless of etag.
                }
                else
                {
                    this.httpWebRequest.Headers.Add("If-Match", etag);  // update or delete only if same record.
                }

                if (httpMethodOverride != null && Regex.IsMatch(httpMethodOverride, "POST|PUT|MERGE|DELETE"))
                {
                    // All POST, PUT, DELETE, MERGE need this RequestDigest value.
                    this.httpWebRequest.Headers.Add("X-RequestDigest", this.getFormDigestValue());
                }


                if (this.debug)
                {
                    WebHeaderCollection webHeaderCollectionRequest = this.httpWebRequest.Headers;
                    string[] requestKeys = httpWebRequest.Headers.AllKeys;
                    Console.WriteLine();
                    foreach (var k in requestKeys)
                    {
                        Console.WriteLine("\t{0,-45} => {1}", k, webHeaderCollectionRequest.Get(k));
                    }
                    Console.WriteLine("JSON payload\n==================================\n");
                    Console.WriteLine(SP2013REST.jsonPretty(jsonPayload));
                    Console.WriteLine();
                }

                // RESPONSE
                HttpWebResponse httpWebResponse = (HttpWebResponse)this.httpWebRequest.GetResponse();
                jsonString = readResponse(httpWebResponse);

                Console.WriteLine("\nHttpWebRequest {0} X-Http-Method-Override: {1} => {2}",
                    this.httpWebRequest.Method, httpMethodOverride, this.endpoint);

                if (this.debug)
                {
                    WebHeaderCollection webHeaderCollectionResponse = httpWebResponse.Headers;
                    string[] responseKeys = httpWebResponse.Headers.AllKeys;

                    Console.WriteLine("\nHttpResponse => " + this.endpoint);
                    foreach (var k in responseKeys)
                    {
                        Console.WriteLine("\t{0,-45} => {1}", k, webHeaderCollectionResponse.Get(k));
                    }

                    Console.WriteLine(SP2013REST.jsonPretty(jsonString));
                }

            }
            catch (Exception ex)
            {
                string msg = String.Format("\nERROR: HttpWebRequest {0} X-Http-Method-Override:{1} => {2}\n\n{3}", 
                    this.httpWebRequest.Method, httpMethodOverride, this.endpoint, ex.Message);
                throw new SP2013Exception(msg);
            }

            return jsonString;
        }

        public static string jsonPretty(string jsonString)
        {
            var obj = JsonConvert.DeserializeObject<Object>(jsonString);
            // Serialize Custom Object back to JSON and indent  (pretty print)
            string formattedJsonString = JsonConvert.SerializeObject(obj, Formatting.Indented);
            return formattedJsonString;
        }

        public static string jsonCompact(string jsonString)
        {
            var obj = JsonConvert.DeserializeObject<Object>(jsonString);
            // Serialize Custom Object back to JSON and indent  (pretty print)
            string formattedJsonString = JsonConvert.SerializeObject(obj, Formatting.None);
            return formattedJsonString;
        }

        public static string objectToJsonPretty(Object obj)
        {
            // Serialize Custom Object back to JSON and indent  (pretty print)
            string formattedJsonString = JsonConvert.SerializeObject(obj, Formatting.Indented);
            return formattedJsonString;
        }

        public static string objectToJsonCompact(Object obj)
        {
            // Serialize Custom Object back to JSON and collapse  (compact print)
            string formattedJsonString = JsonConvert.SerializeObject(obj, Formatting.None);
            return formattedJsonString;
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

