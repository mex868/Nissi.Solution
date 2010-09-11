using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.DataAccess;
using Nissi.Model;

namespace Nissi.Business
{
    public class Nfe
    {
        /// <summary>
        /// Método para executar a proc pr_selecionar_nfe 
        /// </summary>
        public List<NfeVO> Listar(int? codNF)
        {
            return new NfeData().Listar(codNF);
        }


        // ------------------------------------------------------------------------- // 
        /// <summary>
        /// Método para executar a proc pr_incluir_nfe 
        /// </summary>
        public void Incluir(NfeVO identNFe, int? codUsuarioInc)
        {
            new NfeData().Incluir(identNFe, codUsuarioInc);
        }


        // ------------------------------------------------------------------------- // 
        /// <summary>
        /// Método para executar a proc pr_alterar_nfe 
        /// </summary>
        public void Alterar(NfeVO identNfe, int? codUsuarioAlt)
        {
            new NfeData().Alterar(identNfe, codUsuarioAlt);
        }


        // ------------------------------------------------------------------------- // 
        /// <summary>
        /// Método para executar a proc pr_excluir_nfe 
        /// </summary>
        public void Excluir(int? codNF)
        {
            new NfeData().Excluir(codNF);
        }


        // ------------------------------------------------------------------------- // 
        /// <summary>
        /// Método que gera o lote para NF-e
        /// </summary>
        /// <param name="identLote">Classe VO do lote</param>
        /// <returns>codLote</returns>
        public int GerarLote(LoteVO identLote)
        {
            return new NfeData().GerarLote(identLote);
        }
        // ------------------------------------------------------------------------- //
        /// <summary>
        /// Método que grava o recibo para NF-e
        /// </summary>
        /// <param name="identNFe">Classe VO do NFeVO </param>
        /// <returns></returns>
        public void GravarReciboNFe(NfeVO identNFe)
        {
            new NfeData().GravarReciboNFe(identNFe);
        }
        
        // ------------------------------------------------------------------------- //
         /// <summary>
        /// Método que grava o status de envio da NFe
        /// </summary>
        /// <param name="identNFe"></param>
        // ------------------------------------------------------------------------- // 
        public void GravarStatusNFe(NfeVO identNFe)
        {
            new NfeData().GravarStatusNFe(identNFe);
        }
    }
}
