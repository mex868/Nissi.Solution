using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.Model;
using Nissi.DataAccess;

namespace Nissi.Business
{
    public class MensagemNF : NissiBaseBusiness
    {
        /// <summary>
        /// Método para listar os registros da tabela  MensagemNF 
        /// </summary>
        #region Métodos de Listagem
        public List<MensagemNFVO> Listar(MensagemNFVO identMensagemNF)
        {
            return new MensagemNFData().Listar(identMensagemNF);
        }
        #endregion
        // ------------------------------------------------------------------------- // 
        /// <summary>
        /// Método para incluir um registro na tabela  MensagemNF 
        /// </summary>
        #region Métodos de Inclusão
        public int Incluir(MensagemNFVO identMensagemNF, int codUsuarioOperacao)
        {
           return new MensagemNFData().Incluir(identMensagemNF, codUsuarioOperacao);
        }
        #endregion
        // ------------------------------------------------------------------------- // 
        /// <summary>
        /// Método para alterar um registro na tabela  MensagemNF 
        /// </summary>
        #region Métodos de Alteração
        public void Alterar(MensagemNFVO identMensagemNF, int codUsuarioOperacao)
        {
            new MensagemNFData().Alterar(identMensagemNF, codUsuarioOperacao);
        }
        #endregion
        // ------------------------------------------------------------------------- // 
        /// <summary>
        /// Método para excluir um registro na tabela  MensagemNF 
        /// </summary>
        #region Métodos de Exclusão
        public void Excluir(int codMensagemNF, int codUsuarioOperacao)
        {
            new MensagemNFData().Excluir(codMensagemNF, codUsuarioOperacao);
        }
        #endregion
        // ------------------------------------------------------------------------- //
    }
}
