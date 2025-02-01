namespace ChessGame.Table
{
    using ChessGame.Piece.PieceModel;
    using ChessGame.Logic.PositionGame;
    using Wacton.Unicolour;
    using ChessGame.Logic.SystemPlayer;
    using ChessGame.Game.main;

    public class Board
    {
        public Piece[,] Pieces { get; set; }
        public int[] Lenght { get; set; } // [0] == line [1] == column
        public Game Game { get; set; }
        public Board(int numLine, int numCol, Game game)
        {
            // booting slot pieces (headquarters)
            Pieces = new Piece[numLine, numCol];
            Lenght = [numLine, numCol];

            Game = game;
        }

        public Piece? GetPieceByPosition(Position position)
        {
            if (position.Column < 0 || position.Column >= Lenght[1]) return null;
            if (position.Line < 0 || position.Line >= Lenght[1]) return null;

            return Pieces[position.Line, position.Column];
        }

        public void MovePiece(Piece piece, Position pos)
        {
            if (!piece.IsPossibleToMove(pos)) return;
            if (!IsValidPosition(pos)) return;

            Piece? oponent = GetPieceByPosition(pos);
            if (oponent != null) EatPiece(piece, oponent);

            Pieces[piece.Position.Line,piece.Position.Column] = null;
            Pieces[pos.Line,pos.Column] = piece;
            piece.Position.ChangePosition(pos.Line, pos.Column);
        }

        private void EatPiece(Piece piece, Piece oponent)
        {
            if (piece.IsWhite)
            {
                Game.PlayerWhite.AmountPiecesYouCatch += 1;
                Game.PlayerWhite.PiecesYouCatch.Add(oponent);

                Game.PlayerBlack.AmountPieces -= 1;
            }
            else
            {
                Game.PlayerBlack.AmountPiecesYouCatch += 1;
                Game.PlayerBlack.PiecesYouCatch.Add(oponent);

                Game.PlayerWhite.AmountPieces -= 1;
            }
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
    }
}
