<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ListaSubMenu.aspx.cs" Inherits="ListaSubMenu" %>
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
    </script>
    <div style="text-align:center">
    <table style="border-style: solid; border-width: 1px; margin-left: auto; width: 95%; margin-right: auto; border-color:Black;">
        <tr>
            <td style="width: 21px">
                <asp:Image ID="ImgCadastro" runat="server" ImageUrl="~/Imagens/layout.png" />
            </td>
            <td style="width: 97%; text-align: left">
                <b>Lista de SubMenu</b></td>
        </tr>
    </table>
    <br />
<div style=" text-align:center; width:100%;">
    <table class="fundoTabela">
        <tr>
            <td>
                <asp:Button ID="btnVoltar" Width="100px" CssClass="botao" runat="server" 
                    text="Voltar" onclick="btnVoltar_Click" />
            </td>
            <td style="text-align:right" colspan="2">
                <asp:UpdatePanel ID="updBotoes" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                &nbsp;<asp:Button ID="btnIncluir" Width="100px" CssClass="botao" runat="server" 
                        Text="Incluir Novo" onclick="btnIncluir_Click" />
                </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    </div>
<br />    
     <asp:HiddenField ID="hdfCodMenu" runat="server" />    
     <asp:UpdatePanel ID="updListaResultado" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
    <div id="divListaResultado" runat="server" style="overflow: auto; display: block; text-align:center; height:400px;">
                    <cc1:RDCGrid id="grdListaResultado" runat="server" autogeneratecolumns="False" 
                        cellpadding="1" cellspacing="3" 
                        gridlines="None" pagesize="30" 
                        showpagedetails="True"  AllowPaging="True" 
                        MultiSelection="False" ShowHeaderCheckBoxColumn="False" 
                        ShowOptionColumn="False" 
                        onpageindexchanging="grdListaResultado_PageIndexChanging" 
                        onrowcommand="grdListaResultado_RowCommand" 
                        onrowdatabound="grdListaResultado_RowDataBound" CssClass="alinhamento" 
                        Width="95%" EnableModelValidation="True">
                    <Columns>
                        <asp:TemplateField HeaderText="Ações">
                            <itemtemplate>
                            <asp:ImageButton ID="imgEditar" runat="server" ImageUrl="~/Imagens/editar.png">
                                </asp:ImageButton>
                                    <asp:ImageButton ID="imgExcluir" runat="server" 
                                    ImageUrl="~/Imagens/exclusao_Canc.png">
                                </asp:ImageButton> 
                                <asp:ImageButton ID="imgRoles" runat="server" 
                                    ImageUrl="~/Imagens/DatabasePermissionsMenu.png" />
                            </itemtemplate>
                        
                            <HeaderStyle CssClass="headerGrid" HorizontalAlign="Left" Width="10%" />
                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                        
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Descrição" >
                            <HeaderStyle CssClass="headerGrid" HorizontalAlign="Left" Width="30%" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Url" >
                            <HeaderStyle CssClass="headerGrid" HorizontalAlign="Left" Width="30%" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Ordem">
                        <HeaderStyle CssClass="headerGrid" Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="ResolveUrl" >
                            <HeaderStyle CssClass="headerGrid" HorizontalAlign="Left" Width="10%" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Ativo">
                        <HeaderStyle CssClass="headerGrid" Width="10%" />
                        </asp:BoundField>
                    </Columns>
                </cc1:RDCGrid>
    </div>
    </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSalvar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnCancelar" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
        </div>
        <asp:HiddenField ID="hdfTargetInclui" runat="server" />
    <ajaxToolkit:ModalPopupExtender ID="mpeInclui" runat="server" PopupControlID="pnlInclui"
        TargetControlID="hdfTargetInclui" BehaviorID="mpeIncluiID" BackgroundCssClass="modalBackground"
        DropShadow="true" />
    <asp:Panel ID="pnlInclui" runat="server" Style="background-color: #DDDDDD; border: solid 1px Gray; color: Black">
        <div style="text-align: center; width: 250px; height: auto; padding: 5px 5px 5px 5px; background-color: #ffffff;">
                   <asp:UpdatePanel ID="updInclui" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:HiddenField ID="hdfTipoAcao" runat="server" />
                        <asp:HiddenField ID="hdfCodSubMenu" runat="server" />
                        <table border="0" style="width:95%; margin-right: auto; margin-left: auto; border: solid 1px black; text-align:left;" cellpadding="0" cellspacing="3">
                        <!--TÍTULO DA POPUP-->
                        <tr>
                            <td class="FundoLinha2">
                                <b>
                                <% if (hdfTipoAcao.Value == "Incluir") { %>
                                        ::Inclusão de SubMenu
                                <% } %>
                                <% else { %>
                                        ::Alteração de SubMenu
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
                                        ErrorMessage="Campo descrição deve ser preenchido!" 
                                        ValidationGroup="inserir">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Url:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtUrl" runat="server">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                    ControlToValidate="txtUrl" 
                                    ErrorMessage="Campo Url deve ser preenchido!" ValidationGroup="inserir">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Ordem:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtOrdem" runat="server">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                        ControlToValidate="txtOrdem" ErrorMessage="Campo Ordem deve ser preenchido!" 
                                        ValidationGroup="inserir">*</asp:RequiredFieldValidator>
                                </td>
                            <tr>
                                <td>
                                    Resolveurl:
                                </td>
                                <td>
                                    <asp:CheckBox ID="ckbResolveurl" runat="server" ValidationGroup="inserir" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Ativo:
                                </td>
                                <td>
                                    <asp:CheckBox ID="ckbIncluir" runat="server" ValidationGroup="Inserir" />
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
        <br />
    </asp:Panel>
    <asp:HiddenField ID="hdfTargetRoles" runat="server" />  
    <ajaxToolkit:ModalPopupExtender ID="mpeRoles" runat="server" PopupControlID="pnlIncluirRoles"
        TargetControlID="hdfTargetRoles" BehaviorID="mpeRolesID" BackgroundCssClass="modalBackground"
        DropShadow="true" /> 
        <asp:Panel ID="pnlIncluirRoles" runat="server">
        <div style="text-align: center; width: 530px; height: auto; padding: 5px 5px 5px 5px; background-color: #ffffff;">
        <asp:UpdatePanel ID="updRoles" runat="server" UpdateMode="Conditional">
           <ContentTemplate>
                 <asp:HiddenField ID="hdfCodSubMenuRoles" runat="server" />     
            <table class="fundoTabela">
            <tr>
            <td>Menu:</td>
            <td colspan="2"><b><asp:Label ID="lblMenu" runat="server"></asp:Label></b></td>
            </tr>
                        <tr>
                            <td  valign="center" width="25%">
                                Perfis Disponíveis</td>
                            <td style="WIDTH: 10%" valign="center">
                                <span ID="spanComboGrupo">&nbsp; </span>
                            </td>
                            <td valign="center">
                                Perfis Associados</td>
                        </tr>
                        <tr>
                            <td align="middle" width="25%">
                                <p align="left">
                                    <asp:ListBox ID="lbxAssociar" runat="server" CssClass="formulario" 
                                        Height="134px" SelectionMode="Multiple" Width="224px" 
                                        onselectedindexchanged="lbxAssociar_SelectedIndexChanged"></asp:ListBox>
                                </p>
                            </td>
                            <td align="middle" style="WIDTH: 10%" valign="center">
                                <div align="center">
                                    &nbsp;</div>
                                <div align="center">
                                    <asp:Button ID="btnAssociar" runat="server" CausesValidation="False" 
                                        CssClass="botao" Text="&gt;" Width="48px" onclick="btnAssociar_Click" />
                                    <br />
                                    <asp:Button ID="btnRetirar" runat="server" CausesValidation="False" 
                                        CssClass="botao" Text="&lt;" Width="48px" onclick="btnRetirar_Click" />
                                    <br />
                                    <asp:Button ID="btnAssociarTodos" runat="server" CausesValidation="False" 
                                        CssClass="botao"  Text="&gt;&gt;" 
                                        Width="48px" onclick="btnAssociarTodos_Click" />
                                    <br />
                                    <asp:Button ID="btnRetirarTodos" runat="server" CausesValidation="False" 
                                        CssClass="botao" Text="&lt;&lt;" Width="48px" 
                                        onclick="btnRetirarTodos_Click" />
                                </div>
                            </td>
                            <td class="associados" style="WIDTH: 366px" valign="center">
                                <p align="left">
                                    <asp:ListBox ID="lbxAssociados" runat="server" CssClass="formulario" 
                                        Height="134px" SelectionMode="Multiple" Width="224px"></asp:ListBox>
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:left">
                                <asp:Button ID="btnCancelarRoles"  Text="Cancelar" runat="server" 
                                    CssClass="botao" Width="80px" onclick="btnCancelarRoles_Click" />
                                 </td>
                                    <td style="text-align:right" colspan="2">
                                        <asp:Button ID="btnSalvarRoles" Text="Salvar" runat="server" CssClass="botao" 
                                            Width="80px" ValidationGroup="Inserir" onclick="btnSalvarRoles_Click"/>
                                    </td>
                                </tr>
                    </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="grdListaResultado" 
                            EventName="RowCommand" />
            </Triggers>
                    </asp:UpdatePanel>
                    </div>
        </asp:Panel>
        <br />    
</asp:Content>
