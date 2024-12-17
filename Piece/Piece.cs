using ChessGame.Logic.PositionGame;
using ChessGame.Table;

namespace ChessGame.Piece.PieceModel;

public abstract class Piece
{
    public Position Position { get; set; }
    public bool IsPossibleToMove { get; set; }
    public bool IsYour { get; set; }
    private readonly Board _board;

    public Piece(Board board, bool isYour)
    {
        _board = board; // dependence
        IsYour = isYour;
    }

    // To map every possible steps
    public virtual void PossibleMoves(out int[][] possibleStaps)
    {
        var possibleStapsList = new List<int[]>();

        this.IsPossibleToMove = false; // State reset
        int[][] moves = ReturnPieceSteps();

        // pos[0] = line, pos[1] = column
        foreach (var pos in moves)
        {
            var piece = _board.GetPieceByPosition(new Position(pos[0], pos[1]));
            if (piece != null && !piece.IsYour)
                possibleStapsList.Append([pos[0], pos[1]]);
        }

        if (possibleStapsList.Count > 0) this.IsPossibleToMove = true;

        possibleStaps = possibleStapsList.ToArray();
    }

    // function to return every possible piece' steps
    public abstract int[][] ReturnPieceSteps();
}
