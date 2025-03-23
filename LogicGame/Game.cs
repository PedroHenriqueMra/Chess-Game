namespace ChessGame.Game.main
{
    using ChessGame.Piece.Entity;
    using ChessGame.Table;
    using ChessGame.Piece.PieceModel;
    using ChessGame.Logic.PositionGame;
    using System.Security;
    using ChessGame.Logic.SystemPlayer;

    public class Game
    {
        public int Turns { get; set; }
        public bool GameOver { get; set; } = default;
        public Player PlayerWhite { get; set; }
        public Player PlayerBlack { get; set; }

        public Game(Player playerWhite, Player playerBlack)
        {
            PlayerWhite = playerWhite;
            PlayerBlack = playerBlack;
        }

        public void ChangeTurn()
        {
            Turns += 1;
        }

        public Position? MovePiece(Piece piece, Position target)
        {
            if (piece.IsPossibleToMove(target))
            {

            }

            return null;
        }

        // Is in passant
        public bool IsInPassant(Piece piece)
        {
            return false;
        }
    }
}

