<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="CadastraMateriaPrima.aspx.cs" Inherits="CadastraMateriaPrima" %>

<%@ Register Assembly="RDC.Tools" Namespace="RDC.Tools" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="server">
    <link href="../App_Themes/Theme1/Model1.css" type="text/css" rel="Stylesheet" />
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
        //Objetivo.....: Valida os campos
        //--------------------------------------------------------------------------------
        function ValidaCampos() {
            if (($get('<%=rbNorma.ClientID%>').checked) && ($get('<%=txtNorma.ClientID %>').value != '')) {
                return true;
            }
            if (($get('<%=rbClasseTipo.ClientID%>').checked) && ($get('<%=txtClasseTipo.ClientID%>').value != '')) {
                return true;
            }

            else {
                if ($get('<%=rbNorma.ClientID%>').checked)
                    alert("Informe o Norma.");

                else if ($get('<%=rbClasseTipo.ClientID%>').checked)
                    alert('Informe a Classe/Tipo.');

                return false;
            }
        }
        //--------------------------------------------------------------------------------
        //Criado por...:Alexandre Maximiano - 04/11/2010
        //Objetivo.....: Efetua consulta com o retorno do autocomplete
        //--------------------------------------------------------------------------------
        function CarregarValores(source, eventArgs) {
            $get('<%=hdfCodigo.ClientID%>').value = eventArgs.get_value();
            $get('<%=txtNorma.ClientID %>').value = eventArgs._item.outerText;
            $get('<%=txtClasseTipo.ClientID %>').value = eventArgs._item.outerText;
            $get('<%=btnPesquisar.ClientID%>').click();
        }


        //--------------------------------------------------------------------------------
        //Criado por...: Jacqueline Albuquerque - 24/11/2009
        //Objetivo.....: Verifica o tipo de pesquisa e habilita campos e limpa-los
        //--------------------------------------------------------------------------------
        function TipoPesquisa(tvar) {
            $get('<%=txtNorma.ClientID%>').value = ''
            $get('<%=txtClasseTipo.ClientID%>').value = '';
            $get('divCodigo').style.display = 'none';
            $get('divDescricao').style.display = 'none';
            switch (tvar) {
                case 1: //Pesquisa por Norma
                    $get('divCodigo').style.display = 'block';
                    break;
                case 2: //Pesquisa por Classe Tipo
                    $get('divDescricao').style.display = 'block';
                    break;
            }
        }
        //--------------------------------------------------------------------------------
        //Criado por...: Alexandre Maximiano - 02/11/2009
        //Objetivo.....: Acionar botão acessar quando pressionada a tecla ENTER
        //--------------------------------------------------------------------------------
        function KeyDownHandler() {
            if (event.keyCode == 13) {
                event.returnValue = false;
                event.cancel = true;
                $get('<%=btnPesquisar.ClientID%>').click();
            }
        }
    </script>
    <table style="margin-left: auto; width: 95%; margin-right: auto;">
        <tr>
            <td style="width: 28px">
                <asp:Image ID="ImgCadastro" runat="server" ImageUrl="~/Imagens/layout.png" Height="16px" />
            </td>
            <td class="titulo">
                Cadastro de Materia Prima
            </td>
        </tr>
    </table>
    <br />
    <div style="text-align: center; width: 100%;" id="divConsulta" runat="server">
        <div class="fundoTabela" style="width: 95%">
            <table id="Table1" runat="server" style="text-align: left; width: 100%">
                <tr>
                    <td style="width: 20%; padding-left: 17px">
                        Opções de Consulta:
                    </td>
                    <td style="width: 20%">
                        <asp:RadioButton ID="rbNorma" Checked="true" onclick="TipoPesquisa(1)" GroupName="filtroPesq"
                            runat="server" Text="Norma" CssClass="noBorder" />
                    </td>
                    <td style="width: 20%" colspan="2">
                        <asp:RadioButton ID="rbClasseTipo" onclick="TipoPesquisa(2)" runat="server" Text="Classe/Tipo"
                            GroupName="filtroPesq" CssClass="noBorder" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        &nbsp;
                    </td>
                    <td colspan="2" style="height: 36px">
                        <div id="divCodigo" style="display: block">
                            <asp:TextBox ID="txtNorma" MaxLength="18" onkeypress="KeyDownHandler();" runat="server" Height="16px" Width="150px"></asp:TextBox>
                            <asp:CustomValidator ClientValidationFunction="ValidaCampos" Text="*" CssClass="asterisco"
                                ValidationGroup="pesquisar" ErrorMessage="Favor informar a Norma." runat="server"
                                ID="efvCodigo"></asp:CustomValidator>
                            <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtNorma"
                                MinimumPrefixLength="1" ServiceMethod="GetNorma" CompletionInterval="800" EnableCaching="true"
                                CompletionSetCount="10" OnClientItemSelected="CarregarValores" OnClientPopulated="ClientPopulated">
                            </ajaxToolkit:AutoCompleteExtender>
                        </div>
                        <div id="divDescricao" style="display: none">
                            <asp:TextBox ID="txtClasseTipo" onkeypress="KeyDownHandler();" runat="server" Height="16px" Width="600px"></asp:TextBox>
                            <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="txtClasseTipo"
                                MinimumPrefixLength="1" ServiceMethod="GetClasseTipo" CompletionInterval="800"
                                EnableCaching="true" CompletionSetCount="10" OnClientItemSelected="CarregarValores"
                                OnClientPopulated="ClientPopulated">
                            </ajaxToolkit:AutoCompleteExtender>
                            <asp:CustomValidator Text="*" CssClass="asterisco" ID="cvDescricaoPesq" ValidationGroup="pesquisar"
                                ClientValidationFunction="ValidaCampos" ErrorMessage="Favor informar a Descrição."
                                runat="server"></asp:CustomValidator>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left" colspan="2">
                        <asp:Button ID="Button1" runat="server" OnClick="btnVoltar_Click" CssClass="botao"
                            Text="Voltar" Width="100px" UseSubmitBehavior="False" />
                    </td>
                    <td colspan="2" style="text-align: right">
                        <asp:UpdatePanel ID="updBotoes" runat="server" UpdateMode="Conditional">
                            <contenttemplate>
                        <asp:HiddenField ID="hdfCodigo" runat="server" />
                        <asp:Button ID="btnPesquisar" OnClientClick="return ValidaCampos()" runat="server" ValidationGroup="pesquisar" CssClass="botao"
                            Text="Pesquisar" Width="100px" OnClick="btnPesquisar_Click" />
                            &nbsp;<asp:Button ID="btnIncluir" runat="server" CssClass="botao" Width="100px"
                            Text="Incluir Novo"  onclick="btnIncluir_Click" />
                    </contenttemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
        <br style="line-height: 10px" />
        <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="updGrid">
            <contenttemplate>
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
                                     <asp:BoundField HeaderText="Código" >
                                         <headerstyle wrap="false" CssClass="headerGrid"></headerstyle>
                                         <itemstyle wrap="false" HorizontalAlign="Left"></itemstyle>
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="MateriaPrima" >
                                        <HeaderStyle Wrap="false" CssClass="headerGrid"/>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                </Columns>
                            </cc1:RDCGrid>
                        </div>
        </contenttemplate>
            <triggers>
           <asp:AsyncPostBackTrigger ControlID="btnSalvar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnPesquisar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="grdListaResultado" 
                    EventName="RowCommand" />
        </triggers>
        </asp:UpdatePanel>
    </div>
    <br />
    <ajaxToolkit:ModalPopupExtender ID="mpeIncluirMateriaPrima" runat="server" PopupControlID="pnlIncluirMateriaPrima"
        TargetControlID="hdfTargetIncluirMateriaPrima" BehaviorID="mpeIncluirMateriaPrimaID"
        BackgroundCssClass="modalBackground" DropShadow="true" />
    <asp:Panel ID="pnlIncluirMateriaPrima" runat="server">
        <asp:UpdatePanel runat="server" ID="upCadastro" UpdateMode="Conditional">
            <contenttemplate>
            <div style="text-align: center;width:950px; height:auto; padding: 5px 5px 5px 5px; background-color: #ffffff;">
                <table style="width:100%" class="fundoTabela">
                    <!--TÍTULO DA POPUP-->
                    <tr>
                        <td class="titulo">
                            <b>
                                <% if (hdfTipoAcao.Value == "Incluir")
                                   { %>
                                        ::Inclusão de Materia Prima
                                <% } %>
                                <% else
                                    { %>
                                        ::Alteração de Materia Prima
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
                                    <td style="width: 351px"><b>::Dados cadastrais:</b></td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="padding-left:20px; width: 351px;">Norma:</td>
                                    <td style="padding-left:20px">
                                        Classe/Tipo</td>
                                </tr>
                                <tr>
                                    <td style="width: 351px">
                                        <asp:DropDownList ID="ddlNorma" runat="server" Width="193px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlClasseTipo" runat="server" Width="229px">
                                        </asp:DropDownList>
                                    </td>
                                    </tr>
                                    <tr>
            <td colspan="2" style="padding-left: 20px">
            <ajaxToolkit:TabContainer ID="TabContainer1" CssClass="TabPanelImportar" runat="server"
                ActiveTabIndex="0" Height="400px" Width="910px" AutoPostBack="true">
                <ajaxToolkit:TabPanel ID="tpComposicaoMateriaPrima" runat="server" 
                    HeaderText="Composição Quimica">
                    <HeaderTemplate>
                        Composição Química
                    </HeaderTemplate>
                    <ContentTemplate>
                        <asp:UpdatePanel id="updCadastroComposicaoMateriaPrima" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                         <asp:HiddenField ID="hdfTipoAcaoComposicaoMateriaPrima" runat="server" />
                                               <table stryle="width:910px">
                        <tr>
                        <td align="center">
     
                            <asp:Button ID="btnIncluirComposicaoMateriaPrima" runat="server" 
                                Text="Incluir" CssClass="botao" width="80px" height="20px" 
                                onclick="btngrdIncluirComposicaoMateriaPrima_Click" 
                                UseSubmitBehavior="False"/></td>
                        </tr>
                        </table> 
                        <div id="divCadastroComposicaoMateriaPrima" runat="server" visible="false">
                        <table cellpadding="0" cellspacing="0" class="fundoTabela"  style="height: 300; width: 910px">
                                                <tr>
                        <td align="center">Bitola:</td>
                        <td align="center">C:</td>
                        <td align="center">Si:</td>
                        <td align="center">Mn:</td>
                        <td align="center">P:</td>
                        <td align="center">S:</td>
                        </tr>
                        <tr>
                        <td>
                        <label style="padding-left:17px">Min:</label><label style="padding-left:45px">Max:</label>
                        <br /><asp:TextBox ID="txtBitMin" runat="server" Width="50px"></asp:TextBox>
                             <asp:TextBox ID="txtBitMax" runat="server" Width="50px"></asp:TextBox></td>
                        <td><label style="padding-left:17px">Min:</label><label style="padding-left:45px">Max:</label>
                        <br /><asp:TextBox ID="txtCMin" runat="server" Width="50px"></asp:TextBox>
                            <asp:TextBox ID="txtCMax" runat="server" Width="50px"></asp:TextBox></td>
                        <td><label style="padding-left:17px">Min:</label><label style="padding-left:45px">Max:</label>
                        <br /><asp:TextBox ID="txtSiMin" runat="server" Width="50px"></asp:TextBox>
                            <asp:TextBox ID="TxtSiMax" runat="server" Width="50px"></asp:TextBox></td>
                        <td><label style="padding-left:17px">Min:</label><label style="padding-left:45px">Max:</label>
                        <br /><asp:TextBox ID="txtMnMin" runat="server" Width="50px"></asp:TextBox>
                            <asp:TextBox ID="txtMnMax" runat="server" Width="50px"></asp:TextBox></td>
                        <td><label style="padding-left:17px">Min:</label><label style="padding-left:45px">Max:</label>
                        <br /><asp:TextBox ID="txtPMin" runat="server" Width="50px"></asp:TextBox>
                            <asp:TextBox ID="txtPMax" runat="server" Width="50px"></asp:TextBox></td>
                        <td><label style="padding-left:17px">Min:</label><label style="padding-left:45px">Max:</label>
                        <br /><asp:TextBox ID="txtSMin" runat="server" Width="50px"></asp:TextBox>
                            <asp:TextBox ID="txtSMax" runat="server" Width="50px"></asp:TextBox>
                        </td>
                        <tr>
                        <td align="center">Cr:</td>
                        <td align="center">Ni:</td>
                        <td align="center">Mo:</td>
                        <td align="center">Cu:</td>
                        <td align="center">Ti</td>
                        <td align="center">N2:</td>
                        </tr> 
                            <tr>
                                <td>
                                    <label style="padding-left:17px">Min:</label><label style="padding-left:45px">Max:</label>
                        <br /><asp:TextBox ID="txtCrMin" runat="server" Width="50px"></asp:TextBox>
                                    <asp:TextBox ID="txtCrMax" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>
                                    <label style="padding-left:17px">Min:</label><label style="padding-left:45px">Max:</label>
                        <br /><asp:TextBox ID="txtNiMin" runat="server" Width="50px"></asp:TextBox>
                                    <asp:TextBox ID="txtNiMax" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>
                                    <label style="padding-left:17px">Min:</label><label style="padding-left:45px">Max:</label>
                        <br /><asp:TextBox ID="txtMoMin" runat="server" Width="50px"></asp:TextBox>
                                    <asp:TextBox ID="txtMoMax" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>
                                    <label style="padding-left:17px">Min:</label><label style="padding-left:45px">Max:</label>
                        <br /><asp:TextBox ID="txtCuMin" runat="server" Width="50px"></asp:TextBox>
                                    <asp:TextBox ID="txtCuMax" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>
                                    <label style="padding-left:17px">Min:</label><label style="padding-left:45px">Max:</label>
                        <br /><asp:TextBox ID="txtTiMin" runat="server" Width="50px"></asp:TextBox>
                                    <asp:TextBox ID="txtTiMax" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>
                                    <label style="padding-left:17px">Min:</label><label style="padding-left:45px">Max:</label>
                        <br /><asp:TextBox ID="txtN2Min" runat="server" Width="50px"></asp:TextBox>
                                <asp:TextBox ID="txtN2Max" runat="server" Width="50px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    Co:</td>
                                <td align="center">
                                    Al:</td>
                                <td align="center">
                                    Zn:</td>
                                <td align="center">
                                    Sn:</td>
                                <td align="center">
                                    Pb:</td>
                                <td align="center">
                                    Fe:</td>
                            </tr>
                            <tr>

                        <td><label style="padding-left:17px">Min:</label><label style="padding-left:45px">Max:</label>
                        <br /><asp:TextBox ID="txtCoMin" runat="server" Width="50px"></asp:TextBox>
                            <asp:TextBox ID="txtCoMax" runat="server" Width="50px"></asp:TextBox></td>
                        <td><label style="padding-left:17px">Min:</label><label style="padding-left:45px">Max:</label>
                        <br /><asp:TextBox ID="txtAlMin" runat="server" Width="50px"></asp:TextBox>
                            <asp:TextBox ID="txtAlMax" runat="server" Width="50px"></asp:TextBox></td>
                        <td><label style="padding-left:17px">Min:</label><label style="padding-left:45px">Max:</label>
                        <br /><asp:TextBox ID="txtZnMin" runat="server" Width="50px"></asp:TextBox>
                            <asp:TextBox ID="txtZnMax" runat="server" Width="50px"></asp:TextBox></td>
                        <td><label style="padding-left:17px">Min:</label><label style="padding-left:45px">Max:</label>
                        <br /><asp:TextBox ID="txtSnMin" runat="server" Width="50px"></asp:TextBox>
                            <asp:TextBox ID="txtSnMax" runat="server" Width="50px"></asp:TextBox></td>
                        <td><label style="padding-left:17px">Min:</label><label style="padding-left:45px">Max:</label>
                        <br /><asp:TextBox ID="txtPbMin" runat="server" Width="50px"></asp:TextBox>
                            <asp:TextBox ID="txtPbMax" runat="server" Width="50px"></asp:TextBox></td>
                        <td><label style="padding-left:17px">Min:</label><label style="padding-left:45px">Max:</label>
                        <br /><asp:TextBox ID="txtFeMin" runat="server" Width="50px"></asp:TextBox>
                            <asp:TextBox ID="txtFeMax" runat="server" Width="50px"></asp:TextBox></td>
                            </tr>
                            <tr>
                            <td colspan="6">
                            </td>
                            </tr>
                            <tr>
                            <td colspan="3" style="padding-right:2px;text-align:right">
                            <asp:Button ID="btnCancelarComposicaoMateriaPrima" runat="server" CssClass="botao" 
                                    Height="20px" onclick="btnCancelarComposicaoMateriaPrima_Click" Text="Cancelar" 
                                    Width="80px" />
                            </td>
                                <td colspan="3" style="padding-left:2px;text-align: left">
                                    <asp:Button ID="btnSalvarComposicaoMateriaPrima" runat="server" 
                                        CssClass="botao" Height="20px" OnClick="btnIncluirComposicaoMateriaPrima_Click" 
                                        Text="Salvar" Width="80px" />
                                </td>
                            </tr>
                        </table>
                        </div>
                            </ContentTemplate>
                            <triggers>
<asp:AsyncPostBackTrigger 
                                ControlID="grdComposicaoMateriaPrima" EventName="RowCommand" />
</triggers>
                        </asp:UpdatePanel>
                                    <asp:UpdatePanel ID="updComposicaoMateriaPrima" runat="server" 
                                        UpdateMode="Conditional">
                                        <ContentTemplate>
                        <div id="divgrdComposicaoMateriaPrima" runat="server">
                        <table cellpadding="0" cellspacing="0" style="height: 300; width: 900px">
                            <tr>
                                <td>
                                            <cc1:RDCGrid ID="grdComposicaoMateriaPrima" runat="server" AllowPaging="True" 
                                                autogeneratecolumns="False" bordercolor="Black" borderwidth="1px" 
                                                cellpadding="1" cellspacing="3" CssClass="alinhamento" 
                                                EnableModelValidation="True" gridlines="None" MultiSelection="True" 
                                                onpageindexchanging="grdComposicaoMateriaPrima_PageIndexChanging" 
                                                onrowcommand="grdComposicaoMateriaPrima_RowCommand" 
                                                onrowdatabound="grdComposicaoMateriaPrima_RowDataBound" ShowFooter="True" 
                                                ShowHeaderCheckBoxColumn="False" ShowOptionColumn="False" 
                                                showpagedetails="True" Width="100%">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Ações">
                                                                        <itemtemplate>
                                                                            <asp:ImageButton ID="imgEditar" runat="server" Height="15px" 
                                                                ImageUrl="~/Imagens/editar.png" Width="15px" />
                                                                            <asp:ImageButton ID="imgExcluir" runat="server" Height="15px" 
                                                                ImageUrl="~/Imagens/exclusao_Canc.png" Width="15px" />
                                                                        </itemtemplate>
                                                                        <HeaderStyle CssClass="headerGrid" />
                                                                        <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField HeaderText="Bitola">
                                                                        <HeaderStyle CssClass="headerGrid" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="8%" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField HeaderText="C">
                                                                        <HeaderStyle CssClass="headerGrid" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="8%" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField HeaderText="Si">
                                                                        <HeaderStyle CssClass="headerGrid" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="8%" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField HeaderText="Mn">
                                                                        <HeaderStyle CssClass="headerGrid" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="8%" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField HeaderText="P">
                                                                        <HeaderStyle CssClass="headerGrid" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="8%" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField HeaderText="S">
                                                                        <HeaderStyle CssClass="headerGrid" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="8%" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField HeaderText="Cr">
                                                                        <HeaderStyle CssClass="headerGrid" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField HeaderText="Ni ">
                                                                        <HeaderStyle CssClass="headerGrid" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="8%" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField HeaderText="Mo">
                                                                        <HeaderStyle CssClass="headerGrid" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="8%" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField HeaderText="Cu">
                                                                        <HeaderStyle CssClass="headerGrid" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="8%" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField HeaderText="Ti">
                                                                        <HeaderStyle CssClass="headerGrid" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="8%" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField HeaderText="N2">
                                                                        <HeaderStyle CssClass="headerGrid" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="8%" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField HeaderText="Co">
                                                                        <HeaderStyle CssClass="headerGrid" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="8%" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField HeaderText="Al">
                                                                        <HeaderStyle CssClass="headerGrid" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="8%" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField HeaderText="Zn">
                                                                        <HeaderStyle CssClass="headerGrid" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="8%" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField HeaderText="Sn">
                                                                        <HeaderStyle CssClass="headerGrid" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="8%" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField HeaderText="Pb">
                                                                        <HeaderStyle CssClass="headerGrid" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="8%" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField HeaderText="Fe">
                                                                        <HeaderStyle CssClass="headerGrid" />
                                                                        <ItemStyle HorizontalAlign="Left" Width="8%" />
                                                                    </asp:BoundField>
                                                                </Columns>
                                                            </cc1:RDCGrid>
                                </td>
                            </tr>
                        </tr></table>
                                                </div>
                                       </ContentTemplate>
                          
                                        <triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnSalvarComposicaoMateriaPrima" 
                                            EventName="Click" >
                                            </asp:AsyncPostBackTrigger>
                                            <asp:AsyncPostBackTrigger ControlID="btnIncluirComposicaoMateriaPrima" 
                                            EventName="Click" >
                                            </asp:AsyncPostBackTrigger>
                                            <asp:AsyncPostBackTrigger ControlID="grdComposicaoMateriaPrima" 
                                            EventName="RowCommand">
                                            </asp:AsyncPostBackTrigger>
                                            <asp:AsyncPostBackTrigger ControlID="btnCancelarComposicaoMateriaPrima" 
                                            EventName="Click" >
                                            </asp:AsyncPostBackTrigger>
                                        </triggers>
                                    </asp:UpdatePanel>
                       
                    </ContentTemplate>
                
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel ID="tpResistenciaTracao" runat="server" 
                    HeaderText="Resistência a Tração">
                    <HeaderTemplate>
                        Resistência a Tração
                    </HeaderTemplate>
                    <ContentTemplate>
                   <asp:UpdatePanel id="updResistenciaTracaoEdit" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                        <table style="width:100%" class="fundoTabela">

                            <tr>
                                <td style="padding-left: 17px">
                                    Bitola:</td>
                                <td style="padding-left: 17px">
                                    Tolerância:
                                </td>
                                <td style="padding-left: 17px">
                                    Mínimo:
                                </td>
                                <td style="padding-left: 17px; width: 110px;">
                                    Máxima:
                                </td>
                                <td style="padding-left: 17px">
                                    &#160;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddlBitola" runat="server" Width="104px">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTolerancia" runat="server" ></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtMinimo" runat="server" ></asp:TextBox>
                                </td>
                                <td style="width: 110px">
                                    <asp:TextBox ID="txtMaximo" runat="server" ></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btnIncluirResistenciaTracao" runat="server" CssClass="botao" 
                                        OnClick="btnIncluirResistenciaTracao_Click" Text="Incluir" 
                                        ValidationGroup="ValidarResistenciaTracao" Width="80px" />
                                </td>
                                <tr>
                                    <td colspan="5" style="padding-left: 17px">
                                        &#160;</td>
                                </tr>
                                </table>
                            </ContentTemplate>
                                <triggers>
<asp:AsyncPostBackTrigger ControlID="grdResistenciaTracao" 
                                    EventName="RowCommand" />
    <asp:AsyncPostBackTrigger ControlID="btnIncluirResistenciaTracao" EventName="Click" >
    </asp:AsyncPostBackTrigger>
</triggers>
                                </asp:UpdatePanel>
                                <table style="width:100%" class="fundoTabela">
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="updResistenciaTracao" runat="server" 
                                            UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <cc1:RDCGrid ID="grdResistenciaTracao" runat="server" AllowPaging="True" 
                                                    AutoGenerateColumns="False" BorderColor="Black" BorderWidth="1px" 
                                                    CellPadding="1" CellSpacing="3" CssClass="alinhamento" 
                                                    EnableModelValidation="True" GridLines="None" MultiSelection="True" 
                                                    OnPageIndexChanging="grdResistenciaTracao_PageIndexChanging" 
                                                    OnRowCommand="grdResistenciaTracao_RowCommand" 
                                                    OnRowDataBound="grdResistenciaTracao_RowDataBound" PageSize="8" 
                                                    ShowFooter="True" ShowHeaderCheckBoxColumn="False" ShowOptionColumn="False" 
                                                    ShowPageDetails="True" Width="100%" Height="300px">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Ações">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imgEditarResistenciaTracao" runat="server" Height="15px" 
                                                                ImageUrl="~/Imagens/editar.png" Width="15px" />
                                                                        <asp:ImageButton ID="imgExcluirResistenciaTracao" runat="server" Height="15px" 
                                                                ImageUrl="~/Imagens/exclusao_Canc.png" Width="15px" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle CssClass="headerGrid" Width="5%" />
                                                                    <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="Bitola">
                                                                    <HeaderStyle CssClass="headerGrid" />
                                                                    <ItemStyle HorizontalAlign="Left" Width="8%" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="Tolerância">
                                                                    <HeaderStyle CssClass="headerGrid" />
                                                                    <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="Mínima">
                                                                    <HeaderStyle CssClass="headerGrid" />
                                                                    <ItemStyle HorizontalAlign="Left" Width="7%" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="Máxima">
                                                                    <HeaderStyle CssClass="headerGrid" />
                                                                    <ItemStyle HorizontalAlign="Left" Width="7%" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                        </cc1:RDCGrid>
                                                <asp:HiddenField ID="hdfTipoAcaoResistenciaTracao" runat="server" />
                                                <asp:HiddenField ID="hdfCodDuplicata" runat="server" />
                                            </ContentTemplate>
                                            
                                            <triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnIncluirResistenciaTracao" 
                                                EventName="Click">
                                                        </asp:AsyncPostBackTrigger>
                                                        <asp:AsyncPostBackTrigger ControlID="grdResistenciaTracao" 
                                                EventName="RowCommand">
                                                        </asp:AsyncPostBackTrigger>
                                                    </triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td>
                                        &#160;</td>
                                </tr>
                            </tr>
                        </table>
                    </ContentTemplate>
                
                </ajaxToolkit:TabPanel>
            </ajaxToolkit:TabContainer>
        </td>
                                    </tr>
                            </table>
                            <asp:UpdatePanel ID="upBotoesCadastro" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table style="width:100%" class="fundoTabela">
                                        <tr>
                                            <td style="width: 367px">
                                                <asp:Button ID="btnCancelar" runat="server" CssClass="botao" 
                                                    onclick="btnCancelar_Click" Text="Cancelar" Width="80px" 
                                                    UseSubmitBehavior="False" />
                                            </td>
                                            <td  style="text-align:right">
                                                <asp:Button ID="btnSalvar" runat="server" CssClass="botao" 
                                                onclick="btnSalvar_Click" Text="Salvar" ValidationGroup="cadastro" Width="80px" />
                                           </td>
                                           
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <asp:HiddenField ID="hdfCodMateriaPrima" runat="server" />
                </table>
            </div>
            <asp:HiddenField ID="hdfTipoAcao" runat="server" />
        </contenttemplate>
            <triggers>
            <asp:AsyncPostBackTrigger ControlID="btnIncluir" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="grdListaResultado" 
                EventName="RowCommand" />
        </triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:HiddenField ID="hdfCadastroPopup" runat="server" />
    <asp:HiddenField ID="hdfTargetIncluirMateriaPrima" runat="server" />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" ValidationGroup="cadastro" />
</asp:Content>
