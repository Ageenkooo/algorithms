using System;

namespace package
{
    static class Menu
    {
        public static void ShowMenu()
        {
            Package pack = new Package();
            double result;
            bool flag = true;
            while (flag)
            {
                Console.WriteLine("Press: ");
                Console.WriteLine("1 - number");
                Console.WriteLine("2 - root");
                Console.WriteLine("3 - fraction");
                string optionLine = Console.ReadLine();
                int option;
                Int32.TryParse(optionLine, out option);
                result = -1;
                switch (option)
                {
                    case 1:
                        result = Reader.Number(true);
                        break;
                    case 2:
                        result = Reader.Root();
                        break;
                    case 3:
                        result = Reader.Fraction();
                        break;
                    default:
                        Console.WriteLine("Try again");
                        break;
                }
                if (flag)
                {
                    if (result != -1)
                    {
                        pack.AddItem(result);
                        pack.ShowPacksLoad();
                    }
                }

            }
        }
    }
}
