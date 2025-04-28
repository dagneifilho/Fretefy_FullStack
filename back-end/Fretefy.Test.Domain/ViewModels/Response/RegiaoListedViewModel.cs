using System;

namespace Fretefy.Test.Domain.ViewModels.Response
{
    
    public class RegiaoListedViewModel
    {
        public RegiaoListedViewModel() {

        }
        public RegiaoListedViewModel(Guid id, string nome, bool ativa) 
        {
            Id = id;
            Nome = nome;
            Ativa = ativa;
        }
        public Guid Id {get;set;}
        public string Nome {get;set;}
        public bool Ativa {get;set;}

    }

}
