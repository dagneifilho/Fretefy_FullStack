using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Fretefy.Test.Infra.EntityFramework.Repositories
{
    public class RegiaoCidadeRepository : IRegiaoCidadeRepository
    {

        private readonly TestDbContext _dbContext;
        public RegiaoCidadeRepository(TestDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<RegiaoCidade>> InsertManyAsync(IEnumerable<RegiaoCidade> regioesCidades)
        {
            await _dbContext.RegiaoCidade.AddRangeAsync(regioesCidades);
            await _dbContext.SaveChangesAsync();
            return regioesCidades;
        }
        
        public async Task DeleteByRegiaoIdAsync(Guid regiaoId)
        {
            var regioesCidades = await _dbContext.RegiaoCidade.Where(rc => rc.RegiaoId == regiaoId).ToListAsync();
            _dbContext.RegiaoCidade.RemoveRange(regioesCidades);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<RegiaoCidade>> GetByIdsCidades(IEnumerable<Guid> idsCidades)
        {
            return await _dbContext.RegiaoCidade.Where(rc => idsCidades.Contains(rc.CidadeId)).AsNoTracking().ToListAsync();
        }
        
        public async Task<IEnumerable<RegiaoCidade>> ListAsync()
        {
            return await _dbContext.RegiaoCidade.AsNoTracking().ToListAsync();
        }
        

        protected void Dispose(bool disposing)
        {
            _dbContext.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}

