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
        public bool Xeque { get; private set; }

        private HashSet<Pieces> pieces;
        private HashSet<Pieces> catched;
        public Pieces VulnerableEnPassant { get; private set; }




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
            VulnerableEnPassant = null;
        }



        public Pieces MoveExecute(Position origin, Position destiny)
        {
            Pieces piece = Board.RemovePiece(origin);
            piece.IncreaseQuantityMoves();
            Pieces catchedPiece = Board.RemovePiece(destiny);

            Board.InputPiece(piece, destiny);
            if (catchedPiece != null)
            {
                catched.Add(catchedPiece);
            }

            //# Specialmove Castle small
            if (piece is King && destiny.Column == origin.Column + 2)
            {
                Position rookOrigin = new Position(origin.Row, origin.Column + 3);
                Position rookDestiny = new Position(origin.Row, origin.Column + 1);
                Pieces rook = Board.RemovePiece(rookOrigin);
                rook.IncreaseQuantityMoves();
                Board.InputPiece(rook, rookDestiny);

            }
            // #Specialmove Castle big
            if (piece is King && destiny.Column == origin.Column - 2)
            {
                Position rookOrigin = new Position(origin.Row, origin.Column - 4);
                Position rookDestiny = new Position(origin.Row, origin.Column - 1);
                Pieces rook = Board.RemovePiece(rookOrigin);
                rook.IncreaseQuantityMoves();
                Board.InputPiece(rook, rookDestiny);

            }

            // #Specialmove En Passant
            if (piece is Pawn)
            {
                if (origin.Column != destiny.Column && catchedPiece == null)
                {
                    Position posCatchPawn;
                    if (piece.Color == Colors.White)
                    {
                        posCatchPawn = new Position(destiny.Row + 1, destiny.Column);
                    }
                    else
                    {
                        posCatchPawn = new Position(destiny.Row - 1, destiny.Column);
                    }
                    catchedPiece = Board.RemovePiece(posCatchPawn);
                    catched.Add(catchedPiece);
                }
            }


            return catchedPiece;
        }

        public void ReturnMove(Position origin, Position destiny, Pieces catchedPiece)
        {

            Pieces piece = Board.RemovePiece(destiny);
            piece.DecreaseQuantityMoves();
            if (catchedPiece != null)
            {
                Board.InputPiece(catchedPiece, destiny);
                catched.Remove(catchedPiece);

            }
            Board.InputPiece(piece, origin);

            // #Specialmove Castle small
            if (piece is King && destiny.Column == origin.Column + 2)
            {
                Position rookOrigin = new Position(origin.Row, origin.Column + 3);
                Position rookDestiny = new Position(origin.Row, origin.Column + 1);
                Pieces rook = Board.RemovePiece(rookDestiny);
                rook.IncreaseQuantityMoves();
                Board.InputPiece(rook, rookOrigin);

            }

            // #Specialmove Castle big
            if (piece is King && destiny.Column == origin.Column - 2)
            {
                Position rookOrigin = new Position(origin.Row, origin.Column - 4);
                Position rookDestiny = new Position(origin.Row, origin.Column - 1);
                Pieces rook = Board.RemovePiece(rookDestiny);
                rook.IncreaseQuantityMoves();
                Board.InputPiece(rook, rookOrigin);

            }

            // #Specialmove En Passant
            if (piece is Pawn)
            {
                if (origin.Column != destiny.Column && catchedPiece == VulnerableEnPassant )
                {

                    Pieces catchedPawn = Board.RemovePiece(destiny);
                    Position posCatchPawn;
                    if (piece.Color == Colors.White)
                    {
                        posCatchPawn = new Position(3, destiny.Column);
                    }
                    else
                    {
                        posCatchPawn = new Position(4, destiny.Column);

                    }

                    Board.InputPiece(catchedPawn, posCatchPawn);
                }
            }

        }

        public void MakeMove(Position origin, Position destiny)
        {

            Pieces catchPiece = MoveExecute(origin, destiny);
            if (InXeque(ActualPlayer))
            {
                ReturnMove(origin, destiny, catchPiece);
                throw new BoardException("You can't put yourself in XEQUE!");
            }
            Pieces piece = Board.PieceOnTheBoard(destiny);

            // #Specialmove promotion

            if (piece is Pawn)
            {
                if ((piece.Color == Colors.White && destiny.Row == 0) || (piece.Color == Colors.Black && destiny.Row == 7))
                {

                    piece = Board.RemovePiece(destiny);
                    pieces.Remove(piece);
                    Pieces queen = new Queen(Board, piece.Color);
                    Board.InputPiece(queen, destiny);
                    pieces.Add(queen);
                }
            }


            if (InXeque(Enemy(ActualPlayer)))
            {
                Xeque = true;
            }
            else
            {
                Xeque = false;
            }
            if (TestXequeMate(Enemy(ActualPlayer)))
            {
                Finished = true;
            }
            else
            {
            Turn++;
            ChangePlayer();
            }


            // #Specialmove En Passant
            if (piece is Pawn && (destiny.Row == origin.Row - 2 || destiny.Row == origin.Row + 2))
            {
                VulnerableEnPassant = piece;

            }
            else
            {
                VulnerableEnPassant = null;
            }


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

        public bool TestXequeMate(Colors color)
        {
            if (!InXeque(color))
            {
                return false;
            }

            foreach (var item in PiecesInGame(color))
            {
                bool[,] mat = item.PossiblesMoves();
                for (int i = 0; i < Board.Rows; i++)
                {
                    for (int j = 0; j < Board.Columns; j++)
                    {
                        if (mat[i,j])
                        {
                            Position origin = item.Position;
                            Position destiny = new Position(i, j);
                            Pieces catchPiece = MoveExecute(origin, destiny);
                            bool testXeque = InXeque(color);
                            ReturnMove(origin, destiny, catchPiece);
                            if (!testXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void PlacingNewPiece(char column, int row, Pieces piece)
        {
            Board.InputPiece(piece, new ChessPosition(column, row).ToPosition());
            pieces.Add(piece);
        }

        private void PlacingPieces()
        {
            PlacingNewPiece('a', 1, new Rook(Board, Colors.White));
            PlacingNewPiece('h', 1, new Rook(Board, Colors.White));
            PlacingNewPiece('b', 1, new Horse(Board, Colors.White));
            PlacingNewPiece('g', 1, new Horse(Board, Colors.White));
            PlacingNewPiece('c', 1, new Bishop(Board, Colors.White));
            PlacingNewPiece('f', 1, new Bishop(Board, Colors.White));
            PlacingNewPiece('d', 1, new Queen(Board, Colors.White));
            PlacingNewPiece('e', 1, new King(Board, Colors.White, this));

            PlacingNewPiece('a', 2, new Pawn(Board, Colors.White, this));
            PlacingNewPiece('b', 2, new Pawn(Board, Colors.White, this));
            PlacingNewPiece('c', 2, new Pawn(Board, Colors.White, this));
            PlacingNewPiece('d', 2, new Pawn(Board, Colors.White, this));
            PlacingNewPiece('e', 2, new Pawn(Board, Colors.White, this));
            PlacingNewPiece('f', 2, new Pawn(Board, Colors.White, this));
            PlacingNewPiece('g', 2, new Pawn(Board, Colors.White, this));
            PlacingNewPiece('h', 2, new Pawn(Board, Colors.White, this));


            PlacingNewPiece('a', 8, new Rook(Board, Colors.Black));
            PlacingNewPiece('h', 8, new Rook(Board, Colors.Black));
            PlacingNewPiece('b', 8, new Horse(Board, Colors.Black));
            PlacingNewPiece('g', 8, new Horse(Board, Colors.Black));
            PlacingNewPiece('c', 8, new Bishop(Board, Colors.Black));
            PlacingNewPiece('f', 8, new Bishop(Board, Colors.Black));
            PlacingNewPiece('d', 8, new Queen(Board, Colors.Black));
            PlacingNewPiece('e', 8, new King(Board, Colors.Black, this));

            PlacingNewPiece('a', 7, new Pawn(Board, Colors.Black, this));
            PlacingNewPiece('b', 7, new Pawn(Board, Colors.Black, this));
            PlacingNewPiece('c', 7, new Pawn(Board, Colors.Black, this));
            PlacingNewPiece('d', 7, new Pawn(Board, Colors.Black, this));
            PlacingNewPiece('e', 7, new Pawn(Board, Colors.Black, this));
            PlacingNewPiece('f', 7, new Pawn(Board, Colors.Black, this));
            PlacingNewPiece('g', 7, new Pawn(Board, Colors.Black, this));
            PlacingNewPiece('h', 7, new Pawn(Board, Colors.Black, this));

        }
    }
}
