using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessMaster.BoardChess
{
    abstract class Pieces
    {

        public Position Position { get; set; }
        public Colors Color { get; protected set; }
        public int QuantityMovies { get; protected set; }
        public Board Board { get; set; }

        public Pieces(Board board, Colors color )
        {
            Position = null;
            Color = color;
            Board = board;
            QuantityMovies = 0;
        }

        public void IncreaseQuantityMoves()
        {
            QuantityMovies++;
        }
        public void DecreaseQuantityMoves()
        {
            QuantityMovies--;
        }


        public bool ExistPossiblesMoves()
        {
            bool[,] matrix = PossiblesMoves();

            for (int i = 0; i < Board.Rows; i++)
            {
                for (int j = 0; j < Board.Columns; j++)
                {
                    if (matrix[i,j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        
        public bool CanMovePosition(Position pos)
        {
            return PossiblesMoves()[pos.Row, pos.Column];
        }

        public abstract bool[,] PossiblesMoves();
    }
}
