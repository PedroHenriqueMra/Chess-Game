namespace ChessGame.Game.main
{
    using ChessGame.Piece.Entity;
    using ChessGame.Table;
    using ChessGame.Piece.PieceModel;

    public class Game
    {
        public int Turns { get; set; }
        public bool GameOver { get; set; }
        public Board Board { get; protected set; }
        public Game(Board board)
        {
            Board = board;
        }

        // Is in passant
        public bool IsInPassant(Piece piece)
        {
            return false;
        }
    }
}

