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

        public override string ToString()
        {
            return $"P";
        }
    }
}
