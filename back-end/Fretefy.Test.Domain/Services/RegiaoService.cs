using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Exceptions;
using Fretefy.Test.Domain.Interfaces;
using Fretefy.Test.Domain.Interfaces.Repositories;
using Fretefy.Test.Domain.ViewModels.Request;
using Fretefy.Test.Domain.ViewModels.Response;

namespace Fretefy.Test.Domain.Services
{

    public class RegiaoService : IRegiaoService
    {
        private readonly ICidadeRepository _cidadeRepository;
        private readonly IRegiaoRepository _regiaoRepository;
        private readonly IRegiaoCidadeRepository _regiaoCidadeRepository;
        public RegiaoService(ICidadeRepository cidadeRepository,
                                IRegiaoRepository regiaoRepository,
                                IRegiaoCidadeRepository regiaoCidadeRepository)
        {
            _cidadeRepository = cidadeRepository;
            _regiaoRepository = regiaoRepository;
            _regiaoCidadeRepository = regiaoCidadeRepository;
            
        }


        public async  Task<RegiaoViewModel> InsertAsync(RegiaoPostViewModel regiaoPost)
        {
            var cidadesDb = await _cidadeRepository.ListByIdsCidades(regiaoPost.CidadesIds);
        
            var idsCidadesDb = cidadesDb.Select(c => c.Id).ToList();
            if(cidadesDb.Count() < regiaoPost.CidadesIds.Count())
            {
                var cidadesFaltantes = regiaoPost.CidadesIds.Where(c => !idsCidadesDb.Contains(c)).ToList();
                throw new CidadeInexistenteException(cidadesFaltantes, "CidadesIds");
            }

            var regioesCadastradas = await _regiaoCidadeRepository.GetByIdsCidades(regiaoPost.CidadesIds);

            if(regioesCadastradas.Count() > 0)
            {
                var idsCidadesCadastradas = regioesCadastradas.Select(rc => rc.CidadeId);
                throw new CidadesJaCadastradasException(idsCidadesCadastradas, "CidadesIds");
            }


            var regiaoDb = await _regiaoRepository.GetByNomeAsync(regiaoPost.Nome);
            
            if(regiaoDb != null)
                throw new RegiaoExistenteException("Nome");

            var regiao = new Regiao(regiaoPost.Nome);
            
            regiao = await _regiaoRepository.InsertAsync(regiao);

            var regioesCidades = idsCidadesDb.Select(c => new RegiaoCidade(regiao.Id, c)).ToList();
            regioesCidades = (await _regiaoCidadeRepository.InsertManyAsync(regioesCidades)).ToList();

            var regiaoVm = MapViewModel(regiao, cidadesDb);
            return regiaoVm;
        }

        public async Task<IEnumerable<RegiaoListedViewModel>> ListAsync()
        {
            var regioesDb = await _regiaoRepository.ListAsync();
            if(regioesDb.Count() == 0)
                return new List<RegiaoListedViewModel>();

            var regioes = regioesDb.Select(r => new RegiaoListedViewModel(r.Id, r.Nome, r.Ativa)).ToList();

            return regioes;
        }
        
        public async Task<RegiaoViewModel?> GetAsync(Guid id)
        {   
            var regiaoDb = await _regiaoRepository.GetByIdAsync(id);
            if(regiaoDb == null)
                return null;

            var idsCidades = regiaoDb.RegiaoCidades.Select(rc => rc.CidadeId).ToList();
            
            var cidadesDb = await _cidadeRepository.ListByIdsCidades(idsCidades);
            
            return MapViewModel(regiaoDb, cidadesDb);
        }
        
        public async Task<RegiaoViewModel> UpdateAsync(RegiaoUpdateViewModel regiaoUpdate)
        {
            var regiaoDb = await _regiaoRepository.GetByIdAsync(regiaoUpdate.Id, true);
            if(regiaoDb == null)
                throw new RegiaoInexistenteException(regiaoUpdate.Id, "Id");


            var cidadesDb = await _cidadeRepository.ListByIdsCidades(regiaoUpdate.CidadesIds);
        
            var idsCidadesDb = cidadesDb.Select(c => c.Id).ToList();
            if(cidadesDb.Count() < regiaoUpdate.CidadesIds.Count())
            {
                var cidadesFaltantes = regiaoUpdate.CidadesIds.Where(c => !idsCidadesDb.Contains(c)).ToList();
                throw new CidadeInexistenteException(cidadesFaltantes, "CidadesIds");
            }

            var regioesCadastradas = await _regiaoCidadeRepository.GetByIdsCidades(regiaoUpdate.CidadesIds);
            regioesCadastradas = regioesCadastradas.Where(rc => rc.RegiaoId != regiaoUpdate.Id);

            if(regioesCadastradas.Count() > 0)
            {
                var idsCidadesCadastradas = regioesCadastradas.Select(rc => rc.CidadeId);
                throw new CidadesJaCadastradasException(idsCidadesCadastradas, "CidadesIds");
            }


            //delete regioescidades
            await _regiaoCidadeRepository.DeleteByRegiaoIdAsync(regiaoUpdate.Id);


            //update regiao
            regiaoDb.Nome = regiaoUpdate.Nome;
            regiaoDb = await _regiaoRepository.UpdateAsync(regiaoDb);

            //insert regioescidades
            
            var regioesCidades = idsCidadesDb.Select(c => new RegiaoCidade(regiaoDb.Id, c)).ToList();
            regioesCidades = (await _regiaoCidadeRepository.InsertManyAsync(regioesCidades)).ToList();


            return MapViewModel(regiaoDb, cidadesDb);
        }
        
        public async Task<RegiaoViewModel> AlteraStatusAsync(Guid id)
        {
            var regiaoDb = await _regiaoRepository.GetByIdAsync(id, true);
            if(regiaoDb == null)
                throw new RegiaoInexistenteException(id, "Id");
            regiaoDb.Ativa = !regiaoDb.Ativa;
            regiaoDb = await _regiaoRepository.UpdateAsync(regiaoDb);

            var idsCidades = regiaoDb.RegiaoCidades.Select(rc => rc.CidadeId).ToList();
            var cidadesDb = await _cidadeRepository.ListByIdsCidades(idsCidades);

            return MapViewModel(regiaoDb, cidadesDb);
        }   



        private RegiaoViewModel MapViewModel(Regiao regiaoDb, IEnumerable<Cidade> cidadesDb)
        {
            var cidades = cidadesDb.Select(c => new CidadeViewModel(c.Id, c.Nome, c.UF));
            return new RegiaoViewModel(regiaoDb.Id, regiaoDb.Nome, cidades, regiaoDb.Ativa);
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
