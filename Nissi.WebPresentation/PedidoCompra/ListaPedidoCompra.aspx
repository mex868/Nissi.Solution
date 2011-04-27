<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="ListaPedidoCompra.aspx.cs" Inherits="Nissi.WebPresentation.PedidoCompra.ListaPedidoCompra" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Assembly="RDC.Tools" Namespace="RDC.Tools" TagPrefix="cc1" %>
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="server">
    <script type="text/javascript" src="../JScripts/Common.js"></script>
    <script language="javascript" type="text/javascript" src="../JScripts/pedido-compra-consulta.js"></script>
    <style type="text/css">
        .dirty-row {
	        background: #FFFDD8;
        }
        .red-row {
	        background: #FF0000 !important;
	        color: #FFFFFF !important;
        } 
                .x-grid3-hd-inner        
        {            
        white-space: normal !important;                 
        }  
    .x-grid3-cell-inner       
    {           
        white-space: normal;
    }   
    </style>
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
            loadDate();
            uploadFileAjax();
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
                    Ext.Msg.alert("Informe o número do Pedido de Compra");
                    return false;
                }
                if ($get('<%=rbBitola.ClientID%>').checked) {
                    Ext.Msg.alert("Informe a Bitola.");
                    return false;
                }
                else if ($get('<%=rbRazaoSocial.ClientID%>').checked) {
                    Ext.Msg.alert('Informe a Razão Social do Fornecedor.');
                    return false;
                }
                else if ($get('<%=rbClasseTipo.ClientID %>').checked) {
                    Ext.Msg.alert('Informe a Classe/Tipo do Material.');
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
                        Ext.Msg.alert('Período final deve ser maior que Período inicial');
                        return false;
                    }
                    else if (strDataInicial != '') {
                        Ext.Msg.alert('Período final deve ser preenchido');
                        return false;
                    }
                    else if (strDataFinal != '') {
                        Ext.Msg.alert('Período inicial deve ser preenchido');
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
        //Objetivo.....: Visualizar Pedido de Compra
        //--------------------------------------------------------------------------------
        function ChamaVisualizarPedidoCompra(tvar) {
            WaitAsyncPostBack(true);
            window.open("../Relatorios/PedidoCompra.aspx?CodPedidoCompra=" + tvar + "&tipo=imprimir", "_blank", "top=0,left=0,width=800,height=600,scrollbars=yes,resizable=no,toolbar=no");
            WaitAsyncPostBack(false);
        }
        //--------------------------------------------------------------------------------
        //Criado por...: Alexandre Maximiano - 12/09/2010
        //Objetivo.....: Visualizar Certificado Cadastrado
        //--------------------------------------------------------------------------------
        function ChamaVisualizarCertificado(lote)
        {
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
<<<<<<< HEAD
<<<<<<< HEAD
            $get('<%=btnPesquisarExt.ClientID%>').click();
        }
        function BuscaDados() {
            $get('<%=btnPesquisarExt.ClientID%>').click();
=======
            $get('<%=btnPesquisar.ClientID%>').click();
        }
        function BuscaDados() {
            $get('<%=btnPesquisar.ClientID%>').click();
>>>>>>> 0e7e752c875b412e16e75c56679cd0618d11db3e
=======
            $get('<%=btnPesquisarExt.ClientID%>').click();
        }
        function BuscaDados() {
            $get('<%=btnPesquisarExt.ClientID%>').click();
>>>>>>> local
        }
        //--------------------------------------------------------------------------------
        //Criado por...: Alexandre Maximiano - 02/11/2009
        //Objetivo.....: Acionar botão acessar quando pressionada a tecla ENTER
        //--------------------------------------------------------------------------------
        function KeyDownHandler() {
            if (event.keyCode == 13) {
                event.returnValue = false;
                event.cancel = true;
<<<<<<< HEAD
<<<<<<< HEAD
=======
>>>>>>> local
                $get('<%=btnPesquisarExt.ClientID%>').click();
            }
        }

<<<<<<< HEAD
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
=======
                $get('<%=btnPesquisar.ClientID%>').click();
=======
        function NovaEntradaEstoque(record) {
            if (record.data.IdStatus == 0) {
                var hdfCodMateriaPrima = $get("<%=hdfCodMateriaPrima.ClientID%>");
                var hdfCodBitola = $get("<%=hdfCodBitola.ClientID%>");
                var hdfCodPedidoCompraItem = $get("<%=hdfCodPedidoCompraItem.ClientID %>");
                var hdfCodFornecedor = $get("<%=hdfCodFornecedor.ClientID %>");
                var ddlBitola = $get("<%=ddlBitolaItem.ClientID%>");
                var ddlMateriaPrima = $get("<%=ddlMateriaPrimaItem.ClientID%>");
                var ddlUnidade = $get("<%=ddlUnidade.ClientID%>");
                var txtQtdePedidoCompra = $get("<%=txtQtdePedidoCompra.ClientID%>");
                var txtPedidoCompraItem = $get("<%=txtPedidoCompraItem.ClientID%>");
                var txtFornecedor = $get("<%=txtFornecedor.ClientID%>");
                var txtLote = $get("<%=txtLote.ClientID%>");
                var hdfTipoAcaoItem = $get("<%=hdfTipoAcaoItem.ClientID %>");
                var txtIPI = $get("<%=txtIPI.ClientID %>");
                var txtValor = $get("<%=txtValorUnit.ClientID %>");
                var btnCarregarValores = $get("<%=btnCarregarValores.ClientID %>");
                var txtData = $get("<%=txtData.ClientID %>");
                var txtCertificado = $get("<%=txtCertificado.ClientID %>");
                var hdfCodItemPedidoCompra = $get("<%=hdfCodItemPedidoCompra.ClientID %>");
                hdfCodItemPedidoCompra.value = record.data.CodItemPedidoCompra;
                var data = new Date();
                txtPedidoCompraItem.value = record.data.OrdemCompra;
                hdfCodPedidoCompraItem.value = record.data.OrdemCompra;
                txtData.value = data.getDate() + "/" + eval(data.getMonth() + 1) + "/" + data.getFullYear();
                hdfCodFornecedor.value = record.data.CodPessoa;
                txtFornecedor.value = record.data.Fornecedor;
                ddlMateriaPrima.value = record.data.CodMateriaPrima;
                ddlBitola.value = record.data.CodBitola;
                ddlUnidade.value = record.data.CodUnidade;
                txtQtdePedidoCompra.value = record.data.Qtde;
                txtIPI.value = record.data.Ipi;
                txtValor.value = record.data.Preco;
                hdfTipoAcaoItem.value = "IncluirItem";
                btnCarregarValores.click();
                $find("mpeIncluirItem").show();
            }
            else {
                Ext.Msg.alert("Atenção", "Não é possível alterar o item finalizado ou cancelado!");
>>>>>>> local
            }
        }
        function cellClick(grid, rowIndex, colIndex, e) {
            CurrentColIndex = colIndex;     
//            CurrentRow = rowIndex;
//            var record = grid.getStore().getAt(rowIndex);
//            grid.getView().getRow(rowIndex).style.backgroundColor = '#c8ffc8';
//            // Get the Record     
//            grid.getView().refreshRow(record);
        }

<<<<<<< HEAD
>>>>>>> 0e7e752c875b412e16e75c56679cd0618d11db3e
=======
        function LimparCamposItemEntradaEstoque()
        {
            $get("<%=hdfTipoAcaoItem.ClientID%>").Value = "Incluir";
            $get("<%=hdfCodMateriaPrima.ClientID%>").value =
            $get("<%=hdfCodBitola.ClientID%>").value =
            $get("<%=txtLote.ClientID%>").value =
            $get("<%=txtCertificado.ClientID%>").value =
            $get("<%=txtCorrida.ClientID%>").value =
            $get("<%=ddlUnidade.ClientID%>").value =
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
            $get("<%=ddlBitolaItem.ClientID%>").value =
            $get("<%=ddlMateriaPrimaItem.ClientID%>").value =
            $get("<%=hdfCodMateriaPrima.ClientID%>").value =
            $get("<%=hdfCodBitola.ClientID%>").value =
            $get("<%=txtIPI.ClientID%>").value =
            $get("<%=txtNota.ClientID%>").value = "";
            $get("<%=lkbArquivoPdf.ClientID%>").Text = "(Nenhum arquivo carregado)";
            $find("mpeIncluirItem").hide();
        }
        
        function onClickEmail() {
            if (ctl00_cphPrincipal_grdListaResultado1.selModel.selection != null) {
                var record = ctl00_cphPrincipal_grdListaResultado1.selModel.selection.record;
                if (record.data.OrdemCompra != 0) {
                    EnviarPedidoCompra(record.data.OrdemCompra, record.data.CodPessoa);
                }
                else {
                    Ext.Msg.alert("Atenção", "Não é possível, enviar email do item sem Ordem de Compra");
                }
            }
            else {
                Ext.Msg.alert("Atenção", "Nenhuma linha foi selecionada!");
            }
        }
        function onClickImprimir() {
            if (ctl00_cphPrincipal_grdListaResultado1.selModel.selection != null) {
                var record = ctl00_cphPrincipal_grdListaResultado1.selModel.selection.record;
                if (record.data.OrdemCompra != 0) {
                    ChamaVisualizarPedidoCompra(record.data.OrdemCompra);
                }
                else {
                    Ext.Msg.alert("Atenção", "Não é possível, imprimir item sem Ordem de Compra");
                }
            }
            else {
                Ext.Msg.alert("Atenção", "Nenhuma linha foi selecionada!");
            }
        }
        function onClickCancelarItem() 
        {
                if (ctl00_cphPrincipal_grdListaResultado1.selModel.selection != null) 
                {
                    var record = ctl00_cphPrincipal_grdListaResultado1.selModel.selection.record;
                    if (record.data.OrdemCompra != 0) {
                Ext.Msg.confirm('Cancelar',
                'Confirmar cancelamento do Pedido de Compra?',
                function (btn) {
                    if (btn == 'yes') {

                        
                        var btnCancelarItem = $get("<%=btnCancelarItemHide.ClientID %>");
                        var hdfCodItemPedidoCompra = $get("<%=hdfCodItemPedidoCompra.ClientID %>");
                        hdfCodItemPedidoCompra.value = record.data.CodItemPedidoCompra;
                        btnCancelarItem.click();
                    }
                }, this, { stopEvent: true });
            }
            else {
                Ext.Msg.alert("Atenção", "Não é possível, cancelar item sem Ordem de Compra");
            }
                                        }
                        else
                            Ext.Msg.alert("Atenção","Nenhuma linha foi selecionada!");
            }
            function onClickFinalizarItem() {
                if (ctl00_cphPrincipal_grdListaResultado1.selModel.selection != null) {
                    var record = ctl00_cphPrincipal_grdListaResultado1.selModel.selection.record;
                    if (record.data.OrdemCompra != 0) {
                        Ext.Msg.confirm('Finalizar',
                    'Confirmar finalização do Pedido de Compra?',
                    function (btn) {
                        if (btn == 'yes') {
                            
                            var btnFinalizar = $get("<%=btnFinalizar.ClientID %>");
                            var hdfCodItemPedidoCompra = $get("<%=hdfCodItemPedidoCompra.ClientID %>");
                            hdfCodItemPedidoCompra.value = record.data.CodItemPedidoCompra;
                            btnFinalizar.click();
                        }
                    }, this, { stopEvent: true });
                    }
                else {
                    Ext.Msg.alert("Atenção", "Não é possível, finalizar item sem Ordem de Compra");
                    }
                        }
                        else
                            Ext.Msg.alert("Atenção","Nenhuma linha foi selecionada!");
                    
            }
            function onClickDesfazerItem() {
                if (ctl00_cphPrincipal_grdListaResultado1.selModel.selection != null) {
                    var record = ctl00_cphPrincipal_grdListaResultado1.selModel.selection.record;
                    if (record.data.OrdemCompra != 0) {
                        var btnDesfazer = $get("<%=btnDesfazer.ClientID %>");
                        var hdfCodItemPedidoCompra = $get("<%=hdfCodItemPedidoCompra.ClientID %>");
                        hdfCodItemPedidoCompra.value = record.data.CodItemPedidoCompra;
                        btnDesfazer.click();
                    }
                    else {
                        Ext.Msg.alert("Atenção", "Não é possível, desfazer item sem Ordem de Compra");
                    }
 
                }
                else
                    Ext.Msg.alert("Atenção", "Nenhuma linha foi selecionada!");                
            }
            function onClickPesquisar() {
                var btnPesquisar = $get("<%=btnPesquisarExt.ClientID %>");
                if (btnPesquisar != null)
                    btnPesquisar.click();
            }
>>>>>>> local
        var template = '<span style="color:{0};">{1}</span>';
        
        var Ipi = function (value) {
            return String.format(template,'', value + "%");
        };

        var situacao = function (value) {
            return String.format(template, (value == "Cancelado")?"white":(value == "Aberto") ? "black" : (value == "Entregue") ? "green" : (value == "Parcial") ? "black" : "red", value);
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
                if (record.data.OrdemCompra != 0) {
                    ChamaPedidoCompra(record.data.OrdemCompra, record.data.Tipo);
                }
                else {
                    Ext.Msg.alert("Atenção", "Não é possível, alterar item sem Ordem de Compra");
                }
            }
            if (command == "Imprimir") {
                if (record.data.OrdemCompra != 0) {
                    ChamaVisualizarPedidoCompra(record.data.OrdemCompra);
                }
                else {
                    Ext.Msg.alert("Atenção", "Não é possível, visualizar item sem Ordem de Compra");
                }
            }
            if (command == "Enviar") {
                if (record.data.OrdemCompra != 0) {
                    EnviarPedidoCompra(record.data.OrdemCompra, record.data.CodPessoa);
                }
                else {
                    Ext.Msg.alert("Atenção", "Não é possível, enviar email do item sem Ordem de Compra");
                }
            }
            if (command == "Estoque") {
                if (record.data.OrdemCompra != 0) {
                    if (record.data.Situacao != "Entregue" && record.data.Situacao != "Entregue em Atraso") {
                        NovaEntradaEstoque(record);
                    }
                    else {
                        Ext.Msg.alert("Atenção", "Não é possível, incluir no estoque item Entregue");
                    }
                }
                else {
                    Ext.Msg.alert("Atenção", "Não é possível, incluir no estoque item sem Ordem de Compra");
                }

            }
            if (command == "Certificado") {
                ChamaVisualizarCertificado(record.data.Lote);
            }
<<<<<<< HEAD
            if (command == "Estoque") {
                NovaEntradaEstoque(record.data.OrdemCompra, record.data.CodPessoa, record.data.Fornecedor, record.data.Bitola, record.data.Descricao, record.data.Qtde, record.data.CodMateriaPrima, record.data.CodBitola)
            }
=======
>>>>>>> 0e7e752c875b412e16e75c56679cd0618d11db3e
        }
        var beforedelete = function (ar) {
            var args = arguments;

        }

        var getRowClass = function (record) {
            if (record.data.IdStatus == 2) {
                return "red-row";
            }

            if (record.dirty) {
                return "dirty-row";
            }
        }

        var submitValue = function (grid, hiddenFormat, format) {
            hiddenFormat.setValue(format);
            grid.submitData(false);
        };

        var removeFromCache = function (c) {
            var c = window[c];
            window.lookup.remove(c);
            if (c) {
                c.destroy();
            }
        }
        window.lookup = [];

        var addToCache = function (c) {
            window.lookup.push(window[c]);
        }
    </script>
<<<<<<< HEAD
<<<<<<< HEAD


=======
>>>>>>> 0e7e752c875b412e16e75c56679cd0618d11db3e
=======


>>>>>>> local
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
                        <asp:HiddenField ID="hdfCodItemPedidoCompra" runat="server" />
                        <asp:HiddenField ID="hdfLote" runat="server" />
                        <br />
                        <asp:LinkButton ID="lkbArquivoPdf" runat="server" 
                            onclick="lkbArquivoPdf_Click"> (Nenhuma arquivo carregada)</asp:LinkButton>
                        &nbsp;<asp:Button ID="btnPesquisar" OnClientClick="return ValidaCampos()" runat="server"
                            ValidationGroup="pesquisar" CssClass="botao" Text="Pesquisar" Width="100px" OnClick="btnPesquisar_Click" />
                        &nbsp;<asp:Button ID="btnIncluir" runat="server" CssClass="botao" Width="100px" Text="Incluir Novo"
                            OnClick="btnIncluir_Click" />
                            <asp:Button ID="btnCancelarItemHide" runat="server" CssClass="botao" 
                            Width="100px" Text="Cancelar" onclick="btnCancelarItemHide_Click" />
                            <asp:Button ID="btnFinalizar" runat="server" CssClass="botao" 
                            Width="100px" Text="Finalizar" onclick="btnFinalizar_Click" />
                        <asp:Button ID="btnDesfazer" runat="server" CssClass="botao" 
                            Text="Desfazer" Width="100px" onclick="btnDesfazer_Click" />
                        <asp:Button ID="btnCarregarValores" runat="server" CssClass="botao" 
                            Text="Valores" Width="100px" onclick="btnCarregarValores_Click" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnCancelarItemHide" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnFinalizar" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnDesfazer" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnCarregarValores" EventName="Click" />
                    </Triggers>
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
                    <ext:ResourceManager ID="ResourceManager1" runat="server" />
                            <ext:Store ID="StoreListaResultado" runat="server" 
                    onrefreshdata="StoreListaResultado_RefreshData">
                                <Reader>
                                    <ext:JsonReader IDProperty="Name">
                                        <Fields>
                                            <ext:RecordField Name="OrdemCompra" />
                                            <ext:RecordField Name="CodPessoa" />
                                            <ext:RecordField Name="Tipo" />
<<<<<<< HEAD
<<<<<<< HEAD
                                            <ext:RecordField Name="Lote" />
=======
>>>>>>> 0e7e752c875b412e16e75c56679cd0618d11db3e
=======
                                            <ext:RecordField Name="Lote" />
>>>>>>> local
                                            <ext:RecordField Name="DataEmissao" Type="Date"/>
                                            <ext:RecordField Name="Fornecedor" />
                                            <ext:RecordField Name="Bitola" />
                                            <ext:RecordField Name="Descricao" />
                                            <ext:RecordField Name="Preco" />
                                            <ext:RecordField Name="Ipi" />
                                            <ext:RecordField Name="Unidade" />
                                            <ext:RecordField Name="Qtde" />
                                            <ext:RecordField Name="DataPrevista" Type="Date" />
<<<<<<< HEAD
<<<<<<< HEAD
                                            <ext:RecordField Name="DataEntrega" Type="Date" />
                                            <ext:RecordField Name="NotaFiscal" />
=======
>>>>>>> 0e7e752c875b412e16e75c56679cd0618d11db3e
=======
                                            <ext:RecordField Name="DataEntrega" Type="Date" />
                                            <ext:RecordField Name="NotaFiscal" />
>>>>>>> local
                                            <ext:RecordField Name="QtdeEntregue" />
                                            <ext:RecordField Name="Saldo" />
                                            <ext:RecordField Name="Situacao" />
                                            <ext:RecordField Name="DiaEmAtraso" />
<<<<<<< HEAD
<<<<<<< HEAD
                                            <ext:RecordField Name="CodMateriaPrima" />
                                            <ext:RecordField Name="CodBitola" />
=======
>>>>>>> 0e7e752c875b412e16e75c56679cd0618d11db3e
                                        </Fields>
=======
                                            <ext:RecordField Name="CodMateriaPrima" />
                                            <ext:RecordField Name="CodBitola" />
                                            <ext:RecordField Name="CodUnidade" />
                                            <ext:RecordField Name="CodItemPedidoCompra" />
                                            <ext:RecordField Name="IdStatus" />
                                            </Fields>
>>>>>>> local
                                    </ext:JsonReader>
                                </Reader>
                            </ext:Store> 
        <ext:Hidden ID="FormatType" runat="server" />

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
<<<<<<< HEAD
<<<<<<< HEAD
                    <ext:Button ID="btnNovoItem" runat="server" Text="Novo" CtCls="botao" 
                        Width="80px" OnClientClick="ctl00_cphPrincipal_Window1.show();return false;"  />
=======
>>>>>>> 0e7e752c875b412e16e75c56679cd0618d11db3e
=======
                    <ext:Button ID="btnImprimir" runat="server" Text="Imprimir" CtCls="botao" 
                        Width="80px" OnClientClick="onClickImprimir();return false;"  />
                    <ext:Button ID="btnEmail" runat="server" Text="Email" CtCls="botao" 
                        Width="80px" OnClientClick="onClickEmail();return false;"  />
                    <ext:Button ID="btnDbCancelarItem" runat="server" Text="Cancelar" CtCls="botao" 
                        Width="80px" OnClientClick="onClickCancelarItem();return false;"  />
                    <ext:Button ID="btnFinalizarItem" runat="server" Text="Finalizar" CtCls="botao" 
                        Width="80px" OnClientClick="onClickFinalizarItem();return false;"  />
                    <ext:Button ID="btnDesfazerItem" runat="server" Text="Desfazer" CtCls="botao" 
                        Width="80px" OnClientClick="onClickDesfazerItem();return false;"  />

>>>>>>> local
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
<<<<<<< HEAD
<<<<<<< HEAD
                            Height="320" ColumnLines="True" CtCls="GridLayout">
=======
                            Height="350" ColumnLines="True" CtCls="GridLayout">
>>>>>>> 0e7e752c875b412e16e75c56679cd0618d11db3e
=======
                            Height="330" ColumnLines="True" CtCls="GridLayout">
>>>>>>> local
                           <ColumnModel ID="ColumnModel1" runat="server">
		                    <Columns>
                                <ext:CommandColumn Width="85" Header="Ações" Locked="true">
                                    <Commands>
                                        <ext:GridCommand Icon="NoteEdit" CommandName="Editar">
                                            <ToolTip Text="Editar dados do Pedido de Compra" />
                                        </ext:GridCommand>
                                         <ext:CommandSeparator />
                                        <ext:GridCommand Icon="PackageAdd" CommandName="Estoque">
                                            <ToolTip Text="Entrada de Estoque" />
                                        </ext:GridCommand>
                                        <ext:CommandSeparator />
                                        <ext:GridCommand Icon="Note" CommandName="Certificado">
                                            <ToolTip Text="Visualizar Certificado" />
                                        </ext:GridCommand>
<<<<<<< HEAD
                                        <ext:GridCommand Icon="New" CommandName="Estoque">
                                            <ToolTip Text="Entrada de Estoque" />
                                        </ext:GridCommand>
                                    </Commands>
                                </ext:CommandColumn>
                                <ext:Column ColumnId="OrdemCompra" Align="Right"  Header="Ordem de Compra" Width="160" DataIndex="OrdemCompra" Hidden="False" Css="font-family : Arial; 	font-size : 16px;" />
                                <ext:Column ColumnId="Lote" Align="Right"  Header="Lote" Width="160" DataIndex="Lote" Css="font-family : Arial; 	font-size : 16px;" />
=======
                                    </Commands>
                                </ext:CommandColumn>
<<<<<<< HEAD
                                <ext:Column ColumnId="OrdemCompra" Align="Right"  Header="Ordem de Compra" Width="160" DataIndex="OrdemCompra" Hidden="False" Css="font-family : Arial; 	font-size : 16px;" />
>>>>>>> 0e7e752c875b412e16e75c56679cd0618d11db3e
                                <ext:Column ColumnId="CodPessoa" Align="Right"  Header="CodPessoa" Width="160" DataIndex="CodPessoa" Hidden="true" Css="font-family : Arial; 	font-size : 16px;" />
                                <ext:Column ColumnId="Tipo" Align="Right"  Header="Tipo" Width="160" DataIndex="Tipo" Hidden="true" Css="font-family : Arial; 	font-size : 16px;" />
                                <ext:DateColumn ColumnId="DataEmissao" Format="dd/MM/yyyy" Header="Emissão"  Width="90" DataIndex="DataEmissao" Hidden="False" Css="font-family : Arial; 	font-size : 16px;"/>
                                <ext:Column ColumnId="Fornecedor" Header="Fornecedor"  Width="276" DataIndex="Fornecedor" Hidden="False" Css="font-family : Arial; 	font-size : 16px;" />
<<<<<<< HEAD
                                <ext:Column ColumnId="Qtde" Align="Right"  Header="Qtde"  Width="60" DataIndex="Qtde" Hidden="False" Css="font-family : Arial; 	font-size : 16px;" />
                                <ext:Column ColumnId="QtdeEntregue" Align="Right" Header="Qtde Entregue"  Width="72" DataIndex="QtdeEntregue" Hidden="False" Css="font-family : Arial; 	font-size : 16px;" />
                                <ext:Column ColumnId="Unidade" Align="Right"  Header="Unidade"  Width="60" DataIndex="Unidade" Hidden="False" Css="font-family : Arial; 	font-size : 16px;" />
=======
>>>>>>> 0e7e752c875b412e16e75c56679cd0618d11db3e
                                <ext:Column ColumnId="Bitola" Header="Bitola" Align="Right"   Width="65" DataIndex="Bitola" Hidden="False" Css="font-family : Arial; 	font-size : 16px;" />
                               <ext:Column ColumnId="Descricao" Header="Material/Produto"   Width="208" DataIndex="Descricao" Hidden="False" Css="font-family : Arial; 	font-size : 16px;" />
                                <ext:Column ColumnId="Preco" Header="Preço" Align="Right"   Width="68" DataIndex="Preco" Hidden="False" Css="font-family : Arial; 	font-size : 16px;" />
                                <ext:Column ColumnId="Ipi" Header="Ipi" Align="Right"   Width="45" DataIndex="Ipi" Hidden="False" Css="font-family : Arial; 	font-size : 16px;">
                                <Renderer Fn="Ipi" />
                                </ext:Column>
<<<<<<< HEAD
                                <ext:DateColumn ColumnId="DataPrevista" Format="dd/MM/yyyy" Header="Entrega Prevista"  Width="98" DataIndex="DataPrevista" Hidden="False" Css="font-family : Arial; 	font-size : 16px;" />
=======
                                <ext:Column ColumnId="Qtde" Align="Right"  Header="Qtde"  Width="60" DataIndex="Qtde" Hidden="False" Css="font-family : Arial; 	font-size : 16px;" />
                                <ext:Column ColumnId="Unidade" Align="Right"  Header="Unidade"  Width="60" DataIndex="Unidade" Hidden="False" Css="font-family : Arial; 	font-size : 16px;" />
                                <ext:DateColumn ColumnId="DataPrevista" Format="dd/MM/yyyy" Header="Entrega Prevista"  Width="98" DataIndex="DataPrevista" Hidden="False" Css="font-family : Arial; 	font-size : 16px;" />
                                <ext:Column ColumnId="QtdeEntregue" Align="Right" Header="Qtde Entregue"  Width="72" DataIndex="QtdeEntregue" Hidden="False" Css="font-family : Arial; 	font-size : 16px;" />
                                <ext:Column ColumnId="Saldo" Align="Right" Header="Saldo" Width="70" DataIndex="Saldo" Hidden="False" Css="font-family : Arial; 	font-size : 16px;" RightCommandAlign="True" />
>>>>>>> 0e7e752c875b412e16e75c56679cd0618d11db3e
                                <ext:Column ColumnId="Situacao" Header="Situação"  Width="78" DataIndex="Situacao" Hidden="False" Css="font-family : Arial; 	font-size : 16px;">
=======
                                <ext:Column ColumnId="Situacao" Header="Situação"  Wrap="true" Width="100" DataIndex="Situacao" Hidden="False" Locked="true" Css="font-family : Arial; 	font-size : 16px; border-top: 1px solid black; border-left: 1px solid black; border-bottom: 1px solid black; border-right: 1px solid black; ">
>>>>>>> local
                                <Renderer fn="situacao"/>
                                </ext:Column>
                                <ext:Column ColumnId="DiaEmAtraso" Header="Dias de Atraso" Align="Right"  Width="70" DataIndex="DiaEmAtraso" Hidden="False" Css="font-family : Arial; 	font-size : 16px; border-top: 1px solid black; border-right: 1px solid black; border-bottom: 1px solid black;">
                                </ext:Column>
                                <ext:Column ColumnId="Saldo" Align="Right" Header="Saldo" Width="70" DataIndex="Saldo" Hidden="False" Css="font-family : Arial; font-size : 16px; border-top: 1px solid black; border-right: 1px solid black; border-bottom: 1px solid black;" RightCommandAlign="True" >
                                    <Renderer  Fn="Ext.util.Format.numberRenderer('0.000')" />
                                </ext:Column>                                <ext:Column ColumnId="OrdemCompra" Align="Right"  Header="O.C." Width="160" DataIndex="OrdemCompra" Hidden="False" Css="font-family : Arial; font-weight: bold;	font-size : 16px; border-top: 1px solid black; border-right: 1px solid black; border-bottom: 1px solid black;" />
                                <ext:Column ColumnId="Lote" Align="Right"  Header="Lote" Width="70" DataIndex="Lote" Css="font-family : Arial; font-weight: bold;	font-size : 16px; border-top: 1px solid black;16px; border-right: 1px solid black; border-bottom: 1px solid black;" />
                                <ext:Column ColumnId="CodPessoa" Align="Right"  Header="CodPessoa" Width="160" DataIndex="CodPessoa" Hidden="true" Css="font-family : Arial; 	font-size : 16px;border-top: 1px solid black; border-left: 1px solid black; border-bottom: 1px solid black;" />
                                <ext:Column ColumnId="Tipo" Align="Right"  Header="Tipo" Width="160" DataIndex="Tipo" Hidden="true" Css="font-family : Arial; 	font-size : 16px; border-top: 1px solid black;16px; border-right: 1px solid black; border-bottom: 1px solid black;" />
                                <ext:DateColumn ColumnId="DataEmissao" Format="dd/MM/yyyy" Header="Emissão"  Width="90" DataIndex="DataEmissao" Hidden="False" Css="font-family : Arial; 	font-size : 16px; border-top: 1px solid black; border-right: 1px solid black; border-bottom: 1px solid black;"/>
                                <ext:Column ColumnId="Fornecedor" Header="Fornecedor"  Width="120" DataIndex="Fornecedor" Hidden="False" Css="font-family : Arial; 	font-size : 16px; border-top: 1px solid black; border-right: 1px solid black; border-bottom: 1px solid black;" />
                                <ext:Column ColumnId="Qtde" Align="Right"  Header="Qtde"  Width="75" DataIndex="Qtde" Hidden="False" Css="font-family : Arial; font-weight: bold;	font-size : 16px; border-top: 1px solid black; border-right: 1px solid black; border-bottom: 1px solid black;" >
                                    <Renderer  Fn="Ext.util.Format.numberRenderer('0.000')" />
                                </ext:Column>
                                <ext:Column ColumnId="QtdeEntregue" Align="Right" Header="Qtde Entregue"  Width="75" DataIndex="QtdeEntregue" Hidden="False" Css="font-family : Arial; 	font-size : 16px; border-right: 1px solid black; border-top: 1px solid black; border-bottom: 1px solid black;" >
                                    <Renderer  Fn="Ext.util.Format.numberRenderer('0.000')" />
                                </ext:Column>
                                <ext:Column ColumnId="Unidade" Align="Left"  Header="Unidade"  Width="28" DataIndex="Unidade" Hidden="False" Css="font-family : Arial; 	font-size : 16px; border-top: 1px solid black;16px; border-right: 1px solid black; border-bottom: 1px solid black;" />
                                <ext:Column ColumnId="Bitola" Header="Bitola" Align="Right"   Width="75" DataIndex="Bitola" Hidden="False" Css="font-family : Arial; 	font-size : 16px; border-top: 1px solid black; border-right: 1px solid black;border-bottom: 1px solid black;">
                                    <Renderer  Fn="Ext.util.Format.numberRenderer('0.00 mm')" />
                                </ext:Column>
<<<<<<< HEAD
<<<<<<< HEAD
                                <ext:DateColumn ColumnId="DataEntrega" Format="dd/MM/yyyy" Header="Ent. Real"  Width="98" DataIndex="DataEntrega" Hidden="False" Css="font-family : Arial; 	font-size : 16px;" />
                                <ext:Column ColumnId="NotaFiscal"  Header="N.F."  Width="98" DataIndex="NotaFiscal" Hidden="False" Css="font-family : Arial; 	font-size : 16px;" />
                                <ext:Column ColumnId="Saldo" Align="Right" Header="Saldo" Width="70" DataIndex="Saldo" Hidden="False" Css="font-family : Arial; 	font-size : 16px;" RightCommandAlign="True" />
                            </Columns>
                            </ColumnModel>

                            <Listeners>
                                <Command Handler="rowcommand(command, record);" />
                                <CellClick Fn="cellClick" />
=======
=======
                               <ext:Column ColumnId="Descricao" Header="Material/Produto"   Width="208" DataIndex="Descricao" Hidden="False" Css="font-family : Arial; 	font-size : 16px; border-top: 1px solid black; border-right: 1px solid black; border-bottom: 1px solid black;" />
                                <ext:Column ColumnId="Preco" Header="Preço" Align="Right"   Width="85" DataIndex="Preco" Hidden="False" Css="font-family : Arial; 	font-size : 16px; border-top: 1px solid black; border-right: 1px solid black;border-bottom: 1px solid black;">
                                <Renderer  Fn="Ext.util.Format.numberRenderer('R$ 0.00')" />
                                </ext:Column>
                                <ext:Column ColumnId="Ipi" Header="Ipi" Align="Right"   Width="45" DataIndex="Ipi" Hidden="False" Css="font-family : Arial; 	font-size : 16px; border-top: 1px solid black; border-right: 1px solid black; border-bottom: 1px solid black;">
                                <Renderer Fn="Ipi" />
                                </ext:Column>
                                <ext:DateColumn ColumnId="DataPrevista" Format="dd/MM/yyyy" Header="Entrega Prevista"  Width="98" DataIndex="DataPrevista" Hidden="False" Css="font-family : Arial; 	font-size : 16px; border-top: 1px solid black; border-right: 1px solid black; border-bottom: 1px solid black;" />

                                <ext:DateColumn ColumnId="DataEntrega" Format="dd/MM/yyyy" Header="Ent. Real"  Width="98" DataIndex="DataEntrega" Hidden="False" Css="font-family : Arial; 	font-size : 16px; border-top: 1px solid black; border-right: 1px solid black; border-bottom: 1px solid black;" />
                                <ext:Column ColumnId="NotaFiscal"  Header="N.F."  Width="98" DataIndex="NotaFiscal" Hidden="False" Css="font-family : Arial; 	font-size : 16px; border-top: 1px solid black; border-right: 1px solid black; border-bottom: 1px solid black;" />

                                <ext:Column ColumnId="CalcColuna" Align="Right" Header="" Width="70" DataIndex="CalcColuna" Hidden="False" Css="font-family : Arial; 	font-size : 16px; border-top: 1px solid black; border-right: 1px solid black; border-bottom: 1px solid black;;" RightCommandAlign="True" />
                                <ext:Column ColumnId="CalcColuna" Align="Right" Header="" Width="70" DataIndex="CalcColuna" Hidden="False" Css="font-family : Arial; 	font-size : 16px; border-top: 1px solid black; border-right: 1px solid black; border-bottom: 1px solid black;" RightCommandAlign="True" />
                                <ext:Column ColumnId="CalcColuna" Align="Right" Header="" Width="70" DataIndex="CalcColuna" Hidden="False" Css="font-family : Arial; 	font-size : 16px; border-top: 1px solid black; border-right: 1px solid black;border-bottom: 1px solid black;" RightCommandAlign="True" />
>>>>>>> local
                            </Columns>
                            </ColumnModel>
<%--                            <Plugins>
                                <ext:RowExpander ID="RowExpander1" runat="server">
                                    <Template ID="Template1" runat="server">
                                    <Html>
							            <div id="row-{ID}" style="background-color:White;"></div>
						            </Html>
                                </Template>
                    <DirectEvents>
                        <BeforeExpand OnEvent="BeforeExpand">
                            <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={ctl00_cphPrincipal_grdListaResultado1.body}" />
                            <ExtraParams>
                                <ext:Parameter Name="id" Value="record.OrdemCompra" Mode="Raw" />
                            </ExtraParams>
                        </BeforeExpand>
                    </DirectEvents>
                </ext:RowExpander>
            </Plugins>--%>
                            <View>
                                <ext:GridView ID="GridView1" runat="server">
                                    <GetRowClass Fn="getRowClass" />                       
                                </ext:GridView>

                            </View>

                            <Listeners>
                                <Command Handler="rowcommand(command, record);" />
<<<<<<< HEAD
>>>>>>> 0e7e752c875b412e16e75c56679cd0618d11db3e
=======
                                <CellClick Fn="cellClick" />
>>>>>>> local
                            </Listeners> 
                            </ext:GridPanel>
                            </Items>
                            </ext:Panel>
            </div>
            <asp:HiddenField ID="hdfCodPedidoCompra" runat="server" />
            <asp:HiddenField ID="hdfValor" runat="server" />
            <asp:HiddenField ID="hdfOpcao" runat="server" />
            <br />
<<<<<<< HEAD
<<<<<<< HEAD
            <br />
=======
>>>>>>> 0e7e752c875b412e16e75c56679cd0618d11db3e
=======
            <br />
>>>>>>> local
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnPesquisar" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
<<<<<<< HEAD
<<<<<<< HEAD
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
=======
<div>
    <div id="divItem">
    <asp:HiddenField ID="hdfTargetIncluirItem" runat="server" Value="IncluirItem" />
    <ajaxToolkit:ModalPopupExtender ID="mpeIncluirItem" runat="server" PopupControlID="pnlIncluirItem"
        TargetControlID="hdfTargetIncluirItem" BehaviorID="mpeIncluirItem" BackgroundCssClass="modalBackground"
        DropShadow="true" />
    <asp:Panel ID="pnlIncluirItem" runat="server">
        <asp:UpdatePanel runat="server" ID="updCadastroItem" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:HiddenField runat="server" ID="hdfCodPedidoCompraItem" />
                <asp:HiddenField runat="server" ID="hdfTipoAcaoItem" Value="IncluirItem" />
                <asp:HiddenField runat="server" ID="hdfCodFornecedor" />
                <asp:HiddenField runat="server" ID="hdfCodUnidade" />
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
                    <table width="100%" align="center" class="fundoTabela" runat="server">
>>>>>>> local
                        <tr>
                            <td style="padding-left: 20px;">
                                Lote
                            </td>
                            <td style="padding-left: 20px;">
                                Entrega
                            </td>
                            <td style="padding-left: 20px;" colspan="2">
<<<<<<< HEAD
                                Norma<ext:Hidden ID="hdfCodMateriaPrima" runat="server">
                                </ext:Hidden>
                            </td>
                            <td style="padding-left: 20px;" colspan="2">
                                Bitola<ext:Hidden ID="hdfCodBitola" runat="server">
                                </ext:Hidden>
=======
                                Norma
                                <asp:HiddenField ID="hdfCodMateriaPrima" runat="server" />
                            </td>
                            <td style="padding-left: 20px;" colspan="2">
                                Bitola
                                <asp:HiddenField ID="hdfCodBitola" runat="server" />
>>>>>>> local
                            </td>
                        </tr>
                        <tr>
                            <td>
<<<<<<< HEAD
                                <ext:TextField ID="txtLote" runat="server"></ext:TextField>
                            </td>
                            <td>
                                <ext:TextField ID="txtData" runat="server" Cls="dataEmisssao"></ext:TextField>
                            </td>
                            <td colspan="2">
                                <ext:TextField ID="txtNorma" runat="server" Width="300px"></ext:TextField>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                        ControlToValidate="txtNorma" 
=======
                                <asp:TextBox ID="txtLote" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtData" runat="server"></asp:TextBox>
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtenderFromDate" runat="server" 
                                TargetControlID="txtData"
                                Mask="99/99/9999"
                                MessageValidatorTip="true"
                                OnFocusCssClass="MaskedEditFocus"
                                OnInvalidCssClass="MaskedEditError"
                                MaskType="Date"
                                DisplayMoney="Left"
                                AcceptNegative="Left"
                                ErrorTooltipEnabled="True" UserDateFormat="DayMonthYear"/>


                            </td>
                            <td colspan="2">
                                <asp:DropDownList ID="ddlMateriaPrimaItem" runat="server" 
                                    onselectedindexchanged="ddlMateriaPrima_SelectedIndexChanged" 
                                    AutoPostBack="True">
                                </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                        ControlToValidate="ddlMateriaPrimaItem" 
>>>>>>> local
                                        ErrorMessage="Matéria Prima falta ser preenchido." 
                                        ValidationGroup="ValidaDadosItens">*</asp:RequiredFieldValidator>
                            </td>
                            <td colspan="2">
<<<<<<< HEAD
                                <ext:TextField ID="txtBitola" runat="server" Width="100px"></ext:TextField>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                        ControlToValidate="txtBitola" 
=======
                                    <asp:DropDownList ID="ddlBitolaItem" runat="server" 
                                    onselectedindexchanged="ddlBitola_SelectedIndexChanged" 
                                    AutoPostBack="True">
                                </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                        ControlToValidate="ddlBitolaItem" 
>>>>>>> local
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
<<<<<<< HEAD
                                Fornecedor<ext:Hidden ID="hdfCodFornecedor" runat="server">
                                </ext:Hidden>
=======
                                Fornecedor
>>>>>>> local
                            </td>
                        </tr>
                        <tr>
                            <td>
<<<<<<< HEAD
                                <ext:TextField ID="txtPedidoCompraItem" runat="server"  Cls="DesligarTextBox"></ext:TextField>
                                <ext:Hidden ID="hdfCodPedidoCompraItem" runat="server"></ext:Hidden>
                            </td>
                            <td colspan="5">
                                <ext:TextField ID="txtFornecedor" runat="server" Width="500px" Cls="DesligarTextBox"></ext:TextField>
=======
                                <asp:TextBox ID="txtPedidoCompraItem" runat="server" CssClass="DesligarTextBox"></asp:TextBox>
                            </td>
                            <td colspan="5">
                                <asp:TextBox ID="txtFornecedor" runat="server" Width="95%" CssClass="DesligarTextBox"></asp:TextBox>
>>>>>>> local
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
<<<<<<< HEAD
                        <td><ext:TextField ID="txtCertificado" runat="server"></ext:TextField></td>
                        <td><ext:TextField ID="txtNotaFiscalItem" runat="server"></ext:TextField></td>
                        <td><ext:TextField ID="txtDataNotaFiscalItem" CtCls="dataEmissao" runat="server"></ext:TextField></td>
                            <td colspan="3">
                                <ext:TextField ID="txtEspecificacao" runat="server"></ext:TextField></td>
=======
                        <td><asp:TextBox ID="txtCertificado" runat="server"></asp:TextBox></td>
                        <td><asp:TextBox ID="txtNotaFiscalItem" runat="server"></asp:TextBox></td>
                        <td><asp:TextBox ID="txtDataNotaFiscalItem" runat="server" CssClass="dataEmissao"></asp:TextBox>
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" 
                                TargetControlID="txtDataNotaFiscalItem"
                                Mask="99/99/9999"
                                MessageValidatorTip="true"
                                OnFocusCssClass="MaskedEditFocus"
                                OnInvalidCssClass="MaskedEditError"
                                MaskType="Date"
                                DisplayMoney="Left"
                                AcceptNegative="Left"
                                ErrorTooltipEnabled="True"/>                        
                        </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtEspecificacao" runat="server"></asp:TextBox></td>
>>>>>>> local
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
<<<<<<< HEAD
                        <td><ext:TextField ID="txtCorrida" runat="server"></ext:TextField></td>
                        <td><ext:TextField ID="txtQtdePedidoCompra" runat="server" Cls="DesligarTextBox"></ext:TextField>
                            </td>
                        <td><ext:TextField ID="txtQtde" runat="server"></ext:TextField>
=======
                        <td><asp:TextBox ID="txtCorrida" runat="server"></asp:TextBox></td>
                        <td><asp:TextBox ID="txtQtdePedidoCompra" runat="server" CssClass="DesligarTextBox"></asp:TextBox>
                            </td>
                        <td><asp:TextBox ID="txtQtde" runat="server"></asp:TextBox>
>>>>>>> local
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" 
                                ControlToValidate="txtQtde" ErrorMessage="Qtde/Kilo falta ser preenchido." 
                                ValidationGroup="ValidaDadosItens">*</asp:RequiredFieldValidator>
                            </td>
                            <td>
<<<<<<< HEAD
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
=======
                                <asp:DropDownList ID="ddlUnidade" runat="server">
                                </asp:DropDownList>
                            </td>
                        <td>
                            <asp:TextBox ID="txtIPI" runat="server" onkeypress="OnlyMoney();" Width="50px"></asp:TextBox>
                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtValorUnit" runat="server"></asp:TextBox>
>>>>>>> local
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
<<<<<<< HEAD
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
=======
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
>>>>>>> local
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
<<<<<<< HEAD
                                                <ext:TextField ID="txtResistenciaTracao" runat="server" 
                                                    ontextchanged="txtResistenciaTracao_TextChanged" Width="90px" AutoPostBack="True"></ext:TextField>
=======
                                                <asp:TextBox ID="txtResistenciaTracao" runat="server" 
                                                    ontextchanged="txtResistenciaTracao_TextChanged" Width="90px" AutoPostBack="True"></asp:TextBox>
>>>>>>> local
                                                <span id="spanRt" class="asterisco" runat="server" style="display:none">*</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left: 20px; width: 142px; padding-top: 5px; border-left: 1px solid black;">
                                                Dureza:
                                            </td>
                                            <td style="padding-top: 5px; border-right: 1px solid black;">
<<<<<<< HEAD
                                                <ext:TextField ID="txtDureza" runat="server" Width="90px"></ext:TextField>
=======
                                                <asp:TextBox ID="txtDureza" runat="server" Width="90px"></asp:TextBox>
>>>>>>> local
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
<<<<<<< HEAD
                                                <ext:TextField ID="txtNota" runat="server" Width="177px" onblur="getSituacao(this.value)"></ext:TextField>
=======
                                                <asp:TextBox ID="txtNota" runat="server" Width="177px" onblur="getSituacao(this.value)"></asp:TextBox>
>>>>>>> local
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left: 20px; width: 142px; padding-top: 5px; border-left: 1px solid black;">
                                                Situação:
                                            </td>
                                            <td style="padding-top: 5px; border-right: 1px solid black;">
<<<<<<< HEAD
                                                <ext:TextField ID="txtSituacao" runat="server" Cls="DesligarTextBox situacao" 
                                                    Width="285px"></ext:TextField>
=======
                                                <asp:TextBox ID="txtSituacao" runat="server" CssClass="DesligarTextBox situacao" 
                                                    Width="285px"></asp:TextBox>
>>>>>>> local
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
<<<<<<< HEAD
                                <ext:TextArea ID="txtObservacaoItem" Width="900px" runat="server"></ext:TextArea>
=======
                                <asp:TextBox ID="txtObservacaoItem" runat="server" TextMode="MultiLine" 
                                    Width="99%"></asp:TextBox>
>>>>>>> local
                            </td>
                        </tr>
                        <tr>
                        <td colspan="6">
                        <table cellpadding="3" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="padding-left:20px; width: 279px;"><b>Certificado Scanneado</b></td>
                                    </tr>
                                    <tr>
<<<<<<< HEAD
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
=======
                                     <td colspan="3">
                                        <div id="uploadStatus" style="color: red;"></div>
                                        <input type="button" id="uploadFile" style="width:90px" value="Upload File" class="botao"/>
                                        <div id="fileList"></div>
                                     </td>
                                            <td align="right"><asp:CheckBox ID="ckbFinalizarItem" runat="server" Text="Finalizar" /></td>
                                     </tr>
                                     <tr>
                                     <td colspan="4">&nbsp;</td>
>>>>>>> local
                                     </tr>
                                </table>
                        </td>
                        </tr>
                        <tr>
<<<<<<< HEAD
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
=======
</asp:Content>
>>>>>>> 0e7e752c875b412e16e75c56679cd0618d11db3e
=======
                        <td colspan="2">
                        <input type="button" id="btnCancelarItem" onclick="LimparCamposItemEntradaEstoque();" value="Cancelar" style="width:90px" class="botao" /></td>
                        <td colspan="4" align="right"><asp:Button ID="btnIncluirItem" Width="90px" 
                                runat="server" CssClass="botao" Text="Salvar" 
                                onclick="btnIncluirItem_Click" ValidationGroup="ValidaDadosItens" /></td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="lkbArquivoPdf" />
                <asp:AsyncPostBackTrigger ControlID="btnIncluirItem"  EventName="Click"/>
                <asp:AsyncPostBackTrigger ControlID="ddlMateriaPrimaItem" 
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
   </div>
</div>        
 </asp:Content>
>>>>>>> local
