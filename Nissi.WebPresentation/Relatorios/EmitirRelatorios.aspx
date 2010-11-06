<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.Master" CodeBehind="EmitirRelatorios.aspx.cs" Inherits="Nissi.WebPresentation.Relatorios.EmitirRelatorios" %>
<asp:Content runat="server" ContentPlaceHolderID="cphPrincipal">
<link href="../App_Themes/Theme1/Model1.css" type="text/css"  rel="Stylesheet" />
<script type="text/javascript" src="../JScripts/Common.js"></script>
<script type="text/javascript">
    function ValidaDataPesquisa(src, args) {
        var CustonValidator;
        //Custon Validator

        CustonValidator = $get('<%=ctvValidaDataPesquisa.ClientID %>');
        if ($get('<%=ddlRelatorio.ClientID %>').value == "2") {
            var strDataInicial;
            var strDataFinal;
            var DataInicialValida;
            var DataFinalValida;

            //Guarda valores dos textbox
            strDataInicial = Trim($get('<%=tbxPeriodoInicial.ClientID %>').value);
            strDataFinal = Trim($get('<%=tbxPeriodoFinal.ClientID %>').value);
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
            else if (strDataInicial != '') {
                CustonValidator.errormessage = "Período final deve ser preenchido";
                args.IsValid = false;
                return;
            }
            else if (strDataFinal != '') {
                CustonValidator.errormessage = "Período inicial deve ser preenchido";
                
                return;
            }
        }
        /*else {
            var inicio = $get('<%=txtInicio.ClientID %>');
            var fim = $get('<%=txtFim.ClientID %>');
            if (inicio.value != "" || fim.value != "") {
                if (inicio.value == "") {
                    CustonValidator.errormessage = 'Favor informar o valor inicial';
                    inicio.focus();
                    args.IsValid = false;
                    return;
                }
                else if (fim.value == "") {
                    CustonValidator.errormessage = 'Favor informar o valor final';
                    fim.focus();
                    args.IsValid = false;
                    retu;

                }
                else if (inicio.value != "" && fim.value != "") {
                    if (inicio.value > fim) {
                        inicio.focus();
                        CustonValidator.errormessage = 'A faixa inicial não pode ser maior que a final';
                        args.IsValid = false;
                        return;
                    }
                }
            }
        }*/
    }

    function AbrirRelatorio(urlRelatorio, arrParametros) {
        var parametros = "";
        var _arr = arrParametros.split('|');

        for (var i = 0; i < _arr.length; i++) {
            var _arr1 = _arr[i].split('!');
            if (parametros != "")
                parametros += "&";
            parametros += _arr1[0] + "=" + _arr1[1];

        }
        window.open(urlRelatorio + parametros, "_blank", "top=0,left=0,width=800,height=600,scrollbars=yes,resizable=no,toolbar=no");
    }

    function Limpar(tvar) {
        if (tvar == "1") {
            $get('<%=txtInicio.ClientID%>').value = "";
            $get('<%=txtFim.ClientID%>').value = "";
        }
        else {
            $get('<%=txtCodIni.ClientID%>').value = "";
            $get('<%=txtCodFim.ClientID%>').value = "";
        }
    }

    function Habilita(tvar) {

        if (tvar == 1) {
            document.getElementById("ctl00_cphPrincipal_tdDescricao").style.display = "block";
            document.getElementById("ctl00_cphPrincipal_tdCodigo").style.display = "none";
            document.getElementById("<%=txtCodigo.ClientID %>").value = "";
        }
        else if(tvar == 2) {
            document.getElementById("ctl00_cphPrincipal_tdCodigo").style.display = "block";
            document.getElementById("ctl00_cphPrincipal_tdDescricao").style.display = "none";
            document.getElementById("<%=txtDescricao.ClientID %>").value = "";
        }


    }

    //--------------------------------------------------------------------------------
    //Criado por...:Alexandre Maximiano - 05/11/2010
    //Objetivo.....: Efetua consulta com o retorno do autocomplete
    //--------------------------------------------------------------------------------
    function CarregarValoresIni(source, eventArgs) {
        $get('<%=hdfIdRazaoSocialIni.ClientID%>').value = eventArgs.get_value();
    }
    //--------------------------------------------------------------------------------
    //Criado por...:Alexandre Maximiano - 05/11/2010
    //Objetivo.....: Efetua consulta com o retorno do autocomplete
    //--------------------------------------------------------------------------------
    function CarregarValoresFim(source, eventArgs) {
        $get('<%=hdfIdRazaoSocialFim.ClientID%>').value = eventArgs.get_value();
    }
 
</script>
<table style="margin-left: auto; width:95%; margin-right: auto;">
    <tr>
        <td style="width:28px">
            <asp:Image ID="ImgCadastro" runat="server" ImageUrl="~/Imagens/layout.png" Height="16px" />
        </td>
        <td class="titulo">Emissão de Relatórios</td>
    </tr>
</table>
<br />
<table id="tblConsulta" runat="server" style="text-align:left; width:98%; height:100%" class="fundoTabela">
   <tr>
       <td valign="top">
            <table width="100%" style="height:250px" >
                <tr>
                    <td style="padding-left:20px; height: 20px;">Tipos de Relatórios</td>
                </tr>
                <tr>
                    <td style="height: 20px" >
                        <asp:UpdatePanel runat="server" ID="upCombo">
                            <ContentTemplate>
                                <asp:DropDownList AutoPostBack="true" Width="150px" runat="server" ID="ddlRelatorio" 
                                    onselectedindexchanged="ddlRelatorio_SelectedIndexChanged">
                                    <asp:ListItem Text="Selecione" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Clientes" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Nota Fiscal" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Fornecedor" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Transportadora" Value="4"></asp:ListItem>
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td valign="bottom">
                        <asp:Button runat="server" ID="btnGerar" ValidationGroup="ValidaDados" CssClass="botao" Text="Gerar" onclick="btnGerar_Click" />
                        <asp:HiddenField ID="hdfCFOP" runat="server" />
                    </td>
                </tr>
             </table>
        </td>
        <td valign="top">
            <table width="100%">
                <tr>
                    <td>
                       <asp:UpdatePanel runat="server" ID="upDivs">
                       <Triggers>
                           <asp:AsyncPostBackTrigger ControlID="ddlRelatorio" />
                        </Triggers>
                            <ContentTemplate>
                            <asp:HiddenField ID="hdfIdRazaoSocialIni" runat="server" />
                            <asp:HiddenField ID="hdfIdRazaoSocialFim" runat="server" />
                                   <table id="tableVarios" runat="server" style="display:none" >
                                        <tr>
                                            <td runat="server" id="tbRadioCliente">
                                                <asp:RadioButton runat="server" onclick="Limpar(1)"  ID="rbtRazao" AutoPostBack="true"  Checked="true" GroupName="tipo" 
                                                    Text="Razão Social" oncheckedchanged="ddlRelatorio_SelectedIndexChanged" />
                                                 <asp:RadioButton onclick="Limpar(2)"  AutoPostBack="true" runat="server" ID="rbtCod"  Text="Código" GroupName="tipo" 
                                                    oncheckedchanged="ddlRelatorio_SelectedIndexChanged" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left:20px" id="tdRazao1" runat="server">Razão Social De</td>
                                            <td style="padding-left:20px" id="tdRazao2" runat="server">Razão Social Até</td>
                                            <td style="padding-left:20px" id="tdCod1" runat="server">Código Inicial </td>
                                            <td style="padding-left:20px" id="tdCod2" runat="server">Código Final</td>
                                            <td style="padding-left:20px" id="tdUf1" runat="server">UF </td>
                                        </tr>
                                        <tr>
                                            <td id="tdRazao3" runat="server"><asp:TextBox runat="server" ID="txtInicio"></asp:TextBox>
                                                <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtInicio"
                                                MinimumPrefixLength="1" ServiceMethod="GetNames" CompletionInterval="800" EnableCaching="true"
                                                CompletionSetCount="10" OnClientItemSelected="CarregarValoresIni" OnClientPopulated="ClientPopulated">
                                                </ajaxToolkit:AutoCompleteExtender>
                                            </td>
                                            <td id="tdRazao4" runat="server"><asp:TextBox runat="server" ID="txtFim"></asp:TextBox>
                                                <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="txtFim"
                                                MinimumPrefixLength="1" ServiceMethod="GetNames" CompletionInterval="800" EnableCaching="true"
                                                CompletionSetCount="10" OnClientItemSelected="CarregarValoresFim" OnClientPopulated="ClientPopulated">
                                                </ajaxToolkit:AutoCompleteExtender>
                                            </td>
                                            <td id="tdCod3" runat="server"><asp:TextBox runat="server" ID="txtCodIni"></asp:TextBox></td>
                                            <td id="tdCod4" runat="server"><asp:TextBox runat="server" ID="txtCodFim"></asp:TextBox></td>
                                            <td id="tdUf2" runat="server"><asp:DropDownList runat="server" ID="ddlUF"></asp:DropDownList></td>
                                        </tr>
                                    </table>

                                    <table id="tableNotaFiscal" runat="server" style="display:none" >
                                        <tr>
                                            <td class="tituloCampo" style="height: 12px">Período</td>
                                            <td class="tituloCampo" style="height: 12px;padding-left:18px">Região</td>
                                        </tr>
                                        <tr>
                                            <td class="tituloCampo" style="height: 23px">
                                                <b>De:</b>
                                                <asp:TextBox ID="tbxPeriodoInicial" onkeypress="formatar(this, '##/##/####');OnlyNumbers()"
                                                    MaxLength="10" runat="server" CssClass="formNovo"  Width="75px"></asp:TextBox>
                                                <img alt="" id="imgPeriodoInicial" align="absmiddle"  style="cursor: pointer;" src="../Imagens/Calendar_scheduleHS.png" />
                                                <asp:RegularExpressionValidator ControlToValidate="tbxPeriodoInicial" Text="*" CssClass="asterisco"
                                                    ID="RegularExpressionValidator2" ErrorMessage="Período inicial Inválido." ValidationGroup="ValidaDados"
                                                    runat="server" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"></asp:RegularExpressionValidator>
                                                <ajaxToolkit:CalendarExtender Format="dd/MM/yyyy" ID="CalendarInicial" PopupButtonID="imgPeriodoInicial"
                                                    runat="server" TargetControlID="tbxPeriodoInicial" Animated="true">
                                                </ajaxToolkit:CalendarExtender>
                                                &nbsp; &nbsp; <b>Até:</b>
                                                <asp:TextBox ID="tbxPeriodoFinal" runat="server" onkeypress="formatar(this, '##/##/####');OnlyNumbers()"
                                                    MaxLength="10" CssClass="formNovo" TabIndex="12" Width="75px"></asp:TextBox>
                                                <img alt="" align="absmiddle" id="imgPeriodoFinal" style="cursor: pointer" src="../Imagens/Calendar_scheduleHS.png" />
                                                <asp:RegularExpressionValidator ControlToValidate="tbxPeriodoFinal" Text="*" CssClass="asterisco"
                                                    ID="RegularExpressionValidator1" ErrorMessage="Período final Inválido." ValidationGroup="ValidaDados"
                                                    runat="server" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"></asp:RegularExpressionValidator>
                                                <ajaxToolkit:CalendarExtender Format="dd/MM/yyyy" ID="CalendarFinal" PopupButtonID="imgPeriodoFinal"
                                                    runat="server" TargetControlID="tbxPeriodoFinal" Animated="true">
                                                </ajaxToolkit:CalendarExtender>
                                                <asp:CustomValidator ID="ctvValidaDataPesquisa" runat="server" ClientValidationFunction="ValidaDataPesquisa"
                                                    ValidationGroup="ValidaDados" ErrorMessage="Datas inválidas" CssClass="asterisco">*</asp:CustomValidator>
                                                &nbsp;
                                            </td>
                                            <td class="tituloCampo" style="height: 12px"><asp:DropDownList runat="server" ID="ddlRegiao"></asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td>
                                              
                                            </td>
                                        </tr>
                                    </table>
                                    <table  style="display:none;text-align:left" runat="server" id="tableProduto" >
                                    <tr>
                                        <td class="tituloCampo" style="height: 23px">
                                                <b>Período  De:</b>
                                                <asp:TextBox ID="tbxPeriodoIni" onkeypress="formatar(this, '##/##/####');OnlyNumbers()"
                                                    MaxLength="10" runat="server" CssClass="formNovo"  Width="75px"></asp:TextBox>
                                                <img alt="" id="imgPeriodoIni" align="absmiddle"  style="cursor: pointer;" src="../Imagens/Calendar_scheduleHS.png" />
                                                <asp:RegularExpressionValidator ControlToValidate="tbxPeriodoIni" Text="*" CssClass="asterisco"
                                                    ID="RegularExpressionValidator3" ErrorMessage="Período inicial Inválido." ValidationGroup="ValidaDados"
                                                    runat="server" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"></asp:RegularExpressionValidator>
                                                <ajaxToolkit:CalendarExtender Format="dd/MM/yyyy" ID="CalendarExtender1" PopupButtonID="imgPeriodoIni"
                                                    runat="server" TargetControlID="tbxPeriodoIni" Animated="true">
                                                </ajaxToolkit:CalendarExtender>

                                                &nbsp; &nbsp; <b>Até:</b>
                                                <asp:TextBox ID="tbxFinal" runat="server" onkeypress="formatar(this, '##/##/####');OnlyNumbers()"
                                                    MaxLength="10" CssClass="formNovo" TabIndex="12" Width="75px"></asp:TextBox>
                                                <img alt="" align="absmiddle" id="imgFinal" style="cursor: pointer" src="../Imagens/Calendar_scheduleHS.png" />
                                                <asp:RegularExpressionValidator ControlToValidate="tbxFinal" Text="*" CssClass="asterisco"
                                                    ID="RegularExpressionValidator4" ErrorMessage="Período final Inválido." ValidationGroup="ValidaDados"
                                                    runat="server" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"></asp:RegularExpressionValidator>
                                                <ajaxToolkit:CalendarExtender Format="dd/MM/yyyy" ID="CalendarExtender2" PopupButtonID="imgFinal"
                                                    runat="server" TargetControlID="tbxFinal" Animated="true">
                                                </ajaxToolkit:CalendarExtender>
                                                <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="ValidaDataPesquisa"
                                                    ValidationGroup="ValidaDados" ErrorMessage="Datas inválidas" CssClass="asterisco">*</asp:CustomValidator>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td runat="server">
                                                <asp:RadioButton runat="server" onclick="Habilita(1)"  ID="rbtDescricao" Checked="true" GroupName="tipoProduto" 
                                                    Text="Descrição" />
                                                 <asp:RadioButton onclick="Habilita(2)"  AutoPostBack="true" runat="server" ID="rbtCodigo"  Text="Código" GroupName="tipoProduto" 
                                                    />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="tdDescricao">
                                                <span>Descrição</span>
                                                <asp:TextBox runat="server" ID="txtDescricao" CssClass="formNovo"></asp:TextBox>
                                            </td>
                                            <td id="tdCodigo" style="display:none">
                                                <span>Código</span>
                                                <asp:TextBox runat="server" ID="txtCodigo" CssClass="formNovo"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                   </td>
                </tr>

            </table>
        </td>
    </tr>
</table>
<asp:ValidationSummary runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="ValidaDados" />
</asp:Content>
