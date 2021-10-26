using Curso_TDD_LeilaoOnline.Core;
using System.Linq;
using Xunit;

namespace Curso_TDD_LeilaoOnline.Tests
{
    public class LeilaoRecebeOferta
    {
        [Theory]
        [InlineData(2, new double[] { 800, 900 })]
        [InlineData(4, new double[] { 1000, 1200, 1600, 2300 })]
        public void NaoPermiteNovosLancesDadoLeilaoFinalizado(int quantidadeEsperada, double[] ofertas)
        {
            //Arrange
            var leilao = new Leilao("Armadura Medieval");
            var pessoaInteressada1 = new Interessada("Lucas", leilao);

            leilao.IniciaPregao();

            foreach (var valor in ofertas)
            {
                leilao.RecebeLance(pessoaInteressada1, valor);
            }

            leilao.TerminaPregao();

            //Act
            leilao.RecebeLance(pessoaInteressada1, 910);
            leilao.RecebeLance(pessoaInteressada1, 2350);

            //Assert
            var quantidadeObtida = leilao.Lances.Count();

            Assert.Equal(quantidadeEsperada, quantidadeObtida);
        }
    }
}
