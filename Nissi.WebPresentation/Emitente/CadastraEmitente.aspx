<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/MasterPage.Master" EnableEventValidation="false" CodeBehind="CadastraEmitente.aspx.cs" Inherits="CadastraEmitente" %>
<%@ Register assembly="RDC.Tools" namespace="RDC.Tools" tagprefix="cc1" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register src="~/UserControl/Endereco.ascx" tagname="Endereco" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="server">
    <link href="../App_Themes/Theme1/Model1.css" type="text/css"  rel="Stylesheet" />
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

   //--------------------------------------------------------------------------------
   //Criado por...: Jacqueline Albuquerque - 07/12/2009
   //Objetivo.....: Valida os campos do cadastro
   //--------------------------------------------------------------------------------
   function ValidaCamposCadastro(src,args) {
       var valido = true;
       if ($get('<%=txtCNPJ.ClientID%>').value == "" || !ValidarCNPJ($get('<%=txtCNPJ.ClientID%>')))
           valido = false;

       args.IsValid = valido;
   }

   function ValidaArquivoImagem(source, args) {
       if ($get('<%=upFileUp.ClientID %>').value == '') {
           alert('Informe um arquivo de imagem válido');
           return false;
       }
       else
           return true;
   }
</script>
<table style="margin-left: auto; width:95%; margin-right: auto;">
    <tr>
        <td style="width:28px">
            <asp:Image ID="ImgCadastro" runat="server" ImageUrl="~/Imagens/layout.png" Height="16px" />
        </td>
        <td class="titulo">Cadastro de Emitente</td>
    </tr>
</table>
<br />
<div style="text-align:center; width:100%;" id="divConsulta" runat="server">
    <div class="fundoTabela" style="width:95%" >
    <table id="tblConsulta" runat="server" style="text-align:left; width:95%">
        <tr>
            <td style="text-align:left">
                <asp:Button ID="btnVoltar" runat="server" OnClick="btnVoltar_Click" 
                    CssClass="botao" Text="Voltar" Width="100px" UseSubmitBehavior="False" />
            </td>
            <td style="text-align:right">
                <asp:UpdatePanel ID="updIncluir" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                            &nbsp;<asp:Button ID="btnIncluir" runat="server" CssClass="botao" Width="100px"
                            Text="Incluir Novo"  onclick="btnIncluir_Click" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    </div>
    <br style="line-height:10px" />
    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="updGrid">
        <ContentTemplate>
                        <div id="divListaResultado" runat="server" style="overflow: auto; display: block; text-align:center; height:400px;">
                            <cc1:RDCGrid 
                                id="grdListaResultado" 
                                runat="server" 
                                autogeneratecolumns="False" 
                                bordercolor="Black" 
                                borderwidth="1px" 
                                cellpadding="1" 
                                cellspacing="3" 
                                gridlines="None" 
                                pagesize="30" 
                                showpagedetails="True" 
                                AllowPaging="True" 
                                MultiSelection="True" 
                                ShowHeaderCheckBoxColumn="False" 
                                ShowOptionColumn="False" 
                                CssClass="alinhamento" 
                                onpageindexchanging="grdListaResultado_PageIndexChanging" 
                                onrowcommand="grdListaResultado_RowCommand" 
                                onrowdatabound="grdListaResultado_RowDataBound" Width="95%" 
                                EnableModelValidation="True">
                                <Columns>
                                    <asp:TemplateField HeaderText="Ações">
                                        <itemtemplate>
                                            <asp:ImageButton ID="imgEditar" Width="15px" Height="15px" runat="server" ImageUrl="~/Imagens/editar.png" />
                                            <asp:ImageButton ID="imgExcluir" Width="15px" Height="15px" runat="server" ImageUrl="~/Imagens/exclusao_Canc.png" />
                                         </itemtemplate>
                                        <HeaderStyle CssClass="headerGrid" Width="5%" />
                                        <ItemStyle HorizontalAlign="center" Wrap="false"/>
                                    </asp:TemplateField>
                                     <asp:BoundField HeaderText="Razão Social" >
                                         <headerstyle wrap="false" CssClass="headerGrid"></headerstyle>
                                         <itemstyle wrap="false" HorizontalAlign="Left"></itemstyle>
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Nome Fantasia" >
                                        <HeaderStyle Wrap="false" CssClass="headerGrid"/>
                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="CNPJ" >
                                        <HeaderStyle CssClass="headerGrid" />
                                        <ItemStyle HorizontalAlign="Left" Width="15%"  />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Insc. Estadual" >
                                         <HeaderStyle CssClass="headerGrid" />
                                        <ItemStyle HorizontalAlign="Left" Width="20%"  />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Telefone(s)" >
                                         <HeaderStyle CssClass="headerGrid" />
                                        <ItemStyle HorizontalAlign="Left" Width="20%"  />
                                    </asp:BoundField>
                                    <asp:ImageField DataImageUrlField="IMAGE" 
                                        DataImageUrlFormatString="~/GeraImagem.aspx?Variavel_Cache=pdf" 
                                        HeaderText="Teste">
                                    </asp:ImageField>
                                </Columns>
                            </cc1:RDCGrid>
                        </div>
        </ContentTemplate>
        <Triggers>
           <asp:AsyncPostBackTrigger ControlID="btnSalvar" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</div>
<br />
<asp:HiddenField ID="hdfTargetIncluirTrans" runat="server" />  
<ajaxToolkit:ModalPopupExtender ID="mpeTransIncluir" runat="server" PopupControlID="pnlIncluirTrans"
    TargetControlID="hdfTargetIncluirTrans" BehaviorID="mpeTransIncluirID" BackgroundCssClass="modalBackground"
    DropShadow="true" />
    <asp:Panel ID="pnlIncluirTrans" runat="server" Style="background-color:#DDDDDD; border:solid 1px Gray; color:Black">
            <div style="text-align:center; width:900px; height:auto; padding: 5px 5px 5px 5px; background-color:#ffffff;">
                <table width="100%" align="center" class="fundoTabela" >
                    <tr> 
                        <td>
                          <asp:UpdatePanel ID="updDadosCadastrais" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                            <asp:HiddenField ID="hdfTipoAcao" runat="server" />
                            <asp:HiddenField ID="hdfCodEmitente" runat="server" />
                            <asp:HiddenField ID="hdfCodPessoa" runat="server" />    
                              <table class="fundoTabela">
                                <!--TÍTULO DA POPUP-->
                                <tr>
                                    <td class="titulo">
                                        <b>
                                            <% if (hdfTipoAcao.Value == "Incluir") { %>
                                                    ::Inclusão de Emitente
                                            <% } %>
                                            <% else { %>
                                                    ::Alteração de Emitente
                                            <% } %>
                                        </b>
                                    </td>
                                </tr>
                            </table>
                            <br />                          
                            <table cellpadding="3" cellspacing="0" class="fundoTabela" style="text-align:left">
                                    <tr>
                                        <td><b>::Dados cadastrais:
                                            </b></td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left:17px">Razão Social:</td>
                                        <td style="padding-left:20px">Nome Fantasia:</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtRazaoSocial" runat="server" CssClass="formNovo" MaxLength="50" TabIndex="1" Width="300px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ValidationGroup="cadastro" Text="*" runat="server" ControlToValidate="txtRazaoSocial" ID="rfvRazao" ErrorMessage="Razão Social não informada." CssClass="asterisco"></asp:RequiredFieldValidator>
                                            <asp:CustomValidator runat="server" ValidationGroup="cadastro" ID="cvCampos" ClientValidationFunction="ValidaCamposCadastro" Display="None"></asp:CustomValidator>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNomeFantasia" runat="server" CssClass="formNovo" MaxLength="50" TabIndex="2" Width="300px"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" Text="*" ValidationGroup="cadastro" ControlToValidate="txtNomeFantasia" ID="RequiredFieldValidator1" ErrorMessage="Nome Fantasia não informada." CssClass="asterisco"></asp:RequiredFieldValidator>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left:17px">CNPJ:</td>
                                        <td style="padding-left:20px">Inscrição Estadual:</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtCNPJ" MaxLength="18" Width="146px" onkeypress="return digitos(event, this);" onkeyup="Mascara('CNPJ',this,event);" runat="server" CssClass="formNovo" TabIndex="3"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtInscricaoEstadual" runat="server" onkeypress="return digitos(event, this);" CssClass="formNovo" MaxLength="20" TabIndex="4"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ValidationGroup="cadastro" ControlToValidate="txtInscricaoEstadual" ID="RequiredFieldValidator2" ErrorMessage="Inscrição Estadual não informada." Text="*" CssClass="asterisco"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr> 
                                    <tr>
                                        <td style="padding-left:20px">
                                            CNAE Fiscal:</td>
                                        <td style="padding-left:20px">
                                            Telefone:</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtCNAE" runat="server" CssClass="formNovo" MaxLength="7" TabIndex="5">
                                            </asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" Text="*" ValidationGroup="cadastro" 
                                                ControlToValidate="txtCNAE" ID="RequiredFieldValidator4" 
                                                ErrorMessage="CNAE não informada." CssClass="asterisco" Enabled="False"></asp:RequiredFieldValidator></td>
                                        <td>
                                            <asp:TextBox ID="txtTelefone" runat="server" CssClass="formNovo" 
                                                onkeypress="return digitos(event, this);" MaxLength="14" 
                                                onkeyup="Mascara('TEL',this,event);" TabIndex="6" ></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" Text="*" ValidationGroup="cadastro" ControlToValidate="txtTelefone" ID="RequiredFieldValidator3" ErrorMessage="Telefone não informada." CssClass="asterisco"></asp:RequiredFieldValidator>
                                            </td>

                                    </tr>
                                    <tr>
                                        <td style="padding-left:20px">
                                            Fax:</td>
                                        <td style="padding-left:20px">
                                            Email:</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="tbxFax" runat="server" CssClass="formNovo" 
                                                onkeypress="return digitos(event, this);" MaxLength="14" 
                                                onkeyup="Mascara('TEL',this,event);" TabIndex="7" ></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbxEmail" runat="server" CssClass="formNovo" MaxLength="0" 
                                                TabIndex="8" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    </table>
                                </ContentTemplate>
                                 <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnIncluir" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="grdListaResultado" EventName="RowCommand" />
                                </Triggers>     
                            </asp:UpdatePanel>
                            <br style="line-height:5px" />
                            <asp:UpdatePanel ID="updEndereco" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                            <table style="text-align:left" width="100%">
                                <tr>
                                    <td>
                                       <uc2:Endereco ID="Endereco1" runat="server" />

                                    </td>
                                </tr>
                            </table>
                            </ContentTemplate>
                            <Triggers>
                             <asp:AsyncPostBackTrigger ControlID="btnIncluir" EventName="Click" />
                             <asp:AsyncPostBackTrigger ControlID="grdListaResultado" EventName="RowCommand" />
                            </Triggers>
                        </asp:UpdatePanel>
                                 <table cellpadding="3" cellspacing="0" class="fundoTabela" style="text-align:left" width="100%">
                                    <tr>
                                        <td><b>Imagem</b></td>
                                    </tr>
                                    <tr>
		                                <td valign="middle">
		                                    <asp:FileUpload   Width="300px"  runat="server" ID="upFileUp" />
		                                </td>
		                                <td>
                                          <asp:UpdatePanel ID="updImage" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:Image ID="imgImagem" runat="server" AlternateText="(Nenhuma imagem carregada)" BackColor="Transparent" 
                                                    BorderColor="Black" BorderStyle="Solid" CssClass="formNovo" Height="100px" 
                                                    Width="100px" />
                                            </ContentTemplate>
                                            </asp:UpdatePanel>
                                            </td>
                                     </tr>
                                     <tr>
                                            <td>
                                                <asp:UpdatePanel runat="server" ID="upBotoes" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Button ID="btnCarregarImagem" runat="server" CausesValidation="False" 
                                                        CssClass="hiperlink" onclick="btnCarregarImagem_Click" tabIndex="6" 
                                                        Text="Carregar Imagem" Width="136px" />
                                                        &nbsp;&nbsp;
                                                        <asp:LinkButton ID="btnLimparImagem" runat="server" CausesValidation="False" 
                                                        CssClass="hiperlink" style="padding-bottom:2px" Width="120px" 
                                                            onclick="btnLimparImagem_Click">Limpar Imagem</asp:LinkButton>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                    <asp:PostBackTrigger ControlID="btnCarregarImagem" />
                                                    </Triggers>
                                                </asp:UpdatePanel>

                                            </td>                                       
                                     </tr>
                                </table>
                                <asp:UpdatePanel runat="server" id="updBotoes" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table class="fundoTabela">
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnCancelar" runat="server" CssClass="botao" 
                                                        onclick="btnCancelar_Click" Text="Cancelar" Width="80px" 
                                                        UseSubmitBehavior="False" />
                                                </td>
                                                <td style="text-align:right">
                                                    <asp:Button ID="btnSalvar" runat="server" CssClass="botao" onclick="btnSalvar_Click" Text="Salvar" ValidationGroup="cadastro" Width="80px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnIncluir" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="grdListaResultado" EventName="RowCommand" />
                                    </Triggers>
                                </asp:UpdatePanel> 
                            </td>
                        </tr>
                    </table>
                </div>            
        </asp:Panel>
        <asp:HiddenField ID="hdfCadastroPopup" runat="server" />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" HeaderText="Erros encontrados:" ShowSummary="False" ValidationGroup="cadastro" />
        <asp:ValidationSummary ID="vsCep" runat="server" ShowMessageBox="True" HeaderText="Erros encontrados:" ShowSummary="False" ValidationGroup="Localizar" />
        <asp:ValidationSummary ID="vsCadastrarCEP" runat="server" ShowMessageBox="True" HeaderText="Erros encontrados:" ShowSummary="False" ValidationGroup="cadastroCEP" />
    </asp:Content>
