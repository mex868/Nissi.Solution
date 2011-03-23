using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nissi.Model
{
    [Serializable()] //deve ser serializavel para armazenar em viewstate
    public class BairroVO
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
