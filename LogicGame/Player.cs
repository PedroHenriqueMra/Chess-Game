namespace ChessGame.Logic.SystemPlayer
{
    using ChessGame.Piece.PieceModel;
    public class Player
    {
        public bool IsWhite { get; set; }
        public int AmountPieces { get; set; } = 16;
        public int AmountPiecesYouCatch { get; set; }
        public List<Piece> PiecesYouCatch { get; set; } = new List<Piece>();

        public Player(bool isWhite)
        {
            IsWhite = isWhite;
        }
    }
}
