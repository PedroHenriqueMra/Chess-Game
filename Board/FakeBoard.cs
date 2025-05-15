using System.Threading.Tasks.Dataflow;
using ChessGame.Piece.PieceModel;
using ChessGame.Table;

public class FakeBoard : IDisposable
{
    private Board Board { get; set; }
    private Piece?[,] RealPieces { get; set; }
    public FakeBoard(Board board)
    {
        this.Board = board;
        this.RealPieces = new Piece[8, 8];

        for (int c = 0; c < 8; c++)
        {
            for (int l = 0; l < 8; l++)
            {
                var piece = board.Pieces[c, l];
                RealPieces[c, l] = piece != null ? piece.Clone() : null;
            }
        }
    }

    public void Dispose()
    {
       for (int c = 0; c < 8; c++)
        {
            for (int l = 0; l < 8; l++)
            {
                Board.Pieces[c, l] = RealPieces[c, l];
            }
        } 
    }
}
