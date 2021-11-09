using System;

namespace Curso_TDD_LeilaoOnline.Core
{
    public class Lance
    {
        public Lance(Interessada cliente, double valor)
        {
            if (valor < 0)
                throw new ArgumentException("Valor do lance deve ser igual ou maior que zero.");

            Cliente = cliente;
            Valor = valor;
        }

        public Interessada Cliente { get; }
        public double Valor { get; }
    }
}
