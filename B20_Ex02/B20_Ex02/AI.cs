using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B20_Ex02
{
    struct Turns
    {
        int row;
        int col;
        int value;
    }


    class AI
    {
        Turns[] m_turnMem = new Turns[6];
        int m_turns = 0;

        internal AI()
        {
            m_turnMem 
        }       
    }
    
    


        public void PlayTurn(ref int row, ref int col, Board gameboard)
        {
            m_turns++;

            if (m_turns%3==0)
            {
                smartchoice():
            }
            else
            {
                randomchoice();
            }


        }

    }
}
