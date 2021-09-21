using API.CaluladoraDeJuros.Clients;
using API.CaluladoraDeJuros.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
namespace Test.API.CalculadoraDeJuros
{
    public class CalculaJurosServiceTest
    {
        private readonly CalculaJurosService _sut;
        private readonly Mock<ITaxaDeJurosClient> _taxaDeJurosMock = new Mock<ITaxaDeJurosClient>();

        public CalculaJurosServiceTest()
        {
            _sut = new CalculaJurosService(_taxaDeJurosMock.Object);
        }

        [Fact]
        public async Task ValorFuturo_DeveraRetornarOCalculoCorreto_QuandoOsParametrosForemInteirosPositivos()
        {
            _taxaDeJurosMock.Setup(x => x.ObterTaxaDeJuros()).ReturnsAsync(0.01);

            var resultado = await _sut.ValorFuturo(100, 5);

            Assert.Equal(105.1, resultado);
        }
    }
}
