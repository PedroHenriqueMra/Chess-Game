using ChessGame.Logic.Game;
using ChessGame.Logic.Service;
using ChessGame.Exceptions;
using ChessGame.Table;
using ChessGame.Table.Draw;
using ChessGame.Table.Input;
using ChessGame.Logic.PositionGame;
using ChessGame.Piece.PieceModel;
using System.ComponentModel;
using ChessGame.Piece.Entity;
using ChessGame.Logic.Player.PlayerEntity;
using System.Threading.Tasks;

Game game = new Game();
DrawGame drawGame = new DrawGame(game, game.Board);
Input userInputService = new Input();

// dependências
GameUtils utils = new GameUtils();
XequeService xequeService = new XequeService(game, game.Board);
xequeService.Utils = utils;

// injecting dependencies in game class 
game.Utils = utils;
game.XequeService = xequeService;


game.PutPiecesOnBoard();
void JogarTurno()
{
    // get possible movements
    bool[,] playerPlays = new bool[8, 8];
    foreach (var p in game.GetPieceOptions(game.CurrentPlayer))
        playerPlays[p.Position.Column, p.Position.Line] = true;

    drawGame.DrawOptions(playerPlays);
    drawGame.DrawInfo(game.CurrentPlayer);

    while (true)
    {
        try
        {
            Piece piece;
            bool[,] pieceSteps;

            // Get piece on the board
            int column = userInputService.GetColumnCoordinate();
            int line = userInputService.GetLineCoordinate();

            if (!playerPlays[column, line])
                throw new InvalidPieceException($"{column} - {line} is out of your choice!.");

            piece = game.Board.GetPieceByPosition(new Position(column, line));

            pieceSteps = game.AllPieceMovements.First(p => p.Key.Position.Compare(piece.Position)).Value;
            drawGame.DrawBoard(piece, pieceSteps);

            // Get target coordinates
            while (true)
            {
                int columnTarget = userInputService.GetColumnCoordinate("Select the column index (A-H). Or type 'X' to cancel and go back");
                if (columnTarget == -1)
                {
                    drawGame.DrawOptions(playerPlays);
                    drawGame.DrawInfo(game.CurrentPlayer);
                    break;
                }

                int lineTarget = userInputService.GetLineCoordinate("Select the line index (1-8). Or type 'X' to cancel and go back");
                if (lineTarget == -1)
                {
                    drawGame.DrawOptions(playerPlays);
                    drawGame.DrawInfo(game.CurrentPlayer);
                    break;
                }

                if (!pieceSteps[columnTarget, lineTarget])
                    continue;

                game.MovePiece(piece, new Position(columnTarget, lineTarget));
                return; // exit from loop
            }
        }
        catch (InvalidPieceException ex)
        {
            drawGame.DrawMessage(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine("An unexpected error has occurred.");
        }
    }
}

bool VerifyEndGame()
{
    if (!game.IsInXeque) return false;

    King? king = game.GetKing(game.CurrentPlayer);
    if (king == null || game.XequeService.IsInXequeMate(king))
    {
        game.GameIsOver(game.GetEnemyPlayer());
        return true;
    }

    return false;
}

void GameLoop()
{
    while (!game.GameOver)
    {
        try
        {
            game.SetAllPieceMoves();
            game.SetIsInXeque();

            if (VerifyEndGame())
                break;

            JogarTurno();
            game.ChangeTurn();
        }
        catch (Exception ex)
        {
            drawGame.DrawMessage($"Erro inesperado: {ex.Message}");
        }
    }

    drawGame.DrawGameResult(game.Winner.Color);
}


// Init Game:
GameLoop();

// tests
//int column = userInputService.GetColumnCoordinate();
//int line = userInputService.GetLineCoordinate();

//Console.WriteLine($"{column} - {line}");
