using ChessGame.Table;

namespace ChessGame.Piece.Entity
{
    using System.Dynamic;
    using System.Reflection.Metadata;
    using ChessGame.Logic.PositionGame;
    using ChessGame.Piece.PieceModel;
    using ChessGame.Game.main;
    using System.Security.Cryptography.X509Certificates;
    using System.Runtime.InteropServices;
    using System.Reflection.Metadata.Ecma335;

    public class Pawn : Piece
    {
        public bool InPassant { get; set; } = default;
        public Pawn(Board board, bool IsWhite)
        : base(board, IsWhite) { }

        // plus line = get down on the board
        // minus line = get up on the board
        public override bool[,] GetPositionsToStep()
        {
            bool[,] steps = new bool[Board.Lenght[0], Board.Lenght[1]];
            Position pos = new Position(0, 0);

            if (IsWhite)
            {
                // Positions to move piece
                pos.ChangePosition(Position.Line - 1, Position.Column);
                if (Board.GetPieceByPosition(pos) == null)
                {
                    steps[pos.Line, pos.Column] = true;
                }
                if (Moves == 0)
                {
                    pos.ChangePosition(Position.Line - 2, Position.Column);
                    if (Board.GetPieceByPosition(pos) == null)
                    {
                        steps[pos.Line, pos.Column] = true;
                    }
                }

                // Positions to catch enemy piece to right
                pos.ChangePosition(Position.Line - 1, Position.Column + 1);
                if (Board.GetPieceByPosition(pos) != null && !Board.GetPieceByPosition(pos).IsWhite)
                {
                    steps[pos.Line, pos.Column] = true;
                }
                // Position to catch enemy piece to left
                pos.ChangePosition(Position.Line - 1, Position.Column - 1);
                if (!Board.GetPieceByPosition(pos).IsWhite)
                {
                    steps[pos.Line, pos.Column] = true;
                }

                // En passant to right
                pos.ChangePosition(Position.Line - 1, Position.Column + 1);
                Pawn piece = (Pawn)Board.GetPieceByPosition(pos);
                if (piece != null && !piece.IsWhite && piece is Piece)
                {
                    if (piece.InPassant)
                    {
                        steps[pos.Line, pos.Column] = true;
                    }
                }

                // En passant to left
                pos.ChangePosition(Position.Line - 1, Position.Column - 1);
                piece = (Pawn)Board.GetPieceByPosition(pos);
                if (piece != null && !piece.IsWhite && piece is Piece)
                {
                    if (piece.InPassant)
                    {
                        steps[pos.Line, pos.Column] = true;
                    }
                }
            }
            else
            {
                pos.ChangePosition(Position.Line + 1, Position.Column);
                if (Board.GetPieceByPosition(pos) == null)
                {
                    steps[pos.Line, pos.Column] = true;
                }
                if (Moves == 0)
                {
                    pos.ChangePosition(Position.Line + 2, Position.Column);
                    if (Board.GetPieceByPosition(pos) == null)
                    {
                        steps[pos.Line, pos.Column] = true;
                    }
                }

                // Positions to catch enemy piece to right
                pos.ChangePosition(Position.Line + 1, Position.Column - 1);
                if (Board.GetPieceByPosition(pos) != null && !Board.GetPieceByPosition(pos).IsWhite)
                {
                    steps[pos.Line, pos.Column] = true;
                }
                // Position to catch enemy piece to left
                pos.ChangePosition(Position.Line + 1, Position.Column + 1);
                if (!Board.GetPieceByPosition(pos).IsWhite)
                {
                    steps[pos.Line, pos.Column] = true;
                }

                // En passant to right
                pos.ChangePosition(Position.Line + 1, Position.Column - 1);
                Pawn piece = (Pawn)Board.GetPieceByPosition(pos);
                if (piece != null && !piece.IsWhite && piece is Piece)
                {
                    if (piece.InPassant)
                    {
                        steps[pos.Line, pos.Column] = true;
                    }
                }

                // En passant to left
                pos.ChangePosition(Position.Line + 1, Position.Column + 1);
                piece = (Pawn)Board.GetPieceByPosition(pos);
                if (piece != null && !piece.IsWhite && piece is Piece)
                {
                    if (piece.InPassant)
                    {
                        steps[pos.Line, pos.Column] = true;
                    }
                    
                }
            }

            return steps;
        }

        public bool[,] GetPositions(bool isWhite, Position basePosition)
        {
            var oneAhead = new Func<Position, Position>(pos => {
                Position newPosition = new Position(0,0);
                return isWhite
                ? newPosition.ChangePosition(pos.Line -= 1, pos.Column)
                : newPosition.ChangePosition(pos.Line += 1, pos.Column);
            });

            var twoAhead = new Func<Position, Position>(pos => {
                Position newPosition = new Position(0,0);
                return isWhite
                ? newPosition.ChangePosition(pos.Line -= 2, pos.Column)
                : newPosition.ChangePosition(pos.Line += 2, pos.Column);
            });

            var rightDiagonal = new Func<Position, Position>(pos => {
                Position newPosition = new Position(0,0);
                return isWhite
                ? newPosition.ChangePosition(pos.Line -= 1, pos.Column += 1)
                : newPosition.ChangePosition(pos.Line += 1, pos.Column -= 1);
            });
            

            var leftDiagonal = new Func<Position, Position>(pos => {
                Position newPosition = new Position(0,0);
                return isWhite
                ? newPosition.ChangePosition(pos.Line -= 1, pos.Column -= 1)
                : newPosition.ChangePosition(pos.Line += 1, pos.Column += 1);
            });
    
            
            bool[,] steps = new bool[Board.Lenght[0], Board.Lenght[1]];
            Position pos = new Position(0,0);

            // Positions to move piece
            pos = oneAhead(basePosition);
            if (Board.IsValidSpace(pos) && Board.GetPieceByPosition(pos) == null)
            {
                steps[pos.Line, pos.Column] = true;
            }
            if (Moves == 0)
            {
                pos = twoAhead(basePosition);
                if (Board.IsValidSpace(pos) && Board.GetPieceByPosition(pos) == null)
                {
                    steps[pos.Line, pos.Column] = true;
                }
            }

            // Positions to catch enemy piece to right
            pos = rightDiagonal(basePosition);
            if (Board.IsValidSpace(pos) && Board.GetPieceByPosition(pos) != null && !Board.GetPieceByPosition(pos).IsWhite)
            {
                steps[pos.Line, pos.Column] = true;
            }
            // Position to catch enemy piece to left
            pos = leftDiagonal(basePosition);
            if (Board.IsValidSpace(pos) && Board.GetPieceByPosition(pos) != null! && Board.GetPieceByPosition(pos).IsWhite)
            {
                steps[pos.Line, pos.Column] = true;
            }

            // En passant to right
            pos = rightDiagonal(basePosition);
            Pawn piece = (Pawn)Board.GetPieceByPosition(pos);
            if (Board.IsValidSpace(pos) && piece != null && !piece.IsWhite && piece is Piece)
            {
                if (piece.InPassant)
                {
                    steps[pos.Line, pos.Column] = true;
                }
            }

            // En passant to left
            pos = leftDiagonal(basePosition);
            piece = (Pawn)Board.GetPieceByPosition(pos);
            if (Board.IsValidSpace(pos) && piece != null && !piece.IsWhite && piece is Piece)
            {
                if (piece.InPassant)
                {
                    steps[pos.Line, pos.Column] = true;
                }
            }

            return steps;
        }

        public override string ToString()
        {
            if (IsWhite)
            {
                return "P";
            }

            return "p";
        }
    }
}
