using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.DataAccess;
using Nissi.Model;

namespace Nissi.Business
{
    public class NissiMenu:NissiBaseBusiness
    {
        /// <summary>
        /// Método para executar a proc pr_selecionar_menu 
        /// </summary>
        public List<MenuVO> Listar(MenuVO identMenu)
        {
            return new MenuData().Listar(identMenu);
        }
                /// <summary>
        /// Método para incluir um registro na tabela Menu 
        /// </summary>
        public void Incluir(MenuVO menuVO, int codUsuarioOperacao)
        {
            new MenuData().Incluir(menuVO, codUsuarioOperacao);
        }
        /// <summary>
        /// Método para alterar um registro na tabela  Menu 
        /// </summary>
        public void Alterar(MenuVO menuVO, int codUsuarioOperacao)
        {
            new MenuData().Alterar(menuVO, codUsuarioOperacao);
        }
        /// <summary>
        /// Método para excluir um registro na tabela  Menu 
        /// </summary>
        public void Excluir(short? codMenu, int codUsuarioOperacao)
        {
            new MenuData().Excluir(codMenu, codUsuarioOperacao);
        }


    }
}
