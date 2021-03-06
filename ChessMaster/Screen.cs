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
        public static void PrintGame(GameChess game)
        {
            PrintBoard(game.Board);

            Console.WriteLine();
            PrintCatchedPieces(game);
            Console.WriteLine($"\n\nTurno: {game.Turn}");
            if (!game.Finished)
            {
                Console.WriteLine($"\nWaiting for player: {game.ActualPlayer}");
                if (game.Xeque)
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("XEQUE!");
                    Console.ForegroundColor = aux;

                }
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("XEQUE MATE!");
                Console.ForegroundColor = aux;
                Console.WriteLine($"Player winner: {game.ActualPlayer}");
            }


        }

        public static void PrintCatchedPieces(GameChess game)
        {
            ConsoleColor aux = Console.ForegroundColor;
            Console.WriteLine("\nCatched pieces: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"White: ");
            PrintSet(game.CatchedPieces(Colors.White));
            Console.ForegroundColor = aux;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"\nBlack: ");
            PrintSet(game.CatchedPieces(Colors.Black));
            Console.ForegroundColor = aux;

        }

        public static void PrintSet(HashSet<Pieces> set)
        {
            Console.Write("[");
            foreach (var item in set)
            {
                Console.Write($"{item} ");
            }
            Console.Write("]");

        }

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
                    if (possiblesPosition[i, j])
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
            Begin:
            string s = Console.ReadLine();
            if (s == "")
            {
                goto Begin;
            }
            else
            {
                char column = s[0];
                int row = int.Parse($"{s[1]}");
            return new ChessPosition(column, row);
            }
        }

    }

}
//char column = ' ';
//int row = 0;
//string s = "";

//while (s.Length == 0)
//{
//    s = Console.ReadLine();
//    if (s == null)
//    {
//        column = s[0];
//        row = int.Parse($"{s[1]}");
//    }

//}
