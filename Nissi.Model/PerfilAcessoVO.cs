using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nissi.Model
{
    public class PerfilAcessoVO : NissiBaseVO
    {
        #region Campo
        private short? _codPerfilAcesso;
        private string _nomPerfilAcesso;
        private string _descPerfilAcesso;
        private DateTime _dataCadastro;
        private int _usuarioInc;
        private DateTime _dataAlteracao;
        private int _usuarioAlt;
        private bool? _ativo;
        #endregion
        #region Propriedade
        public short? CodPerfilAcesso
        {
            get { return _codPerfilAcesso; }
            set { _codPerfilAcesso = value; }
        }
        public string NomPerfilAcesso
        {
            get { return _nomPerfilAcesso; }
            set { _nomPerfilAcesso = value; }
        }
        public string DescPerfilAcesso
        {
            get { return _descPerfilAcesso; }
            set { _descPerfilAcesso = value; }
        }
        public DateTime DataCadastro
        {
            get { return _dataCadastro; }
            set { _dataCadastro = value; }
        }
        public int UsuarioInc
        {
            get { return _usuarioInc; }
            set { _usuarioInc = value; }
        }
        public DateTime DataAlteracao
        {
            get { return _dataAlteracao; }
            set { _dataAlteracao = value; }
        }
        public int UsuarioAlt
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
    }
}
