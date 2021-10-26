using Curso_TDD_LeilaoOnline.Core;
using Xunit;

namespace Curso_TDD_LeilaoOnline.Tests
{
    public class LeilaoTerminaPregao
    {
        [Theory] //Cria um teste para várias condições de entrada
        [InlineData(1000, new double[] { 800, 900, 1000, 980 })]
        [InlineData(1210, new double[] { 800, 900, 1200, 1210 })]
        [InlineData(800, new double[] { 800 })]
        public void RetornaMaiorValorDadoLeilaoComPeloMenosUmLance(double valorEsperado, double[] valoresDeEntrada)
        {
            //Arranje
            var leilao = new Leilao("Espelho");
            var pessoaInteressada1 = new Interessada("Marcelo", leilao);

            leilao.IniciaPregao();

            foreach (var valor in valoresDeEntrada)
            {
                leilao.RecebeLance(pessoaInteressada1, valor);
            }

            //Act
            leilao.TerminaPregao();

            //Assert
            var valorObtido = leilao.Ganhador.Valor;
            Assert.Equal(valorEsperado, valorObtido);
        }

        [Fact] //Testa algo sem depender de valores de entrada
        public void RetornaMaiorValorDadoLeilaoComLancesEmOrdemOrdenada()
        {
            //Arrange
            var leilao = new Leilao("Televisão Vintage");
            var pessoaInteressada1 = new Interessada("Ana Carolina", leilao);
            var pessoaInteressada2 = new Interessada("Maria", leilao);

            leilao.IniciaPregao();

            leilao.RecebeLance(pessoaInteressada1, 800);
            leilao.RecebeLance(pessoaInteressada2, 900);
            leilao.RecebeLance(pessoaInteressada1, 1000);

            //Act
            leilao.TerminaPregao();

            //Assert
            var valorEsperado = 1000;
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);
        }

        [Fact]
        public void RetornaMaiorValorDadoLeilaoComLancesEmOrdemDesordenada()
        {
            //Arrange - cenário
            var leilao = new Leilao("Sofá Medieval");
            var pessoaInteressada1 = new Interessada("Ana Carolina", leilao);
            var pessoaInteressada2 = new Interessada("Maria", leilao);

            leilao.IniciaPregao();

            leilao.RecebeLance(pessoaInteressada1, 800);
            leilao.RecebeLance(pessoaInteressada2, 1300);
            leilao.RecebeLance(pessoaInteressada1, 1400);
            leilao.RecebeLance(pessoaInteressada2, 1200);

            //Act - método sob teste
            leilao.TerminaPregao();

            //Assert - verificação das expectativas
            var valorEsperado = 1400;
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);
        }

        [Fact]
        public void RetornaMaiorValorDadoLeilaoComApenasUmLance()
        {
            //Arrange
            var leilao = new Leilao("Sofá Medieval");
            var pessoaInteressada1 = new Interessada("Ana Carolina", leilao);

            leilao.IniciaPregao();

            leilao.RecebeLance(pessoaInteressada1, 800);

            //Act
            leilao.TerminaPregao();

            //Assert
            var valorEsperado = 800;
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);
        }

        [Fact]
        public void RetornaZeroDadoLeilaoSemLances()
        {
            //Arrange
            var leilao = new Leilao("Sofá Medieval");

            leilao.IniciaPregao();

            //Act
            leilao.TerminaPregao();

            //Assert
            var valorEsperado = 0;
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);
        }
    }
}
