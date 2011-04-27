using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.Model;
using Nissi.Repositorio;

namespace Nissi.Business
{
    public static class Processo
    {
        #region Métodos de Listagem

        public static List<ProcessoVO> Listar()
        {
            return new ProcessoRepositorio().Listar();
        }
        public static ProcessoVO ListarPorCodigo(int codigo)
        {
            return new ProcessoRepositorio().ListarPorCodigo(codigo);
        }
        public static List<ProcessoVO> ListarPorDescricao(string descricao)
        {
            return new ProcessoRepositorio().ListarPorDescricao(descricao);
        }

        #endregion
        #region Métodos de Inclusão

        public static int Incluir(string descricao, int codUsuarioInc)
        {
            return new ProcessoRepositorio().Incluir(descricao, codUsuarioInc);
        }

        #endregion
        #region Método de Alteração

        public static void Alterar(int codigo, string descricao, int codUsuarioAlt)
        {
            new ProcessoRepositorio().Alterar(codigo, descricao, codUsuarioAlt);
        }

        #endregion
        #region Método de Exclusão

        public static void Excluir(int codigo, int codUsuarioInc)
        {
            new ProcessoRepositorio().Excluir(codigo, codUsuarioInc);
        }

        #endregion
    }
}
