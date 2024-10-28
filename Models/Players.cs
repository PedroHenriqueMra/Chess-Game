using System.ComponentModel;

namespace Xadrez.User;

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
}
