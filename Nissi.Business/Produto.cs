using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.DataAccess;
using Nissi.Model;

namespace Nissi.Business
{
    public class Produto:NissiBaseBusiness
    {
        #region Método de listagem
        /// <summary>
        /// Método para listar os produtos
        /// </summary>
        /// <param name="identProduto">codProduto(opcional),descricao,opcional</param>
        /// <returns></returns>
        public List<ProdutoVO> Listar(ProdutoVO identProduto)
        {
            return new ProdutoData().Lista(identProduto);
        }
        #endregion

        #region Métodos de Inclusão
        /// <summary>
        /// Método de Inclusão do produto
        /// </summary>
        /// <param name="identProduto">passar: Descricao,CodUnidade,UsuarioInc</param>
        public int Incluir(ProdutoVO identProduto)
        {
           return new ProdutoData().Inclui(identProduto);
        }
        #endregion

        #region Métodos de Exclusão
        /// <summary>
        /// Método de exclusão do produto
        /// </summary>
        /// <param name="identProduto">passar: codProduto</param>
        public void Excluir(ProdutoVO identProduto)
        {
            new ProdutoData().Exclui(identProduto);
        }
        #endregion

        #region Métodos de Alteração
        /// <summary>
        /// Método para alteração do produto
        /// </summary>
        /// <param name="identProduto">passar todos os dados modificados</param>
        public void AlterarProduto(ProdutoVO identProduto)
        {
            new ProdutoData().Altera(identProduto);
        }

        #endregion

    }
}
