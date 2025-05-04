using CsvHelper;
using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Fretefy.Test.WebApi.Controllers
{
    [Route("api/cidade")]
    [ApiController]
    public class CidadeController : ControllerBase
    {
        private readonly ICidadeService _cidadeService;

        public CidadeController(ICidadeService cidadeService)
        {
            _cidadeService = cidadeService;
        }

        [HttpGet]
        public IActionResult List([FromQuery] string uf, [FromQuery] string terms)
        {
            IEnumerable<Cidade> cidades;

            if (!string.IsNullOrEmpty(terms))
                cidades = _cidadeService.Query(terms);
            else if (!string.IsNullOrEmpty(uf))
                cidades = _cidadeService.ListByUf(uf);
            else
                cidades = _cidadeService.List();

            return Ok(cidades);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var cidades = _cidadeService.Get(id);
            return Ok(cidades);
        }
        [HttpGet("disponiveis")]
        public async Task<IActionResult> CidadesDisponiveisAsync()
        {
            var cidades = await _cidadeService.GetDisponiveisAsync();
            return Ok(cidades);
        }

        [HttpGet("relatorio")]
        public async Task<IActionResult> Relatorio() 
        {
            var cidades = await _cidadeService.GetReportAsync();
            string nomeArquivo = "cidades"+DateTime.Now.Ticks+".csv";
            using (var ms = new MemoryStream())
            using (var sw = new StreamWriter(ms, Encoding.UTF8))
            using (var csvWriter = new CsvWriter(sw, CultureInfo.InvariantCulture))
            {
                csvWriter.WriteRecords(cidades);
                sw.Flush();

                return File(ms.ToArray(), "text/csv", nomeArquivo);
            }
        }
    }
}
