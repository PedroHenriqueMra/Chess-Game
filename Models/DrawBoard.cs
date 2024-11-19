using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;
using Xadrez.User;

namespace Xadrez.Draw;
/// <summary>
/// It draw the visual game.
/// </summary>
public class DrawBoard
{
    /// <summary>
    /// This is the headquarters of the game.
    /// I keep the fields in string format to avoid type errors.
    /// </summary>
    public readonly string[,] Table = new string[8, 8] // [[8],[8],[8],[8],[8],[8],[8],[8]]
    {
        {"t", "h", "b", "k", "q", "b", "h", "t"},
        {"p", "p", "p", "p", "p", "p", "p", "p"},
        {"-", "-", "-", "-", "-", "-", "-", "-"},
        {"-", "-", "-", "-", "-", "-", "-", "-"},
        {"-", "-", "-", "-", "-", "-", "-", "-"},
        {"-", "-", "-", "-", "-", "-", "-", "-"},
        {"P", "P", "P", "P", "P", "P", "P", "P"},
        {"T", "H", "B", "K", "Q", "B", "H", "T"}
    };

    public readonly string[] CaseNumber = new string[]
    {
        "8", "7", "6", "5", "4", "3", "2", "1"
    };
    public readonly string[] CaseAlpha = new string[]
    {
        "a", "b", "c", "d", "e", "f", "g", "h"
    };
    // Draw the table of game
    public void DrawChessBoard()
    {
        DrawTableGame(null, null);
    }

    public void DrawSelected(string codeLine, string codeCol)
    {
        int line = GetCoordinates.GetCoordinateLine(CaseNumber, codeLine.Trim());
        int column = GetCoordinates.GetCoordinateColumn(CaseAlpha, codeCol.ToLower().Trim());
        Console.WriteLine($"{line} - {column}");
        Console.WriteLine(Table[line, column]);
        Console.WriteLine(Table[line, column] == Table[line, column].ToUpper() ? $"a peça {Table[line, column]} é sua" : $"a peça {Table[line, column]} nao é sua!");

        DrawTableGame(line, column);
    }

    private void DrawTableGame(int? lineSelected, int? columnSelected)
    {
        Console.Write("\n");

        for (int lin = 0; lin < 8; lin++)
        {
            Console.Write($" {CaseNumber[lin]} ");
            // for do proprio jogo
            for (int col = 0; col < 8; col++)
            {
                if (lineSelected != null && columnSelected != null) // selected
                {
                    // color the case selected
                    if (lin == lineSelected && col == columnSelected)
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                }
                else // xadrez
                {
                    Console.BackgroundColor = (lin + col) % 2 == 0 ? ConsoleColor.White : ConsoleColor.Black;
                }
                // cor dos jogadores
                ColorPlayer(lin, col);

                // desenha o tabluleiro
                Console.Write($" {Table[lin, col]} ");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.Write("\n");
        }

        // desenha os indentificadores das linhas (a, b, c...h)
        foreach (string alpha in CaseAlpha)
        {
            Console.Write(Array.IndexOf(CaseAlpha, alpha) == 0 ? $" x  {alpha} " : $" {alpha} ");
        }
        Console.Write("\n");
    }

    private void ColorPlayer(int lin, int col)
    {
        if (Table[lin, col] != "-")
        {
            if ((lin + col) % 2 == 0) // color more dark for light background
            {
                Console.ForegroundColor = Table[lin, col] == Table[lin, col].ToUpper() ? ConsoleColor.DarkBlue : ConsoleColor.DarkRed;
            }
            else // color more light for dark background
            {
                Console.ForegroundColor = Table[lin, col] == Table[lin, col].ToUpper() ? ConsoleColor.Blue : ConsoleColor.Red;
            }
        }
    }
}
