using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.DataAccess;
using Nissi.Model;

namespace Nissi.Business
{
    public class Parametro
    {
        /// <summary>
        /// Método para executar a proc pr_selecionar_parametronfe 
        /// </summary>
        public List<ParametroVO> Listar()
        {
            return new ParametroData().Listar();
        }


        // ------------------------------------------------------------------------- // 
        /// <summary>
        /// Método para executar a proc pr_incluir_parametronfe 
        /// </summary>
        public int Incluir(ParametroVO identParametro)
        {
            return new ParametroData().Incluir(identParametro);
        }


        // ------------------------------------------------------------------------- // 
        /// <summary>
        /// Método para executar a proc pr_alterar_parametronfe 
        /// </summary>
        public void Alterar(ParametroVO identParametro)
        {
            new ParametroData().Alterar(identParametro);
        }


        // ------------------------------------------------------------------------- // 

        /// <summary>
        /// Método para executar a proc pr_excluir_parametronfe 
        /// </summary>
        public void Excluir()
        {
            new ParametroData().Excluir();
        }


        // ------------------------------------------------------------------------- // 


    }
}
