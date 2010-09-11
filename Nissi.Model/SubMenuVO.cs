using System;
using System.Collections.Generic;

namespace Nissi.Model
{
    /// <summary>
    /// Essa classe representa os campos da tabela SubMenu
    /// </summary>
    public class SubMenuVO
    {
        #region Campos
        private short? _codSubMenu = null;
        private short? _codMenu = null;
        private string _text = String.Empty;
        private string _url = String.Empty;
        private bool? _resolveurl = null;
        private DateTime? _dataCadastro = null;
        private int? _usuarioInc = null;
        private DateTime? _dataAlteracao = null;
        private int? _usuarioAlt = null;
        private bool? _ativo = null;
        private List<RolesVO> _roles;
        #endregion
        #region Propriedades
        public short? CodSubMenu
        {
            get { return _codSubMenu; }
            set { _codSubMenu = value; }
        }

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

        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        public bool? Resolveurl
        {
            get { return _resolveurl; }
            set { _resolveurl = value; }
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
        public List<RolesVO> Roles
        {
            get {
                    if (_roles == null)
                        _roles = new List<RolesVO>();
                    return _roles; 
                }
            set { _roles = value; }
        }
        #endregion

        /// <summary>
        /// Retorna um texto que identifique essa instância da classe
        /// </summary>
        #region ToString()
        public override string ToString()
        {
            return "SubMenuVO: " + CodSubMenu.ToString();
        }
        #endregion
    }
}



 // ------------------------------------------------------------------------- // 

