<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VisualizarNFe.aspx.cs"
    Inherits="Nissi.WebPresentation.NFe.VisualizarNFe" %>

<%@ Register Assembly="RDC.Tools" Namespace="RDC.Tools" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/App_Themes/Theme1/Model1.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="conteudo" style="text-align: center;">
        <div style="margin: auto; width: 70%">
            <table border="0" cellpadding="0" cellspacing="0" style="text-align: left; margin: auto;
                width: 100%">
                <tr>
                    <td class="quadrado">
                        <asp:Image ID="Image1" ImageUrl="~/Imagens/logo_novo.jpg" runat="server" />
                    </td>
                    <td class="tres-linhas-direita">
                        <label style="text-decoration: underline">
                            <b>Nissi Metalúrgica</b></label>
                        <br />
                        <br />
                        <label>
                            Rua Alto da Conceição, 254</label>
                        <br />
                        <label>
                            São Paulo</label>
                        <br />
                        <div style="text-align: left; display: inline; width: 80%">
                            <label>
                                Vl. Nova York</label>
                        </div>
                        <div style="text-align: right; display: inline; width: 10%">
                            <label>
                                SP</label>
                        </div>
                        <br />
                        <label>
                            Cep: 03479-050</label>
                    </td>
                    <td class="tres-linhas-direita">
                        <label>
                            Visualisação da Nota Fiscal</label>
                        <br />
                        <br />
                        <label>
                            Tipo:
                            <asp:Label ID="lblEntradaSaida" runat="server" Text="Entrada"></asp:Label></label>
                        <br />
                        <label>
                            Nro:
                            <asp:Label ID="lblNF" runat="server" Text="00000001"></asp:Label></label>
                        <br />
                        <label>
                            Série:
                            <asp:Label ID="lblSerie" runat="server" Text="01"></asp:Label></label>
                    </td>
                    <td class="tres-linhas-direita">
                        <label>
                            Chave de Acesso:</label>
                        <br />
                        <asp:Label ID="lblChaveNFE" runat="server" Text="35101005533093000133550020000000101000003267"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="tres-linhas-baixo">
                        <label style="font-size: 8px">
                            NATUREZA DA OPERACAO:</label><br />
                        <asp:Label ID="lblNaturezaOP" runat="server" Text="Venda Estadual"></asp:Label>
                    </td>
                    <td colspan="2" class="duas-linhas-direita">
                        <h1 style="text-align: center">
                            Sem Valor Fiscal</h1>
                    </td>
                </tr>
                <tr>
                    <td class="tres-linhas-baixo">
                        <label style="font-size: 8px">
                            INSCRIÇÃO ESTADUAL:</label>
                        <br />
                        <asp:Label ID="lblIncricaoEstadual" runat="server" Text="116556236116"></asp:Label>
                    </td>
                    <td colspan="2" class="duas-linhas-direita">
                        <label style="font-size: 8px">
                            INSC. EST. SUBST. TRIBUTARIO:</label>
                    </td>
                    <td class="duas-linhas-direita">
                        <label style="font-size: 8px">
                            CNPJ:</label>
                        <br />
                        <asp:Label ID="lblCNPJ" runat="server" Text="05.533.093/0001-33"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
            <table border="0" cellpadding="0" cellspacing="0" style="text-align: left; margin: auto;
                width: 100%">
                <tr>
                    <td colspan="3">
                        <label>
                            <b>DESTINATARIO/REMETENTE</b></label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" class="quadrado" style="width: 70%">
                        <label style="font-size: 8px">
                            NOME/RAZÃO SOCIAL:</label>
                        <br />
                        <asp:Label ID="lblRazaoSocial" runat="server" Text="Metalúrgica Arouca"></asp:Label>
                    </td>
                    <td class="tres-linhas-direita" style="width: 15%">
                        <label style="font-size: 8px">
                            CNPJ/CPF:</label>
                        <br />
                        <asp:Label ID="lblCPNJ" runat="server" Text="62.821.723/0001-09"></asp:Label>
                    </td>
                    <td class="tres-linhas-direita" style="width: 15%">
                        <label style="font-size: 8px">
                            DATA EMISSÃO:</label>
                        <br />
                        <asp:Label ID="lblDataEmissao" runat="server" Text="01/07/2010"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="tres-linhas-esquerda" style="width: 60%">
                        <label style="font-size: 8px">
                            ENDEREÇO:</label>
                        <br />
                        <asp:Label ID="lblEndereco" runat="server" Text="Rua Tabor, 235"></asp:Label>
                    </td>
                    <td class="duas-linhas-direita" style="width: 15%">
                        <label style="font-size: 8px">
                            BAIRRO/DISTRITO:</label>
                        <br />
                        <asp:Label ID="lblBairro" runat="server" Text="Ipiranga"></asp:Label>
                    </td>
                    <td class="duas-linhas-direita" style="width: 10%">
                        <label style="font-size: 8px">
                            CEP:</label>
                        <br />
                        <asp:Label ID="lblCep" runat="server" Text="04.202-020"></asp:Label>
                    </td>
                    <td class="duas-linhas-direita" style="width: 15%">
                        <label style="font-size: 8px">
                            DATA SAÍDA:</label>
                        <br />
                        <asp:Label ID="lblDataSaida" runat="server" Text="01/07/2010"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tres-linhas-esquerda" style="width: 50%">
                        <label style="font-size: 8px">
                            MUNICÍPIO:</label>
                        <br />
                        <asp:Label ID="lblMunicipio" runat="server" Text="São Paulo"></asp:Label>
                    </td>
                    <td class="duas-linhas-direita" style="width: 15%">
                        <label style="font-size: 8px">
                            FONE/FAX:</label>
                        <br />
                        <asp:Label ID="lblFoneFax" runat="server" Text="(11) 2272-9333"></asp:Label>
                    </td>
                    <td class="duas-linhas-direita" style="width: 15%">
                        <label style="font-size: 8px">
                            UF:</label>
                        <br />
                        <asp:Label ID="lblUF" runat="server" Text="SP"></asp:Label>
                    </td>
                    <td class="duas-linhas-direita" style="width: 10%">
                        <label style="font-size: 8px">
                            INSCRIÇÃO ESTADUAL:</label>
                        <br />
                        <asp:Label ID="lblInscricaoEstadualCliente" runat="server" Text="108358712112"></asp:Label>
                    </td>
                    <td class="duas-linhas-direita" style="width: 15%">
                        <label style="font-size: 8px">
                            HORA SAÍDA:</label>
                        <br />
                        <asp:Label ID="lblHoraSaida" runat="server" Text="01/07/2010"></asp:Label>
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0" style="text-align: left; margin: auto;
                width: 100%">
                <tr>
                    <td>
                        <label>
                            FATURA</label>
                    </td>
                </tr>
                <tr>
                    <td class="quadrado" style="height: 20px">
                        <asp:Label ID="lblFatura" runat="server" Text="1 R$ 671.31 01/07/2010 |"></asp:Label>
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0" style="text-align: left; margin: auto;
                width: 100%">
                <tr>
                    <td>
                        <label>
                            <b>CÁLCULO DO IMPOSTO</b></label>
                    </td>
                </tr>
                <tr>
                    <td class="quadrado">
                        <label style="font-size: 8px">
                            BASE DE CÁLCULO DO ICMS:</label>
                        <br />
                        <div style="text-align: right">
                            <asp:Label ID="lblBaseCalculoICMS" runat="server" Text="0,00"></asp:Label>
                        </div>
                    </td>
                    <td class="tres-linhas-direita">
                        <label style="font-size: 8px" runat="server">
                            VALOR DO ICMS:</label>
                        <br />
                        <div style="text-align: right">
                            <asp:Label ID="lblValorIcms" runat="server" Text="0,00"></asp:Label>
                        </div>
                    </td>
                    <td colspan="2" class="tres-linhas-direita">
                        <label style="font-size: 8px">
                            BASE DE CÁLC. DO ICMS SUBSTITUIÇÃO:</label>
                        <br />
                        <div style="text-align: right">
                            <asp:Label ID="lblBaseCalculoIcmsSub" runat="server" Text="0,00"></asp:Label>
                        </div>
                    </td>
                    <td class="tres-linhas-direita">
                        <label style="font-size: 8px">
                            VALOR DO ICMS SUBSTITUIÇÃO:</label>
                        <br />
                        <div style="text-align: right">
                            <asp:Label ID="lblValorIcmsSub" runat="server" Text="0,00"></asp:Label>
                        </div>
                    </td>
                    <td class="tres-linhas-direita">
                        <label style="font-size: 8px">
                            VALOR TOTAL DOS PRODUTOS:</label>
                        <br />
                        <div style="text-align: right">
                            <asp:Label ID="lblValorTotalProduto" runat="server" Text="0,00"></asp:Label>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="tres-linhas-baixo">
                        <label style="font-size: 8px">
                            VALOR DO FRETE:</label>
                        <br />
                        <div style="text-align: right">
                            <asp:Label ID="lblValorFrete" runat="server" Text="0,00"></asp:Label>
                        </div>
                    </td>
                    <td class="duas-linhas-direita">
                        <label style="font-size: 8px">
                            VALOR DO SEGURO:</label>
                        <br />
                        <div style="text-align: right">
                            <asp:Label ID="lblValorSeguro" runat="server" Text="0,00"></asp:Label>
                        </div>
                    </td>
                    <td class="duas-linhas-direita">
                        <label style="font-size: 8px">
                            DESCONTO:</label>
                        <br />
                        <div style="text-align: right">
                            <asp:Label ID="lblDesconto" runat="server" Text="0,00"></asp:Label>
                        </div>
                    </td>
                    <td class="duas-linhas-direita">
                        <label style="font-size: 8px">
                            OUTRAS DESPESAS ACESSÓRIAS:</label>
                        <br />
                        <div style="text-align: right">
                            <asp:Label ID="lblOutrasDespesas" runat="server" Text="0,00"></asp:Label>
                        </div>
                    </td>
                    <td class="duas-linhas-direita">
                        <label style="font-size: 8px">
                            VALOR IPI:</label>
                        <div style="text-align: right">
                            <asp:Label ID="lblValorIPI" runat="server" Text="0,00"></asp:Label>
                        </div>
                    </td>
                    <td class="duas-linhas-direita">
                        <label style="font-size: 8px">
                            VALOR TOTAL DA NOTA:</label>
                        <br />
                        <div style="text-align: right">
                            <asp:Label ID="lblValorTotalNota" runat="server" Text="0,00"></asp:Label>
                        </div>
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0" style="text-align: left; margin auto;
                width: 100%;">
                <tr>
                    <td colspan="8">
                        TRANSPORTADOR/VOLUMES TRANSPORTADOS
                    </td>
                </tr>
                <tr>
                    <td colspan="3" class="quadrado">
                        <label style="font-size: 8px">
                            RAZÃO SOCIAL:</label>
                        <br />
                        <asp:Label ID="lblRazaoSocialTransportadora" runat="server" Text="Rodo Jacto"></asp:Label>
                    </td>
                    <td class="tres-linhas-direita">
                        <label style="font-size: 8px">
                            FRETE POR CONTA:</label>
                        <br />
                        <asp:Label ID="lblFreteConta" runat="server" Text="1 - Emitente"></asp:Label>
                    </td>
                    <td class="tres-linhas-direita">
                        <label style="font-size: 8px">
                            CÓDIGO ANTT:</label>
                        <br />
                        <asp:Label ID="lblCodigoAntt" runat="server" Text="0000"></asp:Label>
                    </td>
                    <td class="tres-linhas-direita">
                        <label style="font-size: 8px">
                            PLACA VEÍCULO:</label>
                        <br />
                        <asp:Label ID="lblPlacaVeiculo" runat="server" Text="DTW-6371"></asp:Label>
                    </td>
                    <td class="tres-linhas-direita">
                        <label style="font-size: 8px">
                            UF:</label>
                        <br />
                        <asp:Label ID="lblUFVeiculo" runat="server" Text="SP"></asp:Label>
                    </td>
                    <td class="tres-linhas-direita">
                        <label style="font-size: 8px">
                            CNPJ/CPF:</label>
                        <br />
                        <asp:Label ID="lblCNPJTransportadora" runat="server" Text="05.533.093/0001-33"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" class="tres-linhas-baixo">
                        <label style="font-size: 8px">
                            ENDEREÇO:</label>
                        <br />
                        <asp:Label ID="lblEnderecoTransportadora" runat="server" Text="Miguel Dib"></asp:Label>
                    </td>
                    <td colspan="2" class="duas-linhas-direita">
                        <label style="font-size: 8px">
                            MUNICÍPIO:</label>
                        <br />
                        <asp:Label ID="lblMunicipioTransportadora" runat="server" Text="São Paulo"></asp:Label>
                    </td>
                    <td class="duas-linhas-direita">
                        <label style="font-size: 8px">
                            UF:</label>
                        <br />
                        <asp:Label ID="lblUFTransportadora" runat="server" Text="SP"></asp:Label>
                    </td>
                    <td colspan="2" class="duas-linhas-direita">
                        <label style="font-size: 8px">
                            INSCRIÇÃO ESTADUAL:</label>
                        <br />
                        <asp:Label ID="lblInscricaoEstadualTransportadora" runat="server" Text="108358712112"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tres-linhas-esquerda" style="width: 150px">
                        <label style="font-size: 8px">
                            QUANTIDADE:</label>
                        <br />
                        <asp:Label ID="lblQuantidade" runat="server" Text="3"></asp:Label>
                    </td>
                    <td class="duas-linhas-direita" style="width: 100px">
                        <label style="font-size: 8px">
                            ESPÉCIE:</label>
                        <br />
                        <asp:Label ID="lblEspecie" runat="server" Text="Caixa"></asp:Label>
                    </td>
                    <td class="duas-linhas-direita" style="width: 100px">
                        <label style="font-size: 8px">
                            MARCA:</label>
                        <br />
                        <asp:Label ID="lblMarca" runat="server" Text="Nissi"></asp:Label>
                    </td>
                    <td colspan="2" class="duas-linhas-direita">
                        <label style="font-size: 8px">
                            NUMERO:</label>
                        <br />
                        <asp:Label ID="lblNumeroVolume" runat="server" Text="3"></asp:Label>
                    </td>
                    <td class="duas-linhas-direita" style="width: 150px">
                        <label style="font-size: 8px">
                            PESO BRUTO:</label>
                        <br />
                                                <div style="text-align:right">

                        <asp:Label ID="lblPesoBruto" runat="server" Text="0,00"></asp:Label>
                        </div>
                    </td>
                    <td colspan="2" class="duas-linhas-direita">
                        <label style="font-size: 8px">
                            PESO LIQUIDO:</label>
                        <br />
                                                <div style="text-align:right">

                        <asp:Label ID="lblPesoLiquido" runat="server" Text="0,00"></asp:Label>
                        </div>
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0" style="text-align: left; margin auto;
                width: 100%;">
                <tr>
                    <td>
                        <b>DADOS DO PRODUTO/SERVIÇOS</b>
                    </td>
                </tr>
                <tr>
                    <td>
                        <cc1:RDCGrid ID="grdProduto" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            BorderColor="Black" BorderWidth="1px" CellPadding="1" CssClass="alinhamento"
                            GridLines="Vertical" MultiSelection="True" OnPageIndexChanging="grdProduto_PageIndexChanging"
                            OnRowCommand="grdProduto_RowCommand" OnRowDataBound="grdProduto_RowDataBound"
                            ShowHeaderCheckBoxColumn="False" ShowOptionColumn="False" ShowPageDetails="False"
                            Width="100%" EnableModelValidation="True" PageSize="20">
                            <Columns>
                                <asp:BoundField HeaderText="Código">
                                    <HeaderStyle CssClass="tres-linhas-baixo" />
                                    <ItemStyle HorizontalAlign="Left" Width="8%" CssClass="ColumnGrid" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="OP">
                                    <HeaderStyle CssClass="duas-linhas-direita" />
                                    <ItemStyle HorizontalAlign="Center" Width="6%" CssClass="ColumnGrid" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Descrição dos Produtos">
                                    <HeaderStyle CssClass="duas-linhas-direita" />
                                    <ItemStyle HorizontalAlign="Left" Width="30%" CssClass="ColumnGrid" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Cl. Fisc.">
                                    <HeaderStyle CssClass="duas-linhas-direita" />
                                    <ItemStyle HorizontalAlign="Left" Width="7%" CssClass="ColumnGrid" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="S.T.">
                                    <HeaderStyle CssClass="duas-linhas-direita" />
                                    <ItemStyle HorizontalAlign="Left" CssClass="ColumnGrid" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Unid.">
                                    <HeaderStyle CssClass="duas-linhas-direita" />
                                    <ItemStyle HorizontalAlign="Right" CssClass="ColumnGrid" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Qtde.">
                                    <HeaderStyle CssClass="duas-linhas-direita" />
                                    <ItemStyle HorizontalAlign="Right" CssClass="ColumnGrid" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Valor Unit.">
                                    <HeaderStyle CssClass="duas-linhas-direita" />
                                    <ItemStyle HorizontalAlign="Right" CssClass="ColumnGrid" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Valor Total">
                                    <HeaderStyle CssClass="duas-linhas-direita" />
                                    <ItemStyle HorizontalAlign="Right" CssClass="ColumnGrid" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="ICMS">
                                    <HeaderStyle CssClass="duas-linhas-direita" />
                                    <ItemStyle HorizontalAlign="Right" CssClass="ColumnGrid" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="IPI">
                                    <HeaderStyle CssClass="duas-linhas-direita" />
                                    <ItemStyle HorizontalAlign="Right" CssClass="ColumnGrid" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Valor do IPI">
                                    <HeaderStyle CssClass="headerGridVisualizar" />
                                </asp:BoundField>
                            </Columns>
                        </cc1:RDCGrid>
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0" style="text-align: left; margin auto;
                width: 100%;">
                <tr>
                    <td colspan="4">
                        <label>
                            <b>CÁLCULO DO ISSQN</b></label>
                    </td>
                </tr>
                <tr>
                    <td class="quadrado" style="height: 25px">
                        <label style="font-size: 8px">
                            INSCRIÇÃO MUNICIPAL:</label>
                        <br />
                        <asp:Label ID="lblIscricaoMunicipal" runat="server"></asp:Label>
                    </td>
                    <td class="tres-linhas-direita" style="height: 10px">
                        <label style="font-size: 8px">
                            VALOR TOTAL DOS SERVIÇOS:</label>
                        <br />
                        <asp:Label ID="lblValorTotalServico" runat="server"></asp:Label>
                    </td>
                    <td class="tres-linhas-direita" style="height: 10px">
                        <label style="font-size: 8px">
                            BASE DE CÁLCULO DO ISSQN:</label>
                        <br />
                        <asp:Label ID="lblBaseCalculoIssqn" runat="server"></asp:Label>
                    </td>
                    <td class="tres-linhas-direita" style="height: 10px">
                        <label style="font-size: 8px">
                            VALOR DO ISSQN:</label>
                        <br />
                        <asp:Label ID="lblValorIssqn" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0" style="text-align: left; margin auto;
                width: 100%;">
                <tr>
                    <td colspan="4">
                        <label>
                            <b>DADOS ADICIONAIS</b></label>
                    </td>
                </tr>
                <tr>
                    <td class="quadrado" style="height: 150px;width:600px">
                        <label style="font-size: 8px">
                            INFORMAÇÕES COMPLEMENTARES:</label>
                        <br />
                        <asp:Label ID="lblInformacaoComplementar" runat="server"></asp:Label>
                    </td>
                    <td class="tres-linhas-direita">
                        <label style="font-size: 8px">
                            RESERVADO AO FISCO:</label>
                        <br />
                        <asp:Label ID="lblResevadoFisco" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
