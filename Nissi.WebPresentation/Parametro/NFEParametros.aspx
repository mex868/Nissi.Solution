<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.Master" CodeBehind="NFEParametros.aspx.cs" Inherits="Nissi.WebPresentation.NFEParametros" %>
<%@ Register assembly="RDC.Tools" namespace="RDC.Tools" tagprefix="cc1" %>
<asp:Content ContentPlaceHolderID="cphPrincipal" runat="server">
<link href="../App_Themes/Theme1/Model1.css" type="text/css"  rel="Stylesheet" />
<script src="../JScripts/Common.js" type="text/javascript"></script>
<script type="text/javascript">
    //--------------------------------------------------------------------------------
    //Criado por...: Alexandre Maximiano - 02/11/2009
    //Objetivo.....: Cabeçalho padrão da página
    //--------------------------------------------------------------------------------
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_initializeRequest(InitializeRequest);
    prm.add_endRequest(EndRequest);
    var postBackElement;
    function InitializeRequest(sender, args) {
        if (prm.get_isInAsyncPostBack())
            args.set_cancel(true);

        if (args.get_postBackElement().type != 'checkbox')
            WaitAsyncPostBack(true);
    }
    function EndRequest(sender, args) {
        WaitAsyncPostBack(false);
    }


    function uploadComplete(sender, args) {
        var contentType = args.get_contentType();
        var text = args.get_length() + " bytes";
        if (contentType.length > 0) {
            text += ", '" + contentType + "'";
        }
        
    } 
    function ValidarCampos(src, args) {
        $get('<%=cvCampos.ClientID%>').errormessage = "";
    
        var valido = true;

        if ($get("<%=ddlAmbiente.ClientID%>").value == 0) {
            $get('<%=cvCampos.ClientID%>').errormessage = "- Ambiente não selecionado\n";
            valido = false;
        }
        if ($get("<%=ddlTipoDanfe.ClientID%>").value == 0) {
            $get('<%=cvCampos.ClientID%>').errormessage += "- Tipo Dante não selecionado\n";
            valido == false;
        }
        if ($get("<%=ddlTrace.ClientID%>").value == -1) {
            $get('<%=cvCampos.ClientID%>').errormessage += "- Trace não selecionado\n";
            valido == false;
        }
        if ($get('<%=fileLogoDanfe.ClientID%>').value == "" && $get('<%=lblLogoDanfe.ClientID%>').value == "") {
            $get('<%=cvCampos.ClientID%>').errormessage += "- Arquivo LogoDanfe não selecionado\n";
            valido = false; 
        }
        if ($get('<%=filePathPrincipal.ClientID %>').value == "" && $get('<%=lblPathPrincipal.ClientID %>').value == "") {
            $get('<%=cvCampos.ClientID%>').errormessage += "- Arquivo PathPrincipal não selecionado\n";
            valido = false;
        }
        if ($get('<%=fileDataPacket.ClientID %>').value == "" && $get('<%=lblDataPacket.ClientID %>').value == "") {
            $get('<%=cvCampos.ClientID%>').errormessage += "- Arquivo DataPacket não selecionado\n";
            valido = false;
        }
        if ($get('<%=fileSchema.ClientID %>').value == "" && $get('<%=lblSchema.ClientID %>').value == "") {
            $get('<%=cvCampos.ClientID%>').errormessage += "- Arquivo Schema não selecionado\n";
            valido = false;
        }
        if ($get('<%=fileDataPacketForm.ClientID %>').value == "" && $get('<%=lblDataPacketForm.ClientID %>').value == "") {
            $get('<%=cvCampos.ClientID%>').errormessage += "Arquivo DataPacketForm não selecionado\n";
            valido = false;
        }
        if ($get('<%=txtModelo.ClientID%>').value == "") {
            $get('<%=cvCampos.ClientID%>').errormessage += "- Modelo não informado.\n";
            valido = false;
        }
        if ($get('<%= txtCertificado.ClientID%>').value == "") {
            $get('<%=cvCampos.ClientID%>').errormessage += "- Certificado não informado\n";
            valido = false;
        }
        if ($get('<%= txtDanfeInfo.ClientID%>').value == "") {
            $get('<%=cvCampos.ClientID%>').errormessage += "- DanfeInfo não informado.\n";
            valido = false;
        }
        if ($get('<%= txtProc.ClientID%>').value == "") {
            $get('<%=cvCampos.ClientID%>').errormessage += "- Proc não informada.\n";
            valido = false;
        }
        

        args.IsValid = valido;
    }

    function onchangeAmbiente() {
        if ($get('<%=ddlAmbiente.ClientID%>').value == "1") {
            $get('<%=txtRecepcao.ClientID%>').value = "https://nfe.fazenda.sp.gov.br/nfeweb/services/nferecepcao2.asmx";
            $get('<%=txtRetRecepcao.ClientID%>').value= "https://nfe.fazenda.sp.gov.br/nfeweb/services/nferetrecepcao2.asmx";
            $get('<%=txtCancelamento.ClientID%>').value="https://nfe.fazenda.sp.gov.br/nfeweb/services/nfecancelamento2.asmx";
            $get('<%=txtInutilizacao.ClientID%>').value="https://nfe.fazenda.sp.gov.br/nfeweb/services/nfeinutilizacao2.asmx";
            $get('<%=txtProtocolo.ClientID%>').value= "https://nfe.fazenda.sp.gov.br/nfeweb/services/nfeconsulta2.asmx";
            $get('<%=txtStatuS.ClientID%>').value="https://nfe.fazenda.sp.gov.br/nfeweb/services/nfestatusservico2.asmx";
        }
        else if ($get('<%=ddlAmbiente.ClientID%>').value == "2") {
            $get('<%=txtRecepcao.ClientID%>').value= "https://homologacao.nfe.fazenda.sp.gov.br/nfeweb/services/nferecepcao2.asmx";
            $get('<%=txtRetRecepcao.ClientID%>').value="https://homologacao.nfe.fazenda.sp.gov.br/nfeweb/services/nferetrecepcao2.asmx";
            $get('<%=txtCancelamento.ClientID%>').value= "https://homologacao.nfe.fazenda.sp.gov.br/nfeweb/services/nfecancelamento2.asmx";
            $get('<%=txtInutilizacao.ClientID%>').value= "https://homologacao.nfe.fazenda.sp.gov.br/nfeweb/services/nfeinutilizacao2.asmx";
            $get('<%=txtProtocolo.ClientID%>').value= "https://homologacao.nfe.fazenda.sp.gov.br/nfeweb/services/nfeconsulta2.asmx";
            $get('<%=txtStatuS.ClientID%>').value="https://homologacao.nfe.fazenda.sp.gov.br/nfeweb/services/nfestatusservico2.asmx";
        }
        else
        {
            $get('<%=txtRecepcao.ClientID%>').value ="";
            $get('<%=txtRetRecepcao.ClientID%>').value="";
            $get('<%=txtCancelamento.ClientID%>').value= "";
            $get('<%=txtInutilizacao.ClientID%>').value= "";
            $get('<%=txtProtocolo.ClientID%>').value= "";
            $get('<%=txtStatuS.ClientID%>').value="";
        
        }
    }
</script>

<table style="margin-left: auto; width: 95%; margin-right: auto;">
    <tr>
        <td style="width: 28px">
            <asp:Image ID="ImgCadastro" runat="server" ImageUrl="~/Imagens/layout.png" Height="16px" />
        </td>
        <td class="titulo">Cadastro de Parâmetro</td>
    </tr>
</table>
<asp:UpdatePanel  runat="server" ID="upCadastro" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:HiddenField runat="server" ID="hdfTipoAcao" />
        <asp:CustomValidator  runat="server" id="cvCampos" Display="None" ClientValidationFunction="ValidarCampos" ValidationGroup="Incluir"></asp:CustomValidator>           
        <asp:ValidationSummary runat="server" ID="vs" HeaderText="Erros Encontrados" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Incluir" />
            <table class="fundoTabela" width="100%">
                <!--TÍTULO DA POPUP-->
                <tr>
                    <td class="titulo">
                        <b>
                            <% if (hdfTipoAcao.Value == "Incluir") { %>
                                ::Inclusão de Parâmetro
                                <% } %>
                                <% else { %>
                                        ::Alteração de Parâmetro
                                <% } %>
                        </b>
                    </td>
                </tr>
            </table>
            <br style="line-height:5px" />
            <table style="text-align:left;height:370px" class="fundoTabela">
                <tr> 
                    <td colspan="3">
                        <fieldset>
                            <legend>Parâmetros</legend>
                                <table width="100%">
                                    <tr>
                                        <td style="padding-left:20px">Modelo</td>
                                        <td style="padding-left:20px">Serie</td>
                                        <td style="padding-left:20px">Ambiente</td>
                                        <td style="padding-left:20px" colspan="2">Tipo Danfe</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtModelo"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="ddlSerie" Width="150px">
                                                <asp:ListItem Value="0"> -- Selecione -- </asp:ListItem>
                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                <asp:ListItem Value="5">5</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="ddlAmbiente" 
                                                    onchange="onchangeAmbiente();" Width="150px">
                                                    <asp:ListItem Value="0"> -- Selecione -- </asp:ListItem>
                                                    <asp:ListItem Value="1">Produção(Oficial)</asp:ListItem>
                                                    <asp:ListItem Value="2">Homologação(Testes)</asp:ListItem>
                                            </asp:DropDownList></td>
                                            <td colspan="2">
                                                <asp:DropDownList ID="ddlTipoDanfe" runat="server" Width="150px">
                                                    <asp:ListItem Value="0"> -- Selecione -- </asp:ListItem>
                                                    <asp:ListItem Value="1">Retrato</asp:ListItem>
                                                    <asp:ListItem Value="2">Paisagem</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                           <td colspan="5">
                                                <table width="100%">
                                                    <tr>
                                                        <td style="padding-left:20px">Path Principal</td>
                                                        <td style="padding-left:20px">Data Packet</td>
                                                        <td style="padding-left: 20px">Logo Danfe</td>
                                                    </tr>
                                                    <tr> 
                                                                                                         
                                                         <td><ajaxToolkit:AsyncFileUpload  UploaderStyle="Traditional"  ID="filePathPrincipal" runat="server" Width="90%" />

                                                         </td>
                                                         
                                                         <td><ajaxToolkit:AsyncFileUpload OnClientUploadComplete="uploadComplete"   UploaderStyle="Traditional" ID="fileDataPacket" runat="server" Width="90%" /></td>
                                                         <td><ajaxToolkit:AsyncFileUpload OnClientUploadComplete="uploadComplete"  UploaderStyle="Traditional"  ID="fileLogoDanfe" runat="server" Width="90%" /></td>                                                    
                                                   </tr>
                                                    <tr>
                                                        <td style="padding-left:20px" width="250px"><asp:Label runat="server" Font-Bold="true" ForeColor="Red" ID="lblPathPrincipal" Width="200px"></asp:Label></td>
                                                        <td style="padding-left:20px" width="250px"><asp:Label runat="server" Font-Bold="true" ForeColor="Red" ID="lblDataPacket" Width="200px"></asp:Label></td>
                                                        <td style="padding-left:20px" width="250px"><asp:Label runat="server" Font-Bold="true" ForeColor="Red" ID="lblLogoDanfe" Width="200px"></asp:Label></td>
                                                    </tr>
                                                </table>
                                           </td>
                                       </tr>
                                        <tr>
                                            <td style="padding-left: 20px" colspan="2">Schema</td>
                                            <td colspan="3" style="padding-left: 20px">Data Packet Form Seg</td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <ajaxToolkit:AsyncFileUpload OnClientUploadComplete="uploadComplete"  UploaderStyle="Traditional" ID="fileSchema" runat="server" Width="300px" />
                                            </td>
                                            <td colspan="3">
                                                <ajaxToolkit:AsyncFileUpload UploaderStyle="Traditional" OnClientUploadComplete="uploadComplete" ID="fileDataPacketForm" runat="server" Width="300px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="padding-left:20px" width="250px">
                                                <asp:Label ID="lblSchema" Font-Bold="true" ForeColor="Red"  runat="server" Width="200px"></asp:Label>
                                            </td>
                                            <td colspan="3" style="padding-left:20px" width="250px">
                                                <asp:Label Font-Bold="true" ForeColor="Red" ID="lblDataPacketForm" runat="server" Width="200px"></asp:Label>
                                            </td>
                                    </tr>
                                        <tr>
                                            <td style="padding-left: 20px">Nº Serie Certificado</td>
                                            <td colspan="1" style="padding-left: 20px">Ativa Trace</td>
                                            <td colspan="1" style="padding-left: 20px">Ver Proc</td>
                                            <td style="padding-left: 20px">Danfe Info</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td><asp:TextBox ID="txtCertificado" runat="server"></asp:TextBox></td>
                                            <td colspan="1">
                                                <asp:DropDownList ID="ddlTrace" runat="server" Width="150px">
                                                    <asp:ListItem Value="-1"> -- Selecione -- </asp:ListItem>
                                                    <asp:ListItem Value="0">0</asp:ListItem>
                                                    <asp:ListItem Value="1">1</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td colspan="1">
                                                <asp:TextBox ID="txtProc" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDanfeInfo" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkTotalCFOP" runat="server" Text="Total CFOP" />
                                            </td>
                                       </tr>
                                   </table>                                                               
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <fieldset>
                                <legend>Web Services</legend>
                                    <table width="100%">
                                        <tr>
                                            <td style="padding-left:20px" width="50%">NFe Recepção</td>
                                            <td style="padding-left:20px" width="50%">NFe Ret Recepção</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtRecepcao" runat="server" Width="400px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtRetRecepcao" runat="server" Width="400px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left:20px">Nfe Cancelamento</td>
                                            <td style="padding-left:20px">NFe Inutilização</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtCancelamento" runat="server" Width="400px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtInutilizacao" runat="server" Width="400px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left:20px">NFe Consulta Protocolo</td>
                                            <td style="padding-left:20px">NFe Status Serviços</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtProtocolo" runat="server" Width="400px" ></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtStatuS" runat="server" Width="400px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>            
                            </fieldset>
                         </td>
                      </tr>
                     <tr>
                        <td align="left">
                            <asp:Button ID="btnCancelar" runat="server" CssClass="botao" Text="Cancelar" 
                                Width="80px" onclick="btnCancelar_Click" />
                        </td>
                         <td align="center">
                            <asp:Button ID="btnExcluir" runat="server" CssClass="botao" Text="Excluir" 
                                 Width="80px" onclick="btnExcluir_Click" Visible="False" />
                        </td>
                        <td style="text-align:right">
                            <asp:Button ID="btnSalvar" runat="server" CssClass="botao" onclick="btnSalvar_Click" Text="Salvar" ValidationGroup="Incluir" Width="80px" />
                        </td>
                    </tr>       
                </table>
                 <asp:HiddenField ID="hdfCodParametro" runat="server" />

        </ContentTemplate>
    </asp:UpdatePanel>




</asp:Content>
