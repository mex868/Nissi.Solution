using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nissi.Model
{
    public abstract class PedidoModel
    {
        #region Campos

        public PedidoModel()
        {
            CodPedidoCompra = 0;
            Fornecedor = new FornecedorVO();
            DataEmissao = DateTime.Now;
            PrazoEntrega = new PrazoEntregaVO();
            CondicaoFornecimento = String.Empty;
            Observacao = String.Empty;
            FormaPgto = new FormaPgtoVO();
            TipoRetirada = 0;
            FuncionarioAprovador = new FuncionarioVO();
            FuncionarioComprador = new FuncionarioVO();
            DataCadastro = null;
            DataPrazoEntrega = null;
            UsuarioInc = null;
            DataAlteracao = null;
            UsuarioAlt = null;
        }

        public DateTime? DataPrazoEntrega{ get; set; }

        #endregion
        #region Propriedades

        public int CodPedidoCompra { get; set; }

        public FornecedorVO Fornecedor { get; set; }

        public DateTime DataEmissao { get; set; }

        public string CondicaoFornecimento { get; set; }

        public string Observacao { get; set; }

        public PrazoEntregaVO PrazoEntrega { get; set; }

        public FormaPgtoVO FormaPgto { get; set; }

        public short TipoRetirada { get; set; }

        public FuncionarioVO FuncionarioComprador { get; set; }

        public FuncionarioVO FuncionarioAprovador { get; set; }

        public DateTime? DataCadastro { get; set; }

        public int? UsuarioInc { get; set; }

        public DateTime? DataAlteracao { get; set; }
        public TypePedido Tipo { get; set; }
        public int? UsuarioAlt { get; set; }

        public IList<Object> Itens { get; set; }
        #endregion
    }
}
