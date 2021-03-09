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

        public Pieces Peace(int row, int column)
        {
            return Piece[row, column];
        }

        public void InputPiece(Pieces piece, Position position)
        {
            Piece[position.Row, position.Column] = piece;
        }
    }
}
