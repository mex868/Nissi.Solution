using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Nissi.Model
{
    public class CEPVO : NissiBaseVO
    {
        #region Campos
        private string _codCep;
        private string _nomEndereco;
        private BairroVO _bairro;
        private CidadeVO _cidade;
        private TipoLogradouroVO _nomTipoLogradouro;
        #endregion

        #region Propriedades
        public string NomEndereco
        {
            get { return _nomEndereco; }
            set { _nomEndereco = value; }
        }
        public string CodCep
        {
            get { return _codCep; }
            set { _codCep = value; }
        }
        public BairroVO Bairro
        {
            get { if (_bairro == null)
                     _bairro = new BairroVO();

                return _bairro;
            }
            set { _bairro = value; }
        }
        public CidadeVO Cidade
        {
            get { if (_cidade == null)
                    _cidade = new CidadeVO();

                return _cidade;
            }
            set { _cidade = value; }
        }

        public TipoLogradouroVO TipoLogradouro
        {
            get
            {
                if (_nomTipoLogradouro == null)
                    _nomTipoLogradouro = new TipoLogradouroVO();

                return _nomTipoLogradouro;
            }
            set { _nomTipoLogradouro = value; }
        }
        #endregion

    }
}
