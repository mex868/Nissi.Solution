using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.Model;

namespace Nissi.Repositorio
{
    public class ProdutoInsumoRepositorio
    {
        private readonly RepositorioDataContext _repositorioDataContext;
        public ProdutoInsumoRepositorio()
        {
            _repositorioDataContext = new RepositorioDataContext();
        }
        #region Métodos de Listagem
        public List<ProdutoInsumoVO> Listar()
        {
            var query = from p in _repositorioDataContext.ProdutoInsumos
                        select new ProdutoInsumoVO()
                                   {

                                       CodProdutoInsumo = p.CodProdutoInsumo,
                                       Descricao = p.Descricao,
                                       Unidade = (from u in _repositorioDataContext.Unidades
                                                  where u.CodUnidade == p.CodUnidade
                                       select new UnidadeVO()
                                                     {
                                                         CodUnidade = u.CodUnidade,
                                                         Descricao = u.Descricao,
                                                         TipoUnidade = u.Unidade1
                                                     }).FirstOrDefault(),
                                                     Valor = p.Valor
                                   };
            var lstProdutoInsumoVO = new List<ProdutoInsumoVO>();
            if (query.Count() > 0)
                lstProdutoInsumoVO = query.ToList();
            return lstProdutoInsumoVO;
        }
        public ProdutoInsumoVO ListarPorCodigo(int codigo)
        {
            var query = from p in _repositorioDataContext.ProdutoInsumos
                        where p.CodProdutoInsumo == codigo
                        select new ProdutoInsumoVO()
                        {

                            CodProdutoInsumo = p.CodProdutoInsumo,
                            Descricao = p.Descricao,
                            Unidade = (from u in _repositorioDataContext.Unidades
                                       where u.CodUnidade == p.CodUnidade
                                       select new UnidadeVO()
                                       {
                                           CodUnidade = u.CodUnidade,
                                           Descricao = u.Descricao,
                                           TipoUnidade = u.Unidade1
                                       }).FirstOrDefault(),
                            Valor = p.Valor
                        };

                var produtoInsumoVO = query.FirstOrDefault();
            return produtoInsumoVO;

        }
        public List<ProdutoInsumoVO> ListarPorDescricao(string descricao)
        {
            var query = from p in _repositorioDataContext.ProdutoInsumos
                        where p.Descricao.Contains(descricao)
                        select new ProdutoInsumoVO()
                        {

                            CodProdutoInsumo = p.CodProdutoInsumo,
                            Descricao = p.Descricao,
                            Unidade = (from u in _repositorioDataContext.Unidades
                                       where u.CodUnidade == p.CodUnidade
                                       select new UnidadeVO()
                                       {
                                           CodUnidade = u.CodUnidade,
                                           Descricao = u.Descricao,
                                           TipoUnidade = u.Unidade1
                                       }).FirstOrDefault(),
                            Valor = p.Valor
                        };

            var lstProdutoInsumoVO = new List<ProdutoInsumoVO>();
            if (query.Count() > 0)
                lstProdutoInsumoVO = query.ToList();
            return lstProdutoInsumoVO;

        }
       #endregion
        #region Métodos de Inclusão
        public int? Incluir(string descricao, int? codUnidade, decimal? valor,int codUsuarioInc)
        {
            var identProdutoInsumo = new ProdutoInsumo()
                                  {
                                      Descricao = descricao,
                                      CodUnidade = codUnidade,
                                      Valor = valor,
                                      DataCadastro = DateTime.Now,
                                      UsuarioInc = codUsuarioInc
                                  };
            _repositorioDataContext.ProdutoInsumos.InsertOnSubmit(identProdutoInsumo);
            _repositorioDataContext.SubmitChanges();
            return identProdutoInsumo.CodProdutoInsumo;
        }
        #endregion
        #region Método de Alteração
        public void Alterar(int codProdutoInsumo, string descricao, int? codUnidade, decimal? valor, int codUsuarioAlt)
        {
            IQueryable<ProdutoInsumo> query = from p in _repositorioDataContext.ProdutoInsumos
                                       where p.CodProdutoInsumo == codProdutoInsumo
                                       select p;
            var identProdutoInsumo = query.FirstOrDefault();
            identProdutoInsumo.Descricao = descricao;
            identProdutoInsumo.CodUnidade = codUnidade;
            identProdutoInsumo.Valor = valor;
            identProdutoInsumo.DataAlteracao = DateTime.Now;
            identProdutoInsumo.UsuarioAlt = codUsuarioAlt;
            _repositorioDataContext.SubmitChanges();
        }
        #endregion
        #region Método de Exclusão
        public void Excluir(int codProdutoInsumo, int codUsuarioInc)
        {
            IQueryable<ProdutoInsumo> query = from p in _repositorioDataContext.ProdutoInsumos
                                       where p.CodProdutoInsumo == codProdutoInsumo
                                       select p;
            if (query.Count() > 0)
            {
                var identProdutoInsumo = query.FirstOrDefault();
                _repositorioDataContext.ProdutoInsumos.DeleteOnSubmit(identProdutoInsumo);
                _repositorioDataContext.SubmitChanges();
            }
        }
        #endregion 
    }
}
