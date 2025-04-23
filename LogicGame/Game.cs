namespace ChessGame.Game.main
{
    using ChessGame.Piece.Entity;
    using ChessGame.Table;
    using ChessGame.Piece.PieceModel;
    using ChessGame.Logic.PositionGame;
    using System.Security;
    using ChessGame.Logic.SystemPlayer;
    using System.IO.Pipes;

    public class Game
    {
        public int Turns { get; set; }
        public bool GameOver { get; set; } = default;
        public Player PlayerWhite { get; set; }
        public Player PlayerBlack { get; set; }
        public Board Board { get; set; }
        public HashSet<int> PawnInPassant { get; set; }

        public Game(Player playerWhite, Player playerBlack)
        {
            PlayerWhite = playerWhite;
            PlayerBlack = playerBlack;
            
            Board = new Board(8, 8);
        }

        public void ChangeTurn()
        {
            this.Turns += 1;
        }

        public void MovePiece(Piece piece, Position target)
        {
            if (piece.IsPossibleToMove(target))
            {
                if (IsPossibleToCatch(piece, target, out Piece? captured))
                {
                    CatchPiece(captured);
                }

                Board.MovePiece(piece, target);
                piece.IncreaseMovimente();
            }
        }

        private bool IsPossibleToCatch(Piece piece, Position target, out Piece? captured)
        {
            // comum catch
            Piece? enemyPiece = Board.GetPieceByPosition(target);
            if (enemyPiece != null)
            {
                captured = enemyPiece;
                return true;
            }

            // catch with en passant
            if (piece is Pawn)
            {
                // logic to capture en passant
            }

            captured = null;
            return false;
        }

        private void CatchPiece(Piece captured)
        {
            // logic to record catured pieces
            return;
        }

        // Is in passant
        public bool IsInPassant(Piece piece)
        {
            if (piece.GetHashCode().Equals(this.PawnInPassant))
            {
                return true;
            }

            return false;
        }

        public void InPassantMoviment(Piece pawn)
        {
            this.PawnInPassant.Clear();
            this.PawnInPassant.Add(pawn.GetHashCode());
        }

        public bool IsInCheckMate(Position kingCurPos, Position kingTarget, bool isWhite)
        {
            Piece? kingPiece = Board.GetPieceByPosition(kingCurPos);
            if (kingPiece == null)
            {
                return true;
            }

            // retire king piece of table:
            Board.Pieces[kingCurPos.Column, kingCurPos.Line] = null;

            for (int c = 0;c < 8;c++)
            {
                for (int l = 0;l < 8;l++)
                {
                    Piece? piece = Board.GetPieceByPosition(new Position(l, c));
                    if (piece != null && piece.GetType() != typeof(King))
                    {
                        if (piece.IsWhite != isWhite)
                        {
                            bool[,] pieceSteps = piece.GetPositionsToMove();
                            if (pieceSteps[kingTarget.Column, kingTarget.Line])
                            {
                                // return king piece to table:
                                Board.Pieces[kingCurPos.Column, kingCurPos.Line] = kingPiece;
                                return true;
                            }
                        }
                    }
                }
            }

            // return king piece to table:
            Board.Pieces[kingCurPos.Column, kingCurPos.Line] = kingPiece;
            return false;
        }
    }
}
