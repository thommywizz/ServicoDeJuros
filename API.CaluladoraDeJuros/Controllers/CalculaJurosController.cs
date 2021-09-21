using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Globalization;
using API.CaluladoraDeJuros.Services;

namespace API.CaluladoraDeJuros.Controllers
{
    [Route("calculajuros")]
    [ApiController]
    public class CalculaJurosController : ControllerBase
    {
        private readonly ICalculaJurosService _calculaJurosService;

        public CalculaJurosController(ICalculaJurosService calculaJurosService)
        {
            _calculaJurosService = calculaJurosService;
        }
        [HttpGet]
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
    }
}
