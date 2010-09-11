using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Nissi.Model;

namespace Nissi.DataAccess
{
    public class SubMenuData: NissiBaseData
    {

        /// <summary>
        /// Método para executar a proc pr_selecionar_submenu 
        /// </summary>
        #region Métodos de Listagem
        public List<SubMenuVO> Listar(SubMenuVO identSuMenu)
        {
            OpenCommand("pr_selecionar_submenu");
            try
            {
                // Parâmetros de entrada
                AddInParameter("@CodMenu", DbType.Int16, identSuMenu.CodMenu);
                AddInParameter("@CodSubMenu", DbType.Int16, identSuMenu.CodSubMenu);

                List<SubMenuVO> lstSubMenuVO = new List<SubMenuVO>();

                IDataReader dr = ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        SubMenuVO subMenuVO = new SubMenuVO();

                        subMenuVO.CodSubMenu = GetReaderValue<short?>(dr, "CodSubMenu");
                        subMenuVO.CodMenu = GetReaderValue<short?>(dr, "CodMenu");
                        subMenuVO.Text = GetReaderValue<string>(dr, "text");
                        subMenuVO.Url = GetReaderValue<string>(dr, "url");
                        subMenuVO.Resolveurl = GetReaderValue<bool>(dr, "resolveurl");
                        subMenuVO.DataCadastro = GetReaderValue<DateTime>(dr, "DataCadastro");
                        subMenuVO.UsuarioInc = GetReaderValue<int>(dr, "UsuarioInc");
                        subMenuVO.DataAlteracao = GetReaderValue<DateTime>(dr, "DataAlteracao");
                        subMenuVO.UsuarioAlt = GetReaderValue<int>(dr, "UsuarioAlt");
                        subMenuVO.Ativo = GetReaderValue<bool>(dr, "Ativo");

                        lstSubMenuVO.Add(subMenuVO);
                    }
                }
                finally
                {
                    dr.Close();
                }

                return lstSubMenuVO;
            }
            finally
            {
                CloseCommand();
            }
        }
       
        #endregion

        // ------------------------------------------------------------------------- // 


        /// <summary>
        /// Método para incluir um registro na tabela SubMenu 
        /// </summary>
        #region Métodos de Inclusão
        public void Incluir(SubMenuVO subMenuVO, int codUsuarioOperacao)
        {
            OpenCommand("pr_incluir_SubMenu", true);
            try
            {
                // Parâmetros de entrada
                AddInParameter("@CodMenu", DbType.Int16, subMenuVO.CodMenu);
                AddInParameter("@text", DbType.String, subMenuVO.Text);
                AddInParameter("@url", DbType.String, subMenuVO.Url);
                AddInParameter("@resolveurl", DbType.Boolean, subMenuVO.Resolveurl);
                AddInParameter("@UsuarioInc", DbType.Int32, codUsuarioOperacao);
                AddInParameter("@Ativo", DbType.Boolean, subMenuVO.Ativo);

                ExecuteNonQuery();
            }
            finally
            {
                CloseCommand();
            }
        }
        #endregion

        // ------------------------------------------------------------------------- // 
        /// <summary>
        /// Método para alterar um registro na tabela  SubMenu 
        /// </summary>
        #region Métodos de Alteração
        public void Alterar(SubMenuVO subMenuVO, int codUsuarioOperacao)
        {
            OpenCommand("pr_alterar_SubMenu");
            try
            {
                // Parâmetros de entrada
                AddInParameter("@CodSubMenu", DbType.Int16, subMenuVO.CodSubMenu);
                AddInParameter("@text", DbType.String, subMenuVO.Text);
                AddInParameter("@url", DbType.String, subMenuVO.Url);
                AddInParameter("@resolveurl", DbType.Boolean, subMenuVO.Resolveurl);
                AddInParameter("@UsuarioAlt", DbType.Int32, codUsuarioOperacao);
                AddInParameter("@Ativo", DbType.Boolean, subMenuVO.Ativo);

                ExecuteNonQuery();
            }
            finally
            {
                CloseCommand();
            }
        }
        #endregion

        // ------------------------------------------------------------------------- // 

        /// <summary>
        /// Método para excluir um registro na tabela  SubMenu 
        /// </summary>
        #region Métodos de Exclusão
        public void Excluir(short? codSubMenu, int codUsuarioOperacao)
        {
            OpenCommand("pr_excluir_SubMenu");
            try
            {
                // Parâmetros de entrada
                AddInParameter("@CodSubMenu", DbType.Int16, codSubMenu);
                AddInParameter("@CodUsuarioOperacao", DbType.Int32, codUsuarioOperacao);

                ExecuteNonQuery();
            }
            finally
            {
                CloseCommand();
            }
        }
        #endregion

        // ------------------------------------------------------------------------- // 

    }
}
