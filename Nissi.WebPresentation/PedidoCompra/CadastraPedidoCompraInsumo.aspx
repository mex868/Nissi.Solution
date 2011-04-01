<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="CadastraPedidoCompraInsumo.aspx.cs" Inherits="CadastrarPedidoCompraInsumo" EnableEventValidation="false" %>

<%@ Register Assembly="RDC.Tools" Namespace="RDC.Tools" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="server">
    <script language="javascript" type="text/javascript" src="../JScripts/pedido-compra.js"></script>
    <script type="text/javascript" language="javascript">
        //--------------------------------------------------------------------------------
        //Criado por...: Alexandre Maximiano - 02/11/2009
        //Objetivo.....: Cabe�alho padr�o da p�gina
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
            //$("#cadastraritens").hide();
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


        function OcultarBotaoCarregarValores() {
            $get('<%=btnCarregarValores.ClientID%>').style.display = "none";
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
            <asp:HiddenField ID="hdfCodPedidoCompraInsumo" runat="server" />
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
                        N� Pedido de Compra
                    </td>
                    <td style="padding-left: 20px; width: 591px">
                        Emiss�o
                    </td>
                    <td style="padding-left: 20px; width: 106px;">
                        Data da Entrega                    </td>
                    <td style="padding-left: 20px; width: 179px;">

                        Condi��es de Pagamento

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
                            ErrorMessage="Data de Emiss�o falta ser preenchida." ValidationGroup="ValidaDados">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDataPrazoEntrega" runat="server" CssClass="dataPicker"></asp:TextBox>
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left"
                            DisplayMoney="Left" ErrorTooltipEnabled="false" Mask="99/99/9999" MaskType="Date"
                            MessageValidatorTip="false" TargetControlID="txtDataPrazoEntrega" UserDateFormat="DayMonthYear">
                        </ajaxToolkit:MaskedEditExtender>
                        </td>
                    <td>

                        <asp:DropDownList ID="ddlCondicoesPgto" runat="server">
                        </asp:DropDownList>

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
                        <asp:DropDownList ID="ddlTipoRetirada" runat="server" TabIndex="4">
                            <asp:ListItem Value="1">1 - Retira</asp:ListItem>
                            <asp:ListItem Value="0">0 - Fornecedor Entrega</asp:ListItem>
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
                    </td>
                    <td colspan="2">
                        <asp:HiddenField ID="hdfCodAprovador" runat="server" />
                        <asp:TextBox ID="txtAprovador" runat="server"></asp:TextBox>
                        <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender7" runat="server" TargetControlID="txtAprovador"
                            MinimumPrefixLength="1" ServiceMethod="GetFuncionario" CompletionInterval="800"
                            EnableCaching="true" CompletionSetCount="10" OnClientItemSelected="CarregarValoresAprovador"
                            OnClientPopulated="ClientPopulated">
                        </ajaxToolkit:AutoCompleteExtender>
                    </td>
                </tr>
                <tr>
                    <td colspan="5" style="padding-left: 20px">
                        Destinat�rio/Remetente
                    </td>
                </tr>
                <tr>
                    <td style="padding-left: 20px;" colspan="3">
                        Nome/Raz�o Social
                    </td>
                    <td style="padding-left: 20px;">
                        CNPJ/CPF
                    </td>
                    <td style="padding-left: 20px">
                        Inscri��o Estadual
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
                        Endere�o
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
                        Munic�pio
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
                        Condi��es de Fornecimento
                    </td>
                    <td colspan="3" style="padding-left: 20px;">
                        Observa��o
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
                        <ajaxToolkit:TabContainer ID="TabContainer1" CssClass="TabPanelImportar" runat="server"
                            ActiveTabIndex="0" Height="450px" Style="margin-bottom: 30px" Width="932px" AutoPostBack="true">
                            <ajaxToolkit:TabPanel ID="tpProduto" runat="server" HeaderText="Dados do Produto">
                                <HeaderTemplate>
                                    Dados do Produto
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <asp:UpdatePanel ID="updItens" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div id="cadastraritens" style="height: 120px; width: 920px">
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
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtNorma"
                                                                ErrorMessage="Mat�ria Prima falta ser preenchido." ValidationGroup="ValidaDadosItens">*</asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtQtdInsumo" runat="server" CssClass="textNovo" Height="16px" 
                                                                MaxLength="18" onkeypress="OnlyMoney();" TabIndex="2" Width="60px"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                                                                ControlToValidate="txtQtde" ErrorMessage="Qtde falta ser preenchido." 
                                                                ValidationGroup="ValidaDadosItens">*</asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlUnidadeInsumo" runat="server">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtIPIInsumo" runat="server" onkeypress="OnlyMoney();" Width="50px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtValorInsumo" runat="server" CssClass="textNovo" Height="16px" 
                                                                MaxLength="18" onkeypress="OnlyMoney();" TabIndex="3" Width="100px"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                                                                ControlToValidate="txtValorUnit" 
                                                                ErrorMessage="Valor Unit. falta ser preenchido." 
                                                                ValidationGroup="ValidaDadosItens">*</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="5" style="text-align: right">
                                                            <input type="button" id="btnCancelarInsumo" value="Cancelar" style="height: 20px; width: 80px"
                                                                class="botao" onclick="showItens(); limparitens();" />
                                                            &nbsp;<asp:Button ID="btnSalvarInsumo" runat="server" Text="Incluir" Height="20px"
                                                                Width="80px" CssClass="botao" OnClick="btnSalvarItem_Click" ValidationGroup="ValidaDadosItens" />
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
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <table style="height: 300; width: 920px">
                                        <tr>
                                            <td>
                                                <asp:UpdatePanel ID="updProduto" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:HiddenField ID="hdfTipoAcaoItemPedidoCompraInsumo" runat="server" 
                                                            Value="Incluir" />
                                                        <cc1:RDCGrid ID="grdProduto" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                            BorderColor="Black" BorderWidth="1px" CellPadding="1" CellSpacing="3" CssClass="alinhamento"
                                                            GridLines="None" MultiSelection="True" OnPageIndexChanging="grdProduto_PageIndexChanging"
                                                            OnRowCommand="grdProduto_RowCommand" OnRowDataBound="grdProduto_RowDataBound"
                                                            ShowHeaderCheckBoxColumn="False" ShowOptionColumn="False" ShowPageDetails="True"
                                                            ShowFooter="True" Width="100%" EnableModelValidation="True">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="A��es">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imgEditar" runat="server" Height="15px" ImageUrl="~/Imagens/editar.png"
                                                                            Width="15px" />
                                                                        <asp:ImageButton ID="imgExcluir" runat="server" Height="15px" ImageUrl="~/Imagens/exclusao_Canc.png"
                                                                            Width="15px" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle CssClass="headerGrid" />
                                                                    <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="C�digo">
                                                                    <HeaderStyle CssClass="headerGrid" />
                                                                    <ItemStyle HorizontalAlign="Left" Width="8%" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="Descri��o dos Produtos">
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
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                <input type="button" id="btnNovoInsumo" value="Novo" style="height: 20px; width: 80px"
                                                    class="botao" onclick="showItens()" />
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </ajaxToolkit:TabPanel>

                        </ajaxToolkit:TabContainer>
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
                        <asp:Button ID="btnSalvar" CssClass="botao" runat="server" Text="Salvar" Width="80px"
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
</asp:Content>
