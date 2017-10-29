using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
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
        private bool won = false;
        private int inp;      // status of input.
        private bool player1; // Which player is playing right now.
        private string line;
        private bool firstmove;
        // IF file exists, the file will be assigned, else, it will be nulled.
        private System.IO.StreamReader file = File.Exists(Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"..\..\")) + "game.txt") ? new System.IO.StreamReader(@Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"..\..\")) + "game.txt") : null;
        private bool makeMove(int a, int b) // a = row, b = column, c = player( either 1 or 2). returns -1 if move is illegal.
        {
            int c = player1 ? 1 : 2;
            if (board[a, b] == 0)
            {
                board[a, b] = c;
                return true;
            }
            return false;
        }

        private bool move(string plmove)
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
            if (!(makeMove(row, column)))
                return false;
            else
                return true;
        }
        private int checkHorizontals()  //Checks if there is horizontal win, if there is, it returns row. else, returns -1.
        {
            for (int i = 0; i < 3; i++)
                if (board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2] && board[i, 0] != 0)
                    return i;
            return -1; // If there's no win, return -1.

        }

        private int checkVerticals()
        {
            for (int i = 0; i < 3; i++)
                if (board[0, i] == board[1, i] && board[1, i] == board[2, i] && board[0, i] != 0)
                    return i;
            return -1; // If there's no win, return -1.
        }

        private bool checkDiagonals() // Returns 1 if there is a diagonal win. Returns -1 otherwise.
        {
            if (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2] && board[0, 0] != 0)
                return true;
            else if (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0] && board[0, 0] != 0)
                return true;
            return false;
        }

        private int checkWin() // Returns player number , 1 or 2, or -1 if nobody wins.
        {
            if (checkHorizontals() != -1)
                return board[checkHorizontals(), 0];
            else if (checkVerticals() != -1)
                return board[0, checkVerticals()];
            else if (checkDiagonals())
                return board[1, 1];
            return -1;
        }

        private void displayBoard()
        {
            for (int i = 0; i < 3; i++)
                Console.WriteLine($"{board[i, 0]} {board[i, 1]} {board[i, 2]}");
        }

        private bool act()
        {
            if (inp == -1) // IF null reached.
            {
                if (won)
                {
                    displayBoard();
                    won = false;
                    return true;
                }
                displayBoard(); //Display the board.
                return true;
            }
            else if (inp == 0)
            {
                displayBoard(); // Change it with json output.
                board = new int[3, 3];
                return false;
            }
            return false;
        }

        private string getInput()
        {
            line = file.ReadLine();
            if (line == null)
                return "null";
            while (!((int)char.GetNumericValue(line[1]) == 1 || (int)char.GetNumericValue(line[1]) == 2) || !(((int)line[4] > 64) && ((int)line[4] < 68)) || !((int)char.GetNumericValue(line[5]) > -1 && (int)char.GetNumericValue(line[5]) < 3))
            {
                if (line == null) // If end of file reached
                    return "null";
                else if (line.Length >= 10 && line.Substring(0, 10) == "000000000") // If end of game reached.
                    return "0000000000";
                line = file.ReadLine();
                if (line == null) // If end of file reached
                    return "null";
                if (firstmove)
                {
                    player1 = ((int)char.GetNumericValue(line[1]) == 1) ? true : false;
                    firstmove = false;
                }
                else if((int)char.GetNumericValue(line[1]) != (player1 ? 1 : 2))
                { 
                    continue;
                }
            }
            // if this place is reached, that means input is legal. Return move.(does not mean that move is possible).
            return line.Substring(4, 2);

        }
        private int makeSenseOfInput()
        {
            string input;
            do
            {
                input = getInput();
                if (input == "00000000")
                {
                    return 0;
                } else if (input == "null")
                {
                    return -1;
                }

            } while (!move(input)); // If move is not possible, repeat.
            // IF this part of code is reached, this means that move is legal, and move was made.
            // This means, I need to invert the player.
            player1 = !player1;
            return 1;
        }

        private bool firstMove() // Returns true if end reached.
        {
            if (makeSenseOfInput() == 0) // IF 000000 reached
            {
                if (skipMoves()) return true; // Skip moves and return NULL if end reached.
            }
            // determine who is first player.
            //line = file.ReadLine();
            player1 = ((int)char.GetNumericValue(line[1]) == 1) ? true : false; 
            // Make Move;
            move(line.Substring(4, 2));
            player1 = !player1;
            return false;
        }
        // If won or 0000000 reached, skip to the end of file or 
        private bool skipMoves() {
            do
            {
                line = file.ReadLine();
                if (line == null)
                    return true;
            } while (line != "0000000000");
            return false;
        }
        
        private bool reInit()
        {
            displayBoard();
            board = new int[3, 3];
            if (skipMoves()) return true;
            if (firstMove()) return true;
            return false;
        }
        private bool playFromTextFile()
        {
            firstmove = true;
            if (firstMove()) return true;
            won = false;
            while (true) // This is the game loop.
            {
                // Take input, if game is finished, return
                do
                {
                    inp = makeSenseOfInput(); // Returns move if it is legal.
                    if (act())                // If game is finished. Return.
                        return true;
                } while (inp != 1);

                if (inp == 0) // if 00000000 reached
                {
                    if (reInit()) return true;
                }
                else if (checkWin() > -1)  // In case either player wins.
                {
                    won = true;
                    if (reInit()) return true;
                }
            }
        }

        public void twoPlayerLoop()
        {
            Random r = new Random();
            player1 = (r.Next(1, 2) == 1) ? true : false;//Choose player randomly.
            string playerMove = "";
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Last Move: {playerMove}");//Display last move and board.
                displayBoard();
                Console.WriteLine($"PLayer {(player1 ? 1 : 2)}, enter a move:");//Promt for move.
                playerMove = Console.ReadLine();

                while (!(move(playerMove)))//While move  is illegal.
                {
                    Console.WriteLine("Invalid Move! Enter another move:"); // Ask for new move.
                    playerMove = Console.ReadLine();
                }
                if (checkWin() > 0)
                {
                    Console.Clear();
                    displayBoard();
                    Console.WriteLine($"Player{(player1 ? 1 : 2)} won the game!");
                    break;
                }
                player1 = !player1;

            }

        }
        public void gameLoop()
        {
            char gamemode;
            Console.WriteLine("NOTE: If you choose 2 player mode, 1st player will be chosen randomly.");
            Console.Write("If you want to use game.txt file, enter y, if you want 2 player game, enter n:");
            gamemode = Console.ReadKey().KeyChar;
            Console.WriteLine();
            if (gamemode == 'y') // play using game.txt
            {
                if (file != null)  
                {
                    playFromTextFile();
                    board = new int[3, 3];
                }
                else
                {
                    Console.WriteLine("please place game.txt (not game.txt.txt) in the folder with .cs files\n");
                }
            }
            else //  This is 2 player case.
            {
                twoPlayerLoop();
            }                
        }
    }
}
