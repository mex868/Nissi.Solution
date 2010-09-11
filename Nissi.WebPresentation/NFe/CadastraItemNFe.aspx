<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="CadastraItemNFe.aspx.cs" Inherits="CadastraItemNFe" Title="" %>
<%@ Register assembly="RDC.Tools" namespace="RDC.Tools" tagprefix="cc1" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="server">
    <link href="../App_Themes/Theme1/Model1.css" type="text/css"  rel="Stylesheet" />
<script type="text/javascript">
    //--------------------------------------------------------------------------------
    //Criado por...: Alexandre Maximiano - 02/11/2009
    //Objetivo.....: Cabeçalho padrão da página
    //--------------------------------------------------------------------------------
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_initializeRequest(InitializeRequest);
    prm.add_endRequest(EndRequest);
    var postBackElement;
    function InitializeRequest(sender, args) {
        if (prm.get_isInAsyncPostBack())
            args.set_cancel(true);

        if (args.get_postBackElement().type != 'checkbox')
            WaitAsyncPostBack(true);
    }
    //--------------------------------------------------------------------------------
    //Criado por...: Alexandre Maximiano - 08/06/2010
    //Objetivo.....: Calculo dos valores dos itens
    //--------------------------------------------------------------------------------
    function CalculoTotais() {
        var Qtd = Ponto($get("<%=txtQuantidade.ClientID %>").value);
        var PrcVenda = Ponto($get("<%=txtValorItem.ClientID%>").value);
        var PrcDesconto = Ponto($get("<%=txtDesconto.ClientID%>").value);
        var ICMS = Ponto($get("<%=txtICMS.ClientID%>").value);
        var IPI = Ponto($get("<%=txtIPI.ClientID%>").value);
        if (Qtd == '')
            Qtd = 0;
        if (PrcVenda == '')
            PrcVenda = 0;
        if (PrcDesconto == '')
            PrcDesconto = 0;
        if (ICMS == '')
            ICMS = 0;
        if (IPI == '')
            IPI = 0;
        if (Desconto != 0)
            var Desconto = Qtd * PrcVenda * PrcDesconto / 100;

        var Valor = (Qtd * PrcVenda) - Desconto
        $get("<%=txtTotalItem.ClientID%>").value = float2moeda(Valor);
        if (ICMS != 0)
            $get("<%=txtValorICMS.ClientID%>").value = float2moeda(ICMS * Valor / 100);
        if (IPI != 0)
            $get("<%=txtValorIPI.ClientID%>").value = float2moeda(IPI * Valor / 100);
    }
    function Ponto(texto) {
        texto = texto.replace(",", ".");
        return texto;
    }
    function EndRequest(sender, args) {
        WaitAsyncPostBack(false);
    }

</script>

    <asp:UpdatePanel ID="updItens" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
    <table cellpadding="3" cellspacing="0" class="fundoTabela" 
                                style="text-align:left">
        <tr >
            <td style="width: 126px" >
                <b >::Dados cadastrais:</b></td>
        </tr>
        <tr >
        <td style="padding-left:17px; width: 126px;" >Código:</td>
             <td style="padding-left:17px" colspan="3">
                Descrição:</td>
                <td style="padding-left:17px">
                Unidade de Medida:
                </td>
        </tr>
        <tr >
        <td style="width: 126px">
                <asp:TextBox ID="txtCodigo" runat="server" CssClass="formNovo" 
                                            MaxLength="50" Width="97px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ErrorMessage="Código falta ser preenchido." ControlToValidate="txtCodigo" 
                    ValidationGroup="ValidaDados">*</asp:RequiredFieldValidator>
            </td>
            <td  colspan="3">
                <asp:TextBox ID="txtDescricao" runat="server" CssClass="DesligarTextBox" 
                                            MaxLength="50" TabIndex="1" Width="420px" 
                    Height="17px"></asp:TextBox>
            </td>
            <td>
                <asp:DropDownList ID="ddlUnidade" runat="server" CssClass="formNovo" 
                                            MaxLength="50" TabIndex="2" Width="131px" 
                    Enabled="False">
                </asp:DropDownList>
            </td>
        </tr>
        <tr >
            <td style="padding-left:17px; width: 126px;" >
                Quantidade:    
            </td>
                <td style="padding-left:17px; width: 133px;" >
                                        Preço de Venda:</td>
                <td style="padding-left:17px; width: 161px;" >
                                        Desconto %:</td>
                <td style="padding-left:17px" colspan="2" >
                                        Total do Item:</td>
        </tr>
        <tr >
            <td style="width: 126px; height: 23px;" >
                <asp:TextBox ID="txtQuantidade" onkeypress="OnlyMoney();" onBlur = "CalculoTotais();" runat="server" 
                    TabIndex="3" Width="98px" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ErrorMessage="Quatidade falta ser preenchido." 
                    ControlToValidate="txtQuantidade" ValidationGroup="ValidaDados">*</asp:RequiredFieldValidator>
            </td>
            <td style="width: 133px; height: 23px;">
                <asp:TextBox ID="txtValorItem" onkeypress="OnlyMoney();" onBlur = "CalculoTotais();" runat="server" 
                    TabIndex="4" Width="91px" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ErrorMessage="Preço de Venda falta ser preenchido." 
                    ControlToValidate="txtValorItem" ValidationGroup="ValidaDados">*</asp:RequiredFieldValidator>
            </td>
            <td style="width: 161px; height: 23px;"><asp:TextBox ID="txtDesconto" 
                 onBlur = "CalculoTotais();" onkeypress="OnlyMoney();"  runat="server" TabIndex="5"></asp:TextBox></td>
            <td colspan="2" style="height: 23px">
                <asp:TextBox ID="txtTotalItem" onBlur = "CalculoTotais();" runat="server" 
                    TabIndex="6" ForeColor="Black" CssClass="DesligarTextBox" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="padding-left:17px; width: 126px;">
            ICMS:
            </td>
            <td style="padding-left:17px; width: 133px;">
            Base ICMS:
            </td>
            <td style="padding-left:17px; width: 161px;">
            Valor do ICMS:
            </td>
            <td colspan="2">IPI:</td>
        </td>
        </tr>
        <tr>
            <td style="width: 126px"><asp:TextBox ID="txtICMS" onBlur = "CalculoTotais();" 
                    runat="server" TabIndex="7" CausesValidation="True" onkeypress="OnlyMoney();" ></asp:TextBox></td>
            <td style="width: 133px">
                <asp:TextBox ID="txtBaseICMS" runat="server" TabIndex="8" 
                    ForeColor="Black" CssClass="DesligarTextBox" ></asp:TextBox></td>
            <td style="width: 161px">
                <asp:TextBox ID="txtValorICMS" runat="server" 
                    TabIndex="9" ForeColor="Black" CssClass="DesligarTextBox" ></asp:TextBox></td>
            <td colspan="2"><asp:TextBox ID="txtIPI" onBlur = "CalculoTotais();" runat="server" TabIndex="11" onkeypress="OnlyMoney();" ></asp:TextBox></td>
        </tr>
        <tr>
            <td style="padding-left:17px; width: 126px;">
                Valor do IPI:
            <td style="padding-left:17px; width: 133px;">
                Situação Tributária:
            </td>
            <td style="padding-left:17px; width: 161px;">
                Classificação Fiscal:
            </td>
            <td style="padding-left:17px" colspan="2" >
        </tr>
        <tr>
            <td style="width: 126px">
                <asp:TextBox ID="txtValorIPI" runat="server" 
                    TabIndex="12" ForeColor="Black" ReadOnly="True" 
                    CssClass="DesligarTextBox" ></asp:TextBox></td>
            <td style="width: 133px">
                <asp:TextBox ID="txtSituacaoTributaria" runat="server" 
                    TabIndex="13" Width="94px" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ErrorMessage="Situação tributária falta ser preenchido." 
                    ControlToValidate="txtSituacaoTributaria" ValidationGroup="ValidaDados">*</asp:RequiredFieldValidator>
            </td>
            <td style="width: 161px">
                <asp:DropDownList ID="ddlClassificacaoFiscal" 
                                                    runat="server" CssClass="formNovo" 
                    MaxLength="1" TabIndex="14" 
                                            Width="124px" Enabled="False">
                </asp:DropDownList>
                &nbsp;</td>
            <td style="width: 123px">
                <asp:CheckBox ID="ckbCalcSobIpi" runat="server" Text="Calcular ICMS Sob IPI" 
                    TabIndex="15" />
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr >
            <td style="padding-left:17px" colspan="5" >
                                           ICMS:
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <cc1:RDCGrid 
                    id="ICMSGrid" 
                    runat="server" 
                    autogeneratecolumns="False" 
                    bordercolor="Black" 
                    borderwidth="1px" 
                    cellpadding="1" 
                    cellspacing="3" 
                    gridlines="None" 
                    pagesize="15" 
                    showpagedetails="True" 
                    AllowPaging="True" 
                    MultiSelection="True" 
                    ShowHeaderCheckBoxColumn="False"
                    ShowOptionColumn="False" 
                    CssClass="alinhamento" onrowdatabound="ICMSGrid_RowDataBound" 
                    onpageindexchanging="ICMSGrid_PageIndexChanging" >
                    <Columns>
                        <asp:BoundField HeaderText="Tipo de Tributação">
                            <headerstyle wrap="false" CssClass="headerGrid"></headerstyle>
                            <itemstyle wrap="false" HorizontalAlign="Left" Width="55%"></itemstyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Origem">
                            <HeaderStyle Wrap="false" CssClass="headerGrid"/>
                            <ItemStyle HorizontalAlign="Left" Width="40%" />
                        </asp:BoundField>
                    </Columns>
                </cc1:RDCGrid>
            </td>
        </tr>
        </table>
        </ContentTemplate>
    </asp:UpdatePanel>
        <asp:UpdatePanel ID="upBotoesCadastro" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <table cellpadding="3" cellspacing="0" class="fundoTabela" 
                                style="text-align:left" >
                    <tr>
                        <td>
                            <asp:Button ID="btnIncluir" runat="server" CssClass="botao" 
                            onclick="btnIncluir_Click" Text="Incluir" Width="80px" 
                                ValidationGroup="ValidaDados"/>
                        </td>
                        <td style="display:none">
                        <asp:Button ID="btnAtualizar" runat="server" CssClass="botao" onclick="btnAtualizar_Click" />
                        </td>
                    </tr>
                </table>
                <div style="text-align:center">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                        ShowMessageBox="True" ValidationGroup="ValidaDados" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

