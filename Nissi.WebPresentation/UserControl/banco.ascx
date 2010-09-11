<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Banco.ascx.cs" Inherits="Nissi.WebPresentation.UserControl.Banco" %>
<link href="../App_Themes/Theme1/Model1.css" type="text/css" rel="Stylesheet" />
<table class="fundoTabela" width="100%" style="text-align:left">
    <tr>
        <td colspan="5"><b>::Dados Bancários:</b></td>
    </tr>
    <tr>
        <td style="padding-left:17px">Tipo de Conta:</td>
        <td style="padding-left:20px"> Número:</td>
        <td style="padding-left:20px"> Banco:</td>
        <td style="padding-left:20px">Agência:</td>
        <td style="padding-left:20px">Conta:</td>
    </tr>
    <tr>
        <td>
            <asp:DropDownList runat="server" ID="ddlTipoConta" CssClass="formNovo">
                <asp:ListItem Text=" " Value=" "></asp:ListItem>
                <asp:ListItem Text="Corrente" Value="0"></asp:ListItem>
                <asp:ListItem Text="Poupança" Value="1"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td>
            <asp:DropDownList runat="server" ID="ddlNumBanco" CssClass="formNovo" 
                onselectedindexchanged="ddlNumBanco_SelectedIndexChanged" 
                AutoPostBack="True"></asp:DropDownList>
        </td>
        <td> 
            <asp:DropDownList runat="server" ID="ddlBanco" CssClass="formNovo" 
                AutoPostBack="True" onselectedindexchanged="ddlBanco_SelectedIndexChanged"></asp:DropDownList>
        </td>
        <td><asp:TextBox runat="server" MaxLength="8" Width="100px" id="txtAgencia" CssClass="formNovo"></asp:TextBox></td>
        <td><asp:TextBox runat="server" id="txtConta" Width="100px" MaxLength="10" CssClass="formNovo"></asp:TextBox></td>
    </tr>
</table>
