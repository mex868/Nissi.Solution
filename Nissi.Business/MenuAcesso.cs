using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.DataAccess;
using Nissi.Model;

namespace Nissi.Business
{
    public class MenuAcesso:NissiBaseBusiness
    {
        /// <summary>
        /// Método para executar a proc pr_selecionar_menu
        /// Objeto/Parâmetros: identMenu
        /// Valores: identMenu.CodMenu,
        /// identMenu.Text
        /// Se for passado null no valores ele lista todos os dados
        /// </summary>
        public List<MenuVO> Listar(MenuVO identMenu)
        {
            return new MenuData().Listar(identMenu);
        }

        /// <summary>
        /// Método para incluir um registro na tabela Menu
        /// Objeto/Parâmetros: (menuVO, codUsuarioOperacao)
        /// Valores: menuVO.Text,
        /// codUsuarioOperacao,
        /// menuVO.Ativo
        /// </summary>
        public void Incluir(MenuVO menuVO, int codUsuarioOperacao)
        {
            new MenuData().Incluir(menuVO, codUsuarioOperacao);
        }
        /// <summary>
        /// Método para alterar um registro na tabela  Menu 
        /// Objeto/Parâmetros: (menuVO, codUsuarioOperacao)
        /// Valores: menuVO.CodMenu,
        /// menuVO.Text,
        /// codUsuarioOperacao,
        /// menuVO.Ativo
        /// </summary>
        public void Alterar(MenuVO menuVO, int codUsuarioOperacao)
        {
            new MenuData().Alterar(menuVO, codUsuarioOperacao);
        }
        /// <summary>
        /// Método para excluir um registro na tabela  Menu
        /// Objeto/Parâmetros: (codMenu, codUsuarioOperacao)
        /// </summary>
        public void Excluir(short? codMenu, int codUsuarioOperacao)
        {
            new MenuData().Excluir(codMenu, codUsuarioOperacao);
        }


    }
}
