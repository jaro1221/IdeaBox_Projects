using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IB_PrimeFactorization
{
    class Program
    {
        public static List<int> Factors = new List<int>(0);

        static void Main(string[] args)
        {
            while (true)
            {
                Factors.Clear();
                PrintCaption();
                Console.WriteLine("Enter a number for factorization");
                Console.Write("Number: ");
                int number = int.Parse(Console.ReadLine());
                PrimeFactorise(number);

                switch (Factors.Count)
                {
                    case 0:
                        {
                            Console.WriteLine("{0} - Prime Number ", number);
                        }
                        break;
                    default:
                        {
                            Console.Write("{0} = ", number);
                            int counter = Factors.Count;
                            foreach (var item in Factors)
                            {
                                if (counter == 1)
                                    Console.Write(item);
                                else
                                {
                                    Console.Write(item + " * ");
                                    counter--;
                                }

                            }
                        }
                        break;
                }
                Console.WriteLine("\n\nTo factorise another number, press any key. Press 'X' to exit.");
                if (Console.ReadKey() == new ConsoleKeyInfo('x', ConsoleKey.X, false, false, false))
                    Environment.Exit(0);
                Console.Clear();
            }
        }

        private static void PrimeFactorise(int number)
        {
            if (!IsPrime(number))
            {
                while (number != 1)
                {
                    for (int i = 2; i < number + 1; i++)
                    {
                        if (number % i == 0)
                        {
                            Factors.Add(i);
                            number = number / i;
                            break;
                        }
                    }
                }


            }
        }

        private static bool IsPrime(int number)
        {
            for (int i = 2; i < number; i++)
            {
                if (number % i == 0)
                    return false;
            }
            return true;
        }

        private static void PrintCaption()
        {
            Console.WriteLine("PrimeFactorization v. 0.1");
            Console.WriteLine("Coded by imn1oy");
            Console.WriteLine("=========================");
        }
    }
}
