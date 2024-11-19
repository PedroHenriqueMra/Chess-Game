using System.Collections;
using Xadrez.Draw;
using Xadrez.User;
using Xadrez.Business;

namespace Xadrez.Input;

/// <summary>
/// Manage all classes and player input.
/// </summary>
public class Game
{
    public Game()
    {
        // game table
        Board = new DrawBoard();
        // player 1
        PlayerUpper = new Player(true);
        // player 2
        PlayerLower = new Player(false);
        // game logic
        Logic = new Logic();
    }
    private DrawBoard Board;
    private Player PlayerUpper;
    private Player PlayerLower;
    private Logic Logic;
    public async Task GameLoop()
    {
        // Game loop
        while (true)
        {
            Turn(PlayerUpper);
            for(var l=0;l<50;l++)
            {Console.Write("-");};
            await Task.Delay(2000);
            Turn(PlayerLower);

            break;
        }
    }

    // Get user input
    private string[] InputPlayer(Player player)
    {
        Console.WriteLine($"Vez do {(player.IsUpper ? "azul" : "vermelho")}");
        InfoPlayer(player);

        Console.WriteLine("Faça sua jogada:");
        string[] coordinates = CaptureInput();
        Console.WriteLine($"Voce escolheu a linha {coordinates[0]} da coluna {coordinates[1]}");

        return coordinates;
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

    private void Turn(Player player)
    {
        while (true)
        {
            Board.DrawChessBoard();
            var coordinates = InputPlayer(player);
            // coordinate 0 == row | coordinate 1 = column
            Board.DrawSelected(coordinates[0], coordinates[1]);
            if (Logic.IsPlayersPiece(player, Board, coordinates))
            {
                
            }

            break;
        }
    }
}
