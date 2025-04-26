using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fretefy.Test.Domain.Entities;

namespace Fretefy.Test.Domain.Interfaces.Repositories
{
    
    public interface IRegiaoCidadeRepository : IDisposable
    {
        Task<IEnumerable<RegiaoCidade>> InsertManyAsync(IEnumerable<RegiaoCidade> regioesCidades);
        Task<IEnumerable<RegiaoCidade>> GetByIdsCidades(IEnumerable<Guid> idsCidades);
        Task DeleteByRegiaoIdAsync(Guid regiaoId);        
    }

}
