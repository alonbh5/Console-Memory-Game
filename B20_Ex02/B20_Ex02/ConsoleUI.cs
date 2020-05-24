using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B20_Ex02
{
    class ConsoleUI
    {
        Game m_Game;

        public ConsoleUI()
        {
            string player1Name, player2Name;
            bool pvc = false;
            Console.WriteLine("Welcome to Game! \nPlease Enter Your Name:");
            player1Name = Console.ReadLine();
            Console.WriteLine("Press 1 to play Player Vs Player \nPress 2 to play Player Vs PC"); //Check
            int choice = int.Parse(Console.ReadLine());


            if (choice == 1)
            {
                Console.WriteLine("Please Enter Other Player  Name:");
                player2Name = Console.ReadLine();
            }
            else
            {
                player2Name = "GUY RONEN";
                pvc = true;
            }

            
            Console.WriteLine("Enter number of Rows (Between 4-6)"); //Check
            int row = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter number of Cols (Between 4-6)"); //Check
            int col = int.Parse(Console.ReadLine());

            m_Game = new Game(player1Name, player2Name, pvc, row, col);
        }

        public void PlayGame()
        {
            bool turnPlayer1 = true;
            Player CurrentPlayer=m_Game.Player1;
            bool pvc = false;
            string prompt = string.Empty;
            int row1 = 0, col1 = 0, row2 = 0, col2 = 0;

            printGameBoard();

            //from here move to GAME
            while (!m_Game.m_GameBoard.isGameOver())
            {
                if (turnPlayer1)
                {
                    CurrentPlayer = m_Game.Player1;                    
                    pvc = false;
                }
                else
                {
                    CurrentPlayer = m_Game.Player2;                    
                    if (m_Game.Player2.Pc)
                    {
                        pvc = true;
                    }
                }
                Console.WriteLine("{0}'s turn:\n", CurrentPlayer.Name);
                getInput(pvc,ref row1, ref col1);
                m_Game.Revele(row1, col1);
                printGameBoard();
                if (pvc && !turnPlayer1)
                {
                    //this is turn of player 2 and player 2= AI ->need to update memeory of Ai
                    m_Game.Player2.m_pvc.updateMemory(row1, col1, m_Game.m_GameBoard.m_Board[row1, col1]);

                }                
                getInput(pvc,ref row2, ref col2);
                m_Game.Revele(row2, col2);                
                printGameBoard();

                


                if (!m_Game.checkTurn(row1, col1, row2, col2, CurrentPlayer))
                {
                    turnPlayer1 = !turnPlayer1;
                    if (!pvc)
                    {
                        //case not won and not AI turn
                        m_Game.Player2.m_pvc.updateMemory(row1, col1, m_Game.m_GameBoard.m_Board[row1, col1]);
                    }
                    m_Game.Player2.m_pvc.updateMemory(row2, col2, m_Game.m_GameBoard.m_Board[row2, col2]);
                }



                printGameBoard();

            }
            
        }

        private void getInput(bool pvc, ref int io_Row, ref int io_Col)
        {
            if (pvc)
            {
                getInputFromAI(ref io_Row, ref io_Col);
            }
            else
            {
                getInputFromUser(ref io_Row, ref io_Col);
            }
        }

        private void getInputFromUser(ref int io_Row, ref int io_Col)
        {
            string turn = string.Empty;
            bool isValid = false;
            Console.WriteLine("Enter play");
            while (!isValid)
            {
                turn = Console.ReadLine();
                //isValid = checkTurn(turn);
                //check turn
                isValid = true;
            }
            int.TryParse((turn[0] - 'A').ToString(), out io_Col);
            int.TryParse((turn[1] - '1').ToString(), out io_Row);
        }
        private void getInputFromAI(ref int io_Row, ref int io_Col)
        {
            m_Game.Player2.m_pvc.PlayTurn(ref io_Row, ref io_Col, m_Game.m_GameBoard);
        }
        public void printGameBoard ()
        {
            Ex02.ConsoleUtils.Screen.Clear();
            Console.WriteLine(m_Game.m_GameBoard.ToStringBuilder());
        }
    }
}
