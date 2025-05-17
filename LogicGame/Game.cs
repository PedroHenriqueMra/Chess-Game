namespace ChessGame.Logic.Game
{
    using ChessGame.Piece.Entity;
    using ChessGame.Table;
    using ChessGame.Piece.PieceModel;
    using ChessGame.Logic.PositionGame;
    using ChessGame.Logic.Player.PlayerEntity;
    using ChessGame.Logic.Player.Color;
    using ChessGame.Logic.Service;
    using ChessGame.Exceptions;
    using System.Reflection.Metadata;
    using System.Data.Common;

    public class Game
    {
        // dependencies:
        public XequeService XequeService { get; set; }
        public GameUtils Utils { get; set; }

        public int Turns { get; set; }
        public bool GameOver { get; private set; } = default;
        public Player[] Players { get; set; } = new Player[2];
        public Player Winner { get; private set; }
        public Player CurrentPlayer { get; private set; }
        public Board Board { get; set; }
        public Pawn? PawnEnPassant { get; private set; }
        public int? TurnEnPassant { get; private set; }
        public Dictionary<Piece, bool[,]> AllPieceMovements { get; set; }
        public bool IsInXeque { get; set; } = default;

        public Game()
        {
            Board = new Board(8,8);
            Turns = 0;

            Player playerWhite = new Player(PlayerColor.White);
            Player playerBlack = new Player(PlayerColor.Black);

            Players[0] = playerWhite;
            Players[1] = playerBlack;
            CurrentPlayer = playerWhite;

            PawnEnPassant = null;
            TurnEnPassant = null;

            AllPieceMovements = new Dictionary<Piece, bool[,]>();
        }

        public void GameIsOver(Player winner)
        {
            this.GameOver = true;
            this.Winner = winner;
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
                CurrentPlayer = Players.First(p => p.Color == PlayerColor.White);
                return;
            }

            CurrentPlayer = Players.First(p => p.Color == PlayerColor.Black);
        }

        public void SetAllPieceMoves()
        {
            // cleaning
            AllPieceMovements = new Dictionary<Piece, bool[,]>();

            for (int c = 0; c < 8; c++)
            {
                for (int l = 0; l < 8; l++)
                {
                    if (Board.Pieces[c, l] != null)
                    {
                        this.AllPieceMovements.Add(
                        Board.Pieces[c, l],
                        Board.Pieces[c, l].GetPositionsToMove()
                        );
                    }
                }
            }
        }

        public int MovePiece(Piece piece, Position target)
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

                return 0;
            }

            throw new ImpossibleToMoveException($"Isn't possible to move {piece.ToString()} to {target.ToString()}");
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
                    pos = piece.Color == PlayerColor.White
                            ? pos.ChangePosition(pos.Column += 1, pos.Line)
                            : pos.ChangePosition(pos.Column -= 1, pos.Line);

                    captured = Board.GetPieceByPosition(pos);
                    return true;
                }

                if (pieceAsPawn.LeftDiagonalSteps(piece.Position).Compare(target) && pieceAsPawn.CheckToLeft(piece.Position))
                {
                    // check if there's an enemmy pawn to left
                    Position pos = new Position(piece.Position.Column, piece.Position.Line);
                    pos = piece.Color == PlayerColor.White
                            ? pos.ChangePosition(pos.Column -= 1, pos.Line)
                            : pos.ChangePosition(pos.Column += 1, pos.Line);

                    captured = Board.GetPieceByPosition(pos);
                    return true;
                }
            }

            Piece? enemyPiece = Board.GetPieceByPosition(target);
            // comum catch
            if (enemyPiece != null && enemyPiece.Color != piece.Color)
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

            Player enemy = Players.First(p => p.Color != CurrentPlayer.Color);
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

        public void SetIsInXeque()
        {
            King? king = GetKing(CurrentPlayer);
            if (king == null)
                throw new KingWasCatchedException();

            this.IsInXeque = XequeService.IsInXeque(king);
        }

        public King? GetKing(Player player)
        {
            foreach (var dict in AllPieceMovements.Where(d => d.Key.Color == player.Color))
                if (dict.Key is King) return dict.Key as King;

            return null;
        }

        public Player GetEnemyPlayer()
        {
            return this.Players.First(p => p.Color != CurrentPlayer.Color);
        }

        public List<Piece> GetPieceOptions(Player player)
        {
            var dictPieces = AllPieceMovements.Where(p => p.Key.Color == player.Color);
            King? king = dictPieces.First(p => p.Key is King).Key as King;

            if (king == null)
                throw new KingWasCatchedException("The King was Catched! Game finished.");

            bool isInXeque = XequeService.IsInXeque(king);

            List<Piece> pieceOptions = new List<Piece>();
            foreach (var dict in dictPieces)
            {
                Piece piece = dict.Key;
                bool[,] movements = dict.Value;

                if (isInXeque)
                {
                    return XequeService.PiecesBreaksXeque(king);
                }
                else
                {
                    if (piece.HasMove(movements))
                    {
                        pieceOptions.Add(piece);
                    }
                }
            }

            return pieceOptions;
        }

        public bool[,] GetkingMovements(King king)
        {
            bool[,] steps = king.GetPositionsToMove();

            int[] colPositions = { 0, 1, 1, 1, 0, -1, -1, -1 };
            int[] linPositions = { -1, -1, 0, 1, 1, 1, 0, -1 };

            for (int lop = 0; lop < 8; lop++)
            {
                Position kingStep = new Position(king.Position.Column, king.Position.Line);
                kingStep.Column += colPositions[lop];
                kingStep.Line += linPositions[lop];

                if (!kingStep.IsInBoard()) continue;

                // If this position is already compromised
                if (!steps[kingStep.Column, kingStep.Line]) continue;

                // Check if next position is in xeque
                if (XequeService.IsInXequeSimulator(king, kingStep))
                {
                    steps[kingStep.Column, kingStep.Line] = false;
                }
            }

            return steps;
        }

        public void PutPiecesOnBoard()
        {
            //white pieces:
            Board.Pieces[0, 7] = new Rook(this, PlayerColor.White, new Position(0, 7));
            Board.Pieces[1, 7] = new Knight(this, PlayerColor.White, new Position(1, 7));
            Board.Pieces[2, 7] = new Bishop(this, PlayerColor.White, new Position(2, 0));
            Board.Pieces[3, 7] = new King(this, PlayerColor.White, new Position(3, 1));
            Board.Pieces[4, 7] = new Queen(this, PlayerColor.White, new Position(4, 7));
            Board.Pieces[5, 7] = new Bishop(this, PlayerColor.White, new Position(5, 7));
            Board.Pieces[6, 7] = new Knight(this, PlayerColor.White, new Position(6, 7));
            Board.Pieces[7, 7] = new Rook(this, PlayerColor.White, new Position(7, 7));
            Board.Pieces[0, 6] = new Pawn(this, PlayerColor.White, new Position(0, 6));
            Board.Pieces[1, 6] = new Pawn(this, PlayerColor.White, new Position(1, 6));
            Board.Pieces[2, 6] = new Pawn(this, PlayerColor.White, new Position(2, 6));
            Board.Pieces[3, 6] = new Pawn(this, PlayerColor.White, new Position(3, 6));
            Board.Pieces[4, 6] = new Pawn(this, PlayerColor.White, new Position(4, 6));
            Board.Pieces[5, 6] = new Pawn(this, PlayerColor.White, new Position(5, 6));
            Board.Pieces[6, 6] = new Pawn(this, PlayerColor.White, new Position(6, 6));
            Board.Pieces[7, 6] = new Pawn(this, PlayerColor.White, new Position(7, 6));
            //black pieces:
            Board.Pieces[0, 0] = new Rook(this, PlayerColor.Black, new Position(0, 0));
            Board.Pieces[1, 0] = new Knight(this, PlayerColor.Black, new Position(1, 0));
            Board.Pieces[2, 0] = new Bishop(this, PlayerColor.Black, new Position(2, 0));
            Board.Pieces[3, 0] = new King(this, PlayerColor.Black, new Position(3, 0));
            Board.Pieces[4, 0] = new Queen(this, PlayerColor.Black, new Position(4, 0));
            Board.Pieces[5, 0] = new Bishop(this, PlayerColor.Black, new Position(5, 0));
            Board.Pieces[6, 0] = new Knight(this, PlayerColor.Black, new Position(6, 0));
            Board.Pieces[7, 0] = new Rook(this, PlayerColor.Black, new Position(7, 0));
            Board.Pieces[0, 1] = new Pawn(this, PlayerColor.Black, new Position(0, 1));
            Board.Pieces[1, 1] = new Pawn(this, PlayerColor.Black, new Position(1, 1));
            Board.Pieces[2, 1] = new Pawn(this, PlayerColor.Black, new Position(2, 1));
            Board.Pieces[3, 1] = new Pawn(this, PlayerColor.Black, new Position(3, 1));
            Board.Pieces[4, 1] = new Pawn(this, PlayerColor.Black, new Position(4, 1));
            Board.Pieces[5, 1] = new Pawn(this, PlayerColor.Black, new Position(5, 1));
            Board.Pieces[7, 1] = new Pawn(this, PlayerColor.Black, new Position(6, 1));
            Board.Pieces[6, 1] = new Pawn(this, PlayerColor.Black, new Position(7, 1));
        }
    }
}
