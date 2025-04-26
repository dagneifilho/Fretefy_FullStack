using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Interfaces;
using Fretefy.Test.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fretefy.Test.Domain.Services
{
    public class CidadeService : ICidadeService
    {
        private readonly ICidadeRepository _cidadeRepository;
        private readonly IRegiaoCidadeRepository _regiaoCidadeRepository;

        public CidadeService(ICidadeRepository cidadeRepository, IRegiaoCidadeRepository regiaoCidadeRepository)
        {
            _cidadeRepository = cidadeRepository;
            _regiaoCidadeRepository = regiaoCidadeRepository;

        }

        public Cidade Get(Guid id)
        {
            return _cidadeRepository.List().FirstOrDefault(f => f.Id == id);
        }

        public async Task<IEnumerable<Cidade>> GetDisponiveisAsync()
        {
            var regiaoCidades = await _regiaoCidadeRepository.ListAsync();
            var idsRegiaoCidades = regiaoCidades.Select(rc => rc.CidadeId).ToList();

            var cidades = _cidadeRepository.List();       

            cidades = cidades.Where(c => !idsRegiaoCidades.Contains(c.Id));

            return cidades;
        }

        public IEnumerable<Cidade> List()
        {
            return _cidadeRepository.List();
        }

        public IEnumerable<Cidade> ListByUf(string uf)
        {
            return _cidadeRepository.ListByUf(uf);
        }

        public IEnumerable<Cidade> Query(string terms)
        {
            return _cidadeRepository.Query(terms);
        }
    }
}
