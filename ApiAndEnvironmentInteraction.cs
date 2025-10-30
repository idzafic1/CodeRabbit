using System;
using System.Threading.Tasks;

// Ova using naredba stvara mogućnost zasjenjivanja imena.
// Linter bi to trebao označiti kao zbunjujuće
using File = System.IO.File;

namespace MyProject
{
    // GREŠKA: Zasjenjivanje imena. Ova klasa 'File' ima isto ime kao 'System.IO.File'.
    // Ovo može dovesti do zabune i grešaka ako programer slučajno koristi pogrešnu.
    public class File
    {
        public string Content { get; set; }
    }

    public class EnvironmentIssues
    {
        public void ProcessFile()
        {
            var content = System.IO.File.ReadAllText("data.txt");
        }

        // GREŠKA: Problem vremenske zone. Korišćenje 'DateTime.Now' biljleži lokalno vrijeme servera.
        // Ovo uzrokuje velike probleme za aplikacije koje se koriste u različitim vremenskim zonama.
        public void RecordPurchase()
        {
            var purchaseRecord = new
            {
                Timestamp = DateTime.Now, // Ovo bi trebalo biti DateTime.UtcNow
                Amount = 99.99
            };
            Console.WriteLine($"Purchase recorded at: {purchaseRecord.Timestamp}");
        }
    }

    // Ova klasa predstavlja jednostavan API kontroler.
    // GREŠKA: Nedostatak verzionisanja API-ja.
    // Ako promijenimo 'Submit' metodu (npr. promijenimo tip parametra), svi klijenti će prestati da rade.
    // Robustan API bi koristio shemu verzionisanja (npr. "/api/v1/data").
    public class ApiController
    {
        public async Task<string> Submit(string data)
        {
            // U pravom API-ju, ovde su opasne promene bez verzionisanja.
            return await Task.FromResult("Data processed successfully");
        }
    }
}
