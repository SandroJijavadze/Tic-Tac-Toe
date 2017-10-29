using System;
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
                Console.WriteLine("\n\nEither game finished or file has invalid input.");
                Console.WriteLine("Do you want to try again? y/n: ");
                playagain = Console.ReadKey().KeyChar;
            }
            /*
            int a = (int)'a';
            Console.WriteLine(a);
            Console.ReadLine();
            */
        }
    }
}
