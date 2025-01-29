namespace ChessGame.Table
{
    using ChessGame.Piece.PieceModel;
    using ChessGame.Logic.PositionGame;
    using Wacton.Unicolour;

    public class Board
    {
        public Piece[,] Pieces { get; set; }
        public int[] Lenght { get; set; } // [0] == line [1] == column
        public Board(int numLine, int numCol)
        {
            // booting slot pieces (headquarters)
            Pieces = new Piece[numLine, numCol];
            Lenght = [numLine, numCol];
        }

        public Piece? GetPieceByPosition(Position position)
        {
            if (position.Column < 0 || position.Column >= Lenght[1]) return null;
            if (position.Line < 0 || position.Line >= Lenght[1]) return null;

            if (Pieces[position.Line, position.Column] != null)
            {
                return Pieces[position.Line, position.Column];
            }

            return null;
        }

        public bool IsValidSpace(Position position)
        {
            if (position.Line < 0 || position.Line >= Lenght[0])
            {
                return false;
            }
            else if (position.Column < 0 || position.Column >= Lenght[1])
            {
                return false;
            }
            
            return true;
        }

        // public bool IsPossibleToMove(Position piecePos, Position target)
        // {
        //     var piece = GetPieceByPosition(piecePos);
        //     if (piece == null || !piece.IsYour) return false;

        //     // Check piece way. If there are not pieces in piece way
        //     for (var line = 0;line < target.Line;line++)
        //     {
        //         for (var col = 0;line < target.Column;col++)
        //         {
        //             // Check: "if there's a piece in way" and "if piece can jumps (like Knight-piece)"
        //             if (Pieces[line, col] != null && !Pieces[line, col].Jump)
        //             {
        //                 return false;
        //             }
        //         }
        //     }
        // }

        public void MovePiece(Piece piece, Position pos)
        {

        }

        private void RemovePiece(Position pos)
        {

        }

        private void PutPiece(Position target, Piece piece)
        {

        }

        public bool IsValidPosition(Position pos)
        {
            try
            {
                var check = Pieces[pos.Line, pos.Column];
                return true;
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            } 
        }

        // private bool EatPiece(Piece deadPiece)
        // {
        //     // logica para ver se é possivel matar a peça
        //     if (!IsPossibleToEat()) return false;

        //     RemovePiece(deadPiece.Position);
        //     deadPiece.KillPiece();
        //     return true;
        // }

        // public bool IsPossibleToEat(Piece piece, Position target)
        // {

        // }
    }
}
