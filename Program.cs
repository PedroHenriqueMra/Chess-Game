using ChessGame.Logic.PositionGame;
using ChessGame.Piece.Entity;
using ChessGame.Piece.PieceModel;
using ChessGame.Logic.Game;
using ChessGame.Logic.Service;
using ChessGame.Table.Draw;

// To state instances
// Game:
Game game = new Game();
// dependencies
game.XequeService = new XequeService(game, game.Board);
game.Utils = new GameUtils();

//white pieces:
game.Board.Pieces[0,7] = new Rook(game, true, new Position(0,7));
game.Board.Pieces[1,7] = new Knight(game, true, new Position(1,7));
game.Board.Pieces[2,7] = new Bishop(game, true, new Position(2,0));
game.Board.Pieces[3,7] = new King(game, true, new Position(3,1));
game.Board.Pieces[4,7] = new Queen(game, true, new Position(4,7));
game.Board.Pieces[5,7] = new Bishop(game, true, new Position(5,7));
game.Board.Pieces[6,7] = new Knight(game, true, new Position(6,7));
game.Board.Pieces[7,7] = new Rook(game, true, new Position(7,7));
game.Board.Pieces[0,6] = new Pawn(game, true, new Position(0,6));
game.Board.Pieces[1,6] = new Pawn(game, true, new Position(1,6));
game.Board.Pieces[2,6] = new Pawn(game, true, new Position(2,6));
game.Board.Pieces[3,6] = new Pawn(game, true, new Position(3,6));
game.Board.Pieces[4,6] = new Pawn(game, true, new Position(4,6));
game.Board.Pieces[5,6] = new Pawn(game, true, new Position(5,6));
game.Board.Pieces[6,6] = new Pawn(game, true, new Position(6,6));
game.Board.Pieces[7,6] = new Pawn(game, true, new Position(7,6));
//black pieces:
game.Board.Pieces[0,0] = new Rook(game, false, new Position(0,0));
game.Board.Pieces[1,0] = new Knight(game, false, new Position(1,0));
game.Board.Pieces[2,0] = new Bishop(game, false, new Position(2,0));
game.Board.Pieces[3,0] = new King(game, false, new Position(3,0));
game.Board.Pieces[4,0] = new Queen(game, false, new Position(4,0));
game.Board.Pieces[5,0] = new Bishop(game, false, new Position(5,0));
game.Board.Pieces[6,0] = new Knight(game, false, new Position(6,0));
game.Board.Pieces[7,0] = new Rook(game, false, new Position(7,0));
game.Board.Pieces[0,1] = new Pawn(game, false, new Position(0,1));
game.Board.Pieces[1,1] = new Pawn(game, false, new Position(1,1));
game.Board.Pieces[2,1] = new Pawn(game, false, new Position(2,1));
game.Board.Pieces[3,1] = new Pawn(game, false, new Position(3,1));
game.Board.Pieces[4,1] = new Pawn(game, false, new Position(4,1));
game.Board.Pieces[5,1] = new Pawn(game, false, new Position(5,1));
game.Board.Pieces[7,1] = new Pawn(game, false, new Position(6,1));
game.Board.Pieces[6,1] = new Pawn(game, false, new Position(7,1));

DrawGame draw = new DrawGame(game, game.Board);

Piece piece = game.Board.Pieces[7, 6];
bool[,] steps = piece.GetPositionsToMove();
Console.WriteLine("DrawBoard:");
draw.DrawBoard();
Console.WriteLine("DrawPlayPiece:");
draw.DrawPlayPiece(piece.Position, steps);
Console.WriteLine("DrawPlayOptions:");
draw.DrawPlayOptions(steps);
