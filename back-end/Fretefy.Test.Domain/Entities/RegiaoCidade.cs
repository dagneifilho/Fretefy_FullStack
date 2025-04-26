using System;

namespace Fretefy.Test.Domain.Entities
{
    public class RegiaoCidade : IEntity
    {
        public RegiaoCidade()
        {
        
        }

        public RegiaoCidade(Guid regiaoId, Guid cidadeId)
        {
            CidadeId = cidadeId;
            RegiaoId = regiaoId;
        }

        public Guid Id { get; set; }
        public Guid CidadeId { get; set; }
        public Guid RegiaoId {get;set;}
    }
    
}
