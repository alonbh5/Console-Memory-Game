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

        public void PlayGame()
        {
            bool turnPlayer1 = true;
            Player CurrentPlayer=m_Game.Player1;
            bool pvc = false;
            string prompt = string.Empty;
            int row1 = 0, col1 = 0, row2 = 0, col2 = 0;

            printGameBoard();

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
                Console.WriteLine("{0}'s turn:\n", CurrentPlayer.Name);
                getInput(pvc,ref row2, ref col2);
                m_Game.Revele(row2, col2);                
                printGameBoard();

                if (!m_Game.checkTurn(row1, col1, row2, col2, CurrentPlayer))
                {
                    turnPlayer1 = !turnPlayer1;
                    if (m_Game.Player2.Pc)
                    {
                        if (!pvc)
                        {
                            //case not won and not AI turn
                            m_Game.Player2.m_pvc.updateMemory(row1, col1, m_Game.m_GameBoard.m_Board[row1, col1]);
                        }
                        m_Game.Player2.m_pvc.updateMemory(row2, col2, m_Game.m_GameBoard.m_Board[row2, col2]);
                    }
                }

                printGameBoard();
            }
            if (m_Game.getWinner(out Player winner))
            { //case of tie
                Console.WriteLine("It's a TIE!\nWith {0) point each.", winner.Pairs);
            }
            else
            {
                Console.WriteLine("{0}'s WON!\nWith {1} pairs reveled.", winner.Name, winner.Pairs);
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
                isValid = ValidInput(turn, ref io_Col, ref io_Row);                         
            }
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
        private bool ValidInput(string i_userInput, ref int io_Col, ref int io_Row)
        {
            bool valid = true;
            char maxLetter = (char)(m_Game.m_GameBoard.Cols + 'A'-1);
            char maxNumber = (char)(m_Game.m_GameBoard.Rows + '1'-1);

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
                valid = m_Game.checkTile(io_Col, io_Row);
                if (!valid) 
                {
                    Console.WriteLine("Input tile is already reveld! Try again");
                }
            }

            return valid;
        }
    }
}
