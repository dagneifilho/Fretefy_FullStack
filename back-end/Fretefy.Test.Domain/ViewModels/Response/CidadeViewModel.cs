using System;
using Fretefy.Test.Domain.Entities;

namespace Fretefy.Test.Domain.ViewModels.Response
{
    
    public class CidadeViewModel
    {
        public CidadeViewModel() 
        {

        }
        public CidadeViewModel(Guid id, string nome, string uf)
        {
            Id = id;
            Nome = nome;
            UF = uf;
        }

        public Guid Id {get;set;}
        public string Nome {get;set;}
        public string UF {get;set;}
    }

}