using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ZadankoAPI;

class APICheck
{
    static async Task Main()
    {
        string nip;
        using (HttpClient client = new HttpClient())
        {
            Console.WriteLine("Wpisz NIP");
            nip = Console.ReadLine();
            string url = $"https://wl-api.mf.gov.pl/api/search/nip/{nip}?date=2025-07-25";

            try
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if(response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<Root>(json);
                    Console.WriteLine("Odpowiedź API:");
                    Console.WriteLine($"Nazwa:{data.Result.Subject.Name}");
                    Console.WriteLine($"NIP:{data.Result.Subject.Nip}");
                    Console.WriteLine($"REGON:{data.Result.Subject.Regon}");
                    Console.WriteLine($"Request_ID:{data.Result.RequestId}");
                }
            } 
            catch (Exception ex)
            {
                Console.WriteLine("Wystąpił błąd podczas zapytania do API:");
                Console.WriteLine(ex.Message);
            }
        }
        Console.WriteLine("Naciśnij dowolny klawisz, aby zakończyć...");
        Console.ReadKey();
    }
}
