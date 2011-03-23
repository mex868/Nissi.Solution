using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nissi.Business;
using Nissi.Model;

namespace Nissi.WebPresentation.NFe
{
    public partial class VisualizarNFe : System.Web.UI.Page
    {
        #region Propriedades
        private ClienteVO DadosRazaoSocial
        {
            set
            {
                if (value.CodPessoa > 0)
                {
                    lblRazaoSocial.Text = value.RazaoSocial;
                    lblCep.Text = value.Cep.CodCep;
                    lblCNPJ.Text = value.CNPJ;
                    lblInscricaoEstadualCliente.Text = value.InscricaoEstadual;
                    lblEndereco.Text = value.Cep.NomEndereco +
                                                        (value.Numero.Trim().Equals("") ? "" : ", " + value.Numero) +
                                                        (value.Complemento.Trim().Equals("") ? "" : " - " + value.Complemento);
                    lblBairro.Text = value.Cep.Bairro.NomBairro;
                    lblMunicipio.Text = value.Cep.Cidade.NomCidade;
                    lblUF.Text = value.Cep.Cidade.UF.CodUF;
                    lblFoneFax.Text = value.Telefone + (value.Fax.Trim().Equals("") ? " " : " / " + value.Fax);
                }
            }
        }

        private TransportadoraVO DadosClienteTransportadora
        {
            set
            {
                if (value.CodPessoa > 0)
                {
                    lblRazaoSocialTransportadora.Text = value.RazaoSocial;
                    lblCNPJTransportadora.Text = value.CNPJ;
                    string transnumero = string.Empty;
                    if (!string.IsNullOrEmpty(value.Numero))
                        transnumero = ", " + value.Numero.Trim();
                    string complemento = string.Empty;
                    if (!string.IsNullOrEmpty(value.Complemento))
                        complemento = " - " + value.Complemento.Trim();
                    string bairro = string.Empty;
                    if (!string.IsNullOrEmpty(value.Cep.Bairro.NomBairro))
                        bairro = " - " + value.Cep.Bairro.NomBairro.Trim();
                    string enderecoCompleto = value.Cep.NomEndereco.Trim() + transnumero + complemento + bairro;
                    if (enderecoCompleto.Length > 60)
                        enderecoCompleto = enderecoCompleto.Substring(0, 60);
                    lblEnderecoTransportadora.Text = enderecoCompleto; // "Teste End. Transp. Jack";  //<xEnder>
                    lblMunicipioTransportadora.Text = value.Cep.Cidade.NomCidade.Trim(); //"Belo Horizonte";					//<xMun>
                    lblUFTransportadora.Text = value.Cep.Cidade.UF.CodUF.Trim(); //"MG";
                    lblInscricaoEstadualTransportadora.Text = value.InscricaoEstadual;
                }
            }
        }

        private CFOPVO DadosCFOP
        {
            set
            {
                lblNaturezaOP.Text = value.NaturezaOperacao;
            }
        }
        private NotaFiscalVO DadosNotaFiscal
        {
            set
            {
                #region Cabeçalho
                //DadosEmitente = value.Emitente;
                lblSerie.Text = value.Serie;
                lblEntradaSaida.Text = (value.IndEntradaSaida == true ? "1 - Saída" : "0 - Entrada");
                lblFreteConta.Text = (value.IndFretePorConta == true ? "1 - Destinatário" : "0 - Emitente");
                lblNF.Text = value.NF.ToString().PadLeft(8, '0');
                lblChaveNFE.Text = value.NFe.ChaveNFE;
                DadosCFOP = value.CFOP;
                #endregion                
                #region Destinatário
                DadosRazaoSocial = value.Cliente;
                if (value.DataEmissao != null) lblDataEmissao.Text = value.DataEmissao.Value.ToString("dd/MM/yyyy");
                if (value.DataEntradaSaida != null)
                    lblDataSaida.Text = value.DataEntradaSaida.Value.ToString("dd/MM/yyyy");
                if (value.Hora != null)
                    lblHoraSaida.Text = value.Hora.Value.ToString("hh:mm");
                #endregion
                #region Fatura
                lblFatura.Text = string.Empty;
                foreach (var duplicata in (value.Duplicatas))
                {
                    if (duplicata.Vencimento != null)
                        lblFatura.Text += string.Format("{0} {1:C} {2} |", duplicata.Numero, duplicata.Valor, duplicata.Vencimento.Value.ToString("dd/MM/yyyy"));
                }
                #endregion
                #region Calculo do Imposto
                var identNotaFiscal = NotaFiscal.CalcTotais(value);
                /*<total> TAG de grupo de Valores Totais da NF-e */
                if (identNotaFiscal.NFe.CRT == "1")
                {
                    lblBaseCalculoICMS.Text = "0,00";// "50.00";          //ICMSTot <vBC>
                    lblValorIcms.Text = "0,00";// "8.50";          //ICMSTot <vICMS>
                }
                else
                {
                    lblBaseCalculoICMS.Text = string.Format("{0:N}",identNotaFiscal.BaseCalcIcms);// "50.00";          //ICMSTot <vBC>
                    lblValorIcms.Text = string.Format("{0:N}",identNotaFiscal.ValTotalImcs);// "8.50";          //ICMSTot <vICMS>
                }
                lblBaseCalculoIcmsSub.Text = "0,00";          //ICMSTot <vBCST>
                lblValorIcmsSub.Text = "0,00";          //ICMSTot <vST>
                lblValorTotalProduto.Text = string.Format("{0:N}",identNotaFiscal.ValTotalProduto); //"50.00";          //ICMSTot <vProd>
                lblValorFrete.Text = string.Format("{0:N}", value.ValorFrete);
                lblValorSeguro.Text = string.Format("{0:N}", value.ValorSeguro);
                lblOutrasDespesas.Text = string.Format("{0:N}", value.OutDespAce); 
                lblValorTotalNota.Text = string.Format("{0:N}",identNotaFiscal.ValTotalNota); //"50.00";      //ICMSTot <vNF>
                #endregion
                #region Transportador Volumes Transportados
                lblPlacaVeiculo.Text = value.PlacaVeiculo;
                lblUF.Text = value.UF;
                lblQuantidade.Text = value.QtdVolumes;
                lblEspecie.Text = value.Especie;
                lblMarca.Text = value.Marca;
                lblNumeroVolume.Text = value.Numero;
                lblPesoBruto.Text = value.PesoBruto.ToString();
                lblPesoLiquido.Text = value.PesoLiquido;
                DadosClienteTransportadora = value.Transportadora;
                #endregion             
                #region Produtos Serviços
                grdProduto.DataSource = value.Itens;
                grdProduto.DataBind();
                #endregion
                #region Dados Adicionais
                string observacao = string.Empty;
                string enderecoCobranca = string.Empty;
                Regex remover = new Regex(@"[\t\r\n]", RegexOptions.Compiled);
                string mensagemnf = string.Empty;
                if (!string.IsNullOrEmpty(mensagemnf))
                    mensagemnf = " - " + identNotaFiscal.MensagemNF.Descricao.Trim();
                if (!string.IsNullOrEmpty(identNotaFiscal.Observacao))
                    observacao = " - " + remover.Replace(identNotaFiscal.Observacao, "");
                if (!string.IsNullOrEmpty(identNotaFiscal.Cliente.CepCobranca))
                    enderecoCobranca = " - Endereço de Cobrança: Cep: " + identNotaFiscal.Cliente.CepCobranca.Trim();
                if (!string.IsNullOrEmpty(identNotaFiscal.Cliente.EnderecoCobranca))
                    enderecoCobranca += " - " + identNotaFiscal.Cliente.EnderecoCobranca.Trim();

                lblInformacaoComplementar.Text = "DOCUMENTO EMITIDO POR ME OPTANTE PELO SIMPLES NACIONAL. NAO GERA DIREITO A CREDITO FISCAL DE ICMS, ISS E IPI. - " +  //infAdFisco
                               "Valor R$ " + FormataValor(identNotaFiscal.ValTotalImcs.ToString(), 2) + " Aliquota " + identNotaFiscal.ICMS.ToString() + "% Nos termos do Art. 23 da LC 123/2006" + observacao + enderecoCobranca + mensagemnf; 		//infCpl

                #endregion
            }
        }
        #endregion
        public string FormataValor(string valor, int decimais)
        {
            if (valor.Trim() == string.Empty) valor = "0";

            string tmp = string.Empty;
            try
            {
                for (int x = 0; x < decimais; x++)
                    tmp += "0";
                return String.Format("{0:0." + tmp + "}", Convert.ToDouble(valor.Replace(".", ","))).Replace(",", ".");
            }
            catch (Exception ex)
            {
                return valor;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            var identNotaFiscal = new NotaFiscalVO
                                      {
                                          CodNF = Convert.ToInt32(Request.QueryString["CodNF"])
                                      };
            identNotaFiscal = new NotaFiscal().ListarTudo(identNotaFiscal).First();
            DadosNotaFiscal = identNotaFiscal;
        }

        protected void grdProduto_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void grdProduto_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }


        protected void grdProduto_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ItemNotaFiscalVO identItemNotaFiscal = (ItemNotaFiscalVO) e.Row.DataItem;
                e.Row.Cells[0].Text = identItemNotaFiscal.Produto.Codigo;
                if (!string.IsNullOrEmpty(identItemNotaFiscal.OP))
                    e.Row.Cells[1].Text = identItemNotaFiscal.OP.Trim();
                string pedido = string.Empty;
                if (!string.IsNullOrEmpty(identItemNotaFiscal.CodPedidoCliente))
                    pedido = " - Ped.: " + identItemNotaFiscal.CodPedidoCliente.Trim();

                e.Row.Cells[2].Text = identItemNotaFiscal.Produto.Descricao + pedido;
                e.Row.Cells[3].Text = identItemNotaFiscal.Produto.NCM;
                e.Row.Cells[4].Text = identItemNotaFiscal.Icms.CodTipoTributacao;
                e.Row.Cells[5].Text = identItemNotaFiscal.Produto.Unidade.TipoUnidade;
                e.Row.Cells[6].Text = identItemNotaFiscal.Qtd.ToString();
                e.Row.Cells[7].Text = identItemNotaFiscal.Valor.ToString();
                e.Row.Cells[8].Text = identItemNotaFiscal.TotalItem.ToString();
                e.Row.Cells[9].Text = identItemNotaFiscal.ICMS.ToString();
                e.Row.Cells[10].Text = identItemNotaFiscal.IPI.ToString();
                e.Row.Cells[11].Text = identItemNotaFiscal.CalcIPI.ToString();
            }
        }
    }
}