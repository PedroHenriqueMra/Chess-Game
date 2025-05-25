namespace ChessGame.Oponent
{
    using ChessGame.Logic.Game;
    using ChessGame.Logic.Player.Color;
    using ChessGame.Logic.PositionGame;
    using ChessGame.Piece.Entity;
    using ChessGame.Piece.PieceModel;

    public class Oponent
    {
        public Piece RandomPiece(List<Piece> pieces)
        {
            Random rand = new Random();
            int pieceRand = rand.Next(pieces.Count);

            return pieces[pieceRand];
        }

        public Position RandomPieceStep(bool[,] pieceSteps)
        {
            List<Tuple<int, int>> possibleSteps = new List<Tuple<int, int>>();

            for (int c = 0; c < 8; c++)
            {
                for (int l = 0; l < 8; l++)
                {
                    if (pieceSteps[c, l])
                    {
                        possibleSteps.Add(new Tuple<int, int>(c, l));
                    }
                }
            }

            Random rand = new Random();
            int numRand = rand.Next(possibleSteps.Count);

            Tuple<int, int> step = possibleSteps[numRand];
            return new Position(step.Item1, step.Item2);
        }
        
        public Piece RandomPromotion(Game game, PlayerColor color, Position position)
        {
            List<Piece> piecesPromotion = new List<Piece>()
            {
                new Queen(game, color, position),
                new Rook(game, color, position),
                new Knight(game, color, position),
                new Bishop(game, color, position)
            };

            Random rand = new Random();
            int indexPromotion = rand.Next(piecesPromotion.Count);

            return piecesPromotion[indexPromotion];
        }
    }

}
