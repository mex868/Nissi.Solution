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


        #region M�todsos de Listagem
        /// <summary>
        /// M�todo para executar a proc pr_selecionar_menu
        /// Objeto/Par�metros: identMenu
        /// Valores: identMenu.CodMenu,
        /// identMenu.Text
        /// Se for passado null no valores ele lista todos os dados
        /// </summary>
        public List<MenuVO> Listar(MenuVO identMenu)
        {
            OpenCommand("pr_selecionar_menu");
            try
            {
                // Par�metros de entrada
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
        #region M�todos de Inclus�o
        /// <summary>
        /// M�todo para incluir um registro na tabela Menu
        /// Objeto/Par�metros: (menuVO, codUsuarioOperacao)
        /// Valores: menuVO.Text,
        /// codUsuarioOperacao,
        /// menuVO.Ativo
        /// </summary>
        public void Incluir(MenuVO menuVO, int codUsuarioOperacao)
        {
            OpenCommand("pr_incluir_Menu");
            try
            {
                // Par�metros de entrada
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
        #region M�todos de Altera��o
        /// <summary>
        /// M�todo para alterar um registro na tabela  Menu 
        /// Objeto/Par�metros: (menuVO, codUsuarioOperacao)
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
                // Par�metros de entrada
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
        #region M�todos de Exclus�o
        /// <summary>
        /// M�todo para excluir um registro na tabela  Menu
        /// Objeto/Par�metros: (codMenu, codUsuarioOperacao)
        /// </summary>
        public void Excluir(short? codMenu, int codUsuarioOperacao)
        {
            OpenCommand("pr_excluir_Menu");
            try
            {
                // Par�metros de entrada
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
