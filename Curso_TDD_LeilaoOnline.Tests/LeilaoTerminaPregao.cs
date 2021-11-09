using Curso_TDD_LeilaoOnline.Core;
using System;
using Xunit;

namespace Curso_TDD_LeilaoOnline.Tests
{
    public class LeilaoTerminaPregao
    {
        [Theory]
        [InlineData(1200, 1250, new double[] { 800, 1150, 1400, 1250 })]
        public void RetornaValorSuperiorMaisProximoDadoLeilaoNessaModalidade(double valorDestinado, double valorEsperado, double[] ofertas)
        {
            //Arranje
            var leilao = new Leilao("Cristaleira", valorDestinado);
            var pessoaInteressada1 = new Interessada("Fabiana", leilao);
            var pessoaInteressada2 = new Interessada("Geovana", leilao);

            leilao.IniciaPregao();

            for (int i = 0; i < ofertas.Length; i++)
            {
                var valor = ofertas[i];
                if ((i % 2) == 0)
                {
                    leilao.RecebeLance(pessoaInteressada1, valor);
                }
                else
                {
                    leilao.RecebeLance(pessoaInteressada2, valor);
                }
            }

            //Act
            leilao.TerminaPregao();

            //Assert
            Assert.Equal(valorEsperado, leilao.Ganhador.Valor);
        }

        [Theory] //Cria um teste para v�rias condi��es de entrada
        [InlineData(1000, new double[] { 800, 900, 1000, 980 })]
        [InlineData(1210, new double[] { 800, 900, 1200, 1210 })]
        [InlineData(800, new double[] { 800 })]
        public void RetornaMaiorValorDadoLeilaoComPeloMenosUmLance(double valorEsperado, double[] ofertas)
        {
            //Arranje
            var leilao = new Leilao("Espelho");
            var pessoaInteressada1 = new Interessada("Marcelo", leilao);
            var pessoaInteressada2 = new Interessada("Maria", leilao);

            leilao.IniciaPregao();

            for (int i = 0; i < ofertas.Length; i++)
            {
                var valor = ofertas[i];
                if ((i % 2) == 0)
                {
                    leilao.RecebeLance(pessoaInteressada1, valor);
                }
                else
                {
                    leilao.RecebeLance(pessoaInteressada2, valor);
                }
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
            var leilao = new Leilao("Televis�o Vintage");
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
            //Arrange - cen�rio
            var leilao = new Leilao("Sof� Medieval");
            var pessoaInteressada1 = new Interessada("Ana Carolina", leilao);
            var pessoaInteressada2 = new Interessada("Maria", leilao);

            leilao.IniciaPregao();

            leilao.RecebeLance(pessoaInteressada1, 800);
            leilao.RecebeLance(pessoaInteressada2, 1300);
            leilao.RecebeLance(pessoaInteressada1, 1400);
            leilao.RecebeLance(pessoaInteressada2, 1200);

            //Act - m�todo sob teste
            leilao.TerminaPregao();

            //Assert - verifica��o das expectativas
            var valorEsperado = 1400;
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);
        }

        [Fact]
        public void RetornaMaiorValorDadoLeilaoComApenasUmLance()
        {
            //Arrange
            var leilao = new Leilao("Sof� Medieval");
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
        public void LancaInvadidOperationExceptionDadoPregaoNaoIniciado()
        {
            //Arrange
            var leilao = new Leilao("Sof� Medieval");
            
            //Assert
            var exceptionObtida = Assert.Throws<InvalidOperationException>(
                //Act - ser� um delegate
                () => leilao.TerminaPregao());

            var mensagemEsperada = "N�o � poss�vel terminar o preg�o sem que ele tenha come�ado. Utilize o m�todo IniciarPregao().";
            Assert.Equal(mensagemEsperada, exceptionObtida.Message);
        }

        [Fact]
        public void RetornaZeroDadoLeilaoSemLances()
        {
            //Arrange
            var leilao = new Leilao("Sof� Medieval");

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
