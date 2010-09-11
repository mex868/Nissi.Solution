using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Nissi.Model;

namespace Nissi.DataAccess
{
    public class RoleData: NissiBaseData
    {
        /// <summary>
        /// Método para listar os registros da tabela  Roles 
        /// </summary>
        #region Métodos de Listagem
        public List<RolesVO> Listar()
        {
            OpenCommand("pr_selecionar_Roles");
            try
            {
                List<RolesVO> lstRolesVO = new List<RolesVO>();

                IDataReader dr = ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        RolesVO rolesVO = new RolesVO();

                        rolesVO.CodRoles = GetReaderValue<short?>(dr, "CodRoles");
                        rolesVO.CodSubMenu = GetReaderValue<short?>(dr, "CodSubMenu");
                        rolesVO.PerfilAcesso.CodPerfilAcesso = GetReaderValue<short?>(dr, "CodPefilAcesso");
                        rolesVO.DataCadastro = GetReaderValue<DateTime?>(dr, "DataCadastro");
                        rolesVO.UsuarioInc = GetReaderValue<int?>(dr, "UsuarioInc");
                        rolesVO.DataAlteracao = GetReaderValue<DateTime?>(dr, "DataAlteracao");
                        rolesVO.UsuarioAlt = GetReaderValue<int?>(dr, "UsuarioAlt");

                        lstRolesVO.Add(rolesVO);
                    }
                }
                finally
                {
                    dr.Close();
                }

                return lstRolesVO;
            }
            finally
            {
                CloseCommand();
            }
        }
        /// <summary>
        /// Método para executar a proc pr_selecionar_perfilnaoassociado 
        /// </summary>
        public List<RolesVO> ListarPerfilNaoAssociado(short? codSubMenu)
        {
            OpenCommand("pr_selecionar_perfilnaoassociado");
            try
            {
                // Parâmetros de entrada
                AddInParameter("@CodSubMenu", DbType.Int16, codSubMenu);

                List<RolesVO> lstRolesVO = new List<RolesVO>();

                IDataReader dr = ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        RolesVO rolesVO = new RolesVO();

                        rolesVO.PerfilAcesso.CodPerfilAcesso = GetReaderValue<short>(dr, "CodPerfilAcesso");
                        rolesVO.PerfilAcesso.NomPerfilAcesso = GetReaderValue<string>(dr, "NomPerfilAcesso");
                        rolesVO.PerfilAcesso.DescPerfilAcesso = GetReaderValue<string>(dr, "DescPerfilAcesso");
                        lstRolesVO.Add(rolesVO);
                    }
                }
                finally
                {
                    dr.Close();
                }

                return lstRolesVO;
            }
            finally
            {
                CloseCommand();
            }
        }


        // ------------------------------------------------------------------------- // 

        /// <summary>
        /// Método para executar a proc pr_selecionar_perfilassociado 
        /// </summary>
        public List<RolesVO> ListarPerfilAssociado(short? codSubMenu)
        {
            OpenCommand("pr_selecionar_perfilassociado");
            try
            {
                // Parâmetros de entrada
                AddInParameter("@CodSubMenu", DbType.Int16, codSubMenu);

                List<RolesVO> lstRolesVO = new List<RolesVO>();

                IDataReader dr = ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        RolesVO rolesVO = new RolesVO();

                        rolesVO.CodRoles = GetReaderValue<short>(dr, "CodRoles");
                        rolesVO.CodSubMenu = GetReaderValue<short>(dr, "CodSubMenu");
                        rolesVO.PerfilAcesso.CodPerfilAcesso = GetReaderValue<short>(dr, "CodPerfilAcesso");
                        rolesVO.PerfilAcesso.NomPerfilAcesso = GetReaderValue<string>(dr, "NomPerfilAcesso");
                        lstRolesVO.Add(rolesVO);
                    }
                }
                finally
                {
                    dr.Close();
                }

                return lstRolesVO;
            }
            finally
            {
                CloseCommand();
            }
        }

        // ------------------------------------------------------------------------- // 

        #endregion

        // ------------------------------------------------------------------------- // 

        
        /// <summary>
        /// Método para incluir um registro na tabela Roles 
        /// </summary>
        #region Métodos de Inclusão
        public void Incluir(List<RolesVO> lstrolesVO, int codUsuarioOperacao)
        {

            foreach (RolesVO rolesVO in lstrolesVO)
            {
                OpenCommand("pr_incluir_Roles");
                try
                {
                    // Parâmetros de entrada
                    AddInParameter("@CodSubMenu", DbType.Int16, rolesVO.CodSubMenu);
                    AddInParameter("@CodPefilAcesso", DbType.Int16, rolesVO.PerfilAcesso.CodPerfilAcesso);
                    AddInParameter("@UsuarioInc", DbType.Int32, codUsuarioOperacao);
                    ExecuteNonQuery();
                }
                finally
                {
                    CloseCommand();
                }
            }
        }
        #endregion

        // ------------------------------------------------------------------------- // 
        /// <summary>
        /// Método para alterar um registro na tabela  Roles 
        /// </summary>
        #region Métodos de Alteração
        public void Alterar(RolesVO rolesVO, int codUsuarioOperacao)
        {
            OpenCommand("pr_alterar_Roles");
            try
            {
                // Parâmetros de entrada
                AddInParameter("@CodRoles", DbType.Int16, rolesVO.CodRoles);
                AddInParameter("@CodSubMenu", DbType.Int16, rolesVO.CodSubMenu);
                AddInParameter("@CodPefilAcesso", DbType.Int16, rolesVO.PerfilAcesso.CodPerfilAcesso);
                AddInParameter("@UsuarioAlt", DbType.Int32, codUsuarioOperacao);

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
        /// Método para excluir um registro na tabela  Roles 
        /// </summary>
        #region Métodos de Exclusão
        public void Excluir(List<RolesVO> lstRolesVO)
        {
            foreach (RolesVO rolesVO in lstRolesVO)
            {
                OpenCommand("pr_excluir_Roles");
                try
                {
                    // Parâmetros de entrada
                    AddInParameter("@CodSubMenu", DbType.Int16, rolesVO.CodSubMenu);
                    AddInParameter("@CodPerfilAcesso", DbType.Int16, rolesVO.PerfilAcesso.CodPerfilAcesso);
                    ExecuteNonQuery();
                }
                finally
                {
                    CloseCommand();
                }
            }
        }
        #endregion

        // ------------------------------------------------------------------------- // 



    }
}
