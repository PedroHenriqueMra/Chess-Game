using ChessGame.Logic.Game;
using ChessGame.Logic.Service;
using ChessGame.Table.Draw;
using ChessGame.Table.Input;
using ChessGame.Logic.PositionGame;
using ChessGame.Piece.PieceModel;
using ChessGame.Piece.Entity;
using ChessGame.Logic.Player.Color;

Game game = new Game();
DrawGame drawGame = new DrawGame(game, game.Board);
Input userInputService = new Input();

// dependências
GameUtils utils = new GameUtils();
XequeService xequeService = new XequeService(game, game.Board);

// injecting dependencies in game class 
xequeService.Utils = utils;
game.Utils = utils;
game.XequeService = xequeService;


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
            int column = userInputService.GetColumnIndexInput();
            int line = userInputService.GetLineIndexInput();

            if (!playerPlays[column, line])
                break;

            piece = game.Board.GetPieceByPosition(new Position(column, line));

            pieceSteps = game.AllPieceMovements.First(p => p.Key.Position.Compare(piece.Position)).Value;
            drawGame.DrawBoard(piece, pieceSteps);

            // Get target coordinates
            while (true)
            {
                int columnTarget = userInputService.GetColumnIndexInput("COLUMN (A-H). Or type 'X' to cancel and go back: ");
                if (columnTarget == -1)
                {
                    drawGame.DrawOptions(playerPlays);
                    drawGame.DrawInfo(game.CurrentPlayer);
                    break;
                }

                int lineTarget = userInputService.GetLineIndexInput("LINE (1-8). Or type 'X' to cancel and go back: ");
                if (lineTarget == -1)
                {
                    drawGame.DrawOptions(playerPlays);
                    drawGame.DrawInfo(game.CurrentPlayer);
                    break;
                }

                if (!pieceSteps[columnTarget, lineTarget])
                    continue;

                game.MovePiece(piece, new Position(columnTarget, lineTarget));

                // promotion
                if (piece is Pawn)
                {
                    if (piece.Color == PlayerColor.White && piece.Position.Line == 0 || piece.Color == PlayerColor.Black && piece.Position.Line == 7)
                    {
                        drawGame.DrawBoard(piece, new bool[8,8]);
                        drawGame.DrawPromotionOptions();

                        char chosenPiece = userInputService.PromotionInput(new char[]{'q','r','h','b'});
                        Piece promotion = game.Promotion(piece, chosenPiece);

                        // replace pawn for the promotion piece
                        game.RealizePromotion(piece, promotion);
                    }
                }

                return; // exit from loop
            }
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
    // Via xeque-mate
    if (game.IsInXeque)
    {
        King? king = game.GetKing(game.CurrentPlayer);
        if (king == null || game.XequeService.IsInXequeMate(king))
        {
            game.GameIsOver(game.GetEnemyPlayer());
            return true;
        }

        return false;
    }

    // Via draw
    return game.CheckDraw();
}

void GameLoop()
{
    while (!game.GameOver)
    {
        try
        {
            game.RegisterPieceMovesWithXeque();
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
game.PutPiecesOnBoard();
GameLoop();

// tests

