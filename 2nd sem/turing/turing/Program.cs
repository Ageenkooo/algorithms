using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace turing
{
    class Program
    {
        static void Main(string[] args)
        {
            string alphabet1 = "0123";
            string alphabet2 = "ab";

            int currentState = 1;

            Console.WriteLine("Enter data: ");
            string inputData = Console.ReadLine();

            while (true)
            {
                if (!inputData.All(c => alphabet1.Contains(c)) && !inputData.All(c => alphabet2.Contains(c)))
                {
                    Console.WriteLine("WrongData, try again");
                    inputData = Console.ReadLine();
                }
                else
                {
                    inputData = String.Concat("#", inputData, "#");
                    Console.WriteLine("Your string is: " + inputData);
                    break;
                }
            }

            StringBuilder word = new StringBuilder(inputData);

            TurItem[] table1 = { new TurItem( 1, '0', ' ', '-', 2) , 
                                 new TurItem( 1, '1', '1', '+', 1) ,
                                 new TurItem( 1, '2', '2', '+', 1) ,
                                 new TurItem( 1, '3', '3', '+', 1) ,
                                 new TurItem( 1, '#', '#', '-', 7) ,
                                 new TurItem( 2, '0', '0', '-', 2) ,
                                 new TurItem( 2, '1', '1', '-', 2) ,
                                 new TurItem( 2, '2', '2', '-', 2) ,
                                 new TurItem( 2, '3', '3', '-', 2) ,
                                 new TurItem( 2, '#', '#', '+', 3) ,
                                 new TurItem( 3, '0', '0', '+', 3) ,
                                 new TurItem( 3, '1', '0', '+', 4) ,
                                 new TurItem( 3, '2', '0', '+', 5) ,
                                 new TurItem( 3, '3', '0', '+', 6) ,
                                 new TurItem( 3, ' ', '0', '+', 1) ,
                                 new TurItem( 4, '0', '0', '+', 4) ,
                                 new TurItem( 4, '1', '1', '+', 4) ,
                                 new TurItem( 4, '2', '2', '+', 4) ,
                                 new TurItem( 4, '3', '3', '+', 4) ,
                                 new TurItem( 4, ' ', '1', '+', 1) ,
                                 new TurItem( 5, '0', '0', '+', 5) ,
                                 new TurItem( 5, '1', '1', '+', 5) ,
                                 new TurItem( 5, '2', '2', '+', 5) ,
                                 new TurItem( 5, '3', '3', '+', 5) ,
                                 new TurItem( 5, ' ', '2', '+', 1) ,
                                 new TurItem( 6, '0', '0', '+', 6) ,
                                 new TurItem( 6, '1', '1', '+', 6) ,
                                 new TurItem( 6, '2', '2', '+', 6) ,
                                 new TurItem( 6, '3', '3', '+', 6) ,
                                 new TurItem( 6, ' ', '3', '+', 1) ,
                                 new TurItem( 7, '0', '0', '-', 7) ,
                                 new TurItem( 7, '1', '1', '-', 7) ,
                                 new TurItem( 7, '2', '2', '-', 7) ,
                                 new TurItem( 7, '3', '3', '-', 7) ,
                                 new TurItem( 7, '#', '#', '+', 8) ,
                                 new TurItem( 8, '0', '0', '+', 8) ,
                                 new TurItem( 8, '1', '1', '+', 8) ,
                                 new TurItem( 8, '2', ' ', '-', 9) ,
                                 new TurItem( 8, '3', '3', '+', 8) ,
                                 new TurItem( 8, '#', '#', '+', 0) ,
                                 new TurItem( 9, '0', '0', '-', 9) ,
                                 new TurItem( 9, '1', '1', '-', 9) ,
                                 new TurItem( 9, '2', '2', '-', 9) ,
                                 new TurItem( 9, '3', '3', '-', 9) ,
                                 new TurItem( 9, '#', '#', '+', 10) ,
                                 new TurItem( 10, '0', '0', '+', 10) ,
                                 new TurItem( 10, '1', '2', '+', 11) ,
                                 new TurItem( 10, '2', '2', '+', 10) ,
                                 new TurItem( 10, '3', '2', '+', 12) ,
                                 new TurItem( 10, ' ', '2', '+', 8) ,
                                 new TurItem( 11, '0', '0', '+', 11) ,
                                 new TurItem( 11, '1', '1', '+', 11) ,
                                 new TurItem( 11, '2', '2', '+', 11) ,
                                 new TurItem( 11, '3', '3', '+', 11) ,
                                 new TurItem( 11, ' ', '1', '+', 8) ,
                                 new TurItem( 12, '0', '0', '+', 12) ,
                                 new TurItem( 12, '1', '1', '+', 12) ,
                                 new TurItem( 12, '2', '2', '+', 12) ,
                                 new TurItem( 12, '3', '3', '+', 12) ,
                                 new TurItem( 12, ' ', '3', '+', 8) ,
            };

            TurItem[] table2 = {  new TurItem( 1, 'a', ' ', '+', 2 ),
                                  new TurItem( 1, 'b', ' ', '+', 4 ),
                                  new TurItem( 1, '#', '#', '+', 0 ) ,
                                  new TurItem( 1, ' ', ' ', '+', 1 ) ,
                                  new TurItem( 2, 'a', 'a', '+', 2 ) ,
                                  new TurItem( 2, 'b', ' ', '-', 3) ,
                                  new TurItem( 2, '#', '#', '-', 5) ,
                                  new TurItem( 2, ' ', ' ', '+', 2) ,
                                  new TurItem( 3, 'a', 'a', '-', 3) ,
                                  new TurItem( 3, 'b', 'b', '-', 3) ,
                                  new TurItem( 3, '#', '#', '+', 1) ,
                                  new TurItem( 3, ' ', ' ', '-', 3) ,
                                  new TurItem( 4, 'a', ' ', '-', 3) ,
                                  new TurItem( 4, 'b', 'b', '+', 4) ,
                                  new TurItem( 4, '#', '#', '-', 7) ,
                                  new TurItem( 4, ' ', ' ', '+', 4) ,
                                  new TurItem( 5, ' ', 'a', '-', 6) ,
                                  new TurItem( 5, 'a', 'a', '-', 8) ,
                                  new TurItem( 6, 'a', ' ', '-', 6) ,
                                  new TurItem( 6, 'b', ' ', '-', 6) ,
                                  new TurItem( 6, '#', '#', '+', 0) ,
                                  new TurItem( 6, ' ', ' ', '-', 6) ,
                                  new TurItem( 7, 'b', 'b', '-', 8) ,
                                  new TurItem( 7, ' ', 'b', '-', 6) ,
                                  new TurItem( 8, 'a', ' ', '-', 8) ,
                                  new TurItem( 8, 'b', ' ', '-', 8) ,
                                  new TurItem( 8, '#', '#', '+', 0) ,
                                  new TurItem( 8, ' ', ' ', '-', 8) 
            };

            Turing TM = new Turing();

            alphabet1 = String.Concat("#", alphabet1);
            alphabet2 = String.Concat("#", alphabet2);

            if (inputData.All(c => alphabet1.Contains(c)))
            {
                TM.TuringAlg(word, table1, currentState);
            }

            if(inputData.All(c => alphabet2.Contains(c)))
            {
                TM.TuringAlg(word, table2, currentState);
            }

            Console.ReadLine();
        }
    }
}
