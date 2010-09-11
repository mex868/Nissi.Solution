<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Endereco.ascx.cs" Inherits="Nissi.WebPresentation.UserControl.Endereco" %>
<link href="../App_Themes/Theme1/Model1.css" type="text/css" rel="Stylesheet" />
    <asp:HiddenField id="hdfTargetCep" runat="server"></asp:HiddenField>
            <table class="fundoTabela" width="100%" style="text-align:left">
                <tr>
                    <td><b>::Localização:</b></td>
                    <td colspan="2" rowspan="3">
                        <table width="100%">
                            <tr>
                                <td   runat="server" id="tdLabel" style="display:block; color:Red; width:50%" 
                                    colspan="2" width="50%">
                                    <asp:Label runat="server" ID="lblInfornacao"></asp:Label>
                                </td>
                            </tr>
                                <td style="display:none;padding-left:17px" id="tdTipo" runat="server"> Tipo</td>
                            <tr>
                                <td style="display:none" id="tdDLLTipo" runat="server" colspan="2"><asp:DropDownList runat="server" ID="ddlTipoLogradouro" CssClass="formNovo"></asp:DropDownList></td>
                            </tr>
                        </table>
                        </td>
                </tr>
                <tr>
                    <td style="padding-left:17px">Cep:</td>

              </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtCep" onkeypress="return digitos(event, this);" MaxLength="9" onkeyup="Mascara('CEP',this,event);" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                            ControlToValidate="txtCep" ErrorMessage="CEP não informado!" 
                            ValidationGroup="Inserir">*</asp:RequiredFieldValidator>
                        <asp:LinkButton ID="lkbLocalizar" runat="server" OnClick="lkbLocalizar_Click">Localizar</asp:LinkButton>
                    </td>
                    </tr>
                    <tr>
                        <td style="padding-left:20px">Logradouro:</td>
                        <td colspan="2" rowspan="2">
                            <table width="100%">
                                <tr>
                                    <td style="padding-left:20px">Nº:</td>
                                    <td style="padding-left:20px">Complemento:</td>
                                    <td style="padding-left:17px">Bairro:</td>
                                </tr>
                                <tr>
                             <td >
                            <asp:TextBox Width="50px" ID="txtNumero" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtComplemento" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtBairro" Width="150px" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtBairro" CssClass="asterisco" ErrorMessage="Bairro não informado!" ValidationGroup="InserirCEP" runat="server" Text="*" ID="rfvBairro"></asp:RequiredFieldValidator>                            
                        </td>
                      </tr>
                   </table>
                        </td>

                    </tr>
                    <tr>
                     <td>
                        <asp:TextBox Width="200px" ID="txtEndereco" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator Text="*" ErrorMessage="Logradouro não informado!" ControlToValidate="txtEndereco" ValidationGroup="InserirCEP" runat="server" ID="rfvEndereco"></asp:RequiredFieldValidator>
                     </td>
                    </tr>
                    <tr>
                        <td style="padding-left:20px">UF:</td>
                        <td style="padding-left:20px" colspan="2">Município:</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="ddlUF" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td colspan="1">
                            <asp:DropDownList ID="ddlCidade" Width="250px" runat="server">
                            </asp:DropDownList>
                           <ajaxToolkit:CascadingDropDown
                               ID="cddCidade" 
                                runat="server" 
                                TargetControlID="ddlCidade"
                                ServiceMethod="ListaCidade"
                                ParentControlID="ddlUF" 
                                Category="cidade" 
                                ServicePath="~/CascadingWS.asmx"
                                LoadingText="Carregando Cidade...">
                          </ajaxToolkit:CascadingDropDown>
                        </td>
                        <td style="display:none" runat="server" id="tdBotaoIncluirCEP" colspan="1">
                            <table runat="server" width="100%">
                                <tr>
                                    <td colspan="1"> 
                                        <asp:Button Text="Incluir" runat="server" ID="btnIncluirCep" 
                                            CssClass="botao" ValidationGroup="InserirCEP" onclick="btnIncluirCep_Click" Width="100px" />
                                     </td>
                                    <td><asp:Button Text="Cancelar" runat="server" ID="btnCancelar" CssClass="botao" 
                                            onclick="btnCancelar_Click" Width="100px" /></td>
                                </tr>
                            </table>
                       </td>
                    </tr>
            </table>
            <asp:ValidationSummary runat="server" ID="vsSummary" ShowMessageBox="true" ShowSummary="false" ValidationGroup="InserirCEP" />


