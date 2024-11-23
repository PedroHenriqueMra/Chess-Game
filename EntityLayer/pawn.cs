public class pawn : Piece
{
    public bool TwoSteps { get; set; } = true;

    public pawn(int line, int column)
    : base(line, column)
    {
    }

    
}
