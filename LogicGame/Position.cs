using System.Diagnostics.CodeAnalysis;
using ChessGame.Exceptions;

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
        return Column >= 0 && Column <= 7 && Line >= 0 && Line <= 7;
    }

    public bool IsInBoard(int column, int line)
    {
        return column >= 0 && column <= 7 && line >= 0 && line <= 7;
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