using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessMaster.BoardChess
{
    class Board
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        private Pieces[,] Piece { get; set; }

        public Board(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            Piece = new Pieces[rows, columns];
        }

        public Pieces PieceOnTheBoard(int row, int column)
        {
            return Piece[row, column];
        }

        public Pieces PieceOnTheBoard(Position pos)
        {
            return Piece[pos.Row, pos.Column];
        }

        public bool PieceExist(Position pos)
        {
            CheckPosition(pos);

            return PieceOnTheBoard(pos) != null;
        }

        public void InputPiece(Pieces piece, Position position)
        {
            if (PieceExist(position))
            {
                throw new BoardException("There is a piece in this position");
            }
            Piece[position.Row, position.Column] = piece;
            piece.Position = position;
        }

        public bool PositionValid(Position pos)
        {
            if (pos.Row < 0 || pos.Row >= Rows || pos.Column < 0 || pos.Column >= Columns)
            {
                return false;
            }

            return true;
        }

        public void CheckPosition(Position pos)
        {
            if (!PositionValid(pos))
            {
                throw new BoardException("Invalid position");
            }
        }


    }
}
