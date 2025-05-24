using System.Security.Cryptography.X509Certificates;

namespace ChessGame.Table.Input
{
    public class Input
    {
        private readonly List<char> lineIndex = new List<char> { '8', '7', '6', '5', '4', '3', '2', '1' };
        private readonly List<char> columnIndex = new List<char> { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };

        public int GetColumnIndexInput(string? message = null)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine(message != null ? message : "COLUMN (A-H): ");
                    char input = char.Parse(Console.ReadLine());
                    input = char.IsLower(input) ? char.ToUpper(input) : input;

                    if (input == 'X')
                        return -1;

                    int index = columnIndex.IndexOf(input);
                    if (index != -1)
                        return index;
                }
                catch (Exception)
                {
                    Console.WriteLine("Type a valid index!. (One character, without spaces)");
                }
            }
        }

        public int GetLineIndexInput(string? message = null)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine(message != null ? message : "LINE (1-8): ");
                    char input = char.Parse(Console.ReadLine());

                    if (char.IsLetter(input))
                    {
                        input = char.IsLower(input) ? char.ToUpper(input) : input;
                        if (input == 'X')
                            return -1;
                    }

                    int index = lineIndex.IndexOf(input);
                    if (index != -1)
                        return index;
                }
                catch (Exception)
                {
                    Console.WriteLine("Type a valid index!. (One character, without spaces)");
                }
            }
        }

        public bool CancelChoiseInput()
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

        public char PromotionInput(char[] charList)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Do your choose: ");
                    char answer = char.ToUpper(char.Parse(Console.ReadLine()));

                    answer = char.ToLower(answer);
                    if (charList.Contains(answer))
                        return answer;
                }
                catch (Exception)
                {
                    Console.WriteLine("Type a valid answer (one character)!");
                }
            }
        }
    }
}
