using System.Runtime.CompilerServices;
using ChessGame.Table;
using ChessGame.Logic.PositionGame;
using ChessGame.Piece.Entity;
using ChessGame.Piece.PieceModel;
using ChessGame.Game.main;
using ChessGame.Logic.SystemPlayer;
using System.Runtime.InteropServices;
using System.ComponentModel;

// To state instances
// Game:
Game game = new Game();

//white pieces:
//game.Board.Pieces[0,7] = new Rook(game, true, new Position(0,7));
//game.Board.Pieces[1,7] = new Knight(game, true, new Position(1,7));
//game.Board.Pieces[5,0] = new Bishop(game, true, new Position(5,0));
//game.Board.Pieces[3,1] = new King(game, true, new Position(3,1));
//game.Board.Pieces[4,7] = new Queen(game, true, new Position(4,7));
//game.Board.Pieces[5,7] = new Bishop(game, true, new Position(5,7));
//game.Board.Pieces[6,7] = new Knight(game, true, new Position(6,7));
//game.Board.Pieces[7,7] = new Rook(game, true, new Position(7,7));
//game.Board.Pieces[0,6] = new Pawn(game, true, new Position(0,6));
//game.Board.Pieces[1,6] = new Pawn(game, true, new Position(1,6));
//game.Board.Pieces[2,6] = new Pawn(game, true, new Position(2,6));
//game.Board.Pieces[3,6] = new Pawn(game, true, new Position(3,6));
//game.Board.Pieces[4,6] = new Pawn(game, true, new Position(4,6));
//game.Board.Pieces[5,6] = new Pawn(game, true, new Position(5,6));
//game.Board.Pieces[6,6] = new Pawn(game, true, new Position(6,6));
//game.Board.Pieces[7,6] = new Pawn(game, true, new Position(6,7));
//black pieces:
//game.Board.Pieces[0,0] = new Rook(game, false, new Position(0,0));
//game.Board.Pieces[1,0] = new Knight(game, false, new Position(1,0));
//game.Board.Pieces[3,7] = new Bishop(game, false, new Position(3,7));
//game.Board.Pieces[3,0] = new King(game, false, new Position(3,0));
//game.Board.Pieces[4,0] = new Queen(game, false, new Position(4,0));
//game.Board.Pieces[7,5] = new Bishop(game, false, new Position(7,5));
//game.Board.Pieces[6,0] = new Knight(game, false, new Position(6,0));
//game.Board.Pieces[7,0] = new Rook(game, false, new Position(7,0));
//game.Board.Pieces[0,1] = new Pawn(game, false, new Position(0,1));
//game.Board.Pieces[1,1] = new Pawn(game, false, new Position(1,1));
//game.Board.Pieces[2,1] = new Pawn(game, false, new Position(2,1));
//game.Board.Pieces[3,1] = new Pawn(game, false, new Position(3,1));
//game.Board.Pieces[4,1] = new Pawn(game, false, new Position(4,1));
//game.Board.Pieces[5,1] = new Pawn(game, false, new Position(5,1));
//game.Board.Pieces[6,1] = new Pawn(game, false, new Position(6,1));
//game.Board.Pieces[7,1] = new Pawn(game, false, new Position(7,1));

// test piece MAIN:
Position pKing = new Position(1,0);
King king = new King(game, true, new Position(pKing.Column,pKing.Line));
game.Board.Pieces[pKing.Column,pKing.Line] = king;

Position pTest1 = new Position(0,3);
Piece pieceTest1 = new Bishop(game, true, new Position(pTest1.Column,pTest1.Line));
game.Board.Pieces[pTest1.Column,pTest1.Line] = pieceTest1;


// test piece enemy:
Position pTest2 = new Position(4,4);
Piece pieceTest2 = new Bishop(game, false, new Position(pTest2.Column,pTest2.Line));
game.Board.Pieces[pTest2.Column,pTest2.Line] = pieceTest2;

Position pTest3 = new Position(7,4);
Piece pieceTest3 = new Bishop(game, false, new Position(pTest3.Column,pTest3.Line));
game.Board.Pieces[pTest3.Column,pTest3.Line] = pieceTest3;

Position pTest4 = new Position(3,0);
Piece pieceTest4 = new Queen(game, false, new Position(pTest4.Column,pTest4.Line));
game.Board.Pieces[pTest4.Column,pTest4.Line] = pieceTest4;

Position pTest5 = new Position(1,7);
Piece pieceTest5 = new Queen(game, false, new Position(pTest5.Column,pTest5.Line));
game.Board.Pieces[pTest5.Column,pTest5.Line] = pieceTest5;

// getting piece movements
game.SetAllPieceMoves();

bool isInXequeMate = game.IsInXequeMate(king as King);
bool isInXeque = game.IsInXeque(king as King);
Console.WriteLine("");
Console.WriteLine($"King Is in xeque?: {isInXeque}");
Console.WriteLine($"King Is in xequeMate?: {isInXequeMate}");
Console.WriteLine("");

Piece monitoredPart = king;
//bool[,] steps = monitoredPart.GetPositionsToMove();
bool[,] steps = game.GetkingMovements(king);
for (int l = 0;l < 8;l++)
{
    for (int c = 0;c < 8;c++)
    {
        if (steps[c,l])
            Console.BackgroundColor = game.Board.Pieces[c,l] != null ? ConsoleColor.Red : ConsoleColor.Yellow;

        if (game.Board.Pieces[c,l] != null)
        {
            Console.Write($" {game.Board.Pieces[c,l]} ");
        }
        else
        {
            Console.Write(" - ");
        }

        Console.ResetColor();
    }

    Console.WriteLine();
}

game.ChangeTurn();

Console.WriteLine();
Console.WriteLine($"Current turn: {game.Turns}");
Console.WriteLine($"Player-White: {game.Players.First(p => p.IsWhite).AmountPieces}");
Console.WriteLine($"Player-Black: {game.Players.First(p => !p.IsWhite).AmountPieces}");
