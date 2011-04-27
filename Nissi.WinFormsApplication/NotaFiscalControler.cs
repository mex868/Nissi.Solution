using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.Model;
using Nissi.Business;
using System.Net.Mail;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Nissi.WinFormsApplication
{
    
    public class NotaFiscalControler
    {
        
        // ------------------------------------------------------------------------- //
        /// <summary>
        /// Método de criação do xml com os dados da nota fiscal para nfe
        /// </summary>
        /// <param name="identNotaFiscal">Classe onde contém os dados da Nota Fiscal</param>
        /// <param name="parametroNfe">Classe onde contém os parametros da NF-e</param>
        /// <returns></returns>
        public string GerarNFe(NotaFiscalVO identNotaFiscal, nfec.Parametro parametroNfe)
        {
            string[] ide, emit, dest, total, transp, cobr, infAdic;
            string[,] prod;
            ide = new string[14];
            emit = new string[17];
            dest = new string[20];
            prod = new string[Convert.ToInt32(identNotaFiscal.Itens.Count), 60];
            total = new string[16];
            transp = new string[15];
            cobr = new string[6];
            infAdic = new string[2]; 
            try
            {

                /*<ide> TAG de grupo das informações de identificação da NF-e*/
                ide[0] = identNotaFiscal.Emitente.Cep.Cidade.UF.CodUF;//<cUF>
                ide[1] = identNotaFiscal.CodNF.ToString().PadLeft(8, '0');//<cNF> //8 DIGITOS A PARTIR DA VERSAO 2.0 (MANUAL 4.01)
                ide[2] = identNotaFiscal.CFOP.NaturezaOperacao; //<natOp>
                if (identNotaFiscal.IndFaturamento == null)
                    ide[3] = "2";
                else
                    ide[3] = identNotaFiscal.IndFaturamento == false ? "0" : "1"; //<indPag>
                ide[4] = "55"; //<mod>
                ide[5] = identNotaFiscal.Serie; //<serie>
                ide[6] = identNotaFiscal.NF.ToString();//<nNF>
                ide[7] = DateTime.Today.ToString("yyyy-MM-dd");         	 //<dEmi>
                if (identNotaFiscal.DataEntradaSaida != null)
                {
                    ide[8] = identNotaFiscal.DataEntradaSaida.Value.ToString("yyyy-MM-dd");          //<dSaiEnt>
                }
                else
                {
                    ide[8] = string.Empty;

                }
                ide[9] = identNotaFiscal.IndEntradaSaida == false ? "0" : "1";																		 //<tpNF>
                ide[10] = identNotaFiscal.Emitente.Cep.Cidade.CodIBGE;//txtCodMun.Text;//<cMunFG>
                ide[11] = identNotaFiscal.NFe.IndTipoEmissao;//<tpEmis> - Verificar no sistema se for erro de rede
                ide[12] = identNotaFiscal.IndFinalidadeNF;//<finNFe> Criar flag de escolha conforme manual 1- NF-e normal/ 2-NF-e complementar / 3 – NF-e de ajuste 
                ide[13] = identNotaFiscal.RefNFe; //NFref: <refNFe> 'nf-e relacionadas'

                /*<emit>TAG de grupo de identificação do emitente da NF-e*/
                emit[0] = identNotaFiscal.Emitente.CNPJ;
                emit[1] = identNotaFiscal.Emitente.RazaoSocial.Trim(); //<xNome>
                emit[2] = identNotaFiscal.Emitente.NomeFantasia.Trim();//<xFant>
                emit[3] = identNotaFiscal.Emitente.Logradouro.Trim(); //<xLgr>
                emit[4] = identNotaFiscal.Emitente.Numero.ToString(); //<nro>
                emit[5] = identNotaFiscal.Emitente.Complemento.Trim();//<xCpl>
                emit[6] = identNotaFiscal.Emitente.Cep.Bairro.NomBairro.Trim(); //<xBairro>
                emit[7] = identNotaFiscal.Emitente.Cep.Cidade.CodIBGE; //<cMun>
                emit[8] = identNotaFiscal.Emitente.Cep.Cidade.NomCidade.Trim(); //<xMun>
                emit[9] = identNotaFiscal.Emitente.Cep.Cidade.UF.CodUF;//<xUF>
                emit[10] = identNotaFiscal.Emitente.Cep.CodCep.Trim(); //<CEP>
                emit[11] = identNotaFiscal.Emitente.Telefone; //<fone>
                emit[12] = identNotaFiscal.Emitente.InscricaoEstadual; //<IE>
                emit[13] = identNotaFiscal.Emitente.InscricaoMunicipal; //<IM>
                emit[14] = identNotaFiscal.Emitente.CNAE;//<CNAE>
                emit[15] = "";//<IEST>
                emit[16] = identNotaFiscal.NFe.CRT;//<CRT> 1 – Simples Nacional; 2 – Simples Nacional – excesso de sublimite de receita bruta; 3 – Regime Normal

                /*<dest> TAG de grupo de identificação do Destinatário da NF-e*/
                dest[0] = identNotaFiscal.Cliente.CNPJ;//<CNPJ> ou <CPF>
                dest[1] = identNotaFiscal.Cliente.RazaoSocial.Trim();//<xNome>
                dest[2] = identNotaFiscal.Cliente.Cep.NomEndereco.Trim();//<xLgr>
                dest[3] = identNotaFiscal.Cliente.Numero.Trim();//<nro>
                dest[4] = identNotaFiscal.Cliente.Complemento.Trim();//<xCpl>
                dest[5] = identNotaFiscal.Cliente.Cep.Bairro.NomBairro.Trim();//<xBairro>
                dest[6] = identNotaFiscal.Cliente.Cep.Cidade.CodIBGE;//<cMun>
                dest[7] = identNotaFiscal.Cliente.Cep.Cidade.NomCidade.Trim();//<xMun>
                dest[8] = identNotaFiscal.Cliente.Cep.Cidade.UF.CodUF;//<UF>
                dest[9] = identNotaFiscal.Cliente.Cep.CodCep.Trim();//<CEP>
                dest[10] = "1058";//<cPais>
                dest[11] = "BRASIL";//<xPais>
                dest[12] = identNotaFiscal.Cliente.Telefone;//<fone>
                dest[13] = identNotaFiscal.Cliente.InscricaoEstadual;//<IE>
                dest[14] = "";//<ISUF>

                /* Grupo de Exportação v6.01 */
                dest[15] = "SP";														//UFEmbarq
                dest[16] = "11";														//xLocEmbarq

                /* Grupo de Compra v6.01 */
                dest[17] = "SP";														//xNEmp
                dest[18] = "SP";														//xPed
                dest[19] = "SP";														//xCont

                /*<prod> TAG de grupo do detalhamento de Produtos e Serviços da NF-e*/
                int x = 0;
                foreach (ItemNotaFiscalVO identItemNotaFiscal in identNotaFiscal.Itens)
                {
                    prod[x, 0] = identItemNotaFiscal.Produto.Codigo.Trim();//<cProd>
                    prod[x, 1] = "";					//<cEAN>
                    string Op = string.Empty;
                    if (!string.IsNullOrEmpty(identItemNotaFiscal.OP))
                        Op = " - OP.: " + identItemNotaFiscal.OP.Trim();
                    string pedido = string.Empty;
                    if (!string.IsNullOrEmpty(identItemNotaFiscal.CodPedidoCliente))
                        pedido = " - Ped.: " + identItemNotaFiscal.CodPedidoCliente.Trim();
                    prod[x, 2] = identItemNotaFiscal.Produto.Descricao.Trim()+pedido+Op; //<xProd>
                    prod[x, 3] = identItemNotaFiscal.Produto.NCM;//"73181500";                        //<NCM>
                    prod[x, 4] = "";                                //<EXTIPI> //Antes da vr 2.00, esta posicao era o GENERO.
                    prod[x, 5] = identNotaFiscal.CFOP.CFOP;//<CFOP>
                    prod[x, 6] = identItemNotaFiscal.Produto.Unidade.TipoUnidade.Trim();//<uCom>
                    prod[x, 7] = identItemNotaFiscal.Qtd.ToString(); //<qCom>
                    prod[x, 8] = identItemNotaFiscal.Valor.ToString();//<vUnCom>
                    prod[x, 9] = identItemNotaFiscal.TotalItem.ToString();// "10.00";//<vProd>
                    prod[x, 10] = "";   //eantrib //<cEANTrib>
                    prod[x, 11] = identItemNotaFiscal.Produto.Unidade.TipoUnidade.Trim();// "Kg"; //<uTrib>
                    prod[x, 12] = identItemNotaFiscal.Qtd.ToString();  //<qTrib>
                    prod[x, 13] = identItemNotaFiscal.Valor.ToString();// "10.0000";//<vUnTrib>
                    prod[x, 14] = identNotaFiscal.ValorFrete.ToString(); //"1.00";      //<vFrete>
                    prod[x, 15] = identNotaFiscal.ValorSeguro.ToString(); //"1.00";     //<vSeg>
                    prod[x, 16] = identItemNotaFiscal.Desconto.ToString(); //"1.00";    //<vDesc>   

                    /* tag ICMS */
                    prod[x, 17] = identItemNotaFiscal.Icms.CodOrigem.ToString();	//<orig>
                    prod[x, 18] = identItemNotaFiscal.Icms.CodTipoTributacao;      //txtCstIcms.Text;						//<CST>
                    prod[x, 19] = identItemNotaFiscal.Icms.CodBaseCalculo.ToString();//<modBC>
                    prod[x, 20] = identItemNotaFiscal.TotalItem.ToString(); //10.0000 //<vBC>
                    prod[x, 21] = identItemNotaFiscal.ICMS.ToString(); //"17.00";									//<pICMS>
                    prod[x, 22] = identItemNotaFiscal.CalcIcms.ToString(); // "1.70";//<vICMS>
                    if (identNotaFiscal.NFe.CRT == "1")
                    {
                        prod[x, 46] = "0";// "0";									//<modBCST>			{ campo novo }
                        prod[x, 47] = "";// "";										//<pMVAST>			{ campo novo }
                        prod[x, 48] = ""; //"";										//<pRedBCST>		{ campo novo }

                        prod[x, 49] = "0";									//<vBCSTRet>			//foi modificado, antes vBCST; v6.01 = vBCSTRet
                        prod[x, 50] = "0";									//<vICMSSTRet>			//foi modificado, antes vICMSST; v6.01 = vICMSSTRet

                        prod[x, 51] = "0";									//<vICMSST>			{ campo novo }				
                        prod[x, 52] = "0";// "0";             						//<pRedBC>			{ campo novo }


                        /* tag IPI */
                        prod[x, 23] = "99";						    //IPI <CST>
                        prod[x, 24] = "0.00";											//IPI <vBC>
                        prod[x, 25] = "0";												//IPI <pIPI>
                        prod[x, 26] = "0.00";												//IPI <vIPI>

                        /* tag II */
                        prod[x, 27] = "";               							 //II <vBC>
                        prod[x, 28] = "";               							 //II <vDespAdu>
                        prod[x, 29] = "";               								 //II <vII>
                        prod[x, 30] = "";               							 //II <vIOF>


                        /* tag PIS */
                        prod[x, 31] = "99";     						  //<CST>
                        prod[x, 32] = "0.00"; //"10.00";           						  //<vBC>
                        prod[x, 33] = "0.00";             						  //<pPIS>
                        prod[x, 34] = "0.00";             						  //<vPis>
                        prod[x, 45] = "0.00";               						  //<vAliqProd>     { campo novo }

                        /* tag COFINS */
                        prod[x, 35] = "99";  							//<CST>
                        prod[x, 36] = "0.00";//"10.00";										//<vBC>
                        prod[x, 37] = "0.00";											//<pCOFINS>
                        prod[x, 38] = "0.00";											//<vCOFINS>
                        prod[x, 44] = "0.00";											//<vAliqProd>     { campo novo }

                        /* tag ISSQN */
                        prod[x, 39] = "";                					//ISSQN <vBC>
                        prod[x, 40] = "";                					//ISSQN <vAliq>
                        prod[x, 41] = "";                					//ISSQN <vISSQN>
                        prod[x, 42] = "";                            //ISSQN <cMunFG>
                        prod[x, 43] = "";            						//ISSQN <cListServ>

                        prod[x, 53] = identItemNotaFiscal.Observacao;            						//infAdProd

                        /*tag PISST*/
                        prod[x, 54] = "";								//vBC
                        prod[x, 55] = "";								//pPIS
                        prod[x, 56] = "";								//vPIS


                        /*tag COFINSST*/
                        prod[x, 57] = "";								//vBC
                        prod[x, 58] = "";								//pCOFINS
                        prod[x, 59] = "";								//vCOFINS
                        x++;
                    }
                    else
                    {
                        prod[x, 46] = identItemNotaFiscal.Icms.CodBaseCalculoICMSST.ToString();// "0";									//<modBCST>			{ campo novo }
                        prod[x, 47] = identItemNotaFiscal.Icms.PercentualMargemST.ToString();// "";										//<pMVAST>			{ campo novo }
                        prod[x, 48] = identItemNotaFiscal.Icms.PercentualReducaoST.ToString(); //"";										//<pRedBCST>		{ campo novo }
                        prod[x, 52] = identItemNotaFiscal.Icms.PercentualReducao.ToString();// "0";             						//<pRedBC>			{ campo novo }
                    }
                }//for

                /*<total> TAG de grupo de Valores Totais da NF-e */
                if (identNotaFiscal.NFe.CRT == "1")
                {
                    total[0] = "0.00";// "50.00";          //ICMSTot <vBC>
                    total[1] = "0.00";// "8.50";          //ICMSTot <vICMS>
                }
                else
                {
                    total[0] = identNotaFiscal.BaseCalcIcms.ToString();// "50.00";          //ICMSTot <vBC>
                    total[1] = identNotaFiscal.ValTotalImcs.ToString();// "8.50";          //ICMSTot <vICMS>
                }
                total[2] = "0.00";          //ICMSTot <vBCST>
                total[3] = "0.00";          //ICMSTot <vST>
                identNotaFiscal = NotaFiscal.CalcTotais(identNotaFiscal);
                total[4] = identNotaFiscal.ValTotalProduto.ToString(); //"50.00";          //ICMSTot <vProd>
                total[5] = identNotaFiscal.ValorFrete.ToString(); //"5.00";          //ICMSTot <vFrete>
                total[6] = identNotaFiscal.ValorSeguro.ToString(); //"7.10";          //ICMSTot <vSeg>
                total[7] = identNotaFiscal.ValTotalDesc.ToString(); // "8.10";          //ICMSTot <vDesc>
                total[8] = "0.00";          //ICMSTot <vII>
                total[9] = "0.00";        //ICMSTot <vIPI>
                total[10] = "0.00";      //ICMSTot <vPIS>
                total[11] = "0.00";      //ICMSTot <vCOFINS>
                total[12] = identNotaFiscal.OutDespAce.ToString(); // "0.00";      //ICMSTot <vOutro>
                total[13] = identNotaFiscal.ValTotalNota.ToString(); //"50.00";      //ICMSTot <vNF>

                /*<transp> Informações do Transporte da NF-e*/

                transp[0] = identNotaFiscal.IndFretePorConta == true ? "1" : "0"; //"0";	//<modFrete>
                if (!string.IsNullOrEmpty(identNotaFiscal.Transportadora.CNPJ))
                {
                    transp[1] = identNotaFiscal.Transportadora.CNPJ;// "34261131000144";             //<CNPJ> ou <CPF>
                    transp[2] = identNotaFiscal.Transportadora.RazaoSocial.Trim(); // "Teste Transp Jack";			//<xNome>
                    transp[3] = identNotaFiscal.Transportadora.InscricaoEstadual; // "7012578100048";				//<IE>
                    string transnumero = string.Empty;
                    if (!string.IsNullOrEmpty(identNotaFiscal.Transportadora.Numero))
                        transnumero = ", " + identNotaFiscal.Transportadora.Numero.Trim();
                    string complemento = string.Empty;
                    if (!string.IsNullOrEmpty(identNotaFiscal.Transportadora.Complemento))
                        complemento = " - " + identNotaFiscal.Transportadora.Complemento.Trim();
                    string bairro = string.Empty;
                    if (!string.IsNullOrEmpty(identNotaFiscal.Transportadora.Cep.Bairro.NomBairro))
                        bairro = " - " + identNotaFiscal.Transportadora.Cep.Bairro.NomBairro.Trim();
                    string enderecoCompleto = identNotaFiscal.Transportadora.Cep.NomEndereco.Trim()+transnumero+complemento+bairro;
                    if (enderecoCompleto.Length > 60)
                        enderecoCompleto = enderecoCompleto.Substring(0, 60);
                    transp[4] = enderecoCompleto; // "Teste End. Transp. Jack";  //<xEnder>
                    transp[5] = identNotaFiscal.Transportadora.Cep.Cidade.NomCidade.Trim(); //"Belo Horizonte";					//<xMun>
                    transp[6] = identNotaFiscal.Transportadora.Cep.Cidade.UF.CodUF.Trim(); //"MG";										//<UF>
                }
                else
                {
                    transp[1] = "";// "34261131000144";             //<CNPJ> ou <CPF>
                    transp[2] = ""; // "Teste Transp Jack";			//<xNome>
                    transp[3] = ""; // "7012578100048";				//<IE>
                    transp[4] = ""; // "Teste End. Transp. Jack";  //<xEnder>
                    transp[5] = ""; //"Belo Horizonte";					//<xMun>
                    transp[6] = ""; //"MG";										//<UF>
                    transp[7] = ""; // "XOX2255";							//<placa>
                    transp[8] = ""; //"MG";										//<UF>
                }
                
                transp[9] = identNotaFiscal.QtdVolumes.Trim(); //"5";//<qVol>
                transp[10] = identNotaFiscal.Especie.Trim(); // "VOLUME";							//<esp>
                transp[11] = identNotaFiscal.Marca.Trim(); // "JACK";								//<marca>
                transp[12] = identNotaFiscal.Numero.Trim(); // "99";									//<nVol>
                transp[13] = identNotaFiscal.PesoLiquido.Trim(); //"11.535";							//<pesoL>
                transp[14] = identNotaFiscal.PesoBruto.ToString(); //"15.282";							//<pesoB>
                
                /*<cobr> Dados da Cobrança*/
                int j = 0;
                string numero = string.Empty;
                string vencimento = string.Empty;
                string valor = string.Empty;
                foreach (DuplicataVO identDuplicata in identNotaFiscal.Duplicatas)
                {
                    j++;
                    numero += j.ToString().PadLeft(2, '0') + ";";
                    vencimento += identDuplicata.Vencimento.Value.ToString("yyyy-MM-dd") + ";";
                    valor += identDuplicata.Valor.ToString() + ";";
                }
                cobr[0] = identNotaFiscal.NF.ToString();// "22";                   //fat <nFat>
                cobr[1] = identNotaFiscal.ValTotalNota.ToString(); //"50";                   //fat <vOrig>
                if (identNotaFiscal.Duplicatas.Count > 0)
                {
                    cobr[2] = identNotaFiscal.ValTotalNota.ToString(); //"453.28";            //fat <vLiq>
                }
                else
                    cobr[2] = string.Empty; //"453.28";            //fat <vLiq>
                /* neste ex, existem 2 parcelas */
                cobr[3] = numero.ToString().PadLeft(2, '0');//"01;02;";									//dup <nDup>
                cobr[4] = vencimento;//"2008-05-30;2008-06-30;";    //dup <dVenc>
                cobr[5] = valor;//"226.64;226.64;";					//dup <vDup>
                /*<infAdic> Informações Adicionais da NF-e*/
                infAdic[0] = "";
                string mensagemnfe = string.Empty;
                string observacao = string.Empty;
                string enderecoCobranca = string.Empty;
                Regex remover = new Regex(@"[\t\r\n]",RegexOptions.Compiled);
                string mensagemnf = string.Empty;
                if (!string.IsNullOrEmpty(mensagemnf))
                    mensagemnf =" - "+ identNotaFiscal.MensagemNF.Descricao.Trim();
                if (!string.IsNullOrEmpty(identNotaFiscal.Observacao))
                    observacao = " - "+remover.Replace(identNotaFiscal.Observacao, "");
                if (!string.IsNullOrEmpty(identNotaFiscal.Cliente.CepCobranca))
                    enderecoCobranca = " - Endereço de Cobrança: Cep: " + identNotaFiscal.Cliente.CepCobranca.Trim();
                if (!string.IsNullOrEmpty(identNotaFiscal.Cliente.EnderecoCobranca))
                    enderecoCobranca += " - "+identNotaFiscal.Cliente.EnderecoCobranca.Trim();

                infAdic[1] = "DOCUMENTO EMITIDO POR ME OPTANTE PELO SIMPLES NACIONAL. NAO GERA DIREITO A CREDITO FISCAL DE ICMS, ISS E IPI. - " +  //infAdFisco
                               "Valor R$ " + FormataValor(identNotaFiscal.ValTotalImcs.ToString(), 2) + " Aliquota " + identNotaFiscal.ICMS.ToString() + "% Nos termos do Art. 23 da LC 123/2006" + observacao + enderecoCobranca + mensagemnf; 		//infCpl




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }
            /* chamar função para gerar a nf-e*/
            nfec.nfecsharp nfe = new nfec.nfecsharp();

            string erroMensagem = nfe.GeraNFe(ide, emit, dest, prod, total, transp, cobr, infAdic, parametroNfe, true);
            return erroMensagem;
        }

        public bool ValidarNFe(string caminho,nfec.Parametro parametroNFe, string schema)
        {
            return new nfec.nfecsharp().ValidarArquivoXML(caminho, parametroNFe, schema);
        }
        /// <summary>
        /// Método de envio de email
        /// </summary>
        /// <param name="server">Servidor smtp</param>
        /// <param name="arquivo">nome e caminho do arquivo</param>
        /// <param name="titulo">titulo do email</param>
        public void enviarEmailAnexo(string server, string emailEnviar,string arquivo, string titulo, StringBuilder corpo)
        {
            string[] enviarEmails = emailEnviar.Split(';');
            //Cria a Mensagem
            MailMessage msge = new MailMessage();
            msge.From = new MailAddress("nfe@nissimetal.com.br");
            for (int i = 0; i < enviarEmails.Length; i++)
            {
                if (!string.IsNullOrEmpty(enviarEmails[i]))
                msge.To.Add(enviarEmails[i]);
            }
            msge.Subject = titulo;
            if (corpo.ToString().Contains("html"))
                msge.IsBodyHtml = true;
            msge.Body = corpo.ToString();
            //Cria o anexo
            Attachment att = new Attachment(arquivo);
          //Adiciona a mensagem
            msge.Attachments.Add(att);
          //Envia
            SmtpClient client = new SmtpClient(server,25);
            client.Credentials = new NetworkCredential("nfe@nissimetal.com.br", "nissi1973");
            client.Send(msge);
        }
                /// <summary>
        /// Método de envio de email
        /// </summary>
        /// <param name="server">Servidor smtp</param>
        /// <param name="arquivo">nome e caminho do arquivo</param>
        /// <param name="titulo">titulo do email</param>
        public void enviarEmailAnexo(string server, string emailEnviar, string titulo, StringBuilder corpo)
        {
            string[] enviarEmails = emailEnviar.Split(';');
            //Cria a Mensagem
            MailMessage msge = new MailMessage();
            msge.From = new MailAddress("nfe@nissimetal.com.br");
            for (int i = 0; i < enviarEmails.Length; i++)
            {
                if (!string.IsNullOrEmpty(enviarEmails[i]))
                    msge.To.Add(enviarEmails[i]);
            }
            msge.Subject = titulo;
            if (corpo.ToString().Contains("html"))
                msge.IsBodyHtml = true;
            msge.Body = corpo.ToString();
            //Envia
            SmtpClient client = new SmtpClient(server, 25);
            client.Credentials = new NetworkCredential("nfe@nissimetal.com.br", "nissi1973");
            client.Send(msge);
        }
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
                MessageBox.Show("Problemas ao formatar valor!",
                   "NF-e\r\n" + valor,
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Warning);
                return valor;
            }
        }
        public string GerarLote(string p, nfec.Parametro parametroNfe, int codLote)
        {
            return new nfec.nfecsharp().GerarLote(p, parametroNfe, codLote);   
        }

        public string StatusServicoNFe(nfec.Parametro parametroNfe)
        {
            return new nfec.nfecsharp().NfeStatusServico(parametroNfe);
        }

        public string RecepcaoNFe(string refStatus, nfec.Parametro parametroNfe)
        {
            return new nfec.nfecsharp().NfeRecepcao(refStatus, parametroNfe);
        }

        public string RetRecepcaoNfe(string recibo, nfec.Parametro parametroNfe)
        {
            return new nfec.nfecsharp().NfeRetRecepcao(recibo, parametroNfe);
        }

        public string DistribuicaoNFe(string chNFe, string recibo, string lote, nfec.Parametro parametroNfe)
        {
           return new nfec.nfecsharp().DistribuicaoNFe(chNFe, recibo, lote,parametroNfe);
        }
        
        public string CancelamentoNFe(string chNfe, string nProt,string xJust, nfec.Parametro parametroNfe)
        {
           return new nfec.nfecsharp().NfeCancelamento(chNfe, nProt, xJust, parametroNfe);
        }
        public string ConsultaNFe(string chNfe, nfec.Parametro parametroNfe)
        {
            return new nfec.nfecsharp().NfeConsulta(chNfe, parametroNfe);
        }
        public StringBuilder getBody(string serie, int? nF, string razaoSocial, string cNPJ, string chaveNFe, string protocolo)
        {
            StringBuilder sbBody = new StringBuilder();
            //Adiciona estrutura HTML do E-Mail  
            sbBody.Append("<html xmlns='http://www.w3.org/1999/xhtml'>");
            sbBody.Append("<head><title>NF-e Nacional</title>");
            sbBody.Append("<style type='text/css'>body {margin-left: 0px;margin-top: 0px;margin-right: 0px;margin-bottom: 0px;background-color: #E1E0F2;}");
            sbBody.Append("<div style=1margin:20 0 0 1001>");
            sbBody.Append("<div style='border:1px solid black; width:50%; padding:0 0 0 10'>");
            sbBody.Append("body,td,th {font-family: Verdana, Geneva, sans-serif;font-size: 12px;}</style></head><body>");
            sbBody.Append("Está mesangem se refere a Nota Fiscal eletrônica de série número: <b>[" + serie + "/" + nF.ToString().PadLeft(8, '0') + "]</b><br /><br />");
            sbBody.Append("<strong><h3>.::Emitida Para:</h3></strong><br />");
            sbBody.Append("<hr style='width:100%; padding-right:10px'>");
            sbBody.Append("<b>Razão Social:</b><br />");
            //Adiciona texto digitado no Razão Social
            sbBody.Append("[" + razaoSocial + "]");
            sbBody.Append("<br /><br />");
            sbBody.Append("<b>CNPJ:</b><br />");
            //Adiciona texto digitado no CNPJ  
            sbBody.Append("[" + cNPJ + "]");
            sbBody.Append("<br /><br />");
            sbBody.Append("Para verificar a autorização da SEFAZ referente à nota acima mencionada, acesse o site http://www.nfe.fazenda.gov.br/portal");
            sbBody.Append("<br /><br />");
            sbBody.Append("<b>Chave de Acesso:</b><br />");
            //Adiciona texto digitado no Chave de Acesso  
            sbBody.Append("[" + chaveNFe + "]");
            sbBody.Append("<br /><br />");
            sbBody.Append("<b>Protocolo:</b><br />");
            //Adiciona texto digitado no Protocolo   
            sbBody.Append("[" + protocolo + "]");
            sbBody.Append("<br /><br />");
            sbBody.Append("<b></b><br />");
            //Adiciona texto digitado no TextBox txtAssunto  
            sbBody.Append("Este e-mail foi enviado automaticamente pelo Sistema de Nota Fiscal Eletrônica (NF-e) da NISSI METALURGICA");
            sbBody.Append("<br /><br />");
            sbBody.Append("<b>Powered By:</b><br />");
            //Adiciona texto digitado no TextBox txtMensagem  
            sbBody.Append("Macware Sistemas -  http://www.macware.net");
            sbBody.Append("<br /><br />");
            sbBody.Append("</div>");
            sbBody.Append("</div>");
            sbBody.Append("<br /></body></html>");
            return sbBody;
        }
       
        /// <summary>
        /// Método que faz a impressão da Danfe
        /// </summary>
        /// <param name="pathXML"></param>
        /// <param name="pathPDF"></param>
        /// <param name="ambiente"></param>
        /// <param name="tipoImp"></param>
        /// <param name="formSeguranca"></param>
        /// <param name="PathPrincipal"></param>
        /// <param name="TotalizarCfop"></param>
        /// <param name="DataPacketFormSeg"></param>
        /// <param name="TipoDanfe"></param>
        /// <param name="DanfeLogo"></param>
        /// <param name="DanfeInfo"></param>
        /// <param name="DataPacket"></param>
        public void NFeDanfe(string pathXML, string pathPDF, int ambiente, int tipoImp, bool formSeguranca, string PathPrincipal, string TotalizarCfop, string DataPacketFormSeg, string TipoDanfe, string DanfeLogo, string DanfeInfo, string DataPacket)
        {
            new nfec.nfecsharp().NFeDanfe(pathXML, pathPDF, ambiente, tipoImp, formSeguranca, PathPrincipal, TotalizarCfop, DataPacketFormSeg, TipoDanfe, DanfeLogo, DanfeInfo, DataPacket);
        }

    }

}
