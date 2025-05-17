namespace ChessGame.Piece.Entity
{
    using ChessGame.Logic.PositionGame;
    using ChessGame.Piece.PieceModel;
    using ChessGame.Logic.Game;
    using ChessGame.Logic.Player.Color;

    public class Queen : Piece
    {
        public Queen(Game game, PlayerColor color, Position position)
        : base (game, color, position) {}

        public override bool[,] GetPositionsToMove()
        {
            bool[,] steps = new bool[Game.Board.Lenght[0], Game.Board.Lenght[1]];

            // geting rook and bishop steps, because they are the same as the Queen
            bool[,] rookMoves = new Rook(Game, Color, Position).GetPositionsToMove();
            bool[,] bishopMoves = new Bishop(Game, Color, Position).GetPositionsToMove();

            for (int c = 0;c < 8;c++)
            {
                for (int l = 0;l < 8;l++)
                {
                    steps[c,l] = rookMoves[c,l] || bishopMoves[c,l];
                }
            }

            return steps;
        }

        public override Piece Clone()
        {
            return new Queen(
                Game = this.Game,
                Color = this.Color,
                Position = new Position(this.Position.Column,this.Position.Line)
            );
        }

        public override string ToString()
        {
            if (this.Color == PlayerColor.White) return "Q";

            return "q";
        }
    }
}
