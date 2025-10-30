// komentar

using System;
using System.Net.Http;

// GREŠKA (Linter): Neiskorištene 'using' izjave. Treba ih ukloniti.
using System.Collections.Generic;
using Microsoft.VisualBasic;

#nullable enable

public class DesignIssues
{
    // GREŠKA: Prekršaj pravila "Ne ponavljaj se" (DRY). Logika za izračun ukupne cijene je duplicirana.
    public decimal CalculateStandardUserPrice(decimal price)
    {
        decimal tax = price * 0.20m; // Duplicirana logika
        return price + tax;
    }

    public decimal CalculatePremiumUserPrice(decimal price)
    {
        decimal tax = price * 0.20m; // Duplicirana logika
        decimal discount = price * 0.10m;
        return price + tax - discount;
    }
    
    // GREŠKA: Zavaravajuće ime metode. Ime sugeriše da dobavlja korisnika, ali također mijenja stanje.
    public string GetUserAndLogAccess(int userId)
    {
        Console.WriteLine($"Logging access for user {userId}"); // Ovo je sporedni efekat.
        return $"User_{userId}";
    }
}

// GREŠKA: Kršenje principa jedinstvene odgovornosti (SRP). Ova klasa radi previše.
public class UserProcessor
{
    public void ProcessUser(string username)
    {
        // 1. Logika validacije
        if (string.IsNullOrEmpty(username))
        {
            throw new ArgumentException("Username is required.");
        }
        
        // 2. Logika baze podataka
        Console.WriteLine("Saving user to the database...");

        // 3. Logika HTTP klijenta
        var client = new HttpClient();
        client.PostAsync("http://api.example.com/notify", new StringContent("user processed"));
    }
}


public class NullabilityIssues
{
    // GREŠKA: Potencijalno dereferenciranje null vrijednosti. Parametar 'name' može biti null, a dereferencira se bez provjere.
    // Dobar analizator će ovo označiti ako su omogućeni nullable reference types.
    public int GetNameLength(string? name)
    {
        return name.Length; // Upozorenje: Dereferenciranje moguće null reference.
    }

    public void Process(string? message)
    {
        // Ovo je ispravan način da se to obradi.
        if (message != null)
        {
            Console.WriteLine(message.ToUpper());
        }
    }
}

// --- GREŠKE LINTERA --
public class Linting{
        // GREŠKA (Linter): Kršenje pravila imenovanja. Trebalo bi biti 'testVariable'.
        // GREŠKA (Linter): Neusklađena uvlačenja.
        const int testVari_able = 13;
        
        // GREŠKA (Linter): Neusklađeni razmaci prije definicije metode.
        public double loopWhileTest()
        {
            // GREŠKA (Linter): Kršenje pravila imenovanja. Trebalo bi biti 'aRngStupidVar' ili slično.
            const int aRNGsTuPIdVar = 13;
            double result = 13;
            // GREŠKA (Linter): Kršenje pravila imenovanja. Trebalo bi biti 'testVer'.
            int teS_tVer = 13;
            
            // GREŠKA (Linter): Ekstremno uvlačenje.
                    //while(true)
            {
                            Console.WriteLine("I don't know what I am doing");
                            // GREŠKA (Linter): Ekstremno uvlačenje.
                                                  return 0;
            
}}


    }

// GREŠKA (Linter): Kršenje pravila imenovanja. Ime klase 'linting_Helper' trebalo bi biti 'LintingHelper'.
public static class linting_Helper
    {
        public static void HelperMethod()
        {
            const int aOvr = 13;
            Console.WriteLine("This is a helper method.");
            // GREŠKA (Linter): Nedostaje razmak prije otvarajuće vitičaste zagrade '{'.
            for (int i = 0; i < 10; i++){
                Console.WriteLine("This is the uglieset code I have ever written.");
            }
            // GREŠKA (Linter): Neiskorištena lokalna varijabla 'UnusedVariable'.
            // GREŠKA (Linter): Više izjava u istoj liniji.
            int UnusedVariable = 5;
            int result = 0;
            // GREŠKA (Linter): Ugniježdene 'if' izjave bez vitičastih zagrada su obeshrabrene.
            // GREŠKA (Linter): Neusklađena uvlačenja.
            if (true)
                             if (false)
                Console.WriteLine("Nested if just because");

        }
    }


// GREŠKA (Linter): Prazna definicija klase.n and spacing.
// GREŠKA (Linter): Nedosljedno uvlačenje i razmaci.
                public static class lintingRemover{


}
