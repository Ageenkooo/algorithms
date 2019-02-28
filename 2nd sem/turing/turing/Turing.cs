using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace turing
{
    class Turing
    {
        public void TuringAlg(StringBuilder word, TurItem[] table, int currentState)
        {
            for (int i = 1; i < word.Length;)
            {
                for (int j = 0; j < table.GetLength(0); j++)
                {
                    if (currentState.Equals(table[j].currentState) && word[i].Equals(table[j].currentSymbol))
                    {
                        word[i] = table[j].symbolToChange;
                        currentState = table[j].nextState;

                        if (table[j].direction.Equals('+'))
                        {
                            i++;
                        }
                        else
                        {
                            i--;
                        }
                        break;
                    }
                }
                if (currentState == 0)
                {
                    break;
                }
            }
            Console.WriteLine("Result: " + word);
        }
    }
}
