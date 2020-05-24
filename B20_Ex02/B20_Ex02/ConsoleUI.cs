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
            Console.WriteLine("Welcome to Game! \n Please Enter Your Name:");
            player1Name = Console.ReadLine();
            Console.WriteLine("Press 1 to play Player Vs Player \n Press 2 to play Player Vs PC"); //Check
            int choice = int.Parse(Console.ReadLine());


            if (choice == 1)
            {
                Console.WriteLine("Please Enter Other Player  Name:");
                player2Name = Console.ReadLine();
            }
            else
            {
                player2Name = "Guy Ronen";
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
            string prompt = string.Empty;
            int row = 0, col = 0;

            //from here move to GAME
            while (!m_Game.m_GameBoard.isGameOver())
            {
                if (turn)
                {
                    Console.WriteLine("{0}'s turn:\n", m_Game.Player1.Name);

                }
                else
                {
                    Console.WriteLine("{0}'s turn:\n", m_Game.Player2.Name);  ///if pc AI
                }
                getInputFromUser(ref row, ref col);
                // put in Game m_Game.Expose(row, col); //expose (board) -> print(UI) ->expose -> print ->check pair (board) 

                if (turn)
                {
                    //if check=true -> pair++ (board and player)
                    //else unexpose->print
                }
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
            }
            int.TryParse((turn[0] - 'A').ToString(), out io_Col);
            int.TryParse((turn[1] - '1').ToString(), out io_Row);
        }
    }
}
