using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nissi.Model
{
    /// <summary>
    /// Essa classe representa os campos da tabela Departamento
    /// </summary>
    /// 
    [Serializable()] //deve ser serializavel para armazenar em viewstate
    public class DepartamentoVO
    {
        #region Campos
        private short? _codDepartamento = null;
        private string _nome = String.Empty;
        private DateTime? _dataCadastro = null;
        private int? _usuarioInc = null;
        private DateTime? _dataAlteracao = null;
        private int? _usuarioAlt = null;
        #endregion
        #region Propriedades
        public short? CodDepartamento
        {
            get { return _codDepartamento; }
            set { _codDepartamento = value; }
        }

        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
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
            return "DepartamentoVO: " + CodDepartamento.ToString();
        }
        #endregion
    }
}
