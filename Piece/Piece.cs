using ChessGame.Logic.PositionGame;
using ChessGame.Table;

namespace ChessGame.Piece.PieceModel;

public abstract class Piece
{
    public Position Position { get; set; }
    public bool IsWhite { get; set; }
    public bool Jump { get; set; } = default;
    public Board Board { get; protected set; }
    public int Moves { get; set; }

    public Piece(Board board, bool isWhite, Position position)
    {
        Position = position;
        Board = board;
        IsWhite = isWhite;
    }

    public bool IsPossibleToMove(Position pos)
    {
        bool[,] moves = GetPositionsToMove();

        return moves[pos.Line, pos.Column];
    }

    public abstract bool[,] GetPositionsToMove();
}
