namespace ChessGame.Exceptions
{
    [Serializable]
    public class IndexNotExistsException : Exception
    {
        public IndexNotExistsException() { }

        public IndexNotExistsException(string message)
            : base(message)
        {
        }

        public IndexNotExistsException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
