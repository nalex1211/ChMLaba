using System;
using System.Collections.Generic;
using System.Globalization;

namespace ChMLaba
{
    internal class Program
    {
        public static double function(double x) => Math.Pow(x, 7) + x + 4;
        public static double df(double x) => 7 * Math.Pow(x, 6) + 1;
        public static double fi(double x) => -1 * Math.Pow(x + 4, 1.0 / 7.0);
        public static double fiDer(double x) => -1.0 / (7.0 * Math.Pow(x + 4, 6.0 / 7.0));
        static double newton_method(double a, double b, double epsilon)
        {
            double x0 = (a + b) / 2;
            int i = 0;
            Console.WriteLine($"Iteration {i}: x = {x0}, f(x) = {function(x0)}");
            while (Math.Abs(function(x0)) > epsilon)
            {
                i++;
                double x1 = x0 - function(x0) / df(x0);
                Console.WriteLine($"Iteartion {i}: x = {x1}, f(x) = {function(x1)}");
                x0 = x1;
            }
            return x0;
        }
        static double newton_method_20_iters(double a, double b, double epsilon)
        {
            double x0 = (a + b) / 2;
            for (int i = 1; i <= 20; i++)
            {
                double x1 = x0 - function(x0) / df(x0);
                Console.WriteLine($"Iteartion {i}: x = {x1}, f(x) = {function(x1)}");
                x0 = x1;
            }
            return x0;
        }
        static void Main(string[] args)
        {
            var floatFormat = new NumberFormatInfo()
            {
                NumberDecimalSeparator = "."
            };
            double epsilon = 0.000001;
            Console.Write("Enter the left margin of the interval: ");
            double a = Convert.ToDouble(Console.ReadLine(), floatFormat);

            Console.Write("Enter the right border of the interval: ");
            double b = Convert.ToDouble(Console.ReadLine(), floatFormat);

            double newtonRoot = newton_method(a, b, epsilon);
            Console.WriteLine($"The newtonRoot of the equation: {newtonRoot}");

            Console.WriteLine();

            double newtonRoot20Iters = newton_method_20_iters(a, b, epsilon);
            Console.WriteLine($"The newtonRoot 20 iters of the equation: {newtonRoot20Iters}");
            Console.WriteLine();
            double x0 = -1;
            double x1;
            double xTemp;


            int iterationNumber = 0;
            do
            {
                x1 = fi(x0);
                xTemp = x0;
                x0 = x1;
                iterationNumber++;
                Console.WriteLine("Iteration number: " + iterationNumber + "\t X: " + x1 + " |X(" + iterationNumber + ") - X(" + (iterationNumber - 1) + ")|: " + Math.Abs(x1 - xTemp));
            } while (Math.Abs(x1 - xTemp) > epsilon);

            Console.WriteLine();
            double newX1;
            double newXTemp;
            double newX0 = -1;

            for (int i = 1; i <= 20; i++)
            {
                newX1 = fi(newX0);
                newXTemp = newX0;
                newX0 = newX1;
                Console.WriteLine("Iteration number: " + i + "\t X: " + newX1 + " |X(" + i + ") - X(" + (i - 1) + ")|: " + Math.Abs(newX1 - newXTemp));
            }

            double q = Math.Abs(fiDer(a));
            var x = Math.Truncate(Math.Log((fi(x0) - (-1) / ((1 - q) * epsilon))) / Math.Log(1.0 / 0.07));
            Console.WriteLine();
            Console.WriteLine("Result:");
            Console.WriteLine("x = " + x1 + " f(x) = " + function(x1));
            Console.WriteLine($"Priori assessment - {x + 1}");
        }
    }
}
