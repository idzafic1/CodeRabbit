using System;
using System.Threading;
using System.Threading.Tasks;

public class ConcurrencyIssues
{
    private int _sharedCounter = 0;
    private static readonly object _lockA = new object();
    private static readonly object _lockB = new object();

    // ERROR: Race condition. Multiple threads increment a shared counter without a lock.
    public async Task DemonstrateRaceCondition()
    {
        _sharedCounter = 0;
        var task1 = Task.Run(() => { for (int i = 0; i < 100000; i++) _sharedCounter++; });
        var task2 = Task.Run(() => { for (int i = 0; i < 100000; i++) _sharedCounter++; });
        await Task.WhenAll(task1, task2);
        // The final value will be unpredictable and less than 200,000.
        Console.WriteLine($"Race condition result: {_sharedCounter}");
    }

    // ERROR: Deadlock. Two threads lock resources in opposite orders.
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

    // ERROR: Synchronous blocking on async code (.Result), which can cause deadlocks in certain contexts.
    public string BlockOnAsync()
    {
        // In a UI or ASP.NET Classic context, this is a common cause of deadlocks.
        return GetMessageAsync().Result;
    }

    // ERROR: `async void` method. Exceptions thrown in this method can crash the process and are hard to catch.
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