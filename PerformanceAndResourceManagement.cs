using System;
using System.Threading;
using System.Threading.Tasks;
// komentar

public class PerformanceAndResourceManagement
{
    // GREŠKA: Aritmetičko preljevanje. Ako su 'a' i 'b' veliki pozitivni brojevi (npr. int.MaxValue),
    // njihov zbir će preteći i "omotati se" u negativan broj bez bacanja izuzetka.
    public int UncheckedAdd(int a, int b)
    {
        // U zadanoj C# (unchecked) kontekstu, ovo neće baciti izuzetak.
        return a + b;
    }

    // Ovaj metod demonstrira preljevanje.
    public void DemonstrateOverflow()
    {
        int value = UncheckedAdd(int.MaxValue, 1);
        Console.WriteLine($"Result of overflow: {value}"); // Ispisuje veliki negativan broj.
    }

    // GREŠKA: Neefikasna alokacija resursa. Alociranje niza za 100 miliona cijelih brojeva
    // kada je zapravo potrebna samo mala količina. Ovo troši veliku količinu memorije.
    public void InefficientAllocation()
    {
        // Ovo alocira otprilike 400 MB memorije.
        int[] largeArray = new int[100_000_000];
        largeArray[0] = 1;
        // Ostatak niza je neiskorišten, što je rasipanje memorije.
    }

    // GREŠKA: Race Condition. Više ih pristupa i mijenja 'sharedCounter'
    // bez ikakvog zaključavanja. Konačan rezultat će biti nepredvidiv i vrlo vjerovatno pogrešan.
    private int sharedCounter = 0;
    public void RaceCondition()
    {
        var task1 = Task.Run(() => {
            for (int i = 0; i < 100000; i++) sharedCounter++;
        });
        var task2 = Task.Run(() => {
            for (int i = 0; i < 100000; i++) sharedCounter++;
        });

        Task.WaitAll(task1, task2);
        // Očekivana vrijednost je 200000, ali stvarna vrijednost će biti manja.
        Console.WriteLine($"Final counter value (Race Condition): {sharedCounter}");
    }

    // GREŠKA: Deadlock. Dvije niti zaključavaju dva resursa u suprotnom redoslijedu.
    // Nit 1 zaključava 'lockA' pa čeka na 'lockB'.
    // Nit 2 zaključava 'lockB' pa čeka na 'lockA'.
    // One će čekati jedna na drugu zauvijek.
    private static readonly object lockA = new object();
    private static readonly object lockB = new object();

    public void Deadlock()
    {
        var task1 = Task.Run(() => {
            lock (lockA)
            {
                Console.WriteLine("Thread 1 locked lockA");
                Thread.Sleep(100);
                lock (lockB)
                {
                    Console.WriteLine("Thread 1 locked lockB"); // Ovo nikada neće biti dosegnuto
                }
            }
        });

        var task2 = Task.Run(() => {
            lock (lockB)
            {
                Console.WriteLine("Thread 2 locked lockB");
                Thread.Sleep(100);
                lock (lockA)
                {
                    Console.WriteLine("Thread 2 locked lockA"); // Ovo nikada neće biti dosegnuto
                }
            }
        });

        Task.WaitAll(task1, task2);
        Console.WriteLine("Deadlock example finished."); // Ovo nikada neće biti dosegnuto
    }
}
