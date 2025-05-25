using ChessGame.Logic.Game;
using ChessGame.Logic.Service;
using ChessGame.Table.Draw;
using ChessGame.Table.Input;
using ChessGame.Logic.PositionGame;
using ChessGame.Piece.PieceModel;
using ChessGame.Piece.Entity;
using ChessGame.Logic.Player.Color;
using ChessGame.Oponent;
using ChessGame.Exceptions;
using ChessGame.Table;
using ChessGame.Logic.Player.PlayerEntity;


Game game = new Game();
Oponent oponentService = new Oponent();
DrawGame drawGame = new DrawGame(game, game.Board);
Input userInputService = new Input();

// dependências
GameUtils utils = new GameUtils();
XequeService xequeService = new XequeService(game, game.Board);

// injecting dependencies in game class 
xequeService.Utils = utils;
game.Utils = utils;
game.XequeService = xequeService;


void PlayTurn(bool oponentOn = false)
{
    if (oponentOn & game.CurrentPlayer.Color == PlayerColor.Black)
    {
        OponentPlay();
        return;
    }

    // get possible movements
    List<Piece> options = game.GetPieceOptions(game.CurrentPlayer);
    bool[,] playerPlays = new bool[8, 8];
    foreach (var p in options)
    {
        playerPlays[p.Position.Column, p.Position.Line] = true;
    }

    // Play chosen loop
    while (true)
    {
        Piece chosenPiece;
        int columnTarget = 0;
        int lineTarget = 0;

        // Piece chosen
        while (true)
        {
            drawGame.DrawOptions(playerPlays);
            drawGame.DrawInfo(game.CurrentPlayer);

            try
            {
                int columnLabel = userInputService.GetColumnIndexInput();
                if (columnLabel == -1)
                    continue;

                int lineLabel = userInputService.GetLineIndexInput();
                if (columnLabel == -1)
                    continue;

                if (!playerPlays[columnLabel, lineLabel])
                    throw new OutOfPieceOptionsException($"{columnLabel} - {lineLabel} is not a valid option!.");

                chosenPiece = options.First(p => p.Position.Compare(columnLabel, lineLabel));
                break;
            }
            catch (InvalidLabelException ex)
            {
                drawGame.DrawOptions(playerPlays);
                drawGame.DrawInfo(game.CurrentPlayer);
                drawGame.DrawMessage(ex.Message);
            }
            catch (OutOfPieceOptionsException ex)
            {
                drawGame.DrawOptions(playerPlays);
                drawGame.DrawInfo(game.CurrentPlayer);
                drawGame.DrawMessage(ex.Message);
            }
        }

        bool[,] pieceSteps = game.AllPieceMovements.First(p => p.Key.Position.Compare(chosenPiece.Position)).Value;
        drawGame.DrawBoard(chosenPiece, pieceSteps);

        // Get step coordinate
        bool returnAll = false;
        while (true)
        {
            try
            {
                columnTarget = userInputService.GetColumnIndexInput("COLUMN (A-H). Or type 'X' to cancel and go back: ");
                if (columnTarget == -1)
                {
                    returnAll = true;
                    break;
                }

                lineTarget = userInputService.GetLineIndexInput("LINE (1-8). Or type 'X' to cancel and go back: ");
                if (lineTarget == -1)
                {
                    returnAll = true;
                    break;
                }

                if (!pieceSteps[columnTarget, lineTarget])
                    throw new InvalidPieceTragetException("Invalid position!. Please choose an other.");

                break;
            }
            catch (InvalidLabelException ex)
            {
                drawGame.DrawBoard(chosenPiece, pieceSteps);
                drawGame.DrawMessage(ex.Message);
            }
            catch (InvalidPieceTragetException ex)
            {
                drawGame.DrawBoard(chosenPiece, pieceSteps);
                drawGame.DrawMessage(ex.Message);
            }
        }

        if (returnAll)
            continue;

        // Make piece move
        game.MovePiece(chosenPiece, new Position(columnTarget, lineTarget));

        // promotion
        if (chosenPiece is Pawn)
        {
            if (chosenPiece.Color == PlayerColor.White && chosenPiece.Position.Line == 0 || chosenPiece.Color == PlayerColor.Black && chosenPiece.Position.Line == 7)
            {
                drawGame.DrawBoard(chosenPiece, new bool[8, 8]);
                drawGame.DrawPromotionOptions();

                char chosenPromotion = userInputService.PromotionInput(new char[] { 'q', 'r', 'h', 'b' });
                Piece promotion = game.Promotion(chosenPiece, chosenPromotion);

                // replace pawn for the promotion piece
                game.RealizePromotion(chosenPiece, promotion);
            }
        }

        return;
    }
}

void OponentPlay()
{
    Piece oponentPiece = oponentService.RandomPiece(game.GetPieceOptions(game.CurrentPlayer));
    bool[,] pieceSteps = game.AllPieceMovements.First(p => p.Key.Position.Compare(oponentPiece.Position)).Value;

    Position oponentChoose = oponentService.RandomPieceStep(pieceSteps);

    game.MovePiece(oponentPiece, oponentChoose);

    if (oponentPiece is Pawn && oponentPiece.Position.Line == 7)
    {
        Piece oponentPromotion = oponentService.RandomPromotion(game, PlayerColor.Black, oponentChoose);
        game.RealizePromotion(oponentPiece, oponentPromotion);
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

            PlayTurn(true);
            game.ChangeTurn();
        }
        catch (KingIsOutOfBoardException ex)
        {
            King? whiteKing = game.GetKing(game.GetPlayerByColor(PlayerColor.White));
            King? blackKing = game.GetKing(game.GetPlayerByColor(PlayerColor.Black));

            Player winner = game.GetPlayerByColor(whiteKing == null ? PlayerColor.Black : PlayerColor.White);

            game.GameIsOver(winner);

            drawGame.DrawEndGame(winner.Color, ex.Message);
            return;
        }
        catch (Exception ex)
        {
            drawGame.DrawMessage(ex.Message);
        }
    }

    drawGame.DrawEndGame(game.Winner.Color);
}


// Init Game:
game.PutPiecesOnBoard();
GameLoop();
