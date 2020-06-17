using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultaTipoConta
{
    public class Cliente
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public int IdAgencia { get; set; }
    }
}
