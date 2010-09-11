using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.DataAccess;
using Nissi.Model;

namespace Nissi.Business
{
    public class Cargo: NissiBaseBusiness
    {
        
        
        #region Método de Listagem

        /// <summary>
        /// Método para executar a proc pr_selecionar_cargo 
        /// Se for passado null no valores ele lista todos os dados
        /// </summary>
        public List<CargoVO> Listar(short? codCargo)
        {
            return new CargoData().Listar(codCargo);
        }
                #endregion

        /// <summary>
        /// Método para incluir um registro na tabela Cargo 
        /// </summary>
        #region Métodos de Inclusão
        public short Incluir(CargoVO cargoVO, int codUsuarioOperacao)
        {
            return new CargoData().Incluir(cargoVO, codUsuarioOperacao);
        }
        #endregion
        // ------------------------------------------------------------------------- //
        /// <summary>
        /// Método para alterar um registro na tabela  Cargo 
        /// </summary>
        #region Métodos de Alteração
        public void Alterar(CargoVO cargoVO, int codUsuarioOperacao)
        {
            new CargoData().Alterar(cargoVO, codUsuarioOperacao);
        }
        #endregion
        // ------------------------------------------------------------------------- //
        /// <summary>
        /// Método para excluir um registro na tabela  Cargo 
        /// </summary>
        #region Métodos de Exclusão
        public void Excluir(short codCargo, int codUsuarioOperacao)
        {
            new CargoData().Excluir( codCargo,  codUsuarioOperacao);
        }
        #endregion
        // ------------------------------------------------------------------------- // 
    }
}
