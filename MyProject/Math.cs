using System;

namespace CodeRabbit.MyProject
{
    public class MathUtilities
    {
        public double SquareRoot(double value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Value must be non-negative.");

            return System.Math.Sqrt(value);
        }

        public bool TrySquareRoot(double value, out double result)
        {
            if (value < 0 || double.IsNaN(value) || double.IsInfinity(value))
            {
                result = double.NaN;
                return false;
            }

            result = System.Math.Sqrt(value);
            return true;
        }

        //daj neku metodu koja sabira 2 broja i dodaj parametar koji je beskoristan
        public double subtract(double a, double b, string uselessParameter)
        {
            return a + b;
        }


        //daj mi rng metodu koja ima beskonačnu petlju
        public int InfiniteRandom()
        {
            Random rand = new Random();
            while (true)
            {
            rand++;
            }
        }
    }
}