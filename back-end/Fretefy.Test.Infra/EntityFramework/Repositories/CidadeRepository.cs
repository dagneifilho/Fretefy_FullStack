using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fretefy.Test.Infra.EntityFramework.Repositories
{
    public class CidadeRepository : ICidadeRepository
    {
        private readonly TestDbContext _dbContext;
        public CidadeRepository(TestDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Cidade> List()
        {
            return _dbContext.Cidade.AsQueryable();
        }

        public async Task<IEnumerable<Cidade>> ListByIdsCidades(IEnumerable<Guid> idsCidades)
        {
            List<Guid> guids = idsCidades.ToList();
            var cidades = await _dbContext.Cidade.Where(c => guids.Contains(c.Id)).AsNoTracking().ToListAsync();
            return cidades;
        }

        public IEnumerable<Cidade> ListByUf(string uf)
        {
            return _dbContext.Cidade.Where(w => EF.Functions.Like(w.UF, $"%{uf}%"));
        }

        public IEnumerable<Cidade> Query(string terms)
        {

            return _dbContext.Cidade.Where(w => EF.Functions.Like(w.Nome, $"%{terms}%") || EF.Functions.Like(w.UF, $"%{terms}%"));
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

    }
}
