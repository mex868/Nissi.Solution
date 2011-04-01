<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.Master" CodeBehind="CadastraCFOP.aspx.cs" Inherits="Nissi.WebPresentation.Cadastrar_CFOP" %>
<%@ Register assembly="RDC.Tools" namespace="RDC.Tools" tagprefix="cc1" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register src="../UserControl/Endereco.ascx" tagname="Endereco" tagprefix="uc2" %>
<%@ Register src="../UserControl/Banco.ascx" tagname="Banco" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="server">
    <link href="../App_Themes/Theme1/Model1.css" type="text/css"  rel="Stylesheet" />
<script src="../JScripts/Common.js" type="text/javascript"></script>
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
    function EndRequest(sender, args) {
        WaitAsyncPostBack(false);
    }


    //--------------------------------------------------------------------------------
    //Criado por...: Jacqueline Albuquerque - 12/06/2010
    //Objetivo.....: Verifica o tipo de pesquisa e habilita campos e limpa-los
    //--------------------------------------------------------------------------------
    function TipoPesquisa(tvar) {

        $get('<%=txtCodigoPesq.ClientID%>').value = '';
        $get('<%=txtDescPesq.ClientID%>').value = '';
        $get('divDesc').style.display = 'none';
        $get('divCodigo').style.display = 'none';
        switch (tvar) {
            case 1: //Pesquisa por Desc
                $get('divCodigo').style.display = 'block';
                break;
            case 2: //Pesquisa por Descricao
                $get('divDesc').style.display = 'block';
                break;
       }
    }
    //--------------------------------------------------------------------------------
    //Criado por...: Alexandre Maximiano - 15/06/2010
    //Objetivo.....: Valida os campos
    //--------------------------------------------------------------------------------
    function ValidaCampos(src, args) {
        var valido = true;
        if (($get('<%=rbCodigo.ClientID%>').checked) && ($get('<%=txtCodigoPesq.ClientID%>').value == '')) {
            valido = false;
            $get('<%=cvValida.ClientID %>').errormessage = "Favor informar o Código.";
        }
        else if (($get('<%=rbDesc.ClientID%>').checked) && ($get('<%=txtDescPesq.ClientID%>').value == '')) {
            valido = false;
            $get('<%=cvValida.ClientID %>').errormessage = "Favor informar a Descrição.";
        }
         args.IsValid = valido;
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
        <td style="width: 28px">
            <asp:Image ID="ImgCadastro" runat="server" ImageUrl="~/Imagens/layout.png" Height="16px" />
        </td>
        <td class="titulo">Cadastro de CFOP</td>
    </tr>
</table>
<br style="line-height:5px" />
<div style="text-align:center; width:100%;" id="divConsulta" runat="server">
    <div class="fundoTabela" style="width:95%" >
    <table id="tblConsulta" runat="server" style="text-align:left; width:100%">
        <tr>
            <td style="width: 20%;padding-left:17px"> Opções de Consulta:</td>
            <td style="width: 20%">
                <asp:RadioButton ID="rbCodigo" onclick = "TipoPesquisa(1)"  
                    GroupName="filtroPesq" runat="server" Text="Código" Checked="True" 
                    CssClass="noBorder" />
            </td>
            <td style="width: 20%">
                <asp:RadioButton ID="rbDesc" onclick ="TipoPesquisa(2)" runat="server" 
                    Text="Descrição" GroupName="filtroPesq" CssClass="noBorder" />
            </td>
            <td style="width: 20%">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 20%">&nbsp;</td>
            <td colspan="3">
                <div id="divCodigo"  style="display:block">
                    <asp:TextBox ID="txtCodigoPesq" MaxLength="4" onkeypress="OnlyNumbers();KeyDownHandler();" runat="server" Height="16px" Width="146px"></asp:TextBox>
                    
                </div>
                <div id="divDesc" style="display:none">
                    <asp:TextBox ID="txtDescPesq" onkeypress="KeyDownHandler();" runat="server" Height="16px" Width="600px"></asp:TextBox>
                    <asp:CustomValidator Display="None" Text="*" CssClass="asterisco" ID="cvValida" ValidationGroup="pesquisar" ClientValidationFunction="ValidaCampos"  ErrorMessage="Favor informar a Descrição." runat="server"></asp:CustomValidator>
                </div>
            </td>
        </tr>
        <tr>
            <td style="text-align:left" colspan="2">
                <asp:Button ID="btnVoltar" runat="server" OnClick="btnVoltar_Click" 
                    CssClass="botao" Text="Voltar" Width="100px" UseSubmitBehavior="False" />
            </td>
            <td colspan="2" style="text-align:right">
                <asp:UpdatePanel ID="updBotoes" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Button ID="btnPesquisar" runat="server" ValidationGroup="pesquisar" CssClass="botao"
                            Text="Pesquisar" Width="100px" OnClick="btnPesquisar_Click" />
                            &nbsp;<asp:Button ID="btnIncluir" runat="server" CssClass="botao" Width="100px"
                            Text="Incluir Novo"  onclick="btnIncluir_Click" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    </div>
    <br style="line-height:5px" />
    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="updGrid">
        <ContentTemplate>
                        <div id="divListaResultado" runat="server" style="overflow: auto; display: block; text-align:center; height:400px;">
                            <cc1:RDCGrid 
                                id="grdListaResultado" 
                                runat="server" 
                                autogeneratecolumns="False" 
                                bordercolor="Black" 
                                borderwidth="1px" 
                                cellpadding="1" 
                                cellspacing="3" 
                                gridlines="None" 
                                pagesize="30" 
                                showpagedetails="True" 
                                AllowPaging="True" 
                                MultiSelection="True" 
                                ShowHeaderCheckBoxColumn="False" 
                                ShowOptionColumn="False" 
                                CssClass="alinhamento" 
                                onpageindexchanging="grdListaResultado_PageIndexChanging" 
                                onrowcommand="grdListaResultado_RowCommand" 
                                onrowdatabound="grdListaResultado_RowDataBound" Width="95%">
                                <Columns>
                                    <asp:TemplateField HeaderText="Ações">
                                        <itemtemplate>
                                            <asp:ImageButton ID="imgEditar" Width="15px" Height="15px" runat="server" ImageUrl="~/Imagens/editar.png" />
                                            <asp:ImageButton ID="imgExcluir" Width="15px" Height="15px" runat="server" ImageUrl="~/Imagens/exclusao_Canc.png" />
                                         </itemtemplate>
                                        <HeaderStyle CssClass="headerGrid" Width="5%" />
                                        <ItemStyle HorizontalAlign="center" Wrap="false"/>
                                    </asp:TemplateField>
                                     <asp:BoundField HeaderText="Código" >
                                         <headerstyle wrap="false" Width="15%" CssClass="headerGrid"></headerstyle>
                                         <itemstyle wrap="false" HorizontalAlign="Left"></itemstyle>
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Descrição" >
                                        <HeaderStyle Wrap="false" CssClass="headerGrid"/>
                                        <ItemStyle HorizontalAlign="Left" Width="80%" />
                                    </asp:BoundField>
                                </Columns>
                            </cc1:RDCGrid>
                        </div>
        </ContentTemplate>
        <Triggers>
           <asp:AsyncPostBackTrigger ControlID="btnPesquisar" EventName="Click" />
           <asp:AsyncPostBackTrigger ControlID="btnSalvar" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</div>
<br style="line-height:5px" />
<ajaxToolkit:ModalPopupExtender ID="mpeTransIncluir" runat="server" PopupControlID="pnlIncluirForn"
    TargetControlID="hdfTargetIncluirForn" BehaviorID="mpeFornIncluirID" BackgroundCssClass="modalBackground"
    DropShadow="true" />    
<asp:Panel ID="pnlIncluirForn" runat="server">
    <asp:UpdatePanel  runat="server" ID="upCadastro" UpdateMode="Conditional">
        <ContentTemplate>
        <asp:HiddenField runat="server" ID="hdfTipoAcao" />
            <div style="text-align: center;width:900px; height:200px; padding: 5px 5px 5px 5px; background-color: #ffffff; overflow:auto">
                <table class="fundoTabela">
                    <!--TÍTULO DA POPUP-->
                    <tr>
                        <td class="titulo">
                            <b>
                                <% if (hdfTipoAcao.Value == "Incluir") { %>
                                        ::Inclusão de CFOP
                                <% } %>
                                <% else { %>
                                        ::Alteração de CFOP
                                <% } %>
                            </b>
                        </td>
                    </tr>
                </table>
                <br style="line-height:5px" />
                <table width="100%" align="center" class="fundoTabela" >
                    <tr> 
                        <td>
                            <table cellpadding="3" cellspacing="0" class="fundoTabela" style="text-align:left">
                                <tr>
                                    <td><b>::Dados cadastrais:</b></td>
                                </tr>
                                <tr>
                                    <td style="padding-left:20px">Código</td>
                                    <td style="padding-left:20px">Descrição</td>
                                </tr>
                                <tr>
                                  <td>
                                        <asp:TextBox ID="txtCodigo" onkeypress="OnlyNumbers()" MaxLength="4" runat="server" CssClass="formNovo" TabIndex="2" Width="300px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvCodigo" ErrorMessage="Código não informada!" ValidationGroup="Inserir" ControlToValidate="txtCodigo" runat="server" Text="*" CssClass="asterisco"></asp:RequiredFieldValidator>
                                    </td>                                   
                                    <td>
                                        <asp:TextBox ID="txtDescricao" runat="server" CssClass="formNovo" MaxLength="50" TabIndex="2" Width="300px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvDescricao" ErrorMessage="Descricao não informada!" ValidationGroup="Inserir" ControlToValidate="txtDescricao" runat="server" Text="*" CssClass="asterisco"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </table>
                            <br style="line-height:5px" />
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnCancelar" runat="server" CssClass="botao" 
                                            onclick="btnCancelar_Click" Text="Cancelar" Width="80px" />
                                    </td>
                                    <td style="text-align:right">
                                        <asp:Button ID="btnSalvar" runat="server" CssClass="botao" 
                                        onclick="btnSalvar_Click" Text="Salvar" ValidationGroup="Inserir" Width="80px" />
                                   </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <asp:HiddenField ID="hdfCodCFOP" runat="server" />
                </table>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnIncluir" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="grdListaResultado" 
                EventName="RowCommand" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Panel>
<asp:HiddenField ID="hdfTargetIncluirForn" runat="server" />    
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" HeaderText="Os seguintes erros foram encontrados:" ShowSummary="False" ValidationGroup="Inserir" />
<asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"  ShowSummary="False" ValidationGroup="pesquisar" />
</asp:Content>

