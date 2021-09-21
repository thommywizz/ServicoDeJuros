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
                if (valorInicial <= 0)
                    throw new ArgumentException("Valor inserido não é válido, valorInicial necessita ser maior que zero", "valorInicial");

                if(meses <= 0)
                    throw new ArgumentException("Valor inserido não é válido, o número de meses necessita ser maior que zero", "meses");

                var juros = await _client.ObterTaxaDeJuros();

                return Math.Truncate(valorInicial * Math.Pow(1 + juros, meses) * 100) / 100;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);/* inserir no log */
                throw; /* propagar o stack trace, ou substituir por throw ex para limpar o stack trace */
            }

        }
    }
}
