using System;

namespace Nissi.Model
{
    /// <summary>
    /// Essa classe representa os campos da tabela Roles
    /// </summary>
    [Serializable()] //deve ser serializavel para armazenar em viewstate
    public class RolesVO
    {
        #region Campos
        private short? _codRoles = null;
        private short? _codSubMenu = null;
        private DateTime? _dataCadastro = null;
        private int? _usuarioInc = null;
        private DateTime? _dataAlteracao = null;
        private int? _usuarioAlt = null;
        private PerfilAcessoVO _perfilAcesso;
        #endregion
        #region Propriedades
        public short? CodRoles
        {
            get { return _codRoles; }
            set { _codRoles = value; }
        }

        public short? CodSubMenu
        {
            get { return _codSubMenu; }
            set { _codSubMenu = value; }
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
        
        public PerfilAcessoVO PerfilAcesso
        {
            get
            {
                if (_perfilAcesso == null)
                    _perfilAcesso = new PerfilAcessoVO();
                    return _perfilAcesso; 
             }
            set { _perfilAcesso = value; }
        }
        #endregion

        /// <summary>
        /// Retorna um texto que identifique essa instância da classe
        /// </summary>
        #region ToString()
        public override string ToString()
        {
            return "RolesVO: " + CodRoles.ToString();
        }
        #endregion
    }
}


 // ------------------------------------------------------------------------- // 

