using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;

namespace Xadrez.Draw;
public class Board
{
    public string[][] Table = new string[9][] // [[8],[8],[8],[8],[8],[8],[8],[8]]
    {
        new string[] {"8", "t", "r", "b", "k", "q", "b", "r", "t"},
        new string[] {"7", "p", "p", "p", "p", "p", "p", "p", "p"},
        new string[] {"6", "-", "-", "-", "-", "-", "-", "-", "-"},
        new string[] {"5", "-", "-", "-", "-", "-", "-", "-", "-"},
        new string[] {"4", "-", "-", "-", "-", "-", "-", "-", "-"},
        new string[] {"3", "-", "-", "-", "-", "-", "-", "-", "-"},
        new string[] {"2", "p", "p", "p", "p", "p", "p", "p", "p"},
        new string[] {"1", "t", "r", "b", "k", "q", "b", "r", "t"},
        new string[] {"x", "a", "b", "c", "d", "e", "f", "g", "h"}
    };

    public void DrawTable()
    {
        int rows = 9;
        int cols = 9;
        for (int line = 0; line < rows; line++)
        {
            for (int col = 0; col < cols; col++)
            {
                if ((line + col) % 2 == 0 && line != 8 && col != 0)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                Console.Write($" {Table[line][col]} ");
            }
            Console.Write("\n");
        }
        Console.BackgroundColor = ConsoleColor.Black;
    }

    public void Select()
    {
        
    }
}
