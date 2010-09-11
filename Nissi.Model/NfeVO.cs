using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nissi.Model
{
    /// <summary>
    /// Essa classe representa os campos da tabela NFE
    /// </summary>
    [Serializable]
    public class NfeVO
    {
        #region Campos
        private int? _codNFe = null;
        private int? _codNF = null;
        private int? _codNumLote = null;
        private string _chaveNFE = String.Empty;
        private string _numProtocolo = String.Empty;
        private string _numRecibo = null;
        private bool? _indAmbiente = null;
        private bool? _indImpressao = null;
        private DateTime? _dataCadastro = null;
        private int? _usuarioInc = null;
        private DateTime? _dataAlteracao = null;
        private int? _usuarioAlt = null;
        private string _indStatus = String.Empty;
        private string _indTipoEmissao = String.Empty;
        private string _cRT = String.Empty;
        private string _enviarEmail = string.Empty;
        private int? _nF = null;
        private string _serie = string.Empty;
        private string _razaoSocial = string.Empty;
        private string _cNPJ = string.Empty;
        #endregion

        #region Propriedades
        public int? CodNFe
        {
            get { return _codNFe; }
            set { _codNFe = value; }
        }

        public int? CodNF
        {
            get { return _codNF; }
            set { _codNF = value; }
        }

        public int? CodNumLote
        {
            get { return _codNumLote; }
            set { _codNumLote = value; }
        }

        public string ChaveNFE
        {
            get { return _chaveNFE; }
            set { _chaveNFE = value; }
        }

        public string NumProtocolo
        {
            get { return _numProtocolo; }
            set { _numProtocolo = value; }
        }

        public string NumRecibo
        {
            get { return _numRecibo; }
            set { _numRecibo = value; }
        }

        public bool? IndAmbiente
        {
            get { return _indAmbiente; }
            set { _indAmbiente = value; }
        }

        public bool? IndImpressao
        {
            get { return _indImpressao; }
            set { _indImpressao = value; }
        }

        public DateTime? DataCadastro
        {
            get { return _dataCadastro; }
            set { _dataCadastro = value; }
        }

        public int? UsuarioInc
        {
            get { return _usuarioInc; }
            set { _usuarioInc = value; }
        }

        public DateTime? DataAlteracao
        {
            get { return _dataAlteracao; }
            set { _dataAlteracao = value; }
        }

        public int? UsuarioAlt
        {
            get { return _usuarioAlt; }
            set { _usuarioAlt = value; }
        }
        
        public string IndStatus
        {
            get { return _indStatus; }
            set { _indStatus = value; }
        }

        public string IndTipoEmissao
        {
            get { return _indTipoEmissao; }
            set { _indTipoEmissao = value; }
        }

        public string CRT
        {
            get { return _cRT; }
            set { _cRT = value; }
        }
        public string EnviarEmail
        {
            get { return _enviarEmail; }
            set { _enviarEmail = value; }
        }
        public int? NF
        {
            get { return _nF; }
            set { _nF = value; }
        }
        public string Serie
        {
            get { return _serie; }
            set { _serie = value; }
        }
        public string RazaoSocial
        {
            get { return _razaoSocial; }
            set { _razaoSocial = value; }
        }
        public string CNPJ
        {
            get { return _cNPJ; }
            set { _cNPJ = value; }
        }
        #endregion

        /// <summary>
        /// Retorna um texto que identifique essa instância da classe
        /// </summary>
        #region ToString()
        public override string ToString()
        {
            return "NFEVO: " + CodNFe.ToString();
        }
        #endregion

    }
}


// ------------------------------------------------------------------------- // 


