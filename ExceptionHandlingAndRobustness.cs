using System;
using System.IO;

public class ExceptionHandlingIssues
{
    // GREŠKA: Bacanje previše općenite vrste izuzetka.
    // Trebalo bi baciti specifičniji izuzetak poput ArgumentNullException.
    public void ProcessName(string name)
    {
        if (name == null)
        {
            throw new Exception("Name cannot be null.");
        }
    }

    // GREŠKA: Prazan blok catch (gutanje izuzetka).
    // Ovo skriva originalnu grešku, što čini otklanjanje grešaka izuzetno teškim.
    public void ReadFile()
    {
        try
        {
            File.ReadAllText("non_existent_file.txt");
        }
        catch (IOException)
        {
            // Izuzetak je uhvaćen, ali ništa se ne radi. Program nastavlja kao da se ništa nije dogodilo.
        }
    }

    // GREŠKA: Neispravno ponovno bacanje izuzetka, što resetuje stack trace.
    public void IncorrectRethrow()
    {
        try
        {
            int.Parse("not-a-number");
        }
        catch (FormatException ex)
        {
            Console.WriteLine("Logging error...");
            // Ovo 'throw ex' gubi informacije o originalnom stack traceu.
            // Ispravan način je koristiti 'throw;'.
            throw ex;
        }
    }

    // GREŠKA: Neuspjeh u rukovanju graničnim slučajevima (null unos).
    // Ova metoda će baciti NullReferenceException ako je 'items' null.
    public int GetItemCount(string[] items)
    {
        // Nema provjere 'if (items == null)'.
        return items.Length;
    }
}
