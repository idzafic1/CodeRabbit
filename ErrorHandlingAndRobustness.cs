using System;
using System.Linq;

public class ErrorHandlingAndRobustness
{
    // ERROR: Incorrect exception type. A more specific exception like 'ArgumentNullException'
    // or 'ArgumentOutOfRangeException' should be thrown instead of the generic 'Exception'.
    public void SetAge(int age)
    {
        if (age < 0)
        {
            throw new Exception("Age cannot be negative.");
        }
    }

    // ERROR: Not handling boundary cases. This function will throw an 'InvalidOperationException'
    // if the 'numbers' array is empty, because 'Average()' cannot be called on an empty sequence.
    public double CalculateAverage(int[] numbers)
    {
        return numbers.Average();
    }

    // ERROR: Unreliable floating-point comparison.
    // Comparing two double/float values for exact equality is dangerous due to precision errors.
    // This comparison might unexpectedly fail.
    public bool IsValueOne(double value)
    {
        // For example, if value is the result of 0.1 + 0.1 + ... (10 times),
        // it might be 0.9999999999999999, not exactly 1.0.
        return value == 1.0;
    }

    // ERROR: Name shadowing. The 'File' class name conflicts with the system's 'System.IO.File'.
    // This can lead to confusion and bugs if a developer intended to use the system class.
    public class File
    {
        public string Name { get; set; }
    }

    public void ProcessFile(string fileName)
    {
        // A developer might think they are creating an instance of our 'File' class,
        // but if they wrote 'System.IO.File.Create(fileName)', the similar naming is confusing.
        var myFile = new File { Name = fileName };
        Console.WriteLine(myFile.Name);
    }
}