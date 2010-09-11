using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Nissi.Model;

namespace Nissi.DataAccess
{
    public class MenuData : NissiBaseData
    {


        #region Métodsos de Listagem
        /// <summary>
        /// Método para executar a proc pr_selecionar_menu
        /// Objeto/Parâmetros: identMenu
        /// Valores: identMenu.CodMenu,
        /// identMenu.Text
        /// Se for passado null no valores ele lista todos os dados
        /// </summary>
        public List<MenuVO> Listar(MenuVO identMenu)
        {
            OpenCommand("pr_selecionar_menu");
            try
            {
                // Parâmetros de entrada
                AddInParameter("@CodMenu", DbType.Int32, identMenu.CodMenu);
                AddInParameter("@Text", DbType.String, identMenu.Text);

                List<MenuVO> lstMenuVO = new List<MenuVO>();

                IDataReader dr = ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        MenuVO menuVO = new MenuVO();

                        menuVO.CodMenu = GetReaderValue<short?>(dr, "CodMenu");
                        menuVO.Text = GetReaderValue<string>(dr, "text");
                        menuVO.DataCadastro = GetReaderValue<DateTime?>(dr, "DataCadastro");
                        menuVO.UsuarioInc = GetReaderValue<int?>(dr, "UsuarioInc");
                        menuVO.DataAlteracao = GetReaderValue<DateTime?>(dr, "DataAlteracao");
                        menuVO.UsuarioAlt = GetReaderValue<int?>(dr, "UsuarioAlt");
                        menuVO.Ativo = GetReaderValue<bool?>(dr, "Ativo");

                        lstMenuVO.Add(menuVO);
                    }
                }
                finally
                {
                    dr.Close();
                }

                return lstMenuVO;
            }
            finally
            {
                CloseCommand();
            }
        }
        #endregion
        #region Métodos de Inclusão
        /// <summary>
        /// Método para incluir um registro na tabela Menu
        /// Objeto/Parâmetros: (menuVO, codUsuarioOperacao)
        /// Valores: menuVO.Text,
        /// codUsuarioOperacao,
        /// menuVO.Ativo
        /// </summary>
        public void Incluir(MenuVO menuVO, int codUsuarioOperacao)
        {
            OpenCommand("pr_incluir_Menu");
            try
            {
                // Parâmetros de entrada
                AddInParameter("@text", DbType.String, menuVO.Text);
                AddInParameter("@UsuarioInc", DbType.Int32, codUsuarioOperacao);
                AddInParameter("@Ativo", DbType.Boolean, menuVO.Ativo);
                ExecuteNonQuery();
            }
            finally
            {
                CloseCommand();
            }
        }
        #endregion
        #region Métodos de Alteração
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
            OpenCommand("pr_alterar_Menu");
            try
            {
                // Parâmetros de entrada
                AddInParameter("@CodMenu", DbType.Int16, menuVO.CodMenu);
                AddInParameter("@text", DbType.String, menuVO.Text);
                AddInParameter("@UsuarioAlt", DbType.Int32, codUsuarioOperacao);
                AddInParameter("@Ativo", DbType.Boolean, menuVO.Ativo);

                ExecuteNonQuery();
            }
            finally
            {
                CloseCommand();
            }
        }
        #endregion
        #region Métodos de Exclusão
        /// <summary>
        /// Método para excluir um registro na tabela  Menu
        /// Objeto/Parâmetros: (codMenu, codUsuarioOperacao)
        /// </summary>
        public void Excluir(short? codMenu, int codUsuarioOperacao)
        {
            OpenCommand("pr_excluir_Menu");
            try
            {
                // Parâmetros de entrada
                AddInParameter("@CodMenu", DbType.Int16, codMenu);
                ExecuteNonQuery();
            }
            finally
            {
                CloseCommand();
            }
        }
        #endregion
    }
}
