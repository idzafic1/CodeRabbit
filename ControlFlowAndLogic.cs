using System;

public class ControlFlowIssues
{
    // GREŠKA: Beskonačna petlja. Uslov petlje `i >= 0` nikada neće postati neistinit.
    public void InfiniteLoop()
    {
        for (int i = 0; i >= 0; i++)
        {
            // Ovo će se izvršavati dok ne dođe do overflow-a ili dok proces ne bude zaustavljen.
        }
    }

    // GREŠKA: Beskonačna rekurzija. Metoda nema osnovni slučaj za prekid rekurzije.
    public void InfiniteRecursion(int count)
    {
        InfiniteRecursion(count + 1); // Izazvat će StackOverflowException.
    }

    // GREŠKA: Off-by-one greška. Uslov petlje treba biti `i < array.Length`.
    public void OffByOneError(int[] array)
    {
        for (int i = 0; i <= array.Length; i++)
        {
            // Ovo će baciti IndexOutOfRangeException na posljednjoj iteraciji.
            Console.WriteLine(array[i]);
        }
    }

    // GREŠKA: Slučajno prazno tijelo petlje zbog pogrešno postavljenog tačka-zareza.
    public void AccidentalEmptyLoop(int[] data)
    {
        for (int i = 0; i < data.Length; i++); // Tačka-zarez završava petlju ovdje.
        {
            // Ovaj blok NIJE tijelo petlje. Izvršiće se samo jednom, nakon što petlja završi.
            Console.WriteLine("This line runs only once.");
        }
    }

    // GREŠKA: Uvijek-tačan uslov. Broj je uvijek veći od 5 ili manji ili jednak 5.
    public bool TautologyCondition(int x)
    {
        if (x > 5 || x <= 5)
        {
            return true; // Ova grana se uvijek izvršava.
        }
        return false; // Nedostižni kod.
    }

    // GREŠKA: Dodjela (=) u uslovu umjesto poređenja (==).
    public void AssignmentInConditional(int status)
    {
        if (status = 1) // Ovo dodjeljuje 1 varijabli status i evaluira se kao true.
        {
            Console.WriteLine("Status is 1.");
        }
    }
    
    // GREŠKA: Nedostaje 'default' grana u switch izrazu nad enum tipom.
    // Ako se doda nova vrijednost u 'LogLevel', ovaj switch neće ništa učiniti i to će proći neopaženo.
    public enum LogLevel { Info, Warning, Error }
    public void MissingDefaultCase(LogLevel level)
    {
        switch(level)
        {
            case LogLevel.Info:
                Console.WriteLine("Information");
                break;
            case LogLevel.Warning:
                Console.WriteLine("Warning");
                break;
            // Nema default grane za obradu LogLevel.Error ili budućih dodataka.
        }
    }
}
