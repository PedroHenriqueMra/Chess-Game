using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;

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
    private readonly string[,] Table = new string[8, 8] // [[8],[8],[8],[8],[8],[8],[8],[8]]
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

    public void DrawSelected(int setCol, int setLin)
    {
        setCol = 0;
        setLin = 0;
        DrawTableGame(setCol, setLin);
    }





    private void DrawTableGame(int? column, int? line)
    {
        Console.Write("\n");

        for (int col = 0; col < 8; col++)
        {
            Console.Write($" {CaseNumber[col]} ");
            // for do proprio jogo
            for (int lin = 0; lin < 8; lin++)
            {
                if (column != null && line != null) // selected
                {
                    // color the case selected
                    if (col == column && lin == line)
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                }
                else // xadrez
                {
                    Console.BackgroundColor = (col + lin) % 2 == 0 ? ConsoleColor.White : ConsoleColor.Black;
                }
                // cor dos jogadores
                ColorPlayer(col, lin);

                // desenha o tabluleiro
                Console.Write($" {Table[col, lin]} ");
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

    private void ColorPlayer(int col, int lin)
    {
        if (Table[col, lin] != "-")
        {
            if ((col + lin) % 2 == 0) // color more dark for light background
            {
                Console.ForegroundColor = Table[col, lin] == Table[col, lin].ToUpper() ? ConsoleColor.DarkBlue : ConsoleColor.DarkRed;
            }
            else // color more light for dark background
            {
                Console.ForegroundColor = Table[col, lin] == Table[col, lin].ToUpper() ? ConsoleColor.Blue : ConsoleColor.Red;
            }
        }
    }
}
