using System;
using System.Linq;
using System.Threading.Tasks;
using Fretefy.Test.Domain.Interfaces;
using Fretefy.Test.Domain.ViewModels.Request;
using Fretefy.Test.Domain.ViewModels.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fretefy.Test.WebApi.Controllers
{
    [Route("api/regiao")]
    [ApiController]
    public class RegiaoController : ControllerBase
    {
        private readonly IRegiaoService _regiaoService;
        public RegiaoController (IRegiaoService regiaoService) {
            _regiaoService = regiaoService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateRegiao([FromBody] RegiaoPostViewModel regiaoPost)
        {
            if(!ModelState.IsValid)
                return StatusCode(400, new ErrorViewModel(ModelState));
            
            var regiao = await _regiaoService.InsertAsync(regiaoPost);
            return StatusCode(201, regiao);
        }
        [HttpGet]
        public async Task<IActionResult> ListRegioes()
        {
            var regioes = await _regiaoService.ListAsync();
            if(regioes.Count() == 0)
                return StatusCode(204);
            return StatusCode(200, regioes);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRegiao(Guid id)
        {
            var regiao = await _regiaoService.GetAsync(id);
            if(regiao == null)
                return StatusCode(404);
            return StatusCode(200,regiao);

        } 

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] RegiaoUpdateViewModel regiaoUpdate)
        {
            if(!ModelState.IsValid)
                return StatusCode(400, new ErrorViewModel(ModelState));
            var regiao = await _regiaoService.UpdateAsync(regiaoUpdate);

            return StatusCode(200, regiao);
        }

        [HttpPost("{id}/alterar-status")]
        public async Task<IActionResult> AlterarStatusAsync([FromRoute]Guid id)
        {
            var regiao = await _regiaoService.AlteraStatusAsync(id);
            return StatusCode(200, regiao);
        }
        

    }
    
}
