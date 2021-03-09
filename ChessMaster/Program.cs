using ChessMaster.BoardChess;
using ChessMaster.Chess;
using System;

namespace ChessMaster
{
    class Program
    {
        static void Main(string[] args)
        {
            ChessPosition position = new ChessPosition('c', 7);

            Console.WriteLine(position);

            Console.WriteLine(position.ToPosition());

            Console.ReadKey();
        }
    }
}
