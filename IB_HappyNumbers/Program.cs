using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IB_HappyNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {


                PrintCaption();

                Console.WriteLine("Check number for Happy Number - press 'C'");
                Console.WriteLine("Show list of Happy Numbers - press 'L'");
                Console.WriteLine("Generate Happy Numbers - press 'G'");
                Console.WriteLine("\nClose program - press 'X'");



                ConsoleKeyInfo button = new ConsoleKeyInfo();
                button = Console.ReadKey();

                switch (button.Key)
                {
                    case ConsoleKey.C:
                        {
                            Console.Clear();
                            Console.Write("Please enter number to check: ");
                            uint number = uint.Parse(Console.ReadLine());

                            if (IsHappy(number))
                                Console.Write("{0} is Happy Number! :)", number);
                            else
                                Console.Write("{0} is Unhappy Number...", number);
                        }
                        break;

                    case ConsoleKey.G:
                        {
                            Console.Clear();
                            Console.Write("Please enter number of Happy Numbers to generate: ");
                            uint loop = uint.Parse(Console.ReadLine());
                            Console.Write("...in range from: ");
                            uint minRange = uint.Parse(Console.ReadLine());
                            Console.Write("...to: ");
                            uint maxRange = uint.Parse(Console.ReadLine());
                            uint[] happyNumbers;
                            GenerateHappyNumbers(loop, minRange, maxRange, out happyNumbers);
                            Console.WriteLine("Generated Happy Numbers: ");
                            foreach (var number in happyNumbers)
                                Console.Write(number + "  ");
                        }
                        break;

                    case ConsoleKey.L:
                        {
                            Console.Clear();
                            Console.Write("Please enter number of Happy Numbers to list: ");
                            uint number = uint.Parse(Console.ReadLine());
                            uint[] happyNumbers;

                            ListOfHappyNumbers(number, out happyNumbers);

                            Console.WriteLine("List of Happy Numbers: ");
                            foreach (var item in happyNumbers)
                                Console.Write(item + "  ");

                        } break;

                    case ConsoleKey.X:
                        {
                            Environment.Exit(0);

                        }
                        break;

                    default:
                        {
                            Console.WriteLine("\n\nSorry, wrong command. Please try agian.");
                        }
                        break;
                }

                Console.WriteLine("\n\nPress 'X' to exit. Press any other key to repeat.");
                if (Console.ReadKey() == new ConsoleKeyInfo('x', ConsoleKey.X, false, false, false))
                    Environment.Exit(0);
                Console.Clear();
            }
        }

        private static void ListOfHappyNumbers(uint number, out uint[] happyNumbers)
        {
            happyNumbers = new uint[number];
            int found = 0;
            uint tryNumber = 0;
            while (found < number)
            {
                if (IsHappy(tryNumber))
                {
                    happyNumbers[found] = tryNumber;
                    found++;
                }

                tryNumber++;

            }
        }

        private static void GenerateHappyNumbers(uint loop, uint minRange, uint maxRange, out uint[] happyNumbers)
        {
            Random rand = new Random();
            happyNumbers = new uint[loop];
            int found = 0;
            
            while(found < loop)
            {
                uint number = (uint)rand.Next((int)minRange, (int)maxRange);
                if (IsHappy(number))
                {
                    happyNumbers[found] = number;
                    found++;
                }
            }
        }

        private static bool IsHappy(uint number)
        {
            int attempt = 0;

            while (attempt < 100)
            {
                uint[] digitsTable;
                ExtractDigits(number, out digitsTable);

                uint sumOfSquares = 0;

                foreach (uint i in digitsTable)
                {
                    sumOfSquares = sumOfSquares + (i * i);
                }
                number = sumOfSquares;
                if (number == 1)
                    return true;
                else
                    attempt++;
            }

            return false;

        }

        private static void ExtractDigits(uint number, out uint[] digitsTable)
        {

            uint nDigits = (uint)number.ToString().Length;

            digitsTable = new uint[nDigits];

            for (int i = (int)nDigits; i > 0; i--)
            {
                uint digit = (uint)(number / Math.Pow(10, i - 1));
                digitsTable[nDigits - i] = digit;
                number = number - (digit * (uint)(Math.Pow(10, i - 1)));
            }

        }

        private static void PrintCaption()
        {
            Console.WriteLine("HappyNumbers v. 0.1");
            Console.WriteLine("Coded by imn1oy");
            Console.WriteLine("===================\n");
        }
    }
}
