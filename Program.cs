using System.Runtime.CompilerServices;
using ChessGame.Table;
using ChessGame.Logic.PositionGame;
using ChessGame.Piece.Entity;
using ChessGame.Piece.PieceModel;

var board = new Board(8, 8);

// get piece
Pawn pawn1 = new Pawn(board, true);
pawn1.Position = new Position(6, 0);
Pawn pawn2 = new Pawn(board, false);
pawn2.Position = new Position(5, 1);


board.Pieces[pawn1.Position.Line, pawn1.Position.Column] = pawn1;
board.Pieces[pawn2.Position.Line, pawn2.Position.Column] = pawn2;

// Getpositions test:
bool[,] possiblePosition = pawn1.GetPositions(true, pawn1.Position);

// show possible positions:
Console.WriteLine("\nPositions to move with pawn1:");
for (int l = 0;l < 8;l++)
{
    for (int c = 0;c < 8;c++)
    {
        if (possiblePosition[l,c])
        {
            Console.BackgroundColor = ConsoleColor.White; 
        }
        Console.Write($" [-] ");
        Console.BackgroundColor = ConsoleColor.Black;
    }
    Console.WriteLine();
}

// board ilustartion
Console.WriteLine("\nBoard:");
for (int l = 0;l < 8;l++)
{
    for (int c = 0;c < 8;c++)
    {
        Console.Write($" [{board.Pieces[l,c]}] ");
    }
    Console.WriteLine();
}
