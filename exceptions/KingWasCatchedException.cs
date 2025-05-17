namespace ChessGame.Exceptions
{
    [Serializable]
    public class KingWasCatchedException : Exception
    {
        public KingWasCatchedException() { }

        public KingWasCatchedException(string message)
            : base(message)
        {
        }

        public KingWasCatchedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
