using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.Model;
using Nissi.Repositorio;

namespace Nissi.Business
{
    public class PedidoCompra
    {
        #region Métodos de Listagem
        public PedidoCompraVO ListarTudo(int codPedidoCompra)
        {
            return new PedidoCompraRepositorio().ListarTudo(codPedidoCompra);
        }
        public PedidoCompraVO ListarTudoEstoque(int codPedidoCompra)
        {
            return new PedidoCompraRepositorio().ListarTudoEstoque(codPedidoCompra);
        }

        public PedidoCompraInsumoVO ListarTudoInsumo(int codPedidoCompra)
        {
            return new PedidoCompraRepositorio().ListarTudoInsumo(codPedidoCompra);
        }
        public List<ListItemPedidoCompraVO> ListarPorCodigo(int codPedidoCompra)
        {
            return new PedidoCompraRepositorio().ListarPorCodigo(codPedidoCompra);
        }
        public List<ListItemPedidoCompraVO> ListarPorData(DateTime dataInicio, DateTime dataFim)
        {
            return new PedidoCompraRepositorio().ListarPorData(dataInicio, dataFim);
        }

        public List<ListItemPedidoCompraVO> ListarPorFornecedor(int codFornecedor)
        {
            return new PedidoCompraRepositorio().ListarPorFornecedor(codFornecedor);
        }
        public List<ListItemPedidoCompraVO> ListarPorBitola(int codBitola)
        {
            return new PedidoCompraRepositorio().ListarPorBitola(codBitola);
        }
        public List<ListItemPedidoCompraVO> ListarClasseTipo(int codClasseTipo)
        {
            return new PedidoCompraRepositorio().ListarPorClasseTipo(codClasseTipo);
        }
        #endregion
        #region Método de Inclusão
        public int Incluir(int codFornecedor, DateTime dataEmissao, DateTime? dataPrazoEntrega, short tipoEntrega,  short codFormaPgto,
            string condicaoFornecimento, string observacao, int codFuncionarioAprovador, int codFuncionarioComprador,
            int codUsuarioInc, List<ItemPedidoCompraVO> itemPedidoCompraVo, TypePedido tipo)
        {
            return new PedidoCompraRepositorio().Incluir(codFornecedor, dataEmissao, dataPrazoEntrega, tipoEntrega,
                                                         codFormaPgto,
                                                         condicaoFornecimento, observacao,
                                                         codFuncionarioAprovador, codFuncionarioComprador,
                                                         codUsuarioInc, itemPedidoCompraVo, tipo);
        }
        public int Incluir(int codFornecedor, DateTime dataEmissao, DateTime? dataPrazoEntrega, short tipoEntrega, short codFormaPgto,
    string condicaoFornecimento, string observacao, int codFuncionarioAprovador, int codFuncionarioComprador,
    int codUsuarioInc, List<ItemPedidoCompraInsumoVO> itemPedidoCompraInsumoVo, TypePedido tipo)
        {
            return new PedidoCompraRepositorio().Incluir(codFornecedor, dataEmissao, dataPrazoEntrega, tipoEntrega,
                                                         codFormaPgto,
                                                         condicaoFornecimento, observacao,
                                                         codFuncionarioAprovador, codFuncionarioComprador,
                                                         codUsuarioInc, itemPedidoCompraInsumoVo, tipo);
        }
        #endregion
        #region Métodos de Alteração
        public void Alterar(int codPedidoCompra, int codFornecedor, DateTime dataEmissao, DateTime? dataPrazoEntrega,short tipoEntrega, short codFormaPgto,
    string condicaoFornecimento, string observacao, int codFuncionarioAprovador, int codFuncionarioComprador,
    int codUsuarioAlt, List<ItemPedidoCompraVO> itemPedidoCompraVo, TypePedido tipo)
        {
            new PedidoCompraRepositorio().Alterar(codPedidoCompra, codFornecedor, dataEmissao, dataPrazoEntrega, tipoEntrega, codFormaPgto,
    condicaoFornecimento, observacao,  codFuncionarioAprovador, codFuncionarioComprador,
    codUsuarioAlt, itemPedidoCompraVo, tipo);
        }
        public void Alterar(int codPedidoCompra, int codFornecedor, DateTime dataEmissao, DateTime? dataPrazoEntrega, short tipoEntrega, short codFormaPgto,
    string condicaoFornecimento, string observacao, int codFuncionarioAprovador, int codFuncionarioComprador,
    int codUsuarioAlt, List<ItemPedidoCompraInsumoVO> itemPedidoCompraInsumoVo, TypePedido tipo)
        {
            new PedidoCompraRepositorio().Alterar(codPedidoCompra, codFornecedor, dataEmissao, dataPrazoEntrega, tipoEntrega, codFormaPgto,
    condicaoFornecimento, observacao, codFuncionarioAprovador, codFuncionarioComprador,
    codUsuarioAlt, itemPedidoCompraInsumoVo, tipo);
        }
        #endregion
        #region Método de Exclusao
        public void Excluir(int codPedidoCompra)
        {
            new PedidoCompraRepositorio().Excluir(codPedidoCompra);
        }
        public void ExcluirItem(int codItemPedidoCompra)
        {
            new ItemPedidoCompraRepositorio().Excluir(codItemPedidoCompra);
        }

        #endregion
    }
}
