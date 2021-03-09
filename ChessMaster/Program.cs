using ChessMaster.BoardChess;
using ChessMaster.Chess;
using System;

namespace ChessMaster
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8, 8);

            board.InputPiece(new Rook(board, Colors.Blue), new Position(0, 0));
            board.InputPiece(new Rook(board, Colors.Blue), new Position(1, 3));
            board.InputPiece(new King(board, Colors.Blue), new Position(2, 4));

            Screen.PrintBoard(board);

            Console.ReadKey();
        }
    }
}
