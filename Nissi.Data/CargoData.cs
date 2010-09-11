using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Nissi.Model;

namespace Nissi.DataAccess
{
    public class CargoData: NissiBaseData
    {
        #region Método de Listagem

        /// <summary>
        /// Método para executar a proc pr_selecionar_cargo
        /// Objeto/Parametros: (codCargo)
        /// Se for passado null no valores ele lista todos os dados
        /// </summary>
        public List<CargoVO> Listar(short? codCargo)
        {
            OpenCommand("pr_selecionar_cargo");
            try
            {
                // Parâmetros de entrada
                AddInParameter("@CodCargo", DbType.Int16, codCargo);

                List<CargoVO> lstCargoVO = new List<CargoVO>();

                IDataReader dr = ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        CargoVO cargoVO = new CargoVO();

                        cargoVO.CodCargo = GetReaderValue<short?>(dr, "CodCargo");
                        cargoVO.Nome = GetReaderValue<string>(dr, "Nome");
                        cargoVO.DataCadastro = GetReaderValue<DateTime?>(dr, "DataCadastro");
                        cargoVO.UsuarioInc = GetReaderValue<int?>(dr, "UsuarioInc");
                        cargoVO.DataAlteracao = GetReaderValue<DateTime?>(dr, "DataAlteracao");
                        cargoVO.UsuarioAlt = GetReaderValue<int?>(dr, "UsuarioAlt");

                        lstCargoVO.Add(cargoVO);
                    }
                }
                finally
                {
                    dr.Close();
                }

                return lstCargoVO;
            }
            finally
            {
                CloseCommand();
            }
        }


        // ------------------------------------------------------------------------- // 


        #endregion
        /// <summary>
        /// Método para incluir um registro na tabela Cargo
        /// Objeto/Parametros: (cargoVO, codUsuarioOperacao)
        /// Valores: cargoVO.Nome,
        /// codUsuarioOperacao
        /// </summary>
        #region Métodos de Inclusão

        public short Incluir(CargoVO cargoVO, int codUsuarioOperacao)
        {
            OpenCommand("pr_incluir_Cargo", true);
            try
            {
                // Parâmetros de entrada
                AddInParameter("@Nome", DbType.String, cargoVO.Nome);
                AddInParameter("@UsuarioInc", DbType.Int32, codUsuarioOperacao);

                ExecuteNonQuery();
                short chaveGerada =Convert.ToInt16(GetReturnValue());

                return chaveGerada;
            }
            finally
            {
                CloseCommand();
            }
        }
        #endregion
        // ------------------------------------------------------------------------- //
        /// <summary>
        /// Método para alterar um registro na tabela  Cargo 
        /// Objeto/Valores: (cargoVO, codUsuarioOperacao)
        /// Valores: cargoVO.CodCargo,
        /// cargoVO.Nome,
        /// cargoVO.UsuarioAlt
        /// </summary>
        #region Métodos de Alteração
        public void Alterar(CargoVO cargoVO, int codUsuarioOperacao)
        {
            OpenCommand("pr_alterar_Cargo");
            try
            {
                // Parâmetros de entrada
                AddInParameter("@CodCargo", DbType.Int16, cargoVO.CodCargo);
                AddInParameter("@Nome", DbType.String, cargoVO.Nome);
                AddInParameter("@UsuarioAlt", DbType.Int32, cargoVO.UsuarioAlt);

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
        /// Método para excluir um registro na tabela  Cargo
        /// Objeto/Parametros: (codCargo)
        /// </summary>
        #region Métodos de Exclusão
        public void Excluir(short codCargo, int codUsuarioOperacao)
        {
            OpenCommand("pr_excluir_Cargo");
            try
            {
                // Parâmetros de entrada
                AddInParameter("@CodCargo", DbType.Int16, codCargo);


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
