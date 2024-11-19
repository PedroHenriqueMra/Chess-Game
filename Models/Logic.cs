using Xadrez.Draw;
using Xadrez.User;

namespace Xadrez.Business;

/// <summary>
/// Treats of the logic game as parts function, parts movie, checkmate and etc.
/// </summary>
public class Logic
{
    // coordnates = [line, col]
    public bool IsPlayersPiece(Player player, DrawBoard board, string[] coordnates)
    {
        int line = GetCoordinates.GetCoordinateLine(board.CaseNumber, coordnates[0].Trim());
        int column = GetCoordinates.GetCoordinateColumn(board.CaseAlpha, coordnates[1].ToLower().Trim());
        var getPiece = board.Table[line, column];
        Console.WriteLine($"{line} - {column}");
        if (player.IsUpper && getPiece == getPiece.ToUpper())
        {
            return true;
        }

        if (!player.IsUpper && getPiece == getPiece.ToLower())
        {
            return true;
        }

        return false;
    }
}
