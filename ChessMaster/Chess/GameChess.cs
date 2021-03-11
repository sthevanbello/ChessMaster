using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChessMaster.BoardChess;

namespace ChessMaster.Chess
{
    class GameChess
    {
        public Board Board { get; private set; }
        public int Turn { get; private set; }
        public Colors ActualPlayer { get; private set; }
        public bool Finished { get; private set; }


        public GameChess()
        {
            Board = new Board(8, 8);
            Turn = 1;
            ActualPlayer = Colors.White;
            PlacingPieces();
            Finished = false;
        }



        public void MoveExecute(Position origin, Position destiny)
        {
            Pieces piece = Board.RemovePiece(origin);
            piece.IncreaseQuantityMoves();
            Pieces catchPiece = Board.RemovePiece(destiny);

            Board.InputPiece(piece, destiny);

        }

        public void MakeMove(Position origin, Position destiny)
        {

            MoveExecute(origin, destiny);
            Turn++;
            ChangePlayer();

        }

        private void ChangePlayer()
        {
            if (ActualPlayer == Colors.White)
            {
                ActualPlayer = Colors.Black;
            }
            else
            {
                ActualPlayer = Colors.White;
            }
        }

        public void CheckOriginPosition(Position pos)
        {

            if (Board.PieceOnTheBoard(pos) == null)
            {
                throw new BoardException("There is no piece in this choice position");
            }
            if (ActualPlayer != Board.PieceOnTheBoard(pos).Color)
            {
                throw new BoardException("The piece not yours!");
            }
            if (!Board.PieceOnTheBoard(pos).ExistPossiblesMoves())
            {
                throw new BoardException("Not has possible moves for this piece");
            }
        }

        public void CheckDestinyPosition(Position origin, Position destiny)
        {
            if (!Board.PieceOnTheBoard(origin).CanMovePosition(destiny))
            {
                throw new BoardException("Invalid destiny position");
            }


        }

        private void PlacingPieces()
        {

            Board.InputPiece(new Rook(Board, Colors.White), new ChessPosition('a', 1).ToPosition());
            Board.InputPiece(new Rook(Board, Colors.White), new ChessPosition('h', 1).ToPosition());
            Board.InputPiece(new Horse(Board, Colors.White), new ChessPosition('b', 1).ToPosition());
            Board.InputPiece(new Horse(Board, Colors.White), new ChessPosition('g', 1).ToPosition());
            Board.InputPiece(new Bishop(Board, Colors.White), new ChessPosition('c', 1).ToPosition());
            Board.InputPiece(new Bishop(Board, Colors.White), new ChessPosition('f', 1).ToPosition());
            Board.InputPiece(new Queen(Board, Colors.White), new ChessPosition('e', 1).ToPosition());
            Board.InputPiece(new King(Board, Colors.White), new ChessPosition('d', 1).ToPosition());

            //Board.InputPiece(new Pawn(Board, Colors.White), new ChessPosition('a', 2).ToPosition());
            //Board.InputPiece(new Pawn(Board, Colors.White), new ChessPosition('b', 2).ToPosition());
            Board.InputPiece(new Pawn(Board, Colors.White), new ChessPosition('c', 2).ToPosition());
            Board.InputPiece(new Pawn(Board, Colors.White), new ChessPosition('d', 2).ToPosition());
            Board.InputPiece(new Pawn(Board, Colors.White), new ChessPosition('e', 2).ToPosition());
            //Board.InputPiece(new Pawn(Board, Colors.White), new ChessPosition('f', 2).ToPosition());
            //Board.InputPiece(new Pawn(Board, Colors.White), new ChessPosition('g', 2).ToPosition());
            //Board.InputPiece(new Pawn(Board, Colors.White), new ChessPosition('h', 2).ToPosition());


            Board.InputPiece(new Rook(Board, Colors.Black), new ChessPosition('a', 8).ToPosition());
            Board.InputPiece(new Rook(Board, Colors.Black), new ChessPosition('h', 8).ToPosition());
            Board.InputPiece(new Horse(Board, Colors.Black), new ChessPosition('b', 8).ToPosition());
            Board.InputPiece(new Horse(Board, Colors.Black), new ChessPosition('g', 8).ToPosition());
            Board.InputPiece(new Bishop(Board, Colors.Black), new ChessPosition('c', 8).ToPosition());
            Board.InputPiece(new Bishop(Board, Colors.Black), new ChessPosition('f', 8).ToPosition());
            Board.InputPiece(new Queen(Board, Colors.Black), new ChessPosition('e', 8).ToPosition());
            Board.InputPiece(new King(Board, Colors.Black), new ChessPosition('d', 8).ToPosition());

            //Board.InputPiece(new Pawn(Board, Colors.Black), new ChessPosition('a', 7).ToPosition());
            //Board.InputPiece(new Pawn(Board, Colors.Black), new ChessPosition('b', 7).ToPosition());
            Board.InputPiece(new Pawn(Board, Colors.Black), new ChessPosition('c', 7).ToPosition());
            Board.InputPiece(new Pawn(Board, Colors.Black), new ChessPosition('d', 7).ToPosition());
            Board.InputPiece(new Pawn(Board, Colors.Black), new ChessPosition('e', 7).ToPosition());
            //Board.InputPiece(new Pawn(Board, Colors.Black), new ChessPosition('f', 7).ToPosition());
            //Board.InputPiece(new Pawn(Board, Colors.Black), new ChessPosition('g', 7).ToPosition());
            //Board.InputPiece(new Pawn(Board, Colors.Black), new ChessPosition('h', 7).ToPosition());
        }
    }
}
