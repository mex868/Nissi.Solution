using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Nissi.Model
{
    [Serializable()]
    [XmlRoot(ElementName = "Lote")]
    public class LoteVO : NissiBaseVO
    {
        #region Campos
            private int? _codNumLote = null;
            private DateTime? _dataCadastro = null;
            private int? _codUsuario = null;
            private List<NfeVO> _nfe = null;
        #endregion

        #region Propriedades
            public int? CodNumLote
            {
                get { return _codNumLote; }
                set { _codNumLote = value; }
            }

            public DateTime? DataCadastro
            {
                get { return _dataCadastro; }
                set { _dataCadastro = value; }
            }

            public int? CodUsuario
            {
                get { return _codUsuario; }
                set { _codUsuario = value; }
            }
            [XmlElement(ElementName = "NFe")]
            public List<NfeVO> Nfe
            {
                get { return _nfe; }
                set { _nfe = value; }
            }    
        #endregion
    }
}
