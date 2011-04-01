using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nissi.Model
{
    /// <summary>
    /// Essa classe representa os campos da tabela CFOP
    /// </summary>
    [Serializable()] //deve ser serializavel para armazenar em viewstate
    public class CFOPVO
    {
        #region Campos
        private int? _codCFOP = null;
        private string _cFOP = String.Empty;
        private string _naturezaOperacao = String.Empty;
        private int? _usuarioInc = null;
        private DateTime? _dataCadastro = null;
        private int? _usuarioAlt = null;
        private DateTime? _dataAlteracao = null;
        #endregion
        #region Propriedades
        public int? CodCFOP
        {
            get { return _codCFOP; }
            set { _codCFOP = value; }
        }

        public string CFOP
        {
            get { return _cFOP; }
            set { _cFOP = value; }
        }

        public string NaturezaOperacao
        {
            get { return _naturezaOperacao; }
            set { _naturezaOperacao = value; }
        }

        public int? UsuarioInc
        {
            get { return _usuarioInc; }
            set { _usuarioInc = value; }
        }

        public DateTime? DataCadastro
        {
            get { return _dataCadastro; }
            set { _dataCadastro = value; }
        }

        public int? UsuarioAlt
        {
            get { return _usuarioAlt; }
            set { _usuarioAlt = value; }
        }

        public DateTime? DataAlteracao
        {
            get { return _dataAlteracao; }
            set { _dataAlteracao = value; }
        }
        #endregion

        /// <summary>
        /// Retorna um texto que identifique essa instância da classe
        /// </summary>
        #region ToString()
        public override string ToString()
        {
            return "CFOPVO: " + CodCFOP.ToString();
        }
        #endregion
    }
}
