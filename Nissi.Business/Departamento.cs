using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.DataAccess;
using Nissi.Model;

namespace Nissi.Business
{
    public class Departamento: NissiBaseBusiness
    {
        #region Métodos de Listagem
        /// <summary>
        /// Método para executar a proc pr_selecionar_departamento 
        /// Objeto/Parâmetros: (codDepartamento)
        /// Se for passado null no valores ele lista todos os dados
        /// </summary>
        public List<DepartamentoVO> Listar(int? codDepartamento)
        {
            return new DepartamentoData().Listar(codDepartamento);
        }
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
            return new DepartamentoData().Incluir(departamentoVO, codUsuarioOperacao);
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
        public void Alterar(DepartamentoVO departamentoVO, int codUsuarioOperacao)
        {
            new DepartamentoData().Alterar(departamentoVO, codUsuarioOperacao);
        }
        #endregion
        #region Métodos de Exclusão
        /// <summary>
        /// Método para excluir um registro na tabela  Departamento
        /// Objeto/Parâmetros: (codDepartamento, codUsuarioOperacao)
        /// </summary>
        public void Excluir(short codDepartamento, int codUsuarioOperacao)
        {
            new DepartamentoData().Excluir(codDepartamento, codUsuarioOperacao);
        }
        #endregion

        // ------------------------------------------------------------------------- // 
    }
}
