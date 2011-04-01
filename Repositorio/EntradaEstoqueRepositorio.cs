using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.Model;

namespace Nissi.Repositorio
{
    public class EntradaEstoqueRepositorio
    {
        private readonly RepositorioDataContext _repositorioDataContext;
        public EntradaEstoqueRepositorio()
        {
            _repositorioDataContext = new RepositorioDataContext();
        }
        #region Métodos de Listagem
        public EntradaEstoqueVO ListarTudo(int codEntradaEstoque)
        {
            var query = from e in _repositorioDataContext.EntradaEstoques
                        join f in _repositorioDataContext.Pessoas
                            on e.CodFornecedor equals f.CodPessoa
                        join c in _repositorioDataContext.CEPs
                            on f.CodCep equals c.CodCEP
                        join b in _repositorioDataContext.Bairros
                            on c.CodBairro equals b.CodBairro
                        join ci in _repositorioDataContext.Cidades
                            on c.CodCidade equals ci.CodCidade
                        join p in _repositorioDataContext.PedidoCompras
                            on e.CodPedidoCompra equals p.CodPedidoCompra
                        where e.CodEntradaEstoque == codEntradaEstoque
                        select new EntradaEstoqueVO()
                                   {
                                       CodEntradaEstoque = e.CodEntradaEstoque,
                                       Tipo = e.Tipo != null ? (TypePedido)e.Tipo : 0,
                                       PedidoCompra = new PedidoCompraVO()
                                                          {
                                                              CodPedidoCompra = p.CodPedidoCompra,
                                                              CondicaoFornecimento = p.CondicaoFornecimento,
                                                              FuncionarioComprador = (from pc in _repositorioDataContext.Pessoas
                                                                                      join ec in _repositorioDataContext.Funcionarios
                                                                                      on pc.CodPessoa equals ec.CodPessoa
                                                                                      where p.CodFuncionarioComprador == ec.CodFuncionario
                                                                                      select new FuncionarioVO()
                                                                                      {
                                                                                          CodPessoa = pc.CodPessoa,
                                                                                          Nome = pc.RazaoSocial
                                                                                      }).FirstOrDefault(),
                                                              FuncionarioAprovador = (from pa in _repositorioDataContext.Pessoas
                                                                                      join ea in _repositorioDataContext.Funcionarios
                                                                                      on pa.CodPessoa equals ea.CodPessoa
                                                                                      where p.CodFuncionarioAprovador == ea.CodFuncionario
                                                                                      select new FuncionarioVO()
                                                                                      {
                                                                                          CodPessoa = pa.CodPessoa,
                                                                                          Nome = pa.RazaoSocial
                                                                                      }).FirstOrDefault(),
                                       TipoRetirada = p.TipoRetirada,
                                       Observacao = p.Observacao,
                                       DataEmissao = p.DataEmissao,
                                                          },
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
                                    NotaFiscal = e.NotaFiscal,
                                    DataEntrada = e.DataEntrada,
                                    DataNotaFiscal = e.DataNotaFiscal,
                                    Itens = (from i in _repositorioDataContext.ItemEntradaEstoques
                                                         where i.CodEntradaEstoque == codEntradaEstoque
                                                         select new ItemEntradaEstoqueVO()
                                                                    {
                                                                        CodItemEntradaEstoque = i.CodItemEntradaEstoque,
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
                                                                        Lote = i.Lote,
                                                                        Certificado = i.Certificado,
                                                                        Corrida = i.Corrida,
                                                                        QtdPedidoCompra = i.QtdPedidoCompra,
                                                                        Qtd = i.Qtd,
                                                                        Especificacao = i.Especificacao,
                                                                        Ipi = i.IPI,
                                                                        UnidadeVo = (from u in _repositorioDataContext.Unidades
                                                                                     where u.CodUnidade == i.CodUnidade
                                                                                     select new UnidadeVO(){
                                                                                     CodUnidade = u.CodUnidade,
                                                                                     TipoUnidade = u.Unidade1,
                                                                                     Descricao = u.Descricao
                                                                                     }).FirstOrDefault(),
                                                                        Valor = i.Valor,
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
                                                                        Al = i.Al,
                                                                        Co = i.Co,
                                                                        Resistencia = i.Resistencia,
                                                                        Dureza = i.Dureza,
                                                                        Nota = i.Nota,
                                                                        CertificadoScanneado = i.CertificadoScanneado.ToArray(),
                                                                    }).ToList()
                                   };
            var entradaEstoque = query.FirstOrDefault();
            return entradaEstoque;

        }
        public List<ItemEntradaEstoqueVO> ListarPorCodigo(int codEntradaEstoque)
        {
            var queryitem = (from i in _repositorioDataContext.ItemEntradaEstoques
                             join e in _repositorioDataContext.EntradaEstoques
                                 on i.CodEntradaEstoque equals e.CodEntradaEstoque
                             join f in _repositorioDataContext.Pessoas
                                 on e.CodFornecedor equals f.CodPessoa
                             where i.CodEntradaEstoque == codEntradaEstoque
                             select new ItemEntradaEstoqueVO()
                                        {
                                            CodItemEntradaEstoque = i.CodItemEntradaEstoque,
                                            EntradaEstoqueVo = new EntradaEstoqueVO()
                                            {
                                            CodEntradaEstoque = e.CodEntradaEstoque,
                                            Tipo = e.Tipo != null ? (TypePedido)e.Tipo : 0,
                                            Fornecedor = new FornecedorVO()
                                                            {
                                                                CodPessoa = f.CodPessoa,
                                                                RazaoSocial = f.RazaoSocial,
                                                            },
                                            NotaFiscal = e.NotaFiscal,
                                            DataEntrada = e.DataEntrada,
                                            DataNotaFiscal = e.DataNotaFiscal,
                                            },
                                            MateriaPrimaVo = (e.Tipo == 0
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
                                            BitolaVo = (e.Tipo == 0
                                                                ?(from bi in _repositorioDataContext.Bitolas
                                                        where bi.CodBitola == i.CodBitola
                                                        select new BitolaVO()
                                                                   {
                                                                       CodBitola = bi.CodBitola,
                                                                       Bitola = bi.Bitola1,
                                                                   }).FirstOrDefault():new BitolaVO(){CodBitola = 0,
                                                                   Bitola = 0}),
                                            Lote = i.Lote,
                                            Certificado = i.Certificado,
                                            Corrida = i.Corrida,
                                            QtdPedidoCompra = i.QtdPedidoCompra,
                                            Qtd = i.Qtd,
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
                                            Al = i.Al,
                                            Co = i.Co,
                                            Resistencia = i.Resistencia,
                                            Dureza = i.Dureza,
                                            Nota = i.Nota,
                                            CertificadoScanneado = i.CertificadoScanneado.ToArray(),
                                        }).ToList();

        var lstItemEntradaEstoque = new List<ItemEntradaEstoqueVO>();
        if (queryitem.Count() > 0)
            lstItemEntradaEstoque = queryitem.ToList();
            return lstItemEntradaEstoque;
    }
        public List<ItemEntradaEstoqueVO> ListarPorFornecedor(int codFornecedor)
        {
            var queryitem = (from i in _repositorioDataContext.ItemEntradaEstoques
                             join e in _repositorioDataContext.EntradaEstoques
                                 on i.CodEntradaEstoque equals e.CodEntradaEstoque
                             join f in _repositorioDataContext.Pessoas
                                 on e.CodFornecedor equals f.CodPessoa
                             where f.CodPessoa == codFornecedor
                             select new ItemEntradaEstoqueVO()
                                        {
                                            CodItemEntradaEstoque = i.CodItemEntradaEstoque,
                                            EntradaEstoqueVo = new EntradaEstoqueVO()
                                            {
                                            CodEntradaEstoque = e.CodEntradaEstoque,
                                            Tipo = e.Tipo != null ? (TypePedido)e.Tipo : 0,
                                            Fornecedor = new FornecedorVO()
                                                            {
                                                                CodPessoa = f.CodPessoa,
                                                                RazaoSocial = f.RazaoSocial,
                                                            },
                                            NotaFiscal = e.NotaFiscal,
                                            DataEntrada = e.DataEntrada,
                                            DataNotaFiscal = e.DataNotaFiscal,
                                            },
                                            MateriaPrimaVo = (e.Tipo == 0
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
                                            BitolaVo = (e.Tipo == 0
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
                                            Lote = i.Lote,
                                            Certificado = i.Certificado,
                                            Corrida = i.Corrida,
                                            QtdPedidoCompra = i.QtdPedidoCompra,
                                            Qtd = i.Qtd,
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
                                            Al = i.Al,
                                            Co = i.Co,
                                            Resistencia = i.Resistencia,
                                            Dureza = i.Dureza,
                                            Nota = i.Nota,
                                            CertificadoScanneado = i.CertificadoScanneado.ToArray(),
                                        }).ToList();

        var lstItemEntradaEstoque = new List<ItemEntradaEstoqueVO>();
        if (queryitem.Count() > 0)
            lstItemEntradaEstoque = queryitem.ToList();
            return lstItemEntradaEstoque;
        }
        public List<ItemEntradaEstoqueVO> ListarPorLote(string lote)
        {
            var queryitem = (from i in _repositorioDataContext.ItemEntradaEstoques
                             join e in _repositorioDataContext.EntradaEstoques
                                 on i.CodEntradaEstoque equals e.CodEntradaEstoque
                             join f in _repositorioDataContext.Pessoas
                                 on e.CodFornecedor equals f.CodPessoa
                             where i.Lote.Equals(lote)
                             select new ItemEntradaEstoqueVO()
                                        {
                                            CodItemEntradaEstoque = i.CodItemEntradaEstoque,
                                            EntradaEstoqueVo = new EntradaEstoqueVO()
                                            {
                                            CodEntradaEstoque = e.CodEntradaEstoque,
                                            Tipo = e.Tipo != null ? (TypePedido)e.Tipo : 0,
                                            Fornecedor = new FornecedorVO()
                                                            {
                                                                CodPessoa = f.CodPessoa,
                                                                RazaoSocial = f.RazaoSocial,
                                                            },
                                            NotaFiscal = e.NotaFiscal,
                                            DataEntrada = e.DataEntrada,
                                            DataNotaFiscal = e.DataNotaFiscal,
                                            },
                                            MateriaPrimaVo = (e.Tipo == 0
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
                                            BitolaVo = (e.Tipo == 0
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
                                            Lote = i.Lote,
                                            Certificado = i.Certificado,
                                            Corrida = i.Corrida,
                                            QtdPedidoCompra = i.QtdPedidoCompra,
                                            Qtd = i.Qtd,
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
                                            Al = i.Al,
                                            Co = i.Co,
                                            Resistencia = i.Resistencia,
                                            Dureza = i.Dureza,
                                            Nota = i.Nota,
                                            CertificadoScanneado = i.CertificadoScanneado.ToArray(),
                                        }).ToList();

        var lstItemEntradaEstoque = new List<ItemEntradaEstoqueVO>();
        if (queryitem.Count() > 0)
            lstItemEntradaEstoque = queryitem.ToList();
            return lstItemEntradaEstoque;
        }
        public List<ItemEntradaEstoqueVO> ListarPorData(DateTime dataInicio, DateTime dataFim)
        {
            var queryitem = (from i in _repositorioDataContext.ItemEntradaEstoques
                             join e in _repositorioDataContext.EntradaEstoques
                                 on i.CodEntradaEstoque equals e.CodEntradaEstoque
                             join f in _repositorioDataContext.Pessoas
                                 on e.CodFornecedor equals f.CodPessoa
                             where e.DataEntrada >= dataInicio && e.DataEntrada <= dataFim
                             select new ItemEntradaEstoqueVO()
                             {
                                 CodItemEntradaEstoque = i.CodItemEntradaEstoque,
                                 EntradaEstoqueVo = new EntradaEstoqueVO()
                                 {
                                     CodEntradaEstoque = e.CodEntradaEstoque,
                                     Tipo = e.Tipo != null ? (TypePedido)e.Tipo : 0,
                                     Fornecedor = new FornecedorVO()
                                     {
                                         CodPessoa = f.CodPessoa,
                                         RazaoSocial = f.RazaoSocial,
                                     },
                                     NotaFiscal = e.NotaFiscal,
                                     DataEntrada = e.DataEntrada,
                                     DataNotaFiscal = e.DataNotaFiscal,
                                 },
                                 MateriaPrimaVo = (e.Tipo == 0
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
                                 BitolaVo = (e.Tipo == 0
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
                                 Lote = i.Lote,
                                 Certificado = i.Certificado,
                                 Corrida = i.Corrida,
                                 QtdPedidoCompra = i.QtdPedidoCompra,
                                 Qtd = i.Qtd,
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
                                 Al = i.Al,
                                 Co = i.Co,
                                 Resistencia = i.Resistencia,
                                 Dureza = i.Dureza,
                                 Nota = i.Nota,
                                 CertificadoScanneado = i.CertificadoScanneado.ToArray(),
                             }).ToList();

            var lstItemEntradaEstoque = new List<ItemEntradaEstoqueVO>();
            if (queryitem.Count() > 0)
                lstItemEntradaEstoque = queryitem.ToList();
            return lstItemEntradaEstoque;
        }
        public List<ItemEntradaEstoqueVO> ListarPorCorrida(string corrida)
        {
            var queryitem = (from i in _repositorioDataContext.ItemEntradaEstoques
                             join e in _repositorioDataContext.EntradaEstoques
                                 on i.CodEntradaEstoque equals e.CodEntradaEstoque
                             join f in _repositorioDataContext.Pessoas
                                 on e.CodFornecedor equals f.CodPessoa
                             where i.Corrida.Equals(corrida)
                             select new ItemEntradaEstoqueVO()
                                        {
                                            CodItemEntradaEstoque = i.CodItemEntradaEstoque,
                                            EntradaEstoqueVo = new EntradaEstoqueVO()
                                            {
                                            CodEntradaEstoque = e.CodEntradaEstoque,
                                            Tipo = e.Tipo != null ? (TypePedido)e.Tipo : 0,
                                            Fornecedor = new FornecedorVO()
                                                            {
                                                                CodPessoa = f.CodPessoa,
                                                                RazaoSocial = f.RazaoSocial,
                                                            },
                                            NotaFiscal = e.NotaFiscal,
                                            DataEntrada = e.DataEntrada,
                                            DataNotaFiscal = e.DataNotaFiscal,
                                            },
                                            MateriaPrimaVo = (e.Tipo == 0
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
                                            BitolaVo = (e.Tipo == 0
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
                                            Lote = i.Lote,
                                            Certificado = i.Certificado,
                                            Corrida = i.Corrida,
                                            QtdPedidoCompra = i.QtdPedidoCompra,
                                            Qtd = i.Qtd,
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
                                            Al = i.Al,
                                            Co = i.Co,
                                            Resistencia = i.Resistencia,
                                            Dureza = i.Dureza,
                                            Nota = i.Nota,
                                            CertificadoScanneado = i.CertificadoScanneado.ToArray(),
                                        }).ToList();

        var lstItemEntradaEstoque = new List<ItemEntradaEstoqueVO>();
        if (queryitem.Count() > 0)
            lstItemEntradaEstoque = queryitem.ToList();
            return lstItemEntradaEstoque;
        }
        public List<ItemEntradaEstoqueVO> ListarPorCertificado(string certificado)
        {
            var queryitem = (from i in _repositorioDataContext.ItemEntradaEstoques
                             join e in _repositorioDataContext.EntradaEstoques
                                 on i.CodEntradaEstoque equals e.CodEntradaEstoque
                             join f in _repositorioDataContext.Pessoas
                                 on e.CodFornecedor equals f.CodPessoa
                             where i.Certificado.Equals(certificado)
                             select new ItemEntradaEstoqueVO()
                                        {
                                            CodItemEntradaEstoque = i.CodItemEntradaEstoque,
                                            EntradaEstoqueVo = new EntradaEstoqueVO()
                                            {
                                            CodEntradaEstoque = e.CodEntradaEstoque,
                                            Tipo = e.Tipo != null ? (TypePedido)e.Tipo : 0,
                                            Fornecedor = new FornecedorVO()
                                                            {
                                                                CodPessoa = f.CodPessoa,
                                                                RazaoSocial = f.RazaoSocial,
                                                            },
                                            NotaFiscal = e.NotaFiscal,
                                            DataEntrada = e.DataEntrada,
                                            DataNotaFiscal = e.DataNotaFiscal,
                                            },
                                            MateriaPrimaVo = (e.Tipo == 0
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
                                            BitolaVo = (e.Tipo == 0
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
                                            Lote = i.Lote,
                                            Certificado = i.Certificado,
                                            Corrida = i.Corrida,
                                            QtdPedidoCompra = i.QtdPedidoCompra,
                                            Qtd = i.Qtd,
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
                                            Al = i.Al,
                                            Co = i.Co,
                                            Resistencia = i.Resistencia,
                                            Dureza = i.Dureza,
                                            Nota = i.Nota,
                                            CertificadoScanneado = i.CertificadoScanneado.ToArray(),
                                        }).ToList();

        var lstItemEntradaEstoque = new List<ItemEntradaEstoqueVO>();
        if (queryitem.Count() > 0)
            lstItemEntradaEstoque = queryitem.ToList();
            return lstItemEntradaEstoque;
        }
        public EntradaEstoqueInsumoVO ListarTudoInsumo(int codEntradaEstoque)
        {
            var query = from e in _repositorioDataContext.EntradaEstoques
                        join f in _repositorioDataContext.Pessoas
                            on e.CodFornecedor equals f.CodPessoa
                        join c in _repositorioDataContext.CEPs
                            on f.CodCep equals c.CodCEP
                        join b in _repositorioDataContext.Bairros
                            on c.CodBairro equals b.CodBairro
                        join ci in _repositorioDataContext.Cidades
                            on c.CodCidade equals ci.CodCidade
                        join p in _repositorioDataContext.PedidoCompras
                            on e.CodPedidoCompra equals p.CodPedidoCompra
                        where e.CodEntradaEstoque == codEntradaEstoque
                        select new EntradaEstoqueInsumoVO()
                        {
                            CodEntradaEstoque = e.CodEntradaEstoque,
                            Tipo = e.Tipo != null ? (TypePedido)e.Tipo : 0,
                            PedidoCompra = new PedidoCompraVO()
                            {
                                CodPedidoCompra = p.CodPedidoCompra,
                                CondicaoFornecimento = p.CondicaoFornecimento,
                                FuncionarioComprador = (from pc in _repositorioDataContext.Pessoas
                                                        join ec in _repositorioDataContext.Funcionarios
                                                        on pc.CodPessoa equals ec.CodPessoa
                                                        where p.CodFuncionarioComprador == ec.CodFuncionario
                                                        select new FuncionarioVO()
                                                        {
                                                            CodPessoa = pc.CodPessoa,
                                                            Nome = pc.RazaoSocial
                                                        }).FirstOrDefault(),
                                FuncionarioAprovador = (from pa in _repositorioDataContext.Pessoas
                                                        join ea in _repositorioDataContext.Funcionarios
                                                        on pa.CodPessoa equals ea.CodPessoa
                                                        where p.CodFuncionarioAprovador == ea.CodFuncionario
                                                        select new FuncionarioVO()
                                                        {
                                                            CodPessoa = pa.CodPessoa,
                                                            Nome = pa.RazaoSocial
                                                        }).FirstOrDefault(),
                                TipoRetirada = p.TipoRetirada,
                                Observacao = p.Observacao,
                                DataEmissao = p.DataEmissao,
                            },
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
                            NotaFiscal = e.NotaFiscal,
                            DataEntrada = e.DataEntrada,
                            DataNotaFiscal = e.DataNotaFiscal,
                            Itens = (from i in _repositorioDataContext.ItemEntradaEstoques
                                     where i.CodItemEntradaEstoque == codEntradaEstoque
                                     select new ItemEntradaEstoqueInsumoVO()
                                     {
                                         CodItemEntradaEstoqueInsumo = i.CodItemEntradaEstoque,
                                         ProdutoInsumoVo = (from m in _repositorioDataContext.ProdutoInsumos
                                                            where m.CodProdutoInsumo == i.CodMateriaPrima
                                                            select new ProdutoInsumoVO()
                                                            {
                                                                CodProdutoInsumo = m.CodProdutoInsumo,
                                                                Descricao = m.Descricao
                                                            }).FirstOrDefault(),
                                         Qtd = i.Qtd,
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
                                         Lote = i.Lote,
                                         Certificado = i.Certificado,
                                         Corrida = i.Corrida,
                                         QtdPedidoCompra = i.QtdPedidoCompra,
                                         Nota = i.Nota,
                                         CertificadoScanneado = i.CertificadoScanneado.ToArray(),
                                     }).ToList()
                        };
            var entradaEstoque = query.FirstOrDefault();
            return entradaEstoque;
        }
        #endregion
        #region Método de Inclusão
        public int Incluir(int codFornecedor, int codPedidoCompra, DateTime dataNotaFiscal, DateTime dataEntrada, string notaFiscal, int codUsuario, List<ItemEntradaEstoqueVO> itens, TypePedido tipo)
        {
            var entradaEstoque = new EntradaEstoque()
                                     {
                                         CodFornecedor = codFornecedor,
                                         CodPedidoCompra = codPedidoCompra,
                                         DataEntrada = dataEntrada,
                                         DataNotaFiscal = dataNotaFiscal,
                                         NotaFiscal = notaFiscal,
                                         Tipo = (int)tipo,
                                         UsuarioInc = codUsuario,
                                         DataCadastro = DateTime.Now
                                     };
            _repositorioDataContext.EntradaEstoques.InsertOnSubmit(entradaEstoque);
            _repositorioDataContext.SubmitChanges();
            new ItemEntradaEstoqueRepositorio().Incluir(itens, entradaEstoque.CodEntradaEstoque);
            return entradaEstoque.CodEntradaEstoque;
        }

        public int Incluir(int codFornecedor, int codPedidoCompra, DateTime dataNotaFiscal, DateTime dataEntrada, string notaFiscal, int codUsuario, List<ItemEntradaEstoqueInsumoVO> itens, TypePedido tipo)
        {
            var entradaEstoque = new EntradaEstoque()
            {
                CodFornecedor = codFornecedor,
                CodPedidoCompra = codPedidoCompra,
                DataEntrada = dataEntrada,
                DataNotaFiscal = dataNotaFiscal,
                NotaFiscal = notaFiscal,
                Tipo = (int)tipo,
                UsuarioInc = codUsuario,
                DataCadastro = DateTime.Now
            };
            _repositorioDataContext.EntradaEstoques.InsertOnSubmit(entradaEstoque);
            _repositorioDataContext.SubmitChanges();
            new ItemEntradaEstoqueRepositorio().Incluir(itens, entradaEstoque.CodEntradaEstoque);
            return entradaEstoque.CodEntradaEstoque;
        }
        #endregion
        #region Método de Alteração
        public void Alterar(int codEntradaEstoque, int codFornecedor, int codPedidoCompra, DateTime dataNotaFiscal, DateTime dataEntrada, string notaFiscal,int codUsuario, List<ItemEntradaEstoqueVO> itens, TypePedido tipo)
        {
            IQueryable<EntradaEstoque> query = from e in _repositorioDataContext.EntradaEstoques
                                             where e.CodEntradaEstoque == codEntradaEstoque
                                             select e;
            var entradaEstoque = query.FirstOrDefault();
            entradaEstoque.CodPedidoCompra = codPedidoCompra;
            entradaEstoque.CodFornecedor = codFornecedor;
            entradaEstoque.DataNotaFiscal = dataNotaFiscal;
            entradaEstoque.NotaFiscal = notaFiscal;
            entradaEstoque.DataEntrada = dataEntrada;
            entradaEstoque.Tipo = (int) tipo;
            entradaEstoque.UsuarioAlt = codUsuario;
            entradaEstoque.DataAlteracao = DateTime.Now;
            new ItemEntradaEstoqueRepositorio().Alterar(itens, codEntradaEstoque);
            _repositorioDataContext.SubmitChanges();
        }

        public void Alterar(int codEntradaEstoque, int codFornecedor, int codPedidoCompra, DateTime dataNotaFiscal, DateTime dataEntrada, string notaFiscal, int codUsuario, List<ItemEntradaEstoqueInsumoVO> itens, TypePedido tipo)
        {
            IQueryable<EntradaEstoque> query = from e in _repositorioDataContext.EntradaEstoques
                                               where e.CodEntradaEstoque == codEntradaEstoque
                                               select e;
            var entradaEstoque = query.FirstOrDefault();
            entradaEstoque.CodPedidoCompra = codPedidoCompra;
            entradaEstoque.CodFornecedor = codFornecedor;
            entradaEstoque.DataNotaFiscal = dataNotaFiscal;
            entradaEstoque.NotaFiscal = notaFiscal;
            entradaEstoque.DataEntrada = dataEntrada;
            entradaEstoque.Tipo = (int)tipo;
            entradaEstoque.UsuarioAlt = codUsuario;
            entradaEstoque.DataAlteracao = DateTime.Now;
            new ItemEntradaEstoqueRepositorio().Alterar(itens, codEntradaEstoque);
            _repositorioDataContext.SubmitChanges();
        }
        #endregion
        #region Método de Exclusão
        public void Excluir(int codEntradaEstoque)
        {
            IQueryable<EntradaEstoque> query = from e in _repositorioDataContext.EntradaEstoques
                                             where e.CodEntradaEstoque == codEntradaEstoque
                                             select e;
            var entradaEstoque = query.FirstOrDefault();
            new ItemEntradaEstoqueRepositorio().ExcluirTodos(codEntradaEstoque);
            _repositorioDataContext.EntradaEstoques.DeleteOnSubmit(entradaEstoque);
            _repositorioDataContext.SubmitChanges();
        }
        #endregion

    }
}
