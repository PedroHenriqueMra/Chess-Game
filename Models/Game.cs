using Xadrez.Draw;
using Xadrez.User;

namespace Xadrez.Input;

/// <summary>
/// Manage all classes and player input.
/// </summary>
public class Game
{
    public void GameLoop()
    {
        var tabuleiro = new DrawBoard();
        // player 1
        var playerUpper = new Player(true);
        // player 2
        var playerLowwer = new Player(false);
        // Game loop
        while (true)
        {
            tabuleiro.DrawChessBoard();
            InputPlay(playerUpper);
            InputPlay(playerLowwer);
            break;
        }
    }

    internal void LoopGame()
    {
        throw new NotImplementedException();
    }

    // Get user input
    private int InputPlay(Player player)
    {
        Console.WriteLine($"Vez do {(player.IsUpper ? "azul" : "vermelho")}");
        InfoPlayer(player);

        return 0;
    }

    private void InfoPlayer(Player player)
    {

    }
}
