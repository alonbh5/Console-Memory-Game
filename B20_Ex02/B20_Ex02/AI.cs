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
        bool partner;
    }


    class AI
    {
        Turns[] m_turnMem = new Turns[6];
        int m_turns = 0;
        int m_pairsMem = 0;
        int m_indexToAdd = -1;
                         

        public void PlayTurn(ref int row, ref int col, Board gameboard)
        {
            m_turns++;

            if (m_turns%3==0 && m_pairsMem > 0)
            { // Play smart every 3 turns and if there is a pair in memory
                smartchoice():
            }
            else
            {
                randomchoice();
            }
        }

        private void smartchoice(ref int row1, ref int col1, Board gameboard)
        {
            foreach (Turns i in m_turnMem)
            {
            }
        }

        internal void updateMemory(int i_Row, int i_Col)
        {
            m_indexToAdd++;
            if (m_indexToAdd>m_turnMem.Length)
            {
                m_i
            }
        }



    }
}
