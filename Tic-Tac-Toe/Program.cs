﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Tic_Tac_Toe
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            char playagain = 'y';
            while(playagain == 'y')
            {
                game = new Game();
                game.gameLoop();
                Console.WriteLine("Do you want to play again? y/n: ");
                playagain = Console.ReadKey().KeyChar;
            }
            
            Console.ReadLine();
        }
    }
}
