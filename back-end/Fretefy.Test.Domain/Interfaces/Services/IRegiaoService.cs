using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.ViewModels.Request;
using Fretefy.Test.Domain.ViewModels.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretefy.Test.Domain.Interfaces
{
    public interface IRegiaoService : IDisposable
    {
        Task<RegiaoViewModel> InsertAsync(RegiaoPostViewModel regiaoPost);
        Task<IEnumerable<RegiaoListedViewModel>> ListAsync();
        Task<RegiaoViewModel?> GetAsync(Guid id);
        Task<RegiaoViewModel> UpdateAsync(RegiaoUpdateViewModel regiaoUpdate);   
        Task<RegiaoViewModel> AlteraStatusAsync(Guid id);     
    }
}
