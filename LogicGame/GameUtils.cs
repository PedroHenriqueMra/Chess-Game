namespace ChessGame.Logic.Service
{
    using ChessGame.Exceptions;
    using ChessGame.Logic.PositionGame;

    public class GameUtils
    {
        public bool[,] GetPathBetween(Position refPiece, Position enemyPiece) // king first!!
        {
            bool[,] path = new bool[8, 8];
            if (!refPiece.IsInBoard() || !enemyPiece.IsInBoard()) throw new OutOfTableException("Position fail!. The ref piece or enemy piece are out of board!.");

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
