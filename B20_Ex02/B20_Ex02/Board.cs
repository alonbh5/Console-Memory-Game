using System;
using System.Text;

namespace B20_Ex02
{
    internal class Board        
    {
        internal Tile[,] m_Board = null;
        private int m_Rows = 0;
        private int m_Cols = 0;
        private int m_TotalPairs = 0;
        private int m_ExposesPairs = 0;

        internal Board(int i_Rows, int i_Cols)
        {
            if ((i_Rows * i_Cols) % 2 != 0)
            { 
                // Internal check for board size (in addition to UI check)
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

        internal int Rows
        {
            get { return m_Rows; }
        }

        internal int Cols
        {
            get { return m_Cols; }
        }

        private void buildBoard()
        { // Randomize GameBoard with Pairs            
            Random rnd = new Random();
            int row = 0, col = 0, loc1 = 0, loc2 = 0;
            int maxRnd = m_Cols * m_Rows;

            for (int pair = 1; pair <= m_TotalPairs; pair++) 
            {
                do
                {
                    loc1 = rnd.Next(maxRnd);
                }
                while (!intToLocation(loc1, ref row, ref col));

                m_Board[row, col].Value = pair;

                do
                {
                    loc2 = rnd.Next(maxRnd);
                }
                while (!intToLocation(loc2, ref row, ref col));

                m_Board[row, col].Value = pair;
            }
        }

        private bool intToLocation(int i_Num, ref int io_Row, ref int io_Col)
        {
            //// Get num that represent a sqaure in metrix and return correct row, col by ref
            //// For Example 0= > [0,0] , 1 => [0,1] ...

            io_Row = i_Num / m_Cols;
            io_Col = i_Num % m_Cols;

            return m_Board[io_Row, io_Col].Value.Equals(0);
        }

        internal StringBuilder ToStringBuilder(object[] i_PairArr)   
        {
            //// Gets an array of object for whom object[i]= pair #i
            //// NOTICE: object X in array of objects must have ToString Function!!
            //// Function returns a StringBuilder represantion of Gameboard

            StringBuilder boardToPrint = new StringBuilder();
            int rowToPrints = m_Rows + 1;
            int colsToPrints = m_Cols + 1;
            char colLetter = 'A';
            int numOfSpaces = 7;

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
                                int index = m_Board[i - 1, j - 1].Value;
                                boardToPrint.Append(i_PairArr[index - 1].ToString());
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
                boardToPrint.Append('=', numOfSpaces * (colsToPrints - 1));
                boardToPrint.Append("\n");
            }

            return boardToPrint;
        }

        internal bool IsGameOver()
        {
            return m_ExposesPairs == m_TotalPairs;
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
            return m_Board[i_Row1, i_Col1].Value == m_Board[i_Row2, i_Col2].Value;
        }
    }
}