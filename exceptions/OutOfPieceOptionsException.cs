namespace ChessGame.Exceptions
{
    [System.Serializable]
    public class OutOfPieceOptionsException : System.Exception
    {
        public OutOfPieceOptionsException() { }
        public OutOfPieceOptionsException(string message) : base(message) { }
        public OutOfPieceOptionsException(string message, System.Exception inner) : base(message, inner) { }
        protected OutOfPieceOptionsException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    } 
}
