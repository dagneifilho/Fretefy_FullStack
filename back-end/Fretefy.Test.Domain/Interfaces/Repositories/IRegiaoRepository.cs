using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fretefy.Test.Domain.Entities;

namespace Fretefy.Test.Domain.Interfaces.Repositories
{ 
        
    public interface IRegiaoRepository : IDisposable
    {
        Task<Regiao> InsertAsync(Regiao regiao);
        Task<Regiao?> GetByNomeAsync(string nome);
        Task<Regiao?> GetByIdAsync(Guid id, bool forUpdate = false);
        Task<IEnumerable<Regiao>> ListAsync();
        Task<Regiao> UpdateAsync(Regiao regiao);
    }

}