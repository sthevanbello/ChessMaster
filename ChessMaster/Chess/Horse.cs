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

        public override string ToString()
        {
            return $"H";
        }
    }
}
