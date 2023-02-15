using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace post
{
    class postalCode
    {
        [JsonProperty("post code")]
        public string? postcode { get; set; }

        [JsonProperty("country")]
        public string? country { get; set; }

        [JsonProperty("country abbreviation")]
        public string? cabbreviation { get; set; }

    }

    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            await ProcessRepositories();
        }
        private static async Task ProcessRepositories()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter a zip code. Please enter without writing a name to quit the program.");
                    var zip = Console.ReadLine();
                    if (string.IsNullOrEmpty(zip))
                    {
                        break;
                    }
                    var result = await client.GetAsync("https://api.zippopotam.us/US/" + zip);
                    Console.WriteLine("https://api.zippopotam.us/US/" + zip);
                    
                    //Console.WriteLine("check1");
                    var resultRead = await result.Content.ReadAsStringAsync();
                    //Console.WriteLine("check2");
                    //not working
                    var zipc = JsonConvert.DeserializeObject<postalCode>(resultRead);
                    //Console.WriteLine("check3");
                    
                    Console.WriteLine("---");
                    Console.WriteLine("Zip Number: " + zipc.postcode);
                    Console.WriteLine("Country: " + zipc.country);
                    Console.WriteLine("Country Abbreviation: " + zipc.cabbreviation);
                    Console.WriteLine("The parsed stuff is: ",zipc);
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid Zip");
                }
            }

        }
    }
}
