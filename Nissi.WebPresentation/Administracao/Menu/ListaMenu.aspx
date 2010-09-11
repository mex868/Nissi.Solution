<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ListaMenu.aspx.cs" Inherits="ListaMenu" %>
<%@ Register Assembly="RDC.Tools" Namespace="RDC.Tools" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="server">

       <script type="text/javascript" language="javascript">
        //--------------------------------------------------------------------------------
        //Criado por...: Alexandre Maximiano - 02/11/2009
        //Objetivo.....: Cabeçalho padrão da página
        //--------------------------------------------------------------------------------
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_initializeRequest(InitializeRequest);
        prm.add_endRequest(EndRequest);
        var postBackElement;
        function InitializeRequest(sender, args)
        {
            if (prm.get_isInAsyncPostBack())
                args.set_cancel(true);

            if (args.get_postBackElement().type != 'checkbox')
                WaitAsyncPostBack(true);
        }
        function EndRequest(sender, args)
        {
            WaitAsyncPostBack(false);
        }        
    </script >
    <div style="text-align:center">
    <table style="border-style: solid; border-width: 1px; margin-left: auto; width: 95%; margin-right: auto; border-color:Black;">
        <tr>
            <td style="width: 21px">
                <asp:Image ID="ImgCadastro" runat="server" ImageUrl="~/Imagens/layout.png" />
            </td>
            <td style="width: 97%; text-align: left">
                <b>Lista de Menu</b></td>
        </tr>
    </table>
    <br />
    <div style=" text-align:center; width:100%;">
    <table class="fundoTabela">
        <tr>
            <td style="width: 12%; text-align: left">
                Opções de Consulta
            </td>
            <td style="text-align:left; width: 123px;">
                <asp:RadioButton ID="rbTodos" GroupName="filtroPesq" Text="Todos" 
                    runat="server" Checked="True" onclick="HabilitaTextBox(true);" />
            </td>
            <td style="text-align:left">
                <asp:RadioButton ID="rbDescricao" GroupName="filtroPesq" Text="Descrição" runat="server" onclick="HabilitaTextBox(false);" />
            </td>
        </tr>
        <tr>
        <td>
        </td>
        <td colspan="2" style="text-align:left"> 
        <asp:TextBox ID="txbPesquisa" runat="server" Width="204px"></asp:TextBox>
        </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnVoltar" Width="100px" CssClass="botao" runat="server" 
                    text="Voltar" onclick="btnVoltar_Click" />
            </td>
            <td style="text-align:right" colspan="2">
                <asp:UpdatePanel ID="updBotoes" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                <asp:Button ID="btnPesquisar" Width="100px" CssClass="botao" runat="server" 
                        Text="Pesquisar" onclick="btnPesquisar_Click" />
                &nbsp;<asp:Button ID="btnIncluir" Width="100px" CssClass="botao" runat="server" 
                        Text="Incluir Novo" onclick="btnIncluir_Click" />
                </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    </div>
</div>
<br />

    <asp:UpdatePanel ID="updListaResultado" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
    <div id="divListaResultado" runat="server" style="overflow: auto; display: block; text-align:center; height:400px;">
                    <cc1:RDCGrid id="grdListaResultado" runat="server" autogeneratecolumns="False" 
                        cellpadding="1" cellspacing="3" 
                        gridlines="None" pagesize="15" 
                        showpagedetails="True"  AllowPaging="True" 
                        MultiSelection="False" ShowHeaderCheckBoxColumn="False" 
                        ShowOptionColumn="False" 
                        onpageindexchanging="grdListaResultado_PageIndexChanging" 
                        onrowcommand="grdListaResultado_RowCommand" 
                        onrowdatabound="grdListaResultado_RowDataBound" CssClass="alinhamento">
                    <Columns>
                        <asp:TemplateField HeaderText="Ações">
                            <itemtemplate>
                            <asp:ImageButton ID="imgEditar" runat="server" ImageUrl="~/Imagens/editar.png">
                                </asp:ImageButton>
                                    <asp:ImageButton ID="imgExcluir" runat="server" 
                                    ImageUrl="~/Imagens/exclusao_Canc.png">
                                </asp:ImageButton> 
                                <asp:ImageButton ID="imgSubMenu" runat="server" 
                                    ImageUrl="~/Imagens/AdpDiagramRelationships.png" />
                            </itemtemplate>
                        
                            <HeaderStyle CssClass="headerGrid" HorizontalAlign="Left" Width="30%" />
                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                        
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Descrição" >
                            <HeaderStyle CssClass="headerGrid" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Ativo">
                        <HeaderStyle CssClass="headerGrid" />
                        </asp:BoundField>
                    </Columns>
                </cc1:RDCGrid>
    </div>
    </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnPesquisar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnSalvar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnCancelar" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>

    <asp:HiddenField ID="hdfTargetIncluiMenu" runat="server" />
    <ajaxToolkit:ModalPopupExtender ID="mpeIncluiMenu" runat="server" PopupControlID="pnlIncluiMenu"
        TargetControlID="hdfTargetIncluiMenu" BehaviorID="mpeIncluiMenuID" BackgroundCssClass="modalBackground"
        DropShadow="true" />
    <asp:Panel ID="pnlIncluiMenu" runat="server" Style="background-color: #DDDDDD; border: solid 1px Gray; color: Black">
        <div style="text-align: center; width: 250px; height:auto; padding: 5px 5px 5px 5px; background-color: #ffffff;">
                   <asp:UpdatePanel ID="updIncluiMenu" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:HiddenField ID="hdfTipoAcao" runat="server" />
                        <asp:HiddenField ID="hdfCodMenu" runat="server" />
                        <table border="0" style="width:95%; margin-right: auto; margin-left: auto; border: solid 1px black; text-align:left;" cellpadding="0" cellspacing="3">
                        <!--TÍTULO DA POPUP-->
                        <tr>
                            <td class="FundoLinha2">
                                <b>
                                <% if (hdfTipoAcao.Value == "Incluir") { %>
                                        ::Inclusão de Menu
                                <% } %>
                                <% else { %>
                                        ::Alteração de Menu
                                <% } %>
                                </b>
                            </td>
                        </tr>
                        </table>
                        <table class="fundoTabela">
                            <tr>
                                <td style="text-align:left">
                                    Descrição:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtIncluirMenu" runat="server">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                        ControlToValidate="txtIncluirMenu" 
                                        ErrorMessage="Campo descrição deve ser preenchido!" ValidationGroup="inserir">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Ativo:
                                </td>
                                <td>
                                    <asp:CheckBox ID="ckbIncluirMenu" runat="server" ValidationGroup="Inserir" />
                                </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnCancelar"  Text="Cancelar" runat="server" CssClass="botao" 
                                            onclick="btnCancelar_Click" Width="80px" />
                                    </td>
                                    <td style="text-align:right">
                                        <asp:Button ID="btnSalvar" Text="Salvar" runat="server" CssClass="botao" 
                                            Width="80px" ValidationGroup="Inserir" onclick="btnSalvar_Click"/>
                                    </td>
                                </tr>
                        </table>
                    </ContentTemplate>
                       <Triggers>
                           <asp:AsyncPostBackTrigger ControlID="btnIncluir" EventName="Click" />
                           <asp:AsyncPostBackTrigger ControlID="grdListaResultado" 
                               EventName="RowCommand" />
                       </Triggers>
                   </asp:UpdatePanel>
        </div>
        <asp:ValidationSummary ID="vlsIncluirMenu" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Inserir" /> 
    </asp:Panel>
        <br />
</asp:Content>
