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

        private HashSet<Pieces> pieces;
        private HashSet<Pieces> catched;

        public bool Xeque { get; private set; }




        public GameChess()
        {
            Board = new Board(8, 8);
            Turn = 1;
            ActualPlayer = Colors.White;
            pieces = new HashSet<Pieces>();
            catched = new HashSet<Pieces>();
            PlacingPieces();
            Finished = false;
            Xeque = false;
        }



        public Pieces MoveExecute(Position origin, Position destiny)
        {
            Pieces piece = Board.RemovePiece(origin);
            piece.IncreaseQuantityMoves();
            Pieces catchPiece = Board.RemovePiece(destiny);

            Board.InputPiece(piece, destiny);
            if (catchPiece != null)
            {
                catched.Add(catchPiece);
            }
            return catchPiece;
        }

        public void ReturnMove(Position origin, Position destiny, Pieces catchPiece)
        {

            Pieces piece = Board.RemovePiece(destiny);
            piece.DecreaseQuantityMoves();
            if (catchPiece != null)
            {
                Board.InputPiece(catchPiece, destiny);
                catched.Remove(catchPiece);

            }
            Board.InputPiece(piece, origin);

        }

        public void MakeMove(Position origin, Position destiny)
        {

            Pieces catchPiece = MoveExecute(origin, destiny);
            if (InXeque(ActualPlayer))
            {
                ReturnMove(origin, destiny, catchPiece);
                throw new BoardException("You can't put yourself in XEQUE!");
            }
            if (InXeque(Enemy(ActualPlayer)))
            {
                Xeque = true;
            }
            else
            {
                Xeque = false;
            }

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

        public HashSet<Pieces> CatchedPieces(Colors color)
        {
            HashSet<Pieces> aux = new HashSet<Pieces>();

            foreach (Pieces item in catched)
            {
                if (item.Color == color)
                {
                    aux.Add(item);
                }
            }
            return aux;
        }

        public HashSet<Pieces> PiecesInGame(Colors color)
        {
            HashSet<Pieces> aux = new HashSet<Pieces>();

            foreach (Pieces item in pieces)
            {
                if (item.Color == color)
                {
                    aux.Add(item);
                }
            }
            aux.ExceptWith(CatchedPieces(color));
            return aux;

        }

        private Colors Enemy(Colors color)
        {
            if (color == Colors.White)
            {
                return Colors.Black;
            }
            return Colors.White;

        }

        private Pieces KingPiece(Colors color)
        {
            foreach (Pieces item in PiecesInGame(color))
            {
                if (item is King)
                {
                    return item;
                }
            }
            return null;
        }

        public bool InXeque(Colors color)
        {
            Pieces king = KingPiece(color);
            if (king == null)
            {
                throw new BoardException($"Not has king this color {color}");
            }

            foreach (Pieces item in PiecesInGame(Enemy(color)))
            {
                bool[,] mat = item.PossiblesMoves();
                if (mat[king.Position.Row, king.Position.Column])
                {
                    return true;
                }
            }
            return false;

        }

        public void PalcingNewPiece(char column, int row, Pieces piece)
        {
            Board.InputPiece(piece, new ChessPosition(column, row).ToPosition());
            pieces.Add(piece);
        }

        private void PlacingPieces()
        {
            PalcingNewPiece('a', 1, new Rook(Board, Colors.White));
            PalcingNewPiece('h', 1, new Rook(Board, Colors.White));
            //PalcingNewPiece('b', 1, new Horse(Board, Colors.White));
            //PalcingNewPiece('g', 1, new Horse(Board, Colors.White));
            //PalcingNewPiece('c', 1, new Bishop(Board, Colors.White));
            //PalcingNewPiece('f', 1, new Bishop(Board, Colors.White));
            //PalcingNewPiece('e', 1, new Queen(Board, Colors.White));
            PalcingNewPiece('d', 1, new King(Board, Colors.White));

            //PalcingNewPiece('a', 2, new Pawn(Board, Colors.White));
            //PalcingNewPiece('b', 2, new Pawn(Board, Colors.White));
            //PalcingNewPiece('c', 2, new Pawn(Board, Colors.White));
            //PalcingNewPiece('d', 2, new Pawn(Board, Colors.White));
            //PalcingNewPiece('e', 2, new Pawn(Board, Colors.White));
            //PalcingNewPiece('f', 2, new Pawn(Board, Colors.White));
            //PalcingNewPiece('g', 2, new Pawn(Board, Colors.White));
            //PalcingNewPiece('h', 2, new Pawn(Board, Colors.White));


            PalcingNewPiece('a', 8, new Rook(Board, Colors.Black));
            PalcingNewPiece('h', 8, new Rook(Board, Colors.Black));
            //PalcingNewPiece('b', 8, new Horse(Board, Colors.Black));
            //PalcingNewPiece('g', 8, new Horse(Board, Colors.Black));
            //PalcingNewPiece('c', 8, new Bishop(Board, Colors.Black));
            //PalcingNewPiece('f', 8, new Bishop(Board, Colors.Black));
            //PalcingNewPiece('e', 8, new Queen(Board, Colors.Black));
            PalcingNewPiece('d', 8, new King(Board, Colors.Black));

            //PalcingNewPiece('a', 7, new Pawn(Board, Colors.Black));
            //PalcingNewPiece('b', 7, new Pawn(Board, Colors.Black));
            //PalcingNewPiece('c', 7, new Pawn(Board, Colors.Black));
            //PalcingNewPiece('d', 7, new Pawn(Board, Colors.Black));
            //PalcingNewPiece('e', 7, new Pawn(Board, Colors.Black));
            //PalcingNewPiece('f', 7, new Pawn(Board, Colors.Black));
            //PalcingNewPiece('g', 7, new Pawn(Board, Colors.Black));
            //PalcingNewPiece('h', 7, new Pawn(Board, Colors.Black));

        }
    }
}
