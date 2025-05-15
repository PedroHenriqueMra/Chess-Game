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
                    Piece? enemyPiece = Game.Board.GetPieceByPosition(pos);
                    if (enemyPiece == null)
                    {
                        steps[pos.Column, pos.Line] = true;
                        continue;
                    }

                    if (enemyPiece.IsWhite != IsWhite)
                    {
                        steps[pos.Column, pos.Line] = true;
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
                    Piece? enemyPiece = Game.Board.GetPieceByPosition(pos);
                    if (enemyPiece == null)
                    {
                        steps[pos.Column, pos.Line] = true;
                        continue;
                    }

                    if (enemyPiece.IsWhite != IsWhite)
                    {
                        steps[pos.Column, pos.Line] = true;
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
                    Piece? enemyPiece = Game.Board.GetPieceByPosition(pos);
                    if (enemyPiece == null)
                    {
                        steps[pos.Column, pos.Line] = true;
                        continue;
                    }

                    if (enemyPiece.IsWhite != IsWhite)
                    {
                        steps[pos.Column, pos.Line] = true;
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
                    Piece? enemyPiece = Game.Board.GetPieceByPosition(pos);
                    if (enemyPiece == null)
                    {
                        steps[pos.Column, pos.Line] = true;
                        continue;
                    }

                    if (enemyPiece.IsWhite != IsWhite)
                    {
                        steps[pos.Column, pos.Line] = true;
                    }
                }

                break;
            }

            return steps;
        }

        public override Piece Clone()
        {
            return new Rook(
                Game = this.Game,
                IsWhite = this.IsWhite,
                Position = new Position(this.Position.Column,this.Position.Line)
            );
        }

        public override string ToString()
        {
            if (this.IsWhite) return "R";

            return "r";
        }
    }
}
