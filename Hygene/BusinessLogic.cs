using System;

public class Calculator
{
  // Ova klasa ima više logičkih putanja za testiranje.
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
  // Složeniji put.
            if (a > 0 && b > 0)
            {
                return a * b;
            }
            return 0;
        }
        throw new NotSupportedException("Operation not supported.");
    }
}

