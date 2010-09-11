using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.Model;
using Nissi.DataAccess;


namespace Nissi.Business
{
    public class Emitente:NissiBaseBusiness
    {
        #region Método de Listagem
        public List<EmitenteVO> Listar(EmitenteVO identEmitente)
        {
            return new EmitenteData().Lista(identEmitente);
        }
        #endregion

        #region Métodos de Inclusão
        public int Incluir(EmitenteVO identEmitente)
        {
            return new EmitenteData().Inclui(identEmitente);
        }
        #endregion

        #region Métodos de Alteração
        public void Alterar(EmitenteVO identEmitente)
        {
            new EmitenteData().Altera(identEmitente);
        }
        #endregion

        #region Método de Exclusão
        public void Excluir(EmitenteVO identEmitente)
        {
            new EmitenteData().Exclui(identEmitente);
        }
        #endregion
    }
}
