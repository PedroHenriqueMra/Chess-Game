namespace ChessGame.Piece.Entity
{
    using ChessGame.Logic.PositionGame;
    using ChessGame.Piece.PieceModel;
    using ChessGame.Table;
    using ChessGame.Game.main;

    public class Rook : Piece
    {
        public Rook(Game game, bool isWhite, Position position)
        : base (game, isWhite, position) {}

        public override bool[,] GetPositionsToMove()
        {
            bool[,] steps = new bool[Game.Board.Lenght[0], Game.Board.Lenght[1]];
            Position pos = new Position();

            // up steps
            pos.ChangePosition(this.Position.Column,this.Position.Line);
            for (int i = 0;i < 8;i++)
            {
                pos.Line--;
                if (Game.Board.IsValidPosition(pos))
                {
                    if (Game.Board.GetPieceByPosition(pos) == null)
                    {
                        steps[pos.Column, pos.Line] = true;
                        continue;
                    }
                }

                break;
            }

            // bottom steps
            pos.ChangePosition(this.Position.Column,this.Position.Line);
            for (int i = 0;i < 8;i++)
            {
                pos.Line++;
                if (Game.Board.IsValidPosition(pos))
                {
                    if (Game.Board.GetPieceByPosition(pos) == null)
                    {
                        steps[pos.Column, pos.Line] = true;
                        continue;
                    }
                }

                break;
            }

            // right steps
            pos.ChangePosition(this.Position.Column,this.Position.Line);
            for (int i = 0;i < 8;i++)
            {
                pos.Column++;
                if (Game.Board.IsValidPosition(pos))
                {
                    if (Game.Board.GetPieceByPosition(pos) == null)
                    {
                        steps[pos.Column, pos.Line] = true;
                        continue;
                    }
                }

                break;
            }

            // left steps
            pos.ChangePosition(this.Position.Column,this.Position.Line);
            for (int i = 0;i < 8;i++)
            {
                pos.Column--;
                if (Game.Board.IsValidPosition(pos))
                {
                    if (Game.Board.GetPieceByPosition(pos) == null)
                    {
                        steps[pos.Column, pos.Line] = true;
                        continue;
                    }
                }

                break;
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
