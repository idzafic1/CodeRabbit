using System;
using System.Linq;


public class ErrorHandlingAndRobustness
{
    // GREŠKA: Neispravan tip izuzetka. Trebao bi biti bačen specifičniji izuzetak poput 'ArgumentNullException'
    // ili 'ArgumentOutOfRangeException' umjesto generičkog 'Exception'.
    public void SetAge(int age)
    {
        if (age < 0)
        {
            throw new Exception("Age cannot be negative.");
        }
    }

    // GREŠKA: Ne rukuje se graničnim slučajevima. Ova funkcija će baciti 'InvalidOperationException'
    // ako je niz 'numbers' prazan, jer se 'Average()' ne može pozvati na praznu sekvencu.
    public double CalculateAverage(int[] numbers)
    {
        return numbers.Average();
    }

    // GREŠKA: Nepouzdano poređenje brojeva s pokretnim zarezom.
    // Poređenje dvaju double/float vrijednosti za tačnu jednakost je opasno zbog grešaka u preciznosti.
    // Ovo poređenje može neočekivano zakazati.
    public bool IsValueOne(double value)
    {
        // Na primjer, ako je value rezultat 0.1 + 0.1 + ... (10 puta),
        // može biti 0.9999999999999999, a ne tačno 1.0.
        return value == 1.0;
    }

    // GREŠKA: Preklapanje imena. Ime klase 'File' se poklapa sa sistemskom 'System.IO.File'.
    // Ovo može dovesti do zabune i grešaka ako je programer namjeravao koristiti sistemsku klasu.
    public class File
    {
        public string Name { get; set; }
    }

    public void ProcessFile(string fileName)
    {
        // Programer bi mogao pomisliti da kreira instancu naše klase 'File',
        // ali ako napiše 'System.IO.File.Create(fileName)', slično imenovanje je zbunjujuće.
        var myFile = new File { Name = fileName };
        Console.WriteLine(myFile.Name);
    }
}
