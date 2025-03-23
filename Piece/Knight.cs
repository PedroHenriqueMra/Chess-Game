namespace ChessGame.Piece.Entity
{
    using ChessGame.Logic.PositionGame;
    using ChessGame.Piece.PieceModel;
    using ChessGame.Table;

    public class Knight : Piece
    {
        public Knight(Board board, bool isWhite, Position position)
        : base (board, isWhite, position)
        {
            this.Jump = true;
        }

        public override bool[,] GetPositionsToMove()
        {
            bool[,] steps = new bool[Board.Lenght[0], Board.Lenght[1]];
            Position pos = new Position(0,0);

            // movement to top-right
            pos.ChangePosition(this.Position.Line,this.Position.Column);
            pos.Line = pos.Line - 2;
            pos.Column++;
            if (Board.IsValidPosition(pos))
            {
                if (Board.GetPieceByPosition(pos) == null || !Board.GetPieceByPosition(pos).IsWhite)
                {
                    steps[pos.Line,pos.Column] = true;
                }
            }

            // movement to top-left
            pos.ChangePosition(this.Position.Line,this.Position.Column);
            pos.Line = pos.Line - 2;
            pos.Column--;
            if (Board.IsValidPosition(pos))
            {
                if (Board.GetPieceByPosition(pos) == null || !Board.GetPieceByPosition(pos).IsWhite)
                {
                    steps[pos.Line,pos.Column] = true;
                }
            }

            // movement to left-top
            pos.ChangePosition(this.Position.Line,this.Position.Column);
            pos.Column = pos.Column - 2;
            pos.Line--;
            if (Board.IsValidPosition(pos))
            {
                if (Board.GetPieceByPosition(pos) == null || !Board.GetPieceByPosition(pos).IsWhite)
                {
                    steps[pos.Line,pos.Column] = true;
                }
            }
            
            // movement to left-bottom
            pos.ChangePosition(this.Position.Line,this.Position.Column);
            pos.Column = pos.Column - 2;
            pos.Line++;
            if (Board.IsValidPosition(pos))
            {
                if (Board.GetPieceByPosition(pos) == null || !Board.GetPieceByPosition(pos).IsWhite)
                {
                    steps[pos.Line,pos.Column] = true;
                }
            }

            // movement to bottom-left
            pos.ChangePosition(this.Position.Line,this.Position.Column);
            pos.Line = pos.Line + 2;
            pos.Column--;
            if (Board.IsValidPosition(pos))
            {
                if (Board.GetPieceByPosition(pos) == null || !Board.GetPieceByPosition(pos).IsWhite)
                {
                    steps[pos.Line,pos.Column] = true;
                }
            }

            // movement to bottom-right
            pos.ChangePosition(this.Position.Line,this.Position.Column);
            pos.Line = pos.Line + 2;
            pos.Column++;
            if (Board.IsValidPosition(pos))
            {
                if (Board.GetPieceByPosition(pos) == null || !Board.GetPieceByPosition(pos).IsWhite)
                {
                    steps[pos.Line,pos.Column] = true;
                }
            }

            // movement to right-top
            pos.ChangePosition(this.Position.Line,this.Position.Column);
            pos.Column = pos.Column + 2;
            pos.Line--;
            if (Board.IsValidPosition(pos))
            {
                if (Board.GetPieceByPosition(pos) == null || !Board.GetPieceByPosition(pos).IsWhite)
                {
                    steps[pos.Line,pos.Column] = true;
                }
            }

            // movement to right-bottom
            pos.ChangePosition(this.Position.Line,this.Position.Column);
            pos.Column = pos.Column + 2;
            pos.Line++;
            if (Board.IsValidPosition(pos))
            {
                if (Board.GetPieceByPosition(pos) == null || !Board.GetPieceByPosition(pos).IsWhite)
                {
                    steps[pos.Line,pos.Column] = true;
                }
            }

            return steps;
        }



        public override string ToString()
        {
            if (this.IsWhite) return "K";

            return "k";
        }
    }
}
