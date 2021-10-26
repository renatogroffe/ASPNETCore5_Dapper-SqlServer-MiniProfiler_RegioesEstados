﻿using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using APIRegioes.Data;

namespace APIRegioes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegioesController : ControllerBase
    {
        private readonly ILogger<RegioesController> _logger;
        private readonly RegioesRepository _repository;

        public RegioesController(ILogger<RegioesController> logger,
            RegioesRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<Regiao> GetRegioes()
        {
            var dados = _repository.Get();

            _logger.LogInformation(
                $"{nameof(GetRegioes)}: {dados.Count()} registro(s) encontrado(s)");

            return dados;
        }

        [HttpGet("{codRegiao}/Estados")]
        [ProducesResponseType(typeof(Regiao), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public ActionResult<Regiao> GetEstadosPorRegiao(string codRegiao)
        {
            var dados = _repository.Get(codRegiao)
                .SingleOrDefault();
            _logger.LogInformation(
                $"{nameof(GetEstadosPorRegiao)}: {dados?.Estados?.Count() ?? 0} registro(s) encontrado(s)");
 
            if (dados is null)
                return NotFound();
            
            return dados;
        }
    }
}