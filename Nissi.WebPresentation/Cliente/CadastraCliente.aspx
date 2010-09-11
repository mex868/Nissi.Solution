<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="CadastraCliente.aspx.cs" Inherits="CadastraCliente" Title="Cadastro de Cliente" EnableEventValidation="false" %>
<%@ Register Assembly="RDC.Tools" Namespace="RDC.Tools" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register src="../UserControl/Endereco.ascx" tagname="Endereco" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="server">
<script type="text/javascript" src="../JScripts/Common.js"></script>
        <script type="text/javascript" language="javascript">
        //--------------------------------------------------------------------------------
        //Criado por...: Alexandre Maximiano - 02/11/2009
        //Objetivo.....: Cabeçalho padrão da página
        //--------------------------------------------------------------------------------
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_initializeRequest(InitializeRequest);
        prm.add_endRequest(EndRequest);
        var postBackElement;
        function InitializeRequest(sender, args)
        {
            if (prm.get_isInAsyncPostBack())
                args.set_cancel(true);

            if (args.get_postBackElement().type != 'checkbox')
                WaitAsyncPostBack(true);
        }
        function EndRequest(sender, args)
        {
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
            $get('<%=txtCPFPesq.ClientID%>').value = '';
            $get('<%=txtCodigoPesq.ClientID%>').value = '';
            $get('divCNPJ').style.display = 'none';
            $get('divCPF').style.display = 'none';
            $get('divCodigo').style.display = 'none';
            $get('divRazao').style.display = 'none';
            $get('divNomeFantasia').style.display = 'none';
            switch (tvar) {
                case 1: //Pesquisa por Código
                    $get('divCodigo').style.display = 'block';

                    break;
                case 2: //Pesquisa por CNPJ ou CPF
                    if ($get('<%=ddlTipoPessoa.ClientID %>').value == 1)
                        $get('divCNPJ').style.display = 'block';
                    else
                        $get('divCPF').style.display = 'block';
                    break;
                case 3: //Pesquisa por RG
                    $get('divRazao').style.display = 'block';

                    break;
                case 4: //Pesquisa por NOME
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
           if (($get('<%=rbCNPJ.ClientID%>').checked) && ($get('<%=txtCPFPesq.ClientID %>').value != '')) {
               return true;
           }
           if (($get('<%=rbCodigo.ClientID%>').checked) && ($get('<%=txtCodigoPesq.ClientID%>').value != '')) {
               return true;
           }
            if (($get('<%=rbRazaoSocial.ClientID%>').checked) && ($get('<%=txtRazao.ClientID%>').value != '')) {
                return true;
            }
            if (($get('<%=rbNomeFantasia.ClientID%>').checked) && ($get('<%=txtNomeFantasiaPesq.ClientID%>').value != '')) {
                return true;
            }
            else {
                if ($get('<%=rbCodigo.ClientID%>').checked) {
                    alert("Informe o Código do Cliente.");
                    return false;
                }

                if ($get('<%=ddlTipoPessoa.ClientID %>').value == 1) {

                    if ($get('<%=rbCNPJ.ClientID%>').checked) {
                        alert("Informe o C.N.P.J do Cliente.");
                    }
                    else if ($get('<%=rbRazaoSocial.ClientID%>').checked)
                        alert('Informe a Razão Social do Cliente.');
                    else {
                        alert('Informe o Nome Fantasia do Cliente.');
                    }
                }
                else if ($get('<%=ddlTipoPessoa.ClientID %>').value == 2) {
                    if ($get('<%=rbCNPJ.ClientID%>').checked)
                        alert("Informe o CPF do Cliente.");
                    else if ($get('<%=rbRazaoSocial.ClientID%>').checked)
                        alert('Informe o Nome do Cliente.');
                    else
                        alert('Informe o Nome Fantasia do Cliente.');

                }
                return false;
            }
        }

        function VerificaTipoPessoa() {
            $get('<%=lblTitulo.ClientID%>').innerText = "Inclusão de Cliente"; 
            if (
                $get('<%=ddlTipoPessoa.ClientID %>').value == 2) {
                $get('<%=rbCNPJ.ClientID%>').nextSibling.innerText = "CPF";
                $get('<%=rbRazaoSocial.ClientID%>').nextSibling.innerText = "Nome";
                $get('<%=tdRazaoSocial.ClientID%>').innerHTML = "Nome";
                $get('<%=tdInscricaoEstadual.ClientID%>').innerHTML = "RG";
                $get('<%=tdCNPJ.ClientID%>').innerHTML = "CPF";
            }
            else {
                $get('<%=rbCNPJ.ClientID%>').nextSibling.innerText = "C.N.P.J";
                $get('<%=rbRazaoSocial.ClientID%>').nextSibling.innerText = "Razão Social";
                $get('<%=tdRazaoSocial.ClientID%>').innerHTML = "Razão Social";
                $get('<%=tdInscricaoEstadual.ClientID%>').innerHTML = "Inscrição Estadual";
                $get('<%=tdCNPJ.ClientID%>').innerText = "C.N.P.J";
            }
            if ($get('<%=rbCNPJ.ClientID%>').checked)
                TipoPesquisa(2);
        }
        /************************************************************************************
        - Criado Por: Jacqueline Oliveira
        - Criado Em.: 01/12/2009
        - Objetivo..: Manipular itens de associação nas Listboxes
        **************************************************************************************/
        function btnAssociar_Click() {
            fnTrocaLista($get('<%=lbxAssociar.ClientID%>'), $get('<%=lbxAssociados.ClientID%>'), false);
        }
        function btnRetirar_Click() {
            fnTrocaLista($get('<%=lbxAssociados.ClientID%>'), $get('<%=lbxAssociar.ClientID%>'), false);
        }
        function btnAllAssociar_Click() {
            fnTrocaLista($get('<%=lbxAssociar.ClientID%>'), $get('<%=lbxAssociados.ClientID%>'), true);
        }
        function btnAllRetirar_Click() {
            fnTrocaLista($get('<%=lbxAssociados.ClientID%>'), $get('<%=lbxAssociar.ClientID%>'), true);
        }
        
       /************************************************************************************
        - Criado Por: Jacqueline Oliveira
        - Criado Em.: 01/12/2009
        - Objetivo..: Armazenar Localidades associadas em campo hidden
        **************************************************************************************/
        function ItensAssociados() {

            $get('<%=hdnListaTransportadora.ClientID%>').value = '';
            var qtdeTransportadora = 0;
            var nomeTransportadora = '';

            for (var i = 0; i < $get('<%=lbxAssociados.ClientID%>').options.length; i++) {
                $get('<%=hdnListaTransportadora.ClientID%>').value += $get('<%=lbxAssociados.ClientID%>').options[i].value + '|';
                nomeTransportadora = $get('<%=lbxAssociados.ClientID%>').options[i].text;
                qtdeTransportadora++;
            }

            var texto = '';
           /* if (qtdeTransportadora == 0) {
                alert('Nenhuma transportadora associada.\n\n');
                return false;
            }*/
            if (qtdeTransportadora == 1)
                texto = 'Você selecionou a transportadora "' + nomeTransportadora.toString() + '".\n\n';
            else
                texto = 'Você têm ' + qtdeTransportadora.toString() + ' transportadora associadas.\n\n';

            texto += 'Confirmar gravação?'

            return confirm(texto);
        }

        function ChamaPopup() {

            window.showModalDialog('<%=caminhoAplicacao%>' + "Transportadora/cadastraTransportadora.aspx?popup=sim", "", "dialogHeight=600px;dialogWidth=850px;status=no,toolbar=no,menubar=no,location=no;unadorned=no;help=no; resizable: No; status: No;");

            $get('<%=btnAtualizar.ClientID%>').click();
        }

        //--------------------------------------------------------------------------------
        //Criado por...: Jacqueline Albuquerque - 07/12/2009
        //Objetivo.....: Valida os campos do cadastro
        //--------------------------------------------------------------------------------
        function ValidaCamposCadastro(src, args) {
            var valido = true;
            //ctvValidaCampo.errormessage = "";
            var ctvValidaCampo = $get('<%=ctvValidaCampo.ClientID%>');

            if ($get('<%=txtCodigo.ClientID%>').value == "") {
                valido = false;
                ctvValidaCampo.errormessage = "Código não informado!";
            }

            if (($get('<%=ddlTipoPessoa.ClientID %>').value == 1)) {

                if ($get('<%=txtRazaoSocial.ClientID%>').value == "") {
                    ctvValidaCampo.errormessage += "\n- Razão Social não informada!";
                    valido = false;
                }

                if ($get('<%=txtInscricaoEstadual.ClientID%>').value == "") {
                    ctvValidaCampo.errormessage += "\n- Inscrição Estadual não informada!.";
                    valido = false;
                }

                if ($get('<%=txtCNPJ.ClientID%>').value == "") {
                    valido = false;
                    ctvValidaCampo.errormessage += "\n- CNPJ não informado!";
                }
                else {
                    valido = ValidarCNPJ($get('<%=txtCNPJ.ClientID%>'));
                    if (!valido)
                        ctvValidaCampo.errormessage += "\n- CNPJ inválido";
                }

                if ($get('<%=txtNomeFantasia.ClientID%>').value == "") {
                    valido = false;
                    ctvValidaCampo.errormessage += "\n- Nome fantasia não informado!"
                }
            }
            else {
                if ($get('<%=txtRazaoSocial.ClientID%>').value == "") {
                    ctvValidaCampo.errormessage += "\n- Nome não informado!";
                    valido = false;
                }
                if ($get('<%=txtInscricaoEstadual.ClientID%>').value == "") {
                    ctvValidaCampo.errormessage += "\n- RG não informado!";
                    valido = false;
                }
                if ($get('<%=txtCPFCadastro.ClientID%>').value == "") {
                    valido = false;
                    ctvValidaCampo.errormessage += "\n- CPF não informado!";
                }
                else {
                    valido = Verifica_CPF($get('<%=txtCPFCadastro.ClientID%>'));
                    if (!valido)
                        ctvValidaCampo.errormessage += "\n- CPF Inválido!";
                }
                if ($get('<%=txtNomeFantasia.ClientID%>').value == "") {
                    valido = false;
                    ctvValidaCampo.errormessage += "\n- Apelido não informado!"
                }
            }
            if (valido) {
                valido = ItensAssociados();
            }
            args.IsValid = valido;
        }

    </script>
    <div style="text-align:center;">
        <table style="margin-left: auto; width: 95%; margin-right: auto;">
            <tr>
                <td style="width: 21px; text-align:left">
                    <asp:Image ID="ImgCadastro" runat="server" ImageUrl="~/Imagens/layout.png" />
                </td>
                <td style="width: 95%; text-align: left" class="titulo">Cadastro de Cliente</td>
            </tr>
        </table>
        <br />
        <div style=" text-align:center; width:100%;">
            <table class="fundoTabela" style="text-align:left; width:95%">
                <tr>
                    <td><b>Tipo de Pessoa:</b> </td>
                    <td>
                        <asp:UpdatePanel runat="server" ID="updDDL" UpdateMode="Conditional">
                            <ContentTemplate>
                            <asp:DropDownList AutoPostBack="true" runat="server" onchange="VerificaTipoPessoa()" 
                                ID="ddlTipoPessoa" CssClass="formNovo" 
                                onselectedindexchanged="ddlTipoPessoa_SelectedIndexChanged">
                                <asp:ListItem Text="Juridíco" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Físico" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%;padding-left:17px"> Opções de Consulta:</td>
                    <td style="width: 20%">
                        <asp:RadioButton ID="rbCodigo" onclick ="TipoPesquisa(1)"  
                            GroupName="filtroPesq" runat="server" Text="Código" Checked="True" 
                            CssClass="noBorder" />
                    </td>
                    <td style="width: 20%">
                        <asp:RadioButton ID="rbCNPJ" onclick ="TipoPesquisa(2)"  GroupName="filtroPesq" 
                            runat="server" Text="C.N.P.J" CssClass="noBorder" />
                    </td>
                    <td style="width: 20%">
                        <asp:RadioButton ID="rbRazaoSocial" onclick ="TipoPesquisa(3)" runat="server" 
                            Text="Razão Social" GroupName="filtroPesq" CssClass="noBorder" />
                    </td>
                    <td style="width: 20%">
                        <asp:RadioButton ID="rbNomeFantasia" onclick ="TipoPesquisa(4)" runat="server" 
                            Text="Nome Fantasia " GroupName="filtroPesq" CssClass="noBorder" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">&nbsp;</td>
                    <td colspan="4">
                        <div id="divCodigo"  style="display:block">
                            <asp:TextBox ID="txtCodigoPesq" MaxLength="14" onkeypress="ConverterCaixaAlta()" runat="server" Height="16px" Width="100px"></asp:TextBox>
                            <asp:CustomValidator ClientValidationFunction="ValidaCampos" Text="*" CssClass="asterisco" ValidationGroup="pesquisar" ErrorMessage="Favor informar o Código" runat="server" id="CustomValidator1"></asp:CustomValidator>
                        </div>
                        <div id="divCNPJ"  style="display:none">
                            <asp:TextBox ID="txtCNPJPesq" MaxLength="18" onkeypress="return digitos(event, this);" onkeyup="Mascara('CNPJ',this,event);" runat="server" Height="16px" Width="120px"></asp:TextBox>
                            <asp:CustomValidator ClientValidationFunction="ValidaCampos" Text="*" CssClass="asterisco" ValidationGroup="pesquisar" ErrorMessage="Favor informar o C.N.P.J." runat="server" id="efvCNPJ"></asp:CustomValidator>
                        </div>
                        <div id="divCPF"  style="display:none">
                            <asp:TextBox ID="txtCPFPesq" MaxLength="14" onkeypress="return digitos(event, this);" onkeyup="Mascara('CPF',this,event);" runat="server" Height="16px" Width="100px"></asp:TextBox>
                            <asp:CustomValidator ClientValidationFunction="ValidaCampos" Text="*" CssClass="asterisco" ValidationGroup="pesquisar" ErrorMessage="Favor informar o CPF" runat="server" id="CustomValidator2"></asp:CustomValidator>
                        </div>
                        <div id="divRazao" style="display:none">
                            <asp:TextBox ID="txtRazao" runat="server" Height="16px" Width="600px"></asp:TextBox>
                            <asp:CustomValidator Text="*" CssClass="asterisco" ID="cvRazao" ValidationGroup="pesquisar" ClientValidationFunction="ValidaCampos"  ErrorMessage="Favor informar a Razão Social." runat="server"></asp:CustomValidator>
                        </div>
                        <div id="divNomeFantasia" style="display:none">
                            <asp:TextBox ID="txtNomeFantasiaPesq" runat="server" Height="16px" Width="600px"></asp:TextBox>
                            <asp:CustomValidator ValidationGroup="pesquisar" Text="*" cv="cvNomeFant" CssClass="asterisco" ClientValidationFunction="ValidaCampos"  ErrorMessage="Favor informar o Nome Fantasia." runat="server"></asp:CustomValidator>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left" colspan="3">
                        <asp:Button ID="btnVoltar" runat="server" OnClick="btnVoltar_Click" 
                            CssClass="botao" Text="Voltar" Width="100px" UseSubmitBehavior="False" />
                    </td>
                    <td colspan="2" style="text-align:right">
                        <asp:UpdatePanel ID="updBotoes" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Button ID="btnPesquisar" OnClientClick="return ValidaCampos()" runat="server" ValidationGroup="pesquisar" CssClass="botao"
                                    Text="Pesquisar" Width="100px" OnClick="btnPesquisar_Click" />
                                    &nbsp;<asp:Button ID="btnIncluir" runat="server" CssClass="botao" Width="100px"
                                    Text="Incluir Novo" OnClientClick=" VerificaTipoPessoa()" onclick="btnIncluir_Click" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <asp:UpdatePanel ID="updListaResultado" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div  id="divListaResultado" runat="server" style="overflow: auto; display: block; text-align:center; height:400px;">
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
                            <asp:BoundField HeaderText="Razão Social">
                                <HeaderStyle CssClass="headerGrid" />
                                <ItemStyle HorizontalAlign="Left" Width="30%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Nome Fantasia">
                                <HeaderStyle CssClass="headerGrid" />
                                <ItemStyle HorizontalAlign="Left" Width="30%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="CNPJ">
                                <HeaderStyle CssClass="headerGrid"  />
                                <ItemStyle HorizontalAlign="Left" Width="15%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Inscrição Estadual">
                                <HeaderStyle CssClass="headerGrid" />
                                <ItemStyle HorizontalAlign="Left" Width="15%" />
                            </asp:BoundField>
                        </Columns>
                    </cc1:RDCGrid>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:HiddenField ID="hdfTargetInclui" runat="server" />
        <ajaxToolkit:ModalPopupExtender ID="mpeIncluir" runat="server" PopupControlID="pnlIncluir"
            TargetControlID="hdfTargetInclui" BehaviorID="mpeIncluirID" BackgroundCssClass="modalBackground" DropShadow="true" />    
            <asp:Panel ID="pnlIncluir" runat="server">
                <div style="text-align: center;width:900px; height:auto; padding: 5px 5px 5px 5px; background-color: #ffffff;">
                        <table style="width:95%">
                            <tr>
                                <td>
                                    <table style="width:95%"  class="fundoTabela">
                                        <!--TÍTULO DA POPUP-->
                                        <tr>
                                            <td class="titulo">
                                            <asp:UpdatePanel runat="server" ID="upTitulo" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:Label runat="server" ID="lblTitulo"></asp:Label>
                                                 </ContentTemplate>
                                                 <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="grdListaResultado" EventName="RowCommand" />
                                                </Triggers>
                                             </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <div style="overflow: scroll; padding-left:9px; width:100%; height:500px">
                                        <table style="width:95%"  align="center" class="fundoTabela">
                                            <tr>
                                                <td>
                                                    <asp:UpdatePanel runat="server" ID="upCadastro">
                                                        <ContentTemplate>
                                                            <table style="width:95%" align="center" class="fundoTabela" >
                                                                <tr>
                                                                    <td><b>::Dados cadastrais</b></td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="padding-left:17px" id="tdCodigo">Código:</td>
                                                                    <td runat="server" style="padding-left:20px" id="tdRazaoSocial">Razão Social:</td>
                                                                    <td runat="server" style="padding-left:20px" id="tdNomeFantasia">Nome Fantasia:</td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="padding-left:0px;">
                                                                        <asp:TextBox  CssClass="formNovo" ID="txtCodigo" onkeypress="ConverterCaixaAlta()"  MaxLength="20" runat="server"></asp:TextBox></td>
                                                                        <asp:CustomValidator  ID="ctvValidaCampo" runat="server" ClientValidationFunction="ValidaCamposCadastro" CssClass="asterisco" ValidationGroup="Inserir"></asp:CustomValidator>
                                                                    <td>
                                                                        <asp:TextBox CssClass="formNovo" ID="txtRazaoSocial" runat="server" Width="97%"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox CssClass="formNovo" ID="txtNomeFantasia" runat="server" Width="248px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td runat="server" style="padding-left:17px" id="tdCNPJ">CNPJ:</td>    
                                                                    <td runat="server" style="padding-left:20px" id="tdInscricaoEstadual">Inscrição Estadual:</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <div style="display:block" id="divCNPJCadastro" runat="server">
                                                                            <asp:TextBox CssClass="formNovo" ID="txtCNPJ" MaxLength="18" onkeypress="Mascara('CNPJ',this,event);" runat="server" Width="146px"></asp:TextBox>
                                                                        </div>
                                                                         <div style="display:block" id="divCPFCadastro" runat="server">
                                                                            <asp:TextBox CssClass="formNovo" MaxLength="14" ID="txtCPFCadastro" onkeypress="return digitos(event, this);" onkeyup="Mascara('CPF',this,event);" runat="server" Width="146px"></asp:TextBox>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox CssClass="formNovo"  ID="txtInscricaoEstadual" onkeypress="return digitos(event, this);" runat="server" Width="150px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnIncluir" EventName="" />
                                                            <asp:AsyncPostBackTrigger ControlID="grdListaResultado" 
                                                                EventName="RowCommand" />
                                                            <asp:AsyncPostBackTrigger ControlID="btnSalvar" EventName="Click" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                    <br style="line-height:10px" />
                                                    <asp:UpdatePanel runat="server" ID="upContatos">
                                                        <ContentTemplate>
                                                            <div>
                                                                <table style="width:100%;"  align="center">
                                                                    <tr>
                                                                        <td>
                                                                            <uc1:Endereco  ID="Endereco" runat="server" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <br style="line-height:10px" />
                                                                <table style="width:95%;" class="fundoTabela"  align="center">
                                                                    <tr>
                                                                        <td><b>::Contatos:</b></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="padding-left:17px">Telefone:</td>
                                                                        <td style="padding-left:20px">Fax:</td>
                                                                        <td style="padding-left:20px">Celular:</td>
                                                                        <td style="padding-left:20px">Comprador:</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox CssClass="formNovo" ID="txtTelefone" onkeypress="return digitos(event, this);" MaxLength="14" onkeyup="Mascara('TEL',this,event);" runat="server"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox CssClass="formNovo" ID="txtFax" onkeypress="return digitos(event, this);" MaxLength="14" onkeyup="Mascara('TEL',this,event);" runat="server"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox CssClass="formNovo" ID="txtCelular" onkeypress="return digitos(event, this);" MaxLength="14" onkeyup="Mascara('TEL',this,event);" runat="server"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox CssClass="formNovo" ID="txtContato" runat="server"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="padding-left:17px">Email:</td>
                                                                        <td style="padding-left:20px">Site:</td>
                                                                        <td style="padding-left:20px" colspan="2">Vendedor:</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox CssClass="formNovo" ID="txtEmail" runat="server" Width="198px"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtSite" CssClass="formNovo" runat="server"></asp:TextBox>
                                                                        </td>
                                                                        <td colspan="2">
                                                                            <asp:DropDownList CssClass="formNovo" ID="ddlFuncionario" runat="server">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="padding-left:17px" >Email - NF-e:</td>
                                                                        <td style="padding-left:17px" colspan="3">Observação:</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="padding-top:0;" >
                                                                        <asp:TextBox ID="txtEmailNFE" CssClass="formNovo" runat="server" Height="50px"  Width="198px"></asp:TextBox></td>
                                                                        <td colspan="3" style="padding-left:17px">
                                                                            <asp:TextBox CssClass="formNovo" ID="txtObservacao" runat="server" Height="50px" 
                                                                            TextMode="MultiLine" Width="95%"></asp:TextBox></td>
                                                                    </tr>
                                                                    <tr>
                                                                    <td style="padding-left:17px">Cep Cobrança:</td>
                                                                    <td style="padding-left:17px">Endereço Cobrança:</td>
                                                                    </tr>
                                                                    <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="txtCepCobranca" onkeypress="return digitos(event, this);" MaxLength="9" onkeyup="Mascara('CEP',this,event);" runat="server"></asp:TextBox></td>
                                                                    <td colspan="3" >
                                                                        <asp:TextBox ID="txtEnderecoCobranca" runat="server" Width="92%"></asp:TextBox></td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                           </ContentTemplate>
                                                           
                                                        </asp:UpdatePanel>
                                                        <br style="line-height:10px" />
                                                        <asp:UpdatePanel  runat="server" ID="upCadastroTransportadora" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                        <table style="width:95%;" class="fundoTabela" align="center">
                                                            <tr>
                                                                <td><b>::Transportadoras</b></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: left;">
                                                                    <table border="0" cellpadding="5" cellspacing="0" style="width: 100%;">
                                                                        <tr>
                                                                            <td style="text-align: left;padding-left:17px;">Transportado(s) Disponível(is)</td>
                                                                            <td align="left"><img alt="" src="../Imagens/Incluir.png" onclick="ChamaPopup();" title="Incluir Transportadora" style="width:18px; height:18px; cursor:hand"/></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: left; width: 100%;">
                                                                                <asp:ListBox ID="lbxAssociar" runat="server" CssClass="formNovo" SelectionMode="Multiple"
                                                                                    Width="100%" Height="300px" OnDblClick="btnAssociar_Click();"></asp:ListBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td style="text-align: center; width: 10%;" valign="middle">
                                                                    <table border="0" style="width: 100%" cellpadding="1" cellspacing="0">
                                                                        <tr>
                                                                            <td style="text-align: left; width: 100%;">&nbsp;</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: center; width: 100%;">
                                                                                <input class="botao" id="btnAssociar" style="width: 48px" onclick="btnAssociar_Click();"
                                                                                    type="button" value=">" name="btnAssociar" runat="server" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: center; width: 100%;">
                                                                                <input class="botao" id="btnRetirar" style="width: 48px" onclick="btnRetirar_Click();"
                                                                                    type="button" value="<" name="btnRetirar" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: center; width: 100%;">
                                                                                <input class="botao" id="btnAllAssociar" style="width: 48px" onclick="btnAllAssociar_Click();"
                                                                                    type="button" value=">>" name="btnAllAssociar" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: center; width: 100%;">
                                                                                <input class="botao" id="btnAllRetirar" style="width: 48px" onclick="btnAllRetirar_Click();"
                                                                                    type="button" value="<<" name="btnAllRetirar" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td style="text-align: right; width: 45%;">
                                                                    <table border="0" cellpadding="5" cellspacing="0" style="width: 100%">
                                                                        <tr>
                                                                            <td style="text-align: left;padding-left:20px">
                                                                               Transportadora(s) Associada(s)
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: right; width: 100%;">
                                                                                <asp:ListBox ID="lbxAssociados" runat="server" CssClass="formNovo" SelectionMode="Multiple"
                                                                                    Width="95%" Height="300px" OnDblClick="btnRetirar_Click();"></asp:ListBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                           <asp:HiddenField ID="hdfTipoAcao" runat="server" />
                                                            <asp:HiddenField ID="hdfCodCliente" runat="server" />
                                                            <asp:HiddenField ID="hdfTipoPessoa" runat="server" />  
                                                        </ContentTemplate>
                                                            <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnIncluir" EventName="Click" />
                                                             <asp:AsyncPostBackTrigger ControlID="btnAtualizar" EventName="Click" />
                                                             <asp:AsyncPostBackTrigger ControlID="grdListaResultado" EventName="RowCommand" />
                                                             <asp:AsyncPostBackTrigger ControlID="btnSalvar" EventName="Click" />
                                                             <asp:AsyncPostBackTrigger ControlID="btnCancelar" EventName="Click" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                   </td>
                                              </tr>
                                          </table>
                                      </div>
                                      <asp:UpdatePanel ID="upBotoesCadastro" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <table class="fundoTabela">
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="btnCancelar"  Text="Cancelar" runat="server" CssClass="botao" 
                                                        onclick="btnCancelar_Click" Width="80px" />
                                                    </td>
                                                    <td style="text-align:right">
                                                        <asp:Button ID="btnSalvar" Text="Salvar"  runat="server" CssClass="botao" 
                                                        Width="80px" ValidationGroup="Inserir" onclick="btnSalvar_Click"/>
                                                    </td>
                                                    <td style="display:none"><asp:Button  ID="btnAtualizar" runat="server" CssClass="botao" onclick="btnAtualizar_Click"/></td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                     </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                    </asp:Panel>
                </div>
    <asp:HiddenField ID="hdnListaTransportadora" runat="server" /> 
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" HeaderText="Erros encontrados:" ValidationGroup="Inserir" />
</asp:Content>
