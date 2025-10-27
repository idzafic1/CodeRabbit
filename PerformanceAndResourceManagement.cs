using System;
using System.Threading;
using System.Threading.Tasks;

public class PerformanceAndResourceManagement
{
    // ERROR: Arithmetic overflow. If 'a' and 'b' are large positive integers (e.g., int.MaxValue),
    // their sum will overflow and wrap around to a negative number without throwing an exception.
    public int UncheckedAdd(int a, int b)
    {
        // In a default C# (unchecked) context, this will not throw an error.
        return a + b;
    }

    // This method demonstrates the overflow.
    public void DemonstrateOverflow()
    {
        int value = UncheckedAdd(int.MaxValue, 1);
        Console.WriteLine($"Result of overflow: {value}"); // Prints a large negative number.
    }

    // ERROR: Inefficient resource allocation. Allocating an array for 100 million integers
    // when only a small portion is actually needed. This consumes a huge amount of memory.
    public void InefficientAllocation()
    {
        // This allocates approximately 400 MB of memory.
        int[] largeArray = new int[100_000_000];
        largeArray[0] = 1;
        // The rest of the array is unused, which is a waste of memory.
    }

    // ERROR: Race Condition. Multiple threads are accessing and modifying 'sharedCounter'
    // without any locking. The final result will be unpredictable and almost certainly wrong.
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
        // The expected value is 200,000, but the actual value will be lower.
        Console.WriteLine($"Final counter value (Race Condition): {sharedCounter}");
    }

    // ERROR: Deadlock. Two threads lock two resources in opposite orders.
    // Thread 1 locks 'lockA' then waits for 'lockB'.
    // Thread 2 locks 'lockB' then waits for 'lockA'.
    // They will wait for each other forever.
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
                    Console.WriteLine("Thread 1 locked lockB"); // This will never be reached
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
                    Console.WriteLine("Thread 2 locked lockA"); // This will never be reached
                }
            }
        });

        Task.WaitAll(task1, task2);
        Console.WriteLine("Deadlock example finished."); // This will never be reached
    }
}