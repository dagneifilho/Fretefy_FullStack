using System;

namespace Fretefy.Test.Domain.Exceptions 
{
    
    public class RegiaoExistenteException : Exception
    {
        public RegiaoExistenteException(string campo) : base()
        {
            Campo = campo;
        }
        
        public string Campo {get;}
    }

}
