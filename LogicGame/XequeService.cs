namespace ChessGame.Logic.Service
{
    using ChessGame.Logic.Game;
    using ChessGame.Logic.PositionGame;
    using ChessGame.Piece.Entity;
    using ChessGame.Piece.PieceModel;
    using ChessGame.Table;
    using ChessGame.Exceptions;

    public class XequeService
    {
        private Game Game { get; set; }
        private Board Board { get; set; }
        public XequeService(Game game, Board board)
        {
            this.Game = game;
            this.Board = board;
        }

        public bool IsInXequeMate(King king)
        {
            int howManyXeques = HowManyXeques(king);
            if (howManyXeques > 1) // there is too xeques
                return true;
            if (howManyXeques == 0) // there is not xeques
                return false;

            // Verify if king can run
            if (KingCanRun(king))
                return false;

            // Get pieces break xeque mate
            List<Piece> pieces = PiecesBreaksXeque(king);
            if (pieces.Count == 0)
                return true;

            return false;
        }

        public bool KingCanRun(King king)
        {
            int[] colPositions = { 0, 1, 1, 1, 0, -1, -1, -1 };
            int[] linPositions = { -1, -1, 0, 1, 1, 1, 0, -1 };

            bool[,] kingMoves = Game.GetkingMovements(king);

            for (int lop = 0; lop < 8; lop++)
            {
                Position kingStep = new Position(king.Position.Column, king.Position.Line);
                kingStep.Column += colPositions[lop];
                kingStep.Line += linPositions[lop];

                if (!kingStep.IsInBoard()) continue;

                if (kingMoves[kingStep.Column, kingStep.Line])
                {
                    return true;
                }
            }

            return false;
        }

        public List<Piece> PiecesBreaksXeque(King king)
        {
            List<Piece> pieceList = new List<Piece>();

            foreach (var enemy in Game.AllPieceMovements.Where(p => p.Key.Color != king.Color))
            {
                if (!enemy.Value[king.Position.Column, king.Position.Line])
                    continue;

                bool[,] attack = enemy.Value;
                Piece enemyPiece = enemy.Key;

                // Try to capture or intercept
                foreach (var ally in Game.AllPieceMovements.Where(p => p.Key.Color == king.Color))
                {
                    if (ally.Key is King)
                    {
                        if (KingCanRun(king))
                            pieceList.Add(king);
                            
                        continue;
                    }

                    bool[,] allyMoves = ally.Value;
                    Piece allyPiece = ally.Key;

                    if (allyMoves[enemyPiece.Position.Column, enemyPiece.Position.Line])
                    {
                        pieceList.Add(allyPiece); // Can capture
                        break;
                    }

                    if (enemyPiece is not Knight)
                    {
                        bool[,] pathBetween = Game.Utils.GetPathBetween(king.Position, enemyPiece.Position);
                        for (int c = 0; c < 8; c++)
                        {
                            for (int l = 0; l < 8; l++)
                            {
                                if (pathBetween[c, l] && allyMoves[c, l])
                                {
                                    pieceList.Add(allyPiece); // Can intercept
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            return pieceList;
        }

        //public bool[,] PositionsToBreakXeque()
        //{
        //    
        //}

        public bool IsInXeque(King king)
        {
            foreach (var dict in Game.AllPieceMovements)
            {
                if (dict.Key.Color != king.Color && dict.Value[king.Position.Column, king.Position.Line])
                {
                    return true;
                }
            }

            return false;
        }

        public int HowManyXeques(King king)
        {
            int xeques = 0;
            foreach (var dict in Game.AllPieceMovements.Where(p => p.Key.Color != king.Color))
            {
                Position kingPos = king.Position;

                if (dict.Value[kingPos.Column, kingPos.Line])
                    xeques++;
            }

            return xeques;
        }

        public Piece? WhoCausesXeque(King king)
        {
            if (!IsInXeque(king)) return null;

            foreach (var dict in Game.AllPieceMovements.Where(p => p.Key.Color != king.Color))
            {
                Piece enemyPiece = dict.Key;
                bool[,] enemyMoves = dict.Value;

                if (enemyMoves[king.Position.Column, king.Position.Line])
                    return enemyPiece;
            }

            return null;
        }

        public bool IsInXequeSimulator(King realKing, Position fakePos)
        {
            if (!fakePos.IsInBoard())
                return true;

            Dictionary<Piece, bool[,]> dictBackUp = Game.AllPieceMovements;
            King fakeKing = new King(Game, realKing.Color, fakePos);

            using (Board.FakeBoardEnviroument())
            {
                Board.RemovePiece(realKing);
                Board.PutPiece(fakeKing, fakeKing.Position);

                Game.SetAllPieceMoves();
                bool isInXeque = IsInXeque(fakeKing);
                Game.AllPieceMovements = dictBackUp;

                if (isInXeque)
                    return true;
            }

            return false;
        }
    }
}
