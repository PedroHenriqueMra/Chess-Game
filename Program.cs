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
Piece pieceTest = new Bishop(game, true, new Position(4,3));

// enemy pieces
Piece piece1 = new Bishop(game, false, new Position(2,5));
Piece piece2 = new Bishop(game, false, new Position(4,6));

game.Board.Pieces[pieceTest.Position.Column, pieceTest.Position.Line] = pieceTest;
game.Board.Pieces[piece1.Position.Column, piece1.Position.Line] = piece1;
game.Board.Pieces[piece2.Position.Column, piece2.Position.Line] = piece2;

// moviment to catch
game.MovePiece(pieceTest, new Position(5,2));

// Draw tests
var steps = pieceTest.GetPositionsToMove();
for (int c = 0;c < 8;c++)
{
    for (int l = 0;l < 8;l++)
    {
        if (!steps[c,l])
        {
            if (game.Board.Pieces[c,l] != null)
            {
                Console.Write($" {game.Board.Pieces[c,l].ToString()} ");
            }
            else
            {
                Console.Write(" - ");
            }
            
        }
        else
        {
            Console.Write(" 0 ");
        }
    }
    Console.WriteLine("");
}

Console.WriteLine($"Player-White: {game.Players.First(p => p.IsWhite).AmountPieces}");
Console.WriteLine($"Player-Black: {game.Players.First(p => !p.IsWhite).AmountPieces}");



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
