using System;
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json;



namespace HelloWorld
{
    class Program
    {
        static string Get(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using(HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using(Stream stream = response.GetResponseStream())
            using(StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
        public class Price
        {
            public string symbol { get; set; }
            public double price { get; set; }
        }
        static void Main(string[] args)
        {
            string url = "https://api1.binance.com/api/v3/ticker/price?symbol=BTCUSDT";
            for (int i = 0; ;i++ )
            {
                string site = Get(url);
                Price price = JsonConvert.DeserializeObject<Price>(site);
                Console.Write("\r" + price.price + "     ");
                System.Threading.Thread.Sleep(1000);
            }

        }
    }
}


