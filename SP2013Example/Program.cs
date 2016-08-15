using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;  // added
using System.Net;                   // added
using System.IO;
using Newtonsoft.Json;                    // added

// Convert JSON to C# classes  http://jsonutils.com/

namespace SP2013Example
{
    class Program
    {
        static void Main(string[] args)
        {
            // SP2013 REST API endpoint
            HttpWebRequest contextRequest = getHttpWebRequestWithNTLMCredentials("http://edc.micron.com/mti/MEM002/_api/contextinfo");
            // HEADERS
            contextRequest.Method = "POST";
            contextRequest.Accept = "application/json;odata=verbose";
            contextRequest.ContentLength = 0;  // MUST 
            // RESPONSE
            HttpWebResponse contextResponse = (HttpWebResponse)contextRequest.GetResponse();
            String contextJson = readResponse(contextResponse);
            // Deserialize JSON to Custom Object
            SP2013_ContextInfo.ContextInfo contextInfo = JsonConvert.DeserializeObject<SP2013_ContextInfo.ContextInfo>(contextJson);
            Console.WriteLine("\nFormDigestValue => " + contextInfo.d.GetContextWebInformation.FormDigestValue + "\n");
            String newJson = JsonConvert.SerializeObject(contextInfo, Formatting.Indented);
            
            Console.WriteLine("JSON => " + newJson + "\n");

            // *****************************************************************************************************************************************

            // SP2013 REST API endpoint
            HttpWebRequest request1 = getHttpWebRequestWithNTLMCredentials("http://edc.micron.com/mti/MEM002/_api/web/lists?$filter=BaseTemplate eq 101");
            // HEADERS
            request1.Method = "GET";
            request1.Accept = "application/json;odata=verbose";
            // RESPONSE
            HttpWebResponse response1 = (HttpWebResponse)request1.GetResponse();
            String jsonResults1 = readResponse(response1);
            // Deserialize JSON to Custom Object
            SP2013_WebLists.WebLists webLists = JsonConvert.DeserializeObject<SP2013_WebLists.WebLists>(jsonResults1);

            Console.WriteLine("{0,-35} {1,10} {2,12} {3}", "Title", "ItemCount", "BaseTemplate", "URI");
            foreach (SP2013_WebLists.Result result in webLists.d.results)
            {
                Console.WriteLine("{0,-35} {1,10} {2,12} {3}", result.Title, result.ItemCount,  result.BaseTemplate, result.__metadata.uri);
            }

        }

        private static String readResponse(HttpWebResponse httpWebResponse)
        {
            Stream postStream = httpWebResponse.GetResponseStream();
            StreamReader postReader = new StreamReader(postStream);
            String results = postReader.ReadToEnd();

            postReader.Close();
            postStream.Close();
            return results;
        }

        private static HttpWebRequest getHttpWebRequestWithNTLMCredentials(string restUrl)
        {
            CredentialCache credCache = new CredentialCache();
            credCache.Add(new Uri(restUrl), "NTLM", CredentialCache.DefaultNetworkCredentials);
            HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(restUrl);
            httpWebRequest.Credentials = credCache;
            return httpWebRequest;
        }
    }
}


// Basic Auth
/*
string userName = "someuser";
string userPassword = "somepassword";
string authInfo = userName + ":" + userPassword;
authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
request.Headers.Add("Authorization", "Basic " + authInfo);
*/

// OAuth
/*
request.Headers.Add("Authorization", "Bearer " + accessToken);
*/

