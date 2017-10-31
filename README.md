<h1>Tic-Tac-Toe</h1>
2 player, command-line, auto-play from .txt  file.

<b>2 player version:</b> </br>
Input format for moves:  "A1" - first row, first column, "A2" - first row, second column.</br>
Letters are rows (A => 0, B => 1, C=> 2), digits are columns.</br></br>

<b>game.txt</b> </br>
This is for professor to test the tic-tac-toe code using custom input moves for player 1 and 2.</br>
For it to work, place game.txt in the directory with .cs files. </br>

While there is no game.txt, the program will ask user if he wants to try playing again.</br>

It will ask again, if player wants 2 player mode or .txt file.

Format of input moves:</br>

P1: B1</br>
P2: B2</br>
P1: C2</br>
P2: A1</br>
P1: C1</br>
0000000000</br></br>

NOTE: "0000000000" this denotes end of the game.</br>

In case of playing in game.txt mode, the games are output in json array representation as such:</br>
[[1,0,2],[1,1,2],[2,0,1]]</br>
[[0,0,0],[0,0,0],[2,1,2]]</br> 
</br>Each line is the last state of board before winning or ending using "0000000000"</br>
