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
/*
            
            SP2013REST contextInfoRequest = new SP2013REST("http://edc.micron.com/mti/MEM002", "/_api/contextinfo");
            String contextInfoResponse = contextInfoRequest.executePost(null, null, null);

            // Deserialize JSON to Custom Object
            SP2013_ContextInfo.ContextInfo contextInfo = JsonConvert.DeserializeObject<SP2013_ContextInfo.ContextInfo>(contextInfoResponse);
            Console.WriteLine("{0}\n", contextInfo.d.GetContextWebInformation.FormDigestValue);

            //string prettyJson = SP2013REST.objectToJsonPretty(contextInfo);
            //string compactJson = SP2013REST.objectToJsonCompact(contextInfo);
            //Console.WriteLine("{0}\n", prettyJson);
            //Console.WriteLine("{0}\n", compactJson);
            //Console.WriteLine("{0}\n", SP2013REST.jsonPretty(compactJson));
*/

            try
            {
                SP2013REST webRequest = new SP2013REST("http://collab.micron.com/products/memorysolutions/NVE/NVESSD/SSDDEVWS",
                                                        "/_api/Web/Webs?$select=Title,Url,Id&$top=5",
                                                        false);
                String webResponse = webRequest.executeGet();

                SP2013_Webs.Webs webs = JsonConvert.DeserializeObject<SP2013_Webs.Webs>(webResponse);
                Console.WriteLine("\nShowing All Sub sites contained on web site. (top 5 for this example)");
                Console.WriteLine("==========================================================================");
                Console.WriteLine("{0,-20} {1,-40} {2}", "Title", "Id", "URL");
                foreach (SP2013_Webs.Result result in webs.d.results)
                {
                    Console.WriteLine("{0,-20} {1,-40} {2}", result.Title, result.Id, result.Url);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

//            Environment.Exit(0);

            // *****************************************************************************************************************************************

            try
            {
                SP2013REST webRequest = new SP2013REST("http://edc.micron.com/mti/MEM002",
                                                        "/_api/Web?$select=Title,Url",
                                                        false);
                String webResponse = webRequest.executeGet();

                // Deserialize JSON to Custom Object
                SP2013_Web.Web web = JsonConvert.DeserializeObject<SP2013_Web.Web>(webResponse);

                Console.WriteLine("\nShowing Top Web site");
                Console.WriteLine("================================");
                Console.WriteLine("{0,-35} {1}", "Title", "URL");
                Console.WriteLine("{0,-35} {1} ", web.d.Title, web.d.Url);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            // *****************************************************************************************************************************************
            try
            {
                SP2013REST webListsRequest = new SP2013REST("http://edc.micron.com/mti/MEM002",
                                                            "/_api/Web/Lists?$select=Title,ItemCount,BaseTemplate&$filter=BaseTemplate eq 101",
                                                            false);
                String webListsResponse = webListsRequest.executeGet();

                // Deserialize JSON to Custom Object
                SP2013_WebLists.WebLists webLists = JsonConvert.DeserializeObject<SP2013_WebLists.WebLists>(webListsResponse);

                Console.WriteLine("\nShowing All Lists contained on web site.");
                Console.WriteLine("=============================================");
                Console.WriteLine("{0,-35} {1,10} {2,12}", "Title", "ItemCount", "BaseTemplate");
                foreach (SP2013_WebLists.Result result in webLists.d.results)
                {
                    Console.WriteLine("{0,-35} {1,10} {2,12}", result.Title, result.ItemCount, result.BaseTemplate);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // *****************************************************************************************************************************************

            try
            {
                SP2013REST opSSDRequest = new SP2013REST("http://edc.micron.com/mti/MEM002",
                    "/_api/Web/Lists/GetByTitle('Operations SSD')?$select=Title,ItemCount");
                String opSSDResponse = opSSDRequest.executeGet();

                // Deserialize JSON to Custom Object
                SP2013_WebList.WebList webList = JsonConvert.DeserializeObject<SP2013_WebList.WebList>(opSSDResponse);

                Console.WriteLine("\nShowing how to find a list by Title, then get it's etag and uri.");
                Console.WriteLine("===================================================================");
                Console.WriteLine("{0,-35} {1,10} {2,12} {3}", "Title", "ItemCount", "eTag", "URI");
                Console.WriteLine("{0,-35} {1,10} {2,12} {3}", webList.d.Title, webList.d.ItemCount, webList.d.__metadata.etag, webList.d.__metadata.uri);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // *****************************************************************************************************************************************
            try
            {
                /*
                SP2013REST taxonomyRequest = new SP2013REST("http://edc.micron.com/mti/MEM002",
                    "/_api/Web/Lists/GetByTitle('TaxonomyHiddenList')/Items" +
                    "?$select=Title,Id");

                String taxonomyResponse = taxonomyRequest.executeGet();

                // Deserialize JSON to Custom Object
                SP2013_WebListItems.WebListItems taxonomyItems = JsonConvert.DeserializeObject<SP2013_WebListItems.WebListItems>(taxonomyResponse);

                Console.WriteLine("\n");
                foreach (SP2013_WebListItems.Result result in taxonomyItems.d.results)
                {
                    Console.WriteLine("{0} {1}", result.Id, result.Title);
                }
                Console.WriteLine("\n");
                */

               SP2013_CAMLQuery.CAMLQuery camlQuery = new SP2013_CAMLQuery.CAMLQuery();
               SP2013_CAMLQuery.Query query = new SP2013_CAMLQuery.Query();
               camlQuery.query = query;
               SP2013_CAMLQuery.Metadata metadata = new SP2013_CAMLQuery.Metadata();
               camlQuery.query.__metadata = metadata; 
               camlQuery.query.__metadata.type = "SP.CamlQuery";

               camlQuery.query.ViewXml = "<View><Query><OrderBy><FieldRef Name='Modified' Ascending='FALSE'/></OrderBy><Where><IsNotNull><FieldRef Name='EDC_MemoryArea'/></IsNotNull></Where></View></Query>";
               string jsonCaml = SP2013REST.objectToJsonPretty(camlQuery);

               
               SP2013REST opSSDItemsRequest = new SP2013REST("http://edc.micron.com/mti/MEM002",
                   "/_api/web/Lists/GetByTitle('Operations SSD')/GetItems?"  +
                   "$select=Id,Title,Created,Modified,EDC_MemoryArea,EDC_MemoryDocType,EDC_MicronProduct,File/ServerRelativeUrl&" +
                   "$expand=File"
               );

               String opSSDItemsResponse = opSSDItemsRequest.executePost(jsonCaml, null, null);

               // Deserialize JSON to Custom Object
               SP2013_WebListItems.WebListItems webListItems = JsonConvert.DeserializeObject<SP2013_WebListItems.WebListItems>(opSSDItemsResponse);

               Console.WriteLine("\nShow all files from EDC_MemoryArea");
               Console.WriteLine("==========================================");
               foreach (SP2013_WebListItems.Result result in webListItems.d.results)
               {
                   if (result.EDC_MemoryArea == null)
                   {
                       result.EDC_MemoryArea = new SP2013_WebListItems.EDC_MemoryArea();
                   }
                   if (result.EDC_MemoryDocType == null)
                   {
                       result.EDC_MemoryDocType = new SP2013_WebListItems.EDC_MemoryDocType();
                   }
                   if (result.EDC_MicronProduct == null)
                   {
                       result.EDC_MicronProduct = new SP2013_WebListItems.EDC_MicronProduct();
                   }
                   Console.Write("{0,8} ", result.ID);
                   Console.Write("{0} ", String.Format("{0:yyyy'-'MM'-'dd'T'HH':'MM':'ss'.'FFFzz}", result.Modified));
                   Console.Write("{0,-40} ", result.EDC_MemoryArea.Label);
                   Console.Write("{0} ", result.File.ServerRelativeUrl);
                   Console.WriteLine("");
               }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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

