using System;
using System.IO;

public class ExceptionHandlingIssues
{
    // ERROR: Throwing an overly general exception type.
    // Should throw a more specific exception like ArgumentNullException.
    public void ProcessName(string name)
    {
        if (name == null)
        {
            throw new Exception("Name cannot be null.");
        }
    }

    // ERROR: Empty catch block (swallowing an exception).
    // This hides the original error, making debugging extremely difficult.
    public void ReadFile()
    {
        try
        {
            File.ReadAllText("non_existent_file.txt");
        }
        catch (IOException)
        {
            // The exception is caught, but nothing is done. The program continues as if nothing happened.
        }
    }

    // ERROR: Incorrectly re-throwing an exception, which resets the stack trace.
    public void IncorrectRethrow()
    {
        try
        {
            int.Parse("not-a-number");
        }
        catch (FormatException ex)
        {
            Console.WriteLine("Logging error...");
            // This 'throw ex' loses the original stack trace information.
            // The correct way is to use 'throw;'.
            throw ex;
        }
    }

    // ERROR: Failure to handle boundary cases (null input).
    // This method will throw a NullReferenceException if 'items' is null.
    public int GetItemCount(string[] items)
    {
        // There is no 'if (items == null)' check.
        return items.Length;
    }
}