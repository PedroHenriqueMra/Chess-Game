namespace ChessGame.Table.Draw
{
    using System.ComponentModel.Design.Serialization;
    using ChessGame.Logic.Game;
    using ChessGame.Logic.PositionGame;
    using ChessGame.Table;
    using ChessGame.Piece.PieceModel;
    using System.ComponentModel;
    using ChessGame.Logic.Player.PlayerEntity;
    using ChessGame.Logic.Player.Color;
    using ChessGame.Piece.Entity;

    public class DrawGame
    {
        public Game Game { get; set; }
        public Board Board { get; set; }

        private readonly char[] LineNumbers = { '8', '7', '6', '5', '4', '3', '2', '1' };
        private readonly char[] ColumnLirycs = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };

        public DrawGame(Game game, Board board)
        {
            Game = game;
            Board = board;
        }

        public void DrawOptions(bool[,] options)
        {
            ClearScreen();

            // test:
            Console.WriteLine($"Turn: {Game.Turns}");
            Console.WriteLine($"Is in xeque?: {Game.IsInXeque}");
            Console.WriteLine($"Pawn En passant: {Game.PawnEnPassant?.Position.ToString()}");
            Console.WriteLine($"Turn En passant: {Game.TurnEnPassant}\n");

            for (int l = 0; l < 8; l++)
            {
                for (int c = 0; c < 8; c++)
                {
                    if (c == 0)
                        Console.Write($" {LineNumbers[l]}");

                    // Set color
                    if (options[c, l]) Console.BackgroundColor = ConsoleColor.Gray;

                    // Draw board
                    if (Board.Pieces[c, l] != null)
                    {
                        Console.Write($" {Board.Pieces[c, l]} ");
                    }
                    else
                        Console.Write(" - ");

                    Console.ResetColor();
                }

                Console.WriteLine("");
            }

            for (int cl = 0; cl < ColumnLirycs.Length; cl++)
            {
                if (cl == 0)
                    Console.Write(" X");

                Console.Write($" {ColumnLirycs[cl]} ");
            }

            Console.WriteLine("\n");
        }

        public void DrawBoard(Piece pieceSelected, bool[,] steps)
        {
            ClearScreen();

            // test:
            Console.WriteLine($"Turn: {Game.Turns}");
            Console.WriteLine($"Is in xeque?: {Game.IsInXeque}");
            Console.WriteLine($"Pawn En passant: {Game.PawnEnPassant:.Position.ToString()}");
            Console.WriteLine($"Turn En passant: {Game.TurnEnPassant}\n");

            for (int l = 0; l < 8; l++)
            {
                for (int c = 0; c < 8; c++)
                {
                    if (c == 0)
                        Console.Write($" {LineNumbers[l]}");

                    // Set color
                    ConsoleColor consoleColor = SetColor(pieceSelected, steps, c, l);
                    Console.BackgroundColor = consoleColor;

                    // Draw board
                    if (Board.Pieces[c, l] != null)
                    {
                        Console.Write($" {Board.Pieces[c, l]} ");
                    }
                    else
                        Console.Write(" - ");

                    Console.ResetColor();
                }

                Console.WriteLine("");
            }

            for (int cl = 0; cl < ColumnLirycs.Length; cl++)
            {
                if (cl == 0)
                    Console.Write(" X");

                Console.Write($" {ColumnLirycs[cl]} ");
            }
            Console.WriteLine("\n");
        }

        private ConsoleColor SetColor(Piece pieceSelected, bool[,] steps, int c, int l)
        {
            ConsoleColor consoleColor = Console.BackgroundColor;

            if (pieceSelected.Position.Compare(c, l))
                consoleColor = ConsoleColor.Gray;

            if (steps[c, l])
            {
                consoleColor = Board.Pieces[c, l] == null ? ConsoleColor.Yellow : ConsoleColor.Red;

                // color in En Passant play
                if (pieceSelected is Pawn && Board.Pieces[c, l] == null && Game.PawnEnPassant != null)
                {
                    // direction to find out the pawn En Passant position.
                    int pawnDirection = pieceSelected.Color == PlayerColor.White
                        ? +1 : -1;
                    Pawn pawnEnPassant = Game.PawnEnPassant;

                    if (Game.IsInPassant(pawnEnPassant))
                        consoleColor = ConsoleColor.Red;

                    return consoleColor;
                }
            }

            return consoleColor;
        }

        public void DrawMessage(string message)
        {
            Console.WriteLine("");
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public void DrawInfo(Player player)
        {
            Console.WriteLine("-------------------------------");
            Console.WriteLine($"Vez de /{player}/");
            Console.WriteLine("Informations:");
            Console.WriteLine($"Number of pieces: {player.AmountPieces}");
            Console.WriteLine($"Number of pieces catched: {player.AmountPiecesYouCatch}");
            Console.WriteLine("-------------------------------");
        }

        public void DrawGameResult(PlayerColor color)
        {
            string winnerMessage = color == PlayerColor.White ? "congratulations!!. You win!." : "You lost";
            ConsoleColor backgroundColor = color == PlayerColor.White ? ConsoleColor.DarkGreen : ConsoleColor.Red;

            Console.BackgroundColor = backgroundColor;
            Console.WriteLine(winnerMessage);
            Console.ResetColor();
        }

        public void ClearScreen()
        {
            Console.Clear();
            Console.WriteLine("");
        }
    }

}
