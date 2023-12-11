using System;
using System.Net.Http.Headers;
using System.Text;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CSHttpClientSample
{


    public class Rootobject
    {
        public Link[] links { get; set; }
        public Item[] items { get; set; }
        public Searchspecification searchSpecification { get; set; }
        public int totalRows { get; set; }
    }

    public class Searchspecification
    {
        public int rowCount { get; set; }
        public string sort { get; set; }
        public string order { get; set; }
        public int startRow { get; set; }
        public bool isActiveMetadata { get; set; }
        public string[] fields { get; set; }
        public Filter[] filters { get; set; }
    }

    public class Filter
    {
        public string datetime_beginning_ept { get; set; }
    }

    public class Link
    {
        public string rel { get; set; }
        public string href { get; set; }
    }

    public class Item
    {
        public DateTime datetime_beginning_utc { get; set; }
        public DateTime datetime_beginning_ept { get; set; }
        public Int64 pnode_id { get; set; }
        public string pnode_name { get; set; }
        public string type { get; set; }
        public float total_lmp_rt { get; set; }
        public float congestion_price_rt { get; set; }
        public float marginal_loss_price_rt { get; set; }
        public bool occ_check { get; set; }
        public bool ref_caseid_used_multi_interval { get; set; }
    }

    static class Program
    {
        static void Main()
        {
            double result = GetRealTimeLMP("pjm");
            string color = "bronze";
            if (result < 40.0)
                color = "bronze";
            else if ((result >= 40.0) && (result < 70.0))
                color = "silver";
            else if (result >= 70.0)
                color = "gold";

            Console.WriteLine("Hit ENTER to exit...");
            Console.WriteLine(result.ToString());
            Console.WriteLine(color); 
            Console.ReadLine();
        }

        static double GetRealTimeLMP(string rto)
        {
            double result = 0.0;
            switch (rto)
            {
                case "pjm":
                    result = GetPJMRealTimeLMP().Result;
                    break;
                case "neiso":
                    result = 0.0;
                    break;
                case "caiso":
                    result = 0.0;
                    break;
                case "miso":
                    result = 0.0;
                    break;
                default:
                    result = 0.0;
                    break;
            }
            return result;
        }


        static async Task<double> GetPJMRealTimeLMP()
        {
            double lmp = 0.0;
            var httpClient = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            //get subscription key
            //get pnodeID

            // Request headers
            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "312249d38ae6410bbd6ea56f8343eef8");

            // Request parameters
            queryString["pnode_id"] = "2156113753";
            //queryString["download"] = "{boolean}";
            queryString["rowCount"] = "1";
            //queryString["sort"] = "{string}";
            //queryString["order"] = "{string}";
            queryString["startRow"] = "1";
            //queryString["isActiveMetadata"] = "{boolean}";
            //queryString["fields"] = "{string}";
            //queryString["case_approval_datetime_utc"] = "{string}";
            //queryString["case_approval_datetime_ept"] = "{string}";
            //queryString["datetime_beginning_utc"] = "{string}";
            queryString["datetime_beginning_ept"] = "5MinutesAgo";
            var uri = "https://api.pjm.com/api/v1/rt_unverified_fivemin_lmps?" + queryString;

            try
            {
                HttpResponseMessage httpResponse = await httpClient.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead);
                httpResponse.EnsureSuccessStatusCode(); // throws if not 200-299
                var contentStream = await httpResponse.Content.ReadAsStringAsync();

                Console.WriteLine(contentStream);

                Rootobject myRootObject = JsonConvert.DeserializeObject<Rootobject>(contentStream);
                Console.WriteLine(myRootObject.totalRows.ToString());
                int numOfRows = myRootObject.totalRows;
                if (numOfRows > 0)
                {
                    Console.WriteLine(myRootObject.items[0].datetime_beginning_ept.ToString());
                    Console.WriteLine(myRootObject.items[0].total_lmp_rt.ToString());
                    lmp = myRootObject.items[0].total_lmp_rt;
                }
                else
                    Console.WriteLine("Zero Rows Returned");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR");
                Console.WriteLine(ex.ToString());
            }
            return lmp;
        }
    }
}
    

    //static async void MakeRequest1()
    //    {
    //        var httpClient = new HttpClient();
    //        var queryString = HttpUtility.ParseQueryString(string.Empty);

    //        // Request headers
    //        httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "312249d38ae6410bbd6ea56f8343eef8");
    //        // Request parameters
    //        //queryString["download"] = "{boolean}";
    //        queryString["rowCount"] = "1";
    //        //queryString["sort"] = "{string}";
    //        //queryString["order"] = "{string}";
    //        queryString["startRow"] = "1";
    //        //queryString["isActiveMetadata"] = "{boolean}";
    //        //queryString["fields"] = "{string}";
    //        //queryString["case_approval_datetime_utc"] = "{string}";
    //        //queryString["case_approval_datetime_ept"] = "{string}";
    //        //queryString["datetime_beginning_utc"] = "{string}";
    //        queryString["datetime_beginning_ept"] = "5MinutesAgo";
    //        queryString["pnode_id"] = "2156113753";
    //        //var uri = "https://api.pjm.com/api/v1/five_min_itsced_lmps?" + queryString;
    //        var uri = "https://api.pjm.com/api/v1/rt_unverified_fivemin_lmps?" + queryString;

    //        try
    //        {
    //            HttpResponseMessage httpResponse = await httpClient.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead);
                

    //            httpResponse.EnsureSuccessStatusCode(); // throws if not 200-299

    //            var contentStream = await httpResponse.Content.ReadAsStringAsync();
    //            Console.WriteLine(contentStream);

    //           // var streamReader = new StreamReader(contentStream);
//                //var jsonReader = new JsonTextReader(streamReader);
//                //string responseFromServer = streamReader.ReadToEnd();
//                Rootobject myRootObject = JsonConvert.DeserializeObject<Rootobject>(contentStream);
//                //JsonSerializer serializer = new JsonSerializer();
//                Console.WriteLine(myRootObject.totalRows.ToString());
//                int numOfRows = myRootObject.totalRows;
//                for (int i = 0; i < 1; i++)
//                {
//                    Console.WriteLine(myRootObject.items[i].datetime_beginning_ept.ToString());
//                    Console.WriteLine(myRootObject.items[i].total_lmp_rt.ToString());
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine("ERROR");
//                Console.WriteLine(ex.ToString());
//            }
//        }

//    }
//}


        //    else
        //        {
        //            Log2.Trace("SUCCESS");
        //            // Get the stream containing content returned by the server.
        //            // The using block ensures the stream is automatically closed.
        //            using (Stream dataStream = ThingsSpeakResp.GetResponseStream())
        //            {
        //                // Open the stream using a StreamReader for easy access.
        //                StreamReader reader = new StreamReader(dataStream);
        //                // Read the content.
        //                responseFromServer = reader.ReadToEnd();
        //                //Log2.Trace(responseFromServer);
        //            }
        //            // Display the content.

        //            Log2.Trace("Response = {0}", responseFromServer);
        //            string myString;
        //            RootObject myRootObject = JsonConvert.DeserializeObject<RootObject>(responseFromServer);

        //            myString = myRootObject.channel.id.ToString();
        //            Log2.Trace("LATEST ID = {0}", myString);

        //            myString = myRootObject.channel.latitude;
        //            Log2.Trace("LATEST LAT = {0}", myString);


        //            myString = myRootObject.channel.longitude;
        //            Log2.Trace("LATEST LONG = {0}", myString);

        //            myString = myRootObject.channel.updated_at;
        //            Log2.Trace("LATEST Upd At = {0}", myString);

        //            myString = myRootObject.channel.last_entry_id.ToString();
        //            Log2.Trace("LATEST LAST ENTRY ID = {0}", myString);

        //            myString = myRootObject.channel.created_at;
        //            Log2.Trace("LATEST CREATED AT = {0}", myString);

        //            myString = myRootObject.channel.field1;
        //            Log2.Trace("LATEST FIELD1 = {0}", myString);

        //            myString = myRootObject.channel.name;
        //            Log2.Trace("LATEST NAME = {0}", myString);

        //            // Find latest value
        //            string s;
        //            for (int i = 19; i >= 0; i--)
        //            {
        //                //get the last good value from the return feed
        //                s = myRootObject.feeds[i].field1;
        //                if (s != null)
        //                {
        //                    dataVariable.Value = myRootObject.feeds[i].field1;

        //                    Log2.Trace("FOUND LATEST VALUE = {0}", dataVariable.Value);

        //                    try
        //                    {
        //                        myString = myRootObject.feeds[i].created_at;
        //                        Log2.Trace("LATEST JSON TIME = " + myString);
        //                        DateTime time = DateTime.Parse(myString);
        //                        Log2.Trace("PARSED TIME = {0}", time.ToString());

        //                        dataVariable.UpdateTime = time;
        //                        dataVariable.ServerTime = DateTime.Now;
        //                        dataVariable.Status = "GOOD";
        //                        dataVariable.Quality = "GOOD";
        //                        dataVariable.ExternalName = myRootObject.channel.field1;
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        Log2.Error("DateTime Parse Exception = {0}", ex.Message);
        //                    }
        //                    break;
        //                }
        //            }
        //        }
            
            
        //}
           

//private static async Task<User> StreamWithNewtonsoftJson(string uri, HttpClient httpClient)
//{
//    using var httpResponse = await httpClient.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead);

//    httpResponse.EnsureSuccessStatusCode(); // throws if not 200-299

//    if (httpResponse.Content is object && httpResponse.Content.Headers.ContentType.MediaType == "application/json")
//    {
//        var contentStream = await httpResponse.Content.ReadAsStreamAsync();

//        using var streamReader = new StreamReader(contentStream);
//        using var jsonReader = new JsonTextReader(streamReader);

//        JsonSerializer serializer = new JsonSerializer();

//        try
//        {
//            return serializer.Deserialize<User>(jsonReader);
//        }
//        catch (JsonReaderException)
//        {
//            Console.WriteLine("Invalid JSON.");
//        }
//    }
//    else
//    {
//        Console.WriteLine("HTTP Response was invalid and cannot be deserialised.");
//    }

//    return null;
//}


