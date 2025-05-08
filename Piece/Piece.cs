namespace ChessGame.Piece.PieceModel
{
    using ChessGame.Logic.PositionGame;
    using ChessGame.Table;
    using ChessGame.Game.main;

    public abstract class Piece
    {
        public Position Position { get; set; }
        public bool IsWhite { get; set; }
        public Game Game { get; protected set; }
        public int Movements { get; set; }

        public Piece(Game game, bool isWhite, Position position)
        {
            Position = position;
            IsWhite = isWhite;
            Game = game;
        }

        public void IncreaseMoviment()
        {
            this.Movements++; 
        }

        public void ChangePosition(Position newPos)
        {
            this.Position = newPos;
        }

        public bool IsPossibleToMove(Position pos)
        {
            bool[,] moves = GetPositionsToMove();

            return moves[pos.Column,pos.Line];
        }

        public abstract bool[,] GetPositionsToMove();

        public override int GetHashCode()
        {
            return this.Position.GetHashCode();
        }
    }
}
