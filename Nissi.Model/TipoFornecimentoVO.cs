using System;

namespace Nissi.Model
{
/// <summary>
/// Summary description for Class1
/// </summary>
    [Serializable()] //deve ser serializavel para armazenar em viewstate
    public class TipoFornecimentoVO
    {
        #region Campos
        private short? _codTipoFornecimento;
        private string _descricao;
        private DateTime _dataCadastro;
        private int _usuarioInc;
        private DateTime _dataAlteracao;
        private int _usuarioAlt;
        #endregion
        #region Propriendades
        public short? CodTipoFornecimento
        {
            get { return _codTipoFornecimento; }
            set { _codTipoFornecimento = value; }
        }
        public string Descricao
        {
            get { return _descricao; }
            set { _descricao = value; }
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
        #endregion
    }
}
