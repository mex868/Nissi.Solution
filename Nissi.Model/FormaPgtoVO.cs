using System;

namespace Nissi.Model
{
/// <summary>
/// Summary description for Class1
/// </summary>
    public class FormaPgtoVO
    {
        #region Campos
        private short? _codFormaPgto;
        private string _descricao;
        private short _parcela;
        private short _intervalo;
        private DateTime _dataCadastro;
        private int _usuarioInc;
        private DateTime _dataAlteracao;
        private int _usuarioAlt;
        #endregion

        #region Propriendades
        public short? CodFormaPgto
        {
            get { return _codFormaPgto; }
            set { _codFormaPgto = value; }
        }
        public string Descricao
        {
            get { return _descricao; }
            set { _descricao = value; }
        }
        public short Parcela
        {
            get { return _parcela; }
            set { _parcela = value; }
        }
        public DateTime DataCadastro
        {
            get { return _dataCadastro; }
            set { _dataCadastro = value; }
        }
        public short Intervalo
        {
            get { return _intervalo; }
            set { _intervalo = value; }
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
