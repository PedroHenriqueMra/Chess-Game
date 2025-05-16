namespace ChessGame.Logic.Service
{
    using ChessGame.Logic.Game;
    using ChessGame.Logic.PositionGame;
    using ChessGame.Piece.Entity;
    using ChessGame.Piece.PieceModel;
    using ChessGame.Table;

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
            if (!IsInXeque(king)) return false;

            // Verify if king can run
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
                    return false;
                }
            }

            // Enemy pieces that check
            bool canEscape = true;
            int defenses = 0;
            foreach (var enemy in Game.AllPieceMovements.Where(p => p.Key.IsWhite != king.IsWhite))
            {
                if (!enemy.Value[king.Position.Column, king.Position.Line])
                    continue;

                if (defenses > 0) // If will be necessary more than one defenses, it's a XequeMate
                    return true;

                bool[,] attack = enemy.Value;
                Piece enemyPiece = enemy.Key;

                if (!attack[king.Position.Column, king.Position.Line]) continue;

                // Try to capture or intercept
                canEscape = false;
                foreach (var ally in Game.AllPieceMovements.Where(p => p.Key.IsWhite == king.IsWhite))
                {
                    if (ally.Key is King) continue;
                    bool[,] allyMoves = ally.Value;

                    if (allyMoves[enemyPiece.Position.Column, enemyPiece.Position.Line])
                    {
                        canEscape = true; // Can capture
                        defenses++;
                        break;
                    }

                    if (enemyPiece is not Knight)
                    {
                        bool[,] path = Game.Utils.GetPathBetween(king.Position, enemyPiece.Position);
                        for (int c = 0; c < 8; c++)
                        {
                            for (int l = 0; l < 8; l++)
                            {
                                if (path[c, l] && allyMoves[c, l])
                                {
                                    canEscape = true; // Can intercept
                                    defenses++;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            return !canEscape;
        }

        public bool IsInXeque(King king)
        {
            foreach (var dict in Game.AllPieceMovements)
            {
                if (dict.Key.IsWhite != king.IsWhite && dict.Value[king.Position.Column, king.Position.Line])
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsInXequeSimulator(King realKing, Position fakePos)
        {
            if (!fakePos.IsInBoard())
                return true;

            Dictionary<Piece, bool[,]> dictBackUp = Game.AllPieceMovements;
            King fakeKing = new King(Game, realKing.IsWhite, fakePos);

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
