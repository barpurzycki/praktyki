using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        string nip;
        string apiKey = "eyJraWQiOiJjZWlkZyIsImFsZyI6IkhTNTEyIn0.eyJnaXZlbl9uYW1lIjoiQmFydG9zeiIsInBlc2VsIjoiMDEyNDI3MDU2NzIiLCJpYXQiOjE3NTM0Mjg5ODksImZhbWlseV9uYW1lIjoiUHVyenlja2kiLCJjbGllbnRfaWQiOiJVU0VSLTAxMjQyNzA1NjcyLUJBUlRPU1otUFVSWllDS0kifQ.PBbXHjYz_rs9Jwqq5kQHy5X59sx_najs3lL8MLuljNwVsJ-YbqgUdHLTSs6X0lUInY4RfRIdFd1AskJ5agAp0A";
        

        using (HttpClient client = new HttpClient())
        {
            Console.WriteLine("Wpisz numer NIP");
            nip = Console.ReadLine();
            string url = "https://dane.biznes.gov.pl/api/ceidg/v3/firmy?nip=" +nip;
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", apiKey);

            try
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Odpowiedź API:");
                    Console.WriteLine(json);
                }
                else
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Błąd: {(int)response.StatusCode} - {response.ReasonPhrase}");
                    Console.WriteLine("Szczegóły błędu:");
                    Console.WriteLine(errorContent);
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
