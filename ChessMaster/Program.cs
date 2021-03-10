﻿using ChessMaster.BoardChess;
using ChessMaster.Chess;
using System;

namespace ChessMaster
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                GameChess game = new GameChess();
                while (!game.Finished)
                {
                    Console.Clear();
                    Screen.PrintBoard(game.Board);

                    

                    Console.Write("\n\nOrigin: ");
                    Position origin = Screen.ReadPositionChess().ToPosition();

                    Console.Write("Destiny: ");
                    Position destiny = Screen.ReadPositionChess().ToPosition();

                    game.MoveExecute(origin, destiny);
                }

            }
            catch (BoardException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            Console.ReadKey();
        }
    }
}
