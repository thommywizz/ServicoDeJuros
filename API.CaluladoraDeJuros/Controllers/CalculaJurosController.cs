using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Globalization;
using API.CaluladoraDeJuros.Services;
using Microsoft.Extensions.Options;

namespace API.CaluladoraDeJuros.Controllers
{
    [ApiController]
    public class CalculaJurosController : ControllerBase
    {
        private readonly ICalculaJurosService _calculaJurosService;
        private readonly ServiceSettings _settings;

        public CalculaJurosController(ICalculaJurosService calculaJurosService, IOptions<ServiceSettings> options)
        {
            _calculaJurosService = calculaJurosService;
            _settings = options.Value;
        }
        [HttpGet]
        [Route("calculajuros")]
        public async Task<double> Get(int valorinicial, int meses)
        {
            if (valorinicial == 0 || meses == 0)
            {
                Response.StatusCode = 422;
                return 0.0;
            }
            else
            {

                return await _calculaJurosService.ValorFuturo(valorinicial, meses);
            }
        }

        [HttpGet]
        [Route("showmethecode")]
        public string GetCode() => _settings.GitHubUrl;
    }
}
