using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Fretefy.Test.Infra.EntityFramework.Repositories
{

    public class RegiaoRepository : IRegiaoRepository
    {
        private readonly TestDbContext _dbContext;
        public RegiaoRepository(TestDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Regiao?> GetByNomeAsync(string nome)
        {
            return await _dbContext.Regiao.Where(r => r.Nome.Equals(nome)).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<Regiao> InsertAsync(Regiao regiao)
        {
            await _dbContext.Regiao.AddAsync(regiao);
            await _dbContext.SaveChangesAsync();
            return regiao;
        }

        public async Task<IEnumerable<Regiao>> ListAsync()
        {
            return await _dbContext.Regiao.AsNoTracking().ToListAsync();
        }

        public async Task<Regiao?> GetByIdAsync(Guid id, bool forUpdate = false)
        {
            var query =  _dbContext.Regiao.Include(r => r.RegiaoCidades).Where(r => r.Id == id);
            if(!forUpdate)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync();

        }
        public async Task<Regiao> UpdateAsync(Regiao regiao)
        {
            _dbContext.Regiao.Update(regiao);
            await _dbContext.SaveChangesAsync();
            return regiao;
        }


        protected virtual void Dispose(bool disposing)
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