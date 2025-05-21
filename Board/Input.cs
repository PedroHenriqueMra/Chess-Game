using System.Security.Cryptography.X509Certificates;
using ChessGame.Exceptions;

namespace ChessGame.Table.Input
{
    public class Input
    {
        private readonly char[] LineIndex = { '8', '7', '6', '5', '4', '3', '2', '1' };
        private readonly char[] ColumnIndex = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };

        public int GetColumnCoordinate(string message) // with message
        {
            while (true)
            {
                try
                {
                    Console.WriteLine(message);
                    char input = char.ToUpper(char.Parse(Console.ReadLine()));

                    int index = ColumnIndex.ToList().IndexOf(input);

                    if (input == 'X')
                    {
                        if (CancelChoise())
                            return -1;

                        continue;
                    }

                    if (index != -1)
                        return index;

                    throw new IndexNotExistsException($"The index '{input}' is invalid!");
                }
                catch (IndexNotExistsException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public int GetColumnCoordinate() // withou message
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Select the column index (A-H).");
                    char input = char.ToUpper(char.Parse(Console.ReadLine()));

                    int index = ColumnIndex.ToList().IndexOf(input);

                    if (input == 'X')
                    {
                        if (CancelChoise())
                            return -1;

                        continue;
                    }

                    if (index != -1)
                        return index;

                    throw new IndexNotExistsException($"The index '{input}' is invalid!");
                }
                catch (IndexNotExistsException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public int GetLineCoordinate(string message) // with message
        {
            while (true)
            {
                try
                {
                    Console.WriteLine(message);
                    char input = char.ToUpper(char.Parse(Console.ReadLine()));

                    int index = LineIndex.ToList().IndexOf(input);

                    if (input == 'X')
                    {
                        if (CancelChoise())
                            return -1;

                        continue;
                    }

                    if (index != -1)
                        return index;

                    throw new IndexNotExistsException($"The index '{input}' is invalid!");
                }
                catch (IndexNotExistsException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public int GetLineCoordinate() // withou message
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Select the line index (1-8).");
                    char input = char.ToUpper(char.Parse(Console.ReadLine()));

                    int index = LineIndex.ToList().IndexOf(input);

                    if (input == 'X')
                    {
                        if (CancelChoise())
                            return -1;

                        continue;
                    }

                    if (index != -1)
                        return index;

                    throw new IndexNotExistsException($"The index '{input}' is invalid!");
                }
                catch (IndexNotExistsException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public bool CancelChoise()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Are you sure you want to cancel this action?. (y/n)");
                    char answer = char.ToUpper(char.Parse(Console.ReadLine()));

                    if (answer == 'Y')
                        return true;

                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Type a valid answer (one character)!");
                }
            }
        }
    }
}
