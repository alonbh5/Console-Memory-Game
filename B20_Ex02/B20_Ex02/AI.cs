using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace B20_Ex02
{
    internal struct AIMemCell       //// add internal
    {
        internal int m_Row;
        internal int m_Col;
        internal int m_Value;
        internal bool m_PairFound;
        internal int m_PairRow;
        internal int m_PairCol;
        internal bool m_SentFirstLoc;

        internal AIMemCell(int i_Row, int i_Col, int i_Value)
        {
            m_Row = i_Row;
            m_Col = i_Col;
            m_Value = i_Value;
            m_PairFound = false;
            m_SentFirstLoc = false;
            m_PairCol = -1;
            m_PairRow = -1;
        }
    }

    internal class AI       //// add internal and private to members
    {
        private const int k_MaxMem = 6;
        private const int k_NotFound = -1;

        private AIMemCell[] m_AIMem = new AIMemCell[k_MaxMem];
        private int m_Turns = 0;
        private int m_Reveled = 0;
        private bool m_DoSmartChoice = false;
        private int m_PairsInMem = 0;
        private int m_IndexToAdd = 0;

        public void PlayTurn(ref int io_Row, ref int io_Col, Board i_Gameboard)
        {
            bool isRandom = true;
            m_Reveled++;

            if (m_Reveled % 2 != 0)
            {
                m_Turns++;
            }

            if (m_Turns % 3 == 0)
            { // Play smart every 3 turns.
                m_DoSmartChoice = true;
            } 

            if (m_DoSmartChoice && m_PairsInMem > 0)
            { // And if there is a pair in memory
                int index = memoryInRealTime(i_Gameboard);
                if (index != k_NotFound)
                {
                    smartChoice(ref io_Row, ref io_Col, index);
                    isRandom = false;
                }
            }

            if (isRandom)
            {
                randomChoice(ref io_Row, ref io_Col, i_Gameboard);
            }
        }

        private void smartChoice(ref int io_Row, ref int io_Col, int i_Index)
        {
            if (!m_AIMem[i_Index].m_SentFirstLoc) 
            {
                //// Send the first coordiante expooer
                io_Row = m_AIMem[i_Index].m_Row;
                io_Col = m_AIMem[i_Index].m_Col;
                m_AIMem[i_Index].m_SentFirstLoc = true;                
            }
            else
            {
                io_Row = m_AIMem[i_Index].m_PairRow;
                io_Col = m_AIMem[i_Index].m_PairCol;              
            }

            if (m_Reveled % 2 == 0) 
            {
                m_PairsInMem--;
                m_AIMem[i_Index].m_PairFound = false;
                m_AIMem[i_Index].m_SentFirstLoc = false;
                m_DoSmartChoice = false;
            }
        }

        internal void updateMemory(int i_Row, int i_Col, Tile i_Tile)
        { //// Update AI Memory of tails that just got reveled on board
            bool isFound = false;

            if (m_PairsInMem != k_MaxMem) 
            { // If memeory is not full of pairs 
                for (int i = 0; i < m_AIMem.Length; i++)
                { // Find if the AI has seen this tile-pair before
                    if (m_AIMem[i].m_Value == i_Tile.Value)
                    { // Found this value before
                        if (!(m_AIMem[i].m_Row == i_Row && m_AIMem[i].m_Col == i_Col))
                        { // Case the other pair
                            m_AIMem[i].m_PairFound = true;
                            m_AIMem[i].m_PairCol = i_Col;
                            m_AIMem[i].m_PairRow = i_Row;
                            m_PairsInMem++;

                            if (m_Reveled % 2 != 0)
                            { // Case found pair in smartcohice (that goes random)                                
                                m_AIMem[i].m_PairCol = m_AIMem[i].m_Col;
                                m_AIMem[i].m_PairRow = m_AIMem[i].m_Row;
                                m_AIMem[i].m_Col = i_Col;
                                m_AIMem[i].m_Row = i_Row;
                                m_AIMem[i].m_SentFirstLoc = true;
                            }
                        }

                        isFound = true;
                        break;
                    }
                }

                if (!isFound)
                { // Case not seen this tile before - lets remember it
                    while (m_AIMem[m_IndexToAdd % k_MaxMem].m_PairFound)
                    {
                        m_IndexToAdd++;
                    }

                    m_AIMem[m_IndexToAdd++ % k_MaxMem] = new AIMemCell(i_Row, i_Col, i_Tile.Value);
                }
            }
        }

        private int memoryInRealTime(Board i_Gameboard)
        { // Case AI remember a pair for sure
            int index = k_NotFound;

            for (int i = 0; i < m_AIMem.Length; i++)
            {
                if (m_Reveled % 2 == 0)
                { // on second reveled, only look for specific pair
                    if (m_AIMem[i].m_SentFirstLoc)
                    {
                        index = i;
                        break;
                    }
                }
                else
                {
                    if (m_AIMem[i].m_PairFound)
                    {
                        if (!i_Gameboard.m_Board[m_AIMem[i].m_PairRow, m_AIMem[i].m_PairCol].Expose)
                        {
                            index = i;
                            break;
                        }
                        else
                        { // case the pair that the AI remembers was already found by other player
                            m_PairsInMem--;
                            m_AIMem[i].m_PairFound = false;
                            m_AIMem[i].m_SentFirstLoc = false;
                        }
                    }
                }
            }

            return index;
        }

        private void randomChoice(ref int io_Row, ref int io_Col, Board i_Gameboard)
        {
            Random rnd = new Random();
            int loc = 0;
            int maxRnd = i_Gameboard.Cols * i_Gameboard.Rows;

            do
            {
                loc = rnd.Next(maxRnd);
            } while (checkRandomLocation(loc, ref io_Row, ref io_Col, i_Gameboard));
        }

        private bool checkRandomLocation(int i_Num, ref int io_Row, ref int io_Col, Board i_Gameboard)
        {
            io_Row = i_Num / i_Gameboard.Cols;
            io_Col = i_Num % i_Gameboard.Cols;

            return i_Gameboard.m_Board[io_Row, io_Col].Expose;
        }
    }
}