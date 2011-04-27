using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nissi.Model
{
    public class TipoMolaVO
    {
        public int CodTipoMola { get; set; }
        public string Descricao { get; set; }
        public byte[] Desenho { get; set; }
        public int UsuarioInc { get; set; }
        public DateTime DataCadastro { get; set; }
        public int UsuarioAlt { get; set; }
        public int DataAlteracao { get; set; }
    }
}
