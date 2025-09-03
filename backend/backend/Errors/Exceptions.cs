namespace backend.Errors
{
    public class ConflitoException : Exception
    {
        public ConflitoException(string message) : base(message) { }
    }
    public class NaoEncontradoException : Exception
    {
        public NaoEncontradoException(string message) : base(message) { }
    }

    public class NaoAutorizadoException : Exception
    {
        public NaoAutorizadoException(string message) : base(message) { }
    }

    public class ProibidoException : Exception
    {
        public ProibidoException(string message) : base(message) { }
    }
}
