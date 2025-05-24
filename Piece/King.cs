namespace ChessGame.Piece.Entity
{
    using ChessGame.Logic.PositionGame;
    using ChessGame.Piece.PieceModel;
    using ChessGame.Logic.Game;
    using ChessGame.Logic.Player.Color;

    public class King : Piece
    {
        public King(Game game, PlayerColor color, Position position)
        : base (game, color, position) {}

        public override bool[,] GetPositionsToMove()
        {
            bool[,] steps = new bool[Game.Board.NumColumn, Game.Board.NumLine];

            int[] colPositions = { 0,  1, 1, 1, 0, -1,-1, -1};
            int[] linPositions = {-1, -1, 0, 1, 1,  1, 0, -1};

            // Lop to start with all the steps around the king as true
            for (int lop = 0;lop < 8;lop++)
            {
                Position kingPos = new Position(Position.Column,Position.Line);
                kingPos.Column += colPositions[lop];
                kingPos.Line += linPositions[lop];

                if (!kingPos.IsInBoard())
                    continue;

                Piece? pieceTarget = Game.Board.GetPieceByPosition(kingPos);
                if (pieceTarget == null || pieceTarget.Color != this.Color)
                {
                    steps[kingPos.Column,kingPos.Line] = true;
                }

            }

            return steps;
        }

        public override Piece Clone()
        {
            King clone = new King(
                Game = this.Game,
                Color = this.Color,
                Position = new Position(this.Position.Column, this.Position.Line)
            );
            clone.Movements = this.Movements;
            
            return clone;
        }

        public override string ToString()
        {
            if (this.Color == PlayerColor.White) return "K";

            return "k";
        }
    }
}
