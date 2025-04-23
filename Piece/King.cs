namespace ChessGame.Piece.Entity
{
    using ChessGame.Logic.PositionGame;
    using ChessGame.Piece.PieceModel;
    using ChessGame.Table;
    using ChessGame.Game.main;

    public class King : Piece
    {
        public King(Game game, bool isWhite, Position position)
        : base (game, isWhite, position) {}

        public override bool[,] GetPositionsToMove()
        {
            bool[,] steps = new bool[Game.Board.Lenght[0], Game.Board.Lenght[1]];
            int[] colPositions = { 0,  1, 1, 1, 0, -1,-1, -1};
            int[] linPositions = {-1, -1, 0, 1, 1,  1, 0, -1};

            for (int l = 0;l < 8;l++)
            {
                Position target = new Position(Position.Column + colPositions[l], Position.Line + linPositions[l]);
                if (target.IsInBoard())
                {
                    Piece piece = Game.Board.GetPieceByPosition(target);
                    if (piece == null || !piece.IsWhite)
                    {
                        if (!Game.IsInCheckMate(this.Position, target, this.IsWhite))
                        {
                            steps[target.Column,target.Line] = true;
                        }
                    }
                }
            }

            return steps;
        }

        public override string ToString()
        {
            if (this.IsWhite) return "K+";

            return "k+";
        }
    }
}
