using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            get { return Player1; }
        }

        public Player Player2
        {
            get { return Player2; }
        }

    }
}
