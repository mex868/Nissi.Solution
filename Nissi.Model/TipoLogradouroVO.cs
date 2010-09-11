using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nissi.Model
{
    public class TipoLogradouroVO : NissiBaseVO
    {
        #region Campos
        private int? _codNomTipoLog;
        private string _nomTipoLog;
        #endregion

        #region Propriedades
        public int? CodNomTipoLogradouro
        {
            get { return _codNomTipoLog; }
            set { _codNomTipoLog = value; }
        }
        public string NomTipoLogradouro
        {
            get { return _nomTipoLog; }
            set { _nomTipoLog = value; }
        }
        #endregion
    }
}
