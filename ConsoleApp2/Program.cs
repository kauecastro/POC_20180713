using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Attempt to get the catalog ...");
            CatalogItems catalog = GetCatalog("http://www.rtve.es/alacarta/interno/buscador");
            List<DetailedItem> detailedCatalog = new List<DetailedItem>();
            for(int i = 0; i < catalog.Items.Count; i++)
            {
                Console.WriteLine("Item " + (i + 1) + "/" + catalog.Items.Count);
                var detailedItem = GetDetailedItem("http://www.rtve.es/api/programas/" + catalog.Items[i].id + "/");
                detailedCatalog.Add(detailedItem);
            }

            Console.WriteLine("Ok");
            string path = @"C:\temp\file"; // path to file
            using (FileStream fs = File.Create(path))
            {
                // writing data in string
                string dataasstring = Newtonsoft.Json.JsonConvert.SerializeObject(detailedCatalog); //your data
                byte[] info = new UTF8Encoding(true).GetBytes(dataasstring);
                fs.Write(info, 0, info.Length);
            }

            Console.ReadKey();
        }

        static CatalogItems GetCatalog(string url)
        {
            var content = GetRequest(url);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<CatalogItems>(content);
        }

        static DetailedItem GetDetailedItem(string url)
        {
            var content = GetRequest(url);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<DetailedItem>(content);
        }

        static string GetRequest(string url)
        {

            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = client.GetAsync(url).Result)
                {
                    using (HttpContent content = response.Content)
                    {
                        return content.ReadAsStringAsync().Result;
                    }
                }
            }
        }
    }
}
