<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="CadastraCargo.aspx.cs" Inherits="CadastraCargo" %>
<%@ Register assembly="RDC.Tools" namespace="RDC.Tools" tagprefix="cc1" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="server">
    <link href="../App_Themes/Theme1/Model1.css" type="text/css"  rel="Stylesheet" />
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
   //Criado por...: Jacqueline Albuquerque - 07/12/2009
   //Objetivo.....: Valida os campos do cadastro
   //--------------------------------------------------------------------------------

</script>
<table style="margin-left: auto; width: 95%; margin-right: auto;">
    <tr>
        <td style="width: 28px">
            <asp:Image ID="ImgCadastro" runat="server" ImageUrl="~/Imagens/layout.png" Height="16px" />
        </td>
        <td class="titulo">Cadastro de Cargo</td>
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
                                ShowHeaderCheckBoxColumn="false" 
                                ShowOptionColumn="false" 
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
                                         <headerstyle wrap="false" CssClass="headerGrid"></headerstyle>
                                         <itemstyle wrap="false" HorizontalAlign="Left"></itemstyle>
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Cargo" >
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
<ajaxToolkit:ModalPopupExtender ID="mpeIncluirCargo" runat="server" PopupControlID="pnlIncluirCargo"
    TargetControlID="hdfTargetIncluirCargo" BehaviorID="mpeIncluirCargoID" BackgroundCssClass="modalBackground"
    DropShadow="true" />    
<asp:Panel ID="pnlIncluirCargo" runat="server">
    <asp:UpdatePanel  runat="server" ID="upCadastro" UpdateMode="Conditional">
        <ContentTemplate>
            <div style="text-align: center;width:900px; height:auto; padding: 5px 5px 5px 5px; background-color: #ffffff;">
                <table class="fundoTabela">
                    <!--TÍTULO DA POPUP-->
                    <tr>
                        <td class="titulo">
                            <b>
                                <% if (hdfTipoAcao.Value == "Incluir") { %>
                                        ::Inclusão de Cargo
                                <% } %>
                                <% else { %>
                                        ::Alteração de Cargo
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
                                    <td><b>::Dados cadastrais:</b></td>
                                </tr>
                                <tr>
                                    <td style="padding-left:20px">Cargo:</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtCargo" runat="server" CssClass="formNovo" 
                                            MaxLength="50" TabIndex="1" Width="300px"></asp:TextBox>
                                       <asp:RequiredFieldValidator Text="*" ErrorMessage="Favor informar o cargo." runat="server" ID="rfvCargo" ControlToValidate="txtCargo" CssClass="asterisco" ValidationGroup="cadastro"></asp:RequiredFieldValidator>
                                    </td>
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
                    <asp:HiddenField ID="hdfCodCargo" runat="server" />
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
<asp:HiddenField ID="hdfTargetIncluirCargo" runat="server" />    
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="cadastro" />
</asp:Content>
