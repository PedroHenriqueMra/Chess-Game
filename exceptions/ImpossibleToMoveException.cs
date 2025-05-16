namespace ChessGame.Exceptions
{
    [Serializable]
    public class ImpossibleToMoveException : Exception
    {
        public ImpossibleToMoveException() { }

        public ImpossibleToMoveException(string message)
            : base(message)
        {
        }

        public ImpossibleToMoveException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
