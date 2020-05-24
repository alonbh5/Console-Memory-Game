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
            bool turn = true;
            bool pvc = false;
            string prompt = string.Empty;
            int row1 = 0, col1 = 0, row2 = 0, col2 = 0;

            printGameBoard();

            //from here move to GAME
            while (!m_Game.m_GameBoard.isGameOver())
            {
                if (turn)
                {
                    Console.WriteLine("{0}'s turn:\n", m_Game.Player1.Name);
                    pvc = false;
                }
                else
                {
                    Console.WriteLine("{0}'s turn:\n", m_Game.Player2.Name);  ///if pc AI
                    if (m_Game.Player2.Pc)
                        pvc = true;
                }

                getInput(pvc,ref row1, ref col1);
                m_Game.Revele(row1, col1);
                printGameBoard();
                getInput(pvc,ref row2, ref col2);
                m_Game.Revele(row2, col2);
                printGameBoard();

                // put in Game m_Game.Expose(row, col); //expose (board) -> print(UI) ->expose -> print ->check pair (board) 
                //if check=true -> pair++ (board and player)
                //else unexpose->print

                if (turn)
                {                    
                    if(!m_Game.checkTurn(row1, col1, row2, col2, m_Game.Player1))
                    {
                        turn = !turn;
                    }

                }
                else 
                {
                    if (!m_Game.checkTurn(row1, col1, row2, col2, m_Game.Player2))
                    {
                        turn = !turn;
                    }
                }
                Ex02.ConsoleUtils.Screen.Clear();
                Console.WriteLine(m_Game.m_GameBoard.ToStringBuilder());
                
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

        public void printGameBoard ()
        {
            Ex02.ConsoleUtils.Screen.Clear();
            Console.WriteLine(m_Game.m_GameBoard.ToStringBuilder());
        }
    }
}
