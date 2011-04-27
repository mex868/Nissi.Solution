<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="CadastraPedidoCompra.aspx.cs" Inherits="CadastrarPedidoCompra" EnableEventValidation="false" %>

<%@ Register Assembly="RDC.Tools" Namespace="RDC.Tools" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="server">
    <script language="javascript" type="text/javascript" src="../JScripts/pedido-compra.js"></script>
    <script type="text/javascript" language="javascript">
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
            loadTabs();
            OcultarBotaoCarregarValores();
            if ($get("<%=hdfShowItemInsumo.ClientID %>").value != "hiddenNo" && $get("<%=hdfShowItem.ClientID %>").value != "hiddenNo")
                hiddenItens();
        }

        function CarregarValores(source, eventArgs) {
            $get('<%=hdfIdRazaoSocial.ClientID%>').value = eventArgs.get_value();
            $get('<%=txtRazaoSocial.ClientID %>').value = eventArgs._item.outerText;
            $get('<%=btnCarregarValores.ClientID%>').click();
        }

        function CarregarValoresComprador(source, eventArgs) {
            $get('<%=hdfCodComprador.ClientID%>').value = eventArgs.get_value();
            $get('<%=txtComprador.ClientID %>').value = eventArgs._item.outerText;
        }

        function CarregarValoresAprovador(source, eventArgs) {
            $get('<%=hdfCodAprovador.ClientID%>').value = eventArgs.get_value();
            $get('<%=txtAprovador.ClientID %>').value = eventArgs._item.outerText;
        }

        function CarregarValoresNorma(source, eventArgs) {
            $get('<%=hdfCodMateriaPrima.ClientID%>').value = eventArgs.get_value();
            $get('<%=txtNorma.ClientID %>').value = eventArgs._item.outerText;
        }

        function CarregarValoresBitola(source, eventArgs) {
            $get('<%=hdfCodBitola.ClientID%>').value = eventArgs.get_value();
            $get('<%=txtBitola.ClientID %>').value = eventArgs._item.outerText;
        }

        function OcultarBotaoCarregarValores() {
            $get('<%=btnCarregarValores.ClientID%>').style.display = "none";
            $get('<%=btnAtualizarProdutoInsumo.ClientID%>').style.display = "none";
            $get('<%=btnAtualizarMateriaPrima.ClientID%>').style.display = "none";
            $get('<%=btnAtualizarBitola.ClientID%>').style.display = "none";
        }

        function ChamaPopupMateriaPrima() {
            WaitAsyncPostBack(true);
            window.showModalDialog('<%=caminhoAplicacao%>' + "MateriaPrima/CadastraMateriaPrima.aspx?popup=sim", "", "dialogHeight=600px;dialogWidth=950px;status=no,toolbar=no,menubar=no,location=no;unadorned=no;help=no; resizable: No; status: No; scroll:no;");
            WaitAsyncPostBack(false);
            $get('<%=btnAtualizarMateriaPrima.ClientID%>').click();
        }

        function ChamaPopupBitola() {
            WaitAsyncPostBack(true);
            window.showModalDialog('<%=caminhoAplicacao%>' + "MateriaPrima/CadastraBitola.aspx?popup=sim", "", "dialogHeight=150px;dialogWidth=900px;status=no,toolbar=no,menubar=no,location=no;unadorned=no;help=no; resizable: No; status: No; scroll:no;");
            WaitAsyncPostBack(false);
            $get('<%=btnAtualizarBitola.ClientID%>').click();
        }

        function ChamaPopupProdutoInsumo() {
            WaitAsyncPostBack(true);
            window.showModalDialog('<%=caminhoAplicacao%>' + "Produto/CadastraProdutoInsumo.aspx?popup=sim", "", "dialogHeight=200px;dialogWidth=900px;status=no,toolbar=no,menubar=no,location=no;unadorned=no;help=no; resizable: No; status: No; scroll:no;");
            WaitAsyncPostBack(false);
            $get('<%=btnAtualizarProdutoInsumo.ClientID%>').click();
        }

        function ChamaPopup() {
            var codigoFornecedor = $get("<%=hdfCodigoFornecedor.ClientID%>").value;
            if (codigoFornecedor == "") {
                alert("Escolha o Fornecedor antes de adicionar o produto!");
                return;
            }
            window.showModalDialog('<%=caminhoAplicacao%>' + "Produto/CadastraProduto.aspx?acao=IncluirItem&codigo=" + codigoFornecedor, "", "dialogHeight=600px;dialogWidth=900px;status=no,toolbar=no,menubar=no,location=no;unadorned=no;help=no; resizable: No; status: No;");
        }
    </script>
    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="updDados">
        <ContentTemplate>
            <asp:HiddenField ID="hdfCodigoFornecedor" runat="server" />
            <asp:HiddenField ID="hdfTipoAcao" runat="server" />
            <asp:HiddenField ID="hdfCodPedidoCompra" runat="server" />
            <table style="margin-left: auto; width: 75%; margin-right: auto;">
                <tr>
                    <td style="width: 21px; text-align: left">
                        <asp:Image ID="ImgCadastro" runat="server" ImageUrl="~/Imagens/layout.png" />
                    </td>
                    <td style="width: 95%; text-align: left" class="titulo">
                        Cadastro de Pedido de Compra
                    </td>
                </tr>
            </table>
            <table class="fundoTabela" style="text-align: left; width: 75%">
                <tr>
                    <td style="padding-left: 20px">
                    </td>
                    <td style="width: 639px">
                        Nº Pedido de Compra
                    </td>
                    <td style="padding-left: 20px; width: 591px">
                        Emissão
                    </td>
                    <td style="padding-left: 20px; width: 106px;">
                        Data da Entrega                    </td>
                    <td style="padding-left: 20px; width: 179px;">

                        Condições de Pagamento

                    </td>

                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td style="width: 639px">
                        <asp:TextBox ID="txtPedidoCompra" runat="server" Width="80px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmissao" runat="server" CssClass="dataEmissao" Width="106px"></asp:TextBox>
                        <ajaxToolkit:MaskedEditExtender ID="txtEmissao_MaskedEditExtender" runat="server"
                            AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="false" Mask="99/99/9999"
                            MaskType="Date" MessageValidatorTip="false" TargetControlID="txtEmissao" UserDateFormat="DayMonthYear">
                        </ajaxToolkit:MaskedEditExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtEmissao"
                            ErrorMessage="Data de Emissão falta ser preenchido." 
                            ValidationGroup="ValidaDados" CssClass="asterisco">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDataPrazoEntrega" runat="server" CssClass="dataPicker"></asp:TextBox>
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left"
                            DisplayMoney="Left" ErrorTooltipEnabled="false" Mask="99/99/9999" MaskType="Date"
                            MessageValidatorTip="false" TargetControlID="txtDataPrazoEntrega" UserDateFormat="DayMonthYear">
                        </ajaxToolkit:MaskedEditExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" 
                            ControlToValidate="txtDataPrazoEntrega" CssClass="asterisco" 
                            ErrorMessage="Campo Data da Entrega falta ser preenchido." 
                            ValidationGroup="ValidaDados">*</asp:RequiredFieldValidator>
                        </td>
                    <td>
                        <asp:DropDownList ID="ddlCondicoesPgto" runat="server">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" 
                            ControlToValidate="ddlCondicoesPgto" CssClass="asterisco" 
                            ErrorMessage="Campo Condições de Pagamento falta ser preenchido." 
                            ValidationGroup="ValidaDados">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="padding-left: 20px; width: 639px;">
                        &nbsp;</td>
                    <td style="padding-left: 20px">
                        Entrega
                    </td>
                    <td style="padding-left: 20px">
                        Comprador
                    </td>
                    <td style="padding-left: 20px; width: 106px;">
                        Aprovador
                    </td>
                    <td style="padding-left: 20px; width: 179px;">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 639px">
                        &nbsp;</td>
                    <td style="text-align: left">
                        <asp:DropDownList ID="ddlTipoRetirada" runat="server">
                            <asp:ListItem Value="0">0 - Fornecedor Entrega</asp:ListItem>
                            <asp:ListItem Value="1">1 - Retira</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:HiddenField ID="hdfCodComprador" runat="server" />
                        <asp:TextBox ID="txtComprador" runat="server"></asp:TextBox>
                        <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender6" runat="server" TargetControlID="txtComprador"
                            MinimumPrefixLength="1" ServiceMethod="GetFuncionario" CompletionInterval="800"
                            EnableCaching="true" CompletionSetCount="10" OnClientItemSelected="CarregarValoresComprador"
                            OnClientPopulated="ClientPopulated">
                        </ajaxToolkit:AutoCompleteExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" 
                            ControlToValidate="txtComprador" CssClass="asterisco" 
                            ErrorMessage="Campo Comprador falta ser preenchido." 
                            ValidationGroup="ValidaDados">*</asp:RequiredFieldValidator>
                    </td>
                    <td colspan="2">
                        <asp:HiddenField ID="hdfCodAprovador" runat="server" />
                        <asp:TextBox ID="txtAprovador" runat="server"></asp:TextBox>
                        <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender7" runat="server" TargetControlID="txtAprovador"
                            MinimumPrefixLength="1" ServiceMethod="GetFuncionario" CompletionInterval="800"
                            EnableCaching="true" CompletionSetCount="10" OnClientItemSelected="CarregarValoresAprovador"
                            OnClientPopulated="ClientPopulated">
                        </ajaxToolkit:AutoCompleteExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" 
                            ControlToValidate="txtAprovador" CssClass="asterisco" 
                            ErrorMessage="Campo Aprovador falta ser preenchido." 
                            ValidationGroup="ValidaDados">*</asp:RequiredFieldValidator>
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
                        <asp:HiddenField ID="hdfIdRazaoSocial" runat="server" />
                        <asp:TextBox ID="txtRazaoSocial" runat="server" Width="350px"></asp:TextBox>
                        <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtRazaoSocial"
                            MinimumPrefixLength="1" ServiceMethod="GetFornecedor" CompletionInterval="800"
                            EnableCaching="true" CompletionSetCount="10" OnClientItemSelected="CarregarValores"
                            OnClientPopulated="ClientPopulated">
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
                    <td style="padding-left: 20px">
                        UF
                    </td>
                    <td style="padding-left: 20px;">
                        Fone/Fax
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:TextBox ID="txtMunicipio" runat="server" Width="350px" TabIndex="14" CssClass="DesligarTextBox"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtUF" runat="server" Width="194px" TabIndex="16" CssClass="DesligarTextBox"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtFoneFax" runat="server" Width="164px" TabIndex="15" CssClass="DesligarTextBox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="padding-left: 20px;" colspan="3">
                        Contato
                    </td>
                    <td style="padding-left: 20px">
                        E-mail
                    </td>
                    <td style="padding-left: 20px;">
                        Site
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:TextBox ID="txtContato" runat="server" Width="350px" TabIndex="14" CssClass="DesligarTextBox"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server" Width="194px" TabIndex="16" CssClass="DesligarTextBox"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSite" runat="server" Width="164px" TabIndex="15" CssClass="DesligarTextBox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="padding-left: 20px;">
                        Condições de Fornecimento
                    </td>
                    <td colspan="3" style="padding-left: 20px;">
                        Observação
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="padding-left: 20px;">
                        <asp:TextBox ID="txtCondicoesFornecimento" runat="server" TextMode="MultiLine" Width="400px"></asp:TextBox>
                    </td>
                    <td colspan="3" style="padding-left: 20px;">
                        <asp:TextBox ID="txtObservacao" runat="server" TextMode="MultiLine" Width="375px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="5" style="padding-left: 20px">
                    <asp:HiddenField ID="hdfTipoPedido" runat="server" 
                                                            Value="0" 
                            onvaluechanged="hdfTipoPedido_ValueChanged" />
                    <div id="tabs">
                        <ul>
                        <li><a href="#div-content-material">Dados do Material</a></li>
                        <li><a href="#div-content-produto">Dados do Produto</a></li>
                        </ul>
                        <div id="div-content-material" style="Height:450px; Width:932px">
                                     <table style="height: 300; width: 920px">
                                        <tr>
                                            <td>

                                    <asp:UpdatePanel ID="updItens" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div id="cadastraritens" style="height: 120px; width: 920px">
                                                <table width="100%">
                                                    <tr>
                                                        <td style="padding-left: 17px">
                                                            Matéria Prima<asp:HiddenField ID="hdfCodMateriaPrima" runat="server" />
                                                        </td>
                                                        <td style="padding-left: 17px">
                                                            Bitola<asp:HiddenField ID="hdfCodBitola" runat="server" />
                                                        </td>
                                                        <td style="padding-left: 17px">
                                                            Resistencia Tração
                                                        </td>
                                                        <td style="padding-left: 17px">
                                                            Especificação
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtNorma" MaxLength="18" runat="server" Height="16px" Width="0px"
                                                                CssClass="textNovo" Visible="False"></asp:TextBox>
                                                            <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="txtNorma"
                                                                MinimumPrefixLength="1" ServiceMethod="GetNorma" CompletionInterval="800" OnClientItemSelected="CarregarValoresNorma"
                                                                OnClientPopulated="ClientPopulated" DelimiterCharacters="" Enabled="True" ServicePath="">
                                                            </ajaxToolkit:AutoCompleteExtender>
                                                            <asp:DropDownList ID="ddlMateriaPrima" runat="server">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtNorma"
                                                                ErrorMessage="Matéria Prima falta ser preenchido." ValidationGroup="ValidaDadosItens">*</asp:RequiredFieldValidator>
                                                            <img alt="" src="../Imagens/Incluir.png" onclick="ChamaPopupMateriaPrima();" title="Incluir Matéria Prima" style="width:18px; height:18px; cursor:hand"/>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtBitola" MaxLength="18" runat="server" Height="16px" Width="0px"
                                                                TabIndex="1" onkeypress="OnlyMoney();" CssClass="textNovo" Visible="False"></asp:TextBox>
                                                            <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" TargetControlID="txtBitola"
                                                                MinimumPrefixLength="1" ServiceMethod="GetBitola" CompletionInterval="800" OnClientItemSelected="CarregarValoresBitola"
                                                                OnClientPopulated="ClientPopulated" DelimiterCharacters="" Enabled="True" ServicePath="">
                                                            </ajaxToolkit:AutoCompleteExtender>
                                                            <asp:DropDownList ID="ddlBitola" runat="server" AutoPostBack="True" 
                                                                onselectedindexchanged="ddlBitola_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtBitola"
                                                                ErrorMessage="Bitola falta ser preenchido." ValidationGroup="ValidaDadosItens">*</asp:RequiredFieldValidator>
                                                            <img alt="" src="../Imagens/Incluir.png" onclick="ChamaPopupBitola();" title="Incluir Bitola" style="width:18px; height:18px; cursor:hand"/>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtResistenciaTracao" runat="server" Width="129px"  
                                                                onkeypress="OnlyMoney();"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtEspecificacao" runat="server" Width="90px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 17px">
                                                            Qtde
                                                        </td>
                                                        <td style="padding-left: 17px">
                                                            Unidade
                                                        </td>
                                                        <td style="padding-left: 17px">
                                                            IPI
                                                        </td>
                                                        <td style="padding-left: 17px">
                                                            Valor Unit.
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtQtde" MaxLength="18" runat="server" Height="16px" 
                                                                Width="60px" onkeypress="OnlyMoney();" CssClass="textNovo"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtQtde"
                                                                ErrorMessage="Qtde falta ser preenchido." ValidationGroup="ValidaDadosItens">*</asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlUnidade" runat="server">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtIPI" runat="server" Width="50px"  onkeypress="OnlyMoney();"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtValorUnit" MaxLength="18" runat="server" Height="16px" 
                                                                Width="100px" onkeypress="OnlyMoney();" CssClass="textNovo"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtValorUnit"
                                                                ErrorMessage="Valor Unit. falta ser preenchido." ValidationGroup="ValidaDadosItens">*</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" style="text-align: right">
                                                            <input type="button" id="Button1" value="Cancelar" style="height: 20px; width: 80px"
                                                                class="botao" onclick="showItens(); limparitens();" />
                                                            &nbsp;<asp:Button ID="btnIncluirItem" runat="server" Text="Incluir" Height="20px"
                                                                Width="80px" CssClass="botao" OnClick="btnSalvarItem_Click" ValidationGroup="ValidaDadosItens" />
                                                            <asp:Button ID="btnAtualizarMateriaPrima" runat="server" CssClass="botao" 
                                                                onclick="btnAtualizarMateriaPrima_Click" Width="0px" />
                                                            <asp:Button ID="btnAtualizarBitola" runat="server" CssClass="botao" 
                                                                 Width="0px" onclick="btnAtualizarBitola_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <br />
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="grdProduto" EventName="RowCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="ddlBitola" 
                                                EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="btnAtualizarMateriaPrima" 
                                                EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="btnAtualizarBitola" 
                                                EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <asp:UpdatePanel ID="updProduto" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:HiddenField ID="hdfTipoAcaoItemPedidoCompra" runat="server" Value="Incluir" />
                                                        <asp:HiddenField ID="hdfShowItem" runat="server" Value="hiddenYes" />
                                                        <cc1:RDCGrid ID="grdProduto" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                            BorderColor="Black" BorderWidth="1px" CellPadding="1" CellSpacing="3" CssClass="alinhamento"
                                                            GridLines="None" MultiSelection="True" OnPageIndexChanging="grdProduto_PageIndexChanging"
                                                            OnRowCommand="grdProduto_RowCommand" OnRowDataBound="grdProduto_RowDataBound"
                                                            ShowHeaderCheckBoxColumn="False" ShowOptionColumn="False" ShowPageDetails="True"
                                                            ShowFooter="True" Width="100%" EnableModelValidation="True">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Ações">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imgEditar" runat="server" Height="15px" ImageUrl="~/Imagens/editar.png"
                                                                            Width="15px" />
                                                                        <asp:ImageButton ID="imgExcluir" runat="server" Height="15px" ImageUrl="~/Imagens/exclusao_Canc.png"
                                                                            Width="15px" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle CssClass="headerGrid" />
                                                                    <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="Descrição dos Produtos">
                                                                    <HeaderStyle CssClass="headerGrid" />
                                                                    <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="Bitola">
                                                                    <HeaderStyle CssClass="headerGrid" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="Qtde.">
                                                                    <HeaderStyle CssClass="headerGrid" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="Unid.">
                                                                    <HeaderStyle CssClass="headerGrid" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="Rest. Tras.">
                                                                    <HeaderStyle CssClass="headerGrid" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="Especif.">
                                                                    <HeaderStyle CssClass="headerGrid" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="IPI">
                                                                    <HeaderStyle CssClass="headerGrid" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="Valor Unit.">
                                                                    <HeaderStyle CssClass="headerGrid" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="Valor Total">
                                                                    <HeaderStyle CssClass="headerGrid" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                        </cc1:RDCGrid>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="grdProduto" EventName="RowCommand"></asp:AsyncPostBackTrigger>
                                                        <asp:AsyncPostBackTrigger ControlID="btnIncluirItem" EventName="Click" />
                                                        <asp:AsyncPostBackTrigger ControlID="btnAtualizarMateriaPrima" 
                                                            EventName="Click" />
                                                        <asp:AsyncPostBackTrigger ControlID="btnAtualizarBitola" 
                                                            EventName="Click" />
                                                <asp:AsyncPostBackTrigger ControlID="ddlBitola" 
                                                    EventName="SelectedIndexChanged" />
                                                </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                <input type="button" id="btnNovoItem" value="Novo" style="height: 20px; width: 80px"
                                                    class="botao" onclick="showItens()" />
                                            </td>
                                        </tr>
                                    </table>                       
                        </div>
                        <div id="div-content-produto" style="Height:450px; Width:932px">
                                     <table style="height: 300; width: 920px">
                                        <tr>
                                            <td>
                                        <asp:UpdatePanel ID="updItensInsumo" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div id="cadastraritensInsumo" style="height: 120px; width: 920px">
                                                <table width="100%">
                                                    <tr>
                                                        <td style="padding-left: 17px">
                                                            Produto</td>
                                                        <td style="padding-left: 17px">
                                                            Qtde</td>
                                                        <td style="padding-left: 17px">
                                                            Unidade</td>
                                                        <td style="padding-left: 17px">
                                                            IPI</td>
                                                        <td style="padding-left: 17px">
                                                            Valor Unit. </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:DropDownList ID="ddlProdutoInsumo" runat="server">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlProdutoInsumo"
                                                                ErrorMessage="Produto falta ser preenchido." ValidationGroup="ValidaDadosItensInsumo">*</asp:RequiredFieldValidator>
                                                            <img alt="" src="../Imagens/Incluir.png" onclick="ChamaPopupProdutoInsumo();" title="Incluir Produto Insumo" style="width:18px; height:18px; cursor:hand"/>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtQtdInsumo" runat="server" CssClass="textNovo" Height="16px" 
                                                                MaxLength="18" onkeypress="OnlyMoney();" Width="60px"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                                                ControlToValidate="txtQtdInsumo" ErrorMessage="Qtde falta ser preenchido." 
                                                                ValidationGroup="ValidaDadosItensInsumo">*</asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlUnidadeInsumo" runat="server">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtIpiInsumo" runat="server" onkeypress="OnlyMoney();" Width="50px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtValorInsumo" runat="server" CssClass="textNovo" Height="16px" 
                                                                MaxLength="18" onkeypress="OnlyMoney();" Width="100px"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                                                                ControlToValidate="txtValorInsumo" 
                                                                ErrorMessage="Valor Unit. falta ser preenchido." 
                                                                ValidationGroup="ValidaDadosItensInsumo">*</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="5" style="text-align: right">
                                                            <input type="button" id="btnCancelarInsumo" value="Cancelar" style="height: 20px; width: 80px"
                                                                class="botao" onclick="showItensInsumo(); limparitens();" />
                                                            &nbsp;<asp:Button ID="btnSalvarInsumo" runat="server" Text="Incluir" Height="20px"
                                                                Width="80px" CssClass="botao" OnClick="btnSalvarInsumo_Click" 
                                                                ValidationGroup="ValidaDadosItensInsumo" />
                                                            <asp:Button ID="btnAtualizarProdutoInsumo" runat="server" CssClass="botao" 
                                                                onclick="btnAtualizarProdutoInsumo_Click" Width="0px" 
                                                                 />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <br />
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="grdProdutoInsumo" EventName="RowCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="ddlProdutoInsumo" 
                                                EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="btnAtualizarProdutoInsumo" 
                                                EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                                <asp:UpdatePanel ID="updProdutoInsumo" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:HiddenField ID="hdfTipoAcaoItemPedidoCompraInsumo" runat="server" 
                                                            Value="Incluir" />
                                                        <asp:HiddenField ID="hdfShowItemInsumo" runat="server" Value="hiddenYes" />
                                                        <cc1:RDCGrid ID="grdProdutoInsumo" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                            BorderColor="Black" BorderWidth="1px" CellPadding="1" CellSpacing="3" CssClass="alinhamento"
                                                            GridLines="None" MultiSelection="True" OnPageIndexChanging="grdProdutoInsumo_PageIndexChanging"
                                                            OnRowCommand="grdProdutoInsumo_RowCommand" OnRowDataBound="grdProdutoInsumo_RowDataBound"
                                                            ShowHeaderCheckBoxColumn="False" ShowOptionColumn="False" ShowPageDetails="True"
                                                            ShowFooter="True" Width="100%" EnableModelValidation="True">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Ações">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imgEditar" runat="server" Height="15px" ImageUrl="~/Imagens/editar.png"
                                                                            Width="15px" />
                                                                        <asp:ImageButton ID="imgExcluir" runat="server" Height="15px" ImageUrl="~/Imagens/exclusao_Canc.png"
                                                                            Width="15px" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle CssClass="headerGrid" />
                                                                    <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="Código">
                                                                    <HeaderStyle CssClass="headerGrid" />
                                                                    <ItemStyle HorizontalAlign="Left" Width="8%" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="Descrição dos Produtos">
                                                                    <HeaderStyle CssClass="headerGrid" />
                                                                    <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="Qtde.">
                                                                    <HeaderStyle CssClass="headerGrid" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="Unid.">
                                                                    <HeaderStyle CssClass="headerGrid" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="IPI">
                                                                    <HeaderStyle CssClass="headerGrid" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="Valor Unit.">
                                                                    <HeaderStyle CssClass="headerGrid" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="Valor Total">
                                                                    <HeaderStyle CssClass="headerGrid" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                        </cc1:RDCGrid>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="grdProdutoInsumo" EventName="RowCommand"></asp:AsyncPostBackTrigger>
                                                        <asp:AsyncPostBackTrigger ControlID="btnSalvarInsumo" EventName="Click" />
                                                        <asp:AsyncPostBackTrigger ControlID="btnAtualizarProdutoInsumo" 
                                                            EventName="Click" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                <input type="button" id="btnNovoInsumo" value="Novo" style="height: 20px; width: 80px"
                                                    class="botao" onclick="showItensInsumo()" />
                                            </td>
                                        </tr>
                                    </table>                       
                        </div>
                    </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnCancelar" CssClass="botao" runat="server" Text="Cancelar" Height="20px"
                            Width="80px" OnClick="btnCancelar_Click" />
                        &nbsp;
                        <asp:Button ID="btnVoltar" CssClass="botao" runat="server" Text="Voltar" Height="20px"
                            Width="80px" OnClick="btnVoltar_Click" />
                    </td>
                    <td colspan="3" style="text-align: right">
                        &nbsp;<asp:Button ID="btnSalvar" CssClass="botao" runat="server" Text="Salvar" Width="80px"
                            OnClick="btnSalvar_Click" ValidationGroup="ValidaDados" />
                        <asp:Button ID="btnCarregarValores" runat="server" Text="" Width="39px" OnClick="btnCarregarValores_Click" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnCarregarValores" EventName="Click" />
            <asp:PostBackTrigger ControlID="btnSalvar" />
        </Triggers>
    </asp:UpdatePanel>
    <div style="text-align: center">
        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
            ValidationGroup="ValidaDadosItens" />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
            ValidationGroup="ValidaDados"></asp:ValidationSummary>
    </div>
    <br />
    <script language="jscript" type="text/javascript">
        OcultarBotaoCarregarValores();
    </script>
</asp:Content>
