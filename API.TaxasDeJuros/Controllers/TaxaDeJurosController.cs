using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.TaxasDeJuros.Controllers
{
    [Route("taxaJuros")]
    [ApiController]
    public class TaxaDeJurosController : ControllerBase
    {
        [HttpGet]
        public double Get() => 0.01;
    }
}
