namespace ChessGame.Piece.PieceModel
{
    using ChessGame.Logic.PositionGame;
    using ChessGame.Logic.Game;
    using ChessGame.Logic.Player.Color;

    public abstract class Piece
    {
        public Position Position { get; set; }
        public PlayerColor Color { get; set; }
        public Game Game { get; protected set; }
        public int Movements { get; set; }

        public Piece(Game game, PlayerColor color, Position position)
        {
            Position = position;
            Color = color;
            Game = game;

            Movements = 0;
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

        public bool HasMove()
        {
            return HasMove(GetPositionsToMove());
        }

        public bool HasMove(bool[,] movements)
        {
            for (int c = 0; c < 8; c++)
            {
                for (int l = 0; l < 8; l++)
                {
                    if (movements[c, l]) return true;
                }
            }

            return false;
        }

        public abstract Piece Clone();

        public abstract bool[,] GetPositionsToMove();

        public override int GetHashCode()
        {
            return this.Position.GetHashCode();
        }
    }
}
