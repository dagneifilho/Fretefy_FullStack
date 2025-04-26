using System;
using System.Collections;
using System.Collections.Generic;

namespace Fretefy.Test.Domain.Exceptions
{
    public class CidadeInexistenteException : Exception 
    {
        public CidadeInexistenteException(IEnumerable<Guid> idsCidades) : base()
        {   
            IdsCidades = idsCidades;
        }
        public IEnumerable<Guid> IdsCidades {get;}
    }
}