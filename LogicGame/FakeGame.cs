using ChessGame.Logic.Game;
using ChessGame.Piece.PieceModel;
using ChessGame.Table;

public class FakeGame : IDisposable
{
    public Game Game { get; set; }
    public Dictionary<Piece, bool[,]> RealAllPieceMovements { get; set; }
    public Board Board { get; set; }
    public Piece[,] RealPieces { get; set; }

    public FakeGame(Game game)
    {
        this.Game = game;
        this.Board = game.Board;
        this.RealAllPieceMovements = new Dictionary<Piece, bool[,]>();
        this.RealPieces = new Piece[Game.Board.NumColumn, Game.Board.NumLine];

        // Deep piece movements copy
        foreach (var dict in this.Game.AllPieceMovements)
        {
            bool[,] cloneMatrix = new bool[8, 8];
            for (int c = 0; c < 8; c++)
                for (int l = 0; l < 8; l++)
                    cloneMatrix[c, l] = dict.Value[c, l];

            this.RealAllPieceMovements[dict.Key] = cloneMatrix;
        }

        // Deep piece position copy
        for (int c = 0; c < 8; c++)
        {
            for (int l = 0; l < 8; l++)
            {
                var piece = Board.Pieces[c, l];
                RealPieces[c, l] = piece != null ? piece.Clone() : null;
            }
        }
    }

    public void Dispose()
    {
        // Backup piece movements
        this.Game.AllPieceMovements = this.RealAllPieceMovements;
        
        // Backup piece positions
        for (int c = 0; c < 8; c++)
        {
            for (int l = 0; l < 8; l++)
            {
                Board.Pieces[c, l] = RealPieces[c, l];
            }
        } 
    }
}