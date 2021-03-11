using ChessMaster.BoardChess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessMaster.Chess
{
    class Horse : Pieces
    {
        public Horse(Board board, Colors color) : base(board, color)
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

            
            pos.DefineValues(Position.Row - 1, Position.Column - 2);
            if (Board.PositionValid(pos) && MoveOk(pos))
            {
                matrix[pos.Row, pos.Column] = true;
            }

            
            pos.DefineValues(Position.Row - 1, Position.Column + 2);
            if (Board.PositionValid(pos) && MoveOk(pos))
            {
                matrix[pos.Row, pos.Column] = true;
            }

            
            pos.DefineValues(Position.Row - 2, Position.Column - 1);
            if (Board.PositionValid(pos) && MoveOk(pos))
            {
                matrix[pos.Row, pos.Column] = true;
            }

            
            pos.DefineValues(Position.Row - 2, Position.Column + 1);
            if (Board.PositionValid(pos) && MoveOk(pos))
            {
                matrix[pos.Row, pos.Column] = true;
            }

           
            pos.DefineValues(Position.Row - 2, Position.Column - 1);
            if (Board.PositionValid(pos) && MoveOk(pos))
            {
                matrix[pos.Row, pos.Column] = true;
            }

       
            pos.DefineValues(Position.Row - 2, Position.Column +1);
            if (Board.PositionValid(pos) && MoveOk(pos))
            {
                matrix[pos.Row, pos.Column] = true;
            }

            pos.DefineValues(Position.Row + 1, Position.Column - 2);
            if (Board.PositionValid(pos) && MoveOk(pos))
            {
                matrix[pos.Row, pos.Column] = true;
            }

          
            pos.DefineValues(Position.Row + 1, Position.Column + 2);
            if (Board.PositionValid(pos) && MoveOk(pos))
            {
                matrix[pos.Row, pos.Column] = true;
            }

            pos.DefineValues(Position.Row + 2, Position.Column - 1);
            if (Board.PositionValid(pos) && MoveOk(pos))
            {
                matrix[pos.Row, pos.Column] = true;
            }

            pos.DefineValues(Position.Row + 2, Position.Column + 1);
            if (Board.PositionValid(pos) && MoveOk(pos))
            {
                matrix[pos.Row, pos.Column] = true;
            }
            return matrix;
        }

        public override string ToString()
        {
            return $"H";
        }
    }
}
