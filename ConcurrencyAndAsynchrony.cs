using System;
using System.Threading;
using System.Threading.Tasks;


public class ConcurrencyIssues
{
    private int _sharedCounter = 0;
    private static readonly object _lockA = new object();
    private static readonly object _lockB = new object();

    // GREŠKA: Race Condition. Više niti povećava zajednički brojač bez zaključavanja.
    public async Task DemonstrateRaceCondition()
    {
        _sharedCounter = 0;
        var task1 = Task.Run(() => { for (int i = 0; i < 100000; i++) _sharedCounter++; });
        var task2 = Task.Run(() => { for (int i = 0; i < 100000; i++) _sharedCounter++; });
        await Task.WhenAll(task1, task2);
        // Konačna vrijednost će biti nepredvidiva i manja od 200.000.
        Console.WriteLine($"Race condition result: {_sharedCounter}");
    }
    // GREŠKA: Deadlock. Dvije niti zaključavaju resurse u suprotnim redoslijedima.
    public async Task DemonstrateDeadlock()
    {
        var task1 = Task.Run(() => {
            lock (_lockA)
            {
                Thread.Sleep(100);
                lock (_lockB) { Console.WriteLine("This will never be reached."); }
            }
        });

        var task2 = Task.Run(() => {
            lock (_lockB)
            {
                Thread.Sleep(100);
                lock (_lockA) { Console.WriteLine("This will never be reached."); }
            }
        });
        await Task.WhenAll(task1, task2);
    }

    // GREŠKA: Sinhrono blokiranje na asinkodnom kodu (.Result), što može uzrokovati deadlock u određenim kontekstima.
    public string BlockOnAsync()
    {
        // U UI ili ASP.NET Classic kontekstu, ovo je čest uzrok deadlocka.
        return GetMessageAsync().Result;
    }

    // GREŠKA: `async void` metoda. Izuzeci koji se bacaju u ovoj metodi mogu srušiti proces i teško ih je uhvatiti.
    public async void FireAndForget()
    {
        await Task.Delay(100);
        throw new InvalidOperationException("Exception from async void.");
    }

    public async Task<string> GetMessageAsync()
    {
        await Task.Delay(100);
        return "Hello, World!";
    }
}
