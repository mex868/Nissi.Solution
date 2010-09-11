<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="CadastraTransportadora.aspx.cs" Inherits="CadastraTransportadora" Title="Untitled Page" %>
<%@ Register assembly="RDC.Tools" namespace="RDC.Tools" tagprefix="cc1" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register src="../UserControl/Endereco.ascx" tagname="Endereco" tagprefix="uc2" %>
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
    //Criado por...: Jacqueline Albuquerque - 24/11/2009
    //Objetivo.....: Verifica o tipo de pesquisa e habilita campos e limpa-los
    //--------------------------------------------------------------------------------
    function TipoPesquisa(tvar) {
        $get('<%=txtNomeFantasiaPesq.ClientID%>').value = ''
        $get('<%=txtRazao.ClientID%>').value = '';
        $get('<%=txtCNPJPesq.ClientID%>').value = '';
        $get('divCNPJ').style.display = 'none';
        $get('divRazao').style.display = 'none';
        $get('divNomeFantasia').style.display = 'none';
        switch (tvar) {
            case 1: //Pesquisa por CNPJ
                $get('divCNPJ').style.display = 'block';
                break;
            case 2: //Pesquisa por RG
                $get('divRazao').style.display = 'block';

                break;
            case 3: //Pesquisa por NOME
                $get('divNomeFantasia').style.display = 'block';
                break;
        }
    }

    //--------------------------------------------------------------------------------
    //Criado por...: Jacqueline Albuquerque - 24/11/2009
    //Objetivo.....: Valida os campos
    //--------------------------------------------------------------------------------
    function ValidaCampos() {
        if (($get('<%=rbCNPJ.ClientID%>').checked) && ($get('<%=txtCNPJPesq.ClientID %>').value != '')) {
            return true;
        }
        if (($get('<%=rbRazaoSocial.ClientID%>').checked) && ($get('<%=txtRazao.ClientID%>').value != '')) {
            return true;
        }
        if (($get('<%=rbNomeFantasia.ClientID%>').checked) && ($get('<%=txtNomeFantasiaPesq.ClientID%>').value != '')) {
            return true;
        }
        else {
                if ($get('<%=rbCNPJ.ClientID%>').checked) {
                    alert("Informe o C.N.P.J da Transportadora.");
                }
                else if ($get('<%=rbRazaoSocial.ClientID%>').checked)
                    alert('Informe a Razão Social da Transportadora.');
                else {
                    alert('Informe o Nome Fantasia da Transportadora.');
                }
            return false;
       }
   }
   //--------------------------------------------------------------------------------
   //Criado por...: Jacqueline Albuquerque - 07/12/2009
   //Objetivo.....: Valida os campos do cadastro
   //--------------------------------------------------------------------------------
   function ValidaCamposCadastro() {
       var valido = true;
       var msn = "";
       msn = "Os seguintes erros foram encontrados:"
       if ($get('<%=txtRazaoSocial.ClientID%>').value == "") {
           msn += "\n- Razão Social não informada!";
           valido = false;
       }
       if ($get('<%=txtInscricaoEstadual.ClientID%>').value == "") {
           msn += "\n- Inscrição Estadual não informada!.";
           valido = false;
       }
       if ($get('<%=txtCNPJ.ClientID%>').value == "") {
           valido = false;
           msn += "\n- CNPJ não informado!";
       }
       else {
           valido = ValidarCNPJ($get('<%=txtCNPJ.ClientID%>'));
           if (!valido)
               msn += "\n- CNPJ Inválido!";

       }
       if ($get('<%=txtNomeFantasia.ClientID%>').value == "") {
           valido = false;
           msn += "\n- Nome fantasia não informado!"
       }
       if (!valido)
           alert(msn);
       return valido;
   }
</script>
<table style="margin-left: auto; width: 95%; margin-right: auto;">
    <tr>
        <td style="width: 28px">
            <asp:Image ID="ImgCadastro" runat="server" ImageUrl="~/Imagens/layout.png" Height="16px" />
        </td>
        <td class="titulo">Cadastro de Transportadora</td>
    </tr>
</table>
<br />
<div style="text-align:center; width:100%;" id="divConsulta" runat="server">
    <div class="fundoTabela" style="width:95%" >
    <table id="tblConsulta" runat="server" style="text-align:left; width:100%">
        <tr>
            <td style="width: 20%;padding-left:17px"> Opções de Consulta:</td>
            <td style="width: 20%">
                <asp:RadioButton ID="rbCNPJ" Checked="true" onclick ="TipoPesquisa(1)"  GroupName="filtroPesq" runat="server" Text="C.N.P.J" />
            </td>
            <td style="width: 20%">
                <asp:RadioButton ID="rbRazaoSocial" onclick ="TipoPesquisa(2)" runat="server" Text="Razão Social" GroupName="filtroPesq" />
            </td>
            <td style="width: 20%">
                <asp:RadioButton ID="rbNomeFantasia" onclick ="TipoPesquisa(3)" runat="server" Text="Nome Fantasia " GroupName="filtroPesq" />
            </td>
        </tr>
        <tr>
            <td style="width: 20%">&nbsp;</td>
            <td colspan="3">
                <div id="divCNPJ"  style="display:block">
                    <asp:TextBox ID="txtCNPJPesq" MaxLength="18" onkeypress="return digitos(event, this);" onkeyup="Mascara('CNPJ',this,event);" runat="server" Height="16px" Width="150px"></asp:TextBox>
                    <asp:CustomValidator ClientValidationFunction="ValidaCampos" Text="*" CssClass="asterisco" ValidationGroup="pesquisar" ErrorMessage="Favor informar o C.N.P.J." runat="server" id="efvCNPJ"></asp:CustomValidator>
                </div>
                <div id="divRazao" style="display:none">
                    <asp:TextBox ID="txtRazao" runat="server" Height="16px" Width="600px"></asp:TextBox>
                    <asp:CustomValidator Text="*" CssClass="asterisco" ID="cvRazao" ValidationGroup="pesquisar" ClientValidationFunction="ValidaCampos"  ErrorMessage="Favor informar a Razão Social." runat="server"></asp:CustomValidator>
                </div>
                <div id="divNomeFantasia" style="display:none">
                    <asp:TextBox ID="txtNomeFantasiaPesq" runat="server" Height="16px" Width="600px"></asp:TextBox>
                    <asp:CustomValidator ID="CustomValidator1" ValidationGroup="pesquisar" Text="*" cv="cvNomeFant" CssClass="asterisco" ClientValidationFunction="ValidaCampos"  ErrorMessage="Favor informar o Nome Fantasia." runat="server"></asp:CustomValidator>
                </div>
            </td>
        </tr>
        <tr>
            <td style="text-align:left" colspan="2">
                <asp:Button ID="btnVoltar" runat="server" OnClick="btnVoltar_Click" 
                    CssClass="botao" Text="Voltar" Width="100px" UseSubmitBehavior="False" />
            </td>
            <td colspan="2" style="text-align:right">
                <asp:UpdatePanel ID="updBotoes" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Button ID="btnPesquisar" OnClientClick="return ValidaCampos()" runat="server" ValidationGroup="pesquisar" CssClass="botao"
                            Text="Pesquisar" Width="100px" OnClick="btnPesquisar_Click" />
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
                                pagesize="15" 
                                showpagedetails="True" 
                                AllowPaging="True" 
                                MultiSelection="True" 
                                ShowHeaderCheckBoxColumn="false" 
                                ShowOptionColumn="false" 
                                CssClass="alinhamento" 
                                onpageindexchanging="grdListaResultado_PageIndexChanging" 
                                onrowcommand="grdListaResultado_RowCommand" 
                                onrowdatabound="grdListaResultado_RowDataBound">
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
                                </Columns>
                            </cc1:RDCGrid>
                        </div>
        </ContentTemplate>
        <Triggers>
           <asp:AsyncPostBackTrigger ControlID="btnPesquisar" EventName="Click" />
           <asp:AsyncPostBackTrigger ControlID="btnSalvar" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</div>
<br />
<ajaxToolkit:ModalPopupExtender ID="mpeTransIncluir" runat="server" PopupControlID="pnlIncluirTrans"
    TargetControlID="hdfTargetIncluirTrans" BehaviorID="mpeTransIncluirID" BackgroundCssClass="modalBackground"
    DropShadow="true" />    
<asp:Panel ID="pnlIncluirTrans" runat="server">
    <asp:UpdatePanel  runat="server" ID="upCadastro" UpdateMode="Conditional">
        <ContentTemplate>
            <div style="text-align: center;width:900px; height:auto; padding: 5px 5px 5px 5px; background-color: #ffffff;">
                <table class="fundoTabela">
                    <!--TÍTULO DA POPUP-->
                    <tr>
                        <td class="titulo">
                            <b>
                                <% if (hdfTipoAcao.Value == "Incluir") { %>
                                        ::Inclusão de Transportadora
                                <% } %>
                                <% else { %>
                                        ::Alteração de Transportadora
                                <% } %>
                            </b>
                        </td>
                    </tr>
                </table>
                <br />
                <table width="100%" align="center" class="fundoTabela" >
                    <tr> 
                        <td>
                            <table cellpadding="3" cellspacing="0" class="fundoTabela" style="text-align:left">
                                <tr>
                                    <td><b>::Dados cadastrais:</b></td>
                                </tr>
                                <tr>
                                    <td style="padding-left:17px">Razão Social:</td>
                                    <td style="padding-left:20px">Nome Fantasia:</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtRazaoSocial" runat="server" CssClass="formNovo" MaxLength="50" TabIndex="1" Width="300px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNomeFantasia" runat="server" CssClass="formNovo" MaxLength="50" TabIndex="2" Width="300px"></asp:TextBox>

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
                                        <asp:TextBox ID="txtInscricaoEstadual" runat="server" CssClass="formNovo" onkeypress="return digitos(event, this);" MaxLength="20" TabIndex="4"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <br style="line-height:5px" />
                            <table style="text-align:left" width="100%">
                                <tr>
                                    <td>
                                        <asp:UpdatePanel runat="server" ID="updEndereco" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <uc2:Endereco ID="Endereco1" runat="server" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                            <br style="line-height:10px" />
                            <table cellpadding="3" cellspacing="0" class="fundoTabela" style="text-align:left" width="100%">
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
                                        <asp:TextBox ID="txtTelefone" runat="server" onkeypress="return digitos(event, this);" MaxLength="14" onkeyup="Mascara('TEL',this,event);" CssClass="formNovo"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                                            ControlToValidate="txtTelefone" ErrorMessage="Telefone não informado!" 
                                            ValidationGroup="cadastro">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td valign="top">
                                        <asp:TextBox ID="txtFax" runat="server" onkeypress="return digitos(event, this);" MaxLength="14" onkeyup="Mascara('TEL',this,event);" CssClass="formNovo"></asp:TextBox>
                                    </td>
                                    <td valign="top">
                                        <asp:TextBox ID="txtCelular" runat="server" onkeypress="return digitos(event, this);" MaxLength="14" onkeyup="Mascara('TEL',this,event);" CssClass="formNovo"></asp:TextBox>
                                    </td>
                                    <td valign="top">
                                        <asp:TextBox ID="txtContato" runat="server" CssClass="formNovo"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" 
                                            ControlToValidate="txtContato" ErrorMessage="Contato não informado!" 
                                            ValidationGroup="cadastro">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left:17px">Email:</td>
                                    <td style="padding-left:20px">Site:</td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="formNovo"></asp:TextBox>
                                    </td>
                                    <td valign="top">
                                        <asp:TextBox ID="txtSite" runat="server" CssClass="formNovo"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left:17px">Observação:</td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="padding-left:17px" valign="top">
                                        <asp:TextBox ID="txtObservacao" runat="server" Height="50px" 
                                            TextMode="MultiLine" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <asp:UpdatePanel ID="upBotoesCadastro" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table class="fundoTabela">
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnCancelar" runat="server" CssClass="botao" 
                                                    onclick="btnCancelar_Click" Text="Cancelar" Width="80px" 
                                                    UseSubmitBehavior="False" />
                                            </td>
                                            <td style="text-align:right">
                                                <asp:Button ID="btnSalvar" OnClientClick="return ValidaCamposCadastro();" runat="server" CssClass="botao" 
                                                onclick="btnSalvar_Click" Text="Salvar" ValidationGroup="Cadastro" Width="80px" />
                                           </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <asp:HiddenField ID="hdfCodTransportadora" runat="server" />
                    <asp:HiddenField ID="hdfCodPessoa" runat="server" />
                </table>
            </div>
            <asp:HiddenField ID="hdfTipoAcao" runat="server" />
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnIncluir" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="grdListaResultado" 
                EventName="RowCommand" />
            <asp:AsyncPostBackTrigger ControlID="btnSalvar" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Panel>
<asp:HiddenField ID="hdfCadastroPopup" runat="server" />
<asp:HiddenField ID="hdfTargetIncluirTrans" runat="server" />    
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="cadastro" />
</asp:Content>
