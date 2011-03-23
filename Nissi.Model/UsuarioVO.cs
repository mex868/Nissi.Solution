using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nissi.Model
{
    public class UsuarioVO
    {
        public UsuarioVO()
        {
            PerfilAcessos = new List<PerfilAcessoVO>();
        }
        public FuncionarioVO Funcionario { get; set; }
        public IList<PerfilAcessoVO> PerfilAcessos { get; set; }
    }
}
