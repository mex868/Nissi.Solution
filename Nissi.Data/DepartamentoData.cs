using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Nissi.Model;


namespace Nissi.DataAccess
{
    public class DepartamentoData: NissiBaseData
    {
        #region Métodos de Listagem
        /// <summary>
        /// Método para executar a proc pr_selecionar_departamento 
        /// Objeto/Parâmetros: (codDepartamento)
        /// Se for passado null no valores ele lista todos os dados
        /// </summary>
        public List<DepartamentoVO> Listar(int? codDepartamento)
        {
            OpenCommand("pr_selecionar_departamento");
            try
            {
                // Parâmetros de entrada
                AddInParameter("@CodDepartamento", DbType.Int32, codDepartamento);

                List<DepartamentoVO> lstdepartamentoVO = new List<DepartamentoVO>();

                IDataReader dr = ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        DepartamentoVO departamentoVO = new DepartamentoVO();

                        departamentoVO.CodDepartamento = GetReaderValue<short?>(dr, "CodDepartamento");
                        departamentoVO.Nome = GetReaderValue<string>(dr, "Nome");
                        departamentoVO.DataCadastro = GetReaderValue<DateTime?>(dr, "DataCadastro");
                        departamentoVO.UsuarioInc = GetReaderValue<int?>(dr, "UsuarioInc");
                        departamentoVO.DataAlteracao = GetReaderValue<DateTime?>(dr, "DataAlteracao");
                        departamentoVO.UsuarioAlt = GetReaderValue<int?>(dr, "UsuarioAlt");

                        lstdepartamentoVO.Add(departamentoVO);
                    }
                }
                finally
                {
                    dr.Close();
                }

                return lstdepartamentoVO;
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
        /// Método para incluir um registro na tabela Departamento 
        /// Objeto/Parâmetros: (departamentoVO, codUsuarioOperacao)
        /// Valores: departamentoVO.Nome,
        /// codUsuarioOperacao
        /// </summary>        
        public int Incluir(DepartamentoVO departamentoVO, int codUsuarioOperacao)
        {
            OpenCommand("pr_incluir_Departamento", true);
            try
            {
                // Parâmetros de entrada
                AddInParameter("@Nome", DbType.String, departamentoVO.Nome);
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
        /// <summary>
        /// Método para alterar um registro na tabela  Departamento
        /// Objeto/Parâmetros: (departamentoVO, codUsuarioOperacao)
        /// Valores: departamentoVO.CodDepartamento,
        /// departamentoVO.Nome,
        /// departamento.UsuarioAlt
        /// </summary>
        // ------------------------------------------------------------------------- // 
        public void Alterar(DepartamentoVO departamentoVO, int codUsuarioOperacao)
        {
            OpenCommand("pr_alterar_Departamento");
            try
            {
                // Parâmetros de entrada
                AddInParameter("@CodDepartamento", DbType.Int16, departamentoVO.CodDepartamento);
                AddInParameter("@Nome", DbType.String, departamentoVO.Nome);
                AddInParameter("@UsuarioAlt", DbType.Int32, departamentoVO.UsuarioAlt);

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
        /// Método para excluir um registro na tabela  Departamento
        /// Objeto/Parâmetros: (codDepartamento, codUsuarioOperacao)
        /// </summary>
        public void Excluir(short codDepartamento, int codUsuarioOperacao)
        {
            OpenCommand("pr_excluir_Departamento");
            try
            {
                // Parâmetros de entrada
                AddInParameter("@CodDepartamento", DbType.Int16, codDepartamento);

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
