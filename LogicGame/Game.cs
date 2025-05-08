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
    using System.Security.Cryptography;

    public class Game
    {
        public int Turns { get; set; }
        public bool GameOver { get; private set; } = default;
        public Player[] Players { get; set; } = new Player[2];
        public Player CurrentPlayer { get; private set; }
        public Board Board { get; set; }
        public Pawn? PawnEnPassant { get; private set; }
        public int? TurnEnPassant { get; private set; }

        public Game()
        {
            Board = new Board(8, 8);
            Turns = 0;

            Player playerWhite = new Player(true);
            Player playerBlack = new Player(false);

            Players[0] = playerWhite;
            Players[1] = playerBlack;
            CurrentPlayer = playerWhite;

            PawnEnPassant = null;
            TurnEnPassant = null;
        }

        public void ChangeTurn()
        {
            this.Turns += 1;

            this.ChangeCurrentPlayer();

            // clear en passant
            if (TurnEnPassant != null && Turns != TurnEnPassant)
            {
                ClearEnPassant();
            }
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
                if (IsPossibleToCatch(piece, target, out Piece? captured))
                {
                    CatchPiece(captured);
                }

                if (piece is Pawn)
                {
                    // checks if the pawn has made two moves (this is an En Passant case)
                    Pawn pieceAsPawn = piece as Pawn;
                    if (target.Compare(pieceAsPawn.TwoStepsAhead(pieceAsPawn.Position)))
                    {
                        EnPassantMoviment(pieceAsPawn);
                    }
                }

                Board.MovePieceOnBoard(piece, target);
                piece.IncreaseMoviment();
                piece.ChangePosition(target);
            }

            // throw exception (isn't possible to move)
        }

        private bool IsPossibleToCatch(Piece piece, Position target, out Piece? captured)
        {
            captured = null;

            // catch with en passant
            if (piece is Pawn)
            {
                Pawn pieceAsPawn = piece as Pawn;
                // logic to capture en passant
                if (pieceAsPawn.RigthDiagonalSteps(piece.Position).Compare(target) && pieceAsPawn.CheckToRight(piece.Position))
                {
                    // check if there's an enemmy pawn to right
                    Position pos = new Position(piece.Position.Column,piece.Position.Line);
                    pos = piece.IsWhite
                            ? pos.ChangePosition(pos.Column += 1, pos.Line)
                            : pos.ChangePosition(pos.Column -= 1, pos.Line);
                    
                    captured = Board.GetPieceByPosition(pos);
                    return true;
                }

                if (pieceAsPawn.LeftDiagonalSteps(piece.Position).Compare(target) && pieceAsPawn.CheckToLeft(piece.Position))
                {
                    // check if there's an enemmy pawn to left
                    Position pos = new Position(piece.Position.Column,piece.Position.Line);
                    pos = piece.IsWhite
                            ? pos.ChangePosition(pos.Column -= 1, pos.Line)
                            : pos.ChangePosition(pos.Column += 1, pos.Line);
                    
                    captured = Board.GetPieceByPosition(pos);
                    return true;
                }
            }
            
            Piece? enemyPiece = Board.GetPieceByPosition(target);
            // comum catch
            if (enemyPiece != null && enemyPiece.IsWhite != piece.IsWhite) 
            {
                captured = enemyPiece;
                return true;
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
            if (PawnEnPassant != null && PawnEnPassant.Equals(piece))
            {
                return true;
            }

            return false;
        }

        private void EnPassantMoviment(Pawn pawn)
        {
            this.PawnEnPassant = pawn;
            this.TurnEnPassant = Turns;
            this.TurnEnPassant++; // "TurnEnPassant++" cause the En Passant lasts for the next turn and not the current one
        }

        private void ClearEnPassant()
        {
            this.PawnEnPassant = null;
            this.TurnEnPassant = null;
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
