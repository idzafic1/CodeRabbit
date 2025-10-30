using System;
// comment

public class ArithmeticIssues
{
    // GREŠKA: Nepouzdana poređenja sa pomičnim zarezom koristeći direktnu jednakost (==).
    // Zbog grešaka u preciznosti, ovo poređenje može neočekivano propasti.
    public bool IsValueOne(double value)
    {
        // Na primjer, ako je 'value' rezultat `0.1 * 10`, možda neće biti tačno 1.0.
        return value == 1.0;
    }

    // Ispravna implementacija bi koristila epsilon za toleranciju.
    public bool IsValueOneCorrectly(double value)
    {
        return Math.Abs(value - 1.0) < 0.000001;
    }

    // GREŠKA: Prelivanje cijelih brojeva u nekontrolisanom kontekstu.
    // Po defaultu, C# aritmetika je nekontrolisana, tako da će ovo preći u negativni broj bez izuzetka.
    public int UncheckedAddition(int a, int b)
    {
        // Ako je a = int.MaxValue i b = 1, rezultat će biti int.MinValue.
        return a + b;
    }

    public void DemonstrateOverflow()
    {
        int largeNumber = int.MaxValue;
        int result = UncheckedAddition(largeNumber, 1);
        Console.WriteLine($"Result of overflow: {result}"); // Prints -2147483648
    }

    // GREŠKA: Nekontrolisano prebacivanje tipa koje dovodi do oštećenja podataka.
    // Prebacivanje velikog 'long' u 'int' će skratiti vrijednost bez upozorenja.
    public int UnsafeCast(long bigValue)
    {
        // Ako je bigValue veći od int.MaxValue, rezultat će biti netačan.
        return (int)bigValue;
    }

    // GREŠKA: Netačna matematička formula.
    // Ova metoda se pretvara da konvertuje Celzijus u Farenhajt, ali koristi pogrešnu formulu.
    public double CelsiusToFahrenheit(double celsius)
    {
        
        // Ispravna formula je (celsius * 9 / 5) + 32.
        // Ova implementacija je pogrešna.
        return (celsius * 1.5) + 30;
    }
}
