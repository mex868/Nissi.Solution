<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ListaNFe.aspx.cs" Inherits="Nissi.WebPresentation.NFe.ListaNFe" %>
<%@ Register assembly="RDC.Tools" namespace="RDC.Tools" tagprefix="cc1" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
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
            $get('<%=txtNF.ClientID%>').value = ''
            $get('<%=txtDataEmissao.ClientID%>').value = '';
            $get('<%=txtRazaoSocial.ClientID%>').value = '';
            $get('divNF').style.display = 'none';
            $get('divDataEmissao').style.display = 'none';
            $get('divRazaoSocial').style.display = 'none';
            switch (tvar) {
                case 1: //Pesquisa por NF
                    $get('divNF').style.display = 'block';

                    break;
                case 2: //Pesquisa por Data de Emissao
                        $get('divDataEmissao').style.display = 'block';
                    break;
                case 3: //Pesquisa por Razao Social
                    $get('divRazaoSocial').style.display = 'block';
                    break;
            }
        }

            //--------------------------------------------------------------------------------
            //Criado por...: Jacqueline Albuquerque - 24/11/2009
            //Objetivo.....: Valida os campos
            //--------------------------------------------------------------------------------
        function ValidaCampos() {
            if (($get('<%=rbNF.ClientID%>').checked) && ($get('<%=txtNF.ClientID %>').value != '')) {
               return true;
           }
           if (($get('<%=rbDataEmissao.ClientID%>').checked) && ($get('<%=txtDataEmissao.ClientID %>').value != '')) {
               return true;
           }
           if (($get('<%=rbRazaoSocial.ClientID%>').checked) && ($get('<%=txtRazaoSocial.ClientID%>').value != '')) {
               return true;
           }
            else {
                if ($get('<%=rbNF.ClientID%>').checked) {
                    alert("Informe a N.F.");
                    return false;
                }
                    if ($get('<%=rbDataEmissao.ClientID%>').checked) {
                        alert("Informe a Data de Emissao.");
                    }
                    else if ($get('<%=rbRazaoSocial.ClientID%>').checked)
                        alert('Informe a Razão Social do Cliente.');
                }
                return false;
            }

            function ChamaDuplicata(tvar) {
                window.open("../Relatorios/relDuplicata.aspx?CodNF="+tvar+"", "_blank", "top=0,left=0,width=800,height=600,scrollbars=yes,resizable=no,toolbar=no");
            }
</script>
    <table style="margin-left: auto; width: 95%; margin-right: auto;">
        <tr>
            <td style="width: 21px; text-align:left">
                <asp:Image ID="ImgCadastro" runat="server" ImageUrl="~/Imagens/layout.png" />
            </td>
            <td style="width: 95%; text-align: left" class="titulo">
                Cadastro de Nota Fiscal</td>
        </tr>
    </table>
    <br />
            <table class="fundoTabela" style="text-align:left; width:95%">
                <tr>
                    <td style="width: 20%;padding-left:17px"> Opções de Consulta:</td>
                    <td style="width: 20%">
                        <asp:RadioButton ID="rbNF" onclick ="TipoPesquisa(1)"  GroupName="filtroPesq" 
                            runat="server" Text="NF" Checked="True" CssClass="noBorder" />
                    </td>
                    <td style="width: 20%">
                        <asp:RadioButton ID="rbDataEmissao" onclick ="TipoPesquisa(2)"  
                            GroupName="filtroPesq" runat="server" Text="Data de Emissão" 
                            CssClass="noBorder" />
                    </td>
                    <td style="width: 20%">
                        <asp:RadioButton ID="rbRazaoSocial" onclick ="TipoPesquisa(3)" runat="server" 
                            Text="Razão Social" GroupName="filtroPesq" CssClass="noBorder" />
                    </td>
                    <td style="width: 20%">
                        <asp:RadioButton ID="rbNaoEnviadas" onclick ="TipoPesquisa(4)" runat="server" 
                            Text="Não Enviadas" GroupName="filtroPesq" CssClass="noBorder" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">&nbsp;</td>
                    <td colspan="4">
                        <div id="divNF"  style="display:block">
                            <asp:TextBox ID="txtNF" MaxLength="14" 
                                  onkeypress="return digitos(event, this);" runat="server" Height="16px" Width="96px"></asp:TextBox>
                            <asp:CustomValidator ClientValidationFunction="ValidaCampos" Text="*" CssClass="asterisco" ValidationGroup="pesquisar" ErrorMessage="Favor informar o C�digo" runat="server" id="CustomValidator1"></asp:CustomValidator>
                        </div>
                        <div id="divDataEmissao"  style="display:none">
                            <asp:TextBox ID="txtDataEmissao" MaxLength="10" onkeypress="return digitos(event, this);" onkeyup="Mascara('DATA',this,event);" runat="server" Height="16px" Width="120px"></asp:TextBox>
                            <asp:CustomValidator ClientValidationFunction="ValidaCampos" Text="*" CssClass="asterisco" ValidationGroup="pesquisar" ErrorMessage="Favor informar o C.N.P.J." runat="server" id="efvCNPJ"></asp:CustomValidator>
                        </div>
                        <div id="divRazaoSocial"  style="display:none">
                            <asp:TextBox ID="txtRazaoSocial" MaxLength="50" onkeypress="ConverterCaixaAlta()"  runat="server" Height="16px" Width="300px"></asp:TextBox>
                            <asp:CustomValidator ClientValidationFunction="ValidaCampos" Text="*" CssClass="asterisco" ValidationGroup="pesquisar" ErrorMessage="Favor informar o CPF" runat="server" id="CustomValidator2"></asp:CustomValidator>
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
                                    Text="Incluir Novo" onclick="btnIncluir_Click" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
            <asp:UpdatePanel ID="udpListaResultado" runat="server" UpdateMode="Conditional"> 
                  <ContentTemplate>
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
                        ShowHeaderCheckBoxColumn="False" 
                        ShowOptionColumn="False" 
                        CssClass="alinhamento" 
                        onpageindexchanging="grdListaResultado_PageIndexChanging" 
                        onrowcommand="grdListaResultado_RowCommand" 
                        onrowdatabound="grdListaResultado_RowDataBound" EnableModelValidation="True">
                        <Columns>
                            <asp:TemplateField HeaderText="Ações">
                                <itemtemplate>
                                    <asp:ImageButton ID="imgEditar" Width="15px" Height="15px" runat="server" ImageUrl="~/Imagens/editar.png" />
                                    <asp:ImageButton ID="imgExcluir" Width="15px" Height="15px" runat="server" ImageUrl="~/Imagens/exclusao_Canc.png" style="margin-right: 0px" />
                                    <asp:ImageButton style="cursor:pointer" ID="imgDuplicata" Width="15px" Height="15px" runat="server" ImageUrl="~/Imagens/btn-SolicitacaoDocumentos.gif" />
                                </itemtemplate>
                                <HeaderStyle CssClass="headerGrid" Width="5%" />
                                <ItemStyle HorizontalAlign="center" Wrap="false"/>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Série" >
                                <HeaderStyle CssClass="headerGrid" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="NF">
                                <HeaderStyle CssClass="headerGrid" />
                                <ItemStyle HorizontalAlign="Left" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Data de Emissao">
                                <HeaderStyle CssClass="headerGrid" />
                                <ItemStyle HorizontalAlign="Left" Width="13%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Razão Social">
                                <HeaderStyle CssClass="headerGrid"  />
                                <ItemStyle HorizontalAlign="Left" Width="30%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Nome Fantasia" >
                                <ItemStyle HorizontalAlign="Left"  Width="25%" />
                                <HeaderStyle CssClass="headerGrid"  />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Chave NF-e" >
                                <HeaderStyle CssClass="headerGrid" />
                               <ItemStyle HorizontalAlign="Left"  Width="30%" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgStatus" runat="server" Height="15px" 
                                        ImageUrl="~/Imagens/NFeOk.png" Width="15px" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <HeaderStyle CssClass="headerGrid" />
                            </asp:TemplateField>
                        </Columns>
                    </cc1:RDCGrid>
                      <asp:HiddenField ID="hdfCodNF" runat="server" />
                  </ContentTemplate>
                  <Triggers>
                      <asp:AsyncPostBackTrigger ControlID="btnPesquisar" EventName="Click" />
                  </Triggers>
            </asp:UpdatePanel>
                            
        </asp:Content>
