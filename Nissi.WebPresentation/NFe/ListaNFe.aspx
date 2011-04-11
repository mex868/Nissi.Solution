<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="ListaNFe.aspx.cs" Inherits="Nissi.WebPresentation.NFe.ListaNFe" %>
<%@ Register Assembly="RDC.Tools" Namespace="RDC.Tools" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="server">
    <script type="text/javascript" src="../JScripts/Common.js"></script>
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
        }

        //--------------------------------------------------------------------------------
        //Criado por...: Jacqueline Albuquerque - 24/11/2009
        //Objetivo.....: Verifica o tipo de pesquisa e habilita campos e limpa-los
        //--------------------------------------------------------------------------------
        function TipoPesquisa(tvar) {
            $get('<%=txtNF.ClientID%>').value = ''
            $get('<%=txtDataEmissao.ClientID%>').value = '';
            $get('<%=txtRazaoSocial.ClientID%>').value = '';
            $get('divNF').style.display = 'none';
            $get('divDataEmissao').style.display = 'none';
            $get('divRazaoSocial').style.display = 'none';
            $get('divProduto').style.display = 'none';
            $get('divListaNF').style.display = 'block';
            $get('divListaProduto').style.display = 'none';
            $get('divCodigoCliente').style.display = 'none';
            switch (tvar) {
                case 1: //Pesquisa por NF
                    $get('divNF').style.display = 'block';

                    break;
                case 2: //Pesquisa por Data de Emissao
                    $get('divDataEmissao').style.display = 'block';
                    break;
                case 3: //Pesquisa por Razao Social
                    $get('divRazaoSocial').style.display = 'block';
                    break;
                case 4: //Pesquisa por Produto
                    $get('divProduto').style.display = 'block';
                    $get('divListaNF').style.display = 'none';
                    $get('divListaProduto').style.display = 'block';
                    Limpar();
                    break;
                case 5: //Pesquisa por Código do Cliente
                    $get('divCodigoCliente').style.display = 'block';
            }
        }

        //--------------------------------------------------------------------------------
        //Criado por....: Jacqueline Albuquerque - 24/11/2009
        //Objetivo......: Valida os campos
        //Modificado por: Alexandre Maximiano - 12/09/2010
        //--------------------------------------------------------------------------------
        function ValidaCampos() {
            if (($get('<%=rbNF.ClientID%>').checked) && ($get('<%=txtNF.ClientID %>').value != '')) {
                return true;
            }
            if (($get('<%=rbDataEmissao.ClientID%>').checked) && ($get('<%=txtDataEmissao.ClientID %>').value != '')) {
                return true;
            }
            if (($get('<%=rbCodigoCliente.ClientID%>').checked) && $get('<%=txtCodigoCliente.ClientID %>').value != '') {
                return true;
                }
                if (($get('<%=rbRazaoSocial.ClientID%>').checked) && ($get('<%=txtRazaoSocial.ClientID%>').value != '')) {
                return true;
                
            }
            else {
                if ($get('<%=rbNF.ClientID%>').checked) {
                    alert("Informe a N.F.");
                    return false;
                }
                if ($get('<%=rbDataEmissao.ClientID%>').checked) {
                    alert("Informe a Data de Emissao.");
                    return false;
                }
                else if ($get('<%=rbRazaoSocial.ClientID%>').checked) {
                    alert('Informe a Razão Social do Cliente.');
                    return false;
                }
                else if ($get('<%=rbCodigoCliente.ClientID %>').checked) {
                    alert('Informe o Código do Cliente.');
                    return false;
                }
            }
            if ($get("<%=rbProduto.ClientID %>")) {
                //Executa a válidação do campo Código/Descrição
                var rbDescricao = $get("<%=rbOP.ClientID%>");
                var txtCodigoDescricao = $get("<%=txtCodigoDescricao.ClientID%>");
                if ((txtCodigoDescricao.value == '')) {
                    alert("Código deve ser preenchido");
                    return false;
                }
                if ((txtCodigoDescricao.value == '')) {
                    alert("Descrição deve ser preenchido");
                    return false;
                }
                //Guarda valores dos textbox
                var strDataInicial = Trim($get('<%=tbxDataIni.ClientID %>').value);
                var strDataFinal = Trim($get('<%=tbxDataFim.ClientID %>').value);
                //Guarda verificações de data
                var DataInicialValida = isDate(strDataInicial);
                DataFinalValida = isDate(strDataFinal);
                //Verifica se os Data Inicial e Final estão preenchidos
                if ((strDataInicial != '') && (strDataFinal != '')) {
                    //Se estão preenchidos e são datas, data inicial deve ser menor que final
                    var validacao = CompareDates(strDataInicial, strDataFinal)
                    if (validacao == false) {
                        alert("Período final deve ser maior que Período inicial");
                        return false;
                    }
                }
                else if (strDataInicial == '') {
                    alert("Período inicial deve ser preenchido");
                    return false;
                }
                else if (strDataFinal == '') {
                    alert("Período final deve ser preenchido");
                    return false;
                }
                return true;
            }
            return false;
        }

        function ChamaDuplicata(tvar) {
            window.open("../Relatorios/relDuplicata.aspx?CodNF=" + tvar + "", "_blank", "top=0,left=0,width=800,height=600,scrollbars=yes,resizable=no,toolbar=no");
        }
        //--------------------------------------------------------------------------------
        //Criado por...: Alexandre Maximiano - 12/09/2010
        //Objetivo.....: Limpa Campo Codigo/Descricao
        //--------------------------------------------------------------------------------
        function ChamaVisualizarNF(tvar) {
            var w = window.screen.width;
            window.open("VisualizarNFe.aspx?CodNF=" + tvar + "", "_blank", "top=0,left=0,width="+w+",height=600,scrollbars=yes,resizable=no,toolbar=no");
        }

        //--------------------------------------------------------------------------------
        //Criado por...: Alexandre Maximiano - 12/09/2010
        //Objetivo.....: Limpa Campo Codigo/Descricao
        //--------------------------------------------------------------------------------
        function Limpar() {
            $get("<%=txtCodigoDescricao.ClientID %>").value = '';
            $get("<%=tbxDataIni.ClientID %>").value = '';
            $get("<%=tbxDataFim.ClientID %>").value = '';
        }
        //--------------------------------------------------------------------------------
        //Criado por...:Alexandre Maximiano - 04/11/2010
        //Objetivo.....: Efetua consulta com o retorno do autocomplete
        //--------------------------------------------------------------------------------
        function CarregarValores(source, eventArgs) {
            $get('<%=hdfIdRazaoSocial.ClientID%>').value = eventArgs.get_value();
            $get('<%=txtRazaoSocial.ClientID %>').value = eventArgs._item.outerText;
            $get('<%=btnPesquisar.ClientID%>').click();
        }
        //--------------------------------------------------------------------------------
        //Criado por...: Alexandre Maximiano - 02/11/2009
        //Objetivo.....: Acionar botão acessar quando pressionada a tecla ENTER
        //--------------------------------------------------------------------------------
        function KeyDownHandler() {
            if (event.keyCode == 13) {
                event.returnValue = false;
                event.cancel = true;
                $get('<%=btnPesquisar.ClientID%>').click();
            }
        }
    </script>
    <table style="margin-left: auto; width: 95%; margin-right: auto;">
        <tr>
            <td style="width: 21px; text-align: left">
                <asp:Image ID="ImgCadastro" runat="server" ImageUrl="~/Imagens/layout.png" />
            </td>
            <td style="width: 95%; text-align: left" class="titulo">
                Cadastro de Nota Fiscal
            </td>
        </tr>
    </table>
    <br />
    <table class="fundoTabela" style="text-align: left; width: 95%">
        <tr>
            <td style="width: 15%; padding-left: 17px">
                Opções de Consulta:
            </td>
            <td style="width: 15%">
                <asp:RadioButton ID="rbNF" onclick="TipoPesquisa(1)" GroupName="filtroPesq" runat="server"
                    Text="NF" Checked="True" CssClass="noBorder" />
            </td>
            <td style="width: 20%">
                <asp:RadioButton ID="rbDataEmissao" onclick="TipoPesquisa(2)" GroupName="filtroPesq"
                    runat="server" Text="Data de Emissão" CssClass="noBorder" />
            </td>
            <td style="width: 15%">
                <asp:RadioButton ID="rbCodigoCliente" onclick="TipoPesquisa(5)" runat="server" Text="Código Cliente"
                    GroupName="filtroPesq" CssClass="noBorder" />
            </td>
            <td style="width: 20%">
                <asp:RadioButton ID="rbRazaoSocial" onclick="TipoPesquisa(3)" runat="server" Text="Razão Social"
                    GroupName="filtroPesq" CssClass="noBorder" />
            </td>
            <td style="width: 20%">
                <asp:RadioButton ID="rbProduto" onclick="TipoPesquisa(4)" runat="server" Text="Produto"
                    GroupName="filtroPesq" CssClass="noBorder" />
            </td>
        </tr>
        <tr>
            <td style="width: 20%">
                &nbsp;
            </td>
            <td colspan="4">
                <div id="divNF" style="display: block">
                    <asp:TextBox ID="txtNF" MaxLength="14" onkeypress="return digitos(event, this);KeyDownHandler();"
                        runat="server" Height="16px" Width="96px"></asp:TextBox>
                    <asp:CustomValidator ClientValidationFunction="ValidaCampos" Text="*" CssClass="asterisco"
                        ValidationGroup="pesquisar" ErrorMessage="Favor informar o C�digo" runat="server"
                        ID="CustomValidator1"></asp:CustomValidator>
                </div>
                <div id="divDataEmissao" style="display: none">
                    <asp:TextBox ID="txtDataEmissao" MaxLength="10" onkeypress="return digitos(event, this);KeyDownHandler();"
                        onkeyup="Mascara('DATA',this,event);" runat="server" Height="16px" Width="120px"></asp:TextBox>
                    <asp:CustomValidator ClientValidationFunction="ValidaCampos" Text="*" CssClass="asterisco"
                        ValidationGroup="pesquisar" ErrorMessage="Favor informar o C.N.P.J." runat="server"
                        ID="efvCNPJ"></asp:CustomValidator>
                </div>
                <div id="divRazaoSocial" style="display: none">
                    <asp:TextBox ID="txtRazaoSocial" MaxLength="50" onkeypress="ConverterCaixaAlta();KeyDownHandler();"
                        runat="server" Height="16px" Width="300px"></asp:TextBox>
                    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="txtRazaoSocial"
                            MinimumPrefixLength="1" ServiceMethod="GetNames" CompletionInterval="800" EnableCaching="true"
                            CompletionSetCount="10" OnClientItemSelected="CarregarValores" OnClientPopulated="ClientPopulated">
                            </ajaxToolkit:AutoCompleteExtender>
                    <asp:CustomValidator ClientValidationFunction="ValidaCampos" Text="*" CssClass="asterisco"
                        ValidationGroup="pesquisar" ErrorMessage="Favor informar o CPF" runat="server"
                        ID="CustomValidator2"></asp:CustomValidator>
                </div>
                <div id="divProduto" style="display: none">
                    <table id="tableProduto" runat="server" style="margin-left: auto; margin-right: auto;
                        width: 100%">
                        <tr>
                            <td class="tituloCampo" style="height: 12px">
                                <asp:RadioButton ID="rbCodigo" runat="server" onclick="Limpar()" Text="Código" Checked="True"
                                    GroupName="opcao" />
                                <asp:RadioButton ID="rbOP" onclick="Limpar()" runat="server" Text="OP"
                                    GroupName="opcao" />
                            </td>
                            <td class="tituloCampo" style="height: 12px">
                                Período
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtCodigoDescricao" onkeypress="KeyDownHandler();" runat="server"></asp:TextBox>
                                <asp:CustomValidator ID="cvValidaTexto" runat="server" Text="*" ErrorMessage="Código Inválido"
                                    ValidationGroup="pesquisar" ClientValidationFunction="ValidaCampos" CssClass="asterisco"></asp:CustomValidator>
                            </td>
                            <td class="tituloCampo" style="height: 23px">
                                <b>De:</b>
                                <asp:TextBox ID="tbxDataIni" onkeypress="formatar(this, '##/##/####');OnlyNumbers();KeyDownHandler();"
                                    MaxLength="10" runat="server" CssClass="formNovo" Width="75px"></asp:TextBox>
                                <img alt="" id="imgDataIni" align="absmiddle" style="cursor: pointer;" src="../Imagens/Calendar_scheduleHS.png" />
                                <asp:RegularExpressionValidator ControlToValidate="tbxDataIni" Text="*" CssClass="asterisco"
                                    ID="RegularExpressionValidator3" ErrorMessage="Período inicial Inválido." ValidationGroup="pesquisar"
                                    runat="server" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"></asp:RegularExpressionValidator>
                                <ajaxToolkit:CalendarExtender Format="dd/MM/yyyy" ID="CalendarExtender1" PopupButtonID="imgDataIni"
                                    runat="server" TargetControlID="tbxDataIni" Animated="true">
                                </ajaxToolkit:CalendarExtender>
                                &nbsp; &nbsp; <b>Até:</b>
                                <asp:TextBox ID="tbxDataFim" runat="server" onkeypress="formatar(this, '##/##/####');OnlyNumbers();KeyDownHandler();"
                                    MaxLength="10" CssClass="formNovo" TabIndex="12" Width="75px"></asp:TextBox>
                                <img alt="" align="absmiddle" id="imgDataFim" style="cursor: pointer" src="../Imagens/Calendar_scheduleHS.png" />
                                <asp:RegularExpressionValidator ControlToValidate="tbxDataFim" Text="*" CssClass="asterisco"
                                    ID="RegularExpressionValidator4" ErrorMessage="Período final Inválido." ValidationGroup="pesquisar"
                                    runat="server" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"></asp:RegularExpressionValidator>
                                <ajaxToolkit:CalendarExtender Format="dd/MM/yyyy" ID="CalendarExtender2" PopupButtonID="imgDataFim"
                                    runat="server" TargetControlID="tbxDataFim" Animated="true">
                                </ajaxToolkit:CalendarExtender>
                                <asp:CustomValidator ID="ctvValidaDataPesquisa" runat="server" ClientValidationFunction="ValidaCampos"
                                    ValidationGroup="pesquisar" ErrorMessage="Datas inválidas" CssClass="asterisco">*</asp:CustomValidator>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divCodigoCliente" style="display: none">
                    <asp:TextBox ID="txtCodigoCliente" onkeypress="KeyDownHandler();" runat="server"></asp:TextBox>
                    <asp:CustomValidator ClientValidationFunction="ValidaCampos" Text="*" CssClass="asterisco"
                        ValidationGroup="pesquisar" ErrorMessage="Favor informar o Código do Cliente"
                        runat="server" ID="CustomValidator3"></asp:CustomValidator>
                </div>
            </td>
        </tr>
        <tr>
            <td style="text-align: left" colspan="3">
                <asp:Button ID="btnVoltar" runat="server" OnClick="btnVoltar_Click" CssClass="botao"
                    Text="Voltar" Width="100px" UseSubmitBehavior="False" />
            </td>
            <td colspan="2" style="text-align: right">
                <asp:UpdatePanel ID="updBotoes" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:HiddenField ID="hdfIdRazaoSocial" runat="server" />
                        <asp:Button ID="btnPesquisar" OnClientClick="return ValidaCampos()" runat="server"
                            ValidationGroup="pesquisar" CssClass="botao" Text="Pesquisar" Width="100px" OnClick="btnPesquisar_Click" />
                        &nbsp;<asp:Button ID="btnIncluir" runat="server" CssClass="botao" Width="100px" Text="Incluir Novo"
                            OnClick="btnIncluir_Click" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <br />
    <asp:UpdatePanel ID="udpListaResultado" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="divListaNF" style="display: block">
                <cc1:RDCGrid ID="grdListaResultado" runat="server" AutoGenerateColumns="False" BorderColor="Black"
                    BorderWidth="1px" CellPadding="1" CellSpacing="3" GridLines="None" PageSize="30"
                    ShowPageDetails="True" AllowPaging="True" MultiSelection="True" ShowHeaderCheckBoxColumn="False"
                    ShowOptionColumn="False" CssClass="alinhamento" OnPageIndexChanging="grdListaResultado_PageIndexChanging"
                    OnRowCommand="grdListaResultado_RowCommand" OnRowDataBound="grdListaResultado_RowDataBound"
                    EnableModelValidation="True" Width="95%">
                    <Columns>
                        <asp:TemplateField HeaderText="Ações">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEditar" Width="15px" Height="15px" runat="server" ImageUrl="~/Imagens/editar.png" />
                                <asp:ImageButton ID="imgExcluir" Width="15px" Height="15px" runat="server" ImageUrl="~/Imagens/exclusao_Canc.png"
                                    Style="margin-right: 0px" />
                                <asp:ImageButton Style="cursor: pointer" ID="imgDuplicata" Width="15px" Height="15px"
                                    runat="server" ImageUrl="~/Imagens/btn-SolicitacaoDocumentos.gif" />
                                <asp:ImageButton ID="imgVisualizarNF" runat="server" Height="15px" 
                                    ImageUrl="~/Imagens/btn-SolicitacaoDocumentos.gif" Style="cursor: pointer" 
                                    Width="15px" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="headerGrid" Width="5%" />
                            <ItemStyle HorizontalAlign="center" Wrap="false" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Série">
                            <HeaderStyle CssClass="headerGrid" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="NF">
                            <HeaderStyle CssClass="headerGrid" />
                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Data de Emissao">
                            <HeaderStyle CssClass="headerGrid" />
                            <ItemStyle HorizontalAlign="Left" Width="13%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Razão Social">
                            <HeaderStyle CssClass="headerGrid" />
                            <ItemStyle HorizontalAlign="Left" Width="30%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Nome Fantasia">
                            <ItemStyle HorizontalAlign="Left" Width="25%" />
                            <HeaderStyle CssClass="headerGrid" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Chave NF-e">
                            <HeaderStyle CssClass="headerGrid" />
                            <ItemStyle HorizontalAlign="Left" Width="30%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgStatus" runat="server" Height="15px" ImageUrl="~/Imagens/NFeOk.png"
                                    Width="15px" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                            <HeaderStyle CssClass="headerGrid" />
                        </asp:TemplateField>
                    </Columns>
                </cc1:RDCGrid>
            </div>
            <div id="divListaProduto" style="display:block">
                <cc1:RDCGrid ID="grdListaProduto" runat="server" AutoGenerateColumns="False" BorderColor="Black"
                    BorderWidth="1px" CellPadding="1" CellSpacing="3" GridLines="None" PageSize="15"
                    ShowPageDetails="True" AllowPaging="True" MultiSelection="True" ShowHeaderCheckBoxColumn="False"
                    ShowOptionColumn="False" CssClass="alinhamento" OnPageIndexChanging="grdListaProduto_PageIndexChanging"
                    OnRowCommand="grdListaProduto_RowCommand" OnRowDataBound="grdListaProduto_RowDataBound"
                    EnableModelValidation="True" Width="95%" ShowFooter="True">
                    <Columns>
                        <asp:TemplateField HeaderText="Ações">
                            <ItemTemplate>
                                <asp:ImageButton Style="cursor: pointer" ID="imgEditarProduto" Width="15px" Height="15px"
                                    runat="server" ImageUrl="~/Imagens/btn-SolicitacaoDocumentos.gif" />
                                <asp:ImageButton ID="imgVisualizar" runat="server" Height="15px" 
                                    ImageUrl="~/Imagens/btn-SolicitacaoDocumentos.gif" Style="cursor: pointer" 
                                    Width="15px" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="headerGrid" Width="5%" />
                            <ItemStyle HorizontalAlign="center" Wrap="false" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="NF">
                            <HeaderStyle CssClass="headerGrid" />
                            <ItemStyle HorizontalAlign="Left" Width="9%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="OP">
                        <HeaderStyle CssClass="headerGrid" />
                        <ItemStyle Width="8%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Data de Emissao">
                            <HeaderStyle CssClass="headerGrid" />
                            <ItemStyle HorizontalAlign="Left" Width="12%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Codigo">
                            <HeaderStyle CssClass="headerGrid" />
                            <ItemStyle Width="8%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Descricao">
                            <HeaderStyle CssClass="headerGrid" />
                            <ItemStyle HorizontalAlign="Left" Width="32%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Qtde">
                            <HeaderStyle CssClass="headerGrid" />
                            <ItemStyle Width="4%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Valor" DataFormatString="{0:c}">
                            <ItemStyle HorizontalAlign="Right" Width="12%" />
                            <HeaderStyle CssClass="headerGrid" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Total do Item" DataFormatString="{0:c}">
                            <HeaderStyle CssClass="headerGrid" />
                            <ItemStyle HorizontalAlign="Right" Width="30%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Status">
                        <HeaderStyle CssClass="headerGrid" />
                       <ItemTemplate>
                                <asp:ImageButton ID="imgStatusProduto" runat="server" Height="15px" ImageUrl="~/Imagens/NFeOk.png"
                                    Width="15px" />
                            </ItemTemplate>

                        </asp:TemplateField>
                    </Columns>
                </cc1:RDCGrid>
            </div>
            <asp:HiddenField ID="hdfCodNF" runat="server" />
            <asp:HiddenField ID="hdfValor" runat="server" />
            <asp:HiddenField ID="hdfOpcao" runat="server" />
            <br />
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnPesquisar" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
