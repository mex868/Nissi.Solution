﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.Master"CodeBehind="CadastraMensagemNF.aspx.cs" Inherits="Nissi.WebPresentation.cadastrarMensagemNF" %>
<%@ Register assembly="RDC.Tools" namespace="RDC.Tools" tagprefix="cc1" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register src="../UserControl/Endereco.ascx" tagname="Endereco" tagprefix="uc2" %>
<%@ Register src="../UserControl/Banco.ascx" tagname="Banco" tagprefix="uc1" %>
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
    //Criado por...: Alexandre Maximiano - 15/06/2010
    //Objetivo.....: Valida os campos
    //--------------------------------------------------------------------------------
    function ValidaCampos(src,args) {
        if (($get('<%=txtCodigoPesq.ClientID%>').value != '')) {
            args.IsValid = true;
        }
        else
            args.IsValid = false;
        
    }
</script>
<table style="margin-left: auto; width: 95%; margin-right: auto;">
    <tr>
        <td style="width: 28px">
            <asp:Image ID="ImgCadastro" runat="server" ImageUrl="~/Imagens/layout.png" Height="16px" />
        </td>
        <td class="titulo">Cadastro de Mensagem Nota Fiscal</td>
    </tr>
</table>
<br style="line-height:5px" />
<div style="text-align:center; width:100%;" id="divConsulta" runat="server">
    <div class="fundoTabela" style="width:95%" >
    <table id="tblConsulta" runat="server" style="text-align:left; width:100%">
        <tr>
            <td style="width: 20%;padding-left:17px" colspan="2">Consulta:<asp:TextBox 
                    ID="txtCodigoPesq" MaxLength="255" runat="server" Width="423px"></asp:TextBox>
                    <asp:CustomValidator ClientValidationFunction="ValidaCampos" Text="*" CssClass="asterisco" ValidationGroup="pesquisar" ErrorMessage="Favor informar o campo descrição." runat="server" id="efvCNPJ"></asp:CustomValidator>
                </td>
        </tr>
        <tr>
            <td style="text-align:left">
                <asp:Button ID="btnVoltar" runat="server" OnClick="btnVoltar_Click" 
                    CssClass="botao" Text="Voltar" Width="100px" UseSubmitBehavior="False" />
            </td>
            <td style="text-align:right">
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
                                pagesize="15" 
                                showpagedetails="True" 
                                AllowPaging="True" 
                                MultiSelection="True" 
                                ShowHeaderCheckBoxColumn="False" 
                                ShowOptionColumn="False" 
                                CssClass="alinhamento" 
                                onpageindexchanging="grdListaResultado_PageIndexChanging" 
                                onrowcommand="grdListaResultado_RowCommand" 
                                onrowdatabound="grdListaResultado_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Ações">
                                        <itemtemplate>
                                            <asp:ImageButton ID="imgEditar" Width="15px" Height="15px" runat="server" ImageUrl="~/Imagens/editar.png" />
                                            <asp:ImageButton ID="imgExcluir" Width="15px" Height="15px" runat="server" ImageUrl="~/Imagens/exclusao_Canc.png" />
                                         </itemtemplate>
                                        <HeaderStyle CssClass="headerGrid" Width="5%" />
                                        <ItemStyle HorizontalAlign="center" Wrap="false"/>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Descrição" >
                                        <HeaderStyle Wrap="false" CssClass="headerGrid"/>
                                        <ItemStyle HorizontalAlign="Left" Width="95%" />
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
                                        ::Inclusão de MensagemNF
                                <% } %>
                                <% else { %>
                                        ::Alteração de MensagemNF
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
                                    <td style="padding-left:20px">Descrição:</td> 
                                </tr>
                                <tr>                                
                                    <td>
                                        <asp:TextBox ID="txtDescricao" runat="server" CssClass="formNovo" MaxLength="50" TabIndex="2" Width="300px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvDescricao" ErrorMessage="Descrição não informada!" ValidationGroup="Inserir" ControlToValidate="txtDescricao" runat="server" Text="*" CssClass="asterisco"></asp:RequiredFieldValidator>
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
                    <asp:HiddenField ID="hdfCodMensagemNF" runat="server" />
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

