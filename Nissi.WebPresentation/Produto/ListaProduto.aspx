<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="ListaProduto.aspx.cs" Inherits="Nissi.WebPresentation.Produto.ListaProduto" %>

<%@ Register Assembly="RDC.Tools" Namespace="RDC.Tools" TagPrefix="cc1" %>
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

        function ValidaDataPesquisa(src, args) {
            var CustonValidator;
            //Custon Validator
            CustonValidator = $get('<%=ctvValidaDataPesquisa.ClientID %>');
                var strDataInicial;
                var strDataFinal;
                var DataInicialValida;
                var DataFinalValida;

                //Guarda valores dos textbox
                strDataInicial = Trim($get('<%=tbxDataIni.ClientID %>').value);
                strDataFinal = Trim($get('<%=tbxDataFim.ClientID %>').value);
                //Guarda verificações de data
                DataInicialValida = isDate(strDataInicial);
                DataFinalValida = isDate(strDataFinal);

                //Verifica se os Data Inicial e Final estão preenchidos
                if ((strDataInicial != '') && (strDataFinal != '')) {
                    //Se estão preenchidos e são datas, data inicial deve ser menor que final
                    CustonValidator.errormessage = "Período final deve ser maior que Período inicial";
                    args.IsValid = CompareDates(strDataInicial, strDataFinal);
                    return;
                }
                else if (strDataInicial == '') {
                    CustonValidator.errormessage = "Período inicial deve ser preenchido";
                    args.IsValid = false;
                    return;
                }
                else if (strDataFinal == '') {
                    CustonValidator.errormessage = "Período final deve ser preenchido";
                    args.IsValid = false;
                    return;
                }
        }

        function ValidaCampo(src, args) {
            var rbCodigo = $get("<%=rbCodigo.ClientID%>");
            var rbDescricao = $get("<%=rbDescricao.ClientID%>");
            var txtCodigoDescricao = $get("<%=txtCodigoDescricao.ClientID%>");
            var CustomValidator = $get("<%=cvValidaTexto.ClientID %>");
            if ((rbCodigo.checked)&&(txtCodigoDescricao.value == '')){
                CustomValidator.errormessage="Código deve ser preenchido";
                args.IsValid = false;
                return;
            }
            if ((rbDescricao.checked)&&(txtCodigoDescricao.value == '')){
                CustomValidator.errormessage="Descrição deve ser preenchido";
                args.IsValid = false;
                return;
            }
            args.IsValid = true;
        }
        function Limpar()
        {
            $get("<%=txtCodigoDescricao.ClientID %>").value = '';
        }
    </script>
    <table style="margin-left: auto; width: 95%; margin-right: auto;">
        <tr>
            <td style="width: 21px; text-align: left">
                <asp:Image ID="ImgCadastro" runat="server" ImageUrl="~/Imagens/layout.png" />
            </td>
            <td style="width: 95%; text-align: left" class="titulo">
                Lista de Produtos
            </td>
        </tr>
    </table>
    <br />
    <div style="text-align: center;">
        <table id="tableProduto" class="fundoTabela" runat="server" style="margin-left: auto;
            margin-right: auto; width: 95%">
            <tr>
                <td class="tituloCampo" style="height: 12px">
                    <asp:RadioButton ID="rbCodigo" runat="server" onclick="Limpar()" Text="Código" Checked="True"
                        GroupName="opcao" />
                    <asp:RadioButton ID="rbDescricao" onclick="Limpar()" runat="server" Text="Descrição"
                        GroupName="opcao" />
                </td>
                <td class="tituloCampo" style="height: 12px">
                    Período
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtCodigoDescricao" runat="server"></asp:TextBox>
                    <asp:CustomValidator ID="cvValidaTexto" runat="server" Text="*" ErrorMessage="Código Inválido" ValidationGroup="ValidaDados" ClientValidationFunction="ValidaCampo" CssClass="asterisco"></asp:CustomValidator>
                </td>
                <td class="tituloCampo" style="height: 23px">
                    <b>De:</b>
                    <asp:TextBox ID="tbxDataIni" onkeypress="formatar(this, '##/##/####');OnlyNumbers()"
                        MaxLength="10" runat="server" CssClass="formNovo" Width="75px"></asp:TextBox>
                    <img alt="" id="imgDataIni" align="absmiddle" style="cursor: pointer;" src="../Imagens/Calendar_scheduleHS.png" />
                    <asp:RegularExpressionValidator ControlToValidate="tbxDataIni" Text="*" CssClass="asterisco"
                        ID="RegularExpressionValidator3" ErrorMessage="Período inicial Inválido." ValidationGroup="ValidaDados"
                        runat="server" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"></asp:RegularExpressionValidator>
                    <ajaxToolkit:CalendarExtender Format="dd/MM/yyyy" ID="CalendarExtender1" PopupButtonID="imgDataIni"
                        runat="server" TargetControlID="tbxDataIni" Animated="true">
                    </ajaxToolkit:CalendarExtender>
                    &nbsp; &nbsp; <b>Até:</b>
                    <asp:TextBox ID="tbxDataFim" runat="server" onkeypress="formatar(this, '##/##/####');OnlyNumbers()"
                        MaxLength="10" CssClass="formNovo" TabIndex="12" Width="75px"></asp:TextBox>
                    <img alt="" align="absmiddle" id="imgDataFim" style="cursor: pointer" src="../Imagens/Calendar_scheduleHS.png" />
                    <asp:RegularExpressionValidator ControlToValidate="tbxDataFim" Text="*" CssClass="asterisco"
                        ID="RegularExpressionValidator4" ErrorMessage="Período final Inválido." ValidationGroup="ValidaDados"
                        runat="server" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"></asp:RegularExpressionValidator>
                    <ajaxToolkit:CalendarExtender Format="dd/MM/yyyy" ID="CalendarExtender2" PopupButtonID="imgDataFim"
                        runat="server" TargetControlID="tbxDataFim" Animated="true">
                    </ajaxToolkit:CalendarExtender>
                    <asp:CustomValidator ID="ctvValidaDataPesquisa" runat="server" ClientValidationFunction="ValidaDataPesquisa"
                        ValidationGroup="ValidaDados" ErrorMessage="Datas inválidas" CssClass="asterisco">*</asp:CustomValidator>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:Button ID="btnSair" runat="server" CssClass="botao" Text="Sair" Width="73px" />
                </td>
                <td style="text-align: right">
                    <asp:Button ID="btnPesquisar" runat="server" CssClass="botao" Text="Pesquisar" ValidationGroup="ValidaDados"
                        OnClick="btnPesquisar_Click" />
                </td>
            </tr>
        </table>
        <br />
        <asp:UpdatePanel ID="udpListaResultado" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <cc1:RDCGrid ID="grdListaResultado" runat="server" AutoGenerateColumns="False" BorderColor="Black"
                    BorderWidth="1px" CellPadding="1" CellSpacing="3" GridLines="None" PageSize="15"
                    ShowPageDetails="True" AllowPaging="True" MultiSelection="True" ShowHeaderCheckBoxColumn="False"
                    ShowOptionColumn="False" CssClass="alinhamento" OnPageIndexChanging="grdListaResultado_PageIndexChanging"
                    OnRowCommand="grdListaResultado_RowCommand" OnRowDataBound="grdListaResultado_RowDataBound"
                    EnableModelValidation="True" Width="95%" ShowFooter="True">
                    <Columns>
                        <asp:TemplateField HeaderText="Ações">
                            <ItemTemplate>
                                <asp:ImageButton Style="cursor: pointer" ID="imgVisualizar" Width="15px" Height="15px"
                                    runat="server" ImageUrl="~/Imagens/btn-SolicitacaoDocumentos.gif" />
                            </ItemTemplate>
                            <HeaderStyle CssClass="headerGrid" Width="5%" />
                            <ItemStyle HorizontalAlign="center" Wrap="false" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="NF">
                            <HeaderStyle CssClass="headerGrid" />
                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Data de Emissao">
                            <HeaderStyle CssClass="headerGrid" />
                            <ItemStyle HorizontalAlign="Left" Width="13%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Codigo">
                            <HeaderStyle CssClass="headerGrid" />
                            <ItemStyle Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Descricao">
                            <HeaderStyle CssClass="headerGrid" />
                            <ItemStyle HorizontalAlign="Left" Width="35%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Qtde">
                            <HeaderStyle CssClass="headerGrid" />
                            <ItemStyle Width="5%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Valor" DataFormatString="{0:c}">
                            <ItemStyle HorizontalAlign="Right" Width="13%" />
                            <HeaderStyle CssClass="headerGrid" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Total do Item" DataFormatString="{0:c}">
                            <HeaderStyle CssClass="headerGrid" />
                            <ItemStyle HorizontalAlign="Right" Width="30%" />
                        </asp:BoundField>
                    </Columns>
                </cc1:RDCGrid>
                <asp:HiddenField ID="hdfCodNF" runat="server" />
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnPesquisar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <div>
            <asp:ValidationSummary ID="vtsListaProduto" runat="server" ValidationGroup="ValidaDados" ShowSummary="False" ShowMessageBox="True" />
        </div>
    </div>
</asp:Content>
