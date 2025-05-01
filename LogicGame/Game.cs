namespace ChessGame.Game.main
{
    using ChessGame.Piece.Entity;
    using ChessGame.Table;
    using ChessGame.Piece.PieceModel;
    using ChessGame.Logic.PositionGame;
    using System.Security;
    using ChessGame.Logic.SystemPlayer;
    using System.IO.Pipes;
    using System.Runtime.InteropServices;

    public class Game
    {
        public int Turns { get; set; }
        public bool GameOver { get; set; } = default;
        public Player[] Players { get; set; } = new Player[2];
        public Player CurrentPlayer { get; set; }
        public Board Board { get; set; }
        public HashSet<int> PawnInPassant { get; set; }

        public Game()
        {
            Board = new Board(8, 8);

            Player playerWhite = new Player(true);
            Player playerBlack = new Player(false);

            Players[0] = playerWhite;
            Players[1] = playerBlack;
            this.CurrentPlayer = playerWhite;
        }

        public void ChangeTurn()
        {
            this.Turns += 1;

            this.ChangeCurrentPlayer();
        }

        private void ChangeCurrentPlayer()
        {
            if (Turns % 2 == 0)
            {
                CurrentPlayer = Players.First(p => p.IsWhite);
                return;
            }
            
            CurrentPlayer = Players.First(p => !p.IsWhite);
        }

        public void MovePiece(Piece piece, Position target)
        {
            if (piece.IsPossibleToMove(target))
            {
                if (IsPossibleToCatch(piece, target))
                {
                    Piece captured = Board.GetPieceByPosition(target);
                    CatchPiece(captured);
                }

                Board.MovePiece(piece, target);
                piece.IncreaseMoviment();
            }
        }

        private bool IsPossibleToCatch(Piece piece, Position target)
        {
            // comum catch
            Piece? enemyPiece = Board.GetPieceByPosition(target);
            if (enemyPiece != null)
            {
                return true;
            }

            // catch with en passant
            if (piece is Pawn)
            {
                // logic to capture en passant
            }

            return false;
        }

        private void CatchPiece(Piece captured)
        {
            Board.RemovePiece(captured);

            CurrentPlayer.PiecesYouCatch.Append(captured);
            CurrentPlayer.AmountPiecesYouCatch++;

            Player enemy = Players.First(p => p.IsWhite != CurrentPlayer.IsWhite);
            enemy.AmountPieces--;
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
