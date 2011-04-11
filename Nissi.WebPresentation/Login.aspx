<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Nissi.WebPresentation.Login" Title="[Nissi] Login de Acesso" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>

<asp:Content ID="cttLogin" ContentPlaceHolderID="cphPrincipal" runat="server">
    
    <script type ="text/javascript" src ="JScripts/Common.js"></script>

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
        /************************************************************************************
        - Criado Por: Rodrigo Galvão
        - Criado Em.: 09/03/2009
        - Objetivo..: Acionar botão gravar quando pressionada a tecla ENTER na caixa de confirmação da senha
        **************************************************************************************/
        function ValidaNovaSenha(src, args) {
            var ctvConfSenha = $get('<%=ctvConfSenha.ClientID%>');
            var validaCampos = true;

            if ($get('<%=tbxNovaSenha.ClientID%>').value.length < 6) {
                validaCampos = false;
                ctvConfSenha.errormessage = 'A Senha deve conter no mínimo 6 caracteres.';
            }
            else if ($get('<%=tbxConfSenha.ClientID%>').value == '') {
                validaCampos = false;
                ctvConfSenha.errormessage = 'Confirme a Nova Senha informada preenchendo o campo indicado.';
            }
            else if ($get('<%=tbxConfSenha.ClientID%>').value != $get('<%=tbxNovaSenha.ClientID%>').value) {
                validaCampos = false;
                ctvConfSenha.errormessage = 'Nova Senha diferente da Senha Confirmada. Favor digitar novamente.';
            }
            else
                validaCampos = true;

            args.IsValid = validaCampos;
        }
        //--------------------------------------------------------------------------------
        //Criado por...: Alexandre Maximiano - 02/11/2009
        //Objetivo.....: Acionar botão acessar quando pressionada a tecla ENTER na caixa de senha
        //--------------------------------------------------------------------------------
        function KeyDownHandlerAcesso()
        {
            var textboxNomeUsuario = $get('<%=tbxNomeUsuario.ClientID%>');
            var textboxSenha = $get('<%=tbxSenha.ClientID%>');
            if (textboxNomeUsuario.value != '' && textboxSenha.value != '')
            {
                if (event.keyCode == 13)
                {
                    textboxNomeUsuario.disabled = true;
                    textboxSenha.disabled = true;
                    event.returnValue=false;
                    event.cancel = true;
                    $get('<%=btnLogin.ClientID%>').click();
                    textboxNomeUsuario.disabled = false;
                    textboxSenha.disabled = false;
                }
            }
        }

        /************************************************************************************
        - Criado Por: Rodrigo Galvão
        - Criado Em.: 09/03/2009
        - Objetivo..: Acionar botão gravar quando pressionada a tecla ENTER na caixa de confirmação da senha
        **************************************************************************************/
        function KeyDownHandlerNovaSenha() {
            var textboxNovaSenha = $get('<%=tbxNovaSenha.ClientID%>');
            var textboxConfSenha = $get('<%=tbxConfSenha.ClientID%>');
            if (textboxNovaSenha.value != '' && textboxConfSenha.value != '') {
                if (event.keyCode == 13) {
                    textboxNovaSenha.disabled = true;
                    textboxConfSenha.disabled = true;
                    event.returnValue = false;
                    event.cancel = true;
                    $get('<%=btnGrava.ClientID%>').click();
                    textboxNovaSenha.disabled = false;
                    textboxConfSenha.disabled = false;
                }
            }
        }
    </script>
    <div style="text-align: center; margin-top: 100px;">
        <table  style="width: 370px; border-style: solid; border-color: Black;border-width: 1px; margin-right: auto; margin-left: auto;" 
            cellpadding="5">
        <tr>
            <td colspan="2" style="filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0, StartColorStr=#77AA1F, EndColorStr=#FFFFFF);
                    border-right: 1px inset; border-top: 1px inset; border-left: 1px inset;
                    color: #404040; border-bottom: 1px inset; text-align: left; border-color: #CFCFCF;"
                    valign="middle">
            <b>[Nissi] Login de Acesso</b>
            </td>
        </tr>
        <tr>
          <td colspan="2" style="line-height: 5px;border-top:1px solid black">
              &nbsp;</td>
        </tr>
        <tr>
            <td align="right"  style="width: 100px;">
            <b>Usuário:</b>
            </td>
            <td align="left" style="width: 100%;">
              <asp:TextBox ID="tbxNomeUsuario" runat="server"  Font-Size="12px"
              Height="18px" Width="180px" onkeydown="KeyDownHandlerAcesso();"></asp:TextBox>
              <asp:RequiredFieldValidator ID="rfvNomeUsuario" runat="server" ControlToValidate="tbxNomeUsuario" ErrorMessage="Informe o Login do Usuário" ValidationGroup="ValidaDadosLogin">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr style="font-weight: bold">
        <td  align="right"  style="width: 100px;">
        <b>Senha:</b>
        </td>
        <td align="left" style="width:100%;">
          <asp:TextBox ID="tbxSenha" runat="server" Font-Size="12px" Height="18px"
          Text="0" TextMode="Password" Width="120px" MaxLength="10" onkeydown="KeyDownHandlerAcesso();"></asp:TextBox>
          <asp:RequiredFieldValidator ID="rfvSenha" runat="server" ControlToValidate="tbxSenha" ErrorMessage="Informe a Senha do Usuário" ValidationGroup="ValidaDadosLogin">*</asp:RequiredFieldValidator>
        </td>
        </tr>
        <tr>
           <td colspan="2" style="line-height: 5px;">
           &nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: left; width: 100px;">
                    <input type="button" id="btnFecha" value="Fechar" style="width:90px;" 
                                onclick="window.close();" class="botao" />
             </td>
             <td style="text-align: right;">
                    <asp:UpdatePanel ID="updLogin" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Button ID="btnLogin" runat="server" Text="Acesso>>" 
                                ValidationGroup="ValidaDadosLogin" Width="90px" OnClick="btnLogin_Click" 
                                CssClass="botao" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
             </td>
            </tr>
        </table>
    </div>
    <input id="hndTargetControl" runat="server" type="hidden" />
    <ajaxToolkit:ModalPopupExtender ID="mpeNovaSenha" runat="server" PopupControlID="PNL"
        TargetControlID="hndTargetControl" BehaviorID="mpeNovaSenhaID" BackgroundCssClass="modalBackground"
        CancelControlID="CancelButton" DropShadow="true" />
    <asp:Panel ID="PNL" runat="server" Style="cursor: move; background-color: #DDDDDD;
        border: solid 1px Gray; color: Black" TabIndex="5">
        <div style="text-align: center; width: 504px; height: 220px; padding: 5px 5px 5px 5px">
            <table border="0" cellpadding="3" cellspacing="3" style="width: 100%; height: 179px;"
                class="fundotabela">
                <tr>
                    <td class="fundoTitulo" style="height: 10%;" colspan="2">
                        ::Esta é a sua primeira conexão, favor preencher os campos abaixo para criação de
                        SENHA DE ACESSO...
                    </td>
                </tr>
                <tr>
                    <td style="height: 80%;" valign="top" colspan="2">
                        <table border="0" cellpadding="5" cellspacing="5" style="width: 100%">
                            <tr>
                                <td style="width: 40%; font-weight: bold;">
                                    Nova Senha</td>
                                <td style="width: 60%;">
                                    <asp:TextBox ID="tbxNovaSenha" runat="server" CssClass="formNovo" Font-Size="12px"
                                        Height="18px" Text="" TextMode="Password" Width="180px" TabIndex="2" onkeydown="KeyDownHandlerNovaSenha();"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rqvNovaSenha" runat="server" ControlToValidate="tbxNovaSenha"
                                        CssClass="asterico" ErrorMessage="Informe a Nova Senha" ValidationGroup="ValidaDadosNovaSenha">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 40%; font-weight: bold;">
                                    Confirmar Senha</td>
                                <td style="width: 60%;">
                                    <asp:TextBox ID="tbxConfSenha" runat="server" CssClass="formNovo" Font-Size="12px"
                                        Height="18px" Text="" TextMode="Password" Width="180px" TabIndex="3" onkeydown="KeyDownHandlerNovaSenha();"></asp:TextBox>
                                    <asp:CustomValidator ID="ctvConfSenha" ErrorMessage="xxx" runat="server" CssClass="asterisco"
                                        ValidationGroup="ValidaDadosNovaSenha" ClientValidationFunction="ValidaNovaSenha">*</asp:CustomValidator>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 100%; height: 10%;">
                        <asp:Button ID="CancelButton" runat="server" Text="Voltar" CssClass="botaoNovo" Width="100px"
                            TabIndex="5" />
                    </td>
                    <td style="text-align: right; width: 100%; height: 10%;">
                        <asp:UpdatePanel ID="updNovaSenha" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:HiddenField ID="hdnNovaSenha" runat="server" />
                                <asp:Button ID="btnGrava" runat="server" Text="Gravar" CssClass="botaoNovo" Width="100px"
                                    ValidationGroup="ValidaDadosNovaSenha" OnClick="btnGrava_Click" TabIndex="4" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <asp:ValidationSummary id="vlsSumarioLogin" runat="server"  ValidationGroup="ValidaDadosLogin" ShowSummary="false" ShowMessageBox="true"></asp:ValidationSummary>
        <asp:ValidationSummary ID="vlsSumarioNovaSenha" runat="server" 
            CssClass="asterisco" ShowMessageBox="true" ShowSummary="false" 
            ValidationGroup="ValidaDadosNovaSenha" />
 </asp:Content>
