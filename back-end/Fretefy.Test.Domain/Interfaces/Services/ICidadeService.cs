using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.ViewModels.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretefy.Test.Domain.Interfaces
{
    public interface ICidadeService : IDisposable
    {
        Cidade Get(Guid id);
        IEnumerable<Cidade> List();
        IEnumerable<Cidade> ListByUf(string uf);
        IEnumerable<Cidade> Query(string terms);
        Task<IEnumerable<Cidade>> GetDisponiveisAsync();
        Task<IEnumerable<CidadeDetailedViewModel>> GetReportAsync();
    }
}
