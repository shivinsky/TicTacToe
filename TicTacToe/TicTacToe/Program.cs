using System;

namespace TicTacToe
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (TicTacToe game = new TicTacToe())
            {
                game.Run();
            }
        }
    }
}

