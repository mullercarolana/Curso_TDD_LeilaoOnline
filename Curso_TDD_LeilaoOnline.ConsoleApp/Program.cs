using Curso_TDD_LeilaoOnline.Core;
using System;

namespace Curso_TDD_LeilaoOnline.ConsoleApp
{
    class Program
    {
        private static void VerificaTeste(double valorEsperado, double valorObtido)
        {
            var cor = Console.ForegroundColor;

            if (valorEsperado == valorObtido)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("TESTE OK!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"TESTE FALHOU! Esperado: {valorEsperado} - obtido: {valorObtido}");
            }

            Console.ForegroundColor = cor;
        }

        private static void LeilaoComVariosLances()
        {
            //Arrange - cenário
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Sofá Medieval", modalidade);
            var pessoaInteressada1 = new Interessada("Ana Carolina", leilao);
            var pessoaInteressada2 = new Interessada("Maria", leilao);

            leilao.RecebeLance(pessoaInteressada1, 800);
            leilao.RecebeLance(pessoaInteressada2, 1300);
            leilao.RecebeLance(pessoaInteressada1, 1400);
            leilao.RecebeLance(pessoaInteressada2, 1200);

            //Act - método sob teste
            leilao.TerminaPregao();

            //Assert - verificação das expectativas
            var valorEsperado = 1400;
            var valorObtido = leilao.Ganhador.Valor;

            VerificaTeste(valorEsperado, valorObtido);
        }

        private static void LeilaoComApenasUmLance()
        {
            //Arrange
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Sofá Medieval", modalidade);
            var pessoaInteressada1 = new Interessada("Ana Carolina", leilao);

            leilao.RecebeLance(pessoaInteressada1, 800);

            //Act
            leilao.TerminaPregao();

            //Assert
            var valorEsperado = 900;
            var valorObtido = leilao.Ganhador.Valor;

            VerificaTeste(valorEsperado, valorObtido);
        }

        static void Main()
        {
            LeilaoComVariosLances();
            LeilaoComApenasUmLance();
        }
    }
}
