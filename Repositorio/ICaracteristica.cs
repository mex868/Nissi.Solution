using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.Model;

namespace Nissi.Repositorio
{
    interface ICaracteristica
    {
        #region Métodos de Listagem

        List<CaracteristicaVO> Listar();
        CaracteristicaVO ListarPorCodigo(int codigo);
        List<CaracteristicaVO> ListarPorDescricao(string descricao);

        #endregion
        #region Métodos de Inclusão

        int Incluir(string descricao, short? tipo, int codUsuarioInc);

        #endregion
        #region Método de Alteração

        void Alterar(int codigo, string descricao, short? tipo, int codUsuarioAlt);

        #endregion
        #region Método de Exclusão

        void Excluir(int codigo, int codUsuarioInc);

        #endregion

    }
}
