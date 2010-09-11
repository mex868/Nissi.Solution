using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.Model;
using System.Data;
using Nissi.Util;

namespace Nissi.DataAccess
{
    public class NfeData : NissiBaseData
    {
        /// <summary>
        /// Método para executar a proc pr_selecionar_nfe 
        /// </summary>
        public List<NfeVO> Listar(int? codNF)
        {
            OpenCommand("pr_selecionar_nfe");
            try
            {
                // Parâmetros de entrada
                AddInParameter("@CodNF", DbType.Int32, codNF);

                List<NfeVO> lstNfeVO = new List<NfeVO>();

                IDataReader dr = ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        NfeVO nfeVO = new NfeVO();

                        nfeVO.CodNFe = GetReaderValue<int?>(dr, "CodNFe");
                        nfeVO.CodNF = GetReaderValue<int?>(dr, "CodNF");
                        nfeVO.CodNumLote = GetReaderValue<int?>(dr, "CodNumLote");
                        nfeVO.ChaveNFE = GetReaderValue<string>(dr, "ChaveNFE");
                        nfeVO.NumProtocolo = GetReaderValue<string>(dr, "NumProtocolo");
                        nfeVO.NumRecibo = GetReaderValue<string>(dr, "NumRecibo");
                        nfeVO.IndAmbiente = GetReaderValue<bool?>(dr, "IndAmbiente");
                        nfeVO.IndImpressao = GetReaderValue<bool?>(dr, "IndImpressao");
                        nfeVO.DataCadastro = GetReaderValue<DateTime?>(dr, "DataCadastro");
                        nfeVO.UsuarioInc = GetReaderValue<int?>(dr, "UsuarioInc");
                        nfeVO.DataAlteracao = GetReaderValue<DateTime?>(dr, "DataAlteracao");
                        nfeVO.UsuarioAlt = GetReaderValue<int?>(dr, "UsuarioAlt");
                        nfeVO.IndTipoEmissao = GetReaderValue<string>(dr, "IndTipoEmissao");
                        nfeVO.CRT = GetReaderValue<string>(dr, "CRT");
                        nfeVO.IndStatus = GetReaderValue<string>(dr,"IndStatus");
                        lstNfeVO.Add(nfeVO);
                    }
                }
                finally
                {
                    dr.Close();
                }

                return lstNfeVO;
            }
            finally
            {
                CloseCommand();
            }
        }


        // ------------------------------------------------------------------------- // 
        /// <summary>
        /// Método para executar a proc pr_incluir_nfe 
        /// </summary>
        public void Incluir(NfeVO identNfe, int? codUsuarioInc)
        {
            OpenCommand("pr_incluir_nfe");
            try
            {
                // Parâmetros de entrada
                AddInParameter("@CodNF", DbType.Int32, identNfe.CodNF);
                AddInParameter("@CodNumLote", DbType.Int32, identNfe.CodNumLote);
                AddInParameter("@ChaveNFE", DbType.AnsiString, identNfe.ChaveNFE);
                AddInParameter("@NumProtocolo", DbType.Int32, identNfe.NumProtocolo);
                AddInParameter("@NumRecibo", DbType.String, identNfe.NumRecibo);
                AddInParameter("@IndAmbiente", DbType.Boolean, identNfe.IndAmbiente);
                AddInParameter("@IndImpressao", DbType.Boolean, identNfe.IndImpressao);
                AddInParameter("@IndTipoEmissao", DbType.String, identNfe.IndImpressao);
                AddInParameter("@CRT", DbType.String, identNfe.IndImpressao);
                AddInParameter("@IndStatus", DbType.String, identNfe.IndStatus);
                AddInParameter("@UsuarioInc", DbType.Int32, codUsuarioInc);

                ExecuteNonQuery();
            }
            finally
            {
                CloseCommand();
            }
        }


        // ------------------------------------------------------------------------- // 
        /// <summary>
        /// Método para executar a proc pr_alterar_nfe 
        /// </summary>
        public void Alterar(NfeVO identNfe, int? codUsuarioAlt)
        {
            OpenCommand("pr_alterar_nfe");
            try
            {
                // Parâmetros de entrada
                AddInParameter("@CodNF", DbType.Int32, identNfe.CodNF);
                AddInParameter("@CodNumLote", DbType.Int32, identNfe.CodNumLote);
                AddInParameter("@ChaveNFE", DbType.AnsiString, identNfe.ChaveNFE);
                AddInParameter("@NumProtocolo", DbType.Int32, identNfe.NumProtocolo);
                AddInParameter("@NumRecibo", DbType.String, identNfe.NumRecibo);
                AddInParameter("@IndAmbiente", DbType.Boolean, identNfe.IndAmbiente);
                AddInParameter("@IndImpressao", DbType.Boolean, identNfe.IndImpressao);
                AddInParameter("@IndTipoEmissao", DbType.String, identNfe.IndImpressao);
                AddInParameter("@CRT", DbType.String, identNfe.IndImpressao);
                AddInParameter("@IndStatus", DbType.String, identNfe.IndStatus);
                AddInParameter("@UsuarioAlt", DbType.Int32, codUsuarioAlt);

                ExecuteNonQuery();
            }
            finally
            {
                CloseCommand();
            }
        }


        // ------------------------------------------------------------------------- // 
        /// <summary>
        /// Método para executar a proc pr_excluir_nfe 
        /// </summary>
        public void Excluir(int? codNF)
        {
            OpenCommand("pr_excluir_nfe");
            try
            {
                // Parâmetros de entrada
                AddInParameter("@CodNF", DbType.Int32, codNF);

                ExecuteNonQuery();
            }
            finally
            {
                CloseCommand();
            }
        }
        // ------------------------------------------------------------------------- //
        /// <summary>
        /// Método que gera o lote para NF-e
        /// </summary>
        /// <param name="identLote">Classe VO do lote</param>
        /// <returns>codLote</returns>
        public int GerarLote(LoteVO identLote)
        {
            OpenCommand("pr_gerar_lote", true);
            try
            {
                int codLote = int.MinValue;
                AddInParameter("@UsuarioInc",DbType.Int32, identLote.CodUsuario);
                AddInParameter("@xmlNfe", DbType.String, identLote.ToXml());
                ExecuteNonQuery();
                codLote = GetReturnValue();
                return codLote;
            }
            finally
            {
                CloseCommand();
            }
        }
        // ------------------------------------------------------------------------- //
        /// <summary>
        /// Método que grava o recibo para NF-e
        /// </summary>
        /// <param name="identNFe">Classe VO do NFeVO </param>
        /// <returns></returns>
        public void GravarReciboNFe(NfeVO identNFe)
        {
            OpenCommand("pr_gravar_recibonfe");
            try
            {
                AddInParameter("@CodNumLote", DbType.Int32, identNFe.CodNumLote);
                AddInParameter("@NumRecibo", DbType.String, identNFe.NumRecibo);
                ExecuteNonQuery();
            }
            finally
            {
                CloseCommand();
            }
        }
        /// <summary>
        /// Método que grava o status para NF-e
        /// </summary>
        /// <param name="identNFe">Classe VO do NFeVO </param>
        /// <returns></returns>
        public void GravarStatusNFe(NfeVO identNFe)
        {
            OpenCommand("pr_gravar_statusnfe");
            try
            {
                AddInParameter("@ChaveNFe", DbType.String, identNFe.ChaveNFE);
                AddInParameter("@IndStatus", DbType.String, identNFe.IndStatus);
                AddInParameter("@NumProtocolo", DbType.String, identNFe.NumProtocolo);
                ExecuteNonQuery();
            }
            finally
            {
                CloseCommand();
            }
        }
    }

}
