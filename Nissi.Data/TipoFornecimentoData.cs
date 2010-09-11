using System;
using System.Collections.Generic;
using Nissi.Model;
using System.Data;

namespace Nissi.DataAccess
{
/// <summary>
/// Summary description for Class1
/// </summary>
    public class TipoFornecimentoData : NissiBaseData
    {


        #region M�todo de Listagem
        /// <summary>
        /// M�todo para executar a proc pr_selecionar_tipofornecimento 
        /// Objeto/Par�metros: (codTipoFornecimento)
        /// </summary>
        public List<TipoFornecimentoVO> Listar(int? codTipoFornecimento)
        {
            OpenCommand("pr_selecionar_TipoFornecimento");
            try
            {
                // Par�metros de entrada
                AddInParameter("@CodTipoFornecimento", DbType.Int32, codTipoFornecimento);

                List<TipoFornecimentoVO> lstTipoFornecimentoVO = new List<TipoFornecimentoVO>();

                IDataReader dr = ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        TipoFornecimentoVO tipoFornecimentoVO = new TipoFornecimentoVO();

                        tipoFornecimentoVO.CodTipoFornecimento = GetReaderValue<short?>(dr, "CodTipoFornecimento");
                        tipoFornecimentoVO.Descricao = GetReaderValue<string>(dr, "Descricao");
                        tipoFornecimentoVO.DataCadastro = GetReaderValue<DateTime>(dr, "DataCadastro");
                        tipoFornecimentoVO.UsuarioInc = GetReaderValue<int>(dr, "UsuarioInc");
                        tipoFornecimentoVO.DataAlteracao = GetReaderValue<DateTime>(dr, "DataAlteracao");
                        tipoFornecimentoVO.UsuarioAlt = GetReaderValue<int>(dr, "UsuarioAlt");

                        lstTipoFornecimentoVO.Add(tipoFornecimentoVO);
                    }
                }
                finally
                {
                    dr.Close();
                }

                return lstTipoFornecimentoVO;
            }
            finally
            {
                CloseCommand();
            }
        }


        // ------------------------------------------------------------------------- // 


        #endregion
        #region M�todos de Inclus�o
        /// <summary>
        /// M�todo para incluir um registro na tabela TipoFornecimento
        /// Objeto/Par�metros: (tipoFornecimento, codUsuarioOperacao)
        /// Valores: tipoFornecimentoVO.Descricao,
        /// codUsuarioOperacao
        /// </summary>
        public int Incluir(TipoFornecimentoVO tipoFornecimentoVO, int codUsuarioOperacao)
        {
            OpenCommand("pr_incluir_TipoFornecimento", true);
            try
            {
                // Par�metros de entrada
                AddInParameter("@Descricao", DbType.String, tipoFornecimentoVO.Descricao);
                AddInParameter("@UsuarioInc", DbType.Int32, codUsuarioOperacao);

                ExecuteNonQuery();
                int chaveGerada = GetReturnValue();

                return chaveGerada;
            }
            finally
            {
                CloseCommand();
            }
        }
        #endregion
        #region M�todos de Altera��o
        /// <summary>
        /// M�todo para alterar um registro na tabela  TipoFornecimento 
        /// </summary>
        /// Objeto/Par�metros: (tipoFornecimento, codUsuarioOperacao)
        /// Valores: tipoFornecimento.CodTipoFornecimento,
        /// tipoFornecimentoVO.Descricao,
        /// codUsuarioOperacao
        /// </summary>
        public void Alterar(TipoFornecimentoVO tipoFornecimentoVO, int codUsuarioOperacao)
        {
            OpenCommand("pr_alterar_TipoFornecimento");
            try
            {
                // Par�metros de entrada
                AddInParameter("@CodTipoFornecimento", DbType.Int16, tipoFornecimentoVO.CodTipoFornecimento);
                AddInParameter("@Descricao", DbType.String, tipoFornecimentoVO.Descricao);
                AddInParameter("@UsuarioAlt", DbType.Int32, codUsuarioOperacao);

                ExecuteNonQuery();
            }
            finally
            {
                CloseCommand();
            }
        }
        #endregion
        #region M�todos de Exclus�o
        // ------------------------------------------------------------------------- //
        /// <summary>
        /// M�todo para excluir um registro na tabela  TipoFornecimento
        /// Objeto/Par�metros: (codTipoFornecimento, codUsuarioOperacao)
        /// </summary>
        public void Excluir(short codTipoFornecimento, int codUsuarioOperacao)
        {
            OpenCommand("pr_excluir_TipoFornecimento");
            try
            {
                // Par�metros de entrada
                AddInParameter("@CodTipoFornecimento", DbType.Int16, codTipoFornecimento);


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
