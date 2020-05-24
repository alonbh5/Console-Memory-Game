using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;


namespace B20_Ex02
{
    class Game
    {
        Player m_Player1;
        Player m_Player2;
        internal Board m_GameBoard;

        internal Game(string name1, string name2, bool pvc, int row, int col)
        {
            if ((row * col) % 2 != 0)
            {
                row++;
            }

            m_Player1 = new Player(name1, false);
            m_Player2 = new Player(name2, pvc);
            m_GameBoard = new Board(row, col);
        }

        public Player Player1
        {
            get { return m_Player1; }
        }

        public Player Player2
        {
            get { return m_Player2; }
        }

        internal void Revele(int i_Row, int i_Col)
        {
            m_GameBoard.Expose(i_Row, i_Col);
        }
        internal void Unrevele(int i_Row1, int i_Col1, int i_Row2, int i_Col2)
        {
            m_GameBoard.Unexpose(i_Row1, i_Col1);
            m_GameBoard.Unexpose(i_Row2, i_Col2);
        }

        internal bool checkTurn(int i_Row1, int i_Col1, int i_Row2, int i_Col2,Player i_player)
        {
            bool res = false;
            if (m_GameBoard.checkPair(i_Row1,i_Col1,i_Row2,i_Col2))
            {
                i_player.Pairs++;
                m_GameBoard.PairFound();
                res = true;
            }
            else
            {
                Thread.Sleep(2000);
                Unrevele(i_Row1, i_Col1, i_Row2, i_Col2);
            }

            return res;
        }

    }
}
