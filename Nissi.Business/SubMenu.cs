using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.DataAccess;
using Nissi.Model;

namespace Nissi.Business
{
    public class SubMenu: NissiBaseBusiness
    {
        /// <summary>
        /// Método para executar a proc pr_selecionar_submenu 
        /// </summary>
        #region Métodos de Listagem
        public List<SubMenuVO> Listar(SubMenuVO identSubMenu)
        {
            return new SubMenuData().Listar(identSubMenu);
        }

        public int ListarOrdem(SubMenuVO identSuMenu)
        {
            return new SubMenuData().ListarOrdem(identSuMenu);
        }  
        #endregion
        /// <summary>
        /// Método para incluir um registro na tabela SubMenu 
        /// </summary>
        #region Métodos de Inclusão
        public void Incluir(SubMenuVO subMenuVO, int codUsuarioOperacao)
        {
            new SubMenuData().Incluir(subMenuVO, codUsuarioOperacao);
        }
        #endregion
        // ------------------------------------------------------------------------- // 
        /// <summary>
        /// Método para alterar um registro na tabela  SubMenu 
        /// </summary>
        #region Métodos de Alteração
        public void Alterar(SubMenuVO subMenuVO, int codUsuarioOperacao)
        {
            new SubMenuData().Alterar(subMenuVO, codUsuarioOperacao);
        }
        #endregion
        /// <summary>
        /// Método para excluir um registro na tabela  SubMenu 
        /// </summary>
        #region Métodos de Exclusão
        public void Excluir(short? codSubMenu, int codUsuarioOperacao)
        {
            new SubMenuData().Excluir(codSubMenu, codUsuarioOperacao);
        }
        #endregion

    }
}
