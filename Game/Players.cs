/// <summary>
/// Entity class for player 1 and player 2.
/// </summary>
public class Player
{
    public string[] Points { get; set; } = new string[16];
    public int Parts { get; set; } = 16;
    public bool IsUpper { get; set; }
    public Player(bool isUpper)
    {
        IsUpper = isUpper;
    }

    public void InfoPlayer()
    {
        Console.WriteLine($"Informações do jogador {(IsUpper ? "azul" : "vermelho")}:");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"Peças vivas: {Parts}");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Peças capturadas:");
        foreach (string p in Points)
        {
            Console.Write($" {p} ");
        }

        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("\n");
    }
}
