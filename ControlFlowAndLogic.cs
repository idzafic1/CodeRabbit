using System;

public class ControlFlowIssues
{
    // ERROR: Infinite loop. The loop condition `i >= 0` will never be false.
    public void InfiniteLoop()
    {
        for (int i = 0; i >= 0; i++)
        {
            // This will run until an overflow occurs or the process is killed.
        }
    }

    // ERROR: Infinite recursion. The method lacks a base case to terminate recursion.
    public void InfiniteRecursion(int count)
    {
        InfiniteRecursion(count + 1); // StackOverflowException waiting to happen.
    }

    // ERROR: Off-by-one error. The loop condition should be `i < array.Length`.
    public void OffByOneError(int[] array)
    {
        for (int i = 0; i <= array.Length; i++)
        {
            // This will throw an IndexOutOfRangeException on the last iteration.
            Console.WriteLine(array[i]);
        }
    }

    // ERROR: Accidental empty loop body due to a misplaced semicolon.
    public void AccidentalEmptyLoop(int[] data)
    {
        for (int i = 0; i < data.Length; i++); // The semicolon ends the loop here.
        {
            // This block is NOT the loop body. It will execute only once, after the loop finishes.
            Console.WriteLine("This line runs only once.");
        }
    }

    // ERROR: Always-true condition. A number is always greater than 5 or less than or equal to 5.
    public bool TautologyCondition(int x)
    {
        if (x > 5 || x <= 5)
        {
            return true; // This branch is always taken.
        }
        return false; // Unreachable code.
    }

    // ERROR: Assignment (=) in a conditional instead of comparison (==).
    public void AssignmentInConditional(int status)
    {
        if (status = 1) // This assigns 1 to status and evaluates to true.
        {
            Console.WriteLine("Status is 1.");
        }
    }
    
    // ERROR: Missing 'default' case in a switch statement on an enum.
    // If a new value is added to 'LogLevel', this switch will silently do nothing.
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
            // No default case to handle LogLevel.Error or future additions.
        }
    }
}