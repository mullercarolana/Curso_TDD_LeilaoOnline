using Curso_TDD_LeilaoOnline.Core;
using System;
using Xunit;

namespace Curso_TDD_LeilaoOnline.Tests
{
    public class LanceConstrutor
    {
        [Fact]
        public void LancaArgumentExceptionDadoValorNegativo()
        {
            //Arranje
            var valorNegativo = -100;

            //Assert
            Assert.Throws<ArgumentException>(
                //Act
                () => new Lance(null, valorNegativo));
        }
    }
}
