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

    public Piece(Board board, bool isWhite)
    {
        Board = board; // Dependence
        IsWhite = isWhite;
    }

    // Map -> if there's any step to do
    public bool IsPossibleToMove(Position pos)
    {
        bool[,] moves = GetPositionsToMove(IsWhite, Position);

        return moves[pos.Line, pos.Column];
    }

    // function to return every possible piece' steps
    public abstract bool[,] GetPositionsToMove(bool isWhite, Position pos);
}
