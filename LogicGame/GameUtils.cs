namespace ChessGame.Logic.Service
{
    using ChessGame.Logic.PositionGame;

    public class GameUtils
    {
        public bool[,] GetPathBetween(Position positionA, Position positionB) // king first!!
        {
            bool[,] path = new bool[8, 8];

            if (!positionA.IsInBoard() || !positionB.IsInBoard())
                return null;

            // straight path
                if (positionA.Column == positionB.Column)
                {
                    int min = Math.Min(positionA.Line, positionB.Line);
                    int max = Math.Max(positionA.Line, positionB.Line);

                    for (int l = min; l <= max; l++)
                    {
                        path[positionA.Column, l] = true;
                    }

                    return path;
                }
                else if (positionA.Line == positionB.Line)
                {
                    int min = Math.Min(positionA.Column, positionB.Column);
                    int max = Math.Max(positionA.Column, positionB.Column);

                    for (int c = min; c <= max; c++)
                    {
                        path[c, positionA.Line] = true;
                    }

                    return path;
                }

            // diagonal path
            if (!IsDiagonal(positionA, positionB))
                return path;

            // positionA
            int x1 = positionA.Column;
            int y1 = positionA.Line;
            // positionB
            int x2 = positionB.Column;
            int y2 = positionB.Line;

            // directions
            int dirx = x1 > x2 ? -1 : 1;
            int diry = y1 > y2 ? -1 : 1;

            // vars will be the pointer
            int x = x1 += dirx;
            int y = y1 += diry;

            while (x != x2 && y != y2)
            {
                if (x >= 0 && x < 8 && y >= 0 && y < 8)
                    path[x, y] = true;

                x += dirx;
                y += diry;
            }

            return path;
        }

        private bool IsDiagonal(Position origin, Position destination)
        {
            return Math.Abs(origin.Column - destination.Column) == Math.Abs(origin.Line - destination.Line);
        }
    }
}
