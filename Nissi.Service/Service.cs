using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Nissi.Business;
using Nissi.Model;
using System.ComponentModel;
using System.ServiceProcess;
using System.Configuration;
using System.Configuration.Install;



namespace Nissi.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service : IService
    {
        /// <summary>
        /// Método para listar os registros da tabela  NotaFiscal 
        /// </summary>
        public List<Model.NotaFiscalVO> ListarNotaFiscal(NotaFiscalVO identNotaFiscal)
        {
            return new NotaFiscal().ListarTudo(identNotaFiscal);
        }
        /// <summary>
        /// Método para executar a proc pr_selecionar_parametronfe 
        /// </summary>
        public List<ParametroVO> ListarParametro()
        {
            return new Parametro().Listar();
        }

        // ------------------------------------------------------------------------- // 
        /// <summary>
        /// Método para executar a proc pr_incluir_parametronfe 
        /// </summary>
        public void IncluirParametro(ParametroVO identParametro)
        {
            new Parametro().Incluir(identParametro);
        }
        // ------------------------------------------------------------------------- // 
        /// <summary>
        /// Método para executar a proc pr_alterar_parametronfe 
        /// </summary>
        public void AlterarParametro(ParametroVO identParametro)
        {
            new Parametro().Alterar(identParametro);
        }

        // ------------------------------------------------------------------------- // 

        /// <summary>
        /// Método para executar a proc pr_excluir_parametronfe 
        /// </summary>
        public void ExcluirParametro()
        {
            new Parametro().Excluir();
        }
        // ------------------------------------------------------------------------- //

        /// <summary>
        /// Método para executar a proc pr_selecionar_nfe 
        /// </summary>
        public List<NfeVO> ListarNfe(int? codNF)
        {
            return new Nfe().Listar(codNF);
        }


        // ------------------------------------------------------------------------- // 
        /// <summary>
        /// Método para executar a proc pr_incluir_nfe 
        /// </summary>
        public void IncluirNfe(NfeVO identNFe, int? codUsuarioInc)
        {
            new Nfe().Incluir(identNFe, codUsuarioInc);
        }


        // ------------------------------------------------------------------------- // 
        /// <summary>
        /// Método para executar a proc pr_alterar_nfe 
        /// </summary>
        public void AlterarNfe(NfeVO identNfe, int? codUsuarioAlt)
        {
            new Nfe().Alterar(identNfe, codUsuarioAlt);
        }


        // ------------------------------------------------------------------------- // 
        /// <summary>
        /// Método para executar a proc pr_excluir_nfe 
        /// </summary>
        public void ExcluirNfe(int? codNF)
        {
            new Nfe().Excluir(codNF);
        }
        /// <summary>
        /// Método que gera o lote para NF-e
        /// </summary>
        /// <param name="identLote">Classe VO do lote</param>
        /// <returns>codLote</returns>
        // ------------------------------------------------------------------------- // 
        public int GerarLote(LoteVO identLote)
        {
            return new Nfe().GerarLote(identLote);
        }
        /// <summary>
        /// Método que grava o número do recibo NFe
        /// </summary>
        /// <param name="identNFe"></param>
        // ------------------------------------------------------------------------- // 
        public void GravarReciboNFe(NfeVO identNFe)
        {
            new Nfe().GravarReciboNFe(identNFe);
        }

        /// <summary>
        /// Método que grava o status de envio da NFe
        /// </summary>
        /// <param name="identNFe"></param>
        // ------------------------------------------------------------------------- // 
        public void GravarStatusNFe(NfeVO identNFe)
        {
            new Nfe().GravarStatusNFe(identNFe);
        }

    }
}
