using ChessGame.Logic.PositionGame;

using Wacton.Unicolour;

namespace ChessGame.Table
{
    using ChessGame.Piece.PieceModel;

    public class Board
    {
        public Piece[,] Pieces { get; set; }
        public Board(int numLine, int numCol)
        {
            // booting slot pieces (headquarters)
            Pieces = new Piece[numLine, numCol];
        }

        public Piece GetPieceByPosition(Position position)
        {
            if (Pieces[position.Line, position.Column] != null)
            {
                return Pieces[position.Line, position.Column];
            }

            return null;
        }

        // public void MovePiece()
        // {

        // }
    }
}
