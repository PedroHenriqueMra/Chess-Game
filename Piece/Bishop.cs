namespace ChessGame.Piece.Entity
{
    using ChessGame.Logic.PositionGame;
    using ChessGame.Piece.PieceModel;
    using ChessGame.Logic.Game;
    using ChessGame.Logic.Player.Color;

    public class Bishop : Piece
    {
        public Bishop(Game game, PlayerColor color, Position position)
        : base (game, color, position) {}


        public override bool[,] GetPositionsToMove()
        {
            bool[,] steps = new bool[Game.Board.Lenght[0], Game.Board.Lenght[1]];
            Position pos = new Position(0,0);
            
            pos.ChangePosition(this.Position.Column,this.Position.Line);
            // for to iter the right-top diagonal
            for (int diag = 0; diag < 8;diag++)
            {
                DiagonalRightTop(ref pos);
                if (Game.Board.IsValidPosition(pos))
                {
                    Piece? getPieceByPos = Game.Board.GetPieceByPosition(pos);
                    if (getPieceByPos == null)
                    {
                        steps[pos.Column, pos.Line] = true;
                        continue;
                    }
                    else if (getPieceByPos.Color != this.Color)
                    {
                        steps[pos.Column, pos.Line] = true;
                    }
                }

                break;
            }

            pos.ChangePosition(this.Position.Column,this.Position.Line);
            // for to iter the right-bottom diagonal
            for (int diag = 0; diag < 8;diag++)
            {
                DiagonalRightBottom(ref pos);
                if (Game.Board.IsValidPosition(pos))
                {
                    Piece? getPieceByPos = Game.Board.GetPieceByPosition(pos);
                    if (getPieceByPos == null)
                    {
                        steps[pos.Column, pos.Line] = true;
                        continue;
                    }
                    else if (getPieceByPos.Color != this.Color)
                    {
                        steps[pos.Column, pos.Line] = true;
                    }
                }

                break;
            }

            pos.ChangePosition(this.Position.Column,this.Position.Line);
            // for to iter the left-top diagonal
            for (int diag = 0; diag < 8;diag++)
            {
                DiagonalLeftTop(ref pos);
                if (Game.Board.IsValidPosition(pos))
                {
                    Piece? getPieceByPos = Game.Board.GetPieceByPosition(pos);
                    if (getPieceByPos == null)
                    {
                        steps[pos.Column, pos.Line] = true;
                        continue;
                    }
                    else if (getPieceByPos.Color != this.Color)
                    {
                        steps[pos.Column, pos.Line] = true;
                    }
                }

                break;
            }

            pos.ChangePosition(this.Position.Column,this.Position.Line);
            // for to iter the right-bottom diagonal
            for (int diag = 0; diag < 8;diag++)
            {
                DiagonalLeftBottom(ref pos);
                if (Game.Board.IsValidPosition(pos))
                {
                    Piece? getPieceByPos = Game.Board.GetPieceByPosition(pos);
                    if (getPieceByPos == null)
                    {
                        steps[pos.Column, pos.Line] = true;
                        continue;
                    }
                    else if (getPieceByPos.Color != this.Color)
                    {
                        steps[pos.Column, pos.Line] = true;
                    }
                }

                break;
            }

            return steps;
        }

        private void DiagonalRightTop(ref Position currentyPos)
        {
            currentyPos.Column++;
            currentyPos.Line--;
        }
        
        private void DiagonalRightBottom(ref Position currentyPos)
        {
            currentyPos.Column++;
            currentyPos.Line++;
        }

        private void DiagonalLeftTop(ref Position currentyPos)
        {
            currentyPos.Column--;
            currentyPos.Line--;
        }

        private void DiagonalLeftBottom(ref Position currentyPos)
        {
            currentyPos.Column--;
            currentyPos.Line++;
        }

        public override Piece Clone()
        {
            return new Bishop(
                Game = this.Game,
                Color = this.Color,
                Position = new Position(this.Position.Column,this.Position.Line)
            );
        }

        public override string ToString()
        {
            if (this.Color == PlayerColor.White) return "B";

            return "b";
        }
    }
}
