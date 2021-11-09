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

        [Theory] //Cria um teste para várias condições de entrada
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
        public void LancaInvadidOperationExceptionDadoPregaoNaoIniciado()
        {
            //Arrange
            var leilao = new Leilao("Sofá Medieval");
            
            //Assert
            var exceptionObtida = Assert.Throws<InvalidOperationException>(
                //Act - será um delegate
                () => leilao.TerminaPregao());

            var mensagemEsperada = "Não é possível terminar o pregão sem que ele tenha começado. Utilize o método IniciarPregao().";
            Assert.Equal(mensagemEsperada, exceptionObtida.Message);
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
