using System;
using System.Text;

/// <summary>
/// Author: Robert Price
/// Entry point for the "War" card game - Creates a new WarManager object 
/// Puts application in a loop while set to playing - Boolean "toPlay" is set via a function which will reset the game when set to true so users can play again///
/// </summary>

namespace WarCardGame
{
    class Program
    {
        static void Main(string[] args)
        {
            //Set output encoding to UTF8 so we can use card symbols
            Console.OutputEncoding = Encoding.UTF8;

            WarManager warManager = new WarManager();
            bool toPlay = true;
            while (toPlay)
            {
                warManager.StartGame();
                toPlay = warManager.ResetGame();
            }
        }

    }
}
