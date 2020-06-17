using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultaTipoConta
{

    public class VerificarTipoConta
    {

        private IServicoConsultaTipoConta _servicoConsultaTipoConta;
        public VerificarTipoConta(IServicoConsultaTipoConta servicoConsultaTipoConta)
        {
            _servicoConsultaTipoConta = servicoConsultaTipoConta;
        }

        public TiposConta ConsultarTipoConta(Guid id)
        {
            try
            {
                var tipos = _servicoConsultaTipoConta.ConsultarTipoContaPorId(id);

                if (tipos == null)
                {
                    return TiposConta.ContaInvalida;
                }
                else
                {
                    if (tipos.Count == 0)
                        return TiposConta.ContaInvestimento;
                    else
                        return TiposConta.ContaCorrente;
                }
               
            }
            catch (Exception e)
            {
                return TiposConta.ContaConjunta;
            }
        }

    }
}
