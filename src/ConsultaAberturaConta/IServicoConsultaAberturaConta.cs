using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultaAberturaConta
{
    public interface IServicoConsultaAberturaConta
    {
        IList<Cliente> ConsultarClienteCadastradoPorCPF(string cpf);
    }
}
