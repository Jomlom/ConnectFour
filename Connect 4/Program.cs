using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Connect_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool gameOver = false;
            string[,] board = null;
            int gameBoardColumns = 0;
            int gameBoardRows = 0;
            char winner = ' ';
            char playersTurn = 'X';

            Console.SetWindowSize(180, 42);

            CreateBoard(ref board, ref gameBoardRows, ref gameBoardColumns);

            int maxColumn = gameBoardColumns;
            int maxRow = gameBoardRows;

            WriteBoard(board, maxColumn, maxRow);

            while (!gameOver)
            {
                PlayerTurn(ref board, ref playersTurn, maxColumn, maxRow);
                WriteBoard(board, maxColumn, maxRow);
                CheckBoard(ref winner, ref gameOver, board, maxColumn, maxRow);
                if (gameOver)
                {
                    break;
                }
                PlayerTurn(ref board, ref playersTurn, maxColumn, maxRow);
                WriteBoard(board, maxColumn, maxRow);
                CheckBoard(ref winner, ref gameOver, board, maxColumn, maxRow);
                if (gameOver)
                {
                    break;
                }
            }
            Console.WriteLine($"\n{winner} wins!\n\n");
            while (true);
        }

        static void CreateBoard(ref string[,] board, ref int gameBoardRows, ref int gameBoardColumns)
        {
            Console.WriteLine("How many rows should the board have?");
            while (!int.TryParse(Console.ReadLine(), out gameBoardRows))
            {
                Console.WriteLine("\nPlease eneter a number!\nHow many rows should the board have?");
            }
            if (gameBoardRows > 20)
            {
                gameBoardRows = 20;
                Console.WriteLine("\nThats way too big of a number so it has been reduced to 20!\n");
            }
            gameBoardRows += 1;
            Console.WriteLine("How many columns should the board have?");
            while (!int.TryParse(Console.ReadLine(), out gameBoardColumns)) 
            { 
                Console.WriteLine("\nPlease eneter a number!\nHow many columns should the board have?");
            }
            if (gameBoardColumns > 20)
            {
                gameBoardColumns = 20;
                Console.WriteLine("\nThats way too big of a number so it has been reduced to 20!\n");
            }
            gameBoardColumns += 1;
            board = new string[gameBoardRows, gameBoardColumns];
            for (int i = 0; i < gameBoardRows; i++)
            {
                for (int x = 0; x < gameBoardColumns; x++)
                {
                    board[i,x] = "_";
                }
            }
            for (int i = 0; i < gameBoardRows; i++)
            {
                if (i < 10)
                {
                    board[i, 0] = "0" + Convert.ToString(i);
                }
                else
                {
                    board[i, 0] = Convert.ToString(i);
                }
            }
            for (int i = 0; i < gameBoardColumns; i++)
            {
                if (i < 10)
                {
                    board[0, i] = "0" + Convert.ToString(i);
                }
                else
                {
                    board[0, i] = Convert.ToString(i);
                }
            }
        }

        static void WriteBoard(string[,] board, int maxColumn, int maxRow)
        {
            Console.Write("\n\n");
            for (int i = 0; i < maxRow; i++)
            {
                for (int x = 0; x < maxColumn; x++)
                {
                    Console.Write(board[i, x] + "\t");
                }
                Console.Write("\n\n");
            }
            Console.Write("\n\n");
        }

        static void CheckBoard(ref char winner, ref bool gameOver, string[,] board, int maxColumn, int maxRow)
        {
            if (CheckStraightsDown(ref winner, board, maxColumn, maxRow))
            {
                gameOver = true;
            }
            else if (CheckStraightsRight(ref winner, board, maxColumn, maxRow))
            {
                gameOver = true;
            }
            else if (CheckDiagonalsRight(ref winner, board, maxColumn, maxRow))
            {
                gameOver = true;
            }
            else if (CheckDiagonalsLeft(ref winner, board, maxColumn, maxRow))
            {
                gameOver = true;
            }
        }

        static bool CheckStraightsDown(ref char winner, string[,] board, int maxColumn, int maxRow)
        {
            for (int i = 1; i < maxColumn; i++)
            {
                for (int x = 1; x < maxRow - 3; x++)
                {
                    if (board[x, i] == "X" || board[x, i] == "O")
                    {
                        int inARow = 0;
                        winner = Convert.ToChar(board[x, i]);
                        for (int count = 0; count < 4; count++)
                        {
                            if (board[x + count, i] == Convert.ToString(winner))
                            {
                                inARow++;
                            }
                            else
                            {
                                break;
                            }
                            if (inARow == 4)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        static bool CheckStraightsRight(ref char winner, string[,] board, int maxColumn, int maxRow)
        {
            for (int i = 1; i < maxColumn - 3; i++)
            {
                for (int x = 1; x < maxRow; x++)
                {
                    if (board[x, i] == "X" || board[x, i] == "O")
                    {
                        int inARow = 0;
                        winner = Convert.ToChar(board[x, i]);
                        for (int count = 0; count < 4; count++)
                        {
                            if (board[x, i + count] == Convert.ToString(winner))
                            {
                                inARow++;
                            }          
                            else
                            {
                                break;
                            }
                            if (inARow == 4)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        static bool CheckDiagonalsRight(ref char winner, string[,] board, int maxColumn, int maxRow)
        {
            for (int i = 1; i < maxColumn - 3; i++)
            {
                for (int x = 1; x < maxRow - 3; x++)
                {
                    if (board[x, i] == "X" || board[x, i] == "O")
                    {
                        int inARow = 0;
                        winner = Convert.ToChar(board[x, i]);
                        for (int count = 0; count < 4; count++)
                        {
                            if (board[x + count, i + count] == Convert.ToString(winner))
                            {
                                inARow++;
                            }
                            else
                            {
                                break;
                            }
                            if (inARow == 4)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        static bool CheckDiagonalsLeft(ref char winner, string[,] board, int maxColumn, int maxRow)
        {
            for (int i = maxColumn - 1; i > 3; i--)
            {
                for (int x = 1; x < maxRow -3; x++)
                {
                    if (board[x, i] == "X" || board[x, i] == "O")
                    {
                        int inARow = 0;
                        winner = Convert.ToChar(board[x, i]);
                        for (int count = 0; count < 4; count++)
                        {
                            if (board[x + count, i - count] == Convert.ToString(winner))
                            {
                                inARow++;
                            }
                            else
                            {
                                break;
                            }
                            if (inARow == 4)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        static void PlayerTurn(ref string[,] board, ref char playersTurn, int maxColumn, int maxRow)
        {
            bool turnDone = false;
            int choice;
            int row = maxRow - 1;

            Console.WriteLine($"\nIt is player {playersTurn}'s turn.\nEnter the column you want to slot your chip:");
            while (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("\nThats not a column on the board!\nEnter the column you want to slot your chip:");
            }
            for (int i = 1; i < maxRow; i++)
            {
                if (((i == 1) && (board[i, choice] == "X")) || ((i == 1) && (board[i, choice] == "O")))
                {
                    break;
                }
                else if (board[i, choice] == "X" || board[i, choice] == "O")
                {
                    row = i - 1;
                    turnDone = true;
                    break;
                }
                else if (i == maxRow - 1)
                {
                    row = i;
                    turnDone = true;
                    break;
                }
            }

            while (!turnDone)
            {
                Console.WriteLine("\nThat column is full!\nEnter the column you want to slot your chip:");
                while (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("\nThats not a column on the board!\nEnter the column you want to slot your chip:");
                }
                for (int i = 1; i < maxRow; i++)
                {
                    if (((i == 1) && (board[i, choice] == "X")) || ((i == 1) && (board[i, choice] == "O")))
                    {
                            break;
                    }
                    else if (board[i, choice] == "X" || board[i, choice] == "O")
                    {
                        row = i - 1;
                        turnDone = true;
                        break;
                    }
                    else if (i == maxRow - 1)
                    {
                        row = i;
                        turnDone = true;
                        break;
                    }
                }
            }
            board[row, choice] = Convert.ToString(playersTurn);

            if (playersTurn == 'X')
            {
                playersTurn = 'O';
            }
            else
            {
                playersTurn = 'X';
            }
        }
    }
}