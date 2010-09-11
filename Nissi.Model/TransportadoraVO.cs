using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nissi.Model
{
    [Serializable] //deve ser serializavel para armazenar em viewstate
    public class TransportadoraVO : PessoaVO
    {
        #region Campos
        private int? _codTransportadora;
        private double _custo;
        #endregion
        #region Propriedades
        public int? CodTransportadora
        {
            get { return _codTransportadora; }
            set { _codTransportadora = value; }
        }
        public double Custo
        {
            get { return _custo; }
            set { _custo = value; }
        }
        #endregion

     }
}
