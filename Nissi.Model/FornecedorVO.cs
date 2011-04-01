using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nissi.Model
{
    [Serializable()] //deve ser serializavel para armazenar em viewstate
    public class FornecedorVO : PessoaVO
    {
        #region Campos
        private int? _codFornecedor;
        private DepartamentoVO _departamento;
        private int _prazoEntrega;
        private FormaPgtoVO _formaPgto;
        private TipoFornecimentoVO _tipoFornecimento;
        private BancoVO _banco;
        private FuncionarioVO _funcionario;
        #endregion

        #region Propriedades

        public FuncionarioVO Funcionario
        {
            get
            {
                if (_funcionario == null)
                    _funcionario = new FuncionarioVO();
                return _funcionario;
            }
            set { _funcionario = value; }
        }

        public int? CodFornecedor
        {
            get { return _codFornecedor; }
            set { _codFornecedor = value; }
        }
        public DepartamentoVO Departamento
        {
            get {
                if (_departamento == null)
                    _departamento = new DepartamentoVO();
                return _departamento;
            }
            set { _departamento = value; }
        }
        public int PrazoEntrega
        {
            get { return _prazoEntrega; }
            set { _prazoEntrega = value; }
        }
        public FormaPgtoVO FormaPgto
        {
            get
            {
                if (_formaPgto == null)
                    _formaPgto = new FormaPgtoVO();
                return _formaPgto; 
            }
            set { _formaPgto = value; }
        }
        public TipoFornecimentoVO TipoFornecimento
        {
            get
            {
                if (_tipoFornecimento == null)
                    _tipoFornecimento = new TipoFornecimentoVO();
                return _tipoFornecimento;
            }
            set { _tipoFornecimento = value; }
        }

        public BancoVO Banco
        {
            set { _banco = value; }
            get
            {
                if (_banco == null)
                    _banco = new BancoVO();
                return _banco;
            }
        }
        #endregion
        

    }
}
