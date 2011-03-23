using System.Collections.Generic;
using Nissi.Model;
using Nissi.Repositorio;

namespace Nissi.Business
{
    public static class ProdutoInsumo
    {
        public static List<ProdutoInsumoVO> Listar()
        {
            return new ProdutoInsumoRepositorio().Listar();
        }

        public static ProdutoInsumoVO ListarPorCodigo(int codigo)
        {
            return new ProdutoInsumoRepositorio().ListarPorCodigo(codigo);
        }

        public static List<ProdutoInsumoVO> ListarPorDescricao(string descricao)
        {
            return new ProdutoInsumoRepositorio().ListarPorDescricao(descricao);
        }

        public static void Excluir(int codigo)
        {
            new ProdutoInsumoRepositorio().Excluir(codigo, 1);
        }

        public static int? Incluir(string descricao, int? codUnidade, decimal? valor)
        {
           return new ProdutoInsumoRepositorio().Incluir(descricao, codUnidade, valor, 1);
        }

        public static void Alterar(int codigo, string descricao, int? codUnidade, decimal? valor)
        {
            new ProdutoInsumoRepositorio().Alterar(codigo, descricao, codUnidade, valor, 1);
        }
    }
}
