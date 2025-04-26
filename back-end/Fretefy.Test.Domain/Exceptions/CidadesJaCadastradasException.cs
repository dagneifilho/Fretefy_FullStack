using System;
using System.Collections;
using System.Collections.Generic;

namespace Fretefy.Test.Domain.Exceptions
{
        
    public class CidadesJaCadastradasException : Exception
    {
        public CidadesJaCadastradasException(IEnumerable<Guid> idsCiadades) : base()
        {
            IdsCidades = idsCiadades;
        }
        public IEnumerable<Guid> IdsCidades {get;}

    }
}

