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
        public King(Board board, Colors color) : base(board, color)
        {
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

        public override bool[,] PossiblesMoves()
        {
            bool[,] matrix = new bool[Board.Rows, Board.Columns];

            Position pos = new Position(0, 0);

            //Up
            pos.DefineValues(Position.Row - 1, Position.Column);
            if (Board.PositionValid(pos))
            {
                matrix[pos.Row, pos.Column] = true;
            }

            //Diagonal right up
            pos.DefineValues(Position.Row - 1, Position.Column + 1);
            if (Board.PositionValid(pos))
            {
                matrix[pos.Row, pos.Column] = true;
            }

            //Right
            pos.DefineValues(Position.Row, Position.Column + 1);
            if (Board.PositionValid(pos))
            {
                matrix[pos.Row, pos.Column] = true;
            }

            //Diagonal right down
            pos.DefineValues(Position.Row + 1, Position.Column + 1);
            if (Board.PositionValid(pos))
            {
                matrix[pos.Row, pos.Column] = true;
            }

            //Down
            pos.DefineValues(Position.Row + 1, Position.Column);
            if (Board.PositionValid(pos))
            {
                matrix[pos.Row, pos.Column] = true;
            }

            //Diagonal left down
            pos.DefineValues(Position.Row + 1, Position.Column - 1);
            if (Board.PositionValid(pos))
            {
                matrix[pos.Row, pos.Column] = true;
            }

            //Left
            pos.DefineValues(Position.Row, Position.Column - 1);
            if (Board.PositionValid(pos))
            {
                matrix[pos.Row, pos.Column] = true;
            }

            //Diagonal left Up
            pos.DefineValues(Position.Row - 1, Position.Column - 1);
            if (Board.PositionValid(pos))
            {
                matrix[pos.Row, pos.Column] = true;
            }
            return matrix;
        }

    }
}
