using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nissi.Model
{
    public class BancoVO : NissiBaseVO
    {
        #region Campos
        private int? _codBanco;
        private int? _codCompensacao;
        private string _banco;
        private int? _agencia;
        private string _numConta;
        private bool _tipoConta;
        #endregion

        #region Propriedades

        public int? CodBanco
        {
            get { return _codBanco; }
            set { _codBanco = value; }
        }

        public int? CodCompensacao
        {
            get { return _codCompensacao; }
            set { _codCompensacao = value; }
        }

        public string Banco
        {
            get { return _banco; }
            set { _banco = value; }
        }
        public int? Agencia
        {
            get { return _agencia; }
            set { _agencia = value; }
        }
        public string NumConta
        {
            get { return _numConta; }
            set { _numConta = value; }
        }

        public bool TipoConta
        {
            get { return _tipoConta; }
            set { _tipoConta = value; }
        }
        #endregion
    }
}
