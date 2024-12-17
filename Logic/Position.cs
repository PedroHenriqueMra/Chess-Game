namespace ChessGame.Logic.PositionGame;

public class Position
{
    public int Line { get; set; }
    public int Column { get; set; }

    public Position(int line, int column)
    {
        this.Line = line;
        this.Column = column;
    }

    public void ChangePosition(int line, int column)
    {
        Line = line;
        Column = column;
    }

    public override string ToString()
    {
        return $"{this.Line} - {this.Column}";
    }
}