using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.DataAccess;
using Nissi.Model;

namespace Nissi.Business
{
    public class PerfilAcesso: NissiBaseBusiness
    {
        /// <summary>
        /// Método para executar a proc pr_selecionar_perfilacesso 
        /// </summary>
        #region Método de Seleção
        public List<PerfilAcessoVO> Listar(PerfilAcessoVO perfilAcessoVO)
        {
            return new PerfilAcessoData().Listar(perfilAcessoVO);
        }
        #endregion
        /// <summary>
        /// Método para incluir um registro na tabela PerfilAcesso 
        /// </summary>
        #region Métodos de Inclusão
        public void Incluir(PerfilAcessoVO perfilAcessoVO, int codUsuarioOperacao)
        {
            new PerfilAcessoData().Incluir(perfilAcessoVO, codUsuarioOperacao);
        }
        #endregion
        /// <summary>
        /// Método para alterar um registro na tabela  PerfilAcesso 
        /// </summary>
        #region Métodos de Alteração
        public void Alterar(PerfilAcessoVO perfilAcessoVO, int codUsuarioOperacao)
        {
            new PerfilAcessoData().Alterar(perfilAcessoVO, codUsuarioOperacao);
        }
        #endregion
        /// <summary>
        /// Método para excluir um registro na tabela  PerfilAcesso 
        /// </summary>
        #region Métodos de Exclusão
        public void Excluir(short? codPerfilAcesso, int codUsuarioOperacao)
        {
            new PerfilAcessoData().Excluir(codPerfilAcesso, codUsuarioOperacao);
        }
        #endregion
    }
}
