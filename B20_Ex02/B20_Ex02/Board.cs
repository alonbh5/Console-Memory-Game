using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B20_Ex02
{
    class Board
    {
        internal Tile[,] m_Board;
        int m_Rows;
        int m_Cols;
        int m_TotalPairs;
        int m_ExposesPairs;

        internal Board(int i_Rows, int i_Cols)
        {
            if (i_Rows * i_Cols % 2 != 0)
            { // Internal check for board size (in addition to UI check)
              // Set default to 4*4
                i_Rows = 4;
                i_Cols = 4;
            }

            m_Board = new Tile[i_Rows, i_Cols];
            m_Rows = i_Rows;
            m_Cols = i_Cols;
            m_TotalPairs = (i_Rows * i_Cols) / 2;
            m_ExposesPairs = 0;

            buildBoard();
        }

        public int Rows
        {
            get { return m_Rows; }
        }

        public int Cols
        {
            get { return m_Cols; }
        }

        private void buildBoard()
        {
            Random rnd = new Random();
            int row = 0, col = 0, loc1 = 0, loc2 = 0;
            int maxRnd = m_Cols * m_Rows;

            for (int pair = 1; pair <= m_TotalPairs; pair++)
            {
                do
                {
                    loc1 = rnd.Next(maxRnd);
                } while (!intToLocation(loc1, ref row, ref col));

                m_Board[row, col].Value = pair;

                do
                {
                    loc2 = rnd.Next(maxRnd);
                } while (!intToLocation(loc2, ref row, ref col));

                m_Board[row, col].Value = pair;
            }
        }

        private bool intToLocation(int i_Num, ref int io_Row, ref int io_Col)
        {
            io_Row = i_Num / m_Cols;
            io_Col = i_Num % m_Cols;

            return (m_Board[io_Row, io_Col].Value.Equals(0));
        }

        public StringBuilder ToStringBuilder(object[] i_PairArr)   // For UI
        {
            StringBuilder boardToPrint = new StringBuilder();
            int rowToPrints = m_Rows + 1;
            int colsToPrints = m_Cols + 1;
            char colLetter = 'A';
            int index = 0;

            for (int i = 0; i < rowToPrints; i++)
            {
                for (int j = 0; j < colsToPrints; j++)
                {
                    if (i == 0)
                    {
                        if (j == 0)
                        {
                            boardToPrint.Append("      ");
                        }
                        else
                        {
                            boardToPrint.Append(colLetter);
                            boardToPrint.Append("      ");
                            colLetter++;
                        }
                    }
                    else
                    {
                        if (j == 0)
                        {
                            boardToPrint.Append(" ");
                            boardToPrint.Append(i);
                            boardToPrint.Append(" |");
                        }
                        else
                        {
                            if (m_Board[i - 1, j - 1].Expose)
                            {
                                boardToPrint.Append("  ");
                                //char sign = 'A';// + m_Board[i - 1, j - 1].Value;
                                //object sign = i_firstPair;
                                //sign--;
                                index = m_Board[i - 1, j - 1].Value;
                                boardToPrint.Append(i_PairArr[index-1].ToString());
                                boardToPrint.Append("   |");
                            }
                            else
                            {
                                boardToPrint.Append("      |");
                            }
                        }
                    }
                }

                boardToPrint.Append("\n   ");
                boardToPrint.Append('=', 7 * (colsToPrints - 1));
                boardToPrint.Append("\n");
            }

            return (boardToPrint);
        }

        public bool IsGameOver()
        {
            return (m_ExposesPairs == m_TotalPairs);
        }

        internal void PairFound()
        {
            if (m_TotalPairs != m_ExposesPairs)
            {
                m_ExposesPairs++;
            }
        }

        internal void Expose(int i_Row, int i_Col)
        {
            m_Board[i_Row, i_Col].Expose = true;
        }

        internal void Unexpose(int i_Row, int i_Col)
        {
            m_Board[i_Row, i_Col].Expose = false;
        }

        internal bool CheckPair(int i_Row1, int i_Col1, int i_Row2, int i_Col2)
        {
            return (m_Board[i_Row1, i_Col1].Value == m_Board[i_Row2, i_Col2].Value);
        }
    }
}