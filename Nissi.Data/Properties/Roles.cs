using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.DataAccess;
using Nissi.Model;

namespace Nissi.Business
{
    public class Roles: NissiBaseBusiness
    {
                /// <summary>
        /// Método para listar os registros da tabela  Roles 
        /// </summary>
        #region Métodos de Listagem
        public List<RolesVO> Listar()
        {
            return new RolesData().Listar();
        }

        /// <summary>
        /// Método para executar a proc pr_selecionar_perfilnaoassociado 
        /// </summary>
        public List<PerfilAcessoVO> ListarPerfilNaoAssociado(short? codSubMenu)
        {
            List<RolesVO> listaRoles = new RolesData().ListarPerfilNaoAssociado(codSubMenu);
            List<PerfilAcessoVO> listaPerfilAcesso = new List<PerfilAcessoVO>();
            foreach (RolesVO identRoles in listaRoles)
            {
                PerfilAcessoVO identPerfilAcesso = new PerfilAcessoVO();
                identPerfilAcesso.CodPerfilAcesso = identRoles.PerfilAcesso.CodPerfilAcesso;
                identPerfilAcesso.NomPerfilAcesso = identRoles.PerfilAcesso.NomPerfilAcesso;
                listaPerfilAcesso.Add(identPerfilAcesso);
            }
            return listaPerfilAcesso;
        }
                /// <summary>
        /// Método para executar a proc pr_selecionar_perfilassociado 
        /// </summary>
        public List<PerfilAcessoVO> ListarPerfilAssociado(short? codSubMenu)
        {
            List<RolesVO> listaRoles = new RolesData().ListarPerfilAssociado(codSubMenu);
            List<PerfilAcessoVO> listaPerfilAcesso = new List<PerfilAcessoVO>();
            foreach ( RolesVO identRoles in listaRoles )
            {
                PerfilAcessoVO identPerfilAcesso = new PerfilAcessoVO();
                identPerfilAcesso.CodPerfilAcesso = identRoles.PerfilAcesso.CodPerfilAcesso;
                identPerfilAcesso.NomPerfilAcesso = identRoles.PerfilAcesso.NomPerfilAcesso;
                listaPerfilAcesso.Add(identPerfilAcesso);
            }
            return listaPerfilAcesso;
        }
        #endregion

        /// <summary>
        /// Método para incluir um registro na tabela Roles 
        /// </summary>
        #region Métodos de Inclusão
        public void Incluir(List<RolesVO> lstrolesVO, int codUsuarioOperacao)
        {
            new RolesData().Incluir(lstrolesVO, codUsuarioOperacao);
        }
        #endregion

        /// <summary>
        /// Método para alterar um registro na tabela  Roles 
        /// </summary>
        #region Métodos de Alteração
        public void Alterar(RolesVO rolesVO, int codUsuarioOperacao)
        {
            new RolesData().Alterar(rolesVO, codUsuarioOperacao);
        }
        #endregion
        /// <summary>
        /// Método para excluir um registro na tabela  Roles 
        /// </summary>
        #region Métodos de Exclusão
        public void Excluir(List<RolesVO> lstRolesVO)
        {
            new RolesData().Excluir(lstRolesVO);
        }
        #endregion
    }
}
