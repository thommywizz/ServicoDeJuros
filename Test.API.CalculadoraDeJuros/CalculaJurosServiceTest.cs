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

            Assert.Equal(105.1, await _sut.ValorFuturo(100, 5));
        }

        [Fact]
        public async Task ValorFuturo_DeveraRetornarException_QuandoAlgumDosParametrosDeEntradaForNegativo()
        {
            _taxaDeJurosMock.Setup(x => x.ObterTaxaDeJuros()).ReturnsAsync(0.01);

            ArgumentException exception = null;

            exception = null;
            exception = await Assert.ThrowsAsync<ArgumentException>(() => _sut.ValorFuturo(-1, 5));
            Assert.Equal("Valor inserido não é válido, valorInicial necessita ser maior que zero (Parameter 'valorInicial')", exception.Message);

            exception = null;
            exception = await Assert.ThrowsAsync<ArgumentException>(() => _sut.ValorFuturo(100, -1));
            Assert.Equal("Valor inserido não é válido, o número de meses necessita ser maior que zero (Parameter 'meses')", exception.Message);

            exception = null;
            exception = await Assert.ThrowsAsync<ArgumentException>(() => _sut.ValorFuturo(-1, -1));
            Assert.Equal("Valor inserido não é válido, valorInicial necessita ser maior que zero (Parameter 'valorInicial')", exception.Message);
        }

        [Fact]
        public async Task ValorFuturo_DeveraRetornarException_QuandoAlgumDosParametrosDeEntradaForZero()
        {
            _taxaDeJurosMock.Setup(x => x.ObterTaxaDeJuros()).ReturnsAsync(0.01);

            ArgumentException exception = null;

            exception = await Assert.ThrowsAsync<ArgumentException>(() => _sut.ValorFuturo(0, 5));
            Assert.Equal("Valor inserido não é válido, valorInicial necessita ser maior que zero (Parameter 'valorInicial')", exception.Message);


            exception = null;
            exception = await Assert.ThrowsAsync<ArgumentException>(() => _sut.ValorFuturo(100, 0));
            Assert.Equal("Valor inserido não é válido, o número de meses necessita ser maior que zero (Parameter 'meses')", exception.Message);

            exception = null;
            exception = await Assert.ThrowsAsync<ArgumentException>(() => _sut.ValorFuturo(0, 0));
            Assert.Equal("Valor inserido não é válido, valorInicial necessita ser maior que zero (Parameter 'valorInicial')", exception.Message);
        }
    }
}
