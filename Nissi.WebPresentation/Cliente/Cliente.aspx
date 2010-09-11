<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Cliente.aspx.cs" Inherits="NissiDesigner.Cliente.Cliente" Title="Untitled Page" %>
<%@ Register Assembly="RDC.Tools" Namespace="RDC.Tools" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="server">
    <div style="text-align:center">
    <table border="0" style="margin-left: auto; width: 95%; margin-right: auto">
        <tr>
            <td style="width: 21px">
                <asp:Image ID="ImgCadastro" runat="server" ImageUrl="~/Imagens/layout.png" />
            </td>
            <td>
                <b>Cadastro de Cliente</b></td>
        </tr>
        </table>
    <br />
    <table border="0" style="margin-left: auto; width: 95%; margin-right: auto">
        <tr>
            <td style="width: 234px">
                &nbsp;</td>
            <td style="width: 307px">
                &nbsp;</td>
            <td style="width: 119px">
                &nbsp;</td>
            <td style="width: 315px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 234px">
                <asp:RadioButton ID="RdBCNPJ" runat="server" Text="C.N.P.J" />
            </td>
            <td style="width: 307px">
                <asp:RadioButton ID="RdBrazaosocial" runat="server" Text="Razão Social" />
            </td>
            <td style="width: 119px">
                <asp:RadioButton ID="RdBnomefantasia" runat="server" Text="Nome Fantasia" />
            </td>
            <td style="width: 315px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:TextBox ID="Txtpesquisar" runat="server" Height="16px" Width="667px"></asp:TextBox>
            </td>
            <td style="width: 315px">
                <asp:Button ID="btnPesquisar" runat="server" CssClass="botao" 
                    Text="Pesquisar" Width="106px" />
            </td>
            <td>
                <asp:Button ID="btnIncluir" runat="server" CssClass="botao" 
                    Text="Incluir Novo" />
            </td>
        </tr>
        </table>
    <br />
    <table border="0" style="margin-left: auto; width: 95%; margin-right: auto">
        <tr>
            <td>
                <div id="divListaResultado" runat="server" 
                    style="overflow: auto; display: block; width:95%; text-align:left; height:400px;" visible="false">
                    <cc1:RDCGrid id="grdListaResultado" runat="server" autogeneratecolumns="False" 
                        bordercolor="Black" borderwidth="1px" cellpadding="1" cellspacing="3" 
                        gridlines="None" pagesize="15" 
                        showpagedetails="True" width="97%" AllowPaging="True" 
                        MultiSelection="False" ShowHeaderCheckBoxColumn="False" 
                        ShowOptionColumn="False">
                    <Columns>
                        <asp:TemplateField HeaderText="Ações">
                            <itemtemplate>
                            <asp:ImageButton ID="imgEditar" runat="server" ImageUrl="~/Imagens/editar.png">
                                </asp:ImageButton>
                                    <asp:ImageButton ID="imgExcluir" runat="server" 
                                    ImageUrl="~/Imagens/exclusao_Canc.png">
                                </asp:ImageButton> 
                            </itemtemplate>
                        
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Razão Social" />
                        <asp:BoundField HeaderText="Nome Fantasia" />
                        <asp:BoundField HeaderText="CNPJ" />
                        <asp:BoundField HeaderText="Inscrição Estadual"></asp:BoundField>
                    </Columns>
                    
                </cc1:RDCGrid>
                    <br />
                    <br />
                </div>
            </td>
        </tr>
        </table>
        </div>
    
    <div style="text-align:center;">
    <asp:Panel ID="pnlIncluir" runat="server">
    <table border="0" style="margin-left: auto; width: 95%; margin-right: auto">
        <tr>
            <td style="font-family: Verdana; color: #000000; width: 379px;">
                <b>::Dados cadastrais:</b></td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Panel ID="pnlDados" runat="server" 
                    Style="background-color: #DDDDDD; border: solid 1px Gray; color: Black" 
                    Width="878px">
                    <table style="width:100%;">
                        <tr>
                            <td style="width: 111px">
                                Razão Social:</td>
                            <td style="width: 148px">
                                <asp:TextBox ID="Txtrazaosocial" runat="server" Width="139px"></asp:TextBox>
                            </td>
                            <td style="width: 91px">
                                Nome Fantasia:</td>
                            <td style="width: 116px">
                                <asp:TextBox ID="Txtnomefant" runat="server" Width="120px"></asp:TextBox>
                            </td>
                            <td style="width: 36px">
                                CNPJ:</td>
                            <td style="width: 150px">
                                <asp:TextBox ID="Txtcnpj" runat="server" Width="146px"></asp:TextBox>
                            </td>
                            <td style="width: 111px">
                                Incrição Estadual:</td>
                            <td>
                                <asp:TextBox ID="Txtincricao" runat="server" Width="160px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 111px">
                                Endereço:</td>
                            <td colspan="3">
                                <asp:TextBox ID="TextBox4" runat="server" Width="362px"></asp:TextBox>
                            </td>
                            <td style="width: 36px">
                                Nº</td>
                            <td style="width: 150px">
                                <asp:TextBox ID="Txtnumero" runat="server" Width="145px"></asp:TextBox>
                            </td>
                            <td style="width: 111px">
                                Complemento:</td>
                            <td>
                                <asp:TextBox ID="Txtcomplemento" runat="server" Width="163px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 111px">
                                Bairro:</td>
                            <td style="width: 148px">
                                <asp:TextBox ID="Txtbairro" runat="server" Width="139px"></asp:TextBox>
                            </td>
                            <td style="width: 91px">
                                Município:</td>
                            <td style="width: 116px">
                                <asp:TextBox ID="Txtmunicipio" runat="server" Width="120px"></asp:TextBox>
                            </td>
                            <td style="width: 36px">
                                UF:</td>
                            <td style="width: 150px">
                                <asp:TextBox ID="Txtuf" runat="server" Width="143px" Height="17px"></asp:TextBox>
                            </td>
                            <td style="width: 111px">
                                Cep:</td>
                            <td>
                                <asp:TextBox ID="TextBox9" runat="server" Width="163px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                &nbsp;</td>
        </tr>
        </table>
    <br />
    <table border="0" style="margin-left: auto; width: 95%; margin-right: auto">
        <tr>
            <td style="font-family: Verdana; color: #000000; width: 334px;">
                <b>::Contatos:</b></td>
            <td style="font-family: Verdana; color: #000000">
                &nbsp;</td>
            <td style="font-family: Verdana; color: #000000">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Panel ID="pnlContato" runat="server" 
                    Style="background-color: #DDDDDD; border: solid 1px Gray; color: Black" 
                    Width="872px">
                    <table style="width:100%; margin-right: 0px;">
                        <tr>
                            <td style="width: 88px">
                                Telefone:</td>
                            <td style="width: 154px">
                                <asp:TextBox ID="TxtTelefone" runat="server"></asp:TextBox>
                            </td>
                            <td style="width: 52px">
                                &nbsp;Fax:</td>
                            <td style="width: 115px">
                                <asp:TextBox ID="Txtfax" runat="server" Width="117px"></asp:TextBox>
                            </td>
                            <td style="width: 36px">
                                Celular:</td>
                            <td style="width: 150px">
                                <asp:TextBox ID="Txtcelular" runat="server" Width="146px"></asp:TextBox>
                            </td>
                            <td style="width: 68px">
                                Contato:</td>
                            <td>
                                <asp:TextBox ID="Txtcontato" runat="server" style="margin-left: 0px" 
                                    Width="177px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table style="width:100%; margin-right: 0px;">
                        <tr>
                            <td style="width: 131px">
                                Email:</td>
                            <td colspan="2" style="width: 31px">
                                <asp:TextBox ID="Txtemail" runat="server" Width="318px"></asp:TextBox>
                            </td>
                            <td colspan="2">
                                Site:</td>
                            <td style="width: 39px">
                                <asp:TextBox ID="Txtsite" runat="server" style="margin-left: 11px" 
                                    Width="239px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 131px">
                                Comprador:</td>
                            <td style="width: 822px">
                                <asp:TextBox ID="Txtcomprador" runat="server" Width="139px"></asp:TextBox>
                            </td>
                            <td colspan="2" style="width: 83px">
                                Vendedor:</td>
                            <td style="width: 14px">
                                &nbsp;</td>
                            <td style="width: 39px">
                                <asp:TextBox ID="Txtvendedor" runat="server" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                &nbsp;</td>
        </tr>
        </table>
    </asp:Panel>
    </div>    
</asp:Content>
