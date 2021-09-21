using API.CaluladoraDeJuros.Clients;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace API.CaluladoraDeJuros.Services
{
    public class CalculaJurosService : ICalculaJurosService
    {
        private readonly ITaxaDeJurosClient _client;

        public CalculaJurosService(ITaxaDeJurosClient client)
        {
            _client = client;
        }

        public async Task<double> ValorFuturo(int valorInicial, int meses)
        {
            try
            {
                var juros = await _client.ObterTaxaDeJuros();

                if (juros == 0)
                    throw new ApplicationException($"Valor de juros obtido zero. Juros:{juros}");

                return Math.Truncate(valorInicial * Math.Pow(1 + juros, meses) * 100) / 100;
            }
            catch (Exception ex)
            {
                /* inserir no log */
                throw; /* propagar o stack trace, ou substituir por throw ex para limpar o stack trace */
            }

        }
    }
}
