<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Fornecedor.aspx.cs" Inherits="fornecedor" Title="Untitled Page" %>
<%@ Register assembly="RDC.Tools" namespace="RDC.Tools" tagprefix="cc1" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="server">
    <table style="width:100%;">
        <tr>
            <td style="width: 28px">
                <asp:Image ID="ImgCadastro" runat="server" ImageUrl="~/Imagens/layout.png" 
                    Height="16px" />
            </td>
            <td>
                Cadastro de Fornecedor</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <table style="width: 101%;">
        <tr>
            <td style="width: 156px">
                &nbsp;</td>
            <td style="width: 154px">
                &nbsp;</td>
            <td style="width: 105px">
                &nbsp;</td>
            <td style="width: 98px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 156px">
                                        <asp:RadioButton ID="RdBcnpj" runat="server" Text="C.N.P.J" />
                                    </td>
                                    <td style="width: 154px">
                                        <asp:RadioButton ID="RdBnomefantasiaforn" runat="server" Text="Nome Fantasia" />
                                    </td>
                                    <td style="width: 105px">
                                        <asp:RadioButton ID="RdBrazosocialforn" runat="server"                                              Text="Razão Social" />
                                    </td>
                                    <td style="width: 98px">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:TextBox ID="Txtpesquisar" runat="server" Width="436px"></asp:TextBox>
                                    </td>
                                    <td style="width: 98px">
                                        <asp:Button ID="Btnpesquisar" runat="server" Text="Pesquisar" 
                                            CssClass="botao" />
                                    </td>
                                    <td>
                                        <asp:Button ID="Btnincluirnovo" runat="server" Text="Incluir Novo" 
                                            CssClass="botao" />
                                    </td>
                                </tr>
                            </table>
                            <table style="width:100%; height: 845px;">
                                <tr>
                                    <td style="width: 691px">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 691px">
    <table style="width:100%;">
        <tr>
            <td>
                <div id="divListaResultado" runat="server" 
                    style="overflow: auto; display: block; width:95%; text-align:left; height:400px;" visible=false>
                    <cc1:RDCGrid id="grdListaResultadoforn" runat="server" autogeneratecolumns="False" 
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
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 691px">
                                        <br />
                                       Dados Cadastrais:<asp:Panel ID="panelIncluirforn" runat="server" Height="337px" 
                                            Width="696px">
                                            <table style="width:100%;">
                                                <tr>
                                                    <td style="width: 95px">
                                                        Razão Social :</td>
                                                    <td style="width: 103px">
                                                        <asp:TextBox ID="txtrazaosocialforn" runat="server" Width="205px"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 92px" colspan="2">
                                                        Nome Fantasia:</td>
                                                    <td style="width: 151px">
                                                        <asp:TextBox ID="txtnomefantasiaforn" runat="server" Width="205px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 95px">
                                                        C.N.P.J:</td>
                                                    <td style="width: 103px">
                                                        <asp:TextBox ID="txtcnpj" runat="server" Width="205px"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 92px">
                                                        Inscrição Estadual:</td>
                                                    <td colspan="2">
                                                        <asp:TextBox ID="txtinscricaoestadualforn" runat="server" Width="205px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 95px">
                                                        Endereço:</td>
                                                    <td style="width: 103px">
                                                        <asp:TextBox ID="txtenderecoforn" runat="server" Width="205px"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 92px">
                                                        Estado:</td>
                                                    <td colspan="2">
                                                        <asp:TextBox ID="TextBox1" runat="server" Width="205px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 95px">
                                                        Nº</td>
                                                    <td style="width: 103px">
                                                        <asp:TextBox ID="txtnforn" runat="server" Height="17px" Width="205px"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 92px">
                                                        Complemento:</td>
                                                    <td colspan="2">
                                                        <asp:TextBox ID="txtcomplementoforn" runat="server" Width="205px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 95px">
                                                        Bairro:</td>
                                                    <td style="width: 103px">
                                                        <asp:TextBox ID="txtbairroforn" runat="server" Width="205px"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 92px" colspan="2">
                                                        Município:</td>
                                                    <td style="width: 151px">
                                                        <asp:TextBox ID="txtmunicipioforn" runat="server" Width="205px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 95px">
                                                        UF:</td>
                                                    <td style="width: 103px">
                                                        <asp:TextBox ID="Txtufforn" runat="server" Width="205px"></asp:TextBox>
                                                    </td>
                                                    <td colspan="2" style="width: 92px">
                                                        Cep:</td>
                                                    <td style="width: 151px">
                                                        <asp:TextBox ID="txtcepforn" runat="server" Width="205px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 95px">
                                                        &nbsp;</td>
                                                    <td style="width: 103px">
                                                        &nbsp;</td>
                                                    <td colspan="2" style="width: 92px">
                                                        &nbsp;</td>
                                                    <td style="width: 151px">
                                                        &nbsp;</td>
                                                </tr>
                                            </table>
                                            <table style="width:100%;">
                                                <tr>
                                                    <td style="width: 84px">
                                                        &nbsp;</td>
                                                    <td style="width: 170px">
                                                        &nbsp;</td>
                                                    <td style="width: 119px">
                                                        &nbsp;</td>
                                                    <td style="width: 94px">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 84px">
                                                        Contatos:</td>
                                                    <td style="width: 170px">
                                                        &nbsp;</td>
                                                    <td style="width: 119px">
                                                        &nbsp;</td>
                                                    <td style="width: 94px">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 95px">
                                                        Telefone:</td>
                                                    <td style="width: 170px">
                                                        <asp:TextBox ID="txttelefoneforn" runat="server" Width="205px"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 119px">
                                                        Fax:</td>
                                                    <td style="width: 94px">
                                                        <asp:TextBox ID="txtfaxforn" runat="server" Width="205px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 95px">
                                                        Celular:</td>
                                                    <td style="width: 170px">
                                                        <asp:TextBox ID="txtcelularforn" runat="server" Width="205px"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 92px">
                                                        Contato:</td>
                                                    <td style="width: 94px">
                                                        <asp:TextBox ID="txtcontatoforn" runat="server" Width="205px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 95px">
                                                        Email:</td>
                                                    <td style="width: 170px">
                                                        <asp:TextBox ID="txtemailforn" runat="server" Width="205px"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 92px">
                                                        Site:</td>
                                                    <td style="width: 94px">
                                                        <asp:TextBox ID="txtsiteforn" runat="server" Height="16px" Width="205px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 95px">
                                                        Vendedor:</td>
                                                    <td style="width: 170px">
                                                        <asp:TextBox ID="txtvendedor" runat="server" Width="205px"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 92px">
                                                        Departamento:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtdepartamentoforn" runat="server" Width="205px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 95px">
                                                        &nbsp;</td>
                                                    <td style="width: 170px">
                                                        &nbsp;</td>
                                                    <td style="width: 92px">
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;</td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                            </asp:Content>
