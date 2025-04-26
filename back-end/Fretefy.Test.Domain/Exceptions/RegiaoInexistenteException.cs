using System;

namespace Fretefy.Test.Domain.Exceptions
{
    public class RegiaoInexistenteException : Exception
    {
        public RegiaoInexistenteException(Guid id, string campo) : base () 
        {
            Id = id;   
            Campo = campo;
        } 
        public Guid Id {get;}
        public string Campo {get;}

    }
}
