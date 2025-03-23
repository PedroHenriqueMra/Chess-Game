namespace ChessGame.Piece.Entity
{
    using ChessGame.Logic.PositionGame;
    using ChessGame.Piece.PieceModel;
    using ChessGame.Table;

    public class Rook : Piece
    {
        public Rook(Board board, bool isWhite, Position position)
        : base (board, isWhite, position) {}

        public override bool[,] GetPositionsToMove()
        {
            bool[,] steps = new bool[Board.Lenght[0], Board.Lenght[1]];
            Position pos = new Position();

            // up steps
            pos.ChangePosition(this.Position.Line, this.Position.Column);
            for (int i = 0;i < 8;i++)
            {
                pos.Line--;
                if (Board.IsValidPosition(pos))
                {
                    if (Board.GetPieceByPosition(pos) == null)
                    {
                        steps[pos.Column, pos.Line] = true;
                    }
                }
            }

            // bottom steps
            pos.ChangePosition(this.Position.Line, this.Position.Column);
            for (int i = 0;i < 8;i++)
            {
                pos.Line++;
                if (Board.IsValidPosition(pos))
                {
                    if (Board.GetPieceByPosition(pos) == null)
                    {
                        steps[pos.Column, pos.Line] = true;
                    }
                }
            }

            // right steps
            pos.ChangePosition(this.Position.Line, this.Position.Column);
            for (int i = 0;i < 8;i++)
            {
                pos.Column++;
                if (Board.IsValidPosition(pos))
                {
                    if (Board.GetPieceByPosition(pos) == null)
                    {
                        steps[pos.Column, pos.Line] = true;
                    }
                }
            }

            // left steps
            pos.ChangePosition(this.Position.Line, this.Position.Column);
            for (int i = 0;i < 8;i++)
            {
                pos.Column--;
                if (Board.IsValidPosition(pos))
                {
                    if (Board.GetPieceByPosition(pos) == null)
                    {
                        steps[pos.Column, pos.Line] = true;
                    }
                }
            }

            return steps;
        }

        public override string ToString()
        {
            if (this.IsWhite) return "R";

            return "r";
        }
    }
}
