namespace ChessGame.Table
{
    using ChessGame.Piece.PieceModel;
    using ChessGame.Logic.PositionGame;

    public class Board
    {
        public Piece[,] Pieces { get; set; }
        public int NumColumn { get; set; }
        public int NumLine { get; set; }
        public Board(int numCol,int numLine)
        {
            NumColumn = numCol;
            NumLine = numLine;
            Pieces = new Piece[numCol, numLine];
        }

        public Piece? GetPieceByPosition(Position position)
        {
            if (position.Column < 0 || position.Column >= NumColumn) return null;
            if (position.Line < 0 || position.Line >= NumLine) return null;

            return Pieces[position.Column,position.Line];
        }

        public void MovePieceOnBoard(Piece piece, Position target)
        {
            if (IsValidPosition(target))
            {
                this.RemovePiece(piece);
                this.PutPiece(piece, target);
            }
        }

        public void PutPiece(Piece piece, Position target)
        {
            if (this.GetPieceByPosition(target) == null)
            {
                this.Pieces[target.Column, target.Line] = piece;
            }
        }
        
        public void RemovePiece(Piece piece)
        {
            this.Pieces[piece.Position.Column, piece.Position.Line] = null;
        }

        public bool IsValidPosition(Position pos)
        {
            try
            {
                var check = Pieces[pos.Column,pos.Line];
                return true;
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            } 
        }
    }
}
