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
    </script>
    <div style="text-align: center; margin-top: 100px;">
    <asp:Panel ID="pnlDiv" runat="server">
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
          <td colspan="2" style="line-height: 5px;">
              &nbsp;</td>
        </tr>
        <tr>
            <td align="right"  style="width: 100px;">
            <b>Usuário:</b>
            </td>
            <td align="left" style="width: 70%;" colspan="">
              <asp:TextBox ID="tbxNomeUsuario" runat="server"  Font-Size="12px"
              Height="18px" Width="180px"></asp:TextBox>
              <asp:RequiredFieldValidator ID="rfvNomeUsuario" runat="server" ControlToValidate="tbxNomeUsuario" ErrorMessage="Informe o Login do Usuário" ValidationGroup="ValidaDadosLogin">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr style="font-weight: bold">
        <td  align="right"  style="width: 100px;">
        <b>Senha:</b>
        </td>
        <td align="left" style="width: 65%;" colspan="">
          <asp:TextBox ID="tbxSenha" runat="server" Font-Size="12px" Height="18px"
          Text="0" TextMode="Password" Width="120px" MaxLength="10"></asp:TextBox>
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
    <asp:ValidationSummary id="vlsSumarioLogin" runat="server"  ValidationGroup="ValidaDadosLogin" ShowSummary="false" ShowMessageBox="true"></asp:ValidationSummary>
    </asp:Panel>
    </div>
    </asp:Content>
