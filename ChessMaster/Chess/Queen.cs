using ChessMaster.BoardChess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessMaster.Chess
{
    class Queen : Pieces
    {
        public Queen(Board board, Colors color) : base(board, color)
        {
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

            while (Board.PositionValid(pos) && MoveOk(pos))
            {
                matrix[pos.Row, pos.Column] = true;
                if (Board.PieceOnTheBoard(pos) != null && Board.PieceOnTheBoard(pos).Color != this.Color)
                {
                    break;
                }

                pos.Row--;
            }

            //Down
            pos.DefineValues(Position.Row + 1, Position.Column);

            while (Board.PositionValid(pos) && MoveOk(pos))
            {
                matrix[pos.Row, pos.Column] = true;
                if (Board.PieceOnTheBoard(pos) != null && Board.PieceOnTheBoard(pos).Color != this.Color)
                {
                    break;
                }

                pos.Row++;
            }

            //Right
            pos.DefineValues(Position.Row, Position.Column + 1);

            while (Board.PositionValid(pos) && MoveOk(pos))
            {
                matrix[pos.Row, pos.Column] = true;
                if (Board.PieceOnTheBoard(pos) != null && Board.PieceOnTheBoard(pos).Color != this.Color)
                {
                    break;
                }

                pos.Column++;
            }

            //Left
            pos.DefineValues(Position.Row, Position.Column - 1);

            while (Board.PositionValid(pos) && MoveOk(pos))
            {
                matrix[pos.Row, pos.Column] = true;
                if (Board.PieceOnTheBoard(pos) != null && Board.PieceOnTheBoard(pos).Color != this.Color)
                {
                    break;
                }

                pos.Column--;
            }

            return matrix;
        }
        public override string ToString()
        {
            return $"Q";
        }
    }
}
