using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nissi.Model
{
    public class EmailEmitenteVO
    {
        public int CodEmailEmitente { get; set; }

        public TypePedido Tipo { get; set; }

        public string Email { get; set; }

        public DateTime? DataCadastro { get; set; }

        public int? UsuarioInc { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public int? UsuarioAlt { get; set; }
    }
}
