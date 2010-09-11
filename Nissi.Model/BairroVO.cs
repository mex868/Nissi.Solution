using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nissi.Model
{
    public class BairroVO : NissiBaseVO
    {
        #region Campos
        private int? _codBairro;
        private string _nomBairro;
        #endregion
        #region Propriedades
        public int? CodBairro
        {
            get { return _codBairro; }
            set { _codBairro = value; }
        }
        public string NomBairro
        {
            get { return _nomBairro; }
            set { _nomBairro = value; }
        }
        #endregion
    }
}
