namespace ChessGame.Piece.Entity
{
    using ChessGame.Logic.PositionGame;
    using ChessGame.Piece.PieceModel;
    using ChessGame.Table;

    public class Bishop : Piece
    {
        public Bishop(Board board, bool isWhite, Position position)
        : base (board, isWhite, position) {}


        public override bool[,] GetPositionsToMove()
        {
            bool[,] steps = new bool[Board.Lenght[0], Board.Lenght[1]];
            Position pos = new Position(0,0);
            
            pos.ChangePosition(this.Position.Line,this.Position.Column);
            // for to iter the right-top diagonal
            for (int diag = 0; diag < 8;diag++)
            {
                DiagonalRightTop(ref pos);
                if (Board.IsValidPosition(pos))
                {
                    if (Board.GetPieceByPosition(pos) == null)
                    {
                        steps[pos.Column, pos.Line] = true;
                    }
                }
            }

            pos.ChangePosition(this.Position.Line,this.Position.Column);
            // for to iter the right-bottom diagonal
            for (int diag = 0; diag < 8;diag++)
            {
                DiagonalRightBottom(ref pos);
                if (Board.IsValidPosition(pos))
                {
                    if (Board.GetPieceByPosition(pos) == null)
                    {
                        steps[pos.Column, pos.Line] = true;
                    }
                }
            }

            pos.ChangePosition(this.Position.Line,this.Position.Column);
            // for to iter the left-top diagonal
            for (int diag = 0; diag < 8;diag++)
            {
                DiagonalLeftTop(ref pos);
                if (Board.IsValidPosition(pos))
                {
                    if (Board.GetPieceByPosition(pos) == null)
                    {
                        steps[pos.Column, pos.Line] = true;
                    }
                }
            }

            pos.ChangePosition(this.Position.Line,this.Position.Column);
            // for to iter the right-bottom diagonal
            for (int diag = 0; diag < 8;diag++)
            {
                DiagonalLeftBottom(ref pos);
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
