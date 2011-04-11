<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="CadastraEntradaEstoque.aspx.cs" Inherits="CadastraEntradaEstoque"
    EnableEventValidation="false" %>
<%@ Register Assembly="RDC.Tools" Namespace="RDC.Tools" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="server">
    <script language="javascript" type="text/javascript" src="../JScripts/entrada-estoque.js"></script>
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
            hiddenItens();
        }

        function CarregarValores(source, eventArgs) {
            $get('<%=hdfIdRazaoSocial.ClientID%>').value = eventArgs.get_value();
            $get('<%=txtRazaoSocial.ClientID %>').value = eventArgs._item.outerText;
            $get('<%=btnCarregarValores.ClientID%>').click();
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
        }

        function ChamaPopup() {
            var codigoFornecedor = $get("<%=hdfCodigoFornecedor.ClientID%>").value;
            if (codigoFornecedor == "") {
                alert("Escolha o Fornecedor antes de adicionar o produto!");
                return;
            }
            window.showModalDialog('<%=caminhoAplicacao%>' + "Produto/CadastraProduto.aspx?acao=IncluirItem&codigo=" + codigoFornecedor, "", "dialogHeight=600px;dialogWidth=900px;status=no,toolbar=no,menubar=no,location=no;unadorned=no;help=no; resizable: No; status: No;");
        }

        function SetZIndex(control, args) {

            // Set auto complete extender control's z-index to a high value 

            // so it will appear on top of, not under, the ModalPopUp extended panel.

            control._completionListElement.style.zIndex = 99999999;

        }

        function ValidaArquivoImagem(source, args) {
            if ($get('<%=upFileUp.ClientID %>').value == '') {
                alert('Informe um arquivo de imagem válido');
                return false;
            }
            else
                return true;
        }
        //--------------------------------------------------------------------------------
        //Criado por...: Alexandre Maximiano - 02/11/2009
        //Objetivo.....: Acionar botão acessar quando pressionada a tecla ENTER
        //--------------------------------------------------------------------------------
        function KeyDownHandler() {
            if (event.keyCode == 13) {
                event.returnValue = false;
                event.cancel = true;
                $get('<%=btnCarregaValoresPedidoCompra.ClientID%>').click();
            }
        }

    </script>
    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="updDados">
        <ContentTemplate>
            <asp:HiddenField ID="hdfCodigoFornecedor" runat="server" />
            <asp:HiddenField ID="hdfTipoAcao" runat="server" />
            <asp:HiddenField ID="hdfCodEntradaEstoque" runat="server" />
            <table style="margin-left: auto; width: 75%; margin-right: auto;">
                <tr>
                    <td style="width: 21px; text-align: left">
                        <asp:Image ID="ImgCadastro" runat="server" ImageUrl="~/Imagens/layout.png" />
                    </td>
                    <td style="width: 95%; text-align: left" class="titulo">
                        Cadastro de Entrada de Estoque</td>
                </tr>
            </table>
            <table class="fundoTabela" style="text-align: left; width: 75%">
                <tr>
                    <td style="padding-left: 20px; width: 656px">
                        Nº Pedido de Compra
                    </td>
                    <td style="padding-left: 20px; width: 591px; display:none">
                        Nº Entrada de Estoque
                    </td>
                    <td style="padding-left: 20px; width: 106px;">
                        Emissão
                    </td>
                    <td style="padding-left: 20px; width: 179px;">
                        Entrega
                    </td>
                    <td style="padding-left: 20px">
                        &nbsp;Nota Fiscal
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:HiddenField ID="hdfPedidoCompra" runat="server" />
                        <asp:TextBox ID="txtPedidoCompra" runat="server" onkeypress="KeyDownHandler();return digitos(event, this);" Width="60px"></asp:TextBox>
                        <asp:Button ID="btnCarregaValoresPedidoCompra" runat="server" Width="75px" 
                            CssClass="botao" Text="Importar" 
                            onclick="btnCarregaValoresPedidoCompra_Click" />
                    </td>
                    <td style="display:none">
                        <asp:TextBox ID="txtEntradaEstoque" runat="server" Width="80px" CssClass="DesligarTextBox"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmissao" runat="server" Width="106px" CssClass="dataEmissao"></asp:TextBox>
                        <ajaxToolkit:MaskedEditExtender ID="mskDataEmissao" runat="server" AcceptNegative="Left"
                            DisplayMoney="Left" ErrorTooltipEnabled="false" Mask="99/99/9999" MaskType="Date"
                            MessageValidatorTip="false" TargetControlID="txtEmissao" UserDateFormat="DayMonthYear">
                        </ajaxToolkit:MaskedEditExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEmissao"
                            ErrorMessage="Data de Emissão falta ser preenchida." ValidationGroup="ValidaDados">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEntrega" runat="server" CssClass="dataEmissao" Width="106px"></asp:TextBox>
                        <ajaxToolkit:MaskedEditExtender ID="txtEntrega_MaskedEditExtender" runat="server"
                            AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="false" Mask="99/99/9999"
                            MaskType="Date" MessageValidatorTip="false" TargetControlID="txtEntrega" UserDateFormat="DayMonthYear">
                        </ajaxToolkit:MaskedEditExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtEntrega"
                            ErrorMessage="Data de Emissão falta ser preenchida." ValidationGroup="ValidaDados">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNotaFiscal" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                            ControlToValidate="txtNotaFiscal" 
                            ErrorMessage="Nota Fiscal falta ser preenchida." 
                            ValidationGroup="ValidaDados">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="padding-left: 20px">Data Nota Fiscal</td>
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
                        Especificações
                    </td>
                </tr>
                <tr>
                    <td><asp:TextBox ID="txtDataNotaFiscal" runat="server" CssClass="dataEmissao"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" 
                            ControlToValidate="txtDataNotaFiscal" 
                            ErrorMessage="Data da Nota Fiscal falta ser preenchida." 
                            ValidationGroup="ValidaDados">*</asp:RequiredFieldValidator>
                    </td>
                    <td style="text-align: left">
                        <asp:DropDownList ID="ddlTipoRetirada" runat="server" TabIndex="4" CssClass="DesligarTextBox">
                            <asp:ListItem Value="1">1 - Retira</asp:ListItem>
                            <asp:ListItem Value="0">0 - Fornecedor Entrega</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:TextBox ID="txtComprador" runat="server" CssClass="DesligarTextBox"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtAprovador" runat="server" CssClass="DesligarTextBox"></asp:TextBox>
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtEspecificacoes" runat="server" CssClass="DesligarTextBox"></asp:TextBox>
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
                        <asp:TextBox ID="txtCondicoesFornecimento" runat="server" TextMode="MultiLine" Width="400px"
                            CssClass="DesligarTextBox"></asp:TextBox>
                    </td>
                    <td colspan="3" style="padding-left: 20px;">
                        <asp:TextBox ID="txtObservacao" runat="server" TextMode="MultiLine" Width="375px"
                            CssClass="DesligarTextBox"></asp:TextBox>
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
                                                <asp:UpdatePanel ID="updProduto" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
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
                                                                <asp:BoundField HeaderText="Lote">
                                                                <HeaderStyle CssClass="headerGrid" Width="6%" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="Descrição dos Produtos">
                                                                    <HeaderStyle CssClass="headerGrid" />
                                                                    <ItemStyle HorizontalAlign="Left" Width="28%" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="Bitola">
                                                                    <HeaderStyle CssClass="headerGrid" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="Qtde.">
                                                                    <HeaderStyle CssClass="headerGrid" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="Qtde Entregue">
                                                                    <HeaderStyle CssClass="headerGrid" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="Unid.">
                                                                    <HeaderStyle CssClass="headerGrid" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="Rest. Tras.">
                                                                    <HeaderStyle CssClass="headerGrid" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="Especifi.">
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
                                                        <asp:AsyncPostBackTrigger ControlID="btnIncluirItem" EventName="Click" />
                                                        <asp:AsyncPostBackTrigger ControlID="grdProduto" EventName="RowCommand" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left">
                                               <asp:UpdatePanel ID="updBotoes" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                <asp:Button ID="btnNovoItem" runat="server" Text="Novo" CssClass="botao" 
                                                    Width="80px" onclick="btnNovoItem_Click" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                    <div id="div-content-produto" style="Height:450px; Width:932px;">
                                    <table style="height: 300; width: 920px">
                                        <tr>
                                            <td>
                                   <asp:UpdatePanel ID="updItensInsumo" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div id="cadastraritensInsumo" style="height: 120px; width: 920px">
                                                <table width="100%">
                                                    <tr>
                                                    <td style="padding-left: 17px">
                                                    Lote</td>
                                                        <td style="padding-left: 17px">
                                                            Produto</td>
                                                        <td style="padding-left: 17px">
                                                            Qtde</td>
                                                        <td style="padding-left: 17px">
                                                            Qtde Entregue</td>
                                                        <td style="padding-left: 17px">
                                                            Unidade</td>
                                                        <td style="padding-left: 17px">
                                                            IPI</td>
                                                        <td style="padding-left: 17px">
                                                            Valor Unit. </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtLoteInsumo" runat="server" CssClass="textNovo" Height="16px" 
                                                                MaxLength="18" onkeypress="OnlyMoney();" Width="60px"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" 
                                                                ControlToValidate="txtLoteInsumo" ErrorMessage="Qtde falta ser preenchido." 
                                                                ValidationGroup="ValidaDadosItensInsumo">*</asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlProdutoInsumo" runat="server" CssClass="textNovo">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlProdutoInsumo"
                                                                ErrorMessage="Produto falta ser preenchido." ValidationGroup="ValidaDadosItensInsumo">*</asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtQtdInsumo" runat="server" CssClass="DesligarTextBox textNovo" Height="16px" 
                                                                MaxLength="18" onkeypress="OnlyMoney();"  Width="60px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtQtdEntregueInsumo" runat="server" CssClass="textNovo" Height="16px" 
                                                                MaxLength="18" onkeypress="OnlyMoney();" Width="60px"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                                                                ControlToValidate="txtQtdEntregueInsumo" ErrorMessage="Qtde falta ser preenchido." 
                                                                ValidationGroup="ValidaDadosItensInsumo">*</asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlUnidadeInsumo" runat="server" CssClass="textNovo">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtIpiInsumo" runat="server" CssClass="textNovo" onkeypress="OnlyMoney();" Width="50px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtValorInsumo" runat="server" CssClass="textNovo" Height="16px" 
                                                                MaxLength="18" onkeypress="OnlyMoney();" Width="100px"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                                                                ControlToValidate="txtValorInsumo" 
                                                                ErrorMessage="Valor Unit. falta ser preenchido." 
                                                                ValidationGroup="ValidaDadosItensInsumo">*</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="7" style="text-align: right">
                                                            <input type="button" id="btnCancelarInsumo" value="Cancelar" style="height: 20px; width: 80px"
                                                                class="botao" onclick="showItensInsumo(); limparitens();" />
                                                            &nbsp;<asp:Button ID="btnSalvarInsumo" runat="server" Text="Incluir" Height="20px"
                                                                Width="80px" CssClass="botao" OnClick="btnSalvarInsumo_Click" 
                                                                ValidationGroup="ValidaDadosItensInsumo" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <br />
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="grdProdutoInsumo" EventName="RowCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="btnSalvarInsumo" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                                <asp:UpdatePanel ID="updProdutoInsumo" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:HiddenField ID="hdfTipoAcaoItemPedidoCompraInsumo" runat="server" 
                                                            Value="Incluir" />
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
                                                                <asp:BoundField HeaderText="Lote">
                                                                <HeaderStyle CssClass="headerGrid" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="Descrição dos Produtos">
                                                                    <HeaderStyle CssClass="headerGrid" />
                                                                    <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="Qtde.">
                                                                    <HeaderStyle CssClass="headerGrid" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="Qtde Entregue">
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
                        <asp:Button ID="btnSalvar" CssClass="botao" runat="server" Text="Salvar" Width="80px"
                            OnClick="btnSalvar_Click" ValidationGroup="ValidaDados" />
                        <asp:Button ID="btnCarregarValores" runat="server" Text="" Width="0px" 
                            OnClick="btnCarregarValores_Click" />
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
            ValidationGroup="ValidaDadosItens" 
            HeaderText="Os seguintes erros foram encontrados:" />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
            ValidationGroup="ValidaDados" 
            HeaderText="Os seguintes erros foram encontrados:"></asp:ValidationSummary>
    </div>
    <asp:HiddenField ID="hdfTargetIncluirItem" runat="server" Value="Incluir" />
    <ajaxToolkit:ModalPopupExtender ID="mpeIncluirItem" runat="server" PopupControlID="pnlIncluirItem"
        TargetControlID="hdfTargetIncluirItem" BehaviorID="mpeIncluirItemID" BackgroundCssClass="modalBackground"
        DropShadow="true" />
    <asp:Panel ID="pnlIncluirItem" runat="server">
        <asp:UpdatePanel runat="server" ID="updCadastroItem" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:HiddenField runat="server" ID="hdfTipoAcaoItem" />
                <div style="text-align: center; width: 950px; height: auto; padding: 5px 5px 5px 5px;
                    background-color: #ffffff;">
                    <table class="fundoTabela">
                        <!--TÍTULO DA POPUP-->
                        <tr>
                            <td class="titulo">
                                <b>
                                    <% if (hdfTipoAcaoItem.Value == "IncluirItem")
                                       { %>
                                    ::Inclusão de Item de Entrada
                                    <% } %>
                                    <% else
                                        { %>
                                    ::Alteração de Item de Entrada
                                    <% } %>
                                </b>
                            </td>
                        </tr>
                    </table>
                    <br style="line-height: 5px" />
                    <table width="100%" align="center" class="fundoTabela">
                        <tr>
                            <td style="padding-left: 20px;">
                                Lote
                            </td>
                            <td style="padding-left: 20px;">
                                Entrega
                            </td>
                            <td style="padding-left: 20px;" colspan="2">
                                Norma
                                <asp:HiddenField ID="hdfCodMateriaPrima" runat="server" />
                            </td>
                            <td style="padding-left: 20px;" colspan="2">
                                Bitola
                                <asp:HiddenField ID="hdfCodBitola" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtLote" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtData" runat="server" CssClass="DesligarTextBox"></asp:TextBox>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtNorma" runat="server" Width="200px" Visible="False"></asp:TextBox>
                                <asp:DropDownList ID="ddlMateriaPrima" runat="server" 
                                    onselectedindexchanged="ddlMateriaPrima_SelectedIndexChanged" 
                                    AutoPostBack="True">
                                </asp:DropDownList>
                                <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="txtNorma"
                                MinimumPrefixLength="1" ServiceMethod="GetNorma" CompletionInterval="800" 
                                        OnClientItemSelected="CarregarValoresNorma" OnClientPopulated="ClientPopulated" 
                                        DelimiterCharacters="" Enabled="True" ServicePath="" OnClientShown="SetZIndex">
                                </ajaxToolkit:AutoCompleteExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                        ControlToValidate="txtNorma" 
                                        ErrorMessage="Matéria Prima falta ser preenchido." 
                                        ValidationGroup="ValidaDadosItens">*</asp:RequiredFieldValidator>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtBitola" runat="server" Visible="False" Width="16px"></asp:TextBox>
                                <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" TargetControlID="txtBitola"
                                MinimumPrefixLength="1" ServiceMethod="GetBitola" CompletionInterval="800" 
                                        OnClientItemSelected="CarregarValoresBitola" OnClientPopulated="ClientPopulated" 
                                        DelimiterCharacters="" Enabled="True" ServicePath="" OnClientShown="SetZIndex">
                                </ajaxToolkit:AutoCompleteExtender>
                                    <asp:DropDownList ID="ddlBitola" runat="server" 
                                    onselectedindexchanged="ddlBitola_SelectedIndexChanged" 
                                    AutoPostBack="True">
                                </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                        ControlToValidate="txtBitola" 
                                        ErrorMessage="Bitola falta ser preenchido." 
                                        ValidationGroup="ValidaDadosItens">*</asp:RequiredFieldValidator>
                                <span id="spanBi" title="" class="asterisco" runat="server" style="display:none">*</span>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-left: 20px;">
                                Nº Pedido de Compra
                            </td>
                            <td style="padding-left: 20px;" colspan="5">
                                Fornecedor
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtPedidoCompraItem" runat="server" CssClass="DesligarTextBox"></asp:TextBox>
                            </td>
                            <td colspan="5">
                                <asp:TextBox ID="txtFornecedor" runat="server" Width="95%" CssClass="DesligarTextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                        <td style="padding-left:20px">Nº Certificado</td>
                        <td style="padding-left:20px">Nota Fiscal</td>
                        <td style="padding-left:20px">Data Nota Fiscal</td>
                            <td style="padding-left:20px" colspan="3">
                                Especificação</td>
                        </tr>
                        <tr>
                        <td><asp:TextBox ID="txtCertificado" runat="server"></asp:TextBox></td>
                        <td><asp:TextBox ID="txtNotaFiscalItem" runat="server" CssClass="DesligarTextBox"></asp:TextBox></td>
                        <td><asp:TextBox ID="txtDataNotaFiscalItem" runat="server" CssClass="DesligarTextBox"></asp:TextBox></td>
                            <td colspan="3">
                                <asp:TextBox ID="txtEspecificacao" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                        <td style="padding-left:20px">Corrida</td>
                        <td style="padding-left:20px">Qtde/Kilo</td>
                        <td style="padding-left:20px">Qtde/Kilo Entregue</td>
                        <td style="padding-left:20px">Unidade</td>
                            <td style="padding-left:20px">
                                IPI</td>
                            <td style="padding-left:20px">
                                Valor</td>
                        </tr>
                        <tr>
                        <td><asp:TextBox ID="txtCorrida" runat="server"></asp:TextBox></td>
                        <td><asp:TextBox ID="txtQtdePedidoCompra" runat="server" CssClass="DesligarTextBox"></asp:TextBox>
                            </td>
                        <td><asp:TextBox ID="txtQtde" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" 
                                ControlToValidate="txtQtde" ErrorMessage="Qtde/Kilo falta ser preenchido." 
                                ValidationGroup="ValidaDadosItens">*</asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlUnidade" runat="server">
                                </asp:DropDownList>
                            </td>
                        <td>
                            <asp:TextBox ID="txtIPI" runat="server" onkeypress="OnlyMoney();" Width="50px"></asp:TextBox>
                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtValorUnit" runat="server"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" 
                                                                ControlToValidate="txtValorUnit" ErrorMessage="Valor falta ser preenchido." 
                                                                ValidationGroup="ValidaDadosItens">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <table width="100%" cellpadding="0" cellspacing="0" align="center">
                                    <tbody>
                                        <tr>
                                            <td colspan="17" style="padding-left: 20px; border-bottom: 1px solid black">
                                                <b>Análise Quimica</b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left: 20px; border-left: 1px solid black; width: 68px;">
                                                Al
                                            </td>
                                            <td style="padding-left: 20px; width: 80px" >
                                                C
                                            </td>
                                            <td style="padding-left: 20px; width: 71px;">
                                                Si
                                            </td>
                                            <td style="padding-left: 20px;">
                                            Mn
                                            </td>
                                            <td style="padding-left: 20px;">
                                             P
                                            </td>
                                            <td style="padding-left: 20px;">
                                             S
                                            </td>
                                            <td style="padding-left: 20px;">
                                             Cr
                                            </td>
                                            <td style="padding-left: 20px;">
                                                Ni
                                            </td>
                                            <td style="padding-left: 20px;">
                                                Mo
                                            </td>
                                            <td style="padding-left: 20px;">
                                                Cu
                                            </td>
                                            <td style="padding-left: 20px;">
                                                Ti
                                            </td>
                                            <td style="padding-left: 20px;">
                                                N2
                                            </td>
                                            <td style="padding-left: 20px; border-right: 1px solid black">
                                                Co
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-left: 1px solid black; width: 68px;">
                                                <asp:TextBox ID="txtAl" runat="server" Width="40px" 
                                                    ontextchanged="txtAl_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                <span id="spanAl" title="" class="asterisco" runat="server" style="display:none">*</span>
                                            </td>
                                            <td style="width: 80px;">
                                                <asp:TextBox ID="txtC" runat="server" Width="40px" AutoPostBack="True" 
                                                    ontextchanged="txtC_TextChanged"></asp:TextBox>
                                                <span id="spanC" class="asterisco" runat="server" style="display:none">*</span>
                                            </td>
                                            <td style="width: 71px">
                                                <asp:TextBox ID="txtSi" runat="server" Width="40px" AutoPostBack="True" 
                                                    ontextchanged="txtSi_TextChanged"></asp:TextBox>
                                                <span id="spanSi" class="asterisco" runat="server" style="display:none">*</span>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtMn" runat="server" Width="40px" AutoPostBack="True" 
                                                    ontextchanged="txtMn_TextChanged"></asp:TextBox>
                                                <span id="spanMn" class="asterisco" runat="server" style="display:none">*</span>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtP" runat="server" Width="40px" AutoPostBack="True" 
                                                    ontextchanged="txtP_TextChanged"></asp:TextBox>
                                                <span id="spanP" class="asterisco" runat="server" style="display:none">*</span>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtS" runat="server" Width="40px" AutoPostBack="True" 
                                                    ontextchanged="txtS_TextChanged"></asp:TextBox>
                                                <span id="spanS" class="asterisco" runat="server" style="display:none">*</span>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCr" runat="server" Width="40px" AutoPostBack="True" 
                                                    ontextchanged="txtCr_TextChanged"></asp:TextBox>
                                                <span id="spanCr" class="asterisco" runat="server" style="display:none">*</span>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNi" runat="server" Width="40px" AutoPostBack="True" 
                                                    ontextchanged="txtNi_TextChanged"></asp:TextBox>
                                                <span id="spanNi" class="asterisco" runat="server" style="display:none">*</span>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtMo" runat="server" Width="40px" AutoPostBack="True" 
                                                    ontextchanged="txtMo_TextChanged"></asp:TextBox>
                                                <span id="spanMo" class="asterisco" runat="server" style="display:none">*</span>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCu" runat="server" Width="40px" AutoPostBack="True" 
                                                    ontextchanged="txtCu_TextChanged"></asp:TextBox>
                                                <span id="spanCu" class="asterisco" runat="server" style="display:none">*</span>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtTi" runat="server" Width="40px" AutoPostBack="True" 
                                                    ontextchanged="txtTi_TextChanged"></asp:TextBox>
                                                <span id="spanTi" class="asterisco" runat="server" style="display:none">*</span>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtN2" runat="server" Width="40px" AutoPostBack="True" 
                                                    ontextchanged="txtN2_TextChanged"></asp:TextBox>
                                                <span id="spanN2" class="asterisco" runat="server" style="display:none">*</span>
                                            </td>
                                            <td style="border-right: 1px solid black">
                                                <asp:TextBox ID="txtCo" runat="server" Width="40px" AutoPostBack="True" 
                                                    ontextchanged="txtCo_TextChanged"></asp:TextBox>
                                                <span id="spanCo" class="asterisco" runat="server" style="display:none">*</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="17" style="border-bottom: 1px solid black; border-left: 1px solid black;
                                                border-right: 1px solid black;">
                                                &nbsp;</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table width="100%" cellpadding="0" cellspacing="0" align="center">
                                    <tbody>
                                        <tr>
                                            <td colspan="2" style="padding-left: 20px; border-bottom: 1px solid black;">
                                                <b>Ensaios Mecânicos</b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left: 20px; width: 150px; border-left: 1px solid black; padding-top: 2px;">
                                                Resistência à Tração:
                                            </td>
                                            <td style="padding-top: 2px; border-right: 1px solid black;">
                                                <asp:TextBox ID="txtResistenciaTracao" runat="server" 
                                                    ontextchanged="txtResistenciaTracao_TextChanged" Width="90px" AutoPostBack="True"></asp:TextBox>
                                                <span id="spanRt" class="asterisco" runat="server" style="display:none">*</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left: 20px; width: 142px; padding-top: 5px; border-left: 1px solid black;">
                                                Dureza:
                                            </td>
                                            <td style="padding-top: 5px; border-right: 1px solid black;">
                                                <asp:TextBox ID="txtDureza" runat="server" Width="90px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="border-bottom: 1px solid black; border-left: 1px solid black;
                                                border-right: 1px solid black;">
                                                &nbsp;</td>
                                    </tbody>
                                </table>
                            </td>
                            <td colspan="4">
                                <table width="100%" cellpadding="0" cellspacing="0" align="center">
                                    <tbody>
                                        <tr>
                                            <td colspan="2" style="padding-left: 20px; border-bottom: 1px solid black">
                                                <b>Nota</b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left: 20px; width: 150px; padding-top: 2px; border-left: 1px solid black;">
                                                Nota:
                                            </td>
                                            <td style="padding-top: 5px; border-right: 1px solid black;">
                                                <asp:TextBox ID="txtNota" runat="server" Width="177px" onblur="getSituacao(this.value)"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left: 20px; width: 142px; padding-top: 5px; border-left: 1px solid black;">
                                                Situação:
                                            </td>
                                            <td style="padding-top: 5px; border-right: 1px solid black;">
                                                <asp:TextBox ID="txtSituacao" runat="server" CssClass="DesligarTextBox situacao" 
                                                    Width="285px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="border-bottom: 1px solid black; border-left: 1px solid black;
                                            border-right: 1px solid black;">
                                                &nbsp;&nbsp;</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:TextBox ID="txtObservacaoItem" runat="server" TextMode="MultiLine" 
                                    Width="99%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                        <td colspan="6">
                        <table cellpadding="3" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="padding-left:20px; width: 279px;"><b>Certificado Scanneado</b></td>
                                    </tr>
                                    <tr>
		                                <td valign="middle" style="width: 279px">
		                                    <asp:FileUpload   Width="300px"  runat="server" ID="upFileUp" />
		                                </td>
		                                <td>
                                                <asp:LinkButton ID="lkbArquivoPdf" runat="server" 
                                                    onclick="lkbArquivoPdf_Click"> (Nenhuma arquivo carregada)</asp:LinkButton>
                                            </td>
                                     </tr>
                                     <tr>
                                            <td colspan="2">
                                                        <asp:LinkButton ID="btnCarregarCertificado" runat="server" 
                                                            CssClass="hiperlink" onclick="btnCarregarImagem_Click">Carregar Certificado</asp:LinkButton>
                                                        <asp:LinkButton ID="btnLimparImagem" runat="server" CausesValidation="False" 
                                                        CssClass="hiperlink" style="padding-bottom:2px" Width="120px" 
                                                            onclick="btnLimparImagem_Click">Limpar Arquivo</asp:LinkButton>

                                            </td>                                       
                                     </tr>
                                </table>
                        </td>
                        </tr>
                        <tr>
                        <td colspan="2"><asp:Button ID="btnCancelarItem" Width="90px" runat="server" 
                                CssClass="botao" Text="Cancelar" onclick="btnCancelarItem_Click"/></td>
                        <td colspan="4" align="right"><asp:Button ID="btnIncluirItem" Width="90px" 
                                runat="server" CssClass="botao" Text="Salvar" 
                                onclick="btnIncluirItem_Click" ValidationGroup="ValidaDadosItens" /></td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnNovoItem" 
                    EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="grdProduto" 
                    EventName="RowCommand" />
                <asp:PostBackTrigger ControlID="lkbArquivoPdf" />
                <asp:PostBackTrigger ControlID="btnCarregarCertificado" />
                <asp:AsyncPostBackTrigger ControlID="btnIncluirItem" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnCancelarItem" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="ddlMateriaPrima" 
                    EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="txtAl" EventName="TextChanged" />
                <asp:AsyncPostBackTrigger ControlID="txtC" EventName="TextChanged" />
                <asp:AsyncPostBackTrigger ControlID="txtSi" EventName="TextChanged" />
                <asp:AsyncPostBackTrigger ControlID="txtMn" EventName="TextChanged" />
                <asp:AsyncPostBackTrigger ControlID="txtP" EventName="TextChanged" />
                <asp:AsyncPostBackTrigger ControlID="txtS" EventName="TextChanged" />
                <asp:AsyncPostBackTrigger ControlID="txtCr" EventName="TextChanged" />
                <asp:AsyncPostBackTrigger ControlID="txtNi" EventName="TextChanged" />
                <asp:AsyncPostBackTrigger ControlID="txtMo" EventName="TextChanged" />
                <asp:AsyncPostBackTrigger ControlID="txtCu" EventName="TextChanged" />
                <asp:AsyncPostBackTrigger ControlID="txtTi" EventName="TextChanged" />
                <asp:AsyncPostBackTrigger ControlID="txtCo" EventName="TextChanged" />
                <asp:AsyncPostBackTrigger ControlID="txtN2" EventName="TextChanged" />
                <asp:AsyncPostBackTrigger ControlID="ddlBitola" 
                    EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="txtResistenciaTracao" 
                    EventName="TextChanged" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <br />
</asp:Content>
