using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Interfaces;
using Fretefy.Test.Domain.Interfaces.Repositories;
using Fretefy.Test.Domain.ViewModels.Response;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IRegiaoRepository _regiaoRepository;

        public CidadeService(ICidadeRepository cidadeRepository, IRegiaoCidadeRepository regiaoCidadeRepository, IRegiaoRepository regiaoRepository)
        {
            _cidadeRepository = cidadeRepository;
            _regiaoCidadeRepository = regiaoCidadeRepository;
            _regiaoRepository = regiaoRepository;

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
        
        public IEnumerable<Cidade> Query(string terms)
        {
            return _cidadeRepository.Query(terms);
        }

        public async Task<IEnumerable<CidadeDetailedViewModel>> GetReportAsync()
        {
            var regioesCidades= await _regiaoCidadeRepository.ListAsync();     
            var cidades = _cidadeRepository.List(); 
            var rtn = new List<CidadeDetailedViewModel>();
            foreach(var regiaoCidade in regioesCidades)
            {
                var cidade = cidades.FirstOrDefault(c => c.Id == regiaoCidade.CidadeId);
                var regiao = await _regiaoRepository.GetByIdAsync(regiaoCidade.RegiaoId);

                rtn.Add(new CidadeDetailedViewModel(cidade.Nome, cidade.UF, regiao.Nome ?? string.Empty));
            }

            rtn.GroupBy(c => c.NomeRegiao);

            return rtn;
        }

        public IEnumerable<Cidade> List()
        {
            return _cidadeRepository.List();
        }

        public IEnumerable<Cidade> ListByUf(string uf)
        {
            return _cidadeRepository.ListByUf(uf);
        }

        protected virtual void Dispose(bool disposing)
        {
            _cidadeRepository.Dispose();
            _regiaoCidadeRepository.Dispose();
            _regiaoRepository.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
