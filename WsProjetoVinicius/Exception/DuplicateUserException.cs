using System;

namespace WsProjetoVinicius.Model
{
    public class DuplicateUserException : Exception
    {
        public DuplicateUserException() : base() { }
        public DuplicateUserException(string message) : base(message) { }
    }
}
