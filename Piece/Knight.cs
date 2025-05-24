namespace ChessGame.Piece.Entity
{
    using ChessGame.Logic.PositionGame;
    using ChessGame.Piece.PieceModel;
    using ChessGame.Logic.Game;
    using ChessGame.Logic.Player.Color;

    public class Knight : Piece
    {
        public Knight(Game game, PlayerColor color, Position position)
        : base (game, color, position) {}

        public override bool[,] GetPositionsToMove()
        {
            bool[,] steps = new bool[Game.Board.NumColumn, Game.Board.NumLine];
            Position pos = new Position(0,0);

            // movement to top-right
            pos.ChangePosition(this.Position.Column, this.Position.Line);
            pos.Line = pos.Line - 2;
            pos.Column++;
            if (Game.Board.IsValidPosition(pos))
            {
                Piece? piece = Game.Board.GetPieceByPosition(pos);
                if (piece == null || piece.Color != this.Color)
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
                if (piece == null || piece.Color != this.Color)
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
                if (piece == null || piece.Color != this.Color)
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
                if (piece == null || piece.Color != this.Color)
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
                if (piece == null || piece.Color != this.Color)
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
                if (piece == null || piece.Color != this.Color)
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
                if (piece == null || piece.Color != this.Color)
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
                if (piece == null || piece.Color != this.Color)
                {
                    steps[pos.Column,pos.Line] = true;
                }
            }

            return steps;
        }

        public override Piece Clone()
        {
            Knight clone = new Knight(
                Game = this.Game,
                Color = this.Color,
                Position = new Position(this.Position.Column, this.Position.Line)
            );
            clone.Movements = this.Movements;
            
            return clone;
        }

        public override string ToString()
        {
            if (this.Color == PlayerColor.White) return "H";

            return "h";
        }
    }
}
