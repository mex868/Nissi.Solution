using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nissi.Repositorio
{
    public class ItemEntradaEstoqueRepositorio
    {
        private readonly RepositorioDataContext _repositorioDataContext;
        public ItemEntradaEstoqueRepositorio()
        {
            _repositorioDataContext = new RepositorioDataContext();
        }

        internal void Incluir(List<Model.ItemEntradaEstoqueVO> itens, int codEntradaEstoque)
        {
            var lstItemEntradaEstoque = itens.Select(i => new ItemEntradaEstoque()
            {
                CodEntradaEstoque = codEntradaEstoque,
                CodBitola = i.BitolaVo.CodBitola,
                CodMateriaPrima = i.MateriaPrimaVo.CodMateriaPrima,
                Lote = i.Lote,
                Certificado = i.Certificado,
                Corrida = i.Corrida,
                QtdPedidoCompra = i.QtdPedidoCompra,
                Qtd = i.Qtd,
                Especificacao = i.Especificacao,
                IPI =  i.Ipi,
                CodUnidade = i.UnidadeVo.CodUnidade,
                Valor = i.Valor,
                DataCadastro = i.DataCadastro,
                UsuarioInc = i.CodUsuarioInc,
                C = i.C,
                Si = i.Si,
                Mn = i.Mn,
                P = i.P,
                S = i.S,
                Cr = i.Cr,
                Ni = i.Ni,
                Mo = i.Mo,
                Cu = i.Cu,
                Ti = i.Ti,
                N2 = i.N2,
                Co = i.Co,
                Al = i.Al,
                Resistencia = i.Resistencia,
                Dureza = i.Dureza,
                Nota = i.Nota,
                CertificadoScanneado = i.CertificadoScanneado,
                Observação = i.Observacao
            });
            foreach (ItemEntradaEstoque itemEntradaEstoque in lstItemEntradaEstoque)
            {
                if (itemEntradaEstoque.Qtd != 0 && itemEntradaEstoque.Valor != 0)
                {
                    _repositorioDataContext.ItemEntradaEstoques.InsertOnSubmit(itemEntradaEstoque);
                    _repositorioDataContext.SubmitChanges();
                }
            }

        }

        internal void Incluir(List<Model.ItemEntradaEstoqueInsumoVO> itens, int codEntradaEstoque)
        {
            var lstItemEntradaEstoque = itens.Select(i => new ItemEntradaEstoque()
            {
                CodEntradaEstoque = codEntradaEstoque,
                CodMateriaPrima = i.ProdutoInsumoVo.CodProdutoInsumo,
                Lote = i.Lote,
                CodBitola = null,
                Certificado = i.Certificado,
                Corrida = i.Corrida,
                QtdPedidoCompra = i.QtdPedidoCompra,
                Qtd = i.Qtd,
                Especificacao = i.Especificacao,
                IPI = i.Ipi,
                CodUnidade = i.UnidadeVo.CodUnidade,
                Valor = i.Valor,
                DataCadastro = i.DataCadastro,
                UsuarioInc = i.UsuarioInc,
                Nota = i.Nota,
                CertificadoScanneado = i.CertificadoScanneado,
                Observação = i.Observacao
            });
            foreach (ItemEntradaEstoque itemEntradaEstoque in lstItemEntradaEstoque)
            {
                if (itemEntradaEstoque.Qtd != 0 && itemEntradaEstoque.Valor != 0)
                {
                    _repositorioDataContext.ItemEntradaEstoques.InsertOnSubmit(itemEntradaEstoque);
                    _repositorioDataContext.SubmitChanges();
                }
            }
        }

        internal void Alterar(List<Model.ItemEntradaEstoqueVO> itens, int codEntradaEstoque)
        {
             var lstItemEntradaEstoque = itens.Where(i => i.CodItemEntradaEstoque == 0).Select(i => new ItemEntradaEstoque()
                        {
                            CodEntradaEstoque = codEntradaEstoque,
                            CodBitola = i.BitolaVo.CodBitola,
                            CodMateriaPrima = i.MateriaPrimaVo.CodMateriaPrima,
                            Lote = i.Lote,
                            Certificado = i.Certificado,
                            Corrida = i.Corrida,
                            QtdPedidoCompra = i.QtdPedidoCompra,
                            Qtd = i.Qtd,
                            Especificacao = i.Especificacao,
                            IPI = i.Ipi,
                            CodUnidade = i.UnidadeVo.CodUnidade,
                            Valor = i.Valor,
                            DataCadastro = i.DataCadastro,
                            UsuarioInc = i.CodUsuarioAlt,
                            C = i.C,
                            Si = i.Si,
                            Mn = i.Mn,
                            P = i.P,
                            S = i.S,
                            Cr = i.Cr,
                            Ni = i.Ni,
                            Mo = i.Mo,
                            Cu = i.Cu,
                            Ti = i.Ti,
                            N2 = i.N2,
                            Co = i.Co,
                            Al = i.Al,
                            Resistencia = i.Resistencia,
                            Dureza = i.Dureza,
                            Nota = i.Nota,
                            CertificadoScanneado = i.CertificadoScanneado,
                            Observação = i.Observacao
                        });
                        if (lstItemEntradaEstoque.Count() > 0)
                            _repositorioDataContext.ItemEntradaEstoques.InsertAllOnSubmit(lstItemEntradaEstoque);
                        foreach (var item in itens)
                        {
                            if (item.CodItemEntradaEstoque > 0)
                            {
                                IQueryable<ItemEntradaEstoque> query = from i in _repositorioDataContext.ItemEntradaEstoques
                                                                      where i.CodItemEntradaEstoque == item.CodItemEntradaEstoque
                                                                      select i;
                                var itemEntradaEstoque = query.FirstOrDefault();
                                itemEntradaEstoque.CodBitola = item.BitolaVo.CodBitola;
                                itemEntradaEstoque.CodMateriaPrima = item.MateriaPrimaVo.CodMateriaPrima;
                                itemEntradaEstoque.Lote = item.Lote;
                                itemEntradaEstoque.Certificado = item.Certificado;
                                itemEntradaEstoque.Corrida = item.Corrida;
                                itemEntradaEstoque.QtdPedidoCompra = item.QtdPedidoCompra;
                                itemEntradaEstoque.Qtd = item.Qtd;
                                itemEntradaEstoque.Especificacao = item.Especificacao;
                                itemEntradaEstoque.IPI = item.Ipi;
                                itemEntradaEstoque.CodUnidade = item.UnidadeVo.CodUnidade;
                                itemEntradaEstoque.Valor = item.Valor;
                                itemEntradaEstoque.DataAlteracao = DateTime.Now;
                                itemEntradaEstoque.UsuarioAlt = item.CodUsuarioAlt;
                                itemEntradaEstoque.C = item.C;
                                itemEntradaEstoque.Si = item.Si;
                                itemEntradaEstoque.Mn = item.Mn;
                                itemEntradaEstoque.P = item.P;
                                itemEntradaEstoque.S = item.S;
                                itemEntradaEstoque.Cr = item.Cr;
                                itemEntradaEstoque.Ni = item.Ni;
                                itemEntradaEstoque.Mo = item.Mo;
                                itemEntradaEstoque.Cu = item.Cu;
                                itemEntradaEstoque.Ti = item.Ti;
                                itemEntradaEstoque.N2 = item.N2;
                                itemEntradaEstoque.Co = item.Co;
                                itemEntradaEstoque.Al = item.Al;
                                itemEntradaEstoque.Resistencia = item.Resistencia;
                                itemEntradaEstoque.Dureza = item.Dureza;
                                itemEntradaEstoque.Nota = item.Nota;
                                itemEntradaEstoque.CertificadoScanneado = item.CertificadoScanneado;
                            }
                        _repositorioDataContext.SubmitChanges();
                    }           
        }
    #region Método de Exclusao
        public void Excluir(int codItemEntradaEstoque)
        {
                IQueryable<ItemEntradaEstoque> query = from i in _repositorioDataContext.ItemEntradaEstoques
                                                      where i.CodEntradaEstoque == codItemEntradaEstoque
                                                      select i;
                var itemEntradaEstoque = query.FirstOrDefault();
                _repositorioDataContext.ItemEntradaEstoques.DeleteOnSubmit(itemEntradaEstoque);
                _repositorioDataContext.SubmitChanges();
        }
        internal void ExcluirTodos(int codEntradaEstoque)
        {
            IQueryable<ItemEntradaEstoque> query = from i in _repositorioDataContext.ItemEntradaEstoques
                                                       where i.CodEntradaEstoque == codEntradaEstoque
                                                       select i;
            if (query.Count() > 0)
            {
                var itemEntradaEstoque = query.ToList();
                _repositorioDataContext.ItemEntradaEstoques.DeleteAllOnSubmit(itemEntradaEstoque);
                _repositorioDataContext.SubmitChanges();
            }
        }
    #endregion

        internal void Alterar(List<Model.ItemEntradaEstoqueInsumoVO> itens, int codEntradaEstoque)
        {
            var lstItemEntradaEstoque = itens.Where(i => i.CodItemEntradaEstoqueInsumo == 0).Select(i => new ItemEntradaEstoque()
            {
                CodEntradaEstoque = codEntradaEstoque,
                CodMateriaPrima = i.ProdutoInsumoVo.CodProdutoInsumo,
                Lote = i.Lote,
                Certificado = i.Certificado,
                Corrida = i.Corrida,
                QtdPedidoCompra = i.QtdPedidoCompra,
                Qtd = i.Qtd,
                Especificacao = i.Especificacao,
                IPI = i.Ipi,
                CodUnidade = i.UnidadeVo.CodUnidade,
                Valor = i.Valor,
                DataCadastro = i.DataCadastro,
                UsuarioInc = i.UsuarioInc,
                Nota = i.Nota,
                CertificadoScanneado = i.CertificadoScanneado,
                Observação = i.Observacao
            });
            if (lstItemEntradaEstoque.Count() > 0)
                _repositorioDataContext.ItemEntradaEstoques.InsertAllOnSubmit(lstItemEntradaEstoque);
            foreach (var item in itens)
            {
                if (item.CodItemEntradaEstoqueInsumo > 0)
                {
                    IQueryable<ItemEntradaEstoque> query = from i in _repositorioDataContext.ItemEntradaEstoques
                                                           where i.CodItemEntradaEstoque == item.CodItemEntradaEstoqueInsumo
                                                           select i;
                    var itemEntradaEstoque = query.FirstOrDefault();
                    itemEntradaEstoque.CodBitola = null;
                    itemEntradaEstoque.CodMateriaPrima = item.ProdutoInsumoVo.CodProdutoInsumo;
                    itemEntradaEstoque.Lote = item.Lote;
                    itemEntradaEstoque.Certificado = item.Certificado;
                    itemEntradaEstoque.Corrida = item.Corrida;
                    itemEntradaEstoque.QtdPedidoCompra = item.QtdPedidoCompra;
                    itemEntradaEstoque.Qtd = item.Qtd;
                    itemEntradaEstoque.Especificacao = item.Especificacao;
                    itemEntradaEstoque.IPI = item.Ipi;
                    itemEntradaEstoque.CodUnidade = item.UnidadeVo.CodUnidade;
                    itemEntradaEstoque.Valor = item.Valor;
                    itemEntradaEstoque.DataAlteracao = DateTime.Now;
                    itemEntradaEstoque.UsuarioAlt = item.UsuarioAlt;
                    itemEntradaEstoque.Nota = item.Nota;
                    itemEntradaEstoque.CertificadoScanneado = item.CertificadoScanneado;
                }
                _repositorioDataContext.SubmitChanges();
            }           

        }
    }
}
