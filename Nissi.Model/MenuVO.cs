using System;
using System.Collections.Generic;

namespace Nissi.Model
{
    /// <summary>
    /// Essa classe representa os campos da tabela Menu
    /// </summary>
    [Serializable()] //deve ser serializavel para armazenar em viewstate
    public class MenuVO
    {
        #region Campos
        private short? _codMenu = null;
        private string _text = String.Empty;
        private DateTime? _dataCadastro = null;
        private int? _usuarioInc = null;
        private DateTime? _dataAlteracao = null;
        private int? _usuarioAlt = null;
        private bool? _ativo = null;
        #endregion
        #region Propriedades
        public short? CodMenu
        {
            get { return _codMenu; }
            set { _codMenu = value; }
        }

        public string Text
        {
            get { return _text; }
            set { _text = value; }
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

        public bool? Ativo
        {
            get { return _ativo; }
            set { _ativo = value; }
        }
        #endregion

        /// <summary>
        /// Retorna um texto que identifique essa instância da classe
        /// </summary>
        #region ToString()
        public override string ToString()
        {
            return "MenuVO: " + CodMenu.ToString();
        }
        #endregion
    }
}


