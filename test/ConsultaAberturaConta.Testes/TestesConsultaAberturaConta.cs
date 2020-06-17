using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace ConsultaAberturaConta.Testes
{
    public class TestesConsultaAberturaConta
    {

        private const string CPF_INVALIDO = "13548B";
        private const string CPF_ERRO_COMUNICACAO = "7955421";
        private const string CPF_VALIDO = "08888888888";
        private const string CPF_EXISTENTE = "233654";

        private Mock<IServicoConsultaAberturaConta> mock;
        public TestesConsultaAberturaConta()
        {
            mock = new Mock<IServicoConsultaAberturaConta>(MockBehavior.Strict);

            mock.Setup(s => s.ConsultarClienteCadastradoPorCPF(CPF_INVALIDO))
                .Returns(() => null);
            mock.Setup(s => s.ConsultarClienteCadastradoPorCPF(CPF_ERRO_COMUNICACAO))
                .Throws(new Exception("Testando erro de comunnicação"));

            mock.Setup(s => s.ConsultarClienteCadastradoPorCPF(CPF_VALIDO))
                .Returns(() => new List<Cliente>());

            Cliente cliente = new Cliente();
            cliente.CPF = "13546";
            cliente.Nome = "Jaime";
            List<Cliente> clientes = new List<Cliente>();
            clientes.Add(cliente);
            mock.Setup(s => s.ConsultarClienteCadastradoPorCPF(CPF_EXISTENTE))
                .Returns(() => clientes);
        }

        public StatusConsultaAberturaConta ObterStatusAberturaConta(string cpf)
        {
            AnaliseAberturaConta analise = new AnaliseAberturaConta(mock.Object);
            return analise.VerificaCpfCadastrado(cpf);
        }
        [Fact]
        public void TestarCpfInvalidoMoq()
        {
            StatusConsultaAberturaConta status = ObterStatusAberturaConta(CPF_INVALIDO);
            status.Should().Be(StatusConsultaAberturaConta.CpfInvalido,
                "Resultado incorreto para um CPF inválido. ");
        }
        [Fact]
        public void TestarErroComunicacaoMoq()
        {
            StatusConsultaAberturaConta status = ObterStatusAberturaConta(CPF_ERRO_COMUNICACAO);
            status.Should().Be(StatusConsultaAberturaConta.ErroComunicacao,
                "Resultado incorreto para um erro de comunicação. ");
        }
        [Fact]
        public void TestarCpfValidoMoq()
        {
            StatusConsultaAberturaConta status = ObterStatusAberturaConta(CPF_VALIDO);
            status.Should().Be(StatusConsultaAberturaConta.CadastroLiberado,
                "Resultado incorreto para um CPF valido. ");
        }
        [Fact]
        public void TestarCpfExistente()
        {
            StatusConsultaAberturaConta status = ObterStatusAberturaConta(CPF_EXISTENTE);
            status.Should().Be(StatusConsultaAberturaConta.ClienteExistente,
                "Resultado incorreto para um CPF já cadastrado. ");
        }

    }
}
