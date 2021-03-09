using ChessMaster.BoardChess;
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
                Board board = new Board(8, 8);


                board.InputPiece(new Rook(board, Colors.White), new Position(0, 0));
                board.InputPiece(new Rook(board, Colors.Black), new Position(1, 3));
                board.InputPiece(new King(board, Colors.White), new Position(2, 4));
                board.InputPiece(new King(board, Colors.Black), new Position(3, 5));

                Screen.PrintBoard(board);
            }
            catch (BoardException ex)
            {

                Console.WriteLine(ex.Message);
            }


            Console.ReadKey();
        }
    }
}
