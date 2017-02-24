using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IB_FactorialFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                PrintCaption();
                Console.Write("Enter number: ");

                try
                {
                    uint number = uint.Parse(Console.ReadLine());
                    uint factorial = GetFactorial(number);
                    if (factorial == 0)
                        throw new OverflowException("Factorial value is too large to represent.");
                    else
                        Console.WriteLine("{0}! = {1}", number, factorial);
                }
                catch (Exception ex)
                {
                    if (ex is ArgumentNullException)
                    {
                        Console.WriteLine("No number entered. Please enter a number.");
                    }
                    else if (ex is FormatException)
                    {
                        Console.WriteLine("Invalid number format. Please enter a valid number.");
                    }
                    else if (ex is OverflowException)
                    {
                        Console.WriteLine("Your number is too big. Please enter another one.");
                    }


                }
                finally
                {
                    Console.WriteLine("\nPress 'X' to exit program. Press any other key to reply.");
                    if (Console.ReadKey() == new ConsoleKeyInfo('x', ConsoleKey.X, false, false, false))
                    {
                        Environment.Exit(0);
                    }
                    Console.Clear();
                }
            }
        }

        private static uint GetFactorial(uint number)
        {
            uint factorial = 1;
            for (uint i = number; i > 1; i--)
            {
                factorial = factorial * i;
                if(factorial > uint.MaxValue)
                {
                    factorial = 0;
                    break;
                }
            }

            return factorial;
        }

        private static void PrintCaption()
        {
            Console.WriteLine("FactorialFinder v. 0.1");
            Console.WriteLine("Coded by imn1oy");
            Console.WriteLine("=========================\n");
        }
    }
}
