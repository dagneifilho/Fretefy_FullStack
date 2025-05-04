using System;

namespace Fretefy.Test.Domain.ViewModels.Response
{
        
    public class CidadeDetailedViewModel
    {
        public CidadeDetailedViewModel() {

        }
        public CidadeDetailedViewModel(string nome, string uf, string nomeRegiao)
        {
            Nome = nome;
            UF = uf;
            NomeRegiao = nomeRegiao;

        }

        public string Nome { get; set; }

        public string UF { get; set; }
        public string NomeRegiao {get;set;}
    }

}