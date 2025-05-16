namespace ChessGame.Exceptions
{
    [Serializable]
    public class OutOfTableException : Exception
    {
        public OutOfTableException() { }

        public OutOfTableException(string message)
            : base(message)
        {
        }

        public OutOfTableException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
