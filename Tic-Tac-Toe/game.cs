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
    class game
    {
        private int[,] board = new int[3, 3]; 
        public void makeMove(int a, int b, int c) // a = row, b = column, c = player( either 1 or 2).
        {
            if(board[a,b] == 0)
            {
                board[a, b] = c;
            }
            return;
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
        

    }
}
