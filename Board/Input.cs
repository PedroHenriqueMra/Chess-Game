using ChessGame.Exceptions;

namespace ChessGame.Table.Input
{
    public class Input
    {
        private readonly char[] LineIndex = { '8', '7', '6', '5', '4', '3', '2', '1' };
        private readonly char[] ColumnIndex = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };

        public int GetColumnCoordinate()
        {
            Console.WriteLine("Escolha o indice da coluna (A-H)");
            char input = char.Parse(Console.ReadLine());

            int index = ColumnIndex.ToList().IndexOf(char.IsUpper(input) ? input : char.ToUpper(input));
            if (index != -1)
                return index;

            throw new IndexNotExistsException($"The index '{input}' is invalid!");
        }

        public int GetLineCoordinate()
        {
            Console.WriteLine("Escolha o indice da linha (1-8)");
            char input = char.Parse(Console.ReadLine());

            int index = LineIndex.ToList().IndexOf(input);
            if (index != -1)
                return index;

            throw new IndexNotExistsException($"The index '{input}' is invalid!");
        }
    }
}
