using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nissi.Model
{
    /// <summary>
    /// Essa classe representa os campos da tabela ClassificacaoFiscal
    /// </summary>
    /// 
    [Serializable()] //deve ser serializavel para armazenar em viewstate
    public class ClassificacaoFiscalVO: NissiBaseVO
    {
        #region Campos
        private int? _codClassificacaoFiscal = null;
        private string _letra = String.Empty;
        private string _numero = String.Empty;
        private DateTime? _dataCadastro = null;
        private int? _usuarioInc = null;
        private DateTime? _dataAlteracao = null;
        private int? _usuarioAlt = null;
        #endregion
        #region Propriedades
        public int? CodClassificacaoFiscal
        {
            get { return _codClassificacaoFiscal; }
            set { _codClassificacaoFiscal = value; }
        }

        public string Letra
        {
            get { return _letra; }
            set { _letra = value; }
        }

        public string Numero
        {
            get { return _numero; }
            set { _numero = value; }
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
        #endregion

        /// <summary>
        /// Retorna um texto que identifique essa instância da classe
        /// </summary>
        #region ToString()
        public override string ToString()
        {
            return "ClassificacaoFiscalVO: " + CodClassificacaoFiscal.ToString();
        }
        #endregion
    }
}


// ------------------------------------------------------------------------- // 


