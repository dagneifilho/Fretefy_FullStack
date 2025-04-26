using System;
using System.Collections.Generic;

namespace Fretefy.Test.Domain.ViewModels.Response
{   
    public class RegiaoViewModel
    {
        public RegiaoViewModel() 
        {

        }
        public RegiaoViewModel(Guid id, string nome, IEnumerable<CidadeViewModel> cidades, bool ativa) 
        {
            Id = id;
            Nome = nome;
            Cidades = cidades;
            Ativa = ativa;
        }
        public Guid Id {get;set;}
        public string Nome {get;set;}
        public IEnumerable<CidadeViewModel> Cidades {get; set;} = new List<CidadeViewModel>();
        public bool Ativa {get;set;}
    }
}

