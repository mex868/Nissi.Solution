using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.Model;

namespace Nissi.Repositorio.Factory
{
    public class ProdutoProdutoInsumo : IProduto
    {
        private readonly RepositorioDataContext _repositorioDataContext;
        public ProdutoProdutoInsumo()
        {
            _repositorioDataContext = new RepositorioDataContext();
        }
        public string GetNameProduct(int codigo)
        {
            var query = from pi in _repositorioDataContext.ProdutoInsumos
                        where pi.CodProdutoInsumo == codigo
                        select new ProdutoInsumoVO()
                                   {
                                       Descricao = pi.Descricao
                                   };
            var produtoInsumo = new ProdutoInsumoVO();
            if (query.Count() > 0)
                produtoInsumo = query.FirstOrDefault();
            return produtoInsumo.Descricao;

        }
    }
}
