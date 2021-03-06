<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="CadastraFormaPgto.aspx.cs" Inherits="Nissi.WebPresentation.FormaPgto.CadastraFormaPgto" %>
<%@ Register assembly="RDC.Tools" namespace="RDC.Tools" tagprefix="cc1" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="server">
    <link href="../App_Themes/Theme1/Model1.css" type="text/css"  rel="Stylesheet" />
<script type="text/javascript">
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
    }
</script>
<table style="margin-left: auto; width: 95%; margin-right: auto;">
    <tr>
        <td style="width: 28px">
            <asp:Image ID="ImgCadastro" runat="server" ImageUrl="~/Imagens/layout.png" Height="16px" />
        </td>
        <td class="titulo">Cadastro de Forma de Pagamento</td>
    </tr>
</table>
<br />
<div style="text-align:center; width:100%;" id="divConsulta" runat="server">
    <div class="fundoTabela" style="width:95%">
    <table id="tblConsulta" runat="server" style="text-align:left; width:100%">
       <tr>
            <td style="text-align:left" colspan="2">
                <asp:Button ID="btnVoltar" runat="server" OnClick="btnVoltar_Click" 
                    CssClass="botao" Text="Voltar" Width="100px" UseSubmitBehavior="False" />
            </td>
            <td colspan="2" style="text-align:right">
                <asp:UpdatePanel ID="updBotoes" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                            &nbsp;<asp:Button ID="btnIncluir" runat="server" CssClass="botao" Width="100px"
                            Text="Incluir Novo"  onclick="btnIncluir_Click" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    </div>
    <br style="line-height:10px" />
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
                                onrowdatabound="grdListaResultado_RowDataBound" Width="95%" 
                                EnableModelValidation="True">
                                <Columns>
                                    <asp:TemplateField HeaderText="A��es">
                                        <itemtemplate>
                                            <asp:ImageButton ID="imgEditar" Width="15px" Height="15px" runat="server" ImageUrl="~/Imagens/editar.png" />
                                            <asp:ImageButton ID="imgExcluir" Width="15px" Height="15px" runat="server" ImageUrl="~/Imagens/exclusao_Canc.png" />
                                         </itemtemplate>
                                        <HeaderStyle CssClass="headerGrid" Width="5%" />
                                        <ItemStyle HorizontalAlign="center" Wrap="false"/>
                                    </asp:TemplateField>
                                     <asp:BoundField HeaderText="C�digo" >
                                         <headerstyle wrap="false" CssClass="headerGrid"></headerstyle>
                                         <itemstyle wrap="false" HorizontalAlign="Left"></itemstyle>
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Forma de Pagamento" >
                                        <HeaderStyle Wrap="false" CssClass="headerGrid"/>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Parcelas" >
                                        <HeaderStyle Wrap="false" CssClass="headerGrid"/>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Intervalo" >
                                        <HeaderStyle Wrap="false" CssClass="headerGrid"/>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                </Columns>
                            </cc1:RDCGrid>
                        </div>
        </ContentTemplate>
        <Triggers>
           <asp:AsyncPostBackTrigger ControlID="btnSalvar" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</div>
<br />
<ajaxToolkit:ModalPopupExtender ID="mpeIncluirFormaPgto" runat="server" PopupControlID="pnlIncluirFormaPgto"
    TargetControlID="hdfTargetIncluirBitola" BehaviorID="mpeIncluirFormaPgtoID" BackgroundCssClass="modalBackground"
    DropShadow="true" />    
<asp:Panel ID="pnlIncluirFormaPgto" DefaultButton="btnSalvar" runat="server">
    <asp:UpdatePanel  runat="server" ID="upCadastro" UpdateMode="Conditional">
        <ContentTemplate>
            <div style="text-align: center;width:600px; height:auto; padding: 5px 5px 5px 5px; background-color: #ffffff;">
                <table class="fundoTabela">
                    <!--T�TULO DA POPUP-->
                    <tr>
                        <td class="titulo">
                            <b>
                                <% if (hdfTipoAcao.Value == "Incluir") { %>
                                        ::Inclus�o de Forma de Pagamento
                                <% } %>
                                <% else { %>
                                        ::Altera��o de Forma de Pagamento
                                <% } %>
                            </b>
                        </td>
                    </tr>
                </table>
                <br />
                <table width="100%" align="center" class="fundoTabela" >
                    <tr> 
                        <td>
                            <table cellpadding="3" cellspacing="0" class="fundoTabela" style="text-align:left">
                                <tr>
                                    <td style="width: 209px"><b>::Dados cadastrais:</b></td>
                                </tr>
                                <tr>
                                    <td style="padding-left:20px; width: 209px;">Forma de Pagamento:</td>
                                    <td style="padding-left:20px; width: 168px;">Qtde de Parcelas</td>
                                    <td style="padding-left:20px">Intervalo</td>
                                </tr>
                                <tr>
                                    <td style="width: 209px">
                                        <asp:TextBox ID="txtDescricao" runat="server" CssClass="formNovo" 
                                            MaxLength="50" TabIndex="1" Width="174px"></asp:TextBox>
                                       <asp:RequiredFieldValidator Text="*" ErrorMessage="Favor informar o Forma de Pagamento." runat="server" ID="rfvBitola" ControlToValidate="txtDescricao" CssClass="asterisco" ValidationGroup="cadastro"></asp:RequiredFieldValidator>
                                    </td>
                                    <td style="width: 168px"><asp:TextBox runat="server" ID="txtParcelas" onkeypress="OnlyNumbers();"></asp:TextBox>
                                    <asp:RequiredFieldValidator Text="*" ErrorMessage="Favor informar o Qtde de Parcelas." runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtParcelas" CssClass="asterisco" ValidationGroup="cadastro"></asp:RequiredFieldValidator>
                                    </td>
                                    <td><asp:TextBox runat="server" ID="txtIntervalo" onkeypress="OnlyNumbers();"></asp:TextBox></td>
                                    </tr>
                                    </table>
                            <asp:UpdatePanel ID="upBotoesCadastro" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table class="fundoTabela">
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnCancelar" runat="server" CssClass="botao" 
                                                    onclick="btnCancelar_Click" Text="Cancelar" Width="80px" 
                                                    UseSubmitBehavior="False" />
                                            </td>
                                            <td style="text-align:right">
                                                <asp:Button ID="btnSalvar" runat="server" CssClass="botao" 
                                                onclick="btnSalvar_Click" Text="Salvar" ValidationGroup="cadastro" Width="80px" />
                                           </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <asp:HiddenField ID="hdfCodFormaPgto" runat="server" />
                </table>
            </div>
            <asp:HiddenField ID="hdfTipoAcao" runat="server" />
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnIncluir" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="grdListaResultado" 
                EventName="RowCommand" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Panel>
<asp:HiddenField ID="hdfCadastroPopup" runat="server" />
<asp:HiddenField ID="hdfTargetIncluirBitola" runat="server" />    
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="cadastro" />
</asp:Content>
