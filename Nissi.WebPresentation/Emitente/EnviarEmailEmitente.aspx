<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="EnviarEmailEmitente.aspx.cs" Inherits="EnviarEmailEmitente" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="server">
    <div style="width:100%; text-align:center">
        <table style="margin-left: auto; width: 95%; margin-right: auto;">
            <tr>
                <td style="width: 21px; text-align:left">
                    <asp:Image ID="ImgCadastro" runat="server" ImageUrl="~/Imagens/layout.png" />
                </td>
                <td style="width: 95%; text-align: left" class="titulo">Enviar Email Para:</td>
            </tr>
        </table>
<br />
<br />
<br />
<br />
<table class="fundoTabela">
<tr>
<td colspan="2">
    <asp:CheckBoxList ID="ckbListEmail" runat="server">
    </asp:CheckBoxList>
    </td>
</tr>
<tr>
<td>
<asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="botao" />
</td>
<td><asp:Button ID="btnEnviar" runat="server" Text="Enviar" CssClass="botao" 
        onclick="btnEnviar_Click" /></td>
</tr>
</table>
</div>
</asp:Content>
