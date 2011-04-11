using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nissi.Model
{
    [Serializable()] //deve ser serializavel para armazenar em viewstate
    public class ClienteVO : PessoaVO
    {
        #region Campos
        private FuncionarioVO _funcionario;
        private List<TransportadoraVO> _transportadoras;
        #endregion
        #region Propriedades
        public FuncionarioVO Funcionario
        {
            get { return _funcionario ?? (_funcionario = new FuncionarioVO()); }
            set { _funcionario = value; }
        }
        public List<TransportadoraVO> Transportadoras
        {
            get { return _transportadoras; }
            set { _transportadoras = value; }
        }
        #endregion

    }
}
