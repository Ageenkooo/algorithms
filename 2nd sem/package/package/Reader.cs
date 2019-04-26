using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace package
{
    static class Reader
    {
        public static double Fraction()
        {
            double value1, value2;

            Console.WriteLine("Numerator: ");
            while (true)
            {
                value1 = Number();
                if (value1 != -1)
                {
                    break;
                }
            }
            
            Console.WriteLine("Denominator: ");
            while (true)
            {
                value2 = Number();
                if (value2 != -1 && value2 != 0)
                {
                    break;
                }
            }
            
            Console.WriteLine("Fraction: " + value1);
            Console.WriteLine("          " + "-");
            Console.WriteLine("          " + value2);
            return value1 / value2;
        }

        public static double Root()
        {
            double value;
            Console.WriteLine("Number for root: ");
            while (true)
            {
                value = Number();
                if (value > 0)
                {
                    Console.WriteLine("Number: " + Math.Sqrt(value));
                    break;
                }
                Console.WriteLine("Invalid");
            }

            return Math.Sqrt(value);
        }

        public static double Number(bool isNotForFraction = false)
        {
            double value;
            if (isNotForFraction)
            {
                Console.WriteLine("Number: ");
            }
            while (true)
            {
                if (double.TryParse(Console.ReadLine(), out value))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid");
                }
            }

            return value;
        }
    }
}
