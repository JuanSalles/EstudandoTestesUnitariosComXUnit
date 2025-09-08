using JornadaMilhasV1.Modelos;

namespace JornadaMilhas.Test
{
    public class OfertaViagemConstructor
    {
        [Fact]
        public void RetornaOfertaValidaQuandoDadosValidos()
        {
            Rota rota = new("São Paulo", "Rio de Janeiro");

            Periodo periodo = new(new DateTime(2025, 10, 6), new DateTime(2025, 10, 10));

            double preco = 100.00;

            OfertaViagem oferta = new(rota, periodo, preco);

            Assert.True(oferta.EhValido);
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

        [Fact]
        public void RetornaOfertaInvalidaEMensagemDeErroQuandoPrecoNegativo()
        {
            // Arrange
            Rota rota = new("São Paulo", "Rio de Janeiro");
            Periodo periodo = new(new DateTime(2025, 10, 6), new DateTime(2025, 10, 10));
            double preco = -100.00;
            // Act
            OfertaViagem oferta = new(rota, periodo, preco);
            // Assert
            Assert.Contains("O preço da oferta de viagem deve ser maior que zero.", oferta.Erros.Sumario);
            Assert.False(oferta.EhValido);
        }
    }
}