using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B20_Ex02
{
    class Board
    {
        Tile[,] m_Board;
        int m_Rows;
        int m_Cols;
        int m_TotalPairs;
        int m_ExposesPairs;

        internal Board(int rows, int cols)
        {
            m_Board = new Tile[rows, cols];
            m_Rows = rows;
            m_Cols = cols;
            m_TotalPairs = (rows * cols) / 2;
            m_ExposesPairs = 0;

            BuildBoard();
        }

        public int Rows
        {
            get { return m_Rows; }
        }

        public int Cols
        {
            get { return m_Cols; }
        }

        private void BuildBoard()
        {
            Random rnd = new Random();
            int row = 0, col = 0, loc1 = 0, loc2 = 0;
            int maxRnd = m_Cols * m_Rows;

            for (int pair = 1; pair <= m_TotalPairs; pair++)
            {
                do
                {
                    loc1 = rnd.Next(maxRnd);
                } while (!IntToLocation(loc1, ref row, ref col));
                m_Board[row, col].Value = pair;

                do
                {
                    loc2 = rnd.Next(maxRnd);
                } while (!IntToLocation(loc2, ref row, ref col));
                m_Board[row, col].Value = pair;

            }
        }

        private bool IntToLocation(int i_Num, ref int io_Row, ref int io_Col)
        {
            io_Row = i_Num / m_Cols;
            io_Col = i_Num % m_Cols;

            return (m_Board[io_Row, io_Col].Value.Equals(0));
        }

        public StringBuilder ToStringBuilder()   // For UI
        {
            StringBuilder boardToPrint = new StringBuilder();
            int rowToPrints = m_Rows + 1;
            int colsToPrints = m_Cols + 1;
            char colLetter = 'A';
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
                                char sign = 'A';// + m_Board[i - 1, j - 1].Value;
                                sign--;
                                sign += (char)m_Board[i - 1, j - 1].Value;

                                boardToPrint.Append(sign);
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

        public bool isGameOver()
        {
            return (m_ExposesPairs == m_TotalPairs);
        }


        internal void Expose(int o_Row, int o_Col)
        {
            // m_GameBoard[o_Row,o_Col]
        }
    }
}

