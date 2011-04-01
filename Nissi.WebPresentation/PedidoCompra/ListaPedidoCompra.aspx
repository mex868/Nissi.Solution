<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="ListaPedidoCompra.aspx.cs" Inherits="Nissi.WebPresentation.PedidoCompra.ListaPedidoCompra" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Assembly="RDC.Tools" Namespace="RDC.Tools" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="server">
    <script type="text/javascript" src="../JScripts/Common.js"></script>
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
        }

        //--------------------------------------------------------------------------------
        //Criado por...: Jacqueline Albuquerque - 24/11/2009
        //Objetivo.....: Verifica o tipo de pesquisa e habilita campos e limpa-los
        //--------------------------------------------------------------------------------
        function TipoPesquisa(tvar) {
            $get('<%=txtPedidoCompra.ClientID%>').value = ''
            $get('<%=ddlBitola.ClientID%>').value = '';
            $get('<%=txtRazaoSocial.ClientID%>').value = '';
            $get('divPedidoCompra').style.display = 'none';
            $get('divBitola').style.display = 'none';
            $get('divRazaoSocial').style.display = 'none';
            $get('divListaPedidoCompra').style.display = 'block';
            $get('divClasseTipo').style.display = 'none';
            $get('divPeriodo').style.display = 'none';
            switch (tvar) {
                case 1: //Pesquisa por PedidoCompra
                    $get('divPedidoCompra').style.display = 'block';

                    break;
                case 2: //Pesquisa por Data de Emissao
                    $get('divBitola').style.display = 'block';
                    break;
                case 3: //Pesquisa por Razao Social
                    $get('divRazaoSocial').style.display = 'block';
                    break;
                case 4: //Pesquisa por Código do Cliente
                    $get('divClasseTipo').style.display = 'block';
                    break;
                case 5: //Pesquisa por periódo
                    $get('divPeriodo').style.display = 'block';
                    break;
            }
        }

        //--------------------------------------------------------------------------------
        //Criado por....: Jacqueline Albuquerque - 24/11/2009
        //Objetivo......: Valida os campos
        //Modificado por: Alexandre Maximiano - 12/09/2010
        //--------------------------------------------------------------------------------
        function ValidaCampos() {

            if (($get('<%=rbPedidoCompra.ClientID%>').checked) && ($get('<%=txtPedidoCompra.ClientID %>').value != '')) {
                return true;
            }
            if (($get('<%=rbBitola.ClientID%>').checked) && ($get('<%=ddlBitola.ClientID %>').value != '')) {
                return true;
            }
            if (($get('<%=rbClasseTipo.ClientID%>').checked) && $get('<%=ddlClasseTipo.ClientID %>').value != '') {
                return true;
                }
                if (($get('<%=rbRazaoSocial.ClientID%>').checked) && ($get('<%=txtRazaoSocial.ClientID%>').value != '')) {
                    return true;
                }
            if (($get('<%=rbPeriodo.ClientID%>').checked) && ($get('<%=txtDataInicio.ClientID %>').value != '') && ($get('<%=txtDataFim.ClientID %>').value != '')){
                return true;
            }  
            else {
                if ($get('<%=rbPedidoCompra.ClientID%>').checked) {
                    alert("Informe o número do Pedido de Compra");
                    return false;
                }
                if ($get('<%=rbBitola.ClientID%>').checked) {
                    alert("Informe a Bitola.");
                    return false;
                }
                else if ($get('<%=rbRazaoSocial.ClientID%>').checked) {
                    alert('Informe a Razão Social do Fornecedor.');
                    return false;
                }
                else if ($get('<%=rbClasseTipo.ClientID %>').checked) {
                    alert('Informe a Classe/Tipo do Material.');
                    return false;
                }
                else if ($get('<%=rbPeriodo.ClientID %>').checked) {
                    var strDataInicial;
                    var strDataFinal;
                    var DataInicialValida;
                    var DataFinalValida;

                    //Guarda valores dos textbox
                    strDataInicial = Trim($get('<%=txtDataInicio.ClientID %>').value);
                    strDataFinal = Trim($get('<%=txtDataFim.ClientID %>').value);
                    //Guarda verificações de data
                    DataInicialValida = isDate(strDataInicial);
                    DataFinalValida = isDate(strDataFinal);

                    //Verifica se os Data Inicial e Final estão preenchidos
                    if ((strDataInicial != '') && (strDataFinal != '')) {
                        //Se estão preenchidos e são datas, data inicial deve ser menor que final
                        alert('Período final deve ser maior que Período inicial');
                        return false;
                    }
                    else if (strDataInicial != '') {
                        alert('Período final deve ser preenchido');
                        return false;
                    }
                    else if (strDataFinal != '') {
                        alert('Período inicial deve ser preenchido');
                        return false;
                    }
                }
            }
           
            return false;
        }

        function ChamaPedidoCompra(codPedidoCompra, tipo) {
        var hdfValor = $get("<%=hdfValor.ClientID %>");
        var hdfOpcao = $get("<%=hdfOpcao.ClientID %>");
        var rbPedidoCompra = $get("<%=rbPedidoCompra.ClientID %>");
        var txtPedidoCompra = $get("<%=txtPedidoCompra.ClientID %>");
        var rbBitola = $get("<%=rbBitola.ClientID %>");
        var ddlBitola = $get("<%=ddlBitola.ClientID %>");
        var rbRazaoSocial = $get("<%=rbRazaoSocial.ClientID %>");
        var txtRazaoSocial = $get("<%=txtRazaoSocial.ClientID %>");
        var rbClasseTipo = $get("<%=rbClasseTipo.ClientID %>");
        var ddlClasseTipo = $get("<%=ddlClasseTipo.ClientID %>");
            if (rbPedidoCompra.checked && txtPedidoCompra.value != "")
                        {
                            hdfValor.value = txtPedidoCompra.Text;
                            hdfOpcao.value = "PedidoCompra";
                        }
                        if (rbBitola.checked && ddlBitola.value != "")
                        {
                            hdfValor.value = ddlBitola.value;
                            hdfOpcao.value = "Bitola";
                        }
                        if (rbRazaoSocial.checked && txtRazaoSocial.value != "")
                        {
                            hdfValor.value = txtRazaoSocial.Text;
                            hdfOpcao.value = "RazaoSocial";
                        }
                        if (rbClasseTipo.checked && ddlClasseTipo.value != "")
                        {
                            hdfValor.value = ddlClasseTipo.value;
                            hdfOpcao.value = "ClasseTipo";
                        }
            WaitAsyncPostBack(true);
            window.location="CadastraPedidoCompra.aspx?acao=Editar&CodPedidoCompra="+codPedidoCompra+"&tipo="+ tipo +"&valor=" + hdfValor.value + "&opcao=" + hdfOpcao.value;
        }
        function EnviarPedidoCompra(tvar, tvar2) {
            WaitAsyncPostBack(true);
            var w = 300;
            var h = 200;
            LeftPosition = (screen.width) ? (screen.width - w) / 2 : 0;
            TopPosition = (screen.height) ? (screen.height - h) / 2 : 0;
            window.open("../Relatorios/PedidoCompra.aspx?CodPedidoCompra=" + tvar + "&tipo=enviar&CodPessoa=" + tvar2, "_blank", "top=" + TopPosition + ",left=" + LeftPosition + ",width=" + w + ",height=" + h + ",scrollbars=yes,resizable=no,toolbar=no");
            WaitAsyncPostBack(false);
        }
        //--------------------------------------------------------------------------------
        //Criado por...: Alexandre Maximiano - 12/09/2010
        //Objetivo.....: Limpa Campo Codigo/Descricao
        //--------------------------------------------------------------------------------
        function ChamaVisualizarPedidoCompra(tvar) {
            WaitAsyncPostBack(true);
            window.open("../Relatorios/PedidoCompra.aspx?CodPedidoCompra=" + tvar + "&tipo=imprimir", "_blank", "top=0,left=0,width=800,height=600,scrollbars=yes,resizable=no,toolbar=no");
            WaitAsyncPostBack(false);
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
        function BuscaDados() {
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

        var template = '<span style="color:{0};">{1}</span>';
        
        var Ipi = function (value) {
            return String.format(template,'black', value + "%");
        };
        var diasAtraso = function (value) {
            return String.format(template, (value > 0) ? "green" : "red", value);
        };
        var situacao = function (value) {
            return String.format(template, (value == "Aberto")?"black":(value == "Entregue") ? "green" : (value == "Parcial") ? "black" : "red", value);
        };

        var rowcommand = function (command, record) {
            if (command == "Excluir") {
                Ext.Msg.confirm('Cancelar',
'Confirmar cancelamento do Pedido de Compra?',
function (btn) {
    if (btn == 'yes') {
        var store = ctl00_cphPrincipal_grdListaResultado1.store;
        store.remove(record);
        ctl00_cphPrincipal_grdListaResultado1.save();
    }
}, this, { stopEvent: true });
            }
            if (command == "Editar") {
                ChamaPedidoCompra(record.data.OrdemCompra, record.data.Tipo);
            }
            if (command == "Imprimir") {
                ChamaVisualizarPedidoCompra(record.data.OrdemCompra);
            }
            if (command == "Enviar") {
                EnviarPedidoCompra(record.data.OrdemCompra, record.data.CodPessoa);
            }
        }
        var beforedelete = function (ar) {
            var args = arguments;

        }
    </script>
    <table style="margin-left: auto; width: 95%; margin-right: auto;">
        <tr>
            <td style="width: 21px; text-align: left">
                <asp:Image ID="ImgCadastro" runat="server" ImageUrl="~/Imagens/layout.png" />
            </td>
            <td style="width: 95%; text-align: left" class="titulo">
                Cadastro de Pedido de Compra
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
                <asp:RadioButton ID="rbPedidoCompra" onclick="TipoPesquisa(1)" GroupName="filtroPesq" runat="server"
                    Text="Nº do Pedido" Checked="True" CssClass="noBorder" />
            </td>
            <td style="width: 20%">
                <asp:RadioButton ID="rbBitola" onclick="TipoPesquisa(2)" GroupName="filtroPesq"
                    runat="server" Text="Bitola" CssClass="noBorder" />
            </td>
            <td style="width: 15%">
                <asp:RadioButton ID="rbClasseTipo" onclick="TipoPesquisa(4)" runat="server" Text="Classe/Tipo"
                    GroupName="filtroPesq" CssClass="noBorder" />
            </td>
            <td style="width: 20%">
                <asp:RadioButton ID="rbRazaoSocial" onclick="TipoPesquisa(3)" runat="server" Text="Fornecedor"
                    GroupName="filtroPesq" CssClass="noBorder" />
            </td>
            <td style="width: 20%">
                <asp:RadioButton ID="rbPeriodo" onclick="TipoPesquisa(5)" runat="server" Text="Por Data" GroupName="filtroPesq"/></td>
        </tr>
        <tr>
            <td style="width: 20%">
                &nbsp;
            </td>
            <td colspan="5">
                <div id="divPedidoCompra" style="display: none">
                    <asp:TextBox ID="txtPedidoCompra" MaxLength="14" onkeypress="KeyDownHandler();return digitos(event, this);"
                        runat="server" Height="16px" Width="96px"></asp:TextBox>
                    <asp:CustomValidator ClientValidationFunction="ValidaCampos" Text="*" CssClass="asterisco"
                        ValidationGroup="pesquisar" ErrorMessage="Favor informar Ordem de Compra" runat="server"
                        ID="CustomValidator1"></asp:CustomValidator>
                </div>
                <div id="divBitola" style="display: none">
                    <asp:DropDownList ID="ddlBitola" MaxLength="10" onChange="BuscaDados();" onkeypress="return OnlyMoney();" runat="server" Height="16px" Width="120px"></asp:DropDownList>
                    <asp:CustomValidator ClientValidationFunction="ValidaCampos" Text="*" CssClass="asterisco"
                        ValidationGroup="pesquisar" ErrorMessage="Favor informar o Bitola" runat="server"
                        ID="efvCNPJ"></asp:CustomValidator>
                </div>
                <div id="divRazaoSocial" style="display: none">
                    <asp:TextBox ID="txtRazaoSocial" MaxLength="50" onkeypress="ConverterCaixaAlta();KeyDownHandler();"
                        runat="server" Height="16px" Width="300px"></asp:TextBox>
                    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="txtRazaoSocial"
                            MinimumPrefixLength="1" ServiceMethod="GetFornecedor" CompletionInterval="800" EnableCaching="true"
                            CompletionSetCount="10" OnClientItemSelected="CarregarValores" OnClientPopulated="ClientPopulated">
                            </ajaxToolkit:AutoCompleteExtender>
                    <asp:CustomValidator ClientValidationFunction="ValidaCampos" Text="*" CssClass="asterisco"
                        ValidationGroup="pesquisar" ErrorMessage="Favor informar a Razão Social do Fornecedor" runat="server"
                        ID="CustomValidator2"></asp:CustomValidator>
                </div>
                <div id="divClasseTipo" style="display: none">
                    <asp:DropDownList ID="ddlClasseTipo" runat="server" onChange="BuscaDados();"></asp:DropDownList>
                    <asp:CustomValidator ClientValidationFunction="ValidaCampos" Text="*" CssClass="asterisco"
                        ValidationGroup="pesquisar" ErrorMessage="Favor informar a Classe/Tipo"
                        runat="server" ID="CustomValidator3"></asp:CustomValidator>
                </div>
                <div id="divPeriodo" style="display: block">
                De:<asp:TextBox ID="txtDataInicio" onkeypress="KeyDownHandler();" runat="server" CssClass="dataEmissao"></asp:TextBox>
                Até:<asp:TextBox ID="txtDataFim" onkeypress="KeyDownHandler();" runat="server" CssClass="dataEmissao"></asp:TextBox>
                </div>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr style="display:none">
            <td style="text-align: left" colspan="3">
                <asp:Button ID="btnVoltar" runat="server" OnClick="btnVoltar_Click" CssClass="botao"
                    Text="Voltar" Width="100px" UseSubmitBehavior="False" />
            </td>
            <td colspan="4" style="text-align: right">
                <asp:UpdatePanel ID="updBotoes" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:HiddenField ID="hdfIdRazaoSocial" runat="server" />
                        &nbsp;<asp:Button ID="btnPesquisar" OnClientClick="return ValidaCampos()" runat="server"
                            ValidationGroup="pesquisar" CssClass="botao" Text="Pesquisar" Width="100px" OnClick="btnPesquisar_Click" />
                        &nbsp;<asp:Button ID="btnIncluir" runat="server" CssClass="botao" Width="100px" Text="Incluir Novo"
                            OnClick="btnIncluir_Click" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td style="text-align: right">
                &nbsp;</td>
        </tr>
    </table>
    <br />
    <asp:UpdatePanel ID="udpListaResultado" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="divListaPedidoCompra" style="display: block;">
                <cc1:RDCGrid ID="grdListaResultado" runat="server" AutoGenerateColumns="False" BorderColor="Black"
                    BorderWidth="1px" CellPadding="1" CellSpacing="3" GridLines="None" PageSize="30"
                    ShowPageDetails="True" AllowPaging="True" MultiSelection="True" ShowHeaderCheckBoxColumn="False"
                    ShowOptionColumn="False" CssClass="alinhamento" OnPageIndexChanging="grdListaResultado_PageIndexChanging"
                    OnRowCommand="grdListaResultado_RowCommand" OnRowDataBound="grdListaResultado_RowDataBound"
                    EnableModelValidation="True" Width="95%" Visible="false">
                    <Columns>
                        <asp:TemplateField HeaderText="Ações">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEditar" Width="15px" Height="15px" runat="server" ImageUrl="~/Imagens/editar.png" />
                                <asp:ImageButton ID="imgExcluir" Width="15px" Height="15px" runat="server" ImageUrl="~/Imagens/exclusao_Canc.png"
                                    Style="margin-right: 0px" />
                                <asp:ImageButton Style="cursor: pointer" ID="imgImprimir" Width="15px" Height="15px"
                                    runat="server" ImageUrl="~/Imagens/Imprimir.png" />
                                <asp:ImageButton ID="imgEnviarEmail" runat="server" Height="15px" 
                                    ImageUrl="~/Imagens/Enviar.png" Style="cursor: pointer" 
                                    Width="15px" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="headerGrid" Width="5%" />
                            <ItemStyle HorizontalAlign="center" Wrap="false" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Ordem de Compra">
                            <HeaderStyle CssClass="headerGrid" />
                            <ItemStyle HorizontalAlign="Left" Width="8%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Emissao">
                            <HeaderStyle CssClass="headerGrid" />
                            <ItemStyle HorizontalAlign="Left" Width="8%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Fornecedor">
                            <HeaderStyle CssClass="headerGrid" />
                            <ItemStyle HorizontalAlign="Left" Width="20%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Bitola">
                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                            <HeaderStyle CssClass="headerGrid" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Matéria Prima">
                            <HeaderStyle CssClass="headerGrid" />
                           <ItemStyle HorizontalAlign="Left" Width="20%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Preço">
                            <HeaderStyle CssClass="headerGrid" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="IPI" >
                           <HeaderStyle CssClass="headerGrid" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Qtde">
                            <HeaderStyle CssClass="headerGrid" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Entrega Prevista">
                        <HeaderStyle CssClass="headerGrid" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Qtde Entrega">
                            <HeaderStyle CssClass="headerGrid" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Saldo" >
                            <HeaderStyle CssClass="headerGrid" />
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Situação" >
                            <HeaderStyle CssClass="headerGrid" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Dia de Atraso">
                        <HeaderStyle CssClass="headerGrid" />
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                    </Columns>
                </cc1:RDCGrid>
                    <ext:ResourceManager ID="ResourceManager1" runat="server" />
                            <ext:Store ID="StoreListaResultado" runat="server" 
                    onrefreshdata="StoreListaResultado_RefreshData">
                                <Reader>
                                    <ext:JsonReader IDProperty="Name">
                                        <Fields>
                                            <ext:RecordField Name="OrdemCompra" />
                                            <ext:RecordField Name="CodPessoa" />
                                            <ext:RecordField Name="Tipo" />
                                            <ext:RecordField Name="DataEmissao" Type="Date"/>
                                            <ext:RecordField Name="Fornecedor" />
                                            <ext:RecordField Name="Bitola" />
                                            <ext:RecordField Name="Descricao" />
                                            <ext:RecordField Name="Preco" />
                                            <ext:RecordField Name="Ipi" />
                                            <ext:RecordField Name="Unidade" />
                                            <ext:RecordField Name="Qtde" />
                                            <ext:RecordField Name="DataPrevista" Type="Date" />
                                            <ext:RecordField Name="QtdeEntregue" />
                                            <ext:RecordField Name="Saldo" />
                                            <ext:RecordField Name="Situacao" />
                                            <ext:RecordField Name="DiaEmAtraso" />
                                        </Fields>
                                    </ext:JsonReader>
                                </Reader>
                            </ext:Store> 
    <ext:Panel ID="Panel1" 
        runat="server" 
        Title="Example" 
        Height="350"
        Layout="Center" ForceLayout="True" Header="False">
        <TopBar>
            <ext:Toolbar ID="Toolbar1" runat="server">
                <Items>
                    <ext:Button ID="btnVoltarExt" runat="server" CtCls="botao"  Width="100px" Text="Voltar" OnDirectClick="btnVoltar_Click" Split="False" />
                    <ext:Button ID="btnPesquisarExt" runat="server" CtCls="botao" Width="100px" Text="Pesquisar" OnDirectClick="btnPesquisar_Click" />
                    <ext:Button ID="btnIncluirExt" runat="server" CtCls="botao" Width="100px" Text="Incluir" OnDirectClick="btnIncluir_Click" />
                </Items>
            </ext:Toolbar>
        </TopBar>        
        <Items>

                            <ext:GridPanel ID="grdListaResultado1" runat="server" 
                            StoreID="StoreListaResultado"
                            StripeRows="true"
                            Title="Lista de Pedidos de Compra"
                            AutoExpandColumn="OrdemCompra" 
                            Collapsible="true" 
                            EnableColumnMove="true"
                            Height="350" ColumnLines="True" CtCls="GridLayout">
                           <ColumnModel ID="ColumnModel1" runat="server">
		                    <Columns>
                                <ext:CommandColumn Width="115" Header="Ações">
                                    <Commands>
                                        <ext:GridCommand Icon="NoteEdit" CommandName="Editar">
                                            <ToolTip Text="Editar dados do Pedido de Compra" />
                                        </ext:GridCommand>
                                         <ext:CommandSeparator />
                                         <ext:GridCommand Icon="Delete" CommandName="Excluir">
                                            <ToolTip Text="Cancelar dados do Pedido de Compra" />
                                        </ext:GridCommand>
                                        <ext:CommandSeparator />
                                        <ext:GridCommand Icon="PrinterGo" CommandName="Imprimir">
                                            <ToolTip Text="Emissão de Pedido de Compra" />
                                        </ext:GridCommand>
                                        <ext:CommandSeparator />
                                        <ext:GridCommand Icon="EmailGo" CommandName="Enviar">
                                            <ToolTip Text="Enviar  Pedido de Compra por E-mail" />
                                        </ext:GridCommand>
                                    </Commands>
                                </ext:CommandColumn>
                                <ext:Column ColumnId="OrdemCompra" Align="Right"  Header="Ordem de Compra" Width="160" DataIndex="OrdemCompra" Hidden="False" Css="font-family : Arial; 	font-size : 16px;" />
                                <ext:Column ColumnId="CodPessoa" Align="Right"  Header="CodPessoa" Width="160" DataIndex="CodPessoa" Hidden="true" Css="font-family : Arial; 	font-size : 16px;" />
                                <ext:Column ColumnId="Tipo" Align="Right"  Header="Tipo" Width="160" DataIndex="Tipo" Hidden="true" Css="font-family : Arial; 	font-size : 16px;" />
                                <ext:DateColumn ColumnId="DataEmissao" Format="dd/MM/yyyy" Header="Emissão"  Width="90" DataIndex="DataEmissao" Hidden="False" Css="font-family : Arial; 	font-size : 16px;"/>
                                <ext:Column ColumnId="Fornecedor" Header="Fornecedor"  Width="276" DataIndex="Fornecedor" Hidden="False" Css="font-family : Arial; 	font-size : 16px;" />
                                <ext:Column ColumnId="Bitola" Header="Bitola" Align="Right"   Width="65" DataIndex="Bitola" Hidden="False" Css="font-family : Arial; 	font-size : 16px;" />
                               <ext:Column ColumnId="Descricao" Header="Material/Produto"   Width="208" DataIndex="Descricao" Hidden="False" Css="font-family : Arial; 	font-size : 16px;" />
                                <ext:Column ColumnId="Preco" Header="Preço" Align="Right"   Width="68" DataIndex="Preco" Hidden="False" Css="font-family : Arial; 	font-size : 16px;" />
                                <ext:Column ColumnId="Ipi" Header="Ipi" Align="Right"   Width="45" DataIndex="Ipi" Hidden="False" Css="font-family : Arial; 	font-size : 16px;">
                                <Renderer Fn="Ipi" />
                                </ext:Column>
                                <ext:Column ColumnId="Qtde" Align="Right"  Header="Qtde"  Width="60" DataIndex="Qtde" Hidden="False" Css="font-family : Arial; 	font-size : 16px;" />
                                <ext:Column ColumnId="Unidade" Align="Right"  Header="Unidade"  Width="60" DataIndex="Unidade" Hidden="False" Css="font-family : Arial; 	font-size : 16px;" />
                                <ext:DateColumn ColumnId="DataPrevista" Format="dd/MM/yyyy" Header="Entrega Prevista"  Width="98" DataIndex="DataPrevista" Hidden="False" Css="font-family : Arial; 	font-size : 16px;" />
                                <ext:Column ColumnId="QtdeEntregue" Align="Right" Header="Qtde Entregue"  Width="72" DataIndex="QtdeEntregue" Hidden="False" Css="font-family : Arial; 	font-size : 16px;" />
                                <ext:Column ColumnId="Saldo" Align="Right" Header="Saldo" Width="70" DataIndex="Saldo" Hidden="False" Css="font-family : Arial; 	font-size : 16px;" RightCommandAlign="True" />
                                <ext:Column ColumnId="Situacao" Header="Situação"  Width="78" DataIndex="Situacao" Hidden="False" Css="font-family : Arial; 	font-size : 16px;">
                                <Renderer fn="situacao"/>
                                </ext:Column>
                                <ext:Column ColumnId="DiaEmAtraso" Header="Dias de Atraso" Align="Right"  Width="70" DataIndex="DiaEmAtraso" Hidden="False" Css="font-family : Arial; 	font-size : 16px;">
                                <Renderer Fn="diasAtraso" />
                                </ext:Column>
                            </Columns>
                            </ColumnModel>
                            <Listeners>
                                <Command Handler="rowcommand(command, record);" />
                            </Listeners> 
                            </ext:GridPanel>
                            </Items>
                            </ext:Panel>
            </div>
            <asp:HiddenField ID="hdfCodPedidoCompra" runat="server" />
            <asp:HiddenField ID="hdfValor" runat="server" />
            <asp:HiddenField ID="hdfOpcao" runat="server" />
            <br />
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnPesquisar" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
