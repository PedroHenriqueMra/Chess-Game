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

            if (Movements == 0)
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

            // En passant movements
            // En passant to right
            if (CheckToRight(Position))
            {
                pos = RigthDiagonalSteps(Position);
                steps[pos.Column,pos.Line] = true;
            }

            // En passant to left
            if (CheckToLeft(Position))
            {
                pos = LeftDiagonalSteps(Position);
                steps[pos.Column,pos.Line] = true;
            }

            return steps;
        }

        public Position OneStepAhead(Position pos)
        {
            Position newPosition = new Position(0,0);
            return IsWhite
                ? newPosition.ChangePosition(pos.Column, pos.Line -= 1)
                : newPosition.ChangePosition(pos.Column, pos.Line += 1);
        }

        public Position TwoStepsAhead(Position pos)
        {
            Position newPosition = new Position(0,0);
                return IsWhite
                ? newPosition.ChangePosition(pos.Column, pos.Line -= 2)
                : newPosition.ChangePosition(pos.Column, pos.Line += 2);
        }

        public Position RigthDiagonalSteps(Position pos)
        {
            Position newPosition = new Position(0,0);
                return IsWhite
                ? newPosition.ChangePosition(pos.Column += 1, pos.Line -= 1)
                : newPosition.ChangePosition(pos.Column -= 1, pos.Line += 1);
        }

        public Position LeftDiagonalSteps(Position pos)
        {
            Position newPosition = new Position(0,0);
                return IsWhite
                ? newPosition.ChangePosition(pos.Column -= 1, pos.Line -= 1)
                : newPosition.ChangePosition(pos.Column += 1, pos.Line += 1);
        }

        // check enemy pieces En Passant 
        public bool CheckToRight(Position pos)
        {
            Position posToCheck = 
                IsWhite
                ? pos.ChangePosition(pos.Column += 1, pos.Line)
                : pos.ChangePosition(pos.Column -= 1, pos.Line);

            Piece? enemyPiece = Game.Board.GetPieceByPosition(posToCheck);
            if (enemyPiece != null && enemyPiece.IsWhite != this.IsWhite)
            {
                if (Game.PawnEnPassant != null && Game.PawnEnPassant.Equals(enemyPiece))
                {
                    return true;
                }
            }

            return false;
        }

        public bool CheckToLeft(Position pos)
        {
            Position posToCheck = 
                IsWhite
                ? pos.ChangePosition(pos.Column -= 1, pos.Line)
                : pos.ChangePosition(pos.Column += 1, pos.Line);

            Piece? enemyPiece = Game.Board.GetPieceByPosition(posToCheck);
            if (enemyPiece != null && enemyPiece.IsWhite != this.IsWhite)
            {
                if (Game.PawnEnPassant != null && Game.PawnEnPassant.Equals(enemyPiece))
                {
                    return true;
                }
            }

            return false;
        }


        public override string ToString()
        {
            if (this.IsWhite) return "P";

            return "p";
        }
    }
}
