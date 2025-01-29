namespace ChessGame.Logic.PositionGame;

public struct Position
{
    public int Line { get; set; }
    public int Column { get; set; }

    public Position(int line, int column)
    {
        this.Line = line;
        this.Column = column;
    }

    public Position ChangePosition(int line, int column)
    {
        Line = line;
        Column = column;
        return this;
    }

    public override string ToString()
    {
        return $"{this.Line} - {this.Column}";
    }
}