using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Nissi.Model;
using Nissi.Util;

namespace Nissi.DataAccess
{
    public class NotaFiscalData : NissiBaseData
    {
        /// <summary>
        /// Método para executar a proc pr_selecionar_nf 
        /// </summary>
        public List<NotaFiscalVO> Listar(NotaFiscalVO identNotaFiscal)
        {
            OpenCommand("pr_selecionar_nf");
            try
            {
                // Parâmetros de entrada
                AddInParameter("@CodNF", DbType.Int32,identNotaFiscal.CodNF);
                AddInParameter("@NF", DbType.Int32, identNotaFiscal.NF);
                AddInParameter("@DataEmissao", DbType.DateTime, identNotaFiscal.DataEmissao);
                AddInParameter("@RazaoSocial", DbType.String, identNotaFiscal.Cliente.RazaoSocial);
                if (!string.IsNullOrEmpty(identNotaFiscal.Cliente.CodRef))
                    AddInParameter("@CodRef", DbType.String, identNotaFiscal.Cliente.CodRef);
                AddInParameter("@CodCliente", DbType.Int32, identNotaFiscal.Cliente.CodPessoa);

                List<NotaFiscalVO> lstNotaFiscalVO = new List<NotaFiscalVO>();

                IDataReader dr = ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        NotaFiscalVO notaFiscalVO = new NotaFiscalVO();
                        notaFiscalVO.CodNF = GetReaderValue<int?>(dr, "CodNF");
                        notaFiscalVO.NF = GetReaderValue<int?>(dr, "NF");
                        notaFiscalVO.Serie = GetReaderValue<string>(dr, "Serie");
                        notaFiscalVO.Cliente.RazaoSocial = GetReaderValue<string>(dr, "RazaoSocial");
                        notaFiscalVO.Cliente.NomeFantasia = GetReaderValue<string>(dr, "NomeFantasia");
                        notaFiscalVO.DataEmissao = GetReaderValue<DateTime?>(dr, "DataEmissao");
                        ListarNFe(ref notaFiscalVO);
                        lstNotaFiscalVO.Add(notaFiscalVO);
                    }
                }
                finally
                {
                    dr.Close();
                }

                return lstNotaFiscalVO;
            }
            finally
            {
                CloseCommand();
            }
        }

        public List<NotaFiscalVO> ListarTudo(NotaFiscalVO identNotaFiscal)
        {
            OpenCommand("pr_selecionar_nf");
            try
            {

                // Parâmetros de entrada
                AddInParameter("@CodNF", DbType.Int32, identNotaFiscal.CodNF);
                AddInParameter("@NF", DbType.Int32, identNotaFiscal.NF);
                AddInParameter("@DataEmissao", DbType.DateTime, identNotaFiscal.DataEmissao);
                if (!string.IsNullOrEmpty(identNotaFiscal.Cliente.RazaoSocial))
                    AddInParameter("@RazaoSocial", DbType.String, identNotaFiscal.Cliente.RazaoSocial);
                if (!string.IsNullOrEmpty(identNotaFiscal.NFe.IndStatus))
                    AddInParameter("@IndStatus", DbType.String, identNotaFiscal.NFe.IndStatus);

                List<NotaFiscalVO> lstNotaFiscalVO = new List<NotaFiscalVO>();
                IDataReader dr = ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        NotaFiscalVO notaFiscalVO = new NotaFiscalVO();
                        
                        notaFiscalVO.CodNF = GetReaderValue<int?>(dr, "CodNF");
                        notaFiscalVO.NF = GetReaderValue<int?>(dr, "NF");
                        notaFiscalVO.Emitente.CodEmitente = GetReaderValue<int?>(dr, "CodEmitente");
                        notaFiscalVO.Cliente.CodPessoa = GetReaderValue<int?>(dr, "CodCliente");
                        notaFiscalVO.Funcionario.CodFuncionario = GetReaderValue<int?>(dr, "CodVendedor");
                        notaFiscalVO.CodBanco = GetReaderValue<int?>(dr, "CodBanco");
                        notaFiscalVO.Transportadora.CodTransportadora = GetReaderValue<int?>(dr, "CodTransportadora");
                        notaFiscalVO.CFOP.CodCFOP = GetReaderValue<int?>(dr, "CodCFOP");
                        notaFiscalVO.CodPed = GetReaderValue<int?>(dr, "CodPed");
                        notaFiscalVO.MensagemNF.CodMensagemNF = GetReaderValue<int?>(dr, "CodMensagemNF");
                        notaFiscalVO.DataEmissao = GetReaderValue<DateTime?>(dr, "DataEmissao");
                        notaFiscalVO.DataEntradaSaida = GetReaderValue<DateTime?>(dr, "DataEntradaSaida");
                        notaFiscalVO.NumeroPedido = GetReaderValue<string>(dr, "NumeroPedido");
                        notaFiscalVO.Hora = GetReaderValue<DateTime?>(dr, "Hora");
                        notaFiscalVO.IndEntradaSaida = GetReaderValue<bool?>(dr, "IndEntradaSaida");
                        notaFiscalVO.IESUBTRI = GetReaderValue<string>(dr, "IESUBTRI");
                        notaFiscalVO.BASCALICMSSUB = GetReaderValue<decimal?>(dr, "BASCALICMSSUB");
                        notaFiscalVO.IndBaixa = GetReaderValue<bool?>(dr, "IndBaixa");
                        notaFiscalVO.ValorFrete = GetReaderValue<decimal?>(dr, "ValorFrete");
                        notaFiscalVO.ValorSeguro = GetReaderValue<decimal?>(dr, "ValorSeguro");
                        notaFiscalVO.OutDespAce = GetReaderValue<decimal?>(dr, "OUTDESACE");
                        notaFiscalVO.IndFretePorConta = GetReaderValue<bool?>(dr, "IndFretePorConta");
                        notaFiscalVO.PlacaVeiculo = GetReaderValue<string>(dr, "PlacaVeiculo");
                        notaFiscalVO.UF = GetReaderValue<string>(dr, "UF");
                        notaFiscalVO.Especie = GetReaderValue<string>(dr, "Especie");
                        notaFiscalVO.Marca = GetReaderValue<string>(dr, "Marca");
                        notaFiscalVO.Numero = GetReaderValue<string>(dr, "Numero");
                        notaFiscalVO.PesoBruto = GetReaderValue<decimal?>(dr, "PesoBruto");
                        notaFiscalVO.QtdVolumes = GetReaderValue<string>(dr, "QtdTotal");
                        notaFiscalVO.PesoLiquido = GetReaderValue<string>(dr, "PesoLiquido");
                        notaFiscalVO.SemPedido = GetReaderValue<string>(dr, "SemPedido");
                        notaFiscalVO.Observacao = GetReaderValue<string>(dr, "Observacao");
                        notaFiscalVO.Observacao2 = GetReaderValue<string>(dr, "Observacao2");
                        notaFiscalVO.indMovimento = GetReaderValue<bool?>(dr, "indMovimento");
                        notaFiscalVO.IndVendaBeneficiamento = GetReaderValue<bool?>(dr, "IndVendaBeneficiamento");
                        notaFiscalVO.UsuarioInc = GetReaderValue<int?>(dr, "UsuarioInc");
                        notaFiscalVO.DataCadastro = GetReaderValue<DateTime?>(dr, "Datacadastro");
                        notaFiscalVO.UsuarioAlt = GetReaderValue<int?>(dr, "UsuarioAlt");
                        notaFiscalVO.DataAlteracao = GetReaderValue<DateTime?>(dr, "DataAlteracao");
                        notaFiscalVO.IndFaturamento = GetReaderValue<bool?>(dr, "IndFaturamento");
                        notaFiscalVO.CodCFOP2 = GetReaderValue<int?>(dr, "CodCFOP2");
                        notaFiscalVO.IndVendaFaturamento = GetReaderValue<bool?>(dr, "IndVendaFaturamento");
                        notaFiscalVO.Vendedor = GetReaderValue<string>(dr, "Vendedor");
                        notaFiscalVO.Serie = GetReaderValue<string>(dr, "Serie");
                        notaFiscalVO.IndFinalidadeNF = GetReaderValue<string>(dr, "IndFinalidadeNF");
                        notaFiscalVO.RefNFe = GetReaderValue<string>(dr, "RefNFe");
                        ListarEmitente(ref notaFiscalVO);
                        ListarCliente(ref notaFiscalVO);
                        ListarTransportadora(ref notaFiscalVO);
                        ListarCFOP(ref notaFiscalVO);
                        ListarMensagemNF(ref notaFiscalVO);
                        ListarNFe(ref notaFiscalVO);
                        notaFiscalVO.NFe.CRT = "1";
                        notaFiscalVO.NFe.IndTipoEmissao = "1";
                        ListarDuplicata(ref notaFiscalVO, notaFiscalVO.CodNF);
                        ListarItens(ref notaFiscalVO, notaFiscalVO.CodNF);
                        
                        lstNotaFiscalVO.Add(notaFiscalVO);
                    }
                }
                finally
                {
                    dr.Close();
                }
                return lstNotaFiscalVO;
            }
            finally
            {
                CloseCommand();
            }
        }
        /// <summary>
        /// Método que lista o Emitente da Nota Fiscal 
        /// </summary>
        /// <param name="identNotaFiscalVO"></param>
        private void ListarEmitente(ref NotaFiscalVO identNotaFiscal)
        {
            List<EmitenteVO> lstEmitente = new EmitenteData().Lista(identNotaFiscal.Emitente);
             foreach (EmitenteVO tempEmitente in lstEmitente)
             {
                 identNotaFiscal.Emitente = tempEmitente;
             }
        }
        ///<summary>
        ///Método para Listar a Cliente da Nota Fiscal
        ///</summary>
        /// <returns></returns>        
        private void ListarCliente(ref NotaFiscalVO identNotaFiscal)
        {
            List<ClienteVO> lstCliente = new ClienteData().Listar(identNotaFiscal.Cliente);
            foreach (ClienteVO tempCliente in lstCliente)
            {
                identNotaFiscal.Cliente = tempCliente;
            }
        }
        ///<summary>
        ///Método para Listar a Transportadora da Nota Fiscal
        ///</summary>
        /// <returns></returns>
        private void ListarTransportadora(ref NotaFiscalVO identNotaFiscal)
        {
            if (identNotaFiscal.Transportadora.CodTransportadora != null)
            {
                List<TransportadoraVO> lstTransportadora = new TransportadoraData().Listar(identNotaFiscal.Transportadora);
                foreach (TransportadoraVO tempTransportadora in lstTransportadora)
                {
                    identNotaFiscal.Transportadora = tempTransportadora;
                }
            }
        }
        /// <summary>
        /// Método que lista o CFOP
        /// </summary>
        /// <param name="identNotaFiscal"></param>
        private void ListarCFOP(ref NotaFiscalVO identNotaFiscal)
        {
            List<CFOPVO> lstCFOP = new CFOPData().Listar(identNotaFiscal.CFOP);
            foreach (CFOPVO tempCFOP in lstCFOP)
            {
               identNotaFiscal.CFOP = tempCFOP;
            }
        }
        /// <summary>
        /// Método que lista a Mensagem da N.F.
        /// </summary>
        /// <param name="identNotaFiscal"></param>
        private void ListarMensagemNF(ref NotaFiscalVO identNotaFiscal)
        {
            if (identNotaFiscal.MensagemNF.CodMensagemNF != null)
            {
                List<MensagemNFVO> lstMensagemNF = new MensagemNFData().Listar(identNotaFiscal.MensagemNF);
                foreach (MensagemNFVO tempMensagemNF in lstMensagemNF)
                {
                    identNotaFiscal.MensagemNF = tempMensagemNF;
                }
            }
        }
        /// <summary>
        /// Método que lista a NFe enviadas
        /// </summary>
        /// <param name="identNotaFiscal"></param>
        private void ListarNFe(ref NotaFiscalVO identNotaFiscal)
        {
            List<NfeVO> lstNfe = new NfeData().Listar(identNotaFiscal.CodNF);
            foreach(NfeVO tempNFe in lstNfe)
            {
                identNotaFiscal.NFe = tempNFe;
            }
        }
        ///<summary>
        ///Método para Listar as Duplicatas da Nota Fiscal
        ///</summary>
        /// <returns></returns>
        private void ListarDuplicata(ref NotaFiscalVO identNotaFiscal, int? codNF)
        {
            OpenCommand("pr_selecionar_duplicata");
            if (codNF > 0)
            {
                AddInParameter("CodNF", DbType.Int32, codNF);
            }

            IDataReader dr = ExecuteReader();
            try
            {
                while (dr.Read())
                {
                    DuplicataVO tempDuplicata = new DuplicataVO();
                    tempDuplicata.CodDuplicata = GetReaderValue<int?>(dr, "CodDuplicata");
                    tempDuplicata.Dias = GetReaderValue<int?>(dr, "Dias");
                    tempDuplicata.Vencimento = GetReaderValue<DateTime?>(dr, "Vencimento");
                    tempDuplicata.Numero = GetReaderValue<string>(dr, "Numero");
                    tempDuplicata.Valor = GetReaderValue<decimal?>(dr,"Valor");
                    identNotaFiscal.Duplicatas.Add(tempDuplicata);
                }
            }
            finally
            {
                dr.Close();
            }
        }
     
        /// <summary>
	    /// Método para executar a proc pr_selecionar_itemnf 
	    /// </summary>
        private void ListarItens(ref NotaFiscalVO identNotaFiscal, int? codNF)
        {
    		OpenCommand("pr_selecionar_itemnf");
	    	try
		    {
                // Parâmetros de entrada
			    if (codNF > 0)
    			    AddInParameter("@CodNF", DbType.Int32, codNF);
			    IDataReader dr = ExecuteReader();            
			try
			{
				while (dr.Read())
				{					
					ItemNotaFiscalVO tempItemNotaFiscal = new ItemNotaFiscalVO();
					
					tempItemNotaFiscal.CodItemNotaFiscal = GetReaderValue<int?>(dr, "CodItemNotaFiscal");
					tempItemNotaFiscal.CodNF = GetReaderValue<int?>(dr, "CodNF");
					tempItemNotaFiscal.Produto.CodProduto = GetReaderValue<int?>(dr, "CodProduto");
                    tempItemNotaFiscal.Produto.Codigo = GetReaderValue<string>(dr, "Codigo");
                    tempItemNotaFiscal.Codigo = GetReaderValue<string>(dr, "Codigo");
                    tempItemNotaFiscal.Produto.Descricao = GetReaderValue<string>(dr, "Descricao");
                    tempItemNotaFiscal.Produto.Unidade.TipoUnidade = GetReaderValue<string>(dr, "TipoUnidade");
                    tempItemNotaFiscal.CodPedido = GetReaderValue<int?>(dr, "CodPedido");
					tempItemNotaFiscal.Qtd = GetReaderValue<decimal?>(dr, "Qtd");
					tempItemNotaFiscal.Valor = GetReaderValue<decimal?>(dr, "Valor");
					tempItemNotaFiscal.ICMS = GetReaderValue<decimal?>(dr, "ICMS");
					tempItemNotaFiscal.IPI = GetReaderValue<decimal?>(dr, "IPI");
					tempItemNotaFiscal.RED = GetReaderValue<decimal?>(dr, "RED");
					tempItemNotaFiscal.COM = GetReaderValue<decimal?>(dr, "COM");
					tempItemNotaFiscal.Desconto = GetReaderValue<decimal?>(dr, "Desconto");
					tempItemNotaFiscal.BaseICMS = GetReaderValue<decimal?>(dr, "BaseICMS");
					tempItemNotaFiscal.Observacao = GetReaderValue<string>(dr, "Observacao");
					tempItemNotaFiscal.CalcICMSSobIpi = GetReaderValue<bool?>(dr, "CalcICMSSobIpi");
					tempItemNotaFiscal.UsuarioInc = GetReaderValue<int?>(dr, "UsuarioInc");
					tempItemNotaFiscal.DataCadastro = GetReaderValue<DateTime?>(dr, "DataCadastro");
					tempItemNotaFiscal.UsuarioAlt = GetReaderValue<int?>(dr, "UsuarioAlt");
					tempItemNotaFiscal.DataAlteracao = GetReaderValue<DateTime?>(dr, "DataAlteracao");
					tempItemNotaFiscal.indMov = GetReaderValue<bool?>(dr, "indMov");
					tempItemNotaFiscal.Unidade = GetReaderValue<string>(dr, "Unidade");
					tempItemNotaFiscal.Icms.CodTipoTributacao = GetReaderValue<string>(dr, "CodTipoTributacao");
                    tempItemNotaFiscal.Icms.CodOrigem = GetReaderValue<int?>(dr, "CodOrigem");
                    tempItemNotaFiscal.Produto.NCM = GetReaderValue<string>(dr, "NCM");
                    tempItemNotaFiscal.CodPedidoCliente = GetReaderValue<string>(dr, "CodPedidoCliente");
				    tempItemNotaFiscal.OP = GetReaderValue<string>(dr, "OP");
                    tempItemNotaFiscal.Produto.ICMS.Add(tempItemNotaFiscal.Icms);
                    identNotaFiscal.Itens.Add(tempItemNotaFiscal);
				}
			}
			finally
			{
				dr.Close();
			} 
		}            
		finally
		{
			CloseCommand();
		}				
	}

        /// <summary>
        /// Método que gera o número da nota fiscal e pega a série
        /// </summary>
        /// <returns>NotaFiscalVO</returns>
        public NotaFiscalVO gerar_numero_nf()
        {
            OpenCommand("pr_gerar_numero_nf");
            NotaFiscalVO identNotaFiscal = new NotaFiscalVO();
            try
            {
                IDataReader dr = ExecuteReader();
                while (dr.Read())
                {
                    identNotaFiscal.NF = GetReaderValue<int>(dr, "NF");
                    identNotaFiscal.Serie = GetReaderValue<string>(dr, "SERIE");
                }
            }
            finally
            {
                CloseCommand();
            }
            return identNotaFiscal;
        }
        /// <summary>
        /// Metodo que lista o número da NF
        /// </summary>
        /// <param name="codNf">Código da Nota Fiscal</param>
        /// <returns></returns>
        public int ListarNumeroNf(int codNf)
        {
            OpenCommand("pr_selecionar_numero_nf");
            if (codNf > 0)
                AddInParameter("@CodNF", DbType.Int32, codNf);
            return ExecuteScalar<int>();
        }

        // ------------------------------------------------------------------------- // 
        // ------------------------------------------------------------------------- // 
        /// <summary>
        /// Método para incluir um registro na tabela  NotaFiscal 
        /// </summary>
        #region Métodos de Inclusão
        public int Incluir(NotaFiscalVO notaFiscalVO, int codUsuarioOperacao)
        {
            OpenCommand("pr_incluir_nf", true);
            try
            {
                // Parâmetros de entrada
                AddInParameter("@NF", DbType.Int32, notaFiscalVO.NF);
                AddInParameter("@CodEmitente", DbType.Int32, notaFiscalVO.Emitente.CodEmitente);
                AddInParameter("@CodCliente", DbType.Int32, notaFiscalVO.Cliente.CodPessoa);
                AddInParameter("@Tipo", DbType.String, "0");
                AddInParameter("@CodVendedor", DbType.Int32, notaFiscalVO.Funcionario.CodFuncionario);
                AddInParameter("@CodBanco", DbType.Int32, notaFiscalVO.CodBanco);
                if (notaFiscalVO.Transportadora != null)
                    AddInParameter("@CodTransportadora", DbType.Int32, notaFiscalVO.Transportadora.CodTransportadora);
                AddInParameter("@CodCFOP", DbType.Int32, notaFiscalVO.CFOP.CodCFOP);
                AddInParameter("@CodPed", DbType.Int32, notaFiscalVO.CodPed);
                if (notaFiscalVO.MensagemNF != null)
                    AddInParameter("@CodMensagemNF", DbType.Int32, notaFiscalVO.MensagemNF.CodMensagemNF);
                AddInParameter("@DataEmissao", DbType.DateTime, notaFiscalVO.DataEmissao);
                AddInParameter("@DataEntradaSaida", DbType.DateTime, notaFiscalVO.DataEntradaSaida);
                AddInParameter("@NumeroPedido", DbType.String, notaFiscalVO.NumeroPedido);
                AddInParameter("@Hora", DbType.DateTime, notaFiscalVO.Hora);
                AddInParameter("@IndEntradaSaida", DbType.Boolean, notaFiscalVO.IndEntradaSaida);
                AddInParameter("@IESUBTRI", DbType.String, notaFiscalVO.IESUBTRI);
                AddInParameter("@BASCALICMSSUB", DbType.Decimal, notaFiscalVO.BASCALICMSSUB);
                AddInParameter("@IndBaixa", DbType.Boolean, notaFiscalVO.IndBaixa);
                AddInParameter("@ValorFrete", DbType.Currency, notaFiscalVO.ValorFrete);
                AddInParameter("@ValorSeguro", DbType.Currency, notaFiscalVO.ValorSeguro);
                AddInParameter("@OUTDESACE", DbType.Currency, notaFiscalVO.OutDespAce);
                AddInParameter("@IndFretePorConta", DbType.Boolean, notaFiscalVO.IndFretePorConta);
                AddInParameter("@PlacaVeiculo", DbType.String, notaFiscalVO.PlacaVeiculo);
                AddInParameter("@UF", DbType.AnsiStringFixedLength, notaFiscalVO.UF);
                AddInParameter("@Especie", DbType.String, notaFiscalVO.Especie);
                AddInParameter("@Marca", DbType.String, notaFiscalVO.Marca);
                AddInParameter("@Numero", DbType.String, notaFiscalVO.Numero);
                AddInParameter("@PesoBruto", DbType.Decimal, notaFiscalVO.PesoBruto);
                AddInParameter("@QtdTotal", DbType.String, notaFiscalVO.QtdVolumes);
                AddInParameter("@PesoLiquido", DbType.StringFixedLength, notaFiscalVO.PesoLiquido);
                AddInParameter("@SemPedido", DbType.String, notaFiscalVO.SemPedido);
                AddInParameter("@Observacao", DbType.String, notaFiscalVO.Observacao);
                AddInParameter("@Observacao2", DbType.String, notaFiscalVO.Observacao2);
                AddInParameter("@indMovimento", DbType.Boolean, notaFiscalVO.indMovimento);
                AddInParameter("@IndVendaBeneficiamento", DbType.Boolean, notaFiscalVO.IndVendaBeneficiamento);
                AddInParameter("@UsuarioInc", DbType.Int32, notaFiscalVO.UsuarioInc);
                AddInParameter("@IndFaturamento", DbType.Boolean, notaFiscalVO.IndFaturamento);
                AddInParameter("@CodCFOP2", DbType.Int32, notaFiscalVO.CodCFOP2);
                AddInParameter("@IndVendaFaturamento", DbType.Boolean, notaFiscalVO.IndVendaFaturamento);
                AddInParameter("@Vendedor", DbType.String, notaFiscalVO.Vendedor);
                //AddInParameter("@Status", DbType.Int16, 0);
                AddInParameter("@Serie", DbType.String, notaFiscalVO.Serie);
                AddInParameter("@IndFinalidadeNF", DbType.String, notaFiscalVO.IndFinalidadeNF);
                AddInParameter("@RefNFe", DbType.String, notaFiscalVO.RefNFe);
                AddInParameter("@XmlItem", DbType.String, notaFiscalVO.ToXml());
                ExecuteNonQuery();
                int retorno = GetReturnValue();
                return retorno;
            }
            finally
            {
                CloseCommand();
            }
        }
        #endregion
        // ------------------------------------------------------------------------- // 
        /// <summary>
        /// Método para alterar um registro na tabela  NotaFiscal 
        /// </summary>
        #region Métodos de Alteração
        public void Alterar(NotaFiscalVO notaFiscalVO, int codUsuarioOperacao)
        {
            OpenCommand("pr_alterar_nf");
            try
            {
                // Parâmetros de entrada
                AddInParameter("@CodNF", DbType.Int32, notaFiscalVO.CodNF);
                AddInParameter("@NF", DbType.Int32, notaFiscalVO.NF);
                AddInParameter("@CodEmitente", DbType.Int32, notaFiscalVO.Emitente.CodEmitente);
                AddInParameter("@CodCliente", DbType.Int32, notaFiscalVO.Cliente.CodPessoa);
                AddInParameter("@Tipo", DbType.String, "0");
                AddInParameter("@CodVendedor", DbType.Int32, notaFiscalVO.Funcionario.CodFuncionario);
                AddInParameter("@CodBanco", DbType.Int32, notaFiscalVO.CodBanco);
                AddInParameter("@CodTransportadora", DbType.Int32, notaFiscalVO.Transportadora.CodTransportadora);
                AddInParameter("@CodCFOP", DbType.Int32, notaFiscalVO.CFOP.CodCFOP);
                AddInParameter("@CodPed", DbType.Int32, notaFiscalVO.CodPed);
                AddInParameter("@CodMensagemNF", DbType.Int32, notaFiscalVO.MensagemNF.CodMensagemNF);
                AddInParameter("@DataEmissao", DbType.DateTime, notaFiscalVO.DataEmissao);
                AddInParameter("@DataEntradaSaida", DbType.DateTime, notaFiscalVO.DataEntradaSaida);
                AddInParameter("@NumeroPedido", DbType.String, notaFiscalVO.NumeroPedido);
                AddInParameter("@Hora", DbType.DateTime, notaFiscalVO.Hora);
                AddInParameter("@IndEntradaSaida", DbType.Boolean, notaFiscalVO.IndEntradaSaida);
                AddInParameter("@IESUBTRI", DbType.String, notaFiscalVO.IESUBTRI);
                AddInParameter("@BASCALICMSSUB", DbType.Decimal, notaFiscalVO.BASCALICMSSUB);
                AddInParameter("@IndBaixa", DbType.Boolean, notaFiscalVO.IndBaixa);
                AddInParameter("@ValorFrete", DbType.Currency, notaFiscalVO.ValorFrete);
                AddInParameter("@ValorSeguro", DbType.Currency, notaFiscalVO.ValorSeguro);
                AddInParameter("@OUTDESACE", DbType.Currency, notaFiscalVO.OutDespAce);
                AddInParameter("@IndFretePorConta", DbType.Boolean, notaFiscalVO.IndFretePorConta);
                AddInParameter("@PlacaVeiculo", DbType.String, notaFiscalVO.PlacaVeiculo);
                AddInParameter("@UF", DbType.AnsiStringFixedLength, notaFiscalVO.UF);
                AddInParameter("@Especie", DbType.String, notaFiscalVO.Especie);
                AddInParameter("@Marca", DbType.String, notaFiscalVO.Marca);
                AddInParameter("@Numero", DbType.String, notaFiscalVO.Numero);
                AddInParameter("@PesoBruto", DbType.Decimal, notaFiscalVO.PesoBruto);
                AddInParameter("@QtdTotal", DbType.String, notaFiscalVO.QtdVolumes);
                AddInParameter("@PesoLiquido", DbType.StringFixedLength, notaFiscalVO.PesoLiquido);
                AddInParameter("@SemPedido", DbType.String, notaFiscalVO.SemPedido);
                AddInParameter("@Observacao", DbType.String, notaFiscalVO.Observacao);
                AddInParameter("@Observacao2", DbType.String, notaFiscalVO.Observacao2);
                AddInParameter("@indMovimento", DbType.Boolean, notaFiscalVO.indMovimento);
                AddInParameter("@IndVendaBeneficiamento", DbType.Boolean, notaFiscalVO.IndVendaBeneficiamento);
                AddInParameter("@UsuarioAlt", DbType.Int32, notaFiscalVO.UsuarioInc);
                AddInParameter("@IndFaturamento", DbType.Boolean, notaFiscalVO.IndFaturamento);
                AddInParameter("@CodCFOP2", DbType.Int32, notaFiscalVO.CodCFOP2);
                AddInParameter("@IndVendaFaturamento", DbType.Boolean, notaFiscalVO.IndVendaFaturamento);
                AddInParameter("@Vendedor", DbType.String, notaFiscalVO.Vendedor);
                AddInParameter("@Serie", DbType.String, notaFiscalVO.Serie);
                AddInParameter("@IndFinalidadeNF", DbType.String, notaFiscalVO.IndFinalidadeNF);
                AddInParameter("@RefNFe", DbType.String, notaFiscalVO.RefNFe);
                AddInParameter("@XmlItem", DbType.String, notaFiscalVO.ToXml());

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
        /// Método para excluir um registro na tabela  NotaFiscal 
        /// </summary>
        #region Métodos de Exclusão
        public void Excluir(int codNF, int codUsuarioOperacao)
        {
            OpenCommand("pr_excluir_NotaFiscal");
            try
            {
                // Parâmetros de entrada
                AddInParameter("@CodNF", DbType.Int32, codNF);
                AddInParameter("@CodUsuarioOperacao", DbType.Int32, codUsuarioOperacao);

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
