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

            AllPieceMovements = new Dictionary<Piece,bool[,]>();
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

        public bool IsInXeque(King king)
        {
            foreach (var dict in AllPieceMovements)
            {
                if (dict.Key.IsWhite != king.IsWhite && dict.Value[king.Position.Column,king.Position.Line])
                {
                    return true;
                }
            }

            return false;
        }        

        public bool IsInXequeMate(King king)
        {
            if (!IsInXeque(king)) return false;

            // Verify if king can run
            int[] colPositions = { 0,  1, 1, 1, 0, -1,-1, -1};
            int[] linPositions = {-1, -1, 0, 1, 1,  1, 0, -1};

            bool[,] kingMoves = this.GetkingMovements(king);

            for (int lop = 0;lop < 8;lop++)
            {
                Position kingStep = new Position(king.Position.Column,king.Position.Line);
                kingStep.Column += colPositions[lop];
                kingStep.Line += linPositions[lop];

                if (!kingStep.IsInBoard()) continue;

                if (kingMoves[kingStep.Column,kingStep.Line])
                {
                    return false;
                }
            }

            // Enemy pieces that check
            bool canEscape = true;
            int defenses = 0; 
            foreach (var enemy in AllPieceMovements.Where(p => p.Key.IsWhite != king.IsWhite))
            {
                if (!enemy.Value[king.Position.Column,king.Position.Line])
                    continue;

                if (defenses > 0) // If will be necessary more than one defenses, it's a XequeMate
                    return true;

                bool[,] attack = enemy.Value;
                Piece enemyPiece = enemy.Key;

                if (!attack[king.Position.Column, king.Position.Line]) continue;

                // Try to capture or intercept
                canEscape = false;
                foreach (var ally in AllPieceMovements.Where(p => p.Key.IsWhite == king.IsWhite))
                {
                    if (ally.Key is King) continue;
                    bool[,] allyMoves = ally.Value;

                    if (allyMoves[enemyPiece.Position.Column, enemyPiece.Position.Line])
                    {
                        canEscape = true; // Can capture
                        defenses++;
                    }

                    if (enemyPiece is not Knight)
                    {
                        bool[,] path = GetPathBetween(king.Position, enemyPiece.Position);
                        for (int c = 0; c < 8; c++)
                        {
                            for (int l = 0; l < 8; l++)
                            {
                                if (path[c, l] && allyMoves[c, l])
                                {
                                    canEscape = true; // Can intercept
                                    defenses++;
                                }
                            }
                        }
                    }
                }
            }

            return !canEscape;
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
                if (IsInXequeSimulator(king, kingStep))
                {
                    steps[kingStep.Column,kingStep.Line] = false;
                }
            }

            return steps;
        }

        //public Position? InterceptPiece()
        //{

        //}

        private bool IsInXequeSimulator(King realKing, Position fakePos)
        {
            if (!fakePos.IsInBoard())
                return true;

            Dictionary<Piece, bool[,]> dictBackUp = this.AllPieceMovements;
            King fakeKing = new King(this, realKing.IsWhite, fakePos);

            using (Board.FakeBoardEnviroument())
            {
                Board.RemovePiece(realKing);
                Board.PutPiece(fakeKing, fakeKing.Position);

                SetAllPieceMoves();
                bool isInXeque = IsInXeque(fakeKing);
                this.AllPieceMovements = dictBackUp;
                if (isInXeque)
                    return true;
            }

            return false;
        }

        private bool[,] GetPathBetween(Position refPiece, Position enemyPiece) // king first!!
        {
            bool[,] path = new bool[8, 8];
            if (!refPiece.IsInBoard() || !enemyPiece.IsInBoard()) return path; // throw an exeption

            // straight path
            if (refPiece.Column == enemyPiece.Column)
            {
                int min = Math.Min(refPiece.Line, enemyPiece.Line);
                int max = Math.Max(refPiece.Line, enemyPiece.Line);

                for (int l = min; l <= max; l++)
                {
                    if (refPiece.Compare(new Position(refPiece.Column, l)))
                    {
                        continue; // if coordinates are of king (refPiece)
                    }

                    path[refPiece.Column, l] = true;
                }

                return path;
            }
            if (refPiece.Line == enemyPiece.Line)
            {
                int min = Math.Min(refPiece.Line, enemyPiece.Line);
                int max = Math.Max(refPiece.Line, enemyPiece.Line);

                for (int c = min; c <= max; c++)
                {
                    if (refPiece.Compare(new Position(c, refPiece.Line)))
                    {
                        continue; // if coordinates are of king (refPiece)
                    }

                    path[c, refPiece.Line] = true;
                }

                return path;
            }

            // diagonal path
            // get piece above
            Position pAbove = new Position(refPiece.Line < enemyPiece.Line ? refPiece.Column : enemyPiece.Column,
                                           refPiece.Line < enemyPiece.Line ? refPiece.Line : enemyPiece.Line);
            // get piece below
            Position pBelow = new Position(refPiece.Line > enemyPiece.Line ? refPiece.Column : enemyPiece.Column,
                                           refPiece.Line > enemyPiece.Line ? refPiece.Line : enemyPiece.Line);

            while (true)
            {
                if (!pAbove.IsInBoard())
                {
                    path = new bool[8, 8];
                    break;
                }
                if (!refPiece.Compare(pAbove)) path[pAbove.Column, pAbove.Line] = true;

                if (pBelow.Compare(pAbove)) break;

                // diagonal to right
                if (pAbove.Column < pBelow.Column)
                {
                    pAbove.Column++;
                    pAbove.Line++;
                }
                else // diagonal to left
                {
                    pAbove.Column--;
                    pAbove.Line++;
                }
            }

            return path;
        }
    }
}
