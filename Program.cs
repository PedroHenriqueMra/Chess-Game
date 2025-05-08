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

//white pieces:
game.Board.Pieces[0,7] = new Rook(game, true, new Position(0,7));
game.Board.Pieces[1,7] = new Knight(game, true, new Position(1,7));
game.Board.Pieces[2,7] = new Bishop(game, true, new Position(2,7));
game.Board.Pieces[3,7] = new King(game, true, new Position(3,7));
game.Board.Pieces[4,7] = new Queen(game, true, new Position(4,7));
game.Board.Pieces[5,7] = new Bishop(game, true, new Position(5,7));
game.Board.Pieces[6,7] = new Knight(game, true, new Position(6,7));
game.Board.Pieces[7,7] = new Rook(game, true, new Position(7,7));
//game.Board.Pieces[0,6] = new Pawn(game, true, new Position(0,6));
//game.Board.Pieces[1,6] = new Pawn(game, true, new Position(1,6));
//game.Board.Pieces[2,6] = new Pawn(game, true, new Position(2,6));
//game.Board.Pieces[3,6] = new Pawn(game, true, new Position(3,6));
//game.Board.Pieces[4,6] = new Pawn(game, true, new Position(4,6));
//game.Board.Pieces[5,6] = new Pawn(game, true, new Position(5,6));
//game.Board.Pieces[6,6] = new Pawn(game, true, new Position(6,6));
//game.Board.Pieces[7,6] = new Pawn(game, true, new Position(6,7));

// test piece:
Piece pieceTest = new King(game, true, new Position(3,7));
game.Board.Pieces[3,7] = pieceTest;

//black pieces:
game.Board.Pieces[0,0] = new Rook(game, false, new Position(0,0));
game.Board.Pieces[1,0] = new Knight(game, false, new Position(1,0));
game.Board.Pieces[2,0] = new Bishop(game, false, new Position(2,0));
game.Board.Pieces[3,0] = new King(game, false, new Position(3,0));
game.Board.Pieces[4,0] = new Queen(game, false, new Position(4,0));
game.Board.Pieces[5,0] = new Bishop(game, false, new Position(5,0));
game.Board.Pieces[6,0] = new Knight(game, false, new Position(6,0));
game.Board.Pieces[7,0] = new Rook(game, false, new Position(7,0));
//game.Board.Pieces[0,1] = new Pawn(game, false, new Position(0,1));
//game.Board.Pieces[1,1] = new Pawn(game, false, new Position(1,1));
//game.Board.Pieces[2,1] = new Pawn(game, false, new Position(2,1));
//game.Board.Pieces[3,1] = new Pawn(game, false, new Position(3,1));
//game.Board.Pieces[4,1] = new Pawn(game, false, new Position(4,1));
//game.Board.Pieces[5,1] = new Pawn(game, false, new Position(5,1));
//game.Board.Pieces[6,1] = new Pawn(game, false, new Position(6,1));
//game.Board.Pieces[7,1] = new Pawn(game, false, new Position(7,1));


// Draw tegamear steps = pieceTest.GetPositionsToMove();
bool[,] steps = pieceTest.GetPositionsToMove();
for (int l = 0;l < 8;l++)
{
    for (int c = 0;c < 8;c++)
    {
        if (game.Board.Pieces[c,l] != null)
        {
            if (game.Board.Pieces[c,l].Position.Compare(pieceTest.Position))
            {
                var resetColor = Console.BackgroundColor;
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.Write($" {game.Board.Pieces[c,l].ToString()} ");
                Console.BackgroundColor = resetColor;
                continue;
            }
            else
            {
                var resetColor = Console.BackgroundColor;
                if (steps[c,l] && game.Board.Pieces[c,l].IsWhite != pieceTest.IsWhite)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                }
                Console.Write($" {game.Board.Pieces[c,l].ToString()} ");
                Console.BackgroundColor = resetColor;
                continue;
            }
            
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

game.ChangeTurn();

Console.WriteLine($"Current turn: {game.Turns}");

Console.WriteLine($"Player-White: {game.Players.First(p => p.IsWhite).AmountPieces}");
Console.WriteLine($"Player-Black: {game.Players.First(p => !p.IsWhite).AmountPieces}");

//Console.WriteLine($"Movements white pawn: {pieceTest.Movements}");
//Console.WriteLine($"Movements black pawn: {piece1.Movements}");

Console.WriteLine($"Piece En Passant: {game.PawnEnPassant?.ToString()} == {game.PawnEnPassant?.Position.ToString()}");
Console.WriteLine($"En Passant turn is valid: {game.TurnEnPassant}");


//while (!game.GameOver)
//{
    
//}
