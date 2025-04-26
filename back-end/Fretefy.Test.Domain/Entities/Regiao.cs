using System;
using System.Collections.Generic;
using System.Dynamic;

namespace Fretefy.Test.Domain.Entities
{
    public class Regiao : IEntity
    {
        
        public Regiao()
        {
        
        }
        public Regiao(string nome){
            Nome = nome;
        }

        public Regiao(string nome, IEnumerable<RegiaoCidade> regiaoCidades, bool ativa)
        {
            Nome = nome;
            RegiaoCidades = regiaoCidades;
            Ativa = ativa;
        }

        public Guid Id { get; set; }

        public string Nome { get; set; }
        public IEnumerable<RegiaoCidade> RegiaoCidades {get;set;}
        public bool Ativa {get;set;}
    }

}

