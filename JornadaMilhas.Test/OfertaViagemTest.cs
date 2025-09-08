using JornadaMilhasV1.Modelos;

namespace JornadaMilhas.Test
{
    public class OfertaViagemConstructor
    {
        [Theory]
        [InlineData("", null, "2025-10-06", "2025-10-16", 0, false)]
        [InlineData("São Paulo", "Rio de Janeiro", "2025-10-16", "2025-10-06", 100.00, false)]
        [InlineData("São Paulo", "Rio de Janeiro", "2025-10-06", "2025-10-16", -100.00, false)]
        [InlineData("São Paulo", "Rio de Janeiro", "2025-10-06", "2025-10-16", 100.00, true)]
        public void RetornaSeEhValidoDeAcordoComOsDadosDeEntrada(string origem, string destino, string dataIda, string dataVolta, double preco, bool validacao)
        {
            Rota rota = new(origem, destino);

            Periodo periodo = new(DateTime.Parse(dataIda), DateTime.Parse(dataVolta));

            OfertaViagem oferta = new(rota, periodo, preco);

            Assert.Equal(validacao, oferta.EhValido);
        }

        [Fact]
        public void RetornaOfertaInvalidaEMensagemDeErroQuandoRotaNula()
        {
            Rota rota = null;
            Periodo periodo = new(new DateTime(2025, 10, 6), new DateTime(2025, 10, 10));
            double preco = 100.00;
            OfertaViagem oferta = new(rota, periodo, preco);

            Assert.Contains("A oferta de viagem não possui rota ou período válidos.", oferta.Erros.Sumario);
            Assert.False(oferta.EhValido);
        }

        [Fact]
        public void RetornaOfertaInvalidaEMensagemDeErroQuandoDataIdaMenorQueDataVolta()
        {
            Rota rota = new("São Paulo", "Rio de Janeiro");
            Periodo periodo = new(new DateTime(2025, 10, 10), new DateTime(2025, 10, 6));
            double preco = 100.00;
            OfertaViagem oferta = new(rota, periodo, preco);
            Assert.Contains("Data de ida não pode ser maior que a data de volta.", oferta.Erros.Sumario);
            Assert.False(oferta.EhValido);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-100.00)]
        public void RetornaOfertaInvalidaEMensagemDeErroQuandoPrecoMenorOuIgualZero(double preco)
        {
            // Arrange
            Rota rota = new("São Paulo", "Rio de Janeiro");
            Periodo periodo = new(new DateTime(2025, 10, 6), new DateTime(2025, 10, 10));
            // Act
            OfertaViagem oferta = new(rota, periodo, preco);
            // Assert
            Assert.Contains("O preço da oferta de viagem deve ser maior que zero.", oferta.Erros.Sumario);
            Assert.False(oferta.EhValido);
        }
    }
}