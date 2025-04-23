using ChessGame.Table;

namespace ChessGame.Piece.Entity
{
    using ChessGame.Logic.PositionGame;
    using ChessGame.Piece.PieceModel;
    using ChessGame.Game.main;

    public class Pawn : Piece
    {
        public Pawn(Game game, bool isWhite, Position position)
        : base(game, isWhite, position) { }

        // plus line = get down on the board
        // minus line = get up on the board
        public override bool[,] GetPositionsToMove()
        {                 
            bool[,] steps = new bool[Game.Board.Lenght[0], Game.Board.Lenght[1]];
            Position pos = new Position(0,0);

            // Positions to move piece
            pos = OneStepAhead(Position);
            if (Game.Board.IsValidPosition(pos) && Game.Board.GetPieceByPosition(pos) == null)
            {
                steps[pos.Column,pos.Line] = true;
            }
            if (Moves == 0)
            {
                pos = TwoStepsAhead(Position);
                if (Game.Board.IsValidPosition(pos) && Game.Board.GetPieceByPosition(pos) == null)
                {
                    steps[pos.Column,pos.Line] = true;
                }
            }

            // Positions to catch enemy piece to right
            pos = RigthDiagonalSteps(Position);
            if (Game.Board.IsValidPosition(pos) && Game.Board.GetPieceByPosition(pos) != null && Game.Board.GetPieceByPosition(pos).IsWhite != IsWhite)
            {
                steps[pos.Column,pos.Line] = true;
            }
            // Position to catch enemy piece to left
            pos = LeftDiagonalSteps(Position);
            if (Game.Board.IsValidPosition(pos) && Game.Board.GetPieceByPosition(pos) != null! && Game.Board.GetPieceByPosition(pos).IsWhite != IsWhite)
            {
                steps[pos.Column,pos.Line] = true;
            }

            // En passant to right
            pos = RigthDiagonalSteps(Position);
            Pawn? piece = Game.Board.GetPieceByPosition(pos) as Pawn;
            if (piece != null && piece is Pawn && Game.Board.IsValidPosition(pos) && piece.IsWhite != IsWhite)
            {
                if (Game.PawnInPassant.Contains(piece.GetHashCode()))
                {
                    steps[pos.Column,pos.Line] = true;
                }
            }

            // En passant to left
            pos = LeftDiagonalSteps(Position);
            piece = Game.Board.GetPieceByPosition(pos) as Pawn;
            if (piece != null && piece is Pawn && Game.Board.IsValidPosition(pos) && piece.IsWhite != IsWhite)
            {
                if (Game.PawnInPassant.Contains(piece.GetHashCode()))
                {
                    steps[pos.Column,pos.Line] = true;
                }
            }

            return steps;
        }

        private Position OneStepAhead(Position pos)
        {
            Position newPosition = new Position(0,0);
            return IsWhite
                ? newPosition.ChangePosition(pos.Line -= 1, pos.Column)
                : newPosition.ChangePosition(pos.Line += 1, pos.Column);
        }

        private Position TwoStepsAhead(Position pos)
        {
            Position newPosition = new Position(0,0);
                return IsWhite
                ? newPosition.ChangePosition(pos.Line -= 2, pos.Column)
                : newPosition.ChangePosition(pos.Line += 2, pos.Column);
        }

        private Position RigthDiagonalSteps(Position pos)
        {
            Position newPosition = new Position(0,0);
                return IsWhite
                ? newPosition.ChangePosition(pos.Line -= 1, pos.Column += 1)
                : newPosition.ChangePosition(pos.Line += 1, pos.Column -= 1);
        }

        private Position LeftDiagonalSteps(Position pos)
        {
            Position newPosition = new Position(0,0);
                return IsWhite
                ? newPosition.ChangePosition(pos.Line -= 1, pos.Column -= 1)
                : newPosition.ChangePosition(pos.Line += 1, pos.Column += 1);
        }

        public override string ToString()
        {
            if (this.IsWhite) return "P";

            return "p";
        }
    }
}
