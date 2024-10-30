using System.Collections;
using Xadrez.Draw;
using Xadrez.User;

namespace Xadrez.Input;

/// <summary>
/// Manage all classes and player input.
/// </summary>
public class Game
{
    public Game()
    {
        Board = new DrawBoard();
    }
    private DrawBoard Board;
    public void GameLoop()
    {
        // player 1
        var playerUpper = new Player(true);
        // player 2
        var playerLower = new Player(false);
        
        // Game loop
        while (true)
        {
            Board.DrawChessBoard();
            InputPlay(playerUpper);
            break;
        }
    }

    // Get user input
    private int InputPlay(Player player)
    {
        Console.WriteLine($"Vez do {(player.IsUpper ? "azul" : "vermelho")}");
        InfoPlayer(player);

        Console.WriteLine("Faça sua jogada:");
        string[] coordinates = CaptureInput();
        Console.WriteLine($"Voce escolheu a linha {coordinates[0]} da coluna {coordinates[1]}");

        return 0;
    }

    private string[] CaptureInput()
    {
        string row="";
        string column="";
        while (true)
        {
            try
            {
                Console.WriteLine("Escolha a linha [1-8]: ");
                row = Console.ReadLine().Trim();
                if (Board.CaseNumber.Contains(row))
                {
                    break;
                }
                Console.WriteLine($"{row} é invalido!");
            }
            catch
            {
                Console.WriteLine("Charactere invalido!");
            }
        }
        while (true)
        {
            try
            {
                Console.WriteLine("Escolha a coluna [A-H]: ");
                column = Console.ReadLine().Trim().ToLower();
                if (Board.CaseAlpha.Contains(column))
                {
                    break;
                }
                Console.WriteLine($"{column} é invalido!");
            }
            catch
            {
                Console.WriteLine("Charactere invalido!");
            }
        }

        string[] coordinates = [row, column];
        return coordinates;
    }

    private void InfoPlayer(Player player)
    {
        Console.WriteLine($"Informações do jogador {(player.IsUpper ? "azul" : "vermelho")}:");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"Peças vivas: {player.Parts}");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Peças capturadas:");
        foreach (string p in player.Points)
        {
            Console.Write($" {p} ");
        }

        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("\n");
    }
}
