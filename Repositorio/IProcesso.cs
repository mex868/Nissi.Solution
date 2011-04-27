using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.Model;

namespace Nissi.Repositorio
{
    interface IProcesso
    {
        #region Métodos de Listagem

        List<ProcessoVO> Listar();
        ProcessoVO ListarPorCodigo(int codigo);
        List<ProcessoVO> ListarPorDescricao(string descricao);

        #endregion
        #region Métodos de Inclusão

        int Incluir(string descricao, int codUsuarioInc);

        #endregion
        #region Método de Alteração

        void Alterar(int codigo, string descricao,  int codUsuarioAlt);

        #endregion
        #region Método de Exclusão

        void Excluir(int codigo, int codUsuarioInc);

        #endregion
    }
}
