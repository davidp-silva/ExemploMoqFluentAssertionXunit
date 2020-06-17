using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultaTipoConta
{
   public  interface IServicoConsultaTipoConta
    {
        IList<Cliente> ConsultarTipoContaPorId(Guid id);
    }
}
