using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nissi.Model
{
    [Serializable()] //deve ser serializavel para armazenar em viewstate
    public class TipoLogradouroVO
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
