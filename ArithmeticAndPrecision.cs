using System;

public class ArithmeticIssues
{
    // ERROR: Unreliable floating-point comparison using direct equality (==).
    // Due to precision errors, this comparison can fail unexpectedly.
    public bool IsValueOne(double value)
    {
        // For example, if 'value' is the result of `0.1 * 10`, it might not be exactly 1.0.
        return value == 1.0;
    }

    // A correct implementation would use an epsilon for tolerance.
    public bool IsValueOneCorrectly(double value)
    {
        return Math.Abs(value - 1.0) < 0.000001;
    }

    // ERROR: Integer overflow in an unchecked context.
    // By default, C# arithmetic is unchecked, so this will wrap around to a negative number without an exception.
    public int UncheckedAddition(int a, int b)
    {
        // If a = int.MaxValue and b = 1, the result will be int.MinValue.
        return a + b;
    }

    public void DemonstrateOverflow()
    {
        int largeNumber = int.MaxValue;
        int result = UncheckedAddition(largeNumber, 1);
        Console.WriteLine($"Result of overflow: {result}"); // Prints -2147483648
    }

    // ERROR: Unchecked type cast leading to data corruption.
    // Casting a large 'long' to an 'int' will truncate the value without warning.
    public int UnsafeCast(long bigValue)
    {
        // If bigValue is larger than int.MaxValue, the result will be incorrect.
        return (int)bigValue;
    }

    // ERROR: Incorrect mathematical formula.
    // This method purports to convert Celsius to Fahrenheit but uses the wrong formula.
    public double CelsiusToFahrenheit(double celsius)
    {
        // The correct formula is (celsius * 9 / 5) + 32.
        // This implementation is wrong.
        return (celsius * 1.5) + 30;
    }
}