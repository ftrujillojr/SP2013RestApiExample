using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;  // added
using System.Net;                   // added
using System.IO;                    // added

// Convert JSON to C#  http://jsonutils.com/

namespace SP2013Example
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpWebRequest contextRequest = getHttpWebRequestWithNTLMCredentials("http://edc.micron.com/mti/MEM002/_api/contextinfo");
            contextRequest.Method = "POST";
            contextRequest.Accept = "application/json;odata=verbose";
            contextRequest.ContentLength = 0;  // MUST 
            HttpWebResponse contextResponse = (HttpWebResponse)contextRequest.GetResponse();
            String contextJson = getJsonFromResponse(contextResponse);



            HttpWebRequest request1 = getHttpWebRequestWithNTLMCredentials("http://edc.micron.com/mti/MEM002/_api/web/lists?$filter=BaseTemplate eq 101");
            request1.Method = "GET";
            request1.Accept = "application/json;odata=verbose";
            HttpWebResponse response1 = (HttpWebResponse)request1.GetResponse();
            String jsonResults1 = getJsonFromResponse(response1);

            SP2013_Web_Lists.WebLists webLists = Newtonsoft.Json.JsonConvert.DeserializeObject<SP2013_Web_Lists.WebLists>(jsonResults1);

            Console.WriteLine("{0,-35} {1,10} {2,12} {3}", "Title", "ItemCount", "BaseTemplate", "URI");
            foreach (SP2013_Web_Lists.Result result in webLists.d.results)
            {
                Console.WriteLine("{0,-35} {1,10} {2,12} {3}", result.Title, result.ItemCount,  result.BaseTemplate, result.__metadata.uri);
            }
#if DEBUG
            Console.WriteLine("Press enter to close...");
            Console.ReadLine();
#endif
        }



        private static String getJsonFromResponse(HttpWebResponse httpWebResponse)
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

        private static String getFormDigest(string spUrl)
        {
            // 1st request to get the context information
            string restUrl =  spUrl + "/_api/contextinfo";

            HttpWebRequest spRequestNTLM = getHttpWebRequestWithNTLMCredentials(restUrl);
            // stuff the headers
            spRequestNTLM.Method = "POST";
            spRequestNTLM.Accept = "application/json;odata=verbose";
            spRequestNTLM.ContentLength = 0; // MUST do this

            HttpWebResponse httpWebResponse = (HttpWebResponse)spRequestNTLM.GetResponse();
            string jsonResults = getJsonFromResponse(httpWebResponse);

//            Console.WriteLine(jsonResults);

            // Get the FormDigest Value
            var startTag = "FormDigestValue";
            var endTag = "LibraryVersion";
            var startTagIndex = jsonResults.IndexOf(startTag) + 1;
            var endTagIndex = jsonResults.IndexOf(endTag, startTagIndex);
            string newFormDigest = null;
            if ((startTagIndex >= 0) && (endTagIndex > startTagIndex))
            {
                newFormDigest = jsonResults.Substring(startTagIndex + startTag.Length + 2, endTagIndex - startTagIndex - startTag.Length - 5);
            }

            return newFormDigest;
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

