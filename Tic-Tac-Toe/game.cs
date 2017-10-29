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
        private bool makeMove(int a, int b, bool player1) // a = row, b = column, c = player( either 1 or 2). returns -1 if move is illegal.
        {
            int c = player1 ? 1 : 2;
            if(board[a,b] == 0)
            {
                board[a, b] = c;
                return true;
            }
            return false;
        }

        private bool move(string plmove, bool player1)
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
        private int checkHorizontals()  //Checks if there is horizontal win, if there is, it returns row. else, returns -1.
        {
            for (int i = 0; i < 3; i++)
                if (board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2] && board[i, 0] != 0)
                    return i;
            return -1; // If there's no win, return -1.

        }

        private int checkVerticals()
        {
            for(int i = 0; i < 3; i++)
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

        private string getInput(ref StreamReader file, bool player1)
        {
            string line = file.ReadLine();
            if (line == null)
                return "null";
            while (!((int)char.GetNumericValue(line[1]) == 1 || (int)char.GetNumericValue(line[1]) == 2) || !(((int)line[4] > 64) && ((int)line[4] < 68)) || !((int)char.GetNumericValue(line[5]) > -1 && (int)char.GetNumericValue(line[5]) < 3) || (int)char.GetNumericValue(line[1]) != (player1 ? 1 : 2))
            {
                if (line == null) // If end of file reached
                    return "null";
                else if (line.Length >= 10 && line.Substring(0, 10) == "000000000") // If end of game reached.
                    return "0000000000";
                line = file.ReadLine();
                if (line == null) // If end of file reached
                    return "null";
                }
            // if this place is reached, that means input is legal. Return move.(does not mean that move is possible).
            return line.Substring(4, 2);

        }
        private int makeSenseOfInput(ref StreamReader file, ref bool player1)
        {
            string input;
            do
            {
                input = getInput(ref file, player1);
                if (input == "00000000")
                {
                    return 0;
                }else if(input == "null")
                {
                    return -1;
                }

            } while (!move(input, player1)); // If move is not possible, repeat.
            // IF this part of code is reached, this means that move is legal, and move was made.
            // This means, I need to invert the player.
            player1 = !player1;
            return 1;
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
                string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                path = Path.GetFullPath(Path.Combine(path, @"..\..\")) + "game.txt";
                if (File.Exists(@path))  
                {
                    System.IO.StreamReader file = new System.IO.StreamReader(@path);
                    string line;
                    bool player1;
                    line = file.ReadLine();                                             // Get first line.
                    player1 = ((int)char.GetNumericValue(line[1]) == 1) ? true : false; // Determine first player.
                    move(line.Substring(4, 2), player1);
                    player1 = !player1;
                    int inp;
                    bool won = false;
                    while (true)
                    {

                        do
                        {
                            inp = makeSenseOfInput(ref file, ref player1);
                            if (inp == -1) // IF null reached.
                            {
                                if (won)
                                {
                                    displayBoard();
                                    won = false;
                                    goto finish;
                                }
                                displayBoard(); //Display the board.
                                goto finish;
                            }
                            else if (inp == 0)
                            {
                                displayBoard(); // Change it with json output.
                                board = new int[3, 3];
                            }
                        } while (inp != 1);
                        if (inp == 0)
                        {
                            displayBoard();
                            board = new int[3, 3];
                            do
                            {
                                line = file.ReadLine();
                                if (line == null)
                                    goto finish;
                            } while (line != "0000000000");
                            line = file.ReadLine();
                            /*if (line == null)
                                goto finish;*/
                            player1 = ((int)char.GetNumericValue(line[1]) == 1) ? true : false; // Determine first player.
                            move(line.Substring(4, 2), player1); // Make Move
                            player1 = !player1; // change Player
                            continue;
                        }
                        if(checkWin() > -1)
                        {
                            displayBoard();
                            board = new int[3, 3];
                            won = true;
                            // As game ends here, read new lines until null or 0000000 is reached.
                            do
                            {
                                line = file.ReadLine();
                                if (line == null)
                                    goto finish;
                            } while (line != "0000000000");
                            line = file.ReadLine();
                            /*if (line == null)
                                goto finish;
                            */
                            player1 = ((int)char.GetNumericValue(line[1]) == 1) ? true : false; // Determine first player.
                            move(line.Substring(4, 2), player1); // Make Move
                            player1 = !player1; // change Player
                            continue;
                        }
                    }
                finish:
                    board = new int[3, 3];
                }
                else
                {
                    Console.WriteLine($"{path} Does Not Exist!\n make sure text file is not named game.txt.txt instead of game.txt.\n");
                }
            }
            else // 
            {
                Random r = new Random();
                bool player1 = (r.Next(1, 2) == 1)? true:false;//Choose player randomly.
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
