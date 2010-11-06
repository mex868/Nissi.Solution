<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="CadastraFornecedor.aspx.cs" Inherits="CadastraFornecedor" Title="Untitled Page" %>
<%@ Register assembly="RDC.Tools" namespace="RDC.Tools" tagprefix="cc1" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register src="../UserControl/Endereco.ascx" tagname="Endereco" tagprefix="uc2" %>
<%@ Register src="../UserControl/Banco.ascx" tagname="Banco" tagprefix="uc1" %>
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
                    alert("Informe o C.N.P.J da Fornecedor.");
                }
                else if ($get('<%=rbRazaoSocial.ClientID%>').checked)
                    alert('Informe a Razão Social da Fornecedor.');
                else {
                    alert('Informe o Nome Fantasia da Fornecedor.');
                }
            return false;
       }
   }
   //--------------------------------------------------------------------------------
   //Criado por...: Jacqueline Albuquerque - 07/12/2009
   //Objetivo.....: Valida os campos do cadastro
   //--------------------------------------------------------------------------------
   function ValidaCampoCNPJ(src, args) {
       var cvCNPJ = $get('<%=cvCNPJ.ClientID %>');
       var valido = true;
       if ($get('<%=txtCNPJ.ClientID%>').value == "") {
           args.IsValid = false;
           cvCNPJ.errormessage = "CNPJ não informado!";
       }
       else {
           valido = ValidarCNPJ($get('<%=txtCNPJ.ClientID%>'));
           if (!valido)
               cvCNPJ.errormessage = "CNPJ Inválido!";
           args.IsValid = valido;
       }
   }
   //--------------------------------------------------------------------------------
   //Criado por...:Alexandre Maximiano - 04/11/2010
   //Objetivo.....: Efetua consulta com o retorno do autocomplete
   //--------------------------------------------------------------------------------
   function CarregarValores(source, eventArgs) {
       $get('<%=hdfIdRazaoSocial.ClientID%>').value = eventArgs.get_value();
       $get('<%=btnPesquisar.ClientID%>').click();
   }
</script>
<table style="margin-left: auto; width: 95%; margin-right: auto;">
    <tr>
        <td style="width: 28px">
            <asp:Image ID="ImgCadastro" runat="server" ImageUrl="~/Imagens/layout.png" Height="16px" />
        </td>
        <td class="titulo">Cadastro de Fornecedor</td>
    </tr>
</table>
<br style="line-height:5px" />
<div style="text-align:center; width:100%;" id="divConsulta" runat="server">
    <div class="fundoTabela" style="width:95%" >
    <table id="tblConsulta" runat="server" style="text-align:left; width:100%">
        <tr>
            <td style="width: 20%;padding-left:17px"> Opções de Consulta:</td>
            <td style="width: 20%">
                <asp:RadioButton ID="rbCNPJ" Checked="true" onclick ="TipoPesquisa(1)"  
                    GroupName="filtroPesq" runat="server" Text="C.N.P.J" CssClass="noBorder" />
            </td>
            <td style="width: 20%">
                <asp:RadioButton ID="rbRazaoSocial" onclick ="TipoPesquisa(2)" runat="server" 
                    Text="Razão Social" GroupName="filtroPesq" CssClass="noBorder" />
            </td>
            <td style="width: 20%">
                <asp:RadioButton ID="rbNomeFantasia" onclick ="TipoPesquisa(3)" runat="server" 
                    Text="Nome Fantasia " GroupName="filtroPesq" CssClass="noBorder" />
            </td>
        </tr>
        <tr>
            <td style="width: 20%">&nbsp;</td>
            <td colspan="3">
                <div id="divCNPJ"  style="display:block">
                    <asp:TextBox ID="txtCNPJPesq" MaxLength="18" onkeypress="return digitos(event, this);" onkeyup="Mascara('CNPJ',this,event);" runat="server" Height="16px" Width="146px"></asp:TextBox>
                    <asp:CustomValidator ClientValidationFunction="ValidaCampos" Text="*" CssClass="asterisco" ValidationGroup="pesquisar" ErrorMessage="Favor informar o C.N.P.J." runat="server" id="efvCNPJ"></asp:CustomValidator>
                </div>
                <div id="divRazao" style="display:none">
                    <asp:TextBox ID="txtRazao" runat="server" Height="16px" Width="600px"></asp:TextBox>
                    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtRazao"
                            MinimumPrefixLength="1" ServiceMethod="GetFornecedor" CompletionInterval="800" EnableCaching="true"
                            CompletionSetCount="10" OnClientItemSelected="CarregarValores" OnClientPopulated="ClientPopulated">
                            </ajaxToolkit:AutoCompleteExtender>
                    <asp:CustomValidator Text="*" CssClass="asterisco" ID="cvRazao" ValidationGroup="pesquisar" ClientValidationFunction="ValidaCampos"  ErrorMessage="Favor informar a Razão Social." runat="server"></asp:CustomValidator>
                </div>
                <div id="divNomeFantasia" style="display:none">
                    <asp:TextBox ID="txtNomeFantasiaPesq" runat="server" Height="16px" Width="600px"></asp:TextBox>
                    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="txtNomeFantasiaPesq"
                            MinimumPrefixLength="1" ServiceMethod="GetFornecedorFantasia" CompletionInterval="800" EnableCaching="true"
                            CompletionSetCount="10" OnClientItemSelected="CarregarValores" OnClientPopulated="ClientPopulated">
                            </ajaxToolkit:AutoCompleteExtender>
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
                        <asp:HiddenField ID="hdfIdRazaoSocial" runat="server" />
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
    <br style="line-height:5px" />
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
                                onrowdatabound="grdListaResultado_RowDataBound" Width="95%">
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
<br style="line-height:5px" />
<ajaxToolkit:ModalPopupExtender ID="mpeTransIncluir" runat="server" PopupControlID="pnlIncluirForn"
    TargetControlID="hdfTargetIncluirForn" BehaviorID="mpeFornIncluirID" BackgroundCssClass="modalBackground"
    DropShadow="true" />    
<asp:Panel ID="pnlIncluirForn" runat="server">
    <asp:UpdatePanel  runat="server" ID="upCadastro" UpdateMode="Conditional">
        <ContentTemplate>
        <asp:HiddenField runat="server" ID="hdfTipoAcao" />
            <div style="text-align: center;width:900px; height:580px; padding: 5px 5px 5px 5px; background-color: #ffffff; overflow:auto">
                <table class="fundoTabela">
                    <!--TÍTULO DA POPUP-->
                    <tr>
                        <td class="titulo">
                            <b>
                                <% if (hdfTipoAcao.Value == "Incluir") { %>
                                        ::Inclusão de Fornecedor
                                <% } %>
                                <% else { %>
                                        ::Alteração de Fornecedor
                                <% } %>
                            </b>
                        </td>
                    </tr>
                </table>
                <br style="line-height:5px" />
                <table width="100%" align="center" class="fundoTabela" >
                    <tr> 
                        <td>
                            <table cellpadding="3" cellspacing="0" class="fundoTabela" style="text-align:left">
                                <tr>
                                    <td colspan="3"><b>::Dados cadastrais:</b></td>
                                </tr>
                                <tr>
                                    <td style="padding-left:17px">Razão Social:</td>
                                    <td style="padding-left:20px" colspan="2">Nome Fantasia:</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtRazaoSocial" runat="server" CssClass="formNovo" MaxLength="50" TabIndex="1" Width="300px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ControlToValidate="txtRazaoSocial" ErrorMessage="Razão Social não Informada!" ValidationGroup="Inserir" runat="server" Text="*" CssClass="asterisco"></asp:RequiredFieldValidator>
                                    </td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtNomeFantasia" runat="server" CssClass="formNovo" MaxLength="50" TabIndex="2" Width="300px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvNomeFantasia" ErrorMessage="Nome Fantasia não Informada!" ValidationGroup="Inserir" ControlToValidate="txtNomeFantasia" runat="server" Text="*" CssClass="asterisco"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left:17px">CNPJ:</td>
                                    <td style="padding-left:20px">Inscrição Estadual:</td>
                                    <td style="padding-left:17px">Tipo de fornecimento:</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtCNPJ" MaxLength="18" Width="146px" onkeypress="return digitos(event, this);" onkeyup="Mascara('CNPJ',this,event);" runat="server" CssClass="formNovo" TabIndex="3"></asp:TextBox>
                                        <asp:CustomValidator ValidationGroup="Inserir"   runat="server" ID="cvCNPJ"  ClientValidationFunction="ValidaCampoCNPJ" CssClass="asterisco">*</asp:CustomValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtInscricaoEstadual" runat="server" CssClass="formNovo" onkeypress="return digitos(event, this);" MaxLength="20" TabIndex="4"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvInscrEstadual" ValidationGroup="Inserir" ControlToValidate="txtInscricaoEstadual" ErrorMessage="Inscrição Estadual não informada!" runat="server" Text="*" CssClass="asterisco"></asp:RequiredFieldValidator>
                                    </td>
                                    <td   valign="top">
                                        <asp:DropDownList ID="ddlTipoFornecimento" runat="server" CssClass="formNovo"></asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                            <br style="line-height:5px" />
                            <table style="text-align:left" width="100%">
                                <tr>
                                    <td>
                                        <uc2:Endereco ID="Endereco1" runat="server" />
                                    </td>
                                </tr>
                            </table>
                            <br style="line-height:5px" />
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
                                        <asp:RequiredFieldValidator ID="rfvTelefone" runat="server" 
                                            ControlToValidate="txtTelefone" ErrorMessage="Telefone não informado!" 
                                            ValidationGroup="Inserir">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td valign="top">
                                        <asp:TextBox ID="txtFax" runat="server" onkeypress="return digitos(event, this);" MaxLength="14" onkeyup="Mascara('TEL',this,event);" CssClass="formNovo"></asp:TextBox>
                                    </td>
                                    <td valign="top">
                                        <asp:TextBox ID="txtCelular" runat="server" onkeypress="return digitos(event, this);" MaxLength="14" onkeyup="Mascara('TEL',this,event);" CssClass="formNovo"></asp:TextBox>
                                    </td>
                                    <td valign="top">
                                        <asp:TextBox ID="txtContato" runat="server" CssClass="formNovo"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvContato" runat="server" 
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
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="formNovo"></asp:TextBox>
                                    </td>
                                    <td valign="top">
                                        <asp:TextBox ID="txtSite" runat="server" CssClass="formNovo"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left:20px">Observação:</td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="padding-left:17px" valign="top">
                                        <asp:TextBox ID="txtObservacao" runat="server" Height="30px" 
                                            TextMode="MultiLine" Width="250px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <br style="line-height:5px" />
                            <table width="100%">
                                <tr>
                                    <td>
                                        <uc1:Banco ID="ucBanco" runat="server" />
                                    </td>
                                </tr>
                            </table>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnCancelar" runat="server" CssClass="botao" 
                                            onclick="btnCancelar_Click" Text="Cancelar" Width="80px" />
                                    </td>
                                    <td style="text-align:right">
                                        <asp:Button ID="btnSalvar" runat="server" CssClass="botao" 
                                        onclick="btnSalvar_Click" Text="Salvar" ValidationGroup="Inserir" Width="80px" />
                                   </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <asp:HiddenField ID="hdfCodFornecedor" runat="server" />
                    <asp:HiddenField ID="hdfCodPessoa" runat="server" />
                </table>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnIncluir" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="grdListaResultado" 
                EventName="RowCommand" />
            <asp:AsyncPostBackTrigger ControlID="btnSalvar" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Panel>
<asp:HiddenField ID="hdfTargetIncluirForn" runat="server" />    
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" HeaderText="Os seguintes erros foram encontrados:" ShowSummary="False" ValidationGroup="Inserir" />
</asp:Content>
