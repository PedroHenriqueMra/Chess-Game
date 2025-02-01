using System.Runtime.CompilerServices;
using ChessGame.Table;
using ChessGame.Logic.PositionGame;
using ChessGame.Piece.Entity;
using ChessGame.Piece.PieceModel;
using ChessGame.Game.main;
using ChessGame.Logic.SystemPlayer;

// To state instances
// Game:
Player playerWhite = new Player(true);
Player playerBlack = new Player(false);
Game game = new Game(playerWhite, playerBlack);
Board board = new Board(8, 8, game);

// Puting pieces:
// White player
board.Pieces[0,0] = new Rook(board, true);
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
board.Pieces[6,7] = new Pawn(board, false);

// loop game
while (true)
{
    
}
