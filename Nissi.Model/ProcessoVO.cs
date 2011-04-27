using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nissi.Model
{
    public class ProcessoVO
    {
        public int CodProcesso { get; set; }
        public string Descricao { get; set; }
        public int UsuarioInc { get; set; }
        public DateTime DataCadastro { get; set; }
        public int UsuarioAlt { get; set; }
        public DateTime DataAlteracao { get; set; }
    }
}
