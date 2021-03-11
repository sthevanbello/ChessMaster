using ChessMaster.BoardChess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessMaster.Chess
{
    class Pawn : Pieces
    {
        public Pawn(Board board, Colors color) : base(board, color)
        {
        }
        private bool MoveOk(Position position)
        {
            Pieces piece = Board.PieceOnTheBoard(position);
            return piece == null || piece.Color != this.Color;
        }

        private bool ExistEnemy(Position pos)
        {
            Pieces piece = Board.PieceOnTheBoard(pos);
            return piece != null && piece.Color != this.Color;
        }

        private bool Free(Position pos)
        {
            return Board.PieceOnTheBoard(pos) == null;
        }

        public override bool[,] PossiblesMoves()
        {
            bool[,] matrix = new bool[Board.Rows, Board.Columns];

            Position pos = new Position(0, 0);


            if (Color == Colors.White)
            {
                pos.DefineValues(Position.Row - 1, Position.Column);
                if (Board.PositionValid(pos) && Free(pos))
                {
                    matrix[pos.Row, pos.Column] = true;
                }

                pos.DefineValues(Position.Row - 2, Position.Column);
                if (Board.PositionValid(pos) && Free(pos) && QuantityMovies == 0)
                {
                    matrix[pos.Row, pos.Column] = true;
                }

                pos.DefineValues(Position.Row - 1, Position.Column - 1);
                if (Board.PositionValid(pos) && ExistEnemy(pos))
                {
                    matrix[pos.Row, pos.Column] = true;
                }

                pos.DefineValues(Position.Row - 1, Position.Column + 1);
                if (Board.PositionValid(pos) && ExistEnemy(pos))
                {
                    matrix[pos.Row, pos.Column] = true;
                }
            }
            else
            {
                pos.DefineValues(Position.Row + 1, Position.Column);
                if (Board.PositionValid(pos) && Free(pos))
                {
                    matrix[pos.Row, pos.Column] = true;
                }

                pos.DefineValues(Position.Row + 2, Position.Column);
                if (Board.PositionValid(pos) && Free(pos) && QuantityMovies == 0)
                {
                    matrix[pos.Row, pos.Column] = true;
                }
                pos.DefineValues(Position.Row + 1, Position.Column + 1);
                if (Board.PositionValid(pos) && ExistEnemy(pos))
                {
                    matrix[pos.Row, pos.Column] = true;
                }
                pos.DefineValues(Position.Row + 1, Position.Column - 1);
                if (Board.PositionValid(pos) && ExistEnemy(pos))
                {
                    matrix[pos.Row, pos.Column] = true;
                }
            }


            return matrix;
        }
        public override string ToString()
        {
            return $"P";
        }
    }
}
