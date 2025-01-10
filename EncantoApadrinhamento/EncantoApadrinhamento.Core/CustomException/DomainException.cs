namespace EncantoApadrinhamento.Core.CustomException
{
    public class DomainException : Exception
    {
        internal List<string> _errors = [];
        public List<string> Errors => _errors;

        public DomainException(List<string> errors) => _errors = errors;

        public DomainException() { }
        public DomainException(string message) : base(message) { }
        public DomainException(string message, Exception inner) : base(message, inner) { }
    }
}
