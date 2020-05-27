using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

//To Play Game:
///While (IsGameOver)
///{  
///     1.First Revele (row col)
///     2.Second Revele(row col)
///     3.Check Turn
/// }
/// To get winner use getWinner 
/// to get score use  getScore
/// to Get StringBuilde of the Gameboard use ToStringBuilder (Array of Object that use ToString (object[i]=pair # i))

namespace B20_Ex02
{
    class Game
    {
        Player m_Player1;
        Player m_Player2;
        Board m_GameBoard;

        internal Game(string i_Name1, string i_Name2, bool i_Pvc, int i_Row, int i_Col)
        {
            m_Player1 = new Player(i_Name1, false);
            m_Player2 = new Player(i_Name2, i_Pvc);
            m_GameBoard = new Board(i_Row, i_Col);
        }

        internal string Player1Name()
        {
            return m_Player1.Name;
        }

        internal string Player2Name()
        {
            return m_Player2.Name;
        }

        internal int Player1Score()
        {
            return m_Player1.Pairs;
        }

        internal int Player2Score()
        {
            return m_Player2.Pairs;
        }

        internal int BoardCols()
        {
            return m_GameBoard.Cols;
        }

        internal int BoardRows()
        {
            return m_GameBoard.Rows;
        }

        internal void FirstRevele(int i_Row, int i_Col,bool i_TurnPlayer1)
        {
            m_GameBoard.Expose(i_Row, i_Col);

            if (m_Player2.Pc && !i_TurnPlayer1) 
            { // This is turn of player 2 and it is AI -> need to update memeory of AI.
                m_Player2.m_PlayerVsComputer.updateMemory(i_Row, i_Col,m_GameBoard.m_Board[i_Row,i_Col]);
            }
        }

        internal void SecondRevele(int i_Row, int i_Col, bool i_TurnPlayer1)
        {
            m_GameBoard.Expose(i_Row, i_Col);
        }

        internal void GetInputFromAI(ref int io_Row, ref int io_Col)
        {
            
            m_Player2.m_PlayerVsComputer.PlayTurn(ref io_Row, ref io_Col, m_GameBoard);
        }

        private void unrevele(int i_Row1, int i_Col1, int i_Row2, int i_Col2)
        {
            m_GameBoard.Unexpose(i_Row1, i_Col1);
            m_GameBoard.Unexpose(i_Row2, i_Col2);
        }

        internal void CheckTurn(int i_Row1, int i_Col1, int i_Row2, int i_Col2, ref bool io_TurnPlayer1)
        {
            bool changePlayer = false;
            Player currentPlayer = m_Player1;

            if (!io_TurnPlayer1)
            {
                currentPlayer = m_Player2;
            }

            if (m_GameBoard.CheckPair(i_Row1, i_Col1, i_Row2, i_Col2))
            {// CASE FOUND PAIR
                currentPlayer.Pairs++;
                m_GameBoard.PairFound();                
            }
            else
            {
                Thread.Sleep(2000);
                unrevele(i_Row1, i_Col1, i_Row2, i_Col2);
                changePlayer = true;
            }

            if (changePlayer)
            {               
                if (m_Player2.Pc)
                {
                    if (io_TurnPlayer1)
                    { // case not won and not AI turn
                        m_Player2.m_PlayerVsComputer.updateMemory(i_Row1, i_Col1, m_GameBoard.m_Board[i_Row1, i_Col1]);
                    }
                    m_Player2.m_PlayerVsComputer.updateMemory(i_Row2, i_Col2, m_GameBoard.m_Board[i_Row2, i_Col2]);
                }

                io_TurnPlayer1 = !io_TurnPlayer1;
            }
        }

        internal bool GetWinner(out string o_Winner)
        {
            bool isTie = false;

            if (m_Player1.Pairs == m_Player2.Pairs)
            {
                isTie = true;
                o_Winner = m_Player1.Name;
            }

            if (m_Player1.Pairs > m_Player2.Pairs)
            {
                o_Winner = m_Player1.Name;
            }
            else
            {
                o_Winner = m_Player2.Name;
            }

            return isTie;
        }

        internal bool CheckTile(int io_Col, int io_Row)
        {
            return (!m_GameBoard.m_Board[io_Row, io_Col].Expose);
        }

        internal bool IsGameOver()
        {
            return m_GameBoard.IsGameOver();
        }

        internal bool IsAIPlay()
        {
            return m_Player2.Pc;
        }

        internal StringBuilder ToStringBuilder(object[] io_CharsToPrint)
        {
            return m_GameBoard.ToStringBuilder(io_CharsToPrint);
        }
    }
}