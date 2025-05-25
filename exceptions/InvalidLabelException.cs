namespace ChessGame.Exceptions
{
    [System.Serializable]
    public class InvalidLabelException : System.Exception
    {
        public InvalidLabelException() { }
        public InvalidLabelException(string message) : base(message) { }
        public InvalidLabelException(string message, System.Exception inner) : base(message, inner) { }
        protected InvalidLabelException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    } 
}
