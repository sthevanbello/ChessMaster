using System;
using System.Collections.Generic;
using ChessMaster.BoardChess;
using System.Text;
using System.Threading.Tasks;

namespace ChessMaster
{
    class Screen
    {

        public static void PrintBoard(Board board)
        {

            for (int i = 0; i < board.Rows; i++)
            {
                for (int j = 0; j < board.Columns; j++)
                {
                    if (board.Peace(i, j) == null)
                    {
                        Console.Write("_ ");
                    }
                    else
                    {
                        Console.Write($"{board.Peace(i, j)} ");
                    }
                }
                Console.WriteLine();

            }

        }

    }
}
