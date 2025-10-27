using System;

public class Calculator
{
    // This class has multiple logic paths to test.
    public int Calculate(int a, int b, string operation)
    {
        if (operation == "ADD")
        {
            return a + b;
        }
        if (operation == "SUBTRACT")
        {
            return a - b;
        }
        if (operation == "MULTIPLY")
        {
            // A more complex path.
            if (a > 0 && b > 0)
            {
                return a * b;
            }
            return 0;
        }
        throw new NotSupportedException("Operation not supported.");
    }
}