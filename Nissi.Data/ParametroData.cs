using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.Model;
using System.Data;

namespace Nissi.DataAccess
{
   public class ParametroData : NissiBaseData
    {
        /// <summary>
        /// Método para executar a proc pr_selecionar_parametronfe 
        /// </summary>
        public List<ParametroVO> Listar()
        {
            OpenCommand("pr_selecionar_parametronfe");
            try
            {
                List<ParametroVO> lstParametronfeVO = new List<ParametroVO>();

                IDataReader dr = ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        ParametroVO parametronfeVO = new ParametroVO();

                        parametronfeVO.Modelo = GetReaderValue<string>(dr, "Modelo");
                        parametronfeVO.Serie = GetReaderValue<string>(dr, "Serie");
                        parametronfeVO.DataPacket = GetReaderValue<string>(dr, "DataPacket");
                        parametronfeVO.Schemas = GetReaderValue<string>(dr, "Schemas");
                        parametronfeVO.DanfeLogo = GetReaderValue<string>(dr, "DanfeLogo");
                        parametronfeVO.Ambiente = GetReaderValue<string>(dr, "Ambiente");
                        parametronfeVO.NoSerieCertificado = GetReaderValue<string>(dr, "NoSerieCertificado");
                        parametronfeVO.AtivaTrace = GetReaderValue<string>(dr, "AtivaTrace");
                        parametronfeVO.VerProc = GetReaderValue<string>(dr, "VerProc");
                        parametronfeVO.NFeRecepcao = GetReaderValue<string>(dr, "NFeRecepcao");
                        parametronfeVO.NFeRetRecepcao = GetReaderValue<string>(dr, "NFeRetRecepcao");
                        parametronfeVO.NFeCancelamento = GetReaderValue<string>(dr, "NFeCancelamento");
                        parametronfeVO.NFeInutilizacao = GetReaderValue<string>(dr, "NFeInutilizacao");
                        parametronfeVO.NFeConsultaProtocolo = GetReaderValue<string>(dr, "NFeConsultaProtocolo");
                        parametronfeVO.NFeStatusServico = GetReaderValue<string>(dr, "NFeStatusServico");
                        parametronfeVO.DanfeInfo = GetReaderValue<string>(dr, "DanfeInfo");
                        parametronfeVO.PathPrincipal = GetReaderValue<string>(dr, "PathPrincipal");
                        parametronfeVO.TipoDanfe = GetReaderValue<string>(dr, "TipoDanfe");
                        parametronfeVO.TotalizarCfop = GetReaderValue<string>(dr, "TotalizarCfop");
                        parametronfeVO.DataPacketFormSeg = GetReaderValue<string>(dr, "DataPacketFormSeg");
                        parametronfeVO.CNPJ = GetReaderValue<string>(dr, "CNPJ");
                        parametronfeVO.UnidadeFederada = GetReaderValue<string>(dr, "UF");

                        lstParametronfeVO.Add(parametronfeVO);
                    }
                }
                finally
                {
                    dr.Close();
                }

                return lstParametronfeVO;
            }
            finally
            {
                CloseCommand();
            }
        }


        // ------------------------------------------------------------------------- // 
        /// <summary>
        /// Método para executar a proc pr_incluir_parametronfe 
        /// </summary>
        public int Incluir(ParametroVO identParametro)
        {
            OpenCommand("pr_incluir_parametronfe",true);
            try
            {
                // Parâmetros de entrada
                AddInParameter("@Modelo", DbType.AnsiStringFixedLength, identParametro.Modelo);
                AddInParameter("@Serie", DbType.AnsiStringFixedLength, identParametro.Serie);
                AddInParameter("@DataPacket", DbType.String, identParametro.DataPacket);
                AddInParameter("@Schemas", DbType.String, identParametro.Schemas);
                AddInParameter("@DanfeLogo", DbType.String, identParametro.DanfeLogo);
                AddInParameter("@Ambiente", DbType.AnsiStringFixedLength, identParametro.Ambiente);
                AddInParameter("@NoSerieCertificado", DbType.String, identParametro.NoSerieCertificado);
                AddInParameter("@AtivaTrace", DbType.AnsiStringFixedLength, identParametro.AtivaTrace);
                AddInParameter("@VerProc", DbType.AnsiStringFixedLength, identParametro.VerProc);
                AddInParameter("@NFeRecepcao", DbType.String, identParametro.NFeRecepcao);
                AddInParameter("@NFeRetRecepcao", DbType.String, identParametro.NFeRetRecepcao);
                AddInParameter("@NFeCancelamento", DbType.String, identParametro.NFeCancelamento);
                AddInParameter("@NFeInutilizacao", DbType.String, identParametro.NFeInutilizacao);
                AddInParameter("@NFeConsultaProtocolo", DbType.String, identParametro.NFeConsultaProtocolo);
                AddInParameter("@NFeStatusServico", DbType.String, identParametro.NFeStatusServico);
                AddInParameter("@DanfeInfo", DbType.String, identParametro.DanfeInfo);
                AddInParameter("@PathPrincipal", DbType.String, identParametro.PathPrincipal);
                AddInParameter("@TipoDanfe", DbType.AnsiStringFixedLength, identParametro.TipoDanfe);
                AddInParameter("@TotalizarCfop", DbType.AnsiStringFixedLength, identParametro.TotalizarCfop);
                AddInParameter("@DataPacketFormSeg", DbType.String, identParametro.DataPacketFormSeg);

                ExecuteNonQuery();
                int retorno = GetReturnValue();
                return retorno;
            }
            finally
            {
                CloseCommand();
            }
        }


        // ------------------------------------------------------------------------- // 
        /// <summary>
        /// Método para executar a proc pr_alterar_parametronfe 
        /// </summary>
        public void Alterar(ParametroVO identParametro)
        {
            OpenCommand("pr_alterar_parametronfe");
            try
            {
                // Parâmetros de entrada
                AddInParameter("@Modelo", DbType.AnsiStringFixedLength, identParametro.Modelo);
                AddInParameter("@Serie", DbType.AnsiStringFixedLength, identParametro.Serie);
                AddInParameter("@DataPacket", DbType.String, identParametro.DataPacket);
                AddInParameter("@Schemas", DbType.String, identParametro.Schemas);
                AddInParameter("@DanfeLogo", DbType.String, identParametro.DanfeLogo);
                AddInParameter("@Ambiente", DbType.AnsiStringFixedLength, identParametro.Ambiente);
                AddInParameter("@NoSerieCertificado", DbType.String, identParametro.NoSerieCertificado);
                AddInParameter("@AtivaTrace", DbType.AnsiStringFixedLength, identParametro.AtivaTrace);
                AddInParameter("@VerProc", DbType.AnsiStringFixedLength, identParametro.VerProc);
                AddInParameter("@NFeRecepcao", DbType.String, identParametro.NFeRecepcao);
                AddInParameter("@NFeRetRecepcao", DbType.String, identParametro.NFeRetRecepcao);
                AddInParameter("@NFeCancelamento", DbType.String, identParametro.NFeCancelamento);
                AddInParameter("@NFeInutilizacao", DbType.String, identParametro.NFeInutilizacao);
                AddInParameter("@NFeConsultaProtocolo", DbType.String, identParametro.NFeConsultaProtocolo);
                AddInParameter("@NFeStatusServico", DbType.String, identParametro.NFeStatusServico);
                AddInParameter("@DanfeInfo", DbType.String, identParametro.DanfeInfo);
                AddInParameter("@PathPrincipal", DbType.String, identParametro.PathPrincipal);
                AddInParameter("@TipoDanfe", DbType.AnsiStringFixedLength, identParametro.TipoDanfe);
                AddInParameter("@TotalizarCfop", DbType.AnsiStringFixedLength, identParametro.TotalizarCfop);
                AddInParameter("@DataPacketFormSeg", DbType.String, identParametro.DataPacketFormSeg);

                ExecuteNonQuery();
            }
            finally
            {
                CloseCommand();
            }
        }


        // ------------------------------------------------------------------------- // 

        /// <summary>
        /// Método para executar a proc pr_excluir_parametronfe 
        /// </summary>
        public void Excluir()
        {
            OpenCommand("pr_excluir_parametronfe");
            try
            {
                ExecuteNonQuery();
            }
            finally
            {
                CloseCommand();
            }
        }


        // ------------------------------------------------------------------------- // 


    }
}
