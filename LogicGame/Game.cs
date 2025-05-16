namespace ChessGame.Logic.Game
{
    using ChessGame.Piece.Entity;
    using ChessGame.Table;
    using ChessGame.Piece.PieceModel;
    using ChessGame.Logic.PositionGame;
    using ChessGame.Logic.SystemPlayer;
    using ChessGame.Logic.Service;
    using ChessGame.Exceptions;

    public class Game
    {
        // dependencies:
        public XequeService XequeService { get; set; }
        public GameUtils Utils { get; set; }
        
        public int Turns
        { get; set; }
        public bool GameOver { get; private set; } = default;
        public Player[] Players { get; set; } = new Player[2];
        public Player CurrentPlayer { get; private set; }
        public Board Board { get; set; }
        public Pawn? PawnEnPassant { get; private set; }
        public int? TurnEnPassant { get; private set; }
        public Dictionary<Piece, bool[,]> AllPieceMovements { get; set; }

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

            AllPieceMovements = new Dictionary<Piece, bool[,]>();
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

        public void SetAllPieceMoves()
        {
            // cleaning
            AllPieceMovements = new Dictionary<Piece,bool[,]>();

            for (int c = 0;c < 8;c++)
            {
                for (int l = 0;l < 8;l++)
                {
                    if (Board.Pieces[c,l] != null)
                    {
                        this.AllPieceMovements.Add(
                        Board.Pieces[c,l],
                        Board.Pieces[c,l].GetPositionsToMove()
                        );
                    }
                    
                }
            }
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

            throw new ImpossibleToMoveException($"Isn't possible to movove {piece.ToString()} to {target.ToString()}"); 
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
                    Position pos = new Position(piece.Position.Column, piece.Position.Line);
                    pos = piece.IsWhite
                            ? pos.ChangePosition(pos.Column += 1, pos.Line)
                            : pos.ChangePosition(pos.Column -= 1, pos.Line);

                    captured = Board.GetPieceByPosition(pos);
                    return true;
                }

                if (pieceAsPawn.LeftDiagonalSteps(piece.Position).Compare(target) && pieceAsPawn.CheckToLeft(piece.Position))
                {
                    // check if there's an enemmy pawn to left
                    Position pos = new Position(piece.Position.Column, piece.Position.Line);
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
            this.TurnEnPassant++; // "TurnEnPassant++" cause the En Passant is valid for the next turn and not the current one
        }

        private void ClearEnPassant()
        {
            this.PawnEnPassant = null;
            this.TurnEnPassant = null;
        } 

        public bool[,] GetkingMovements(King king)
        {
            bool[,] steps = king.GetPositionsToMove();
            
            int[] colPositions = { 0,  1, 1, 1, 0, -1,-1, -1};
            int[] linPositions = {-1, -1, 0, 1, 1,  1, 0, -1};

            for (int lop = 0;lop < 8;lop++)
            {
                Position kingStep = new Position(king.Position.Column,king.Position.Line);
                kingStep.Column += colPositions[lop];
                kingStep.Line += linPositions[lop];
                
                if (!kingStep.IsInBoard()) continue;

                // If this position is already compromised
                if (!steps[kingStep.Column,kingStep.Line]) continue;
                
                // Check if next position is in xeque
                if (XequeService.IsInXequeSimulator(king, kingStep))
                {
                    steps[kingStep.Column,kingStep.Line] = false;
                }
            }

            return steps;
        }
    }
}
