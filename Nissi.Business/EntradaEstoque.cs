using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.Model;
using Nissi.Repositorio;

namespace Nissi.Business
{
    public class EntradaEstoque
    {
        #region Métodos de Listagem
        public EntradaEstoqueVO ListarTudo(int codEntradaEstoque)
        {
            return new EntradaEstoqueRepositorio().ListarTudo(codEntradaEstoque);
        }

        public List<ItemEntradaEstoqueVO> ListarPorCodigo(int codEntradaEstoque)
        {
            return new EntradaEstoqueRepositorio().ListarPorCodigo(codEntradaEstoque);
        }
        public List<ItemEntradaEstoqueVO> ListarPorFornecedor(int codFornecedor)
        {
            return new EntradaEstoqueRepositorio().ListarPorFornecedor(codFornecedor);
        }
        public List<ItemEntradaEstoqueVO> ListarPorLote(string lote)
        {
            return new EntradaEstoqueRepositorio().ListarPorLote(lote);
        }
        public List<ItemEntradaEstoqueVO> ListarPorData(DateTime dataInicio, DateTime dataFim)
        {
            return new EntradaEstoqueRepositorio().ListarPorData(dataInicio, dataFim);
        }

        public List<ItemEntradaEstoqueVO> ListarPorCorrida(string corrida)
        {
            return new EntradaEstoqueRepositorio().ListarPorCorrida(corrida);
        }
        public List<ItemEntradaEstoqueVO> ListarPorCertificado(string certificado)
        {
            return new EntradaEstoqueRepositorio().ListarPorCertificado(certificado);
        }

        public EntradaEstoqueInsumoVO ListarTudoInsumo(int codEntradaEstoque)
        {
            return new EntradaEstoqueRepositorio().ListarTudoInsumo(codEntradaEstoque);
        }
        #endregion
        #region Método de Inclusão
        public int Incluir(int codFornecedor, int codPedidoCompra, DateTime dataNotaFiscal, DateTime dataEntrada, string notaFiscal,int codUsuario, List<ItemEntradaEstoqueVO> itens, TypePedido tipo)
        {

            return new EntradaEstoqueRepositorio().Incluir(codFornecedor, codPedidoCompra, dataNotaFiscal, dataEntrada, notaFiscal, codUsuario,  itens, tipo);
        }
        public int Incluir(int codFornecedor, int codPedidoCompra, DateTime dataNotaFiscal, DateTime dataEntrada, string notaFiscal, int codUsuario, List<ItemEntradaEstoqueInsumoVO> itens, TypePedido tipo)
        {

            return new EntradaEstoqueRepositorio().Incluir(codFornecedor, codPedidoCompra, dataNotaFiscal, dataEntrada, notaFiscal, codUsuario, itens, tipo);
        }
        #endregion
        #region Método de Alteração
        public void Alterar(int codEntradaEstoque, int codFornecedor, int codPedidoCompra, DateTime dataNotaFiscal, DateTime dataEntrada, string notaFiscal,int codUsuario, List<ItemEntradaEstoqueVO> itens, TypePedido tipo)
        {
            new EntradaEstoqueRepositorio().Alterar(codEntradaEstoque,  codFornecedor,  codPedidoCompra,  dataNotaFiscal,  dataEntrada,  notaFiscal, codUsuario,  itens, tipo);
        }
        public void Alterar(int codEntradaEstoque, int codFornecedor, int codPedidoCompra, DateTime dataNotaFiscal, DateTime dataEntrada, string notaFiscal, int codUsuario, List<ItemEntradaEstoqueInsumoVO> itens, TypePedido tipo)
        {
            new EntradaEstoqueRepositorio().Alterar(codEntradaEstoque, codFornecedor, codPedidoCompra, dataNotaFiscal, dataEntrada, notaFiscal, codUsuario, itens, tipo);
        }
        #endregion
        #region Método de Exclusão
        public void Excluir(int codEntradaEstoque)
        {
            new EntradaEstoqueRepositorio().Excluir(codEntradaEstoque);
        }
        public void ExcluirItem(int codItemEntradaEstoqueInsumo)
        {
            new ItemEntradaEstoqueRepositorio().Excluir(codItemEntradaEstoqueInsumo);
        }
        #endregion


    }
}
