<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="CadastraNFe.aspx.cs" Inherits="CadastrarNFe" EnableEventValidation="false" %>

<%@ Register Assembly="RDC.Tools" Namespace="RDC.Tools" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            loadDate = function () {
                $(".dataEmissao").datepicker({ changeMonth: true, changeYear: true });
                $(".dataSaida").datepicker({ changeMonth: true, changeYear: true });
            }
            loadDate();
        });
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
        function EndRequest(sender, args) {
            WaitAsyncPostBack(false);
            loadDate();
        }
        function CarregarValores(source, eventArgs) {
            $get('<%=hdfIdRazaoSocial.ClientID%>').value = eventArgs.get_value();
            $get('<%=txtRazaoSocial.ClientID %>').value = eventArgs._item.outerText;
            $get('<%=btnCarregarValores.ClientID%>').click();
        }
        function CarregarValoresCFOP() {
            if ($get('<%=txtCFOP.ClientID%>').value != '')
                $get('<%=btnCarregarValoresCFOP.ClientID%>').click();
        }
        function OcultarBotaoCarregarValores() {
            $get('<%=btnCarregarValores.ClientID%>').style.display = "none";
            $get('<%=btnCarregarValoresCFOP.ClientID%>').style.display = "none";
        }

        function ChamaPopup() {
            var codigoCliente = $get("<%=hdfCodigoCliente.ClientID%>").value;
            if (codigoCliente == "") {
                alert("Escolha o cliente antes de adicionar o produto!");
                return;
            }
            window.showModalDialog('<%=caminhoAplicacao%>' + "Produto/CadastraProduto.aspx?acao=IncluirItem&codigo=" + codigoCliente, "", "dialogHeight=600px;dialogWidth=900px;status=no,toolbar=no,menubar=no,location=no;unadorned=no;help=no; resizable: No; status: No;");
        }
        function ReplicarValor() {
            if ($get("<%=txtBruto.ClientID%>").value != "")
                $get("<%=txtLiquido.ClientID%>").value = $get("<%=txtBruto.ClientID%>").value;
        }
        function ValidarFatura(src, args) {
            if ($get("<%=txtNumeroFatura.ClientID %>").value == "00000000" || $get("<%=txtNumeroFatura.ClientID %>").value == "")
                args.IsValid = false;
        }

    </script>
    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="updDados">
        <contenttemplate>
       <asp:HiddenField ID="hdfIdRazaoSocial" runat="server" />
       <asp:HiddenField ID="hdfCodigoCliente" runat="server" />
       <asp:HiddenField ID="hdfTipoAcao" runat="server" />
       <asp:HiddenField ID="hdfSerie" runat="server" />
       <asp:HiddenField ID="hdfCodNF" runat="server" />
       <asp:HiddenField ID="hdfCodEmitente" runat="server" />
        <table style="margin-left: auto; width: 75%; margin-right: auto;">
            <tr>
                <td style="width: 21px; text-align:left">
                    <asp:Image ID="ImgCadastro" runat="server" ImageUrl="~/Imagens/layout.png" />
                </td>
                <td style="width: 95%; text-align: left" class="titulo">
                    Cadastro de Nota Fiscal</td>
            </tr>
        </table >
        <table class="fundoTabela" style="text-align:left; width:75%">
            <tr>
            <td style="width: 656px"></td>
            <td style="padding-left:20px; width: 591px">Série</td>
            <td style="padding-left:20px; width: 106px;">Emissão</td>
            <td style="padding-left:20px; width: 179px;">Saída/Entrada</td>
            <td style="padding-left:20px">Hora Saída</td>
            </tr>
            <tr>
            <td></td>
            <td><asp:TextBox ID="txtSerie" runat="server" Width="80px"></asp:TextBox></td>
            <td>
                        <asp:TextBox ID="txtEmissao" runat="server" Width="106px" CssClass="dataEmissao"></asp:TextBox>
                        <ajaxToolkit:MaskedEditExtender ID="mskDataEmissao" runat="server" 
                        AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="false" 
                        Mask="99/99/9999" MaskType="Date" MessageValidatorTip="false" 
                        TargetControlID="txtEmissao" UserDateFormat="DayMonthYear">
                        </ajaxToolkit:MaskedEditExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                            ControlToValidate="txtEmissao" 
                            ErrorMessage="Data de Emissão falta ser preenchida." 
                            ValidationGroup="ValidaDados">*</asp:RequiredFieldValidator>
            </td>
            <td><asp:TextBox id="txtSaida" runat="server" Width="96px" TabIndex="1" 
                    CssClass="dataSaida"></asp:TextBox>
            <ajaxToolkit:MaskedEditExtender ID="mskDataSaida" runat="server" 
            AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="false" 
            Mask="99/99/9999" MaskType="Date" MessageValidatorTip="false" 
            TargetControlID="txtSaida" UserDateFormat="DayMonthYear">
            </ajaxToolkit:MaskedEditExtender>
            </td>
            <td><asp:TextBox ID="txtHora" runat="server" Width="93px" TabIndex="2"></asp:TextBox>
            <ajaxToolkit:MaskedEditExtender ID="mskHoraSaida" runat="server" 
            AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="false" 
            Mask="99:99" MessageValidatorTip="false" 
            TargetControlID="txtHora" UserTimeFormat="TwentyFourHour" CultureName="pt-BR" 
                    ClearMaskOnLostFocus="False">
            </ajaxToolkit:MaskedEditExtender>
            </td>
            </tr>
            <tr>
            <td style="padding-left:20px">Tipo de Documento</td>
                <td style="padding-left:20px">
                    Finalidade de Emissão</td>
            <td style="padding-left:20px; width: 106px;">Nº N.F.</td>
            <td style="padding-left:20px; width: 179px;">CFOP</td>
            <td style="padding-left:20px">Natureza da Operação</td>
            </tr>
            <tr>
            <td style="text-align:left">
                <asp:DropDownList ID="ddlTipoDocumento" runat="server" TabIndex="4">
                    <asp:ListItem Value="1">1 - Saída</asp:ListItem>
                    <asp:ListItem Value="0">0 - Entrada</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:DropDownList ID="ddlFinalidadeEmissao" runat="server" Width="177px" 
                    TabIndex="4">
                    <asp:ListItem Value="1">1 - NF-e Normal</asp:ListItem>
                    <asp:ListItem Value="2">2 - NF-e complementar</asp:ListItem>
                    <asp:ListItem Value="3">3 - NF-e de ajuste</asp:ListItem>
                </asp:DropDownList>
                </td>
            <td><asp:TextBox ID="txtNF" runat="server" onkeypress="OnlyNumbers();" TabIndex="5"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ControlToValidate="txtNF" ErrorMessage="Nº N.F. Falta ser preenchido." 
                    ValidationGroup="ValidaDados">*</asp:RequiredFieldValidator>
                </td>
            <td>
                <asp:TextBox ID="txtCFOP" runat="server" onkeypress="OnlyNumbers();" onBlur="CarregarValoresCFOP();" 
                    TabIndex="6"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ControlToValidate="txtCFOP" ErrorMessage="CFOP falta ser preenchido." 
                    ValidationGroup="ValidaDados">*</asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:UpdatePanel ID="updNaturezaOperacao" runat="server" UpdateMode="Conditional">
                    <Contenttemplate>
                        <asp:TextBox ID="txtNaturezaOperacao" runat="server" Width="180px" TabIndex="7" 
                            CssClass="DesligarTextBox"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                            ErrorMessage="Natureza da Operação falta ser preenchido." 
                            ControlToValidate="txtNaturezaOperacao" ValidationGroup="ValidaDados">*</asp:RequiredFieldValidator>
                                    <asp:HiddenField ID="hdfCodCFOP" runat="server" />
                        &nbsp;
                    </Contenttemplate>
                    <triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnCarregarValoresCFOP" 
                            EventName="Click" />
                    </triggers>
                    </asp:UpdatePanel>
                    </td>

    </tr>
    <tr>
        <td colspan="5" style="padding-left: 20px">
            Destinatário/Remetente
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px;" colspan="3">
            Nome/Razão Social
        </td>
        <td style="padding-left: 20px;">
            CNPJ/CPF
        </td>
        <td style="padding-left: 20px">
            Inscrição Estadual
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:TextBox ID="txtRazaoSocial" runat="server" Width="350px" TabIndex="8"></asp:TextBox>
            <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtRazaoSocial"
                MinimumPrefixLength="1" ServiceMethod="GetNames" CompletionInterval="800" EnableCaching="true"
                CompletionSetCount="10" OnClientItemSelected="CarregarValores" OnClientPopulated="ClientPopulated">
            </ajaxToolkit:AutoCompleteExtender>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRazaoSocial"
                ErrorMessage="Nome/Razao Social falta ser preenchido." ValidationGroup="ValidaDados">*</asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:TextBox ID="txtCNPJ" runat="server" Width="164px" TabIndex="9" CssClass="DesligarTextBox"></asp:TextBox>
        </td>
        <td>
            <asp:TextBox ID="txtInscricaoEstatual" runat="server" Width="194px" TabIndex="10"
                CssClass="DesligarTextBox"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px;" colspan="3">
            Cep
        </td>
        <td style="padding-left: 20px;">
            Endereço
        </td>
        <td style="padding-left: 20px">
            Bairro/Distrito
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:TextBox ID="txtCEP" runat="server" Width="129px" TabIndex="11" CssClass="DesligarTextBox"></asp:TextBox>
        </td>
        <td>
            <asp:TextBox ID="txtEndereco" runat="server" Width="164px" TabIndex="12" CssClass="DesligarTextBox"></asp:TextBox>
        </td>
        <td>
            <asp:TextBox ID="txtBairro" runat="server" Width="194px" TabIndex="13" CssClass="DesligarTextBox"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 20px;" colspan="3">
            Município
        </td>
        <td style="padding-left: 20px;">
            Fone/Fax
        </td>
        <td style="padding-left: 20px">
            UF
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:TextBox ID="txtMunicipio" runat="server" Width="350px" TabIndex="14" CssClass="DesligarTextBox"></asp:TextBox>
        </td>
        <td>
            <asp:TextBox ID="txtFoneFax" runat="server" Width="164px" TabIndex="15" CssClass="DesligarTextBox"></asp:TextBox>
        </td>
        <td>
            <asp:TextBox ID="txtUF" runat="server" Width="194px" TabIndex="16" CssClass="DesligarTextBox"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td colspan="5" style="padding-left: 20px">
            <ajaxToolkit:TabContainer ID="TabContainer1" CssClass="TabPanelImportar" runat="server"
                ActiveTabIndex="0" Height="510px" Style="margin-bottom: 30px" 
                Width="932px" AutoPostBack="true"
                OnActiveTabChanged="TabContainer1_ActiveTabChanged">
                <ajaxToolkit:TabPanel ID="tpProduto" runat="server" HeaderText="Dados do Produto">
                    <HeaderTemplate>
                        Dados do Produto
                    </HeaderTemplate>
                    <ContentTemplate>
                        <table style="height: 300; width: 920px">
                            <tr>
                                <td>
                                    <asp:UpdatePanel ID="updProduto" runat="server" UpdateMode="Conditional">
                                        <contenttemplate>
                            <cc1:RDCGrid ID="grdProduto" runat="server" AllowPaging="True" 
                                autogeneratecolumns="False" bordercolor="Black" borderwidth="1px" 
                                cellpadding="1" cellspacing="3" CssClass="alinhamento" gridlines="None" 
                                MultiSelection="True" onpageindexchanging="grdProduto_PageIndexChanging" 
                                onrowcommand="grdProduto_RowCommand" 
                                onrowdatabound="grdProduto_RowDataBound" 
                                ShowHeaderCheckBoxColumn="False" ShowOptionColumn="False" 
                                showpagedetails="True" ShowFooter="True" Width="100%" EnableModelValidation="True">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Ações">
                                            <itemtemplate>
                                                <asp:ImageButton ID="imgEditar" runat="server" Height="15px" 
                                                ImageUrl="~/Imagens/editar.png" Width="15px" />
                                                <asp:ImageButton ID="imgExcluir" runat="server" Height="15px" 
                                                ImageUrl="~/Imagens/exclusao_Canc.png" Width="15px" />
                                            </itemtemplate>
                                            <HeaderStyle CssClass="headerGrid"/>
                                            <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Código">
                                            <HeaderStyle CssClass="headerGrid" />
                                            <ItemStyle HorizontalAlign="Left" Width="8%" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="OP">
                                        <HeaderStyle CssClass="headerGrid" />
                                        <ItemStyle HorizontalAlign="Center" Width="6%" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Descrição dos Produtos">
                                            <HeaderStyle CssClass="headerGrid" />
                                            <ItemStyle HorizontalAlign="Left" Width="30%" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Cl. Fisc.">
                                            <HeaderStyle CssClass="headerGrid" />
                                            <ItemStyle HorizontalAlign="Left" Width="7%" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="S.T.">
                                            <HeaderStyle CssClass="headerGrid" />
                                            <ItemStyle HorizontalAlign="Left"/>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Unid.">
                                            <HeaderStyle CssClass="headerGrid" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Qtde.">
                                            <HeaderStyle CssClass="headerGrid" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Valor Unit.">
                                            <HeaderStyle CssClass="headerGrid" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Valor Total">
                                            <HeaderStyle CssClass="headerGrid" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="ICMS">
                                            <HeaderStyle CssClass="headerGrid" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="IPI">
                                            <HeaderStyle CssClass="headerGrid" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Valor do IPI">
                                            <HeaderStyle CssClass="headerGrid" />
                                        </asp:BoundField>
                                    </Columns>
                                </cc1:RDCGrid>
                            </contenttemplate>
                                        <triggers>
<asp:AsyncPostBackTrigger ControlID="btnIncluirProduto" EventName="Click" ></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="grdProduto" EventName="RowCommand" ></asp:AsyncPostBackTrigger>
</triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left">
                                    <asp:Button ID="btnIncluirProduto" runat="server" Text="Incluir" CssClass="botao"
                                        Height="20px" Width="80px" OnClick="btnIncluirProduto_Click" />
                                </td>
                            </tr>
                        </table>
                        <asp:HiddenField ID="hdfTipoAcaoItemNF" runat="server" />
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel ID="tpFatura" runat="server" HeaderText="Fatura">
                    <HeaderTemplate>
                        Fatura
                    </HeaderTemplate>
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td style="padding-left: 17px">
                                    Número
                                </td>
                                <td style="padding-left: 17px">
                                    Valor Original:
                                </td>
                                <td style="padding-left: 17px">
                                    Valor do Desconto:
                                </td>
                                <td style="padding-left: 17px">
                                    Valor Líquido:
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtNumeroFatura" runat="server" CssClass="DesligarTextBox"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="O número da fatura não pode ser 00000000"
                                        OnServerValidate="CustomValidator1_ServerValidate" ValidationGroup="ValidarFatura"
                                        ClientValidationFunction="ValidarFatura">*</asp:CustomValidator>
                                    <asp:TextBox ID="txtValorOriginal" runat="server" CssClass="DesligarTextBox"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtValorDesconto" runat="server" CssClass="DesligarTextBox"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtValorLiquido" runat="server" CssClass="DesligarTextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-left: 17px">
                                    Dias
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:UpdatePanel ID="updDias" runat="server">
                                        <contenttemplate>
                           <asp:TextBox ID="txtDias" runat="server" onkeypress="OnlyNumbers();"></asp:TextBox>
                        </contenttemplate>
                                        <triggers>
<asp:AsyncPostBackTrigger ControlID="grdFatura" 
                            EventName="RowCommand" ></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="btnIncluirFatura" 
                            EventName="Click" ></asp:AsyncPostBackTrigger>
</triggers>
                                    </asp:UpdatePanel>
                                </td>
                                <td>
                                    <asp:Button ID="btnIncluirFatura" runat="server" CssClass="botao" Text="Incluir"
                                        Width="80px" OnClick="btnIncluirFatura_Click" ValidationGroup="ValidarFatura" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:UpdatePanel ID="updFatura" runat="server" UpdateMode="Conditional">
                                        <contenttemplate>
                        <cc1:RDCGrid ID="grdFatura" runat="server" AllowPaging="True" 
                            AutoGenerateColumns="False" BorderColor="Black" BorderWidth="1px" 
                            CellPadding="1" CellSpacing="3" CssClass="alinhamento" GridLines="None" 
                            MultiSelection="True" 
                            OnPageIndexChanging="grdFatura_PageIndexChanging" 
                            OnRowCommand="grdFatura_RowCommand" 
                            OnRowDataBound="grdFatura_RowDataBound" PageSize="5" 
                            ShowHeaderCheckBoxColumn="False" ShowOptionColumn="False" 
                            ShowPageDetails="True" ShowFooter="True">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Ações">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgEditarFatura" runat="server" Height="15px" 
                                            ImageUrl="~/Imagens/editar.png" Width="15px" />
                                                        <asp:ImageButton ID="imgExcluirFatura" runat="server" Height="15px" 
                                            ImageUrl="~/Imagens/exclusao_Canc.png" Width="15px" />
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="headerGrid" Width="5%" />
                                                    <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Dias">
                                                    <HeaderStyle CssClass="headerGrid" />
                                                    <ItemStyle HorizontalAlign="Left" Width="8%" />
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Vencimento">
                                                    <HeaderStyle CssClass="headerGrid" />
                                                    <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Número">
                                                    <HeaderStyle CssClass="headerGrid" />
                                                    <ItemStyle HorizontalAlign="Left" Width="7%" />
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Valor">
                                                    <HeaderStyle CssClass="headerGrid" />
                                                    <ItemStyle HorizontalAlign="Left" Width="7%" />
                                                </asp:BoundField>
                                            </Columns>
                                        </cc1:RDCGrid>
                        <asp:HiddenField ID="hdfTipoAcaoFatura" runat="server" />
                        <asp:HiddenField ID="hdfCodDuplicata" runat="server" />
                        </contenttemplate>
                                        <triggers>
<asp:AsyncPostBackTrigger ControlID="btnIncluirFatura" 
                            EventName="Click" ></asp:AsyncPostBackTrigger>
</triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-left: 17px">
                                    Cep Cobrança
                                </td>
                                <td colspan="3" style="padding-left: 17px">
                                    Endereço de Cobrança
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtCepCobranca" runat="server" CssClass="DesligarTextBox" Width="100px"></asp:TextBox>
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtEnderecoCobranca" runat="server" Width="726px" CssClass="DesligarTextBox"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel ID="tpTransportadora" runat="server" HeaderText="Transportadora/Volumes Transportados">
                    <ContentTemplate>
                        <asp:HiddenField ID="hdnCodTransportadora" runat="server" />
                        <table>
                            <tr>
                                <td style="padding-left: 20px;">
                                    Selecione:
                                </td>
                                <td colspan="5">
                                    <asp:DropDownList ID="ddlTransportadora" runat="server" Width="95%" OnSelectedIndexChanged="ddlTransportadora_SelectedIndexChanged"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-left: 20px;">
                                    Nome/Razão Social
                                </td>
                                <td style="padding-left: 20px;" colspan="2">
                                    Frete por Conta
                                </td>
                                <td style="padding-left: 20px;">
                                    Placa do Veículo
                                </td>
                                <td style="padding-left: 20px;">
                                    UF
                                </td>
                                <td style="padding-left: 20px; width: 191px;">
                                    CPF/CNPJ
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtTransportadora" runat="server" CssClass="DesligarTextBox"></asp:TextBox>
                                </td>
                                <td colspan="2">
                                    <asp:DropDownList ID="ddlFreteConta" runat="server">
                                        <asp:ListItem Value="0">0 - Por conta do emitente</asp:ListItem>
                                        <asp:ListItem Value="1">1 - Por conta do destinatário</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPlaca" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlUFTransportadora1" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 191px">
                                    <asp:TextBox ID="txtCNPJTransportadora" runat="server" CssClass="DesligarTextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-left: 20px;" colspan="2">
                                    Endereço
                                </td>
                                <td style="padding-left: 20px;" colspan="2">
                                    Município
                                </td>
                                <td style="padding-left: 20px;">
                                    UF
                                </td>
                                <td style="padding-left: 20px; width: 191px;">
                                    Inscrição Estadual
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:TextBox ID="txtEnderecoTransportadora" runat="server" Width="209px" CssClass="DesligarTextBox"></asp:TextBox>
                                </td>
                                <td colspan="2">
                                    <asp:TextBox ID="txtMunicipioTransportadora" runat="server" Width="219px" CssClass="DesligarTextBox"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlUFTransportadora2" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 191px">
                                    <asp:TextBox ID="txtInscricaoTransportadora" runat="server" CssClass="DesligarTextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-left: 20px;">
                                    Quantidade
                                </td>
                                <td style="padding-left: 20px;">
                                    Espécie
                                </td>
                                <td style="padding-left: 20px;">
                                    Marca
                                </td>
                                <td style="padding-left: 20px;">
                                    Número
                                </td>
                                <td style="padding-left: 20px;">
                                    Peso Bruto
                                </td>
                                <td style="padding-left: 20px; width: 191px;">
                                    Peso Liquido
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtQuantidade" runat="server" Width="144px" onkeypress="OnlyNumbers();"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEspecie" runat="server" Width="91px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtMarca" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNumero" runat="server" onkeypress="OnlyNumbers();"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtBruto" runat="server" onkeypress="OnlyMoney() ;" onBlur="ReplicarValor();"></asp:TextBox>
                                </td>
                                <td style="width: 191px">
                                    <asp:TextBox ID="txtLiquido" runat="server" onkeypress="OnlyMoney();"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-left: 20px">
                                    Bairro
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtBairroTransportadora" runat="server" CssClass="DesligarTextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel ID="tpAdicionais" runat="server" HeaderText="Dados Adicionais">
                    <ContentTemplate>
                        <table style="height: auto">
                            <tr>
                                <td>
                                    Observação
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="width: 122px">
                                    <asp:TextBox ID="txtObservacao" runat="server" Height="113px" TextMode="MultiLine"
                                        Width="862px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 122px; padding-left: 20px">
                                    Mensagens na NF
                                </td>
                                <td style="padding-left: 20px">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 122px">
                                    <asp:DropDownList ID="ddlMensagemNF" runat="server" Height="16px" Width="693px">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel ID="tbCalculoImposto" runat="server" HeaderText="Cálculo de Imposto">
                    <ContentTemplate>
                        <asp:UpdatePanel ID="updCalculoImposto" runat="server" UpdateMode="Conditional">
                            <contenttemplate>
                    <table>
                    <tr>
                    <td style="padding-left:20px; width: 284px;">Base de Cálculo ICMS</td>
                    <td style="padding-left:20px; width: 262px;">Valor do ICMS</td>
                    <td style="padding-left:20px">Base Cálc. ICMS Subst.</td>
                    <td style="padding-left:20px">Valor do ICMS Subst.</td>
                    </tr>
                    <tr>
                    <td style="width: 284px">
                        <asp:TextBox ID="txtBaseCalculo" runat="server" 
                            AutoPostBack="True" ontextchanged="campo_TextChanged" 
                            CssClass="DesligarTextBox"></asp:TextBox></td>
                    <td style="width: 262px">
                        <asp:TextBox ID="txtValorIcms" runat="server" 
                            AutoPostBack="True" ontextchanged="campo_TextChanged" 
                            CssClass="DesligarTextBox"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtBaseCalculoSub" runat="server" AutoPostBack="True" 
                            ontextchanged="campo_TextChanged" CssClass="DesligarTextBox"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtValorIcmsSub" runat="server" AutoPostBack="True" 
                            ontextchanged="campo_TextChanged" CssClass="DesligarTextBox"></asp:TextBox></td>
                    </tr>
                    <tr>
                    <td style="padding-left:20px; width: 284px;">Valor Total dos Produtos</td>
                    <td style="padding-left:20px; width: 262px;">Valor do Frete</td>
                    <td style="padding-left:20px">Valor do Seguro</td>
                    <td style="padding-left:20px">Valor Total de Desconto</td>
                    </tr>
                    <tr>
                    <td style="width: 284px">
                        <asp:TextBox ID="txtTotalProduto" runat="server" 
                            AutoPostBack="True" ontextchanged="campo_TextChanged" 
                            CssClass="DesligarTextBox"></asp:TextBox></td>
                    <td style="width: 262px"><asp:TextBox ID="txtValorFrete" runat="server" 
                            AutoPostBack="True" ontextchanged="campo_TextChanged" onkeypress="OnlyMoney();"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtValorSeguro" runat="server" AutoPostBack="True" 
                            ontextchanged="campo_TextChanged" onkeypress="OnlyMoney();"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtValorTotalDesconto" CssClass="DesligarTextBox" runat="server" AutoPostBack="True" 
                            onkeypress="OnlyMoney();"></asp:TextBox></td>
                    </tr>
                    <tr>
                    <td style="padding-left:20px; width: 284px;">Outras Despesas</td>
                    <td style="padding-left:20px; width: 262px;">Valor do IPI</td>
                    <td style="padding-left:20px">Valor Total da Nota</td>
                    </tr>
                    <tr>
                    <td style="width: 284px">
                        <asp:TextBox ID="txtOutrasDespesas" runat="server" 
                            AutoPostBack="True" ontextchanged="campo_TextChanged" onkeypress="OnlyMoney();"></asp:TextBox></td>
                    <td style="width: 262px">
                        <asp:TextBox ID="txtValorIPI" runat="server" 
                            AutoPostBack="True" ontextchanged="campo_TextChanged" 
                            CssClass="DesligarTextBox"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtValorTotalNota" runat="server" AutoPostBack="True" 
                            ontextchanged="campo_TextChanged" CssClass="DesligarTextBox" ></asp:TextBox></td>
                    </tr>
                    <tr><td></td></tr>
                    <tr><td></td></tr>
                    <tr><td></td></tr>
                    <tr><td></td></tr>
                    <tr><td></td></tr>
                    <tr><td></td></tr>
                    <tr><td></td></tr>
                    <tr><td></td></tr>
                    <tr><td></td></tr>
                    </table>
                    </contenttemplate>
                            <triggers>
                            <asp:AsyncPostBackTrigger ControlID="txtValorFrete" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="txtValorSeguro" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="txtOutrasDespesas" 
                                EventName="TextChanged" />
                        </triggers>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
            </ajaxToolkit:TabContainer>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Button ID="btnCancelar" CssClass="botao" runat="server" Text="Cancelar" Height="20px"
                Width="80px" OnClick="btnCancelar_Click" />
                    &nbsp; <asp:Button ID="btnVoltar" CssClass="botao" runat="server" 
                Text="Voltar" Height="20px" Width="80px" onclick="btnVoltar_Click" />
        </td>
        <td colspan="2" style="text-align: right">
            <asp:Button ID="btnSalvar" CssClass="botao" runat="server" Text="Salvar" Width="80px"
                OnClick="btnSalvar_Click" ValidationGroup="ValidaDados" />
            <asp:Button ID="btnCarregarValoresCFOP" runat="server" Text="" Width="99px" OnClick="btnCarregarValoresCFOP_Click" />
            <asp:Button ID="btnCarregarValores" runat="server" OnClick="btnCarregarValores_Click"
                Text="" Width="99px" />
        </td>
    </tr>
    </table> 
    </contenttemplate>
        <triggers>
        <asp:AsyncPostBackTrigger ControlID="btnCarregarValores" EventName="Click" />
            <asp:PostBackTrigger ControlID="btnSalvar" />
    </triggers>
    </asp:UpdatePanel>
    <div style="text-align: center">
        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
            ValidationGroup="ValidarFatura" />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
            ValidationGroup="ValidaDados"></asp:ValidationSummary>
    </div>
    <br />
</asp:Content>
