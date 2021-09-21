using API.CaluladoraDeJuros;
using API.CaluladoraDeJuros.Clients;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Test.API.CalculadoraDeJuros
{
    public class TaxaDeJurosClientTest
    {
        private readonly TaxaDeJurosClient _sut;
        private readonly Mock<IHttpClientFactory> _taxaDeJurosMock = new Mock<IHttpClientFactory>();
        private readonly Mock<IOptions<ServiceSettings>> _settingsMock = new Mock<IOptions<ServiceSettings>>();

        public TaxaDeJurosClientTest()
        {
            _sut = new TaxaDeJurosClient(_taxaDeJurosMock.Object,_settingsMock.Object);
        }

        [Fact]
        public async Task ObterTaxaDeJuros_DeveRetornarOValorDeJuros()
        {
            var client = new HttpClient();
            _taxaDeJurosMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);


            var juros = await _sut.ObterTaxaDeJuros();
        }
    }
}
