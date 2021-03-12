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
        private GameChess Game;
        public Pawn(Board board, Colors color, GameChess game) : base(board, color)
        {
            Game = game;
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

                //# Specialmove En passant
                if (Position.Row == 3)
                {
                    Position left = new Position(Position.Row, Position.Column - 1);
                    if (Board.PositionValid(left) && ExistEnemy(left) && Board.PieceOnTheBoard(left) == Game.VulnerableEnPassant)
                    {
                        matrix[left.Row - 1, left.Column] = true;
                    }

                    Position right = new Position(Position.Row, Position.Column + 1);
                    if (Board.PositionValid(right) && ExistEnemy(right) && Board.PieceOnTheBoard(right) == Game.VulnerableEnPassant)
                    {
                        matrix[right.Row - 1, right.Column] = true;
                    }

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

                if (Position.Row == 4)
                {
                    Position left = new Position(Position.Row, Position.Column - 1);
                    if (Board.PositionValid(left) && ExistEnemy(left) && Board.PieceOnTheBoard(left) == Game.VulnerableEnPassant)
                    {
                        matrix[left.Row + 1, left.Column] = true;
                    }


                    Position right = new Position(Position.Row, Position.Column + 1);
                    if (Board.PositionValid(right) && ExistEnemy(right) && Board.PieceOnTheBoard(right) == Game.VulnerableEnPassant)
                    {
                        matrix[right.Row + 1, right.Column] = true;
                    }
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
