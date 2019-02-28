using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace turing
{
    class TurItem
    {
        public int currentState;
        public char currentSymbol;
        public char symbolToChange;
        public char direction;
        public int nextState;

        public TurItem(int currentState, char currentSymbol, char symbolToChange, char direction, int nextState)
        {
            this.currentState = currentState;
            this.currentSymbol = currentSymbol;
            this.symbolToChange = symbolToChange;
            this.direction = direction;
            this.nextState = nextState;
        }
    }
}
