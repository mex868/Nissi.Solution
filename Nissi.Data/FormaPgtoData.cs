using System;
using System.Data;
using Nissi.Model;
using System.Collections.Generic;

namespace Nissi.DataAccess
{
/// <summary>
/// Summary description for Class1
/// </summary>
    public class FormaPgtoData : NissiBaseData
    {
        #region Método de Listagem
        /// <summary>
        /// Método para executar a proc pr_selecionar_cargo
        /// Objeto/Parâmetros: (codFormaPgto)
        /// Se for passado null no valores ele lista todos os dados
        /// </summary> 
        public List<FormaPgtoVO> Listar(int? codFormaPgto)
        {
            OpenCommand("pr_selecionar_formapgto");
            try
            {
                // Parâmetros de entrada
                AddInParameter("@CodFormaPgto", DbType.Int32, codFormaPgto);

                List<FormaPgtoVO> lstFormaPgtoVO = new List<FormaPgtoVO>();

                IDataReader dr = ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        FormaPgtoVO formaPgtoVO = new FormaPgtoVO();

                        formaPgtoVO.CodFormaPgto = GetReaderValue<short?>(dr, "CodFormaPgto");
                        formaPgtoVO.Descricao = GetReaderValue<string>(dr, "Descricao");
                        formaPgtoVO.Intervalo= GetReaderValue<short>(dr, "intervalo");
                        formaPgtoVO.Parcela = GetReaderValue<short>(dr, "Parcelas");
                        formaPgtoVO.DataCadastro = GetReaderValue<DateTime>(dr, "DataCadastro");
                        formaPgtoVO.UsuarioInc = GetReaderValue<int>(dr, "UsuarioInc");
                        formaPgtoVO.DataAlteracao = GetReaderValue<DateTime>(dr, "DataAlteracao");
                        formaPgtoVO.UsuarioAlt = GetReaderValue<int>(dr, "UsuarioAlt");

                        lstFormaPgtoVO.Add(formaPgtoVO);
                    }
                }
                finally
                {
                    dr.Close();
                }

                return lstFormaPgtoVO;
            }
            finally
            {
                CloseCommand();
            }
        }


        // ------------------------------------------------------------------------- // 


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
            OpenCommand("pr_incluir_FormaPgto", true);
            try
            {
                // Parâmetros de entrada
                AddInParameter("@Descricao", DbType.String, formaPgtoVO.Descricao);
                AddInParameter("@Parcela", DbType.Int32, formaPgtoVO.Parcela);
                AddInParameter("@Intervalo", DbType.Int32, formaPgtoVO.Intervalo);
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
            OpenCommand("pr_alterar_FormaPgto");
            try
            {
                // Parâmetros de entrada
                AddInParameter("@CodFormaPgto", DbType.Int16, formaPgtoVO.CodFormaPgto);
                AddInParameter("@Descricao", DbType.String, formaPgtoVO.Descricao);
                AddInParameter("@Parcela", DbType.Int32, formaPgtoVO.Parcela);
                AddInParameter("@Intervalo", DbType.Int32, formaPgtoVO.Intervalo);
                AddInParameter("@UsuarioAlt", DbType.Int32, codUsuarioOperacao);

                ExecuteNonQuery();
            }
            finally
            {
                CloseCommand();
            }
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
            OpenCommand("pr_excluir_formapgto");
            try
            {
                // Parâmetros de entrada
                AddInParameter("@CodFormaPgto", DbType.Int16, codFormaPgto);


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
