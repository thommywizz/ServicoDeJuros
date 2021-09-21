using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace API.CaluladoraDeJuros.Clients
{
    public class TaxaDeJurosClient : ITaxaDeJurosClient
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ServiceSettings _settings;

        public TaxaDeJurosClient(IHttpClientFactory clientFactory, IOptions<ServiceSettings> options)
        {
            _clientFactory = clientFactory;
            _settings = options.Value;
        }
        public async Task<double> ObterTaxaDeJuros()
        {
            try
            {
                var apiUri = _settings.TaxaDeJurosUri;

                if (!Uri.TryCreate(apiUri, UriKind.Absolute, out Uri uriResult)
                       && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
                    throw new ApplicationException("Url da api não é válida");

                if (String.IsNullOrWhiteSpace(apiUri))
                    throw new ApplicationException($"url do serviço TaxaDeJuros inválido. url: {apiUri}");

                using var client = _clientFactory.CreateClient();

                var response = await client.GetAsync(apiUri);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    throw new ApplicationException($"Retorno da API({uriResult.AbsoluteUri}) não foi o esperado. Retorno:{response.StatusCode}");

                if (!Double.TryParse(await response.Content.ReadAsStringAsync(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out double valor))
                    throw new ApplicationException($"O Valor obtido na chamada da API({uriResult.AbsoluteUri}) não foi passível de conversão para double. Valor:{valor}");

                return valor;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); /* inserir a exception no log */
                throw; /* propagar o stack trace, ou substituir por throw ex para limpar o stack trace */
            }
        }
    }
}
