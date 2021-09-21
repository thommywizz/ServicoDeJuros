using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.CaluladoraDeJuros.Clients
{
    public interface ITaxaDeJurosClient
    {
       Task<double> ObterTaxaDeJuros();
    }
}
