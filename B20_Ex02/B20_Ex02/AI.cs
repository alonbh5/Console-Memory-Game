using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;



namespace B20_Ex02
{


    struct Turns
    {

        internal int row;
        internal int col;
        internal int value;
        internal bool partner;
        internal int partnerRow;
        internal int partnerCol;
        internal bool sent;


        internal Turns(int i_row, int i_col, int i_value)
        {

            row = i_row;
            col = i_col;
            value = i_value;
            partner = false;
            sent = false;
            partnerCol = -1;
            partnerRow = -1;

        }
    }


    class AI
    {
        const int MAX_MEM = 6;
        const int NOT_FOUND = -1;

        Turns[] m_turnMem = new Turns[MAX_MEM];
        int m_turns = 0;
        int m_pairsMem = 0;
        int m_indexToAdd = -1;


        public void PlayTurn(ref int io_row, ref int io_col, Board i_gameboard)
        {
            bool doRandom = true;
            m_turns++;

            if (m_turns % 3 == 0 && m_pairsMem > 0)
            { // Play smart every 3 turns and if there is a pair in memory
                int index = MemoryInRealTime(i_gameboard);
                if (index != NOT_FOUND)
                {
                    smartchoice(ref io_row, ref io_col, index);
                    doRandom = false;
                }
                
            }            

            if (doRandom)
            {
                randomchoice(ref io_row, ref io_col, i_gameboard);
            }
        }

        private void smartchoice(ref int io_row, ref int io_col, int i_index)
        {

            if (!m_turnMem[i_index].sent)
            {
                //send the first coordiante
                io_row = m_turnMem[i_index].row;
                io_col = m_turnMem[i_index].col;
                m_turnMem[i_index].sent = true;
            }
            else
            {
                io_row = m_turnMem[i_index].partnerRow;
                io_col = m_turnMem[i_index].partnerCol;
                m_pairsMem--;
                m_turnMem[i_index].partner = false;
                m_indexToAdd = i_index;

            }
                    
        }
        internal void updateMemory(int i_Row, int i_Col, Tile i_tile)
        {
            //Update AI Memory of tails that just got reveled on board
            m_indexToAdd++;
            bool found = false;

            if (m_pairsMem != MAX_MEM) //if memeory is not full of pairs 
            {
                for (int i = 0; i < m_turnMem.Length; i++) //find if the AI has seen this tile-pair before
                {
                    if ((m_turnMem[i].value == i_tile.Value) && (m_turnMem[i].row != i_Row && m_turnMem[i].col != i_Col))
                    {
                        m_turnMem[i].partner = true;
                        m_turnMem[i].partnerCol = i_Col;
                        m_turnMem[i].partnerRow = i_Row;
                        m_pairsMem++;
                        found = true;
                        break;
                    }
                }

                if (!found) //case not seen this tile before - lets remember it
                {
                    while (m_turnMem[m_indexToAdd % MAX_MEM].partner)
                    {
                        m_indexToAdd++;
                    }
                    m_turnMem[m_indexToAdd % MAX_MEM] = new Turns(i_Row, i_Col, i_tile.Value);
                }

            }
        }
        private int MemoryInRealTime(Board i_gameboard)
        {
            //case AI remember a pair for sure
            int res = NOT_FOUND;

            for (int i = 0; i < m_turnMem.Length; i++)
            {
                if (m_turnMem[i].partner)
                {
                    if (!(i_gameboard.m_Board[m_turnMem[i].partnerRow, m_turnMem[i].partnerCol].Expose))
                    {
                        res = i;
                        break;
                    }
                    else
                    {
                        //case the pair AI remembers was found before AI
                        m_pairsMem--;
                        m_turnMem[i].partner = false;
                        m_indexToAdd = i;

                    }
                }                
            }
            return res;
        }
        private void randomchoice(ref int io_row, ref int io_col, Board i_gameboard)
        {

        }

    }
}

