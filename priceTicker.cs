using System;
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json;



namespace priceTick
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
            public float price { get; set; }
        }
        static void Main(string[] args)
        {
            string url = "https://api1.binance.com/api/v3/ticker/price?symbol=BTCUSDT";
            float lastPrice = 0;
            for (int i = 0; ;i++ )
            {
                string site = Get(url);
                Price price = JsonConvert.DeserializeObject<Price>(site);
                float priceChange = price.price - lastPrice;
                lastPrice = price.price;
                Console.WriteLine("\rCurrent price: " + price.price + "   priceChange: " + priceChange.ToString("0.00"));
                System.Threading.Thread.Sleep(1000);
            }

        }
    }
}


