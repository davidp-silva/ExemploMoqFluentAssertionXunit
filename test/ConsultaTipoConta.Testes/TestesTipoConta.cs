using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace ConsultaTipoConta.Testes
{
    public class TestesTipoConta
    {
        private Mock<IServicoConsultaTipoConta> mock;

        private Guid contaCorrente =  Guid.Parse("8a4ad8a8-e7f3-4797-8c50-2d94d8d59fce");
        private Guid contaPouPança = Guid.Parse("8eaf21b7-c783-4e41-b9aa-5311d75b2f5d");
        private Guid contaInvestimento = Guid.Parse("16819af8-e7b1-4722-8479-a18c24867265");

        
        public TestesTipoConta()
        {
            mock = new Mock<IServicoConsultaTipoConta>(MockBehavior.Strict);

            var cliente = new Cliente()
            {
                Id = contaCorrente,
                IdAgencia = 25,
                Nome = "Henrique"
            };

            var cliente2 = new Cliente()
            {
                Id = contaPouPança,
                Nome = "Jaime",
                IdAgencia = 45
            };

            var cliente3 = new Cliente()
            {
                Id = contaInvestimento,
                Nome = "David",
                IdAgencia = 30
            };

            var clientes = new List<Cliente>
            {
                cliente,
                cliente2,
                cliente3
            };
            mock.Setup(s => s.ConsultarTipoContaPorId(contaCorrente))
                .Returns(() => clientes);

            mock.Setup(s => s.ConsultarTipoContaPorId(contaPouPança));
            mock.Setup(s => s.ConsultarTipoContaPorId(contaInvestimento));
        }

        
        private TiposConta ConsultarTipoConta(Guid id)
        {
            VerificarTipoConta verifica = new VerificarTipoConta(mock.Object);
            return verifica.ConsultarTipoConta(id);
        }
        [Fact]
        public void TestarContaCorrente()
        {
            TiposConta verifica =
                ConsultarTipoConta(contaCorrente);
            verifica.Should().Be(TiposConta.ContaCorrente, "Resultado Incorreto para Conta Corrente");
        }

        [Fact]
        public void TestarContaPoupanca()
        {
            TiposConta verifica =
                ConsultarTipoConta(contaPouPança);
            verifica.Should().Be(TiposConta.ContaPouPança, "Resultado Incorreto para Conta Poupança");
        }

        [Fact]
        public void TestarContaInvestimento()
        {
            TiposConta verifica = ConsultarTipoConta(contaInvestimento);
            verifica.Should().Be(TiposConta.ContaPouPança, "Resultado Incorreto para Conta Investimento");
        }
    }
}
