<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" EnableEventValidation="false" AutoEventWireup="true"  CodeBehind="CadastraEmailEmitente.aspx.cs" Inherits="CadastraEmailEmitente" Title="Cadastro de Produto" %>
<%@ Register assembly="RDC.Tools" namespace="RDC.Tools" tagprefix="cc1" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="server">
    <link href="../App_Themes/Theme1/Model1.css" type="text/css"  rel="Stylesheet" />
<script type="text/javascript">
    //--------------------------------------------------------------------------------
    //Criado por...: Alexandre Maximiano - 02/11/2009
    //Objetivo.....: Cabe�alho padr�o da p�gina
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
        $get('<%=txtEmailPesq.ClientID%>').value = '';
        $get('<%=txtCodigoPesq.ClientID%>').value = '';
        
        $get('divDescricao').style.display = 'none';
        $get('divCodigo').style.display = 'none';
        switch (tvar) {
            case 1: //Pesquisa por Codigo
                $get('divCodigo').style.display = 'block';
                $get('<%=txtCodigoPesq.ClientID%>').focus();
                break;
            case 2: //Pesquisa por Descri��o
                $get('divDescricao').style.display = 'block';
                $get('<%=txtEmailPesq.ClientID%>').focus();
                break;
        }
    }

    //--------------------------------------------------------------------------------
    //Criado por...: Jacqueline Albuquerque - 24/11/2009
    //Objetivo.....: Valida os campos
    //--------------------------------------------------------------------------------
    function ValidaCampos() {
        if (($get('<%=rbCodigo.ClientID%>').checked) && ($get('<%=txtCodigoPesq.ClientID %>').value != '')) {
            return true;
        }
        if (($get('<%=rbDescricao.ClientID%>').checked) && ($get('<%=txtEmailPesq.ClientID%>').value != '')) {
            return true;
        }

        else {
            if ($get('<%=rbCodigo.ClientID%>').checked)
                alert("Informe o C�digo do Produto.");

            else if ($get('<%=rbDescricao.ClientID%>').checked)
                alert('Informe a Descri��o do Produto.');

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
       if ($get('<%=txtEmail.ClientID%>').value == "") {
           valido = false;
           msn += "\n- E-mail n�o informada!";
       }
       if (!valido)
           alert(msn);
       return valido;
   }

   //--------------------------------------------------------------------------------
   //Criado por...: Naka - 15/03/2010
   //Objetivo.....: Valida os campos do cadastro do ICMS
   //--------------------------------------------------------------------------------
   function ValidaCamposCadastroICMS() {
       var valido = true;
       
       return valido;
   }
   

   function ChamaPopup() {

       window.showModalDialog('<%=caminhoAplicacao%>' + "Produto/CadastraUnidade.aspx?popup=sim", "", "dialogHeight=170px;dialogWidth=910px;status=no,toolbar=no,menubar=no,location=no;unadorned=no;help=no; resizable: No; status: No; scroll:no;");
   }
   function ChamaPopupClassificado() {
       window.showModalDialog('<%=caminhoAplicacao%>' + "Classificado/CadastraClassificado.aspx?popup=sim", "", "dialogHeight=200px;dialogWidth=910px;status=no,toolbar=no,menubar=no,location=no;unadorned=no;help=no; resizable: No; status: No; scroll:no;");
   }
   //--------------------------------------------------------------------------------
   //Criado por...:Alexandre Maximiano - 04/11/2010
   //Objetivo.....: Efetua consulta com o retorno do autocomplete
   //--------------------------------------------------------------------------------
   function CarregarValores(source, eventArgs) {
       $get('<%=hdfIdRazaoSocial.ClientID%>').value = eventArgs.get_value();
       $get('<%=txtEmailPesq.ClientID %>').value = eventArgs._item.outerText;
       $get('<%=btnPesquisar.ClientID%>').click();
   }
</script>
<table style="margin-left: auto; width: 95%; margin-right: auto;">
    <tr>
        <td style="width: 28px">
            <asp:Image ID="ImgCadastro" runat="server" ImageUrl="~/Imagens/layout.png" Height="16px" />
        </td>
        <td class="titulo">Cadastro de Envio de E-mail</td>
    </tr>
</table>
<br />
<div style="text-align:center; width:100%;" id="divConsulta" runat="server">
    <div class="fundoTabela" style="width:95%" >
    <table id="tblConsulta" runat="server" style="text-align:left; width:100%">
        <tr>
            <td style="width: 20%;padding-left:17px"> Op��es de Consulta:</td>
            <td style="width: 20%">
                <asp:RadioButton ID="rbCodigo" Checked="true" onclick ="TipoPesquisa(1)"  
                    GroupName="filtroPesq" runat="server" Text="C�digo" CssClass="noBorder" />
            </td>
            <td style="width: 20%" colspan="2">
                <asp:RadioButton ID="rbDescricao" onclick ="TipoPesquisa(2)" runat="server" 
                    Text="E-mail" GroupName="filtroPesq" CssClass="noBorder" />
            </td>
         </tr>
        <tr>
            <td style="width: 20%">&nbsp;</td>
            <td colspan="2" style="height:36px">
                <div id="divCodigo"  style="display:block">
                    <asp:TextBox ID="txtCodigoPesq" MaxLength="18" runat="server" Height="16px" 
                        Width="150px"></asp:TextBox>
                    <asp:CustomValidator ClientValidationFunction="ValidaCampos" Text="*" CssClass="asterisco" ValidationGroup="pesquisar" ErrorMessage="Favor informar o C�digo." runat="server" id="efvCodigo"></asp:CustomValidator>
                </div>
                <div id="divDescricao" style="display:none">
                    <asp:TextBox ID="txtEmailPesq" runat="server" Height="16px" Width="600px"></asp:TextBox>
                            <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="txtEmailPesq"
                            MinimumPrefixLength="1" ServiceMethod="GetEmail" CompletionInterval="800" EnableCaching="true"
                            CompletionSetCount="10" OnClientItemSelected="CarregarValores" OnClientPopulated="ClientPopulated">
                            </ajaxToolkit:AutoCompleteExtender>
                    <asp:CustomValidator Text="*" CssClass="asterisco" ID="cvDescricaoPesq" ValidationGroup="pesquisar" ClientValidationFunction="ValidaCampos"  ErrorMessage="Favor informar a Descri��o." runat="server"></asp:CustomValidator>
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
                                onrowcommand="grdListaResultado_RowCommand" 
                                onrowdatabound="grdListaResultado_RowDataBound" 
                                onpageindexchanging="grdListaResultado_PageIndexChanging" Width="95%" 
                                EnableModelValidation="True">
                                <Columns>
                                    <asp:TemplateField HeaderText="A��es">
                                        <itemtemplate>
                                            <asp:ImageButton ID="imgIncluirItem" Width="15px" Height="15px" runat="server" 
                                                ImageUrl="~/Imagens/IncluirItem.png" Visible="False" />
                                            &nbsp;<asp:ImageButton ID="imgEditar" runat="server" Height="15px" 
                                                ImageUrl="~/Imagens/editar.png" Width="15px" />
                                            <asp:ImageButton ID="imgExcluir" Width="15px" Height="15px" runat="server" ImageUrl="~/Imagens/exclusao_Canc.png" />
                                         </itemtemplate>
                                        <HeaderStyle CssClass="headerGrid" Width="5%" />
                                        <ItemStyle HorizontalAlign="center" Wrap="false"/>
                                    </asp:TemplateField>
                                     <asp:BoundField HeaderText="C�digo" >
                                         <headerstyle wrap="false" CssClass="headerGrid"></headerstyle>
                                         <itemstyle wrap="false" HorizontalAlign="Left" Width="10%"></itemstyle>
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Email" >
                                        <HeaderStyle Wrap="false" CssClass="headerGrid"/>
                                        <ItemStyle HorizontalAlign="Left" Width="50%" />
                                    </asp:BoundField>
                                   <asp:BoundField HeaderText="Enviar para:" >
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
<ajaxToolkit:ModalPopupExtender ID="mpeIncluirProduto" runat="server" PopupControlID="pnlIncluirProduto"
    TargetControlID="hdfTargetIncluirProduto" BehaviorID="mpeIncluirProdutoID" BackgroundCssClass="modalBackground"
    DropShadow="true" />    
<asp:Panel ID="pnlIncluirProduto" runat="server">
    <asp:UpdatePanel  runat="server" ID="upCadastro" UpdateMode="Conditional">
        <ContentTemplate>
            <div style="text-align: center;width:600px; height:auto; padding: 5px 5px 5px 5px; background-color: #ffffff;">
                <table class="fundoTabela">
                    <!--T�TULO DA POPUP-->
                    <tr>
                        <td class="titulo">
                            <b>
                                <% if (hdfTipoAcao.Value == "Incluir") { %>
                                        ::Inclus�o de Envio de E-mail
                                <% } %>
                                <% else { %>
                                        ::Altera��o de Envio de E-mail
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
                                    <td colspan="3" style="width: 148px"><b>::Dados cadastrais:</b></td>
                                </tr>
                                <tr>
                                    <td style="padding-left:17px; width: 87px;">C�digo:</td>
                                    <td style="padding-left:20px; width: 178px;">Email:</td>
                                    <td style="padding-left:17px; width: 148px;">Enviar para:</td>
                                </tr>
                                <tr>
                                    <td style="width: 87px">
                                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="DesligarTextBox" 
                                            MaxLength="50" TabIndex="1" Width="50px"></asp:TextBox>
                                    </td>
                                    <td style="width: 178px">
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="formNovo" 
                                            MaxLength="50" TabIndex="1" Width="200px"></asp:TextBox>

                                    </td>
                                       <td style="width: 148px">
                                        <asp:DropDownList ID="ddlTipo" runat="server" CssClass="formNovo" MaxLength="50" 
                                               TabIndex="2" Width="200px">
                                            <asp:ListItem Value="0">Compras</asp:ListItem>
                                            <asp:ListItem Value="2">Vendas</asp:ListItem>
                                           </asp:DropDownList>
                                           &nbsp;</td>
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
                                            <td colspan="3" style="text-align:right">
                                                <asp:Button ID="btnSalvar" OnClientClick="return ValidaCamposCadastro();" runat="server" CssClass="botao" 
                                                onclick="btnSalvar_Click" Text="Salvar" ValidationGroup="Cadastro" Width="80px" />                                                                         
                                           </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <asp:HiddenField ID="hdfCodProduto" runat="server" />
                    <asp:HiddenField ID="hdfCodigo" runat="server" />
                </table>
            </div>
            <asp:HiddenField ID="hdfTipoAcao" runat="server" />
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnIncluir" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnCancelar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="grdListaResultado"  EventName="RowCommand" />
            <asp:AsyncPostBackTrigger ControlID="btnSalvar" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Panel>
<asp:HiddenField ID="hdfCadastroPopup" runat="server" />
<asp:HiddenField ID="hdfTargetIncluirProduto" runat="server" />    
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="cadastro" />
</asp:Content>
