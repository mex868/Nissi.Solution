using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nissi.Model
{
    public class CidadeVO : NissiBaseVO
    {
        #region Campos
        private int? _codCidade;
        private string _codIBGE;
        private string _nomCidade;
        private UnidadeFederacaoVO _UF;
        #endregion
        #region
        public int? CodCidade
        {
            get { return _codCidade; }
            set { _codCidade = value; }
        }
        public string NomCidade
        {
            get { return _nomCidade; }
            set { _nomCidade = value; }
        }
        public string CodIBGE
        {
            get { return _codIBGE; }
            set { _codIBGE = value; }
        }
        public UnidadeFederacaoVO UF
        {
            get
            {
                if (_UF == null)
                    _UF = new UnidadeFederacaoVO();
                return _UF; }
            set { _UF = value; }
        }
        #endregion
    }
}
