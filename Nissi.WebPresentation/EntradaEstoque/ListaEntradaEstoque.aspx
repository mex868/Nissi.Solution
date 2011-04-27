<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="ListaEntradaEstoque.aspx.cs" Inherits="Nissi.WebPresentation.EntradaEstoque.ListaEntradaEstoque" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
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
            $get('<%=txtEntradaEstoque.ClientID%>').value = ''
            $get('<%=txtLote.ClientID%>').value = '';
            $get('<%=txtRazaoSocial.ClientID%>').value = '';
            $get('<%=txtCorrida.ClientID%>').value = '';
            $get('<%=txtCertificado.ClientID%>').value = '';
            $get('divEntradaEstoque').style.display = 'none';
            $get('divLote').style.display = 'none';
            $get('divRazaoSocial').style.display = 'none';
            $get('divListaEntradaEstoque').style.display = 'block';
            $get('divCorrida').style.display = 'none';
            $get('divCertificado').style.display = 'none';
            $get('divPeriodo').style.display = 'none';
            switch (tvar) {
                case 1: //Pesquisa por EntradaEstoque
                    $get('divEntradaEstoque').style.display = 'block';

                    break;
                case 2: //Pesquisa por Lote
                    $get('divLote').style.display = 'block';
                    break;
                case 3: //Pesquisa por Razao Social
                    $get('divRazaoSocial').style.display = 'block';
                    break;
                case 4: //Pesquisa por Código do Corrida
                    $get('divCorrida').style.display = 'block';
                    break;
                case 5: //Pesquisa por Código do Certificado
                    $get('divCertificado').style.display = 'block';
                    break;
                case 6: //Pesquisa por periódo
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
            if (($get('<%=rbEntradaEstoque.ClientID%>').checked) && ($get('<%=txtEntradaEstoque.ClientID %>').value != '')) {
                return true;
            }
            if (($get('<%=rbLote.ClientID%>').checked) && ($get('<%=txtLote.ClientID %>').value != '')) {
                return true;
            }

            if (($get('<%=rbCorrida.ClientID%>').checked) && $get('<%=txtCorrida.ClientID %>').value != '') {
                return true;
                }
            if (($get('<%=rbCertificado.ClientID%>').checked) && $get('<%=txtCertificado.ClientID %>').value != '') {
                return true;
            }

            if (($get('<%=rbRazaoSocial.ClientID%>').checked) && ($get('<%=txtRazaoSocial.ClientID%>').value != '')) {
                return true;
                
            }
            else {
                if ($get('<%=rbEntradaEstoque.ClientID%>').checked) {
                    alert("Informe o número da entrada de estoque");
                    return false;
                }
                if ($get('<%=rbLote.ClientID%>').checked) {
                    alert("Informe o número do Lote.");
                    return false;
                }
                else if ($get('<%=rbRazaoSocial.ClientID%>').checked) {
                    alert('Informe a Razão Social do Fornecedor.');
                    return false;
                }
                else if ($get('<%=rbCorrida.ClientID %>').checked) {
                    alert('Informe o Código do Corrida.');
                    return false;
                }
                if ($get('<%=rbCertificado.ClientID %>').checked) {
                    alert('Informe o Código do Certificado.');
                    return false;
                }
            }
           
            return false;
        }
        function ChamaEntradaEstoque(codEntradaEstoque, tipo) {
            var rbEntradaEstoque = $get("<%=rbEntradaEstoque.ClientID %>");
            var txtEntradaEstoque = $get("<%=txtEntradaEstoque.ClientID %>");
            var rbLote = $get("<%=rbLote.ClientID %>");
            var txtLote = $get("<%=txtLote.ClientID %>");
            var rbRazaoSocial = $get("<%=rbRazaoSocial.ClientID %>");
            var txtRazaoSocial = $get("<%=txtRazaoSocial.ClientID %>");
            var rbCorrida = $get("<%=rbCorrida.ClientID %>");
            var txtCorrida = $get("<%=txtCorrida.ClientID %>");
            var rbCertificado = $get("<%=rbCertificado.ClientID %>");
            var txtCertificado = $get("<%=txtCertificado.ClientID %>");
            var hdfValor = $get("<%=hdfValor.ClientID %>");
            var hdfOpcao = $get("<%=hdfOpcao.ClientID %>");
            if (rbEntradaEstoque.checked && txtEntradaEstoque.value != "") {
                hdfValor.Value = txtEntradaEstoque.value;
                hdfOpcao.Value = "EntradaEstoque";
            }
            if (rbLote.checked && txtLote.value != "") {
                hdfValor.Value = txtLote.value;
                hdfOpcao.Value = "Lote";
            }
            if (rbRazaoSocial.checked && txtRazaoSocial.value) {
                hdfValor.Value = txtRazaoSocial.value;
                hdfOpcao.Value = "RazaoSocial";
            }
            if (rbCorrida.checked && txtCorrida.value) {
                hdfValor.Value = txtCorrida.value;
                hdfOpcao.Value = "Corrida";
            }
            if (rbCertificado.checked && !string.IsNullOrEmpty(txtCertificado.value)) {
                hdfValor.Value = txtCertificado.value;
                hdfOpcao.Value = "Certificado";
            }
            WaitAsyncPostBack(true);
            window.location="CadastraEntradaEstoque.aspx?acao=Editar&CodEntradaEstoque=" +
                                          codEntradaEstoque + "&tipo=" + tipo +
                                          "&valor=" + hdfValor.Value + "&opcao=" + hdfOpcao.Value;
        }
        function ChamaDuplicata(tvar) {
            window.open("../Relatorios/relDuplicata.aspx?CodEntradaEstoque=" + tvar + "", "_blank", "top=0,left=0,width=800,height=600,scrollbars=yes,resizable=no,toolbar=no");
        }
        //--------------------------------------------------------------------------------
        //Criado por...: Alexandre Maximiano - 12/09/2010
        //Objetivo.....: Limpa Campo Codigo/Descricao
        //--------------------------------------------------------------------------------
        function ChamaVisualizarEntradaEstoque(tvar) {
            var w = window.screen.width;
            window.open("VisualizarEntradaEstoque.aspx?CodEntradaEstoque=" + tvar + "", "_blank", "top=0,left=0,width="+w+",height=600,scrollbars=yes,resizable=no,toolbar=no");
        }

        //--------------------------------------------------------------------------------
        //Criado por...: Alexandre Maximiano - 12/09/2010
        //Objetivo.....: Visualizar Certificado Cadastrado
        //--------------------------------------------------------------------------------
        function ChamaVisualizarCertificado(lote) {
            //WaitAsyncPostBack(true);
            //WaitAsyncPostBack(false);
            lkbArquivoPdf = $get("<%=lkbArquivoPdf.ClientID %>");
            hdfLote = $get("<%=hdfLote.ClientID %>");
            hdfLote.value = lote;
            lkbArquivoPdf.click();
        }

        //--------------------------------------------------------------------------------
        //Criado por...:Alexandre Maximiano - 04/11/2010
        //Objetivo.....: Efetua consulta com o retorno do autocomplete
        //--------------------------------------------------------------------------------
        function CarregarValores(source, eventArgs) {
            $get('<%=hdfIdRazaoSocial.ClientID%>').value = eventArgs.get_value();
            $get('<%=txtRazaoSocial.ClientID %>').value = eventArgs._item.outerText;
            $get('<%=btnPesquisarExt.ClientID%>').click();
        }
        function BuscaDados() {
            $get('<%=btnPesquisarExt.ClientID%>').click();
        }
        //--------------------------------------------------------------------------------
        //Criado por...: Alexandre Maximiano - 02/11/2009
        //Objetivo.....: Acionar botão acessar quando pressionada a tecla ENTER
        //--------------------------------------------------------------------------------
        function KeyDownHandler() {
            if (event.keyCode == 13) {
                event.returnValue = false;
                event.cancel = true;
                $get('<%=btnPesquisarExt.ClientID%>').click();
            }
        }
        var template = '<span style="color:{0};">{1}</span>';

        var Ipi = function (value) {
            return String.format(template, 'black', value + "%");
        };
        var diasAtraso = function (value) {
            return String.format(template, (value > 0) ? "green" : "red", value);
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
                ChamaEntradaEstoque(record.data.CodEntradaEstoque, record.data.Tipo);
            }
            if (command == "Certificado") {
                ChamaVisualizarCertificado(record.data.Lote);
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
                Cadastro de Entrada de Mão de Obra
            </td>
        </tr>
    </table>
    <br />
    <table class="fundoTabela" style="text-align: left; width: 95%">
        <tr>
            <td style="width: 15%; padding-left: 17px">
                Opções de Consulta:
            </td>
            <td style="width: 15%; display:none">
                <asp:RadioButton ID="rbEntradaEstoque" onclick="TipoPesquisa(1)" 
                    GroupName="filtroPesq" runat="server"
                    Text="Nº da Entrada" CssClass="noBorder" />
            </td>
            <td style="width: 10%">
                <asp:RadioButton ID="rbLote" onclick="TipoPesquisa(2)" GroupName="filtroPesq"
                    runat="server" Text="Lote" CssClass="noBorder" Checked="True" />
            </td>
            <td style="width: 15%">
                <asp:RadioButton ID="rbCorrida" onclick="TipoPesquisa(4)" runat="server" Text="Corrida"
                    GroupName="filtroPesq" CssClass="noBorder" />
            </td>
           <td style="width: 15%">
                <asp:RadioButton ID="rbCertificado" onclick="TipoPesquisa(5)" runat="server" Text="Certificado"
                    GroupName="filtroPesq" CssClass="noBorder" />
            </td>
            <td style="width: 10%">
                <asp:RadioButton ID="rbRazaoSocial" onclick="TipoPesquisa(3)" runat="server" Text="Fornecedor"
                    GroupName="filtroPesq" CssClass="noBorder" />
            </td>
            <td style="width: 15%">
                <asp:RadioButton ID="rbPeriodo" onclick="TipoPesquisa(6)" runat="server" Text="Por Data" GroupName="filtroPesq"/></td>
        </tr>
        <tr>
            <td style="width: 20%">
                &nbsp;
            </td>
            <td colspan="6">
                <div id="divEntradaEstoque" style="display: none">
                    <asp:TextBox ID="txtEntradaEstoque" MaxLength="14" onkeypress="return digitos(event, this);KeyDownHandler();"
                        runat="server" Height="16px" Width="96px"></asp:TextBox>
                    <asp:CustomValidator ClientValidationFunction="ValidaCampos" Text="*" CssClass="asterisco"
                        ValidationGroup="pesquisar" ErrorMessage="Favor informar Ordem de Compra" runat="server"
                        ID="CustomValidator1"></asp:CustomValidator>
                </div>
                <div id="divLote" style="display: none">
                    <asp:TextBox ID="txtLote" onkeypress="KeyDownHandler();" runat="server"></asp:TextBox>
                    <asp:CustomValidator ClientValidationFunction="ValidaCampos" Text="*" CssClass="asterisco"
                        ValidationGroup="pesquisar" ErrorMessage="Favor informar o Lote" runat="server"
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
                <div id="divCorrida" style="display: none">
                    <asp:TextBox ID="txtCorrida" onkeypress="KeyDownHandler();" runat="server" ></asp:TextBox>
                    <asp:CustomValidator ClientValidationFunction="ValidaCampos" Text="*" CssClass="asterisco"
                        ValidationGroup="pesquisar" ErrorMessage="Favor informar a Corrida"
                        runat="server" ID="CustomValidator3"></asp:CustomValidator>
                </div>
                <div id="divCertificado" style="display: none">
                    <asp:TextBox ID="txtCertificado" onkeypress="KeyDownHandler();" runat="server"></asp:TextBox>
                    <asp:CustomValidator ClientValidationFunction="ValidaCampos" Text="*" CssClass="asterisco"
                        ValidationGroup="pesquisar" ErrorMessage="Favor informar a Certificado"
                        runat="server" ID="CustomValidator4"></asp:CustomValidator>
                </div>
                <div id="divPeriodo" style="display: block">
                De:<asp:TextBox ID="txtDataInicio" onkeypress="KeyDownHandler();" runat="server" CssClass="dataEmissao"></asp:TextBox>
                Até:<asp:TextBox ID="txtDataFim" onkeypress="KeyDownHandler();" runat="server" CssClass="dataEmissao"></asp:TextBox>
                </div>
            </td>

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
                        <asp:HiddenField ID="hdfLote" runat="server" />
                        <asp:LinkButton ID="lkbArquivoPdf" runat="server" onclick="lkbArquivoPdf_Click"> (Nenhuma arquivo carregada)</asp:LinkButton>
                        <asp:Button ID="btnPesquisar" OnClientClick="return ValidaCampos()" runat="server"
                            ValidationGroup="pesquisar" CssClass="botao" Text="Pesquisar" Width="100px" OnClick="btnPesquisar_Click" />
                        &nbsp;<asp:Button ID="btnIncluir" runat="server" CssClass="botao" Width="100px" Text="Incluir Novo"
                            OnClick="btnIncluir_Click" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="lkbArquivoPdf" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <br />
    <asp:UpdatePanel ID="udpListaResultado" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="divListaEntradaEstoque" style="display: block;Width:95%;padding-left:35px">
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
                                    runat="server" ImageUrl="~/Imagens/btn-SolicitacaoDocumentos.gif" 
                                    Visible="False" />
                                <asp:ImageButton ID="imgVisualizarEntradaEstoque" runat="server" Height="15px" 
                                    ImageUrl="~/Imagens/btn-SolicitacaoDocumentos.gif" Style="cursor: pointer" 
                                    Width="15px" Visible="False" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="headerGrid" Width="5%" />
                            <ItemStyle HorizontalAlign="center" Wrap="false" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Lote">
                            <HeaderStyle CssClass="headerGrid" />
                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Corrida">
                            <HeaderStyle CssClass="headerGrid" />
                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Certificado">
                            <HeaderStyle CssClass="headerGrid" />
                            <ItemStyle HorizontalAlign="Left" Width="8%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Fornecedor">
                            <ItemStyle HorizontalAlign="Left" Width="25%" />
                            <HeaderStyle CssClass="headerGrid" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Produto">             
                            <HeaderStyle CssClass="headerGrid" />
                           <ItemStyle HorizontalAlign="Left" Width="30%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Qtde">
                            <HeaderStyle CssClass="headerGrid" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Entregue" >
                                                    <HeaderStyle CssClass="headerGrid" />
                            <ItemStyle HorizontalAlign="Left" Width="8%" />
                       </asp:BoundField>
                    </Columns>
                </cc1:RDCGrid>
                    <ext:ResourceManager ID="ResourceManager1" runat="server" />
                            <ext:Store ID="StoreListaResultado" runat="server" 
                    onrefreshdata="StoreListaResultado_RefreshData">
                                <Reader>
                                    <ext:JsonReader IDProperty="Name">
                                        <Fields>
                                            <ext:RecordField Name="CodEntradaEstoque" />
                                            <ext:RecordField Name="CodPessoa" />
                                            <ext:RecordField Name="Tipo" />
                                            <ext:RecordField Name="Lote"/>
                                            <ext:RecordField Name="Corrida" />
                                            <ext:RecordField Name="Certificado" />
                                            <ext:RecordField Name="Fornecedor" />
                                            <ext:RecordField Name="Descricao" />
                                            <ext:RecordField Name="Unidade" />
                                            <ext:RecordField Name="Qtd" />
                                            <ext:RecordField Name="Entregue" Type="Date"  />
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
                            Title="Lista de Entrada de Mão de Obra"
                            AutoExpandColumn="CodEntradaEstoque" 
                            Collapsible="true" 
                            EnableColumnMove="true"
                            Height="350" ColumnLines="True" CtCls="GridLayout">
                           <ColumnModel ID="ColumnModel1" runat="server">
		                    <Columns>
                                <ext:CommandColumn Width="85" Header="Ações">
                                    <Commands>
                                        <ext:GridCommand Icon="NoteEdit" CommandName="Editar">
                                            <ToolTip Text="Editar dados da Entrada de Estoque" />
                                        </ext:GridCommand>
                                         <ext:CommandSeparator />
                                         <ext:GridCommand Icon="Delete" CommandName="Excluir">
                                            <ToolTip Text="Cancelar dados da Entrada de Estoque" />
                                        </ext:GridCommand>
                                         <ext:CommandSeparator />
                                        <ext:GridCommand Icon="Note" CommandName="Certificado">
                                            <ToolTip Text="Visualizar Certificado" />
                                        </ext:GridCommand>
                                    </Commands>
                                </ext:CommandColumn>
                                <ext:Column ColumnId="CodEntradaEstoque" Align="Right"  Header="CodEntradaEstoque" DataIndex="CodEntradaEstoque" Hidden="true" Css="font-family : Arial; 	font-size : 16px;" />
                                <ext:Column ColumnId="CodPessoa" Align="Right"  Header="CodPessoa" DataIndex="CodPessoa" Hidden="true" Css="font-family : Arial; 	font-size : 16px;" />
                                <ext:Column ColumnId="Tipo" Align="Right"  Header="Tipo" DataIndex="Tipo" Hidden="true" Css="font-family : Arial; 	font-size : 16px;" />
                                <ext:Column ColumnId="Lote" Header="Lote" Align="Right"   Width="80" DataIndex="Lote" Hidden="False" Css="font-family : Arial; 	font-size : 16px; border-top: 1px solid black; border-left:1px solid black; border-right: 1px solid black; border-bottom: 1px solid black;" />
                                <ext:Column ColumnId="Corrida" Header="Corrida" Align="Right"   Width="80" DataIndex="Corrida" Hidden="False" Css="font-family : Arial; 	font-size : 16px; border-top: 1px solid black; border-right: 1px solid black; border-bottom: 1px solid black;" />
                                <ext:Column ColumnId="Certificado" Header="Certificado" Align="Right"   Width="80" DataIndex="Certificado" Hidden="False" Css="font-family : Arial; 	font-size : 16px; border-top: 1px solid black; border-right: 1px solid black; border-bottom: 1px solid black;"/>
                                <ext:Column ColumnId="Fornecedor" Header="Fornecedor"  Width="276" DataIndex="Fornecedor" Hidden="False" Css="font-family : Arial; 	font-size : 16px; border-top: 1px solid black; border-right: 1px solid black; border-bottom: 1px solid black;" />
                               <ext:Column ColumnId="Descricao" Header="Material/Produto"   Width="208" DataIndex="Descricao" Hidden="False" Css="font-family : Arial; 	font-size : 16px; border-top: 1px solid black; border-right: 1px solid black; border-bottom: 1px solid black;" />
                                <ext:Column ColumnId="Unidade"  Header="Unidade"  Width="72" DataIndex="Unidade" Hidden="False" Css="font-family : Arial; 	font-size : 16px; border-top: 1px solid black; border-right: 1px solid black; border-bottom: 1px solid black;" />
                                <ext:Column ColumnId="Qtd" Align="Right"  Header="Qtde"  Width="60" DataIndex="Qtd" Hidden="False" Css="font-family : Arial; 	font-size : 16px; border-top: 1px solid black; border-right: 1px solid black; border-bottom: 1px solid black;" />
                                <ext:DateColumn ColumnId="Entregue" Format="dd/MM/yyyy" Header="Entregue" Width="90" DataIndex="Entregue" Hidden="False" Css="font-family : Arial; 	font-size : 16px; border-top: 1px solid black; border-right: 1px solid black; border-bottom: 1px solid black;" RightCommandAlign="True" />
                            </Columns>
                            </ColumnModel>
                            <Listeners>
                                <Command Handler="rowcommand(command, record);" />
                            </Listeners> 
                            </ext:GridPanel>
                            </Items>
                            </ext:Panel>
                
            </div>
            <asp:HiddenField ID="hdfCodEntradaEstoque" runat="server" />
            <asp:HiddenField ID="hdfValor" runat="server" />
            <asp:HiddenField ID="hdfOpcao" runat="server" />
            <br />
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnPesquisar" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
