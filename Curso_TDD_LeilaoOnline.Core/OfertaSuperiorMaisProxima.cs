using System.Linq;

namespace Curso_TDD_LeilaoOnline.Core
{
    public class OfertaSuperiorMaisProxima : IModalidadeAvaliacao
    {
        public OfertaSuperiorMaisProxima(double valorDestinado)
        {
            ValorDestinado = valorDestinado;
        }

        public double ValorDestinado { get; }

        public Lance Avalia(Leilao leilao)
        {
            return leilao.Lances
                .DefaultIfEmpty(new Lance(null, 0))
                .Where(l => l.Valor > ValorDestinado)
                .OrderBy(l => l.Valor)
                .FirstOrDefault();
        }
    }
}
