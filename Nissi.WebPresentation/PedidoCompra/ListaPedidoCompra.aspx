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

        function NovaEntradaEstoque(OrdemCompra, CodPessoa, Fornecedor, Bitola, Descricao, Qtde, CodMateriaPrima, CodBitola) {
            var hdfCodMateriaPrima = $get("<%=hdfCodMateriaPrima.ClientID%>");
            var hdfCodBitola = $get("<%=hdfCodBitola.ClientID%>");
            var txtBitola = $get("<%=txtBitola.ClientID%>");
            var txtNorma = $get("<%=txtNorma.ClientID%>");
            var txtQtdePedidoCompra = $get("<%=txtQtdePedidoCompra.ClientID%>");
            var txtPedidoCompraItem = $get("<%=txtPedidoCompraItem.ClientID%>");
            var hdfCodPedidoCompraItem = $get("<%=hdfCodPedidoCompraItem.ClientID %>");
            var txtFornecedor = $get("<%=txtFornecedor.ClientID%>");
            var hdfCodFornecedor = $get("<%=hdfCodFornecedor.ClientID%>");
            var txtLote = $get("<%=txtLote.ClientID%>");
            var btnNovoItem = $get("<%=btnNovoItem.ClientID %>");
            var hdfTipoAcaoItem = $get("<%=hdfTipoAcaoItem.ClientID %>");

             txtPedidoCompraItem.value = OrdemCompra;
             hdfCodPedidoCompraItem.value = OrdemCompra;
             hdfCodFornecedor.value = CodPessoa;
             txtFornecedor.value = Fornecedor;
             txtNorma.value = Descricao;
             txtBitola.value = Bitola;
             txtQtdePedidoCompra.value = Qtde;
             hdfCodMateriaPrima.value = CodMateriaPrima;
             hdfCodBitola.value = CodBitola;
             hdfTipoAcaoItem.value = "Incluir";
             btnNovoItem.click(); 
        }
        function cellClick(grid, rowIndex, colIndex, e) {
            CurrentColIndex = colIndex;     
            CurrentRow = rowIndex;
            var record = grid.getStore().getAt(rowIndex);
            grid.getView().getRow(rowIndex).style.backgroundColor = '#c8ffc8';
            // Get the Record     
            grid.getView().refreshRow(record);
        }

        function LimparCamposItemEntradaEstoque()
        {
            $get("<%=hdfTipoAcaoItem.ClientID%>").Value = "Incluir";
            $get("<%=hdfCodMateriaPrima.ClientID%>").value =
            $get("<%=hdfCodBitola.ClientID%>").value =
            $get("<%=txtLote.ClientID%>").value =
            $get("<%=txtCertificado.ClientID%>").value =
            $get("<%=txtCorrida.ClientID%>").value =
            $get("<%=txtBitola.ClientID%>").value =
            $get("<%=txtQtdePedidoCompra.ClientID%>").value =
            $get("<%=txtQtde.ClientID%>").value =
            $get("<%=txtValorUnit.ClientID%>").value =
            $get("<%=txtC.ClientID%>").value =
            $get("<%=txtSi.ClientID%>").value =
            $get("<%=txtMn.ClientID%>").value =
            $get("<%=txtP.ClientID%>").value =
            $get("<%=txtS.ClientID%>").value =
            $get("<%=txtCr.ClientID%>").value =
            $get("<%=txtNi.ClientID%>").value =
            $get("<%=txtMo.ClientID%>").value =
            $get("<%=txtCu.ClientID%>").value =
            $get("<%=txtTi.ClientID%>").value =
            $get("<%=txtN2.ClientID%>").value =
            $get("<%=txtCo.ClientID%>").value =
            $get("<%=txtAl.ClientID%>").value =
            $get("<%=txtResistenciaTracao.ClientID%>").value =
            $get("<%=txtDureza.ClientID%>").value =
            $get("<%=txtNorma.ClientID%>").value =
            $get("<%=txtBitola.ClientID%>").value =
            $get("<%=hdfCodMateriaPrima.ClientID%>").value =
            $get("<%=hdfCodBitola.ClientID%>").value =
            $get("<%=ddlUnidade.ClientID%>").value =
            $get("<%=txtIPI.ClientID%>").value =
            $get("<%=txtNota.ClientID%>").value = "";
            $get("<%=lkbArquivoPdf.ClientID%>").Text = "(Nenhum arquivo carregado)";
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
            if (command == "Estoque") {
                NovaEntradaEstoque(record.data.OrdemCompra, record.data.CodPessoa, record.data.Fornecedor, record.data.Bitola, record.data.Descricao, record.data.Qtde, record.data.CodMateriaPrima, record.data.CodBitola)
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
                                            <ext:RecordField Name="Lote" />
                                            <ext:RecordField Name="DataEmissao" Type="Date"/>
                                            <ext:RecordField Name="Fornecedor" />
                                            <ext:RecordField Name="Bitola" />
                                            <ext:RecordField Name="Descricao" />
                                            <ext:RecordField Name="Preco" />
                                            <ext:RecordField Name="Ipi" />
                                            <ext:RecordField Name="Unidade" />
                                            <ext:RecordField Name="Qtde" />
                                            <ext:RecordField Name="DataPrevista" Type="Date" />
                                            <ext:RecordField Name="DataEntrega" Type="Date" />
                                            <ext:RecordField Name="NotaFiscal" />
                                            <ext:RecordField Name="QtdeEntregue" />
                                            <ext:RecordField Name="Saldo" />
                                            <ext:RecordField Name="Situacao" />
                                            <ext:RecordField Name="DiaEmAtraso" />
                                            <ext:RecordField Name="CodMateriaPrima" />
                                            <ext:RecordField Name="CodBitola" />
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
                    <ext:Button ID="btnNovoItem" runat="server" Text="Novo" CtCls="botao" 
                        Width="80px" OnClientClick="ctl00_cphPrincipal_Window1.show();return false;"  />
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
                            Height="320" ColumnLines="True" CtCls="GridLayout">
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
                                        <ext:GridCommand Icon="New" CommandName="Estoque">
                                            <ToolTip Text="Entrada de Estoque" />
                                        </ext:GridCommand>
                                    </Commands>
                                </ext:CommandColumn>
                                <ext:Column ColumnId="OrdemCompra" Align="Right"  Header="Ordem de Compra" Width="160" DataIndex="OrdemCompra" Hidden="False" Css="font-family : Arial; 	font-size : 16px;" />
                                <ext:Column ColumnId="Lote" Align="Right"  Header="Lote" Width="160" DataIndex="Lote" Css="font-family : Arial; 	font-size : 16px;" />
                                <ext:Column ColumnId="CodPessoa" Align="Right"  Header="CodPessoa" Width="160" DataIndex="CodPessoa" Hidden="true" Css="font-family : Arial; 	font-size : 16px;" />
                                <ext:Column ColumnId="Tipo" Align="Right"  Header="Tipo" Width="160" DataIndex="Tipo" Hidden="true" Css="font-family : Arial; 	font-size : 16px;" />
                                <ext:DateColumn ColumnId="DataEmissao" Format="dd/MM/yyyy" Header="Emissão"  Width="90" DataIndex="DataEmissao" Hidden="False" Css="font-family : Arial; 	font-size : 16px;"/>
                                <ext:Column ColumnId="Fornecedor" Header="Fornecedor"  Width="276" DataIndex="Fornecedor" Hidden="False" Css="font-family : Arial; 	font-size : 16px;" />
                                <ext:Column ColumnId="Qtde" Align="Right"  Header="Qtde"  Width="60" DataIndex="Qtde" Hidden="False" Css="font-family : Arial; 	font-size : 16px;" />
                                <ext:Column ColumnId="QtdeEntregue" Align="Right" Header="Qtde Entregue"  Width="72" DataIndex="QtdeEntregue" Hidden="False" Css="font-family : Arial; 	font-size : 16px;" />
                                <ext:Column ColumnId="Unidade" Align="Right"  Header="Unidade"  Width="60" DataIndex="Unidade" Hidden="False" Css="font-family : Arial; 	font-size : 16px;" />
                                <ext:Column ColumnId="Bitola" Header="Bitola" Align="Right"   Width="65" DataIndex="Bitola" Hidden="False" Css="font-family : Arial; 	font-size : 16px;" />
                               <ext:Column ColumnId="Descricao" Header="Material/Produto"   Width="208" DataIndex="Descricao" Hidden="False" Css="font-family : Arial; 	font-size : 16px;" />
                                <ext:Column ColumnId="Preco" Header="Preço" Align="Right"   Width="68" DataIndex="Preco" Hidden="False" Css="font-family : Arial; 	font-size : 16px;" />
                                <ext:Column ColumnId="Ipi" Header="Ipi" Align="Right"   Width="45" DataIndex="Ipi" Hidden="False" Css="font-family : Arial; 	font-size : 16px;">
                                <Renderer Fn="Ipi" />
                                </ext:Column>
                                <ext:DateColumn ColumnId="DataPrevista" Format="dd/MM/yyyy" Header="Entrega Prevista"  Width="98" DataIndex="DataPrevista" Hidden="False" Css="font-family : Arial; 	font-size : 16px;" />
                                <ext:Column ColumnId="Situacao" Header="Situação"  Width="78" DataIndex="Situacao" Hidden="False" Css="font-family : Arial; 	font-size : 16px;">
                                <Renderer fn="situacao"/>
                                </ext:Column>
                                <ext:Column ColumnId="DiaEmAtraso" Header="Dias de Atraso" Align="Right"  Width="70" DataIndex="DiaEmAtraso" Hidden="False" Css="font-family : Arial; 	font-size : 16px;">
                                <Renderer Fn="diasAtraso" />
                                </ext:Column>
                                <ext:DateColumn ColumnId="DataEntrega" Format="dd/MM/yyyy" Header="Ent. Real"  Width="98" DataIndex="DataEntrega" Hidden="False" Css="font-family : Arial; 	font-size : 16px;" />
                                <ext:Column ColumnId="NotaFiscal"  Header="N.F."  Width="98" DataIndex="NotaFiscal" Hidden="False" Css="font-family : Arial; 	font-size : 16px;" />
                                <ext:Column ColumnId="Saldo" Align="Right" Header="Saldo" Width="70" DataIndex="Saldo" Hidden="False" Css="font-family : Arial; 	font-size : 16px;" RightCommandAlign="True" />
                            </Columns>
                            </ColumnModel>

                            <Listeners>
                                <Command Handler="rowcommand(command, record);" />
                                <CellClick Fn="cellClick" />
                            </Listeners> 
                            </ext:GridPanel>
                            </Items>
                            </ext:Panel>
            </div>
            <asp:HiddenField ID="hdfCodPedidoCompra" runat="server" />
            <asp:HiddenField ID="hdfValor" runat="server" />
            <asp:HiddenField ID="hdfOpcao" runat="server" />
            <br />
            <br />
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnPesquisar" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
        <ext:Window 
            ID="Window1" 
            runat="server" 
            Title="Inclusão de Item de Entrada"  
            Icon="Application"
            Height="600px" 
            Width="950px"
            BodyStyle="background-color: #fff;" 
            Padding="0"
            Collapsible="true" 
            Modal="true"
            Hidden="true">
            <Content>
                <ext:Hidden runat="server" ID="hdfTipoAcaoItem" />
                <div style="text-align: center; width: 950px; height: auto; padding: 5px 5px 5px 0px;
                    background-color: #ffffff;">
                    <table width="100%" align="center" class="fundoTabela">
                        <tr>
                            <td style="padding-left: 20px;">
                                Lote
                            </td>
                            <td style="padding-left: 20px;">
                                Entrega
                            </td>
                            <td style="padding-left: 20px;" colspan="2">
                                Norma<ext:Hidden ID="hdfCodMateriaPrima" runat="server">
                                </ext:Hidden>
                            </td>
                            <td style="padding-left: 20px;" colspan="2">
                                Bitola<ext:Hidden ID="hdfCodBitola" runat="server">
                                </ext:Hidden>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <ext:TextField ID="txtLote" runat="server"></ext:TextField>
                            </td>
                            <td>
                                <ext:TextField ID="txtData" runat="server" Cls="dataEmisssao"></ext:TextField>
                            </td>
                            <td colspan="2">
                                <ext:TextField ID="txtNorma" runat="server" Width="300px"></ext:TextField>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                        ControlToValidate="txtNorma" 
                                        ErrorMessage="Matéria Prima falta ser preenchido." 
                                        ValidationGroup="ValidaDadosItens">*</asp:RequiredFieldValidator>
                            </td>
                            <td colspan="2">
                                <ext:TextField ID="txtBitola" runat="server" Width="100px"></ext:TextField>
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
                                Fornecedor<ext:Hidden ID="hdfCodFornecedor" runat="server">
                                </ext:Hidden>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <ext:TextField ID="txtPedidoCompraItem" runat="server"  Cls="DesligarTextBox"></ext:TextField>
                                <ext:Hidden ID="hdfCodPedidoCompraItem" runat="server"></ext:Hidden>
                            </td>
                            <td colspan="5">
                                <ext:TextField ID="txtFornecedor" runat="server" Width="500px" Cls="DesligarTextBox"></ext:TextField>
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
                        <td><ext:TextField ID="txtCertificado" runat="server"></ext:TextField></td>
                        <td><ext:TextField ID="txtNotaFiscalItem" runat="server"></ext:TextField></td>
                        <td><ext:TextField ID="txtDataNotaFiscalItem" CtCls="dataEmissao" runat="server"></ext:TextField></td>
                            <td colspan="3">
                                <ext:TextField ID="txtEspecificacao" runat="server"></ext:TextField></td>
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
                        <td><ext:TextField ID="txtCorrida" runat="server"></ext:TextField></td>
                        <td><ext:TextField ID="txtQtdePedidoCompra" runat="server" Cls="DesligarTextBox"></ext:TextField>
                            </td>
                        <td><ext:TextField ID="txtQtde" runat="server"></ext:TextField>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" 
                                ControlToValidate="txtQtde" ErrorMessage="Qtde/Kilo falta ser preenchido." 
                                ValidationGroup="ValidaDadosItens">*</asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <ext:SelectBox 
                                ID="ddlUnidade"
                                runat="server"
                                DisplayField="TipoUnidade"
                                ValueField="CodUnidade"
                                EmptyText="Selecione a Unidade...">
                                <Store>
                                    <ext:Store runat="server">
                                        <Reader>
                                            <ext:ArrayReader>
                                                <Fields>
                                                    <ext:RecordField Name="CodUnidade">
                                                    </ext:RecordField>
                                                    <ext:RecordField Name="TipoUnidade">
                                                    </ext:RecordField>
                                                    <ext:RecordField Name="Unidade">
                                                    </ext:RecordField>
                                                </Fields> 
                                            </ext:ArrayReader>
                                        </Reader>
                                    </ext:Store>
                                </Store>                                </ext:SelectBox>

                            </td>
                        <td>
                            <ext:TextField ID="txtIPI" runat="server" onkeypress="OnlyMoney();" Width="50px"></ext:TextField>
                        </td>
                                                        <td>
                                                            <ext:TextField ID="txtValorUnit" runat="server"></ext:TextField>
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
                                                <ext:TextField ID="txtAl" runat="server" Width="40px" 
                                                    ontextchanged="txtAl_TextChanged" AutoPostBack="True"></ext:TextField>
                                                <span id="spanAl" title="" class="asterisco" runat="server" style="display:none">*</span>
                                            </td>
                                            <td style="width: 80px;">
                                                <ext:TextField ID="txtC" runat="server" Width="40px" AutoPostBack="True" 
                                                    ontextchanged="txtC_TextChanged"></ext:TextField>
                                                <span id="spanC" class="asterisco" runat="server" style="display:none">*</span>
                                            </td>
                                            <td style="width: 71px">
                                                <ext:TextField ID="txtSi" runat="server" Width="40px" AutoPostBack="True" 
                                                    ontextchanged="txtSi_TextChanged"></ext:TextField>
                                                <span id="spanSi" class="asterisco" runat="server" style="display:none">*</span>
                                            </td>
                                            <td>
                                                <ext:TextField ID="txtMn" runat="server" Width="40px" AutoPostBack="True" 
                                                    ontextchanged="txtMn_TextChanged"></ext:TextField>
                                                <span id="spanMn" class="asterisco" runat="server" style="display:none">*</span>
                                            </td>
                                            <td>
                                                <ext:TextField ID="txtP" runat="server" Width="40px" AutoPostBack="True" 
                                                    ontextchanged="txtP_TextChanged"></ext:TextField>
                                                <span id="spanP" class="asterisco" runat="server" style="display:none">*</span>
                                            </td>
                                            <td>
                                                <ext:TextField ID="txtS" runat="server" Width="40px" AutoPostBack="True" 
                                                    ontextchanged="txtS_TextChanged"></ext:TextField>
                                                <span id="spanS" class="asterisco" runat="server" style="display:none">*</span>
                                            </td>
                                            <td>
                                                <ext:TextField ID="txtCr" runat="server" Width="40px" AutoPostBack="True" 
                                                    ontextchanged="txtCr_TextChanged"></ext:TextField>
                                                <span id="spanCr" class="asterisco" runat="server" style="display:none">*</span>
                                            </td>
                                            <td>
                                                <ext:TextField ID="txtNi" runat="server" Width="40px" AutoPostBack="True" 
                                                    ontextchanged="txtNi_TextChanged"></ext:TextField>
                                                <span id="spanNi" class="asterisco" runat="server" style="display:none">*</span>
                                            </td>
                                            <td>
                                                <ext:TextField ID="txtMo" runat="server" Width="40px" AutoPostBack="True" 
                                                    ontextchanged="txtMo_TextChanged"></ext:TextField>
                                                <span id="spanMo" class="asterisco" runat="server" style="display:none">*</span>
                                            </td>
                                            <td>
                                                <ext:TextField ID="txtCu" runat="server" Width="40px" AutoPostBack="True" 
                                                    ontextchanged="txtCu_TextChanged"></ext:TextField>
                                                <span id="spanCu" class="asterisco" runat="server" style="display:none">*</span>
                                            </td>
                                            <td>
                                                <ext:TextField ID="txtTi" runat="server" Width="40px" AutoPostBack="True" 
                                                    ontextchanged="txtTi_TextChanged"></ext:TextField>
                                                <span id="spanTi" class="asterisco" runat="server" style="display:none">*</span>
                                            </td>
                                            <td>
                                                <ext:TextField ID="txtN2" runat="server" Width="40px" AutoPostBack="True" 
                                                    ontextchanged="txtN2_TextChanged"></ext:TextField>
                                                <span id="spanN2" class="asterisco" runat="server" style="display:none">*</span>
                                            </td>
                                            <td style="border-right: 1px solid black">
                                                <ext:TextField ID="txtCo" runat="server" Width="40px" AutoPostBack="True" 
                                                    ontextchanged="txtCo_TextChanged"></ext:TextField>
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
                                                <ext:TextField ID="txtResistenciaTracao" runat="server" 
                                                    ontextchanged="txtResistenciaTracao_TextChanged" Width="90px" AutoPostBack="True"></ext:TextField>
                                                <span id="spanRt" class="asterisco" runat="server" style="display:none">*</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left: 20px; width: 142px; padding-top: 5px; border-left: 1px solid black;">
                                                Dureza:
                                            </td>
                                            <td style="padding-top: 5px; border-right: 1px solid black;">
                                                <ext:TextField ID="txtDureza" runat="server" Width="90px"></ext:TextField>
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
                                                <ext:TextField ID="txtNota" runat="server" Width="177px" onblur="getSituacao(this.value)"></ext:TextField>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left: 20px; width: 142px; padding-top: 5px; border-left: 1px solid black;">
                                                Situação:
                                            </td>
                                            <td style="padding-top: 5px; border-right: 1px solid black;">
                                                <ext:TextField ID="txtSituacao" runat="server" Cls="DesligarTextBox situacao" 
                                                    Width="285px"></ext:TextField>
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
                                <ext:TextArea ID="txtObservacaoItem" Width="900px" runat="server"></ext:TextArea>
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
		                                    <ext:FormPanel runat="server" ID="pnl2" FileUpload="true" RenderFormElement="true" Layout="Form" Padding="5">
			                                <Items>
				                        <ext:FileUploadField runat="server" ID="upFileUp" Name="file" AllowBlank="false" Width="100px" ButtonText="Navegar..." />
				                            <ext:Button ID="btnUploadFile" runat="server" text="Carregar arquivo" OnClientClick="
					                            if(!#{upFileUp}.isValid()){
						                            Ext.Msg.alert('Error', 'Por favor selecione um arquivo para upload');
						                            return;
					                            }
					
					                            #{pnl2}.getForm().submit({ url: #{pnl2}.submitUrl,
						                            waitMsg: 'Carregando... Por favor aguarde...',
						                            success: function(fp, o) {
							                            Ext.Msg.alert('Success', o.result.msg);
						                            },
						                            failure: function(form, action) {
							                            switch (action.failureType) {
								                            case Ext.form.Action.CLIENT_INVALID:
									                            Ext.Msg.alert('Falha', 'O campo do formulário não pode fazer upload com valores inválidos');
									                            break;
								                            case Ext.form.Action.CONNECT_FAILURE:
									                            Ext.Msg.alert('Falhar', 'A comunicação Ajax falhou');
									                            break;
								                            case Ext.form.Action.SERVER_INVALID:
									                            Ext.Msg.alert('Faolha', action.result.msg);
							                            }
						                            }
					                            });
				                            " />

			                            </Items>
                  <Buttons>
                     <ext:Button ID="Button1" runat="server" Text="Submit this form">
                        <DirectEvents>
                            <Click OnEvent="Submit1" FormID="pnl2" />
                        </DirectEvents>
                     </ext:Button>
                  </Buttons>
		</ext:FormPanel>
                                        </td>
		                                <td>
                                                <asp:LinkButton ID="lkbArquivoPdf" runat="server" 
                                                    onclick="lkbArquivoPdf_Click"> (Nenhuma arquivo carregada)</asp:LinkButton>
                                            </td>
                                     </tr>
                                     <tr>
                                            <td colspan="2">
                                                        <asp:LinkButton ID="btnCarregarCertificado" runat="server" 
                                                            CtCls="hiperlink" onclick="btnCarregarImagem_Click">Carregar Certificado</asp:LinkButton>
                                                        <asp:LinkButton ID="btnLimparImagem" runat="server" CausesValidation="False" 
                                                        CtCls="hiperlink" style="padding-bottom:2px" Width="120px" 
                                                            onclick="btnLimparImagem_Click">Limpar Arquivo</asp:LinkButton>

                                            </td>                                       
                                     </tr>
                                </table>
                        </td>
                        </tr>
                        <tr>
                        <td colspan="2" style="padding-left:90px">
                        <ext:Button ID="btnCancelarItem" Width="100px" runat="server" 
                                Text="Cancelar" OnClientClick="LimparCamposItemEntradaEstoque();ctl00_cphPrincipal_Window1.hide();return false;" Cls="botao" /></td>
                        <td colspan="3"></td>
                        <td>
                        <ext:Button ID="btnIncluirItem" runat="server" Cls="botao" Width="100px" Text="Salvar" 
                                OnDirectClick="btnIncluirItem_Click" ValidationGroup="ValidaDadosItens" />
                                </td>
                        </tr>
                    </table>
                </div>
            </Content>
        </ext:Window>
 </asp:Content>
