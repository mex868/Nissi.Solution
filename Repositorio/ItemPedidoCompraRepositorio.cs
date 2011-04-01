using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.Model;

namespace Nissi.Repositorio
{
    public class ItemPedidoCompraRepositorio
    {
        private readonly RepositorioDataContext _repositorioDataContext;
        public ItemPedidoCompraRepositorio()
        {
            _repositorioDataContext = new RepositorioDataContext();
        }
        internal void Incluir(List<Model.ItemPedidoCompraVO> itemPedidoCompraVo, int codPedidoCompra)
        {
            var lstItemPedidoCompra = itemPedidoCompraVo.Select(i => new ItemPedidoCompra()
                                                                         {
                                                                             CodPedidoCompra = codPedidoCompra,
                                                                             CodBitola = i.BitolaVo.CodBitola,
                                                                             CodMateriaPrima = i.MateriaPrimaVo.CodMateriaPrima,
                                                                             ResistenciaTracao = i.ResistenciaTracao,
                                                                             Especificacao = i.Especificacao,
                                                                             IPI =  i.Ipi,
                                                                             Qtd = i.Qtd,
                                                                             CodUnidade = i.UnidadeVo.CodUnidade,
                                                                             Valor = i.Valor,
                                                                             DataCadastro = i.DataCadastro,
                                                                             UsuarioInc = i.UsuarioInc,
                                                                         });
            _repositorioDataContext.ItemPedidoCompras.InsertAllOnSubmit(lstItemPedidoCompra);
            _repositorioDataContext.SubmitChanges();
        }
        internal void Incluir(List<Model.ItemPedidoCompraInsumoVO> itemPedidoCompraVo, int codPedidoCompra)
        {
            var lstItemPedidoCompra = itemPedidoCompraVo.Select(i => new ItemPedidoCompra()
            {
                CodPedidoCompra = codPedidoCompra,
                CodMateriaPrima = i.ProdutoInsumoVo.CodProdutoInsumo,
                ResistenciaTracao = i.ResistenciaTracao,
                Especificacao = i.Especificacao,
                CodBitola = null,
                IPI = i.Ipi,
                Qtd = i.Qtd,
                CodUnidade = i.UnidadeVo.CodUnidade,
                Valor = i.Valor,
                DataCadastro = i.DataCadastro,
                UsuarioInc = i.UsuarioInc,
            });
            _repositorioDataContext.ItemPedidoCompras.InsertAllOnSubmit(lstItemPedidoCompra);
            _repositorioDataContext.SubmitChanges();
        }
        internal void Alterar(List<Model.ItemPedidoCompraVO> itemPedidoCompraVo, int codPedidoCompra)
        {
            var lstItemPedidoCompra = itemPedidoCompraVo.Where(i => i.CodItemPedidoCompra == 0).Select(i => new ItemPedidoCompra()
            {
                CodPedidoCompra = codPedidoCompra,
                CodBitola = i.BitolaVo.CodBitola,
                CodMateriaPrima = i.MateriaPrimaVo.CodMateriaPrima,
                ResistenciaTracao = i.ResistenciaTracao,
                Especificacao = i.Especificacao,
                IPI = i.Ipi,
                Qtd = i.Qtd,
                CodUnidade = i.UnidadeVo.CodUnidade,
                Valor = i.Valor,
                DataCadastro = i.DataCadastro,
                UsuarioInc = i.UsuarioInc,
            });
            if (lstItemPedidoCompra.Count() > 0)
                _repositorioDataContext.ItemPedidoCompras.InsertAllOnSubmit(lstItemPedidoCompra);
             foreach (var item in itemPedidoCompraVo)
            {
                if (item.CodItemPedidoCompra > 0)
                {
                    IQueryable<ItemPedidoCompra> query = from i in _repositorioDataContext.ItemPedidoCompras
                                                          where i.CodItemPedidoCompra == item.CodItemPedidoCompra
                                                          select i;
                    var itemPedidoCompra = query.FirstOrDefault();
                    itemPedidoCompra.CodBitola = item.BitolaVo.CodBitola;
                    itemPedidoCompra.CodMateriaPrima = item.MateriaPrimaVo.CodMateriaPrima;
                    itemPedidoCompra.ResistenciaTracao = item.ResistenciaTracao;
                    itemPedidoCompra.Especificacao = item.Especificacao;
                    itemPedidoCompra.IPI = item.Ipi;
                    itemPedidoCompra.Qtd = item.Qtd;
                    itemPedidoCompra.CodUnidade = item.UnidadeVo.CodUnidade;
                    itemPedidoCompra.Valor = item.Valor;
                    itemPedidoCompra.DataAlteracao = DateTime.Now;
                    itemPedidoCompra.UsuarioAlt = item.UsuarioAlt;
                }
            _repositorioDataContext.SubmitChanges();
        }
    }
        internal void Alterar(List<Model.ItemPedidoCompraInsumoVO> itemPedidoCompraInsumoVo, int codPedidoCompra)
        {
            var lstItemPedidoCompra = itemPedidoCompraInsumoVo.Where(i => i.CodItemPedidoCompraInsumo == 0).Select(i => new ItemPedidoCompra()
            {
                CodPedidoCompra = codPedidoCompra,
                CodMateriaPrima = i.ProdutoInsumoVo.CodProdutoInsumo,
                ResistenciaTracao = i.ResistenciaTracao,
                Especificacao = i.Especificacao,
                IPI = i.Ipi,
                Qtd = i.Qtd,
                CodUnidade = i.UnidadeVo.CodUnidade,
                Valor = i.Valor,
                DataCadastro = i.DataCadastro,
                UsuarioInc = i.UsuarioInc,
            });
            if (lstItemPedidoCompra.Count() > 0)
                _repositorioDataContext.ItemPedidoCompras.InsertAllOnSubmit(lstItemPedidoCompra);
            foreach (var item in itemPedidoCompraInsumoVo)
            {
                if (item.CodItemPedidoCompraInsumo > 0)
                {
                    IQueryable<ItemPedidoCompra> query = from i in _repositorioDataContext.ItemPedidoCompras
                                                         where i.CodItemPedidoCompra == item.CodItemPedidoCompraInsumo
                                                         select i;
                    var itemPedidoCompra = query.FirstOrDefault();
                    itemPedidoCompra.CodMateriaPrima = item.ProdutoInsumoVo.CodProdutoInsumo;
                    itemPedidoCompra.ResistenciaTracao = item.ResistenciaTracao;
                    itemPedidoCompra.Especificacao = item.Especificacao;
                    itemPedidoCompra.IPI = item.Ipi;
                    itemPedidoCompra.Qtd = item.Qtd;
                    itemPedidoCompra.CodUnidade = item.UnidadeVo.CodUnidade;
                    itemPedidoCompra.Valor = item.Valor;
                    itemPedidoCompra.DataAlteracao = DateTime.Now;
                    itemPedidoCompra.UsuarioAlt = item.UsuarioAlt;
                }
                _repositorioDataContext.SubmitChanges();
            }
        }
    #region Método de Exclusao
    public void Excluir(int codItemPedidoCompra)
    {
            IQueryable<ItemPedidoCompra> query = from i in _repositorioDataContext.ItemPedidoCompras
                                                  where i.CodItemPedidoCompra == codItemPedidoCompra
                                                  select i;
            var itemPedidoCompra = query.FirstOrDefault();
            _repositorioDataContext.ItemPedidoCompras.DeleteOnSubmit(itemPedidoCompra);
            _repositorioDataContext.SubmitChanges();
    }
    internal void ExcluirTodos(int codPedidoCompra)
    {
            //Excluir Itens do Pedido de Compra
            IQueryable<ItemPedidoCompra> queryItemPedido = from i in _repositorioDataContext.ItemPedidoCompras
                                                       where i.CodPedidoCompra == codPedidoCompra
                                                       select i;
            if (queryItemPedido.Count() > 0)
            {
                var itemPedidoCompras = queryItemPedido.ToList();
                _repositorioDataContext.ItemPedidoCompras.DeleteAllOnSubmit(itemPedidoCompras);
                _repositorioDataContext.SubmitChanges();
            }
            //----------------------------------
            //Excluir Itens do Pedido de Insumo
            IQueryable<ItemPedidoCompra> queryItemPedidoInsumo = from i in _repositorioDataContext.ItemPedidoCompras
                                                 where i.CodPedidoCompra == codPedidoCompra
                                                 select i;
            if (queryItemPedidoInsumo.Count() > 0)
            {
                var itemPedidoCompras = queryItemPedidoInsumo.ToList();
                _repositorioDataContext.ItemPedidoCompras.DeleteAllOnSubmit(itemPedidoCompras);
                _repositorioDataContext.SubmitChanges();
            }
            //--------------------------------
    }
    #endregion
 
}
    }
