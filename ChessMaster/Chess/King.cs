using ChessMaster.BoardChess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessMaster.Chess
{
    class King : Pieces
    {
        private GameChess Game;
        public King(Board board, Colors color, GameChess game) : base(board, color)
        {
            Game = game;
        }

        public override string ToString()
        {
            return $"K";
        }

        private bool MoveOk(Position position)
        {
            Pieces piece = Board.PieceOnTheBoard(position);
            return piece == null || piece.Color != this.Color;
        }

        private bool TestRookToCastle(Position pos)
        {
            Pieces piece = Board.PieceOnTheBoard(pos);
            return piece != null && piece is Rook && piece.Color == Color && piece.QuantityMovies == 0;

        }

        public override bool[,] PossiblesMoves()
        {
            bool[,] matrix = new bool[Board.Rows, Board.Columns];

            Position pos = new Position(0, 0);

            //Up
            pos.DefineValues(Position.Row - 1, Position.Column);
            if (Board.PositionValid(pos) && MoveOk(pos))
            {
                matrix[pos.Row, pos.Column] = true;
            }

            //Diagonal right up
            pos.DefineValues(Position.Row - 1, Position.Column + 1);
            if (Board.PositionValid(pos) && MoveOk(pos))
            {
                matrix[pos.Row, pos.Column] = true;
            }

            //Right
            pos.DefineValues(Position.Row, Position.Column + 1);
            if (Board.PositionValid(pos) && MoveOk(pos))
            {
                matrix[pos.Row, pos.Column] = true;
            }

            //Diagonal right down
            pos.DefineValues(Position.Row + 1, Position.Column + 1);
            if (Board.PositionValid(pos) && MoveOk(pos))
            {
                matrix[pos.Row, pos.Column] = true;
            }

            //Down
            pos.DefineValues(Position.Row + 1, Position.Column);
            if (Board.PositionValid(pos) && MoveOk(pos))
            {
                matrix[pos.Row, pos.Column] = true;
            }

            //Diagonal left down
            pos.DefineValues(Position.Row + 1, Position.Column - 1);
            if (Board.PositionValid(pos) && MoveOk(pos))
            {
                matrix[pos.Row, pos.Column] = true;
            }

            //Left
            pos.DefineValues(Position.Row, Position.Column - 1);
            if (Board.PositionValid(pos) && MoveOk(pos))
            {
                matrix[pos.Row, pos.Column] = true;
            }

            //Diagonal left Up
            pos.DefineValues(Position.Row - 1, Position.Column - 1);
            if (Board.PositionValid(pos) && MoveOk(pos))
            {
                matrix[pos.Row, pos.Column] = true;
            }

            // #Specialmove castle

            if (QuantityMovies == 0 && !Game.Xeque)
            {
                //Castle small

                Position rookPosition1 = new Position(Position.Row, Position.Column + 3);

                if (TestRookToCastle(rookPosition1))
                {
                    Position p1 = new Position(Position.Row, Position.Column + 1);
                    Position p2 = new Position(Position.Row, Position.Column + 2);
                    if (Board.PieceOnTheBoard(p1) == null && Board.PieceOnTheBoard(p2) == null)
                    {
                        matrix[Position.Row, Position.Column + 2] = true;

                    }

                }

                //#Specialmove castle big

                Position rookPosition2 = new Position(Position.Row, Position.Column - 4);

                if (TestRookToCastle(rookPosition2))
                {
                    Position p1 = new Position(Position.Row, Position.Column - 1);
                    Position p2 = new Position(Position.Row, Position.Column - 2);
                    Position p3 = new Position(Position.Row, Position.Column - 3);
                    if (Board.PieceOnTheBoard(p1) == null && Board.PieceOnTheBoard(p2) == null && Board.PieceOnTheBoard(p3) == null)
                    {
                        matrix[Position.Row, Position.Column - 2] = true;

                    }

                }

            }

            return matrix;
        }

    }
}
