using System;
using CodeRabbit.MyProject;

class Program
{
    static void Main(string[] args)
    {
        var math = new MathUtilities();

        double sqrtOfFour = math.SquareRoot(4);
        Console.WriteLine($"Sqrt(4) = {sqrtOfFour}");
        Console.WriteLine("TrySquareRoot method test: " + math.TrySquareRoot(-7, out double result) + ", Result: " + result);
    }
}
