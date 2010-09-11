using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nissi.Model
{
    /// <summary>
    /// Essa classe representa os campos da tabela MensagemNF
    /// </summary>
    public class MensagemNFVO : NissiBaseVO
    {
        #region Campos
        private int? _codMensagemNF = null;
        private string _descricao = String.Empty;
        private int? _usuarioInc = null;
        private DateTime? _dataCadastro = null;
        private int? _usuarioAlt = null;
        private DateTime? _dataAlteracao = null;
        #endregion
        #region Propriedades
        public int? CodMensagemNF
        {
            get { return _codMensagemNF; }
            set { _codMensagemNF = value; }
        }

        public string Descricao
        {
            get { return _descricao; }
            set { _descricao = value; }
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
            return "MensagemNFVO: " + CodMensagemNF.ToString();
        }
        #endregion
    }
}
