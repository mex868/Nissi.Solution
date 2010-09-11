using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nissi.Model
{
   public class UnidadeVO : NissiBaseVO
    {
        #region Variaveis

        private string _descricao;
        private int? _codUnidade;
        private string _unidade;
        #endregion

        #region Propriedades

        public string Descricao
        {
            get { return _descricao; }
            set { _descricao = value; }
        }

        public int? CodUnidade
        {
            get { return _codUnidade; }
            set { _codUnidade = value; }
        }
        public string TipoUnidade
        {
            get { return _unidade; }
            set { _unidade = value; }
        }

        #endregion
    }
}
