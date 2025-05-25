namespace ChessGame.Table.Input
{
    using ChessGame.Exceptions;

    public class Input
    {
        private readonly List<char> linelabel = new List<char> { '8', '7', '6', '5', '4', '3', '2', '1' };
        private readonly List<char> columnlabel = new List<char> { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };

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
                    {
                        if (CancelChoiseInput())
                        {
                            return -1;
                        }
                    }

                    int index = columnlabel.IndexOf(input);
                    if (index == -1)
                        throw new InvalidLabelException($"{input} is not a valid column!.");

                    return index; 
                }
                catch (Exception)
                {
                    Console.WriteLine("Type a valid index!. Between (A-H).");
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
                        {
                            if (CancelChoiseInput())
                            {
                                return -1;
                            }
                        }
                    }

                    int index = linelabel.IndexOf(input);
                    if (index == -1)
                        throw new InvalidLabelException($"{input} is not a valid line!.");

                    return index;
                }
                catch (Exception)
                {
                    Console.WriteLine("Type a valid index!. Between (1-8).");
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
                catch (Exception)
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
