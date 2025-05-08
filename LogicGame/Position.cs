using System.Diagnostics.CodeAnalysis;

namespace ChessGame.Logic.PositionGame;

public struct Position
{
    public int Column { get; set; }
    public int Line { get; set; }

    public Position(int column, int line)
    {
        this.Column = column;
        this.Line = line;
    }

    public Position ChangePosition(int column, int line)
    {
        Line = line;
        Column = column;
        return this;
    }

    public bool IsInBoard()
    {
        return Column >= 0 && Column <= 8 && Line >= 0 && Line <= 8;
    }


    public bool Compare(Position position)
    {
        return this.Column == position.Column && this.Line == position
        .Line;
    }

    public override string ToString()
    {
        return $"C:{this.Column} - L:{this.Line}";
    }
}