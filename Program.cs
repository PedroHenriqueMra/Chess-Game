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
game.XequeService = new XequeService(game, game.Board);
game.Utils = new GameUtils();


game.PutPiecesOnBoard();
void JogarTurno()
{
    bool[,] playerPlays = new bool[8, 8];
    foreach (var p in game.GetPieceOptions(game.CurrentPlayer))
        playerPlays[p.Position.Column, p.Position.Line] = true;

    drawGame.DrawOptions(playerPlays);
    drawGame.DrawInfo(game.CurrentPlayer);

    while (true)
    {
        try
        {
            // Get piece on the board
            int column = userInputService.GetColumnCoordinate();
            int line = userInputService.GetLineCoordinate();
            if (!playerPlays[column, line])
                throw new InvalidPieceException($"{column} - {line} is out of your choice!.");

            Piece piece = game.Board.GetPieceByPosition(new Position(column, line));

            bool[,] pieceSteps = game.AllPieceMovements.First(p => p.Key.Position.Compare(piece.Position)).Value;
            drawGame.DrawBoard(piece, pieceSteps);

            // Get target coordinates
            int columnTarget = userInputService.GetColumnCoordinate();
            int lineTarget = userInputService.GetLineCoordinate();
            if (!pieceSteps[columnTarget, lineTarget])
                throw new ImpossibleToMoveException($"{columnTarget} - {lineTarget} is not an option!.");

            game.MovePiece(piece, new Position(columnTarget, lineTarget));
            break;
        }
        catch (Exception ex)
        {
            drawGame.DrawMessage(ex.Message);
        }
    }
}

bool VerifyEndGame()
{
    if (!game.IsInXeque) return false;

    King? king = game.GetKing(game.GetEnemyPlayer());
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

    drawGame.DrawMessage("Fim de jogo!");
}


// Init Game:
GameLoop();

// tests
//int column = userInputService.GetColumnCoordinate();
//int line = userInputService.GetLineCoordinate();

//Console.WriteLine($"{column} - {line}");
