﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ExemploAPIMiddleware.Models;

namespace ExemploAPIMiddleware.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContadorController : ControllerBase
    {
        private static readonly Contador _CONTADOR = new ();
        private readonly ILogger<ContadorController> _logger;
        private readonly IConfiguration _configuration;

        public ContadorController(ILogger<ContadorController> logger,
            IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        [MiddlewareFilter(typeof(IndisponibilidadePipeline))]
        public ResultadoContador Get()
        {
            return GetResultadoContador();
        }

        [HttpGet("semfilter")]
        public ResultadoContador GetSemFilter()
        {
            return GetResultadoContador();
        }

        private ResultadoContador GetResultadoContador()
        {
            lock (_CONTADOR)
            {
                _CONTADOR.Incrementar();
                _logger.LogInformation($"Contador - Valor atual: {_CONTADOR.ValorAtual}");

                return new()
                {
                    ValorAtual = _CONTADOR.ValorAtual,
                    Local = _CONTADOR.Local,
                    Kernel = _CONTADOR.Kernel,
                    TargetFramework = _CONTADOR.TargetFramework,
                    MensagemFixa = "Teste",
                    MensagemVariavel = _configuration["MensagemVariavel"]
                };
            }
        }
    }
}