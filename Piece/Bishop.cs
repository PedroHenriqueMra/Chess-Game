namespace ChessGame.Piece.Entity
{
    using ChessGame.Logic.PositionGame;
    using ChessGame.Piece.PieceModel;
    using ChessGame.Table;
    using ChessGame.Game.main;

    public class Bishop : Piece
    {
        public Bishop(Game game, bool isWhite, Position position)
        : base (game, isWhite, position) {}


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
                    if (Game.Board.GetPieceByPosition(pos) == null)
                    {
                        steps[pos.Column, pos.Line] = true;
                        continue;
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
                    if (Game.Board.GetPieceByPosition(pos) == null)
                    {
                        steps[pos.Column, pos.Line] = true;
                        continue;
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
                    if (Game.Board.GetPieceByPosition(pos) == null)
                    {
                        steps[pos.Column, pos.Line] = true;
                        continue;
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

        public override string ToString()
        {
            if (this.IsWhite) return "B";

            return "b";
        }
    }
}
