using System;

namespace Fretefy.Test.Domain.Exceptions
{
    public class RegiaoInexistenteException : Exception
    {
        public RegiaoInexistenteException(Guid id) : base () 
        {
            Id = id;   
        } 
        public Guid Id {get;}

    }
}
