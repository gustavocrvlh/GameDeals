using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

class Program
{
    public class Deal
    {
        public string Title { get; set; }
        public string StoreID { get; set; }
        public string SalePrice { get; set; }
        public string NormalPrice { get; set; }
        public string Savings { get; set; }
        public string DealRating { get; set; }
    }

    static async Task Main(string[] args)
    {
        string apiUrl = "https://www.cheapshark.com/api/1.0/deals";

        try
        {
            using (HttpClient client = new HttpClient())
            {
                Console.WriteLine("Getting data from CheapShark...");

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                string responseData = await response.Content.ReadAsStringAsync();

                List<Deal> deals = JsonConvert.DeserializeObject<List<Deal>>(responseData);

                Console.WriteLine("Promoções disponíveis:");
                Console.WriteLine(new string('-', 50));

                foreach (var deal in deals)
                {
                    Console.WriteLine($"Title: {deal.Title}");
                    Console.WriteLine($"Store: {deal.StoreID}");
                    Console.WriteLine($"Sale price: ${deal.SalePrice}");
                    Console.WriteLine($"Normal price: ${deal.NormalPrice}");
                    Console.WriteLine($"Savings: {deal.Savings}%");
                    Console.WriteLine($"Deal rating: {deal.DealRating}/10");
                    Console.WriteLine(new string('-', 50));
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error ocurred: {ex.Message}");
        }
    }
}