using System;
using System.Collections.Generic;
using System.Linq;

namespace Curso_TDD_LeilaoOnline.Core
{
    public enum EstadoLeilao
    {
        LeilaoAntesDoPregao,
        LeilaoEmAndamento,
        LeilaoFinalizado
    }
    public class Leilao
    {
        public Leilao(string peca, IModalidadeAvaliacao modalidadeAvaliacaoLeilao)
        {
            Peca = peca;
            _modalidadeAvaliacaoLeilao = modalidadeAvaliacaoLeilao;
            _lances = new List<Lance>();
            Estado = EstadoLeilao.LeilaoAntesDoPregao;
        }

        private IModalidadeAvaliacao _modalidadeAvaliacaoLeilao;
        private Interessada _ultimoCliente;
        private IList<Lance> _lances;

        public IEnumerable<Lance> Lances => _lances;
        public string Peca { get; }
        public Lance Ganhador { get; private set; }
        public EstadoLeilao Estado { get; private set; }

        public void IniciaPregao()
        {
            Estado = EstadoLeilao.LeilaoEmAndamento;
        }

        private bool NovoLanceEhAceito(Interessada cliente, double valor)
        {
            return (Estado == EstadoLeilao.LeilaoEmAndamento)
                && (cliente != _ultimoCliente);
        }

        public void RecebeLance(Interessada cliente, double valor)
        {
            if (NovoLanceEhAceito(cliente, valor))
            {
                _lances.Add(new Lance(cliente, valor));
                _ultimoCliente = cliente;
            }
        }

        public void TerminaPregao()
        {
            if (Estado != EstadoLeilao.LeilaoEmAndamento)
            {
                throw new InvalidOperationException("Não é possível terminar o pregão sem que ele tenha começado. Utilize o método IniciarPregao().");
            }

            Ganhador = _modalidadeAvaliacaoLeilao.Avalia(this);
            Estado = EstadoLeilao.LeilaoFinalizado;
        }
    }
}
