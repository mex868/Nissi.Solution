<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="CadastraFuncionario.aspx.cs" Inherits="CadastraFuncionario" Title="Untitled Page" EnableEventValidation="false" %>
<%@ Register assembly="RDC.Tools" namespace="RDC.Tools" tagprefix="cc1" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register src="../UserControl/Endereco.ascx" tagname="Endereco" tagprefix="uc1" %>
<%@ Register src="../UserControl/Banco.ascx" tagname="Banco" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="server">
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
            switch (tvar) {
                case 1: //Pesquisa por CPF
                    $get('divCPF').style.display = 'block';
                    $get('divRG').style.display = 'none';
                    $get('divNome').style.display = 'none';
                    $get('<%=txtNome.ClientID%>').value = ''
                    $get('<%=txtRGPesq.ClientID%>').value = '';
                    $get('<%=txtCPFPesq.ClientID%>').value = '';
                    break;
                case 2: //Pesquisa por RG
                    $get('divCPF').style.display = 'none';
                    $get('divRG').style.display = 'block';
                    $get('divNome').style.display = 'none';
                    $get('<%=txtNome.ClientID%>').value = ''
                    $get('<%=txtRGPesq.ClientID%>').value = '';
                    $get('<%=txtCPFPesq.ClientID%>').value = '';
                    break;
                case 3: //Pesquisa por NOME
                    $get('divCPF').style.display = 'none';
                    $get('divRG').style.display = 'none';
                    $get('divNome').style.display = 'block';
                    $get('<%=txtNome.ClientID%>').value = ''
                    $get('<%=txtRGPesq.ClientID%>').value = '';
                    $get('<%=txtCPFPesq.ClientID%>').value = '';
                    break;
            }
        }
        //--------------------------------------------------------------------------------
        //Criado por...: Jacqueline Albuquerque - 24/11/2009
        //Objetivo.....: Valida os campos
        //--------------------------------------------------------------------------------
        function ValidaCampos() {
            if ($get('<%=rbCpf.ClientID%>').checked) {
                var NumCPF = $get('<%=txtCPFPesq.ClientID %>');
                return Verifica_CPF(NumCPF);
            }
            if (($get('<%=rbRg.ClientID%>').checked) && ($get('<%=txtRGPesq.ClientID%>').value != '')) {
                return true;
            }
            if (($get('<%=rbNome.ClientID%>').checked) && ($get('<%=txtNome.ClientID%>').value != '')) {
                return true;
            }
            else {
                if ($get('<%=rbNome.ClientID%>').checked)
                    alert("Informe o Nome do Funcionário");
                else
                    alert('Informe o Número do RG');
                return false;
            }
        }

        function ValidaCPFInclusao() {
            var NumCPF = $get('<%=txtCPF.ClientID %>');
            return Verifica_CPF(NumCPF);
        }

        function ValidaCamposInclusao(srn, args) {
            if ($get('<%=chkAcessa.ClientID%>').checked) {
                if ($get('<%=txtLogin.ClientID %>').value == "")
                    args.IsValid = false;
            }
            if(!ValidaCPFInclusao())
                $get('<%=cvCampos.ClientID%>').errormessage = "CPF Inválido!";
            args.IsValid = ValidaCPFInclusao();
        }

        function HabilitaLogin() {
            if ($get('<%=chkAcessa.ClientID%>').checked) {
                $get('<%=divLogin.ClientID %>').style.display = 'block';
                $get('tdLogin').style.display = 'block';
            }
            else {
                $get('<%=divLogin.ClientID%>').style.display = 'none';
                $get('tdLogin').style.display = 'none';
            }
        }
    </script>
    <table style="margin-left: auto; width: 95%; margin-right: auto;">
        <tr>
            <td style="width: 21px">
                <asp:Image ID="ImgCadastro" runat="server" ImageUrl="~/Imagens/layout.png" />
            </td>
            <td style="text-align:left" class="titulo">Cadastro de Funcion&aacuterio</td>
        </tr>
    </table>
    <br style="line-height:5px" />
    <div class="fundoTabela" style="width:95%">
    <table id="tblConsulta" runat="server" style="text-align:left; width:100%">
        <tr>
            <td style="width: 25%"> Op&ccedil&otildees de Consulta:</td>
            <td style="width: 25%">
                <asp:RadioButton ID="rbCpf" onclick ="TipoPesquisa(1)"  GroupName="filtroPesq" 
                    runat="server" Text="C.P.F" Checked="True" CssClass="noBorder" />
            </td>
            <td style="width: 25%">
                <asp:RadioButton ID="rbRg" onclick ="TipoPesquisa(2)" runat="server" Text="R.G" 
                    GroupName="filtroPesq" CssClass="noBorder" />
            </td>
            <td style="width: 25%">
                <asp:RadioButton ID="rbNome" onclick ="TipoPesquisa(3)" runat="server" 
                    Text="Nome " GroupName="filtroPesq" CssClass="noBorder" />
            </td>
        </tr>
        <tr>
            <td style="width: 25%">&nbsp;</td>
            <td colspan="4">
                <div id="divCPF"  style="display:block">
                    <asp:TextBox ID="txtCPFPesq" MaxLength="14" onkeypress="return digitos(event, this);" onkeyup="Mascara('CPF',this,event);" runat="server" Height="16px" Width="100px"></asp:TextBox>
                </div>
                <div id="divRG" style="display:none">
                    <asp:TextBox ID="txtRGPesq" onkeypress="return digitos(event, this);" runat="server" Height="16px" Width="100px"></asp:TextBox>
                </div>
                <div id="divNome" style="display:none">
                    <asp:TextBox ID="txtNome" runat="server" Height="16px" Width="667px"></asp:TextBox>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <table width="100%">
                    <tr>
                        <td style="text-align:left; width: 25%;">
                            <asp:Button ID="btnVoltar" runat="server" OnClick="btnVoltar_Click" 
                                CssClass="botao" Text="Voltar" Width="100px" UseSubmitBehavior="False" />
                        </td>
                        <td colspan="2" style="text-align:right">
                            <asp:UpdatePanel ID="updBotoes" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Button ID="btnPesquisar" runat="server" CssClass="botao" 
                                        Text="Pesquisar" Width="100px" ValidationGroup="ValidarCPF" OnClientClick="return ValidaCampos()"  OnClick="btnPesquisar_Click" />
                                        &nbsp;<asp:Button ID="btnIncluir" runat="server" CssClass="botao" Width="100px"
                                        Text="Incluir Novo" onclick="btnIncluir_Click" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </div>
    <br style="line-height:5px" />
    <asp:UpdatePanel ID="updListaResultado" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="divListaResultado" runat="server" style="overflow: auto; display: block; text-align:center; height:400px;">
                 <cc1:RDCGrid id="grdListaResultado" runat="server" autogeneratecolumns="False" 
                    bordercolor="Black" borderwidth="1px" cellpadding="1" cellspacing="3" 
                    gridlines="None" pagesize="15" 
                    showpagedetails="True" AllowPaging="True" 
                    MultiSelection="False" ShowHeaderCheckBoxColumn="False" 
                    ShowOptionColumn="False" CssClass="alinhamento" 
                    onpageindexchanging="grdListaResultado_PageIndexChanging" 
                    onrowcommand="grdListaResultado_RowCommand" 
                    onrowdatabound="grdListaResultado_RowDataBound" 
                    onselectedindexchanged="grdListaResultado_SelectedIndexChanged">
                    <Columns>
                        <asp:TemplateField HeaderText="Ações">
                            <itemtemplate>
                                <asp:ImageButton  ID="imgEditar" runat="server" ImageUrl="~/Imagens/editar.png" />
                                <asp:ImageButton ID="imgReiniciar" runat="server" ImageUrl="~/Imagens/undo.png" />
                             </itemtemplate>
                            <HeaderStyle CssClass="headerGrid" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="13%"/>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Nome">
                            <HeaderStyle CssClass="headerGrid" />
                            <ItemStyle HorizontalAlign="Left" Width="20%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="RG" >
                            <HeaderStyle CssClass="headerGrid"/>
                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="CPF" >
                            <HeaderStyle CssClass="headerGrid" />
                            <ItemStyle HorizontalAlign="Left" Width="10%"  />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Departamento" >
                             <HeaderStyle CssClass="headerGrid" />
                            <ItemStyle HorizontalAlign="Left" Width="20%"  />
                        </asp:BoundField>                   
                        <asp:BoundField HeaderText="Cargo">
                            <HeaderStyle CssClass="headerGrid" />
                            <ItemStyle HorizontalAlign="Left" Width="20%" />
                        </asp:BoundField>
                    </Columns>
                </cc1:RDCGrid>
            </div>
        </ContentTemplate>
        <Triggers>
         <asp:AsyncPostBackTrigger ControlID="btnSalvar" EventName="Click" />
         <asp:AsyncPostBackTrigger ControlID="btnPesquisar" EventName="Click" />
         <asp:AsyncPostBackTrigger ControlID="btnCancelar" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:HiddenField ID="hdfTargetIncluir" runat="server" />
    <ajaxToolkit:ModalPopupExtender 
        ID="mpeIncluir" 
        runat="server" 
        PopupControlID="pnlIncluir"
        TargetControlID="hdfTargetIncluir" 
        BehaviorID="mpeIncluirID" 
        BackgroundCssClass="modalBackground"
        DropShadow="true" />    
    <asp:Panel ID="pnlIncluir" runat="server">
        <div style="text-align: center;width:900px; height:626px; padding: 5px 5px 5px 5px; background-color: #ffffff; overflow:auto">
            <asp:UpdatePanel ID="updIncluir" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:HiddenField ID="hdfTipoAcao" runat="server" />
                    <asp:HiddenField ID="hdfCodFuncionario" runat="server" />
                    <asp:HiddenField ID="hdfCodPessoa" runat="server" />
                        <table class="fundoTabela">
                        <!--TÍTULO DA POPUP-->
                            <tr>
                                <td class="titulo">
                                    <% if (hdfTipoAcao.Value == "Incluir") { %>
                                        ::Inclus&atildeo de Funcion&aacuterio
                                        <% } %>
                                        <% else { %>
                                            ::Altera&ccedil&atildeo de Funcion&aacuterio
                                        <% } %>
                                </td>
                            </tr>
                        </table>
                        <br style="line-height:5px" />
                        <table  class="fundoTabela" cellpadding="0" cellspacing="3">
                            <tr>
                                <td><b>::Dados cadastrais:</b></td>
                            </tr>
                            <tr>
                                <td>
                                    <table style="width:100%;text-align:left">
                                        <tr>
                                            <td align="left" style="padding-left:17px">Nome:</td>
                                            <td style="padding-left:20px">Apelido:</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtRazaoSocial" runat="server" Width="341px" CssClass="formNovo"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" 
                                                    ControlToValidate="txtRazaoSocial" ErrorMessage="Nome não informado!" 
                                                    ValidationGroup="Inserir">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNomeFantasia" runat="server" Width="200px" CssClass="formNovo"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                                    ControlToValidate="txtNomeFantasia" ErrorMessage="Apelido não informado!" 
                                                    ValidationGroup="Inserir">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td><asp:CheckBox runat="server" Checked="true" ID="chkAtivo" Text="Ativo" /></td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <table width="100%">
                                                    <tr>
                                                        <td  align="left" style="padding-left:17px; width:25%">CPF:</td>
                                                        <td style="padding-left:20px; width:25%">RG:</td>
                                                        <td>&nbsp</td>
                                                        <td style="padding-left:20px;display:none; width:25%" id="tdLogin">Login:</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox onkeypress="return digitos(event, this);" MaxLength="14" onkeyup="Mascara('CPF',this,event);" ID="txtCPF" runat="server" Width="146px" CssClass="formNovo"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                                                ControlToValidate="txtCPF" ErrorMessage="CPF não informado!" 
                                                                ValidationGroup="Inserir">*</asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox onkeypress="return digitos(event, this);" MaxLength="15" ID="txtRG" runat="server" Width="127px" CssClass="formNovo"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                                                ControlToValidate="txtRG" ErrorMessage="RG não informado!" 
                                                                ValidationGroup="Inserir">*</asp:RequiredFieldValidator>
                                                        </td>
                                                        <td style="width:25%"><asp:CheckBox runat="server" Checked="false" onclick="HabilitaLogin()" Text="Acessa Sistema" ID="chkAcessa" CssClass="formNovo" /></td>
                                                        <td style="width:25%">
                                                            <div runat="server" id="divLogin">
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox runat="server" ID="txtLogin" CssClass="formNovo"></asp:TextBox>
                                                                            <asp:CustomValidator ID="cvCampos" ClientValidationFunction="ValidaCamposInclusao" Text="*" CssClass="asterisco" runat="server" ErrorMessage="Login não Informado!" ValidationGroup="Inserir"></asp:CustomValidator>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <br style="line-height:5px" />
                        <uc1:Endereco ID="Endereco" runat="server" />
                        <br style="line-height:5px" />
                            <table class="fundoTabela">
                                <tr>
                                    <td style="font-family: Verdana; color: #000000; text-align:left">
                                        <b>::RH:</b>
                                    </td>
                                <tr>
                                    <td>
                                        <table style="width:100%;text-align:left">
                                            <tr>
                                                <td  align="left" style="padding-left:17px; width: 154px;">Admiss&atildeo:</td>
                                                <td style="padding-left:20px; width: 131px;">Desmiss&atildeo:</td>
                                                <td  style="padding-left:20px">Departamento:</td>
                                                <td style="padding-left:20px">Cargo:</td>
                                            </tr>
                                            <tr>
                                                <td style="width: 154px">
                                                    <ajaxToolkit:MaskedEditExtender ID="mskDataEmissao" runat="server" 
                                                        AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="false" 
                                                        Mask="99/99/9999" MaskType="Date" MessageValidatorTip="false" 
                                                        TargetControlID="txtDataAdmissao" UserDateFormat="DayMonthYear">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                    <asp:TextBox CssClass="formNovo" ID="txtDataAdmissao" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                                                        ControlToValidate="txtDataAdmissao" ErrorMessage="Admissão não informado!" 
                                                        ValidationGroup="Inserir">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td style="width: 131px">
                                                    <ajaxToolkit:MaskedEditExtender ID="mskDataDemissao" runat="server" 
                                                        AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="false" 
                                                        Mask="99/99/9999" MaskType="Date" MessageValidatorTip="false" 
                                                        TargetControlID="txtDataDemissao" UserDateFormat="DayMonthYear">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                    <asp:TextBox CssClass="formNovo" ID="txtDataDemissao" runat="server"></asp:TextBox>
                                               </td>
                                                <td align="left">
                                                    <asp:DropDownList CssClass="formNovo" ID="ddlDepartamento" runat="server" Width="186px"></asp:DropDownList>
                                                     <asp:ImageButton  CssClass="BotaoImg" ToolTip="Incluir Novo Departamento." ID="imgDepartamento" runat="server" Width="16px" 
                                                        onclick="imgDepartamento_Click" ImageUrl="~/Imagens/editar.png" />
                                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                                                        ControlToValidate="ddlDepartamento" ErrorMessage="Departamento não informado!" 
                                                        ValidationGroup="Inserir">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:DropDownList CssClass="formNovo" ID="ddlCargo" runat="server" 
                                                        Width="180px"></asp:DropDownList>
                                                    <asp:ImageButton ID="imgCargo" CssClass="BotaoImg" Width="16px" runat="server"
                                                        onclick="imgCargo_Click" ToolTip="Incluir Novo Cargo." ImageUrl="~/Imagens/editar.png" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                                                        ControlToValidate="ddlCargo" ErrorMessage="Cargo não informado!" 
                                                        ValidationGroup="Inserir">*</asp:RequiredFieldValidator>
                                               </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <br style="line-height:5px" />
                              <uc2:Banco ID="ucBanco" runat="server" />
                            <br style="line-height:5px"/>
                            <table class="fundoTabela">
                                <tr>
                                    <td style="font-family: Verdana; color: #000000; text-align:left">
                                        <b>::Contatos:</b></td>
                                </tr>   
                                <tr>
                                    <td>
                                        <table style="width:100%;text-align:left">
                                            <tr>
                                                <td style="padding-left:17px">Telefone:</td>
                                                <td style="padding-left:20px">Fax:</td>
                                                <td style="padding-left:20px">Celular:</td>
                                                <td style="padding-left:20px">Contato:</td>
                                            </tr>
                                            <tr>
                                                <td valign="top">
                                                    <asp:TextBox CssClass="formNovo" ID="txtTelefone" onkeypress="return digitos(event, this);" MaxLength="14" onkeyup="Mascara('TEL',this,event);" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                                                    ControlToValidate="txtTelefone" ErrorMessage="Telefone não informado!" 
                                                    ValidationGroup="Inserir">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td valign="top">
                                                    <asp:TextBox CssClass="formNovo" ID="txtFax" onkeypress="return digitos(event, this);" MaxLength="14" onkeyup="Mascara('TEL',this,event);" runat="server" Width="150px"></asp:TextBox>
                                                </td>
                                                <td valign="top">
                                                    <asp:TextBox CssClass="formNovo" ID="txtCelular" onkeypress="return digitos(event, this);" MaxLength="14" onkeyup="Mascara('TEL',this,event);" runat="server" Width="150px"></asp:TextBox>
                                                </td>
                                                <td valign="top">
                                                    <asp:TextBox CssClass="formNovo" ID="txtContato" runat="server" Width="150px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" 
                                                    ControlToValidate="txtContato" ErrorMessage="Contato não informado!" 
                                                    ValidationGroup="Inserir">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left:17px">Email:</td>
                                                <td style="padding-left:20px">Site:</td>
                                                <td style="padding-left:20px" colspan="2">Observa&ccedil&atildeo:</td>
                                            </tr>
                                            <tr>
                                                <td valign="top">
                                                    <asp:TextBox CssClass="formNovo" ID="txtEmail" runat="server" Width="150px"></asp:TextBox>
                                                </td>
                                                <td valign="top">
                                                    <asp:TextBox CssClass="formNovo" ID="txtSite" runat="server" Width="150px"></asp:TextBox>
                                                </td>
                                                <td valign="top" style="padding-left:20px" colspan="2">
                                                    <asp:TextBox ID="txtObservacao" runat="server" Height="30px" TextMode="MultiLine" Width="350px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <br style="line-height:5px" />
                            <table class="fundoTabela">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnCancelar"  Text="Cancelar" runat="server" CssClass="botao" 
                                            onclick="btnCancelar_Click" Width="80px" />
                                    </td>
                                    <td style="text-align:right">
                                        <asp:Button ID="btnSalvar" Text="Salvar" runat="server" CssClass="botao" 
                                        Width="80px" ValidationGroup="Inserir" onclick="btnSalvar_Click"/>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnIncluir" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="grdListaResultado" EventName="RowCommand" />
                            <asp:AsyncPostBackTrigger ControlID="btnSalvar" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </asp:Panel>
            <br style="line-height:5px" />
    <asp:HiddenField ID="hdfTargetIncluiDepartamento" runat="server" />
    <ajaxToolkit:ModalPopupExtender ID="mpeIncluiDepartamento" runat="server" PopupControlID="pnlIncluiDepartamento"
        TargetControlID="hdfTargetIncluiDepartamento" BehaviorID="mpeIncluiDepartamentoID" BackgroundCssClass="modalBackground"
        DropShadow="true" />
    <asp:Panel ID="pnlIncluiDepartamento" runat="server" Style="background-color: #DDDDDD; border: solid 1px Gray; color: Black">
        <div style="text-align: center; width: 300px; height:auto; padding: 5px 5px 5px 5px; background-color: #ffffff;">
                   <asp:UpdatePanel ID="updDepartamento" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:HiddenField ID="hdfTipoAcaoDepartamento" runat="server" />
                        <asp:HiddenField ID="hdfCodDepartamento" runat="server" />
                        <table border="0" style="width:95%; margin-right: auto; margin-left: auto; border: solid 1px black; text-align:left;" cellpadding="0" cellspacing="3">
                        <!--TÍTULO DA POPUP-->
                        <tr>
                            <td class="FundoLinha2">
                                <b>
                                <% if (hdfTipoAcaoDepartamento.Value == "Incluir")
                                   { %>
                                        ::Inclus&atildeo de Departamento
                                <% } %>
                                <% else { %>
                                        ::Altera&ccedil&atildeo de Departamento
                                <% } %>
                                </b>
                            </td>
                        </tr>
                        </table>
                        <table class="fundoTabela">
                            <tr>
                                <td style="text-align:left">Descri&ccedil&atildeo:</td>
                                <td>
                                    <asp:TextBox ID="txtDepartamento" runat="server">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                        ControlToValidate="txtDepartamento" 
                                        ErrorMessage="Descrição não informado!" 
                                        ValidationGroup="inserirDepartamento">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnCancelarDepartamento"  Text="Cancelar" runat="server" CssClass="botao" 
                                            Width="80px" onclick="btnCancelarDepartamento_Click" />
                                    </td>
                                    <td style="text-align:right">
                                        <asp:Button ID="btnSalvarDepartamento" Text="Salvar" runat="server" CssClass="botao" 
                                            Width="80px" ValidationGroup="InserirDepartamento" 
                                            onclick="btnSalvarDepartamento_Click" />
                                    </td>
                                </tr>
                        </table>
                    </ContentTemplate>
                       <Triggers>
                           <asp:AsyncPostBackTrigger ControlID="imgDepartamento" EventName="Click" />
                       </Triggers>
                   </asp:UpdatePanel>
                 </div>
            </asp:Panel>
            <asp:HiddenField ID="hdfTargetIncluirCargo" runat="server" />
            <ajaxToolkit:ModalPopupExtender ID="mpeIncluirCargo" runat="server" PopupControlID="pnlIncluirCargo"
                TargetControlID="hdfTargetIncluirCargo" BehaviorID="mpeIncluirCargoID" BackgroundCssClass="modalBackground"
                DropShadow="true" />
            <asp:Panel ID="pnlIncluirCargo" runat="server" Style="background-color: #DDDDDD; border: solid 1px Gray; color: Black">
            <div style="text-align: center; width: 300px; height:auto; padding: 5px 5px 5px 5px; background-color: #ffffff;">
                   <asp:UpdatePanel ID="updCargo" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:HiddenField ID="hdfTipoAcaoCargo" runat="server" />
                        <asp:HiddenField ID="hdfCodCargo" runat="server" />
                        <table border="0" style="width:95%; margin-right: auto; margin-left: auto; border: solid 1px black; text-align:left;" cellpadding="0" cellspacing="3">
                        <!--TÍTULO DA POPUP-->
                        <tr>
                            <td class="FundoLinha2">
                                <b>
                                <% if (hdfTipoAcaoCargo.Value == "Incluir")
                                   { %>
                                        ::Inclus&atildeo de Cargo
                                <% } %>
                                <% else { %>
                                        ::Altera&ccedil&atildeo de Cargo
                                <% } %>
                                </b>
                            </td>
                        </tr>
                        </table>
                        <table class="fundoTabela">
                            <tr>
                                <td style="text-align:left"> Descri&ccedil&atildeo: </td>
                                <td>
                                    <asp:TextBox ID="txtCargo" runat="server">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                        ControlToValidate="txtCargo" 
                                        ErrorMessage="Descrição não informado!" ValidationGroup="inserirCargo">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnCancelarCargo"  Text="Cancelar" runat="server" CssClass="botao" 
                                            Width="80px" onclick="btnCancelarCargo_Click"/>
                                    </td>
                                    <td style="text-align:right">
                                        <asp:Button ID="btnSalvarCargo" Text="Salvar" runat="server" CssClass="botao" 
                                            Width="80px" ValidationGroup="inserirCargo" onclick="btnSalvarCargo_Click" />
                                    </td>
                                </tr>
                        </table>
                    </ContentTemplate>
                       <Triggers>
                           <asp:AsyncPostBackTrigger ControlID="imgCargo" EventName="Click" />
                       </Triggers>
                   </asp:UpdatePanel>
                 </div>
        <asp:ValidationSummary ID="vlsCargo" runat="server" ShowMessageBox="true" ShowSummary="false" HeaderText="Os seguintes erros foram encontrados:" ValidationGroup="Inserir" /> 
    </asp:Panel>
     </asp:Content>
