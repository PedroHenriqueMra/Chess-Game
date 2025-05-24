namespace ChessGame.Table.Draw
{
    using ChessGame.Logic.Game;
    using ChessGame.Table;
    using ChessGame.Piece.PieceModel;
    using ChessGame.Logic.Player.PlayerEntity;
    using ChessGame.Logic.Player.Color;
    using ChessGame.Piece.Entity;
    using System.Text;

    public class DrawGame
    {
        public Game Game { get; set; }
        public Board Board { get; set; }

        private ConsoleColor whitePieceColor = ConsoleColor.White;
        private ConsoleColor blackPieceColor = ConsoleColor.Yellow;
        private ConsoleColor indexColor = ConsoleColor.DarkYellow;

        private readonly char[] lineLabels = { '8', '7', '6', '5', '4', '3', '2', '1' };
        private readonly char[] columnLabels = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };

        public DrawGame(Game game, Board board)
        {
            Game = game;
            Board = board;
        }

        public void DrawOptions(bool[,] options)
        {
            DrawBoardInternal(options);
        }

        public void DrawBoard(Piece selectedPiece, bool[,] steps)
        {
            DrawBoardInternal(steps, selectedPiece);
        }

        private void DrawBoardInternal(bool[,] steps, Piece? selectedPiece = null)
        {
            ClearScreen();

            for (int l = 0; l < 8; l++)
            {
                Console.ForegroundColor = indexColor;
                Console.Write($" {lineLabels[l]}");
                Console.ResetColor();

                for (int c = 0; c < 8; c++)
                {
                    Console.BackgroundColor = GetColor(selectedPiece, steps, c, l);

                    Piece? piece = Board.Pieces[c, l];
                    if (piece != null)
                    {
                        Console.ForegroundColor = GetPieceColor(piece);
                        Console.Write($" {piece.ToString()} ");
                    }
                    else
                    {
                        Console.Write(" - ");
                    }

                    Console.ResetColor();
                }

                Console.WriteLine();
            }

            DrawColumnLabels();
            Console.WriteLine();
        }

        private void DrawColumnLabels()
        {
            Console.ForegroundColor = indexColor;
            Console.Write(" X");
            foreach (var label in columnLabels)
                Console.Write($" {label} ");

            Console.ResetColor();
            Console.WriteLine();
        }

        private ConsoleColor GetColor(Piece? selectedPiece, bool[,] steps, int c, int l)
        {
            ConsoleColor consoleColor = Console.BackgroundColor;

            if (selectedPiece == null && steps[c, l])
            {
                return ConsoleColor.Gray;
            }

            if (selectedPiece != null && selectedPiece.Position.Compare(c, l))
            {
                consoleColor = ConsoleColor.Gray;
            }

            if (steps[c, l])
            {
                consoleColor = Board.Pieces[c, l] == null ? ConsoleColor.Yellow : ConsoleColor.Red;
            }

            return consoleColor;
        }

        private ConsoleColor GetPieceColor(Piece? piece)
        {
            return piece?.Color == PlayerColor.White ? whitePieceColor : blackPieceColor;
        }

        public void DrawMessage(string message)
        {
            string border = GenBorder('~', message.Length);
            Console.WriteLine(border);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
            Console.WriteLine(border);
        }

        public void DrawInfo(Player player)
        {
            string border = GenBorder('-', 30);
            Console.WriteLine(border);
            Console.WriteLine($"{player.ToString().ToUpper()}'s turn");
            Console.WriteLine("Informations:");
            Console.WriteLine($"Number of pieces: {player.AmountPieces}");
            Console.WriteLine($"Captured pieces: {player.AmountPiecesYouCatch}");
            Console.WriteLine(border);
        }

        public void DrawGameResult(PlayerColor color)
        {
            string message = color == PlayerColor.White
                ? "Congratulations! You win!"
                : "Unfortunately, you lost.";

            ConsoleColor bg = color == PlayerColor.White
                ? ConsoleColor.DarkGreen
                : ConsoleColor.Red;

            string border = GenBorder('#', 30);
            Console.WriteLine(border);

            Console.BackgroundColor = bg;
            Console.WriteLine(message);
            Console.ResetColor();

            Console.WriteLine(border);
        }

        public void DrawPromotionOptions()
        {
            string border = GenBorder('-', 30);
            Console.WriteLine(border);
            Console.WriteLine("PROMOTION TIME!");
            Console.WriteLine("Choose one of these pieces to replace your pawn:");
            Console.WriteLine("'q' - Queen");
            Console.WriteLine("'b' - Bishop");
            Console.WriteLine("'r' - Rook");
            Console.WriteLine("'h' - Knight");
            Console.WriteLine(border);
        }

        public void ClearScreen()
        {
            Console.Clear();
            Console.WriteLine();
        }

        private string GenBorder(char c, int length)
        {
            return new string(c, length);
        }
    }
}
