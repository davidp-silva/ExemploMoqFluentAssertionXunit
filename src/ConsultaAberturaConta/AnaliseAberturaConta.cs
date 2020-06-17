using System;

namespace ConsultaAberturaConta
{
    public class AnaliseAberturaConta
    {
        IServicoConsultaAberturaConta _servicoConsultaAberturaConta;
        public AnaliseAberturaConta(IServicoConsultaAberturaConta servicoConsultaAberturaConta )
        {
            _servicoConsultaAberturaConta = servicoConsultaAberturaConta;
        }

        public StatusConsultaAberturaConta VerificaCpfCadastrado(string cpf)
        {
            try
            {
                var registro = _servicoConsultaAberturaConta.ConsultarClienteCadastradoPorCPF(cpf);

                if (registro == null)
                    return StatusConsultaAberturaConta.CpfInvalido;
                else if (registro.Count == 0)
                    return StatusConsultaAberturaConta.CadastroLiberado;
                else
                    return StatusConsultaAberturaConta.ClienteExistente;
            }
            catch (Exception)
            {
                return StatusConsultaAberturaConta.ErroComunicacao;
            }
        }


    }
}
