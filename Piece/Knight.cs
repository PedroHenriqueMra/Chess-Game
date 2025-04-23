namespace ChessGame.Piece.Entity
{
    using ChessGame.Logic.PositionGame;
    using ChessGame.Piece.PieceModel;
    using ChessGame.Table;
    using ChessGame.Game.main;

    public class Knight : Piece
    {
        public Knight(Game game, bool isWhite, Position position)
        : base (game, isWhite, position) {}

        public override bool[,] GetPositionsToMove()
        {
            bool[,] steps = new bool[Game.Board.Lenght[0], Game.Board.Lenght[1]];
            Position pos = new Position(0,0);

            // movement to top-right
            pos.ChangePosition(this.Position.Column, this.Position.Line);
            pos.Line = pos.Line - 2;
            pos.Column++;
            if (Game.Board.IsValidPosition(pos))
            {
                Piece? piece = Game.Board.GetPieceByPosition(pos);
                if (piece == null || !piece.IsWhite)
                {
                    steps[pos.Column,pos.Line] = true;
                }
            }

            // movement to top-left
            pos.ChangePosition(this.Position.Column, this.Position.Line);
            pos.Line = pos.Line - 2;
            pos.Column--;
            if (Game.Board.IsValidPosition(pos))
            {
                Piece? piece = Game.Board.GetPieceByPosition(pos);
                if (piece == null || !piece.IsWhite)
                {
                    steps[pos.Column,pos.Line] = true;
                }
            }

            // movement to left-top
            pos.ChangePosition(this.Position.Column,this.Position.Line);
            pos.Column = pos.Column - 2;
            pos.Line--;
            if (Game.Board.IsValidPosition(pos))
            {
                Piece? piece = Game.Board.GetPieceByPosition(pos);
                if (piece == null || !piece.IsWhite)
                {
                    steps[pos.Column,pos.Line] = true;
                }
            }
            
            // movement to left-bottom
            pos.ChangePosition(this.Position.Column,this.Position.Line);
            pos.Column = pos.Column - 2;
            pos.Line++;
            if (Game.Board.IsValidPosition(pos))
            {
                Piece? piece = Game.Board.GetPieceByPosition(pos);
                if (piece == null || !piece.IsWhite)
                {
                    steps[pos.Column,pos.Line] = true;
                }
            }

            // movement to bottom-left
            pos.ChangePosition(this.Position.Column,this.Position.Line);
            pos.Line = pos.Line + 2;
            pos.Column--;
            if (Game.Board.IsValidPosition(pos))
            {
                Piece? piece = Game.Board.GetPieceByPosition(pos);
                if (piece == null || !piece.IsWhite)
                {
                    steps[pos.Column,pos.Line] = true;
                }
            }

            // movement to bottom-right
            pos.ChangePosition(this.Position.Column,this.Position.Line);
            pos.Line = pos.Line + 2;
            pos.Column++;
            if (Game.Board.IsValidPosition(pos))
            {
                Piece? piece = Game.Board.GetPieceByPosition(pos);
                if (piece == null || !piece.IsWhite)
                {
                    steps[pos.Column,pos.Line] = true;
                }
            }

            // movement to right-top
            pos.ChangePosition(this.Position.Column,this.Position.Line);
            pos.Column = pos.Column + 2;
            pos.Line--;
            if (Game.Board.IsValidPosition(pos))
            {
                Piece? piece = Game.Board.GetPieceByPosition(pos);
                if (piece == null || !piece.IsWhite)
                {
                    steps[pos.Column,pos.Line] = true;
                }
            }

            // movement to right-bottom
            pos.ChangePosition(this.Position.Column,this.Position.Line);
            pos.Column = pos.Column + 2;
            pos.Line++;
            if (Game.Board.IsValidPosition(pos))
            {
                Piece? piece = Game.Board.GetPieceByPosition(pos);
                if (piece == null || !piece.IsWhite)
                {
                    steps[pos.Column,pos.Line] = true;
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
