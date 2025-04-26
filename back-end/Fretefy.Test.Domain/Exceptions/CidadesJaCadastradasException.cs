using System;
using System.Collections;
using System.Collections.Generic;

namespace Fretefy.Test.Domain.Exceptions
{
        
    public class CidadesJaCadastradasException : Exception
    {
        public CidadesJaCadastradasException(IEnumerable<Guid> idsCiadades, string campo) : base()
        {
            IdsCidades = idsCiadades;
            Campo = campo;
        }
        public IEnumerable<Guid> IdsCidades {get;}
        public string Campo {get;}

    }
}

