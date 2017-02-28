using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IB_CoinFlip
{
    class Program
    {
        public static int TailsCounter { get; set; } = 0;
        public static int HeadsCounter { get; set; } = 0;
        public static int Counter { get; set; } = 0;
        public static List<char> FlipsHistory = new List<char>();


        static void Main(string[] args)
        {
            PrintCaption();
            Console.WriteLine("To start simulation, press Enter");
            Console.WriteLine("Press 'X' to exit");

            if (Console.ReadKey() == new ConsoleKeyInfo('x', ConsoleKey.X, false, false, false))
                Environment.Exit(0);

            Console.Clear();
            Console.WriteLine("To flip a coin, press any key.");
            Console.WriteLine("Press 'X' to exit\n");

            if (Console.ReadKey() == new ConsoleKeyInfo('x', ConsoleKey.X, false, false, false))
                Environment.Exit(0);

            while (true)
            {
                Console.Clear();

                switch (FlipTheCoin())
                {
                    case 't':
                        {
                            FlipsHistory.Add('T');
                            TailsCounter++;
                            Counter++;
                            PrintStats();
                            Console.WriteLine("TAIL!\n");
                            Console.WriteLine("To flip a coin again, press any key.");
                            Console.WriteLine("Press 'X' to exit.\n");
                            if (Console.ReadKey() == new ConsoleKeyInfo('x', ConsoleKey.X, false, false, false))
                                Environment.Exit(0);
                        }
                        break;

                    case 'h':
                        {
                            FlipsHistory.Add('H');
                            HeadsCounter++;
                            Counter++;
                            PrintStats();
                            Console.WriteLine("HEAD!\n");
                            Console.WriteLine("To flip a coin again, press any key.");
                            Console.WriteLine("Press 'X' to exit.\n");
                            if (Console.ReadKey() == new ConsoleKeyInfo('x', ConsoleKey.X, false, false, false))
                                Environment.Exit(0);
                        }
                        break;
                }
            }
        }

        private static char FlipTheCoin()
        {
            Random rand = new Random();
            int randValue = rand.Next();

            if (randValue % 2 == 0)
                return 't';
            else
                return 'h';

        }

        private static void PrintStats()
        {
            Console.WriteLine("Coins fliped: {0}", Counter);
            Console.WriteLine("Tails: {0}", TailsCounter);
            Console.WriteLine("Heads: {0}", HeadsCounter);
            Console.Write("History: ");

            foreach (var item in FlipsHistory)
                Console.Write(item + " ");

            Console.Write("\n\n");
        }

        private static void PrintCaption()
        {
            Console.WriteLine("CoinFlip v. 0.1");
            Console.WriteLine("Coded by imn1oy");
            Console.WriteLine("=========================");
        }
    }
}
