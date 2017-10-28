using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Sandro Jijavadze - 819819553
/// The game class for tic tac toe.
/// Members: board,
/// </summary>

namespace Tic_Tac_Toe
{
    class Game
    {
        private int[,] board = new int[3, 3];  // Initialize empty board.
        public bool makeMove(int a, int b, bool player1) // a = row, b = column, c = player( either 1 or 2). returns -1 if move is illegal.
        {
            int c = player1 ? 1 : 2;
            if(board[a,b] == 0)
            {
                board[a, b] = c;
                return true;
            }
            return false;
        }

        public bool move(string plmove, bool player1)
        {
            int row;
            if (plmove[0] == 'A')
                row = 0;
            else if (plmove[0] == 'B')
                row = 1;
            else if (plmove[0] == 'C')
                row = 2;
            else
                return false;
            int column = (int)char.GetNumericValue(plmove[1]);
            if (!(makeMove(row, column, player1)))
                return false;
            else
                return true;
        }
        public int checkHorizontals()  //Checks if there is horizontal win, if there is, it returns row. else, returns -1.
        {
            for (int i = 0; i < 3; i++)
                if (board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2] && board[i, 0] != 0)
                    return i;
            return -1; // If there's no win, return -1.

        }

        public int checkVerticals()
        {
            for(int i = 0; i < 3; i++)
                    if (board[0, i] == board[1, i] && board[1, i] == board[2, i] && board[0, i] != 0)
                        return i;
            return -1; // If there's no win, return -1.
        }

        public bool checkDiagonals() // Returns 1 if there is a diagonal win. Returns -1 otherwise.
        {
            if (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2] && board[0, 0] != 0)
                return true;
            else if (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0] && board[0, 0] != 0)
                return true;
            return false;
        }

        public int checkWin() // Returns player number , 1 or 2, or -1 if nobody wins.
        {
            int i;
            if (checkHorizontals() != -1)
                return board[checkHorizontals(), 0];
            else if (checkVerticals() != -1)
                return board[0, checkVerticals()];
            else if (checkDiagonals())
                return board[1, 1];
            return -1;
        }

        public void displayBoard()
        {
            for (int i = 0; i < 3; i++)
                Console.WriteLine($"{board[i, 0]} {board[i, 1]} {board[i, 2]}");
        }

        public void gameLoop()
        {
            char gamemode;
            Console.WriteLine("NOTE: If you choose 2 player mode, 1st player will be chosen randomly.");
            Console.Write("If you want to use game.txt file, enter y, if you want 2 player game, enter n:");
            gamemode = Console.ReadKey().KeyChar;
            if (gamemode == 'y') // Game.txt exists.
            {
            }
            else // 
            {
                Random r = new Random();
                bool player1 = (r.Next(1, 2) == 1)? true:false;//Choose player randomly.
                int i = 0;
                string playerMove = "";
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine($"Last Move: {playerMove}");//Display last move and board.
                    displayBoard();
                    Console.WriteLine($"PLayer {(player1? 1:2)}, enter a move:");//Promt for move.
                    playerMove = Console.ReadLine();

                    while(!(move(playerMove, player1)))//While move  is illegal.
                    {
                        Console.WriteLine("Invalid Move! Enter another move:"); // Ask for new move.
                        playerMove = Console.ReadLine();
                    }
                    if(checkWin() > 0)
                    {
                        Console.Clear();
                        displayBoard();
                        Console.WriteLine($"Player{(player1 ? 1 : 2)} won the game!");
                        break;
                    }
                    player1 = !player1;
                }
            }
        }
    }
}
