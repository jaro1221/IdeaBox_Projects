using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IB_RomanNumeral
{
    class Program
    {
        public static int Number { get; set; }
        public static string[] RomanTableI = new string[] { "", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX" };
        public static string[] RomanTableII = new string[] { "", "X", "XX", "XXX", "XL", "L", "LX", "LXX", "LXXX", "XC" };
        public static string[] RomanTableIII = new string[10] { "", "C", "CC", "CCC", "CD", "D", "DC", "DCC", "DCCC", "CM" };
        public static string[] RomanTableIV = new string[4] { "", "M", "MM", "MMM" };
        private static List<string[]> RomanTableList = new List<string[]>();



        static void Main(string[] args)
        {
            RomanTableList.Add(RomanTableI);
            RomanTableList.Add(RomanTableII);
            RomanTableList.Add(RomanTableIII);
            RomanTableList.Add(RomanTableIV);

            while (true)
            {
                PrintCaption();
                Console.WriteLine("Please enter your number");
                Console.Write("Number: ");
                
                Number = int.Parse(Console.ReadLine());
                Console.Write("Roman Number: {0}", ConvertToRoman(Number));
                Console.WriteLine("\n\nTo convert another number, press any key. Press 'X' to exit");
                if (Console.ReadKey() == new ConsoleKeyInfo('x', ConsoleKey.X, false, false, false))
                    Environment.Exit(0);
                Console.Clear();
            }



        }

        private static string ConvertToRoman(int number)
        {
            string RomanOutput = "";
            string numberAsString = number.ToString();
            for (int i = 1; i < numberAsString.Length + 1; i++)
            {
                int digit = int.Parse(numberAsString.Substring(i - 1, 1));
                RomanOutput += (RomanTableList[Math.Abs((i - numberAsString.Length))])[digit];
            }
            return RomanOutput;

        }

        private static void PrintCaption()
        {
            Console.WriteLine("RomanNumeral v. 0.1");
            Console.WriteLine("Coded by imn1oy");
            Console.WriteLine("===================");
        }
    }
}
