<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeFile="CadastroPerfilAcesso.aspx.cs" Inherits="Administracao_PerfilAcesso_CadastroPerfilAcesso" EnableEventValidation="false" Title="Cadastro de Perfil" %>
<%@ Register Assembly="RDC.Tools" Namespace="RDC.Tools" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="server">
    <script type ="text/javascript" src ="../../JS/FuncoesComuns.js"></script>

    <script type="text/javascript" language="javascript">

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_initializeRequest(InitializeRequest);
        prm.add_endRequest(EndRequest);
        var postBackElement;

        function InitializeRequest(sender, args)
        {
            if (prm.get_isInAsyncPostBack())
                args.set_cancel(true);

            if (args.get_postBackElement().type != 'checkbox')
                Load(true);
        }

        function EndRequest(sender, args)
        {
            Load(false);
        }

        function mudarCor()
        {
            document.getElementById('TagBody').style.background = "#E5E5E5";
        }
        mudarCor();

        /************************************************************************************
        - Criado Por: Rodrigo Galvão
        - Criado Em.: 25/02/2009
        - Objetivo..: Manipular itens de associação nas Listboxes
        **************************************************************************************/
        function btnAssociar_Click()
        {
            fnTrocaLista($get('<%=lbxAssociar.ClientID%>'), $get('<%=lbxAssociados.ClientID%>'), false);
        }
        function btnRetirar_Click()
        {
            fnTrocaLista($get('<%=lbxAssociados.ClientID%>'), $get('<%=lbxAssociar.ClientID%>'), false);
        }
        function btnAllAssociar_Click()
        {
            fnTrocaLista($get('<%=lbxAssociar.ClientID%>'), $get('<%=lbxAssociados.ClientID%>'), true);
        }
        function btnAllRetirar_Click()
        {
            fnTrocaLista($get('<%=lbxAssociados.ClientID%>'), $get('<%=lbxAssociar.ClientID%>'), true);
        }

        /************************************************************************************
        - Criado Por: Rodrigo Galvão
        - Criado Em.: 26/02/2009
        - Objetivo..: Armazenar funcionalidades associadas em campo hidden
        **************************************************************************************/
        function ItensAssociados()
        {
            if ($get('<%=tbxNomePerfil.ClientID%>').value != '')
            {
                $get('<%=hdnListaFuncionalidade.ClientID%>').value = '';
                var qtdeFuncionalidade = 0;
                var nomeFuncionalidade = '';

                for(var i=0; i<$get('<%=lbxAssociados.ClientID%>').options.length; i++)
                {
                    $get('<%=hdnListaFuncionalidade.ClientID%>').value += $get('<%=lbxAssociados.ClientID%>').options[i].value + '|';
                    nomeFuncionalidade = $get('<%=lbxAssociados.ClientID%>').options[i].text;
                    qtdeFuncionalidade++;
                }

                var texto = '';

                if (qtdeFuncionalidade == 0)
                    texto = 'Nenhuma Funcionalidade associada.\n\n';
                else if (qtdeFuncionalidade == 1)
                    texto = 'Você selecionou a Funcionalidade "' + nomeFuncionalidade.toString() + '".\n\n';
                else
                    texto = 'Você têm ' + qtdeFuncionalidade.toString() + ' funcionalidades associadas.\n\n';

                texto += 'Confirmar gravação?'

                return confirm(texto);
            }
            else
            {
                alert('Favor informar o Nome do Perfil!');
                PosicionarFoco();
                return false;
            }
        }

        function PosicionarFoco()
        {
            $get('<%=tbxNomePerfil.ClientID%>').focus();
        }

    </script>

    <br />

    <div style="text-align:center;">

        <table border="0" style="width:95%; margin-right: auto; margin-left: auto;">
            <tr>
                <td style="width:3%;text-align:left;"><img height="16" src="../../Imagens/layout.png" width="18" alt="" /></td>
                <td style="width:97%;text-align:left;" class="titulo">
                    <asp:Label ID="lblTitulo" runat="server" CssClass="titulo">
                        Cadastro de Perfil
                    </asp:Label>
                </td>
            </tr>
        </table>

        <br style="line-height: 7px" />

        <asp:UpdatePanel ID="updFormPerfil" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnGrava" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnCancelaEdicao" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="grdFPerfil" EventName="RowCommand" />
            </Triggers>
            <ContentTemplate>
                <asp:HiddenField ID="hdnCodPerfilAlteracao" runat="server" />
                <table id="tbPerfil" runat="server" style="width:95%; margin-right: auto; margin-left: auto;" class="fundotabelaou" cellpadding="3" cellspacing="0">
                    <tr>
                        <td class="tituloCampo" style="text-align:left;width:40%;" valign="top">
                            <table border="0" style="width:100%; margin-right: auto; margin-left: auto; height:263px;" cellpadding="3" cellspacing="0" class="fundotabelain">
                                <tr>
                                    <td style="width:100%; line-height:15px;" class="fundoTitulo" colspan="3">
                                        ::Dados Principais
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tituloCampo" style="text-align:left;width:45%;" valign="top">
                                        <table border="0" cellpadding="3" cellspacing="0" style="width:100%;">
                                            <tr>
                                                <td class="tituloCampo" style="text-align:left;width:100%;">
                                                    Nome do Perfil
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tituloCampo" style="text-align:left;width:100%;">
                                                    <asp:TextBox ID="tbxNomePerfil" runat="server" CssClass="formNovo" Width="70%" MaxLength="60"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tituloCampo" style="text-align:left;width:100%;">
                                                    Descrição
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tituloCampo" style="text-align:left;width:100%;">
                                                    <asp:TextBox ID="tbxDescricao" runat="server" CssClass="formNovo" Width="90%" MaxLength="100" TextMode="MultiLine" Rows="5"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tituloCampo" style="text-align:left;width:45%;" valign="bottom">
                                        <table border="0" cellpadding="0" cellspacing="0" style="width:100%;">
                                            <tr>
                                                <td class="tituloCampo" style="text-align:left;width:34%;">
                                                    <asp:Button ID="btnSai" runat="server" Text="Sair" Width="110px" CssClass="botaoNovo" OnClick="btnSai_Click" />
                                                </td>
                                                <td class="tituloCampo" style="text-align:center;width:33%;">
                                                    <asp:Button ID="btnCancelaEdicao" runat="server" Text="Cancelar Edição" Width="110px" CssClass="botaoNovo" Visible="false" OnClick="btnCancelaEdicao_Click" />
                                                </td>
                                                <td class="tituloCampo" style="text-align:right;width:34%;">
                                                    <asp:HiddenField ID="hdnListaFuncionalidade" runat="server" />
                                                    <asp:Button ID="btnGrava" runat="server" Text="Gravar" Width="110px" CssClass="botaoNovo"
                                                        OnClientClick="return ItensAssociados();" OnClick="btnGrava_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="tituloCampo" style="text-align:left;width:60%;">
                            <div id="dvListBoxAssociar" runat="server" style="overflow: auto; display: block; width:100%; text-align:left;">
                                <table border="0" style="width:100%; margin-right: auto; margin-left: auto; height:263px;" cellpadding="10" cellspacing="0" class="fundotabelain">
                                    <tr>
                                        <td style="width:100%; line-height:7px;" class="fundoTitulo" colspan="3">
                                            ::Associação de Funcionalidade
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tituloCampo" style="text-align:left;width:45%;">
                                            <table border="0" cellpadding="0" cellspacing="0" style="width:100%;">
                                                <tr>
                                                    <td class="tituloCampo" style="text-align:left;width:100%;">
                                                        <table border="0" cellpadding="0" cellspacing="0" style="width:100%;">
                                                            <tr>
                                                                <td class="tituloCampo" style="text-align:left;width:100%;">
                                                                    Funcionalidade(s) Disponível(is)
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tituloCampo" style="text-align:left;width:100%;">
                                                        <asp:ListBox ID="lbxAssociar" runat="server" CssClass="formNovo" 
                                                            SelectionMode="Multiple" Width="100%" Height="190px"
                                                            OnDblClick="btnAssociar_Click();">
                                                        </asp:ListBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="tituloCampo" style="text-align:center;width:10%;">
                                            <table border="0" style="width:100%" cellpadding="0" cellspacing="5">
                                                <tr>
                                                    <td class="tituloCampo" style="text-align:center;width:100%;">
                                                        <input class="botaoNovo" id="btnAssociar" style="WIDTH: 48px" 
                                                            onclick="btnAssociar_Click();" type="button" value=">" name="btnAssociar" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tituloCampo" style="text-align:center;width:100%;">
                                                        <input class="botaoNovo" id="btnRetirar" style="WIDTH: 48px" 
                                                            onclick="btnRetirar_Click();" type="button" value="<" name="btnRetirar" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tituloCampo" style="text-align:center;width:100%;">
                                                        <input class="botaoNovo" id="btnAllAssociar" style="WIDTH: 48px" 
                                                            onclick="btnAllAssociar_Click();" type="button" value=">>" name="btnAllAssociar" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tituloCampo" style="text-align:center;width:100%;">
                                                        <input class="botaoNovo" id="btnAllRetirar" style="WIDTH: 48px" 
                                                            onclick="btnAllRetirar_Click();" type="button" value="<<" name="btnAllRetirar" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="tituloCampo" style="text-align:right;width:45%;">
                                            <table border="0" cellpadding="0" cellspacing="0" style="width:100%">
                                                <tr>
                                                    <td class="tituloCampo" style="text-align:left;width:100%;">
                                                        <br style="line-height:5px;" />
                                                        Funcionalidade(s) Associada(s)
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tituloCampo" style="text-align:right;width:100%;">
                                                        <asp:ListBox ID="lbxAssociados" runat="server" CssClass="formNovo" 
                                                            SelectionMode="Multiple" Width="100%" Height="190px" OnDblClick="btnRetirar_Click();">
                                                        </asp:ListBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>

        <br style="line-height: 7px" />

        <asp:UpdatePanel ID="updPerfil" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnGrava" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnCancelaEdicao" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="grdFPerfil" EventName="RowCommand" />
            </Triggers>
            <ContentTemplate>
                <div id="dvGridPerfil" runat="server" style="overflow: auto; display: block; width:95%; text-align:left; height:280px;">

                    <cc1:RDCGrid cssClass="fundotabelain" ID="grdFPerfil" ShowPageDetails="True" runat="server" AutoGenerateColumns="False" 
                        CellPadding="1" CellSpacing="3" GridLines="None" Width="100%" PageSize="7" BorderColor="Black" BorderWidth="1px" 
                        OnPageIndexChanging="grdFPerfil_PageIndexChanging" OnRowDataBound="grdFPerfil_RowDataBound" OnRowCommand="grdFPerfil_RowCommand">
                        <HeaderStyle CssClass="classtd" />
                        <Columns>

                            <asp:TemplateField HeaderText="A&#231;&#245;es">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgEditar" runat="server" />
                                    <asp:ImageButton ID="imgExcluir"  runat="server" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Width="5%" />
                            </asp:TemplateField>

                            <asp:BoundField HeaderText="Nome do Perfil">
                                <ItemStyle CssClass="classtdleft" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" Width="35%" />
                            </asp:BoundField>

                            <asp:BoundField HeaderText="Descrição">
                                <ItemStyle CssClass="classtdleft" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" Width="60%" />
                            </asp:BoundField>

                        </Columns>
                    </cc1:RDCGrid>

                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>

</asp:Content>

