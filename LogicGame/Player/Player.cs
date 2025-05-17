namespace ChessGame.Logic.Player.PlayerEntity
{
    using ChessGame.Piece.PieceModel;
    using ChessGame.Logic.Player.Color;
    public class Player
    {
        public PlayerColor Color { get; set; }
        public int AmountPieces { get; set; } = 16;
        public int AmountPiecesYouCatch { get; set; }
        public List<Piece> PiecesYouCatch { get; set; } = new List<Piece>();

        public Player(PlayerColor color)
        {
            Color = color;
        }

        public override string ToString()
        {
            return $"Player {this.Color.ToString()}";
        }
    }
}
