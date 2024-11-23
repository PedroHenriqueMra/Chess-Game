public class Piece
{
    public int[] Position { get; set; } = new int[2];
    public bool Jump { get; set; } = default;
    public bool PositionToEat { get; set; } = default;

    public Piece(int line, int column)
    {
        Position[0] = line;
        Position[1] = column;
    }

    public void Move(int line, int column)
    {
        Position[0] = line;
        Position[1] = column;
    }

    
}
