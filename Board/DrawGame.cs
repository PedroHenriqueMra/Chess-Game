namespace ChessGame.Table.Draw
{
    using System.ComponentModel.Design.Serialization;
    using ChessGame.Logic.Game;
    using ChessGame.Logic.PositionGame;
    using ChessGame.Table;

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

        public void DrawBoard()
        {
            for (int l = 0; l < 8; l++)
            {
                for (int c = 0; c < 8; c++)
                {
                    if (c == 0)
                        Console.Write($" {LineNumbers[l]}");

                    if (Board.Pieces[c, l] != null)
                        Console.Write($" {Board.Pieces[c, l]} ");
                    else
                        Console.Write(" - ");
                }

                Console.WriteLine("");
            }

            for (int cl = 0; cl < ColumnLirycs.Length; cl++)
            {
                if (cl == 0)
                    Console.Write(" X");

                Console.Write($" {ColumnLirycs[cl]} ");
            }  

            Console.WriteLine("");
        }

        public void DrawPlayPiece(Position piecePos, bool[,] playPiece)
        {
            for (int l = 0; l < 8; l++)
            {
                for (int c = 0; c < 8; c++)
                {
                    if (c == 0)
                        Console.Write($" {LineNumbers[l]}");

                    if (playPiece[c, l])
                        Console.BackgroundColor = Board.Pieces[c, l] != null ? ConsoleColor.Red : ConsoleColor.Yellow;

                    if (Board.Pieces[c, l] != null)
                    {
                        if (c == piecePos.Column && l == piecePos.Line)
                            Console.BackgroundColor = ConsoleColor.Gray;

                        Console.Write($" {Board.Pieces[c, l]} ");
                        Console.ResetColor();
                        continue;
                    }

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

            Console.WriteLine("");
        }
        
        public void DrawPlayOptions(bool[,] options)
        {
            for (int l = 0; l < 8; l++)
            {
                for (int c = 0; c < 8; c++)
                {
                    if (options[c, l])
                        Console.BackgroundColor = ConsoleColor.Gray;

                    if (c == 0)
                        Console.Write($" {LineNumbers[l]}");

                    if (Board.Pieces[c, l] != null)
                        Console.Write($" {Board.Pieces[c, l]} ");
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

            Console.WriteLine("");
        }

        public void DrawMessage()
        {

        }

        public void DrawInfo()
        {

        }

        public void DrawGameResult()
        {

        }

        public void ClearScreen()
        {
            Console.Clear();
        }
    }

}
