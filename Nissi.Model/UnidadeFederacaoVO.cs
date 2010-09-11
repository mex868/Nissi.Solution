using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nissi.Model
{
    public class UnidadeFederacaoVO : NissiBaseVO
    {
        #region Campos
        private string _codUF;
        private string _nomUF;
        #endregion
        #region Propriedades
        public string CodUF
        {
            get { return _codUF; }
            set { _codUF = value; }
        }
        public string NomUF
        {
            get { return _nomUF; }
            set { _nomUF = value; }
        }
        #endregion

    }
}
