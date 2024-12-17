using System.Runtime.CompilerServices;
using ChessGame.Table;
using ChessGame.Logic.PositionGame;
using ChessGame.Piece.Entity;
using ChessGame.Piece.PieceModel;

var board = new Board(8, 8);

Pawn pawn1 = new Pawn(board, true);
pawn1.Position = new Position(1, 0);

Pawn pawn2 = new Pawn(board, true);
pawn2.Position = new Position(1, 1);


board.Pieces[pawn1.Position.Line, pawn1.Position.Column] = pawn1;
board.Pieces[pawn2.Position.Line, pawn2.Position.Column] = pawn2;
foreach (var p in board.Pieces)
{
    if (p != null)
    {
        Console.WriteLine($"{p.ToString()} - [{p.Position.Line}, {p.Position.Column}]");
    }
}
