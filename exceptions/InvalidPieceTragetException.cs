namespace ChessGame.Exceptions
{
    [System.Serializable]
    public class InvalidPieceTragetException : System.Exception
    {
        public InvalidPieceTragetException() { }
        public InvalidPieceTragetException(string message) : base(message) { }
        public InvalidPieceTragetException(string message, System.Exception inner) : base(message, inner) { }
        protected InvalidPieceTragetException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    } 
}
