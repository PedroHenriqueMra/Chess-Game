using System.Runtime.CompilerServices;
using ChessGame.Table;
using ChessGame.Logic.PositionGame;
using ChessGame.Piece.Entity;
using ChessGame.Piece.PieceModel;
using ChessGame.Game.main;
using ChessGame.Logic.SystemPlayer;
using System.Runtime.InteropServices;
using System.ComponentModel;


// knight - ok
// queen - ok
// king - ok
// pawn - ok
// rook - ok
// bishop - ok


// To state instances
// Game:
Game game = new Game();

// white piece
Pawn pieceTest = new Pawn(game, true, new Position(3,5));

// enemy pieces
Pawn piece1 = new Pawn(game, false, new Position(4,5));
game.PawnEnPassant = piece1;
game.TurnEnPassant = 1;

game.Board.Pieces[pieceTest.Position.Column, pieceTest.Position.Line] = pieceTest;
game.Board.Pieces[piece1.Position.Column, piece1.Position.Line] = piece1;
//game.Board.Pieces[piece2.Position.Column, piece2.Position.Line] = piece2;

// moviment to catch
game.MovePiece(pieceTest, new Position(4,4));

// Draw tests
var steps = pieceTest.GetPositionsToMove();
for (int l = 0;l < 8;l++)
{
    for (int c = 0;c < 8;c++)
    {
        if (game.Board.Pieces[c,l] != null)
        {
            Console.Write($" {game.Board.Pieces[c,l].ToString()} ");
            continue;
        }
        
        if(steps[c,l])
        {
            var resetColor = Console.BackgroundColor;

            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.Write(" x ");
            Console.BackgroundColor = resetColor;
            continue;
        }

        Console.Write(" - ");
    }
    Console.WriteLine("");
}



Console.WriteLine($"Player-White: {game.Players.First(p => p.IsWhite).AmountPieces}");
Console.WriteLine($"Player-Black: {game.Players.First(p => !p.IsWhite).AmountPieces}");

Console.WriteLine($"Movements white pawn: {pieceTest.Movements}");
Console.WriteLine($"Movements black pawn: {piece1.Movements}");



// Puting pieces:
// White player
/* board.Pieces[0,0] = new Rook(board, true);
board.Pieces[0,1] = new Knight(board, true);
board.Pieces[0,2] = new Bishop(board, true);
board.Pieces[0,3] = new King(board, true);
board.Pieces[0,4] = new Queen(board, true);
board.Pieces[0,5] = new Bishop(board, true);
board.Pieces[0,6] = new Knight(board, true);
board.Pieces[0,7] = new Rook(board, true);
board.Pieces[1,0] = new Pawn(board, true);
board.Pieces[1,1] = new Pawn(board, true);
board.Pieces[1,2] = new Pawn(board, true);
board.Pieces[1,3] = new Pawn(board, true);
board.Pieces[1,4] = new Pawn(board, true);
board.Pieces[1,5] = new Pawn(board, true);
board.Pieces[1,6] = new Pawn(board, true);
board.Pieces[1,7] = new Pawn(board, true);

// Black player
board.Pieces[7,0] = new Rook(board, false);
board.Pieces[7,1] = new Knight(board, false);
board.Pieces[7,2] = new Bishop(board, false);
board.Pieces[7,3] = new King(board, false);
board.Pieces[7,4] = new Queen(board, false);
board.Pieces[7,5] = new Bishop(board, false);
board.Pieces[7,6] = new Knight(board, false);
board.Pieces[7,7] = new Rook(board, false);
board.Pieces[6,0] = new Pawn(board, false);
board.Pieces[6,1] = new Pawn(board, false);
board.Pieces[6,2] = new Pawn(board, false);
board.Pieces[6,3] = new Pawn(board, false);
board.Pieces[6,4] = new Pawn(board, false);
board.Pieces[6,5] = new Pawn(board, false);
board.Pieces[6,6] = new Pawn(board, false);
board.Pieces[6,7] = new Pawn(board, false); */

//while (!game.GameOver)
//{
    
//}
