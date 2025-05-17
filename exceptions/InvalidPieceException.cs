namespace ChessGame.Exceptions
{
    [Serializable]
    public class InvalidPieceException : Exception
    {
        public InvalidPieceException() { }

        public InvalidPieceException(string message)
            : base(message)
        {
        }

        public InvalidPieceException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
