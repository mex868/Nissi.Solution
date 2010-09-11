<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CadastroTransportadora.ascx.cs" Inherits="Nissi.WebPresentation.UserControl.CadastroTransportadora" %>
<%@ Register assembly="RDC.Tools" namespace="RDC.Tools" tagprefix="cc1" %>
<%@ Register src="Endereco.ascx" tagname="Endereco" tagprefix="uc1" %>
<link href="../App_Themes/Theme1/Model1.css" type="text/css"  rel="Stylesheet" />
<script language="javascript" type="text/javascript">


</script>
<div style="text-align:center">
    <table width="100%" align="center" class="fundoTabela" >
        <tr>
            <td>
                <table class="fundoTabela" style="text-align:left" cellpadding="3" cellspacing="0">
                    <tr>
                        <td><b>::Dados cadastrais:</b></td>
                    </tr>
                    <tr>
                        <td style="padding-left:17px">Razão Social:</td>
                        <td style="padding-left:20px">Nome Fantasia:</td>
                    </tr>
                    <tr>
                        <td><asp:TextBox runat="server" TabIndex="1" ID="txtRazaoSocial" MaxLength="50" Width="300px" CssClass="formNovo"></asp:TextBox></td>
                        <td><asp:TextBox runat="server" TabIndex="2" ID="txtNomeFantasia"  Width="300px" MaxLength="50" CssClass="formNovo"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="padding-left:17px">CNPJ:</td>
                        <td style="padding-left:20px">Inscrição Estadual:</td>
                    </tr>
                    <tr>
                        <td><asp:TextBox runat="server" TabIndex="3" MaxLength="14" ID="txtCnpj" CssClass="formNovo"></asp:TextBox></td>
                        <td><asp:TextBox runat="server" TabIndex="4" MaxLength="20" ID="txtInscEst" CssClass="formNovo"></asp:TextBox></td>
                    </tr>
                </table>
                <br style="line-height:5px"/>
                <table width="100%" style="text-align:left">
                    <tr>
                       <td>
                           <uc1:Endereco ID="Endereco1" runat="server" />
                       </td>
                    </tr>
                </table>
               <br style="line-height:10px"/>
                <table class="fundoTabela" width="100%" style="text-align:left" cellpadding="3" cellspacing="0">
                    <tr>
                        <td><b>::Contatos:</b></td>
                    </tr>   
                        <tr>
                            <td style="padding-left:17px">Telefone:</td>
                            <td style="padding-left:20px">Fax:</td>
                            <td style="padding-left:20px">Celular:</td>
                            <td style="padding-left:20px">Contato:</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <asp:TextBox CssClass="formNovo" ID="txtTelefone" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                                    ControlToValidate="txtTelefone" ErrorMessage="Telefone não informado!" 
                                    ValidationGroup="Inserir">*</asp:RequiredFieldValidator>
                            </td>
                            <td valign="top">
                                <asp:TextBox CssClass="formNovo" ID="txtFax" runat="server"></asp:TextBox>
                            </td>
                            <td valign="top">
                                <asp:TextBox CssClass="formNovo" ID="txtCelular" runat="server"></asp:TextBox>
                            </td>
                            <td valign="top">
                                <asp:TextBox CssClass="formNovo" ID="txtContato" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" 
                                    ControlToValidate="txtContato" ErrorMessage="Contato não informado!" 
                                    ValidationGroup="Inserir">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-left:17px">Email:</td>
                            <td style="padding-left:20px">Site:</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <asp:TextBox CssClass="formNovo" ID="txtEmail" runat="server"></asp:TextBox>
                            </td>
                            <td valign="top">
                                <asp:TextBox CssClass="formNovo" ID="txtSite" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-left:17px">Observação:</td>
                        </tr>
                        <tr>
                            <td valign="top" colspan="3" style="padding-left:17px">
                                <asp:TextBox ID="txtObservacao" runat="server" Height="50px" 
                                TextMode="MultiLine" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                  </table>
               </td>
            </tr>
        </table>
        <br style="line-height:10px" />
        <table width="95%" style="text-align:left" cellpadding="3" cellspacing="0">
        <tr>
            <td><b>::Transportadoras Cadastradas:</b></td>
        </tr>
       <tr>
            <td>
                 <div id="divListaResultado" runat="server" style="overflow: auto; display: block; text-align:center; height:400px;">
                     <cc1:RDCGrid id="grdListaResultado" runat="server" autogeneratecolumns="False" 
                        bordercolor="Black" borderwidth="1px" cellpadding="1" cellspacing="3" 
                        gridlines="None" pagesize="15" 
                        showpagedetails="True" AllowPaging="True" 
                        MultiSelection="False" ShowHeaderCheckBoxColumn="False" 
                        ShowOptionColumn="False" CssClass="alinhamento" 
                        onpageindexchanging="grdListaResultado_PageIndexChanging" 
                        onrowcommand="grdListaResultado_RowCommand" 
                        onrowdatabound="grdListaResultado_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="Ações">
                                <itemtemplate>
                                    <asp:ImageButton ID="imgEditar" runat="server" ImageUrl="~/Imagens/editar.png" />
                                    <asp:ImageButton ID="imgExcluir" runat="server" ImageUrl="~/Imagens/exclusao_Canc.png" />
                                 </itemtemplate>
                                <HeaderStyle CssClass="headerGrid" />
                                <ItemStyle HorizontalAlign="Left" Width="10%"/>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Razão social">
                                <itemtemplate>
                                    <asp:HyperLink CssClass="hiperlink" id="lnkRazao" Runat="server"  style="cursor:hand; underline:true">&gt;</asp:HyperLink>
                                 </itemtemplate>
                                 <headerstyle wrap="false" CssClass="headerGrid"></headerstyle>
                                 <itemstyle wrap="false"></itemstyle>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Nome Fantasia" >
                                <HeaderStyle CssClass="headerGrid"/>
                                <ItemStyle HorizontalAlign="Left" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="CNPJ" >
                                <HeaderStyle CssClass="headerGrid" />
                                <ItemStyle HorizontalAlign="Left" Width="10%"  />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Insc. Estadual" >
                                 <HeaderStyle CssClass="headerGrid" />
                                <ItemStyle HorizontalAlign="Left" Width="20%"  />
                            </asp:BoundField>
                        </Columns>
                    </cc1:RDCGrid>
                </div>
            </td>
       </tr>
   </table>
</div>

