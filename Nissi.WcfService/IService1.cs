using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Nissi.Model;

namespace Nissi.WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        /// <summary>
        /// Método para executar a proc pr_selecionar_notafiscal 
        /// </summary>
        [OperationContract]
        List<NotaFiscalVO> ListarNotaFiscal(NotaFiscalVO identNotaFiscal);

        /// <summary>
        /// Método para executar a proc pr_selecionar_parametronfe 
        /// </summary>
        [OperationContract]
        List<ParametroVO> ListarParametro();

        // ------------------------------------------------------------------------- // 
        /// <summary>
        /// Método para executar a proc pr_incluir_parametronfe 
        /// </summary>
        [OperationContract]
        void IncluirParametro(ParametroVO identParametro);
        // ------------------------------------------------------------------------- // 
        /// <summary>
        /// Método para executar a proc pr_alterar_parametronfe 
        /// </summary>
        [OperationContract]
        void AlterarParametro(ParametroVO identParametro);

        // ------------------------------------------------------------------------- // 

        /// <summary>
        /// Método para executar a proc pr_excluir_parametronfe 
        /// </summary>
        [OperationContract]
        void ExcluirParametro();
        // ------------------------------------------------------------------------- // 

        /// <summary>
        /// Método para executar a proc pr_selecionar_nfe 
        /// </summary>
        [OperationContract]
        List<NfeVO> ListarNfe(int? codNF);


        // ------------------------------------------------------------------------- // 
        /// <summary>
        /// Método para executar a proc pr_incluir_nfe 
        /// </summary>
        [OperationContract]
        void IncluirNfe(NfeVO identNFe, int? codUsuarioInc);


        // ------------------------------------------------------------------------- // 
        /// <summary>
        /// Método para executar a proc pr_alterar_nfe 
        /// </summary>
        [OperationContract]
        void AlterarNfe(NfeVO identNfe, int? codUsuarioAlt);


        // ------------------------------------------------------------------------- // 
        /// <summary>
        /// Método para executar a proc pr_excluir_nfe 
        /// </summary>
        [OperationContract]
        void ExcluirNfe(int? codNF);

        // ------------------------------------------------------------------------- // 
        
        /// <summary>
        /// Método para gerar o número de lote
        /// </summary>
        /// <param name="identLote">Classe VO do LoteVO</param>
        /// <returns>codLote</returns>
        [OperationContract]
        int GerarLote(LoteVO identLote);

        // ------------------------------------------------------------------------- // 
        /// <summary>
        /// Método que grava o número do recibo NFe
        /// </summary>
        /// <param name="identNFe"></param>
        [OperationContract]
        void GravarReciboNFe(NfeVO identNFe);

        // ------------------------------------------------------------------------- // 
         /// <summary>
        /// Método que grava o status de envio da NFe
        /// </summary>
        /// <param name="identNFe"></param>
        // ------------------------------------------------------------------------- //
        [OperationContract]
        void GravarStatusNFe(NfeVO identNFe);
        
    }

}
