using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.Model;
using Nissi.DataAccess;

namespace Nissi.Business
{
    public class TipoFornecimento : NissiBaseBusiness
    {

        #region Método de Listagem
        /// <summary>
        /// Método para executar a proc pr_selecionar_tipofornecimento 
        /// Objeto/Parâmetros: (codTipoFornecimento)
        /// </summary>
        public List<TipoFornecimentoVO> Listar(int? codTipoFornecimento)
        {
            return new TipoFornecimentoData().Listar(codTipoFornecimento);
        }
        #endregion
        #region Métodos de Inclusão
        /// <summary>
        /// Método para incluir um registro na tabela TipoFornecimento
        /// Objeto/Parâmetros: (tipoFornecimento, codUsuarioOperacao)
        /// Valores: tipoFornecimentoVO.Descricao,
        /// codUsuarioOperacao
        /// </summary>
        public int Incluir(TipoFornecimentoVO tipoFornecimentoVO, int codUsuarioOperacao)
        {
            return new TipoFornecimentoData().Incluir(tipoFornecimentoVO, codUsuarioOperacao);
        }
        #endregion
        #region Métodos de Alteração
        /// <summary>
        /// Método para alterar um registro na tabela  TipoFornecimento 
        /// </summary>
        /// Objeto/Parâmetros: (tipoFornecimento, codUsuarioOperacao)
        /// Valores: tipoFornecimento.CodTipoFornecimento,
        /// tipoFornecimentoVO.Descricao,
        /// codUsuarioOperacao
        /// </summary>
        public void Alterar(TipoFornecimentoVO tipoFornecimentoVO, int codUsuarioOperacao)
        {
            new TipoFornecimentoData().Alterar(tipoFornecimentoVO, codUsuarioOperacao);
        }
        #endregion
        #region Métodos de Exclusão
        /// <summary>
        /// Método para excluir um registro na tabela  TipoFornecimento
        /// Objeto/Parâmetros: (codTipoFornecimento, codUsuarioOperacao)
        /// </summary>
        public void Excluir(short codTipoFornecimento, int codUsuarioOperacao)
        {
            new TipoFornecimentoData().Excluir(codTipoFornecimento, codUsuarioOperacao);
        }
        #endregion
        // ------------------------------------------------------------------------- // 
    }
}
