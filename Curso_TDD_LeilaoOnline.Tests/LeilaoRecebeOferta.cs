using Curso_TDD_LeilaoOnline.Core;
using System.Linq;
using Xunit;

namespace Curso_TDD_LeilaoOnline.Tests
{
    public class LeilaoRecebeOferta
    {
        [Fact]
        public void NaoAceitaProximoLancheDadoMesmoClienteRealizouUltimoLance()
        {
            //Arrange
            var leilao = new Leilao("Escudo Mediaval");
            var pessoaInteressada1 = new Interessada("Samuel", leilao);
            leilao.IniciaPregao();
            leilao.RecebeLance(pessoaInteressada1, 900);

            //Act
            leilao.RecebeLance(pessoaInteressada1, 1000);

            //Assert
            var quantidadeEsperada = 1;
            var quantidadeObtida = leilao.Lances.Count();
            Assert.Equal(quantidadeEsperada, quantidadeObtida);
        }

        [Theory]
        [InlineData(2, new double[] { 800, 900 })]
        [InlineData(4, new double[] { 1000, 1200, 1600, 2300 })]
        public void NaoPermiteNovosLancesDadoLeilaoFinalizado(int quantidadeEsperada, double[] ofertas)
        {
            //Arrange
            var leilao = new Leilao("Armadura Medieval");
            var pessoaInteressada1 = new Interessada("Lucas", leilao);
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

            leilao.TerminaPregao();

            //Act
            leilao.RecebeLance(pessoaInteressada1, 2350);

            //Assert
            var quantidadeObtida = leilao.Lances.Count();
            Assert.Equal(quantidadeEsperada, quantidadeObtida);
        }
    }
}
