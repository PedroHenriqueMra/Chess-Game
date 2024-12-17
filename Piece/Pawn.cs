using ChessGame.Table;

namespace ChessGame.Piece.Entity
{
    using ChessGame.Piece.PieceModel;

    public class Pawn : Piece
    {
        public Pawn(Board board, bool isYour)
        : base(board, isYour) {}

        public override int[][] ReturnPieceSteps()
        {
            return [[2, 0], [3, 0]];
        }

        public override string ToString()
        {
            if (IsYour)
            {
                return "P";
            }

            return "p";
        }
    }
}
