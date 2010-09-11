using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.DataAccess;
using Nissi.Model;

namespace Nissi.Business
{
    public class Unidade:NissiBaseBusiness
    {
        #region Métodos de Listagem
        /// <summary>
        /// Lista as unidades cadastradas no banco
        /// </summary>
        /// <returns></returns>
        public List<UnidadeVO> Listar(UnidadeVO identUnidade)
        {
            return new UnidadeData().Lista(identUnidade);
        }
        #endregion

        #region Métodos de Inclusão
        /// <summary>
        /// Método para incluir a unidade
        /// </summary>
        /// <param name="identUnidade">passar: descricao,unidade,usuarioInc</param>
        public void Incluir(UnidadeVO identUnidade)
        {
            new UnidadeData().Inclui(identUnidade);
        }
        #endregion

        #region Métodos de Exclusão
        /// <summary>
        /// Método de Exclusão da unidade
        /// </summary>
        /// <param name="identUnidade">passar:codUnidade</param>
        public void Excluir(UnidadeVO identUnidade)
        {
            new UnidadeData().Exclui(identUnidade);
        }
        #endregion

        #region Métodos de Alteração
        /// <summary>
        /// Método de alteração da unidade
        /// </summary>
        /// <param name="identUnidade">passar; parametros para alteração</param>
        public void Alterar(UnidadeVO identUnidade)
        {
            new UnidadeData().Altera(identUnidade);
        }
        #endregion
    }
}
