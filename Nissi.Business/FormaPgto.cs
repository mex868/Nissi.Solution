using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.Model;
using Nissi.DataAccess;

namespace Nissi.Business
{
    public class FormaPgto : NissiBaseBusiness
    {
        #region Método de Listagem
        /// <summary>
        /// Método para executar a proc pr_selecionar_cargo
        /// Objeto/Parâmetros: (codFormaPgto)
        /// Se for passado null no valores ele lista todos os dados
        /// </summary> 
        public List<FormaPgtoVO> Listar()
        {
            return new FormaPgtoData().Listar(null);
        }

        public List<FormaPgtoVO> ListarPorCodigo(int? codFormaPgto)
        {
            return new FormaPgtoData().Listar(codFormaPgto);
        }

        // ------------------------------------------------------------------------- // 
        
        public List<FormaPgtoVO> ListarPorDescricao(string descricao)
        {
            return new FormaPgtoData().ListarPorDescricao(descricao);
        }

        #endregion
        #region Métodos de Inclusão
        /// <summary>
        /// Método para incluir um registro na tabela Cargo
        /// Objeto/Parâmetros: (formaPgtoVo, codUsuarioOperacao)
        /// Valores: formaPgtoVO.Descricao,
        /// formaPgtoVO.Parcela,
        /// formaPgtoVO.Intervalo,
        /// codUsuarioOperacao
        /// </summary>
        public int Incluir(FormaPgtoVO formaPgtoVO, int codUsuarioOperacao)
        {
           return new FormaPgtoData().Incluir(formaPgtoVO, codUsuarioOperacao);
        }
        #endregion
        #region Métodos de Alteração
        // ------------------------------------------------------------------------- //
        /// <summary>
        /// Método para alterar um registro na tabela  Cargo
        /// Objeto/Parâmetros: (formaPgtoVO, codUsuarioOperacao)
        /// Valores: formaPgtoVO.CodFormaPgto,
        /// formaPgtoVO.Descricao,
        /// formaPgtoVO.Parcela,
        /// codUsuarioOperacao
        /// </summary>
        public void Alterar(FormaPgtoVO formaPgtoVO, int codUsuarioOperacao)
        {
            new FormaPgtoData().Alterar(formaPgtoVO, codUsuarioOperacao); 
        }
        #endregion
        #region Métodos de Exclusão
        // ------------------------------------------------------------------------- //
        /// <summary>
        /// Método para excluir um registro na tabela  Cargo
        /// Objeto/Parâmetros: (codFormaPgto, codUsuarioOperacao)
        /// </summary>
        public void Excluir(short codFormaPgto, int codUsuarioOperacao)
        {
            new FormaPgtoData().Excluir(codFormaPgto, codUsuarioOperacao);
        }
        #endregion
    }
}
