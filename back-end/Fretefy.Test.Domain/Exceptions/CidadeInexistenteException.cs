using System;
using System.Collections;
using System.Collections.Generic;

namespace Fretefy.Test.Domain.Exceptions
{
    public class CidadeInexistenteException : Exception 
    {
        public CidadeInexistenteException(IEnumerable<Guid> idsCidades, string campo) : base()
        {   
            IdsCidades = idsCidades;
            Campo = campo;
        }
        public IEnumerable<Guid> IdsCidades {get;}
        public string Campo {get;}
    }
}