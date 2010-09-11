using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.Model;
using Nissi.DataAccess;

namespace Nissi.Business
{
    public class CFOP
    {
                /// <summary>
        /// Método para executar a proc pr_selecionar_cfop 
        /// </summary>
        public List<CFOPVO> Listar(CFOPVO identCFOP)
        {
            return new CFOPData().Listar(identCFOP);
        }
        // ------------------------------------------------------------------------- // 
        /// <summary>
        /// Método para executar a proc pr_incluir_cfop 
        /// </summary>
        public int Incluir(CFOPVO identCFOP, int usuarioAtivo)
        {
           return new CFOPData().Incluir(identCFOP, usuarioAtivo);
        }
        // ------------------------------------------------------------------------- // 
        /// <summary>
        /// Método para executar a proc pr_alterar_cfop 
        /// </summary>
        public void Alterar(CFOPVO identCFOP)
        {
            new CFOPData().Alterar(identCFOP);
        }
                // ------------------------------------------------------------------------- //
        /// <summary>
        /// Método para executar a proc pr_excluir_cfop
        /// </summary>
        /// <param name="codCFOP"></param>
        public void Excluir(int codCFOP)
        {
            new CFOPData().Excluir(codCFOP);

        }
    }
}
