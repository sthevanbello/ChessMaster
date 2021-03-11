using System;
using System.Collections.Generic;
using ChessMaster.BoardChess;
using System.Text;
using System.Threading.Tasks;
using ChessMaster.Chess;

namespace ChessMaster
{
    class Screen
    {

        public static void PrintBoard(Board board)
        {

            for (int i = 0; i < board.Rows; i++)
            {
                Console.Write($"{8 - i} ");

                for (int j = 0; j < board.Columns; j++)
                {
                        PrintingPiece(board.PieceOnTheBoard(i, j));
                }

                Console.WriteLine();
            }

            Console.Write($"  a b c d e f g h");
        }

        public static void PrintBoard(Board board, bool[,] possiblesPosition)
        {
            ConsoleColor origin = Console.BackgroundColor;
            ConsoleColor altered = ConsoleColor.DarkGray;
            for (int i = 0; i < board.Rows; i++)
            {
                Console.Write($"{8 - i} ");

                for (int j = 0; j < board.Columns; j++)
                {
                    if (possiblesPosition[i,j])
                    {
                        Console.BackgroundColor = altered;
                    }
                    else
                    {
                        Console.BackgroundColor = origin;
                    }
                    PrintingPiece(board.PieceOnTheBoard(i, j));
                    Console.BackgroundColor = origin;
                }
                Console.BackgroundColor = origin;
                Console.WriteLine();
            }

            Console.Write($"  a b c d e f g h");
            
        }

        public static void PrintingPiece(Pieces piece)
        {
            if (piece == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (piece.Color == Colors.White)
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"{piece}");
                    Console.ForegroundColor = aux;
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($"{piece}");
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }

        public static ChessPosition ReadPositionChess()
        {

            string s = Console.ReadLine();
            char column = s[0];
            int row = int.Parse($"{s[1]}");

            return new ChessPosition(column, row);
        }

    }

}

