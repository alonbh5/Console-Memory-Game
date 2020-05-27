using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace B20_Ex02
{
    class ConsoleUI
    {
        
        private Game m_Game;
        private object[] m_CharsToPrint = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R' };

         ConsoleUI()
        {
            string player1Name, player2Name;
            bool pvc = false;
            int row = 0, col = 0, choice = 0;
            
            Console.WriteLine("Welcome to Memory Game! \nPlease Enter Your Name:");
            player1Name = Console.ReadLine();

            while (choice != 1 && choice != 2)
            {
                Console.WriteLine("Press 1 to play Player Vs Player \nPress 2 to play Player Vs PC");
                int.TryParse(Console.ReadLine(), out choice);
            }

            if (choice == 1)
            {
                Console.WriteLine("Please Enter Other Player Name:");
                player2Name = Console.ReadLine();
            }
            else
            {
                player2Name = "GUY RONEN";
                pvc = true;
            }

            getBoardSize(ref row, ref col);

            m_Game = new Game(player1Name, player2Name, pvc, row, col);
        }

        private void getBoardSize(ref int io_Col, ref int io_Row)
        {
            do
            {
                io_Row = 0;
                io_Col = 0;

                while (io_Row < 4 || io_Row > 6)
                {
                    Console.WriteLine("Enter number of Rows (Between 4-6)");
                    int.TryParse(Console.ReadLine(), out io_Row);
                }

                while (io_Col < 4 || io_Col > 6)
                {
                    Console.WriteLine("Enter number of Cols (Between 4-6)");
                    int.TryParse(Console.ReadLine(), out io_Col);
                }

                if ((io_Row * io_Col) % 2 != 0)
                {
                    Console.WriteLine("The number of board tiles must be even!");
                }

            } while ((io_Row * io_Col) % 2 != 0);
        }

        private void playGame()
        {
            bool turnPlayer1 = true;
            string prompt = string.Empty;
            int row1 = 0, col1 = 0, row2 = 0, col2 = 0;

            while (!m_Game.IsGameOver())
            {
                printGameBoard();
                reveleTile(ref row1, ref col1, turnPlayer1);
                reveleTile(ref row2, ref col2, turnPlayer1);
                m_Game.CheckTurn(row1, col1, row2, col2, ref turnPlayer1);
            }
            printScore();
        }

        private void reveleTile(ref int io_Row, ref int io_Col, bool io_TurnPlayer1)
        {
            string playerName = m_Game.Player1Name();

            if (!io_TurnPlayer1)
            {
                playerName = m_Game.Player2Name();
            }

            Console.WriteLine("{0}'s turn:\n", playerName);
      
            getInput(ref io_Row, ref io_Col, io_TurnPlayer1);
            m_Game.Revele(io_Row, io_Col, io_TurnPlayer1);
            printGameBoard();
        }

        private void printScore()
        {
            if (m_Game.GetWinner(out string winner))
            { //case of tie
                Console.WriteLine("It's a TIE!");
            }
            else
            {
                Console.WriteLine("{0} WON!", winner);
            }
            Console.WriteLine("{0} with {1} pairs reveled.\n{2} with {3} pairs reveled.",
                m_Game.Player1Name(),
                m_Game.Player1Score(),
                m_Game.Player2Name(),
                m_Game.Player2Score());
        }

        private void getInput(ref int io_Row, ref int io_Col,bool io_TurnPlayer1)
        {
            if (!io_TurnPlayer1 && m_Game.IsAIPlay())
            {
                Thread.Sleep(1000);
                m_Game.GetInputFromAI(ref io_Row, ref io_Col);
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
            Console.WriteLine("Enter play (Enter 'Q' to Quit)");
            while (!isValid)
            {
                turn = Console.ReadLine();
                isValid = validInput(turn, ref io_Col, ref io_Row);                         
            }
        }

        private void printGameBoard()
        {            
            Ex02.ConsoleUtils.Screen.Clear();
            Console.WriteLine(m_Game.ToStringBuilder(m_CharsToPrint));
        }

        private bool validInput(string i_userInput, ref int io_Col, ref int io_Row)
        {
            bool valid = true;
            char maxLetter = (char)(m_Game.BoardCols() + 'A'-1);
            char maxNumber = (char)(m_Game.BoardRows() + '1'-1);

            if (i_userInput == "Q")
            {
                Console.WriteLine("Bye Bye!\nexit game.");
                System.Environment.Exit(1);
            }

            if (i_userInput.Length > 2 || i_userInput.Length == 0)
            {
                Console.WriteLine("Input must be 'col-row' (for example: 'A1')");
                valid = false;
            }
            else
            {
                if (i_userInput[0] > maxLetter || i_userInput[0] < 'A')
                {
                    Console.WriteLine("Input col must be in board size ('A'-'{0}')", maxLetter);
                    valid = false;
                }
                if (i_userInput[1] > maxNumber || i_userInput[1] < '1')
                {
                    Console.WriteLine("Input row must be in board size ('1'-'{0}')", maxNumber);
                    valid = false;
                }
            }

            if(valid)
            {
                int.TryParse((i_userInput[0] - 'A').ToString(), out io_Col);
                int.TryParse((i_userInput[1] - '1').ToString(), out io_Row);
                valid = m_Game.CheckTile(io_Col, io_Row);
                if (!valid) 
                {
                    Console.WriteLine("Input tile is already reveld! Try again");
                }
            }

            return valid;        
        }

        public static void RunMainMenu ()
        {
            bool play = true;
            char input = ' ';
            while (play)
            {
                Ex02.ConsoleUtils.Screen.Clear();
                ConsoleUI Mygame = new ConsoleUI();
                Mygame.playGame();


                Console.WriteLine("Do you wish to play again? \nType 'Y' for Yes \nType 'N' for No");
                while (!char.TryParse(Console.ReadLine(), out input) && input != 'Y' && input != 'N') 
                {
                    Console.WriteLine("Wrong input!! \nType 'Y' for Yes \nType 'N' for No");
                }
                
                if (input == 'N')
                {
                    play = false;
                }
            }

        }
    }
}
