using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.Model;

namespace Nissi.Repositorio
{
    public class PedidoCompraRepositorio
    {
        private readonly RepositorioDataContext _repositorioDataContext;
        public PedidoCompraRepositorio()
        {
            _repositorioDataContext = new RepositorioDataContext();
        }
        #region Métodos de Listagem
        public PedidoCompraVO ListarTudo(int codPedidoCompra)
        {
            var query = from p in _repositorioDataContext.PedidoCompras
                        join f in _repositorioDataContext.Pessoas
                            on p.CodFornecedor equals f.CodPessoa
                        join c in _repositorioDataContext.CEPs
                            on f.CodCep equals c.CodCEP
                        join b in _repositorioDataContext.Bairros
                            on c.CodBairro equals b.CodBairro
                        join ci in _repositorioDataContext.Cidades
                            on c.CodCidade equals ci.CodCidade
                        join fp in _repositorioDataContext.FormaPgtos
                            on p.CodFormaPgto equals fp.CodFormaPgto
                        where p.CodPedidoCompra == codPedidoCompra
                        select new PedidoCompraVO()
                                   {
                                       CodPedidoCompra = p.CodPedidoCompra,
                                       Fornecedor = new FornecedorVO()
                                                        {
                                                            CodPessoa = f.CodPessoa,
                                                            RazaoSocial = f.RazaoSocial,
                                                            CNPJ = f.CNPJ,
                                                            InscricaoEstadual = f.InscricaoEstadual,
                                                            Telefone = f.Telefone,
                                                            Fax = f.Fax,
                                                            Contato = f.Contato,
                                                            Email = f.Email,
                                                            Numero = f.Numero,
                                                            Complemento = f.Complemento,
                                                            Cep = new CEPVO()
                                                                      {
                                                                          CodCep = c.CodCEP,
                                                                          NomEndereco = c.NomLogrCEP,
                                                                          Bairro = new BairroVO()
                                                                                       {
                                                                                           NomBairro = b.NomBairro
                                                                                       },
                                                                          Cidade = new CidadeVO()
                                                                                       {
                                                                                           NomCidade = ci.NomCidade,
                                                                                           UF = new UnidadeFederacaoVO()
                                                                                                    {
                                                                                                        CodUF = ci.CodUF
                                                                                                    }
                                                                                       }
                                                                      }
                                                        },
                                       FormaPgto = new FormaPgtoVO()
                                                       {
                                                           CodFormaPgto = fp.CodFormaPgto,
                                                           Descricao = fp.Descricao,
                                                           Intervalo = fp.Intervalo,
                                                           Parcelas = fp.Parcelas
                                                       },
                                       CondicaoFornecimento = p.CondicaoFornecimento,
                                       DataEmissao = p.DataEmissao,
                                       DataPrazoEntrega = p.DataPrazoEntrega,
                                       Tipo = p.Tipo != null? (TypePedido)p.Tipo:0,
                                       FuncionarioComprador = (from fc in _repositorioDataContext.Pessoas
                                                               join e in _repositorioDataContext.Funcionarios
                                                               on fc.CodPessoa equals e.CodPessoa
                                                               where p.CodFuncionarioComprador == e.CodFuncionario
                                                               select new FuncionarioVO()
                                                                          {
                                                                              CodPessoa = e.CodFuncionario,
                                                                              Nome = fc.RazaoSocial
                                                                          }).FirstOrDefault(),
                                       FuncionarioAprovador = (from fa in _repositorioDataContext.Pessoas
                                                               join e in _repositorioDataContext.Funcionarios
                                                               on fa.CodPessoa equals e.CodPessoa
                                                               where p.CodFuncionarioAprovador == e.CodFuncionario
                                                               select new FuncionarioVO()
                                                                          {
                                                                              CodPessoa = e.CodFuncionario,
                                                                              Nome = fa.RazaoSocial
                                                                          }).FirstOrDefault(),
                                    TipoRetirada = p.TipoRetirada,
                                    Observacao = p.Observacao,
                                    ItemPedidoCompraVo = (from i in _repositorioDataContext.ItemPedidoCompras
                                                         where i.CodPedidoCompra == codPedidoCompra
                                                         select new ItemPedidoCompraVO()
                                                                    {
                                                                        CodItemPedidoCompra = i.CodItemPedidoCompra,
                                                                        MateriaPrimaVo = (from m in _repositorioDataContext.MateriaPrimas
                                                                                          join n in _repositorioDataContext.Normas
                                                                                              on m.CodNorma equals (n.CodNorma)
                                                                                          where m.CodMateriaPrima == i.CodMateriaPrima
                                                                                          select new MateriaPrimaVO()
                                                                                          {
                                                                                              CodMateriaPrima = m.CodMateriaPrima,
                                                                                              ClasseTipoVo = (from ct in _repositorioDataContext.ClasseTipos
                                                                                                              where ct.CodClasseTipo == m.CodClasseTipo
                                                                                                              select new ClasseTipoVO()
                                                                                                              {
                                                                                                                  CodClasseTipo = ct.CodClasseTipo,
                                                                                                                  Descricao = ct.Descricao
                                                                                                              }).FirstOrDefault(),
                                                                                              NormaVo = new NormaVO()
                                                                                              {
                                                                                                  CodNorma = n.CodNorma,
                                                                                                  Descricao = n.Descricao,
                                                                                                  Revisao = n.Revisao
                                                                                              }
                                                                                          }).FirstOrDefault(),
                                                                        BitolaVo = (from bi in _repositorioDataContext.Bitolas
                                                                                    where bi.CodBitola == i.CodBitola
                                                                                    select new BitolaVO()
                                                                                    {
                                                                                        CodBitola = bi.CodBitola,
                                                                                        Bitola = bi.Bitola1,
                                                                                    }).FirstOrDefault(),
                                                                        Qtd = i.Qtd,
                                                                        ResistenciaTracao = i.ResistenciaTracao,
                                                                        Especificacao = i.Especificacao,
                                                                        Ipi = i.IPI,
                                                                        UnidadeVo = (from u in _repositorioDataContext.Unidades
                                                                                         where u.CodUnidade == i.CodUnidade
                                                                                         select new UnidadeVO()
                                                                                                    {
                                                                                                        CodUnidade = u.CodUnidade,
                                                                                                        Descricao = u.Descricao,
                                                                                                        TipoUnidade = u.Unidade1
                                                                                                    }).FirstOrDefault(),
                                                                        Valor = i.Valor,

                                                                    }).ToList()
                                   };
            var pedidoCompra = query.FirstOrDefault();
            return pedidoCompra;
        }
        public PedidoCompraVO ListarTudoEstoque(int codPedidoCompra)
        {
            var query = from p in _repositorioDataContext.PedidoCompras
                        join f in _repositorioDataContext.Pessoas
                            on p.CodFornecedor equals f.CodPessoa
                        join c in _repositorioDataContext.CEPs
                            on f.CodCep equals c.CodCEP
                        join b in _repositorioDataContext.Bairros
                            on c.CodBairro equals b.CodBairro
                        join ci in _repositorioDataContext.Cidades
                            on c.CodCidade equals ci.CodCidade
                        join fp in _repositorioDataContext.FormaPgtos
                            on p.CodFormaPgto equals fp.CodFormaPgto
                        where p.CodPedidoCompra == codPedidoCompra
                        select new PedidoCompraVO()
                        {
                            CodPedidoCompra = p.CodPedidoCompra,
                            Fornecedor = new FornecedorVO()
                            {
                                CodPessoa = f.CodPessoa,
                                RazaoSocial = f.RazaoSocial,
                                CNPJ = f.CNPJ,
                                InscricaoEstadual = f.InscricaoEstadual,
                                Telefone = f.Telefone,
                                Fax = f.Fax,
                                Contato = f.Contato,
                                Email = f.Email,
                                Numero = f.Numero,
                                Complemento = f.Complemento,
                                Cep = new CEPVO()
                                {
                                    CodCep = c.CodCEP,
                                    NomEndereco = c.NomLogrCEP,
                                    Bairro = new BairroVO()
                                    {
                                        NomBairro = b.NomBairro
                                    },
                                    Cidade = new CidadeVO()
                                    {
                                        NomCidade = ci.NomCidade,
                                        UF = new UnidadeFederacaoVO()
                                        {
                                            CodUF = ci.CodUF
                                        }
                                    }
                                }
                            },
                            FormaPgto = new FormaPgtoVO()
                            {
                                CodFormaPgto = fp.CodFormaPgto,
                                Descricao = fp.Descricao,
                                Intervalo = fp.Intervalo,
                                Parcelas = fp.Parcelas
                            },
                            CondicaoFornecimento = p.CondicaoFornecimento,
                            DataEmissao = p.DataEmissao,
                            DataPrazoEntrega = p.DataPrazoEntrega,
                            Tipo = p.Tipo != null ? (TypePedido)p.Tipo : 0,
                            FuncionarioComprador = (from fc in _repositorioDataContext.Pessoas
                                                    join e in _repositorioDataContext.Funcionarios
                                                    on fc.CodPessoa equals e.CodPessoa
                                                    where p.CodFuncionarioComprador == e.CodFuncionario
                                                    select new FuncionarioVO()
                                                    {
                                                        CodPessoa = e.CodFuncionario,
                                                        Nome = fc.RazaoSocial
                                                    }).FirstOrDefault(),
                            FuncionarioAprovador = (from fa in _repositorioDataContext.Pessoas
                                                    join e in _repositorioDataContext.Funcionarios
                                                    on fa.CodPessoa equals e.CodPessoa
                                                    where p.CodFuncionarioAprovador == e.CodFuncionario
                                                    select new FuncionarioVO()
                                                    {
                                                        CodPessoa = e.CodFuncionario,
                                                        Nome = fa.RazaoSocial
                                                    }).FirstOrDefault(),
                            TipoRetirada = p.TipoRetirada,
                            Observacao = p.Observacao,
                            ItemPedidoCompraVo = (from i in _repositorioDataContext.ItemPedidoCompras
                                                  where i.CodPedidoCompra == codPedidoCompra
                                                  select new ItemPedidoCompraVO()
                                                  {
                                                      CodItemPedidoCompra = i.CodItemPedidoCompra,
                                                      MateriaPrimaVo = (p.Tipo == 0
                                                                          ? (from m in _repositorioDataContext.MateriaPrimas
                                                                             join n in _repositorioDataContext.Normas
                                                                                 on m.CodNorma equals (n.CodNorma)
                                                                             where m.CodMateriaPrima == i.CodMateriaPrima
                                                                             select new MateriaPrimaVO()
                                                                             {
                                                                                 CodMateriaPrima = m.CodMateriaPrima,
                                                                                 ClasseTipoVo = (from ct in _repositorioDataContext.ClasseTipos
                                                                                                 where ct.CodClasseTipo == m.CodClasseTipo
                                                                                                 select new ClasseTipoVO()
                                                                                                 {
                                                                                                     CodClasseTipo = m.CodClasseTipo,
                                                                                                     Descricao = ct.Descricao
                                                                                                 }).FirstOrDefault(),
                                                                                 NormaVo = new NormaVO()
                                                                                 {
                                                                                     CodNorma = m.CodNorma,
                                                                                     Descricao = n.Descricao,
                                                                                     Revisao = n.Revisao
                                                                                 }
                                                                             }).FirstOrDefault() : (from pi in _repositorioDataContext.ProdutoInsumos
                                                                                                    where pi.CodProdutoInsumo == i.CodMateriaPrima
                                                                                                    select new MateriaPrimaVO()
                                                                                                    {
                                                                                                        CodMateriaPrima = pi.CodProdutoInsumo,
                                                                                                        DescricaoInsumo = pi.Descricao,
                                                                                                    }).FirstOrDefault()),
                                                      BitolaVo = (p.Tipo == 0
                                                                          ? (from bi in _repositorioDataContext.Bitolas
                                                                             where bi.CodBitola == i.CodBitola
                                                                             select new BitolaVO()
                                                                             {
                                                                                 CodBitola = bi.CodBitola,
                                                                                 Bitola = bi.Bitola1,
                                                                             }).FirstOrDefault() : new BitolaVO()
                                                                             {
                                                                                 CodBitola = 0,
                                                                                 Bitola = 0
                                                                             }),
                                                      Qtd = i.Qtd,
                                                      QtdeEntregue = (from pe in _repositorioDataContext.EntradaEstoques
                                                                    join ie in _repositorioDataContext.ItemEntradaEstoques
                                                                    on pe.CodEntradaEstoque equals (ie.CodEntradaEstoque)
                                                                    where pe.CodPedidoCompra == p.CodPedidoCompra
                                                                    && ie.CodMateriaPrima == i.CodMateriaPrima
                                                                    && ie.CodBitola == i.CodBitola
                                                                    select ie.Qtd).Sum(),
                                                      ResistenciaTracao = i.ResistenciaTracao,
                                                      Especificacao = i.Especificacao,
                                                      Ipi = i.IPI,
                                                      UnidadeVo = (from u in _repositorioDataContext.Unidades
                                                                   where u.CodUnidade == i.CodUnidade
                                                                   select new UnidadeVO()
                                                                   {
                                                                       CodUnidade = u.CodUnidade,
                                                                       Descricao = u.Descricao,
                                                                       TipoUnidade = u.Unidade1
                                                                   }).FirstOrDefault(),
                                                      Valor = i.Valor,

                                                  }).ToList()
                        };
            var pedidoCompra = query.FirstOrDefault();
            return pedidoCompra;
        }
        public PedidoCompraInsumoVO ListarTudoInsumo(int codPedidoCompra)
        {
            var query = from p in _repositorioDataContext.PedidoCompras
                        join f in _repositorioDataContext.Pessoas
                            on p.CodFornecedor equals f.CodPessoa
                        join c in _repositorioDataContext.CEPs
                            on f.CodCep equals c.CodCEP
                        join b in _repositorioDataContext.Bairros
                            on c.CodBairro equals b.CodBairro
                        join ci in _repositorioDataContext.Cidades
                            on c.CodCidade equals ci.CodCidade
                        join fp in _repositorioDataContext.FormaPgtos
                            on p.CodFormaPgto equals fp.CodFormaPgto
                        where p.CodPedidoCompra == codPedidoCompra
                        select new PedidoCompraInsumoVO()
                        {
                            CodPedidoCompraInsumo = p.CodPedidoCompra,
                            Fornecedor = new FornecedorVO()
                            {
                                CodPessoa = f.CodPessoa,
                                RazaoSocial = f.RazaoSocial,
                                CNPJ = f.CNPJ,
                                InscricaoEstadual = f.InscricaoEstadual,
                                Telefone = f.Telefone,
                                Fax = f.Fax,
                                Contato = f.Contato,
                                Email = f.Email,
                                Numero = f.Numero,
                                Complemento = f.Complemento,
                                Cep = new CEPVO()
                                {
                                    CodCep = c.CodCEP,
                                    NomEndereco = c.NomLogrCEP,
                                    Bairro = new BairroVO()
                                    {
                                        NomBairro = b.NomBairro
                                    },
                                    Cidade = new CidadeVO()
                                    {
                                        NomCidade = ci.NomCidade,
                                        UF = new UnidadeFederacaoVO()
                                        {
                                            CodUF = ci.CodUF
                                        }
                                    }
                                }
                            },
                            FormaPgto = new FormaPgtoVO()
                            {
                                CodFormaPgto = fp.CodFormaPgto,
                                Descricao = fp.Descricao,
                                Intervalo = fp.Intervalo,
                                Parcelas = fp.Parcelas
                            },
                            CondicaoFornecimento = p.CondicaoFornecimento,
                            DataEmissao = p.DataEmissao,
                            DataPrazoEntrega = p.DataPrazoEntrega,
                            Tipo = p.Tipo != null ? (TypePedido)p.Tipo : 0,
                            FuncionarioComprador = (from fc in _repositorioDataContext.Pessoas
                                                    join e in _repositorioDataContext.Funcionarios
                                                    on fc.CodPessoa equals e.CodPessoa
                                                    where p.CodFuncionarioComprador == e.CodFuncionario
                                                    select new FuncionarioVO()
                                                    {
                                                        CodPessoa = e.CodFuncionario,
                                                        Nome = fc.RazaoSocial
                                                    }).FirstOrDefault(),
                            FuncionarioAprovador = (from fa in _repositorioDataContext.Pessoas
                                                    join e in _repositorioDataContext.Funcionarios
                                                    on fa.CodPessoa equals e.CodPessoa
                                                    where p.CodFuncionarioAprovador == e.CodFuncionario
                                                    select new FuncionarioVO()
                                                    {
                                                        CodPessoa = e.CodFuncionario,
                                                        Nome = fa.RazaoSocial
                                                    }).FirstOrDefault(),
                            TipoRetirada = p.TipoRetirada,
                            Observacao = p.Observacao,
                            ItemPedidoCompraInsumoVo = (from i in _repositorioDataContext.ItemPedidoCompras
                                                        where i.CodPedidoCompra == codPedidoCompra
                                                        select new ItemPedidoCompraInsumoVO()
                                                        {
                                                            CodItemPedidoCompraInsumo = i.CodItemPedidoCompra,
                                                            ProdutoInsumoVo = (from m in _repositorioDataContext.ProdutoInsumos
                                                                               where m.CodProdutoInsumo == i.CodMateriaPrima
                                                                               select new ProdutoInsumoVO()
                                                                               {
                                                                                   CodProdutoInsumo = m.CodProdutoInsumo,
                                                                                   Descricao = m.Descricao
                                                                               }).FirstOrDefault(),
                                                            Qtd = i.Qtd,
                                                            ResistenciaTracao = i.ResistenciaTracao,
                                                            Especificacao = i.Especificacao,
                                                            Ipi = i.IPI,
                                                            UnidadeVo = (from u in _repositorioDataContext.Unidades
                                                                         where u.CodUnidade == i.CodUnidade
                                                                         select new UnidadeVO()
                                                                         {
                                                                             CodUnidade = u.CodUnidade,
                                                                             Descricao = u.Descricao,
                                                                             TipoUnidade = u.Unidade1
                                                                         }).FirstOrDefault(),
                                                            Valor = i.Valor,

                                                        }).ToList()
                        };
            var pedidoCompra = query.FirstOrDefault();
            return pedidoCompra;
        }
        public List<ListItemPedidoCompraVO> ListarPorCodigo(int codPedidoCompra)
        {
            var queryitem = (from i in _repositorioDataContext.ItemPedidoCompras
                             join p in _repositorioDataContext.PedidoCompras
                                 on i.CodPedidoCompra equals p.CodPedidoCompra
                             join f in _repositorioDataContext.Pessoas
                                 on p.CodFornecedor equals f.CodPessoa
                             where p.CodPedidoCompra == codPedidoCompra
                             select new ListItemPedidoCompraVO()
                                        {
                                            OrdemCompra = p.CodPedidoCompra,
                                            DataEmissao = p.DataEmissao,
                                            CodPessoa = p.CodFornecedor,
                                            DataPrevista = p.DataPrazoEntrega,
                                            Fornecedor = f.RazaoSocial,
                                            Tipo = p.Tipo?? 0,
                                            CodBitola = i.CodBitola,
                                            CodMateriaPrima = i.CodMateriaPrima,
                                            MateriaPrimaVo = (p.Tipo == 0
                                                                ? (from m in _repositorioDataContext.MateriaPrimas
                                                                   join n in _repositorioDataContext.Normas
                                                                       on m.CodNorma equals (n.CodNorma)
                                                                   where m.CodMateriaPrima == i.CodMateriaPrima
                                                                   select new MateriaPrimaVO()
                                                                   {
                                                                       ClasseTipoVo = (from ct in _repositorioDataContext.ClasseTipos
                                                                                       where ct.CodClasseTipo == m.CodClasseTipo
                                                                                       select new ClasseTipoVO()
                                                                                       {
                                                                                           Descricao = ct.Descricao
                                                                                       }).FirstOrDefault(),
                                                                       NormaVo = new NormaVO()
                                                                       {
                                                                           Descricao = n.Descricao,
                                                                           Revisao = n.Revisao
                                                                       }
                                                                   }).FirstOrDefault() : (from pi in _repositorioDataContext.ProdutoInsumos
                                                                                          where pi.CodProdutoInsumo == i.CodMateriaPrima
                                                                                          select new MateriaPrimaVO()
                                                                                          {
                                                                                              DescricaoInsumo = pi.Descricao
                                                                                          }).FirstOrDefault()),
                                            Bitola = p.Tipo == 0
                                                                ? Convert.ToDecimal((from b in _repositorioDataContext.Bitolas
                                                                                     where b.CodBitola == i.CodBitola
                                                                                     select new BitolaVO()
                                                                                     {
                                                                                         Bitola = b.Bitola1
                                                                                     }).FirstOrDefault().Bitola) : decimal.Zero,
                                            Ipi = i.IPI,
                                            Preco = i.Valor,
                                            Qtde = i.Qtd,
                                            Unidade = (from un in _repositorioDataContext.Unidades
                                                       where un.CodUnidade == i.CodUnidade
                                                       select un).FirstOrDefault().Unidade1,
                                            QtdeEntregue = p.Tipo == 0 ? (from pe in _repositorioDataContext.EntradaEstoques
                                                                          join ie in _repositorioDataContext.ItemEntradaEstoques
                                                                          on pe.CodEntradaEstoque equals (ie.CodEntradaEstoque)
                                                                          where pe.CodPedidoCompra == p.CodPedidoCompra
                                                                          && ie.CodMateriaPrima == i.CodMateriaPrima
                                                                          && ie.CodBitola == i.CodBitola
                                                                          select ie.Qtd).Sum() : (from pe in _repositorioDataContext.EntradaEstoques
                                                                                                  join ie in _repositorioDataContext.ItemEntradaEstoques
                                                                                                  on pe.CodEntradaEstoque equals (ie.CodEntradaEstoque)
                                                                                                  where pe.CodPedidoCompra == p.CodPedidoCompra
                                                                                                  && ie.CodMateriaPrima == i.CodMateriaPrima
                                                                                                  select ie.Qtd).Sum(),
                                            DataEntrega = (from pe in _repositorioDataContext.EntradaEstoques
                                                           where pe.CodPedidoCompra == p.CodPedidoCompra
                                                           select new EntradaEstoqueVO()
                                                           {
                                                               DataEntrada = pe.DataEntrada
                                                           }).FirstOrDefault().DataEntrada,
                                            NotaFiscal = (from pe in _repositorioDataContext.EntradaEstoques
                                                          where pe.CodPedidoCompra == p.CodPedidoCompra
                                                          select new EntradaEstoqueVO()
                                                          {
                                                              NotaFiscal = pe.NotaFiscal
                                                          }).FirstOrDefault().NotaFiscal,
                                            Lote = p.Tipo == 0 ? (from pe in _repositorioDataContext.EntradaEstoques
                                                                  join ie in _repositorioDataContext.ItemEntradaEstoques
                                                                  on pe.CodEntradaEstoque equals (ie.CodEntradaEstoque)
                                                                  where pe.CodPedidoCompra == p.CodPedidoCompra
                                                                  && ie.CodMateriaPrima == i.CodMateriaPrima
                                                                  && ie.CodBitola == i.CodBitola
                                                                  select new ItemEntradaEstoqueVO
                                                                  {
                                                                      Lote = ie.Lote
                                                                  }).FirstOrDefault().Lote : 0

                                 }).ToList();

            var lstItemPedidoCompra = new List<ListItemPedidoCompraVO>();
            if (queryitem.Count() > 0)
                lstItemPedidoCompra = queryitem.ToList();
                return lstItemPedidoCompra;
        }
        public List<ListItemPedidoCompraVO> ListarPorData(DateTime dataInicio, DateTime dataFim)
        {
            var queryitem = (from i in _repositorioDataContext.ItemPedidoCompras
                             join p in _repositorioDataContext.PedidoCompras
                                 on i.CodPedidoCompra equals p.CodPedidoCompra
                             join f in _repositorioDataContext.Pessoas
                                 on p.CodFornecedor equals f.CodPessoa
                             where p.DataEmissao >= dataInicio && p.DataEmissao <= dataFim
                             select new ListItemPedidoCompraVO()
                             {
                                 OrdemCompra = p.CodPedidoCompra,
                                 DataEmissao = p.DataEmissao,
                                 CodPessoa = p.CodFornecedor,
                                 DataPrevista = p.DataPrazoEntrega,
                                 Fornecedor = f.RazaoSocial,
                                 Tipo = p.Tipo ?? 0,
                                 CodBitola = i.CodBitola,
                                 CodMateriaPrima = i.CodMateriaPrima,
                                 MateriaPrimaVo = (p.Tipo == 0
                                                     ? (from m in _repositorioDataContext.MateriaPrimas
                                                        join n in _repositorioDataContext.Normas
                                                            on m.CodNorma equals (n.CodNorma)
                                                        where m.CodMateriaPrima == i.CodMateriaPrima
                                                        select new MateriaPrimaVO()
                                                        {
                                                            ClasseTipoVo = (from ct in _repositorioDataContext.ClasseTipos
                                                                            where ct.CodClasseTipo == m.CodClasseTipo
                                                                            select new ClasseTipoVO()
                                                                            {
                                                                                Descricao = ct.Descricao
                                                                            }).FirstOrDefault(),
                                                            NormaVo = new NormaVO()
                                                            {
                                                                Descricao = n.Descricao,
                                                                Revisao = n.Revisao
                                                            }
                                                        }).FirstOrDefault() : (from pi in _repositorioDataContext.ProdutoInsumos
                                                                               where pi.CodProdutoInsumo == i.CodMateriaPrima
                                                                               select new MateriaPrimaVO()
                                                                               {
                                                                                   DescricaoInsumo = pi.Descricao
                                                                               }).FirstOrDefault()),
                                 Bitola = p.Tipo == 0
                                                     ? Convert.ToDecimal((from b in _repositorioDataContext.Bitolas
                                                                          where b.CodBitola == i.CodBitola
                                                                          select new BitolaVO()
                                                                          {
                                                                              Bitola = b.Bitola1
                                                                          }).FirstOrDefault().Bitola) : decimal.Zero,
                                 Ipi = i.IPI,
                                 Preco = i.Valor,
                                 Qtde = i.Qtd,
                                 Unidade = (from un in _repositorioDataContext.Unidades
                                                where un.CodUnidade == i.CodUnidade
                                                select un).FirstOrDefault().Unidade1,
                                 QtdeEntregue = p.Tipo == 0?(from pe in _repositorioDataContext.EntradaEstoques
                                                 join ie in _repositorioDataContext.ItemEntradaEstoques
                                                 on pe.CodEntradaEstoque equals (ie.CodEntradaEstoque)
                                                 where pe.CodPedidoCompra == p.CodPedidoCompra
                                                 && ie.CodMateriaPrima == i.CodMateriaPrima
                                                 && ie.CodBitola == i.CodBitola
                                                             select ie.Qtd).Sum() : (from pe in _repositorioDataContext.EntradaEstoques
                                                                                     join ie in _repositorioDataContext.ItemEntradaEstoques
                                                                                     on pe.CodEntradaEstoque equals (ie.CodEntradaEstoque)
                                                                                     where pe.CodPedidoCompra == p.CodPedidoCompra
                                                                                     && ie.CodMateriaPrima == i.CodMateriaPrima
                                                                                     select ie.Qtd).Sum(),
                                 DataEntrega = (from pe in _repositorioDataContext.EntradaEstoques
                                                where pe.CodPedidoCompra == p.CodPedidoCompra
                                                select new EntradaEstoqueVO()
                                                {
                                                    DataEntrada = pe.DataEntrada
                                                }).FirstOrDefault().DataEntrada,
                                 NotaFiscal = (from pe in _repositorioDataContext.EntradaEstoques
                                                where pe.CodPedidoCompra == p.CodPedidoCompra
                                                select new EntradaEstoqueVO()
                                                {
                                                    NotaFiscal = pe.NotaFiscal
                                                }).FirstOrDefault().NotaFiscal,
                                 Lote = p.Tipo == 0?(from pe in _repositorioDataContext.EntradaEstoques
                                                 join ie in _repositorioDataContext.ItemEntradaEstoques
                                                 on pe.CodEntradaEstoque equals (ie.CodEntradaEstoque)
                                                 where pe.CodPedidoCompra == p.CodPedidoCompra
                                                 && ie.CodMateriaPrima == i.CodMateriaPrima
                                                 && ie.CodBitola == i.CodBitola
                                                             select new ItemEntradaEstoqueVO
                                                                        {
                                                                            Lote = ie.Lote
                                                                        }).FirstOrDefault().Lote: 0

                             }).ToList();

            var lstItemPedidoCompra = new List<ListItemPedidoCompraVO>();
            if (queryitem.Count() > 0)
                lstItemPedidoCompra = queryitem.ToList();
            return lstItemPedidoCompra;
        }
        public List<ListItemPedidoCompraVO> ListarPorFornecedor(int codFornecedor)
        {
            var queryitem = (from i in _repositorioDataContext.ItemPedidoCompras
                             join p in _repositorioDataContext.PedidoCompras
                                 on i.CodPedidoCompra equals p.CodPedidoCompra
                             join f in _repositorioDataContext.Pessoas
                                 on p.CodFornecedor equals f.CodPessoa
                             where p.CodFornecedor == codFornecedor
                             select new ListItemPedidoCompraVO()
                             {
                                 OrdemCompra = p.CodPedidoCompra,
                                 CodPessoa = p.CodFornecedor,
                                 DataEmissao = p.DataEmissao,
                                 DataPrevista = p.DataPrazoEntrega,
                                 Fornecedor = f.RazaoSocial,
                                 Tipo = p.Tipo ?? 0,
                                 CodBitola = i.CodBitola,
                                 CodMateriaPrima = i.CodMateriaPrima,
                                 MateriaPrimaVo = (p.Tipo == 0
                                                     ? (from m in _repositorioDataContext.MateriaPrimas
                                                        join n in _repositorioDataContext.Normas
                                                            on m.CodNorma equals (n.CodNorma)
                                                        where m.CodMateriaPrima == i.CodMateriaPrima
                                                        select new MateriaPrimaVO()
                                                        {
                                                            ClasseTipoVo = (from ct in _repositorioDataContext.ClasseTipos
                                                                            where ct.CodClasseTipo == m.CodClasseTipo
                                                                            select new ClasseTipoVO()
                                                                            {
                                                                                Descricao = ct.Descricao
                                                                            }).FirstOrDefault(),
                                                            NormaVo = new NormaVO()
                                                            {
                                                                Descricao = n.Descricao,
                                                                Revisao = n.Revisao
                                                            }
                                                        }).FirstOrDefault() : (from pi in _repositorioDataContext.ProdutoInsumos
                                                                               where pi.CodProdutoInsumo == i.CodMateriaPrima
                                                                               select new MateriaPrimaVO()
                                                                               {
                                                                                   DescricaoInsumo = pi.Descricao
                                                                               }).FirstOrDefault()),
                                 Bitola = p.Tipo == 0
                                                     ? Convert.ToDecimal((from b in _repositorioDataContext.Bitolas
                                                                          where b.CodBitola == i.CodBitola
                                                                          select new BitolaVO()
                                                                          {
                                                                              Bitola = b.Bitola1
                                                                          }).FirstOrDefault().Bitola) : decimal.Zero,
                                 Ipi = i.IPI,
                                 Preco = i.Valor,
                                 Qtde = i.Qtd,
                                 Unidade = (from un in _repositorioDataContext.Unidades
                                            where un.CodUnidade == i.CodUnidade
                                            select un).FirstOrDefault().Unidade1,
                                 QtdeEntregue = p.Tipo == 0 ? (from pe in _repositorioDataContext.EntradaEstoques
                                                               join ie in _repositorioDataContext.ItemEntradaEstoques
                                                               on pe.CodEntradaEstoque equals (ie.CodEntradaEstoque)
                                                               where pe.CodPedidoCompra == p.CodPedidoCompra
                                                               && ie.CodMateriaPrima == i.CodMateriaPrima
                                                               && ie.CodBitola == i.CodBitola
                                                               select ie.Qtd).Sum() : (from pe in _repositorioDataContext.EntradaEstoques
                                                                                       join ie in _repositorioDataContext.ItemEntradaEstoques
                                                                                       on pe.CodEntradaEstoque equals (ie.CodEntradaEstoque)
                                                                                       where pe.CodPedidoCompra == p.CodPedidoCompra
                                                                                       && ie.CodMateriaPrima == i.CodMateriaPrima
                                                                                       select ie.Qtd).Sum(),
                                 DataEntrega = (from pe in _repositorioDataContext.EntradaEstoques
                                                where pe.CodPedidoCompra == p.CodPedidoCompra
                                                select new EntradaEstoqueVO()
                                                {
                                                    DataEntrada = pe.DataEntrada
                                                }).FirstOrDefault().DataEntrada,
                                 NotaFiscal = (from pe in _repositorioDataContext.EntradaEstoques
                                               where pe.CodPedidoCompra == p.CodPedidoCompra
                                               select new EntradaEstoqueVO()
                                               {
                                                   NotaFiscal = pe.NotaFiscal
                                               }).FirstOrDefault().NotaFiscal,
                                 Lote = p.Tipo == 0 ? (from pe in _repositorioDataContext.EntradaEstoques
                                                       join ie in _repositorioDataContext.ItemEntradaEstoques
                                                       on pe.CodEntradaEstoque equals (ie.CodEntradaEstoque)
                                                       where pe.CodPedidoCompra == p.CodPedidoCompra
                                                       && ie.CodMateriaPrima == i.CodMateriaPrima
                                                       && ie.CodBitola == i.CodBitola
                                                       select new ItemEntradaEstoqueVO
                                                       {
                                                           Lote = ie.Lote
                                                       }).FirstOrDefault().Lote : 0
                             }).ToList();

            var lstItemPedidoCompra = new List<ListItemPedidoCompraVO>();
            if (queryitem.Count() > 0)
                lstItemPedidoCompra = queryitem.ToList();
            return lstItemPedidoCompra;
        }
        public List<ListItemPedidoCompraVO> ListarPorBitola(int codBitola)
        {
            var queryitem = (from i in _repositorioDataContext.ItemPedidoCompras
                             join p in _repositorioDataContext.PedidoCompras
                                 on i.CodPedidoCompra equals p.CodPedidoCompra
                             join f in _repositorioDataContext.Pessoas
                                 on p.CodFornecedor equals f.CodPessoa
                             where i.CodBitola == codBitola
                             select new ListItemPedidoCompraVO()
                             {
                                 OrdemCompra = p.CodPedidoCompra,
                                 CodPessoa = p.CodFornecedor,
                                 DataEmissao = p.DataEmissao,
                                 DataPrevista = p.DataPrazoEntrega,
                                 Fornecedor = f.RazaoSocial,
                                 Tipo = p.Tipo ?? 0,
                                 CodBitola = i.CodBitola,
                                 CodMateriaPrima = i.CodMateriaPrima,
                                 MateriaPrimaVo = (p.Tipo == 0
                                                     ? (from m in _repositorioDataContext.MateriaPrimas
                                                        join n in _repositorioDataContext.Normas
                                                            on m.CodNorma equals (n.CodNorma)
                                                        where m.CodMateriaPrima == i.CodMateriaPrima
                                                        select new MateriaPrimaVO()
                                                        {
                                                            ClasseTipoVo = (from ct in _repositorioDataContext.ClasseTipos
                                                                            where ct.CodClasseTipo == m.CodClasseTipo
                                                                            select new ClasseTipoVO()
                                                                            {
                                                                                Descricao = ct.Descricao
                                                                            }).FirstOrDefault(),
                                                            NormaVo = new NormaVO()
                                                            {
                                                                Descricao = n.Descricao,
                                                                Revisao = n.Revisao
                                                            }
                                                        }).FirstOrDefault() : (from pi in _repositorioDataContext.ProdutoInsumos
                                                                               where pi.CodProdutoInsumo == i.CodMateriaPrima
                                                                               select new MateriaPrimaVO()
                                                                               {
                                                                                   DescricaoInsumo = pi.Descricao
                                                                               }).FirstOrDefault()),
                                 Bitola = p.Tipo == 0
                                                     ? Convert.ToDecimal((from b in _repositorioDataContext.Bitolas
                                                                          where b.CodBitola == i.CodBitola
                                                                          select new BitolaVO()
                                                                          {
                                                                              Bitola = b.Bitola1
                                                                          }).FirstOrDefault().Bitola) : decimal.Zero,
                                 Ipi = i.IPI,
                                 Preco = i.Valor,
                                 Qtde = i.Qtd,
                                 Unidade = (from un in _repositorioDataContext.Unidades
                                            where un.CodUnidade == i.CodUnidade
                                            select un).FirstOrDefault().Unidade1,
                                 QtdeEntregue = p.Tipo == 0 ? (from pe in _repositorioDataContext.EntradaEstoques
                                                               join ie in _repositorioDataContext.ItemEntradaEstoques
                                                               on pe.CodEntradaEstoque equals (ie.CodEntradaEstoque)
                                                               where pe.CodPedidoCompra == p.CodPedidoCompra
                                                               && ie.CodMateriaPrima == i.CodMateriaPrima
                                                               && ie.CodBitola == i.CodBitola
                                                               select ie.Qtd).Sum() : (from pe in _repositorioDataContext.EntradaEstoques
                                                                                       join ie in _repositorioDataContext.ItemEntradaEstoques
                                                                                       on pe.CodEntradaEstoque equals (ie.CodEntradaEstoque)
                                                                                       where pe.CodPedidoCompra == p.CodPedidoCompra
                                                                                       && ie.CodMateriaPrima == i.CodMateriaPrima
                                                                                       select ie.Qtd).Sum(),
                                 DataEntrega = (from pe in _repositorioDataContext.EntradaEstoques
                                                where pe.CodPedidoCompra == p.CodPedidoCompra
                                                select new EntradaEstoqueVO()
                                                {
                                                    DataEntrada = pe.DataEntrada
                                                }).FirstOrDefault().DataEntrada,
                                 NotaFiscal = (from pe in _repositorioDataContext.EntradaEstoques
                                               where pe.CodPedidoCompra == p.CodPedidoCompra
                                               select new EntradaEstoqueVO()
                                               {
                                                   NotaFiscal = pe.NotaFiscal
                                               }).FirstOrDefault().NotaFiscal,
                                 Lote = p.Tipo == 0 ? (from pe in _repositorioDataContext.EntradaEstoques
                                                       join ie in _repositorioDataContext.ItemEntradaEstoques
                                                       on pe.CodEntradaEstoque equals (ie.CodEntradaEstoque)
                                                       where pe.CodPedidoCompra == p.CodPedidoCompra
                                                       && ie.CodMateriaPrima == i.CodMateriaPrima
                                                       && ie.CodBitola == i.CodBitola
                                                       select new ItemEntradaEstoqueVO
                                                       {
                                                           Lote = ie.Lote
                                                       }).FirstOrDefault().Lote : 0

                             }).ToList();

            var lstItemPedidoCompra = new List<ListItemPedidoCompraVO>();
            if (queryitem.Count() > 0)
                lstItemPedidoCompra = queryitem.ToList();
            return lstItemPedidoCompra;
        }
        public List<ListItemPedidoCompraVO> ListarPorClasseTipo(int codClasseTipo)
        {
            var queryitem = (from i in _repositorioDataContext.ItemPedidoCompras
                             join p in _repositorioDataContext.PedidoCompras
                                 on i.CodPedidoCompra equals p.CodPedidoCompra
                             join f in _repositorioDataContext.Pessoas
                                 on p.CodFornecedor equals f.CodPessoa
                             join ma in _repositorioDataContext.MateriaPrimas
                             on i.CodMateriaPrima equals ma.CodMateriaPrima
                             where ma.CodClasseTipo == codClasseTipo
                             select new ListItemPedidoCompraVO()
                             {
                                 OrdemCompra = p.CodPedidoCompra,
                                 DataEmissao = p.DataEmissao,
                                 CodPessoa = p.CodFornecedor,
                                 DataPrevista = p.DataPrazoEntrega,
                                 Fornecedor = f.RazaoSocial,
                                 Tipo = p.Tipo ?? 0,
                                 CodBitola = i.CodBitola,
                                 CodMateriaPrima = i.CodMateriaPrima,
                                 MateriaPrimaVo = (p.Tipo == 0
                                                     ? (from m in _repositorioDataContext.MateriaPrimas
                                                        join n in _repositorioDataContext.Normas
                                                            on m.CodNorma equals (n.CodNorma)
                                                        where m.CodMateriaPrima == i.CodMateriaPrima
                                                        select new MateriaPrimaVO()
                                                        {
                                                            ClasseTipoVo = (from ct in _repositorioDataContext.ClasseTipos
                                                                            where ct.CodClasseTipo == m.CodClasseTipo
                                                                            select new ClasseTipoVO()
                                                                            {
                                                                                Descricao = ct.Descricao
                                                                            }).FirstOrDefault(),
                                                            NormaVo = new NormaVO()
                                                            {
                                                                Descricao = n.Descricao,
                                                                Revisao = n.Revisao
                                                            }
                                                        }).FirstOrDefault() : (from pi in _repositorioDataContext.ProdutoInsumos
                                                                               where pi.CodProdutoInsumo == i.CodMateriaPrima
                                                                               select new MateriaPrimaVO()
                                                                               {
                                                                                   DescricaoInsumo = pi.Descricao
                                                                               }).FirstOrDefault()),
                                 Bitola = p.Tipo == 0
                                                     ? Convert.ToDecimal((from b in _repositorioDataContext.Bitolas
                                                                          where b.CodBitola == i.CodBitola
                                                                          select new BitolaVO()
                                                                          {
                                                                              Bitola = b.Bitola1
                                                                          }).FirstOrDefault().Bitola) : decimal.Zero,
                                 Ipi = i.IPI,
                                 Preco = i.Valor,
                                 Qtde = i.Qtd,
                                 Unidade = (from un in _repositorioDataContext.Unidades
                                            where un.CodUnidade == i.CodUnidade
                                            select un).FirstOrDefault().Unidade1,
                                 QtdeEntregue = p.Tipo == 0 ? (from pe in _repositorioDataContext.EntradaEstoques
                                                               join ie in _repositorioDataContext.ItemEntradaEstoques
                                                               on pe.CodEntradaEstoque equals (ie.CodEntradaEstoque)
                                                               where pe.CodPedidoCompra == p.CodPedidoCompra
                                                               && ie.CodMateriaPrima == i.CodMateriaPrima
                                                               && ie.CodBitola == i.CodBitola
                                                               select ie.Qtd).Sum() : (from pe in _repositorioDataContext.EntradaEstoques
                                                                                       join ie in _repositorioDataContext.ItemEntradaEstoques
                                                                                       on pe.CodEntradaEstoque equals (ie.CodEntradaEstoque)
                                                                                       where pe.CodPedidoCompra == p.CodPedidoCompra
                                                                                       && ie.CodMateriaPrima == i.CodMateriaPrima
                                                                                       select ie.Qtd).Sum(),
                                 DataEntrega = (from pe in _repositorioDataContext.EntradaEstoques
                                                where pe.CodPedidoCompra == p.CodPedidoCompra
                                                select new EntradaEstoqueVO()
                                                {
                                                    DataEntrada = pe.DataEntrada
                                                }).FirstOrDefault().DataEntrada,
                                 NotaFiscal = (from pe in _repositorioDataContext.EntradaEstoques
                                               where pe.CodPedidoCompra == p.CodPedidoCompra
                                               select new EntradaEstoqueVO()
                                               {
                                                   NotaFiscal = pe.NotaFiscal
                                               }).FirstOrDefault().NotaFiscal,
                                 Lote = p.Tipo == 0 ? (from pe in _repositorioDataContext.EntradaEstoques
                                                       join ie in _repositorioDataContext.ItemEntradaEstoques
                                                       on pe.CodEntradaEstoque equals (ie.CodEntradaEstoque)
                                                       where pe.CodPedidoCompra == p.CodPedidoCompra
                                                       && ie.CodMateriaPrima == i.CodMateriaPrima
                                                       && ie.CodBitola == i.CodBitola
                                                       select new ItemEntradaEstoqueVO
                                                       {
                                                           Lote = ie.Lote
                                                       }).FirstOrDefault().Lote : 0

                             }).ToList();

            var lstItemPedidoCompra = new List<ListItemPedidoCompraVO>();
            if (queryitem.Count() > 0)
                lstItemPedidoCompra = queryitem.ToList();
            return lstItemPedidoCompra;
        }
        #endregion
        #region Método de Inclusão
        public int Incluir(int codFornecedor, DateTime dataEmissao, DateTime? dataPrazoEntrega, short tipoRetirada, short codFormaPgto,
            string condicaoFornecimento, string observacao, int codFuncionarioAprovador,int codFuncionarioComprador,
            int codUsuarioInc, List<ItemPedidoCompraVO> itemPedidoCompraVo, TypePedido tipo)
        {
            var pedidocompra = new PedidoCompra()
                                   {
                                       CodFornecedor = codFornecedor,
                                       DataEmissao = dataEmissao,
                                       DataPrazoEntrega = dataPrazoEntrega,
                                       TipoRetirada = tipoRetirada,
                                       CodFormaPgto = codFormaPgto,
                                       CondicaoFornecimento = condicaoFornecimento,
                                       Observacao = observacao,
                                       Tipo = (int)tipo,
                                       CodFuncionarioAprovador = codFuncionarioAprovador,
                                       CodFuncionarioComprador = codFuncionarioComprador,
                                       DataCadastro = DateTime.Now,
                                       UsuarioInc = codUsuarioInc
                                   };
            _repositorioDataContext.PedidoCompras.InsertOnSubmit(pedidocompra);
            _repositorioDataContext.SubmitChanges();
            new ItemPedidoCompraRepositorio().Incluir(itemPedidoCompraVo, pedidocompra.CodPedidoCompra);
            return pedidocompra.CodPedidoCompra;
        }
        public int Incluir(int codFornecedor, DateTime dataEmissao, DateTime? dataPrazoEntrega, short tipoRetirada, short codFormaPgto,
            string condicaoFornecimento, string observacao, int codFuncionarioAprovador, int codFuncionarioComprador,
            int codUsuarioInc, List<ItemPedidoCompraInsumoVO> itemPedidoCompraInsumoVo, TypePedido tipo)
        {
            var pedidocompra = new PedidoCompra()
            {
                CodFornecedor = codFornecedor,
                DataEmissao = dataEmissao,
                DataPrazoEntrega = dataPrazoEntrega,
                TipoRetirada = tipoRetirada,
                CodFormaPgto = codFormaPgto,
                CondicaoFornecimento = condicaoFornecimento,
                Observacao = observacao,
                Tipo = (int)tipo,
                CodFuncionarioAprovador = codFuncionarioAprovador,
                CodFuncionarioComprador = codFuncionarioComprador,
                DataCadastro = DateTime.Now,
                UsuarioInc = codUsuarioInc
            };
            _repositorioDataContext.PedidoCompras.InsertOnSubmit(pedidocompra);
            _repositorioDataContext.SubmitChanges();
            new ItemPedidoCompraRepositorio().Incluir(itemPedidoCompraInsumoVo, pedidocompra.CodPedidoCompra);
            return pedidocompra.CodPedidoCompra;
        }
        #endregion
        #region Métodos de Alteração
        public void Alterar(int codPedidoCompra, int codFornecedor, DateTime dataEmissao, DateTime? dataPrazoEntrega, short tipoRetirada, short codFormaPgto,
    string condicaoFornecimento, string observacao, int codFuncionarioAprovador, int codFuncionarioComprador,
    int codUsuarioAlt, List<ItemPedidoCompraVO> itemPedidoCompraVo, TypePedido tipo)
        {
            IQueryable<PedidoCompra> query = from p in _repositorioDataContext.PedidoCompras
                                             where p.CodPedidoCompra == codPedidoCompra
                                             select p;




            var pedidocompra = query.FirstOrDefault();
            pedidocompra.CodFornecedor = codFornecedor;
            pedidocompra.DataEmissao = dataEmissao;
            pedidocompra.DataPrazoEntrega = dataPrazoEntrega;
            pedidocompra.TipoRetirada = tipoRetirada;
            pedidocompra.CodFormaPgto = codFormaPgto;
            pedidocompra.CondicaoFornecimento = condicaoFornecimento;
            pedidocompra.Observacao = observacao;
            pedidocompra.Tipo = (int) tipo;
            pedidocompra.CodFuncionarioAprovador = codFuncionarioAprovador;
            pedidocompra.CodFuncionarioComprador = codFuncionarioComprador;
            pedidocompra.DataAlteracao = DateTime.Now;
            pedidocompra.UsuarioAlt = codUsuarioAlt;
            new ItemPedidoCompraRepositorio().Alterar(itemPedidoCompraVo, codPedidoCompra);
            _repositorioDataContext.SubmitChanges();
        }
        public void Alterar(int codPedidoCompra, int codFornecedor, DateTime dataEmissao, DateTime? dataPrazoEntrega, short tipoRetirada, short codFormaPgto,
    string condicaoFornecimento, string observacao, int codFuncionarioAprovador, int codFuncionarioComprador,
    int codUsuarioAlt, List<ItemPedidoCompraInsumoVO> itemPedidoCompraInsumoVo, TypePedido tipo)
        {
            IQueryable<PedidoCompra> query = from p in _repositorioDataContext.PedidoCompras
                                             where p.CodPedidoCompra == codPedidoCompra
                                             select p;




            var pedidocompra = query.FirstOrDefault();
            pedidocompra.CodFornecedor = codFornecedor;
            pedidocompra.DataEmissao = dataEmissao;
            pedidocompra.DataPrazoEntrega = dataPrazoEntrega;
            pedidocompra.TipoRetirada = tipoRetirada;
            pedidocompra.CodFormaPgto = codFormaPgto;
            pedidocompra.CondicaoFornecimento = condicaoFornecimento;
            pedidocompra.Observacao = observacao;
            pedidocompra.Tipo = (int) tipo;
            pedidocompra.CodFuncionarioAprovador = codFuncionarioAprovador;
            pedidocompra.CodFuncionarioComprador = codFuncionarioComprador;
            pedidocompra.DataAlteracao = DateTime.Now;
            pedidocompra.UsuarioAlt = codUsuarioAlt;
            new ItemPedidoCompraRepositorio().Alterar(itemPedidoCompraInsumoVo, codPedidoCompra);
            _repositorioDataContext.SubmitChanges();
        }
        #endregion
        #region Método de Exclusao
        public void Excluir(int codPedidoCompra)
        {
            IQueryable<PedidoCompra> query = from p in _repositorioDataContext.PedidoCompras
                                             where p.CodPedidoCompra == codPedidoCompra
                                             select p;
            var pedidocompra = query.FirstOrDefault();
            new ItemPedidoCompraRepositorio().ExcluirTodos(codPedidoCompra);
            _repositorioDataContext.PedidoCompras.DeleteOnSubmit(pedidocompra);
            _repositorioDataContext.SubmitChanges();

        }
        #endregion

    }
}
