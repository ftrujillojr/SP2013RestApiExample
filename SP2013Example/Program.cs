using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;  // added
using System.Net;                   // added
using System.IO;
using Newtonsoft.Json;              // added

// Convert JSON to C# classes  http://jsonutils.com/

namespace SP2013Example
{
    class Program
    {
        static void Main(string[] args)
        {
            SP2013REST contextInfoRequest = new SP2013REST("http://edc.micron.com/mti/MEM002", "/_api/contextinfo");
            String contextInfoResponse = contextInfoRequest.executePost(null, null, null);

            // Deserialize JSON to Custom Object
            SP2013_ContextInfo.ContextInfo contextInfo = JsonConvert.DeserializeObject<SP2013_ContextInfo.ContextInfo>(contextInfoResponse);
            Console.WriteLine("{0}\n", contextInfo.d.GetContextWebInformation.FormDigestValue);

            string prettyJson = SP2013REST.objectToJsonPretty(contextInfo);
            //string compactJson = SP2013REST.objectToJsonCompact(contextInfo);
            Console.WriteLine("{0}\n", prettyJson);
            //Console.WriteLine("{0}\n", compactJson);
            //Console.WriteLine("{0}\n", SP2013REST.jsonPretty(compactJson));

            // *****************************************************************************************************************************************

            SP2013REST webListsRequest = new SP2013REST("http://edc.micron.com/mti/MEM002", "/_api/Web/Lists?$filter=BaseTemplate eq 101");
            String webListsResponse = webListsRequest.executeGet();

            // Deserialize JSON to Custom Object
            SP2013_WebLists.WebLists webLists = JsonConvert.DeserializeObject<SP2013_WebLists.WebLists>(webListsResponse);

            Console.WriteLine("\n{0,-35} {1,10} {2,12} {3}", "Title", "ItemCount", "BaseTemplate", "URI");
            foreach (SP2013_WebLists.Result result in webLists.d.results)
            {
                Console.WriteLine("{0,-35} {1,10} {2,12} {3}", result.Title, result.ItemCount, result.BaseTemplate, result.__metadata.uri);
            }

            // *****************************************************************************************************************************************

            SP2013REST opSSDRequest = new SP2013REST("http://edc.micron.com/mti/MEM002", "/_api/Web/Lists/GetByTitle('Operations SSD')");
            String opSSDResponse = opSSDRequest.executeGet();

            // Deserialize JSON to Custom Object
            SP2013_WebList.WebList webList = JsonConvert.DeserializeObject<SP2013_WebList.WebList>(opSSDResponse);

            Console.WriteLine("\n{0,-35} {1,10} {2,12} {3}", "Title", "ItemCount", "eTag", "URI");
            Console.WriteLine("{0,-35} {1,10} {2,12} {3}", webList.d.Title, webList.d.ItemCount, webList.d.__metadata.etag, webList.d.__metadata.uri);

            // *****************************************************************************************************************************************

            SP2013REST opSSDItemsRequest = new SP2013REST("http://edc.micron.com/mti/MEM002", "/_api/Web/Lists/GetByTitle('Operations SSD')/Items?$select=Id,Title,Created,Modified,File/Name,File/Title,File/ServerRelativeUrl&$expand=File");
            String opSSDItemsResponse = opSSDItemsRequest.executeGet();

            // Deserialize JSON to Custom Object
            SP2013_WebListItems.WebListItems webListItems = JsonConvert.DeserializeObject<SP2013_WebListItems.WebListItems>(opSSDItemsResponse);

            Console.WriteLine("\n");
            foreach (SP2013_WebListItems.Result result in webListItems.d.results)
            {
                Console.Write("{0,8} ", result.ID);
                Console.Write("{0} ", String.Format("{0:yyyy'-'MM'-'dd'T'HH':'MM':'ss'.'FFFzz}", result.Modified));
                Console.Write("{0} ", result.File.ServerRelativeUrl);
                //                Console.Write("{0,25} ", result.Created);
//                Console.Write("{0,25} ", result.Modified);
//                Console.Write("{0} ", result.EDC_MemoryArea);
//                Console.Write("{0} ", result.EDC_MemoryDocType);
//                Console.Write("{0} ", result.EDC_MicronProduct);
                Console.WriteLine("");
            }

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

