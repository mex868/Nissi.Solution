<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master"  AutoEventWireup="true"
    CodeFile="AssociaUsuarioPerfilAcesso.aspx.cs" Inherits="Administracao_Usuario_AssociaUsuarioPerfilAcesso"
    EnableEventValidation="false" Title="Associação Usuário x Perfil" %>

<%@ Register Assembly="RDC.Tools" Namespace="RDC.Tools" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPrincipal" runat="server">

    <script type="text/javascript" src="../../JS/FuncoesComuns.js"></script>

    <script type="text/javascript" language="javascript">

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_initializeRequest(InitializeRequest);
        prm.add_endRequest(EndRequest);
        var postBackElement;

        function InitializeRequest(sender, args)
        {
            if (prm.get_isInAsyncPostBack())
                args.set_cancel(true);

            if (args.get_postBackElement().type != 'checkbox')
                Load(true);
        }

        function EndRequest(sender, args)
        {
            Load(false);
        }

        function mudarCor()
        {
            document.getElementById('TagBody').style.background = "#E5E5E5";
        }
        mudarCor();

        /************************************************************************************
        - Criado Por: Rodrigo Galvão
        - Criado Em.: 25/02/2009
        - Objetivo..: Validadores da página
        **************************************************************************************/
        function ValidaFuncionario(src, args)
        {
            args.IsValid = true;
            if($get('<%=ddlFuncionario.ClientID%>').selectedIndex == -1)
                args.IsValid = false;
        }
        function ValidaUsuario(src, args)
        {
            args.IsValid = true;
            if($get('<%=ddlUsuario.ClientID%>').selectedIndex == -1)
                args.IsValid = false;
        }
        function ValidaDepartamento(src, args)
        {
            args.IsValid = true;
            if($get('<%=ddlDepartamento.ClientID%>').selectedIndex <= 0)
                args.IsValid = false;
        }
        function ValidaAssociados(src, args)
        {
            var retorno = true;

            if(($get('<%=hdnCategoriaFuncional.ClientID%>').value == 'N') && 
               ($get('<%=lbxAssociados.ClientID%>').options.length == 0))
                retorno = false;

            args.IsValid = retorno;
        }
        function ValidaCategoriaFuncional(src, args)
        {
            var retorno = true;

            if(($get('<%=hdnCategoriaFuncional.ClientID%>').value == 'S') && 
               ($get('<%=ddlCategoriaFuncional.ClientID%>').value == ''))
                retorno = false;

            args.IsValid = retorno;
        }
        function RedimensionaCalendarioDataInicio()
        {
            //Solução para o calendário não aparecer atrás dos outros objetos da pagina.
            if ($find("calendariodatainicio")._popupDiv != null)
                $find("calendariodatainicio")._popupDiv.style.zIndex = 100000000;
        }
        function RedimensionaCalendarioDataTermino()
        {
            //Solução para o calendário não aparecer atrás dos outros objetos da pagina.
            if ($find("calendariodatatermino")._popupDiv != null)
                $find("calendariodatatermino")._popupDiv.style.zIndex = 100000000;
        }

        /************************************************************************************
        - Criado Por: Rodrigo Galvão
        - Criado Em.: 25/02/2009
        - Objetivo..: Manipular itens de associação nas Listboxes
        **************************************************************************************/
        function btnAssociar_Click()
        {
            fnTrocaLista($get('<%=lbxAssociar.ClientID%>'), $get('<%=lbxAssociados.ClientID%>'), false);
        }
        function btnRetirar_Click()
        {
            fnTrocaLista($get('<%=lbxAssociados.ClientID%>'), $get('<%=lbxAssociar.ClientID%>'), false);
        }
        function btnAllAssociar_Click()
        {
            fnTrocaLista($get('<%=lbxAssociar.ClientID%>'), $get('<%=lbxAssociados.ClientID%>'), true);
        }
        function btnAllRetirar_Click()
        {
            fnTrocaLista($get('<%=lbxAssociados.ClientID%>'), $get('<%=lbxAssociar.ClientID%>'), true);
        }

        /************************************************************************************
        - Criado Por: Rodrigo Galvão
        - Criado Em.: 26/02/2009
        - Objetivo..: Armazenar perfis associados em campo hidden
        **************************************************************************************/
        function ItensAssociados()
        {
            if ($get('<%=hdnCategoriaFuncional.ClientID%>').value == 'N')
            {
                $get('<%=hdnListaPerfil.ClientID%>').value = '';
                var qtdePerfil = 0;
                var nomePerfil = '';

                for(var i=0; i<$get('<%=lbxAssociados.ClientID%>').options.length; i++)
                {
                    $get('<%=hdnListaPerfil.ClientID%>').value += $get('<%=lbxAssociados.ClientID%>').options[i].value + '|';
                    nomePerfil = $get('<%=lbxAssociados.ClientID%>').options[i].text;
                    qtdePerfil++;
                }

                var texto = '';

                if (qtdePerfil == 0)
                    texto = 'Nenhum Perfil associado.\n\n';
                else if (qtdePerfil == 1)
                    texto = 'Você selecionou o Perfil "' + nomePerfil.toString() + '".\n\n';
                else
                    texto = 'Você têm ' + qtdePerfil.toString() + ' perfis associados.\n\n';

                texto += 'Confirmar gravação?'

                return confirm(texto);
            }
            else
            {
                var retorno = true;
                var mensagem = 'Favor preencher o(s) seguinte(s) campo(s):\n';

                if ($get('<%=ddlCategoriaFuncional.ClientID%>').value == '')
                {
                    mensagem += '\n- Categoria Funcional;';
                    retorno = false;
                }

                if ($get('<%=tbxDataInicio.ClientID%>').value == '')
                {
                    mensagem += '\n- Data de Início da Atuação;';
                    retorno = false;
                }

                if (!retorno)
                    alert(mensagem);

                return retorno;
            }
        }

        /************************************************************************************
        - Criado Por: Rodrigo Galvão
        - Criado Em.: 26/02/2009
        - Objetivo..: Validar quantidade de perfis selecionados para visualização de Funcionalidades.
        **************************************************************************************/
        function ValidarPerfilSelecionado()
        {
            var qtdeItens = 0;
            for(var i=0; i<$get('<%=lbxAssociar.ClientID%>').length; i++)
            {
                if ($get('<%=lbxAssociar.ClientID%>').options[i].selected)
                {
                    qtdeItens++;
                    if (qtdeItens == 1)
                    {
                        $get('<%=hdnPerfilSelecionado.ClientID%>').value = $get('<%=lbxAssociar.ClientID%>').options[i].value;
                        $get('<%=hdnNomePerfilSelecionado.ClientID%>').value = $get('<%=lbxAssociar.ClientID%>').options[i].text;
                    }
                }
            }

            if (qtdeItens == 1)
                return true;
            else
            {
                alert('Selecione "1" Perfil para visualizar as suas Funcionalidade!');
                return false;
            }
        }
        
        /************************************************************************************
        - Criado Por: mmello
        - Criado Em.: 12/11/2009
        - Objetivo..: Validar funcionário selecionado para visualização de Categorias Funcionais.
        **************************************************************************************/
        function ValidarFuncionarioSelecionado()
        {
            var ddlFuncionario = $get('<%=ddlFuncionario.ClientID%>');
            if (ddlFuncionario.selectedIndex > -1)
            {
                $get('<%=hdnFuncionarioSelecionado.ClientID%>').value = ddlFuncionario.options[ddlFuncionario.selectedIndex].value;
                $get('<%=hdnNomeFuncionarioSelecionado.ClientID%>').value = ddlFuncionario.options[ddlFuncionario.selectedIndex].text;
                return true;
            }
            else
            {
                alert('Selecione um Funcionário para visualizar as suas Categorias Funcionais!');
                return false;
            }
        }

        /************************************************************************************
        - Criado Por: Rodrigo Galvão
        - Criado Em.: 11/03/2009
        - Objetivo..: Preencher campos da popup de informe de término.
        **************************************************************************************/
        function PreencherCamposTermino(descCategoria, dataTermino)
        {
            $get('<%=tbxDataTermino.ClientID%>').value = '';
            $get('<%=lblDataInicio.ClientID%>').innerText = dataTermino.toString();
            $get('<%=lblDescCategoriaFuncional.ClientID%>').innerText = descCategoria.toString();
        }

    </script>

    <asp:HiddenField ID="hdnCategoriaFuncional" runat="server" />
    <div style="text-align: center;">
        <br style="line-height: 5px;" />
        <table border="0" style="width: 95%; margin-right: auto; margin-left: auto;">
            <tr>
                <td style="width: 3%; text-align: left;">
                    <img height="16" src="../../Imagens/layout.png" width="18" alt="" />
                </td>
                <td style="width: 97%; text-align: left;" class="titulo">
                    <asp:Label ID="lblTitulo" runat="server" CssClass="titulo">
                        Associação Usuário x Perfil
                    </asp:Label>
                </td>
            </tr>
        </table>
        <br style="line-height: 5px;" />
        <asp:UpdatePanel ID="updIniciarAssoc" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnGravar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnCancelar" EventName="Click" />
            </Triggers>
            <ContentTemplate>
                <table id="tbAssociaUsuPerfil" runat="server" style="width: 95%; margin-right: auto;
                    margin-left: auto;" class="fundotabelain" cellpadding="3" cellspacing="0">
                    <tr>
                        <td class="tituloCampo" style="text-align: left; width: 15%;">
                            Funcionário
                        </td>
                        <td class="tituloCampo" style="text-align: left; width: 80%;" colspan="2">
                            <asp:DropDownList ID="ddlFuncionario" runat="server" CssClass="formNovo" Width="370px">
                            </asp:DropDownList>
                            <ajaxToolkit:CascadingDropDown ID="cddFuncionario" runat="server" TargetControlID="ddlFuncionario"
                                ServiceMethod="ListaFuncionariosAtivos" Category="funcionario" ServicePath="../../CascadingWS.asmx"
                                LoadingText="Carregando Funcionários...">
                            </ajaxToolkit:CascadingDropDown>
                            <asp:ImageButton ID="imbCategoriaFuncionalFuncionario" runat="server" ImageUrl="~/Imagens/Icone_Pasta_Visualizar.gif"
                                OnClick="imbCategoriaFuncionalFuncionario_Click" ToolTip="Visualizar categorias funcionais do funcionário selecionado"
                                Style="cursor: hand;" OnClientClick="return ValidarFuncionarioSelecionado();" />
                            <asp:CustomValidator ID="ctvFuncionario" ErrorMessage="Funcionário não informado"
                                runat="server" CssClass="asterisco" ValidationGroup="ValidaCombos" ClientValidationFunction="ValidaFuncionario">*</asp:CustomValidator>
                            
                        </td>
                    </tr>
                    <tr>
                        <td class="tituloCampo" style="text-align: left; width: 15%;">
                            Usuário
                        </td>
                        <td class="tituloCampo" style="text-align: left; width: 80%;" colspan="2">
                            <asp:DropDownList ID="ddlUsuario" runat="server" CssClass="formNovo" Width="370px">
                            </asp:DropDownList>
                            <ajaxToolkit:CascadingDropDown ID="cddUsuario" runat="server" TargetControlID="ddlUsuario"
                                ServiceMethod="ListaUsuariosAtivosPorFuncionario" Category="usuario" ParentControlID="ddlFuncionario"
                                ServicePath="../../CascadingWS.asmx" LoadingText="Carregando Usuários...">
                            </ajaxToolkit:CascadingDropDown>
                            <asp:CustomValidator ID="ctvUsuario" ErrorMessage="Usuário não informado" runat="server"
                                CssClass="asterisco" ValidationGroup="ValidaCombos" ClientValidationFunction="ValidaUsuario">*</asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="tituloCampo" style="text-align: left; width: 15%;">
                            Localidade
                        </td>
                        <td class="tituloCampo" style="text-align: left; width: 80%;" colspan="2">
                            <asp:DropDownList ID="ddlLocalidade" runat="server" CssClass="formNovo" Width="370px">
                            </asp:DropDownList>
                            <ajaxToolkit:CascadingDropDown ID="cddLocalidade" runat="server" TargetControlID="ddlLocalidade"
                                ServiceMethod="ListaLocalidade" Category="localidade" ServicePath="../../CascadingWS.asmx"
                                LoadingText="Carregando Localidades...">
                            </ajaxToolkit:CascadingDropDown>
                        </td>
                    </tr>
                    <tr>
                        <td class="tituloCampo" style="text-align: left; width: 15%;">
                            Departamento
                        </td>
                        <td class="tituloCampo" style="text-align: left; width: 65%;">
                            <asp:DropDownList ID="ddlDepartamento" runat="server" CssClass="formNovo" Width="370px">
                            </asp:DropDownList>
                            <ajaxToolkit:CascadingDropDown ID="cddDepartamento" runat="server" TargetControlID="ddlDepartamento"
                                ServiceMethod="ListaDepartamentosPorLocalidade_02" Category="departamento" ParentControlID="ddlLocalidade"
                                ServicePath="../../CascadingWS.asmx" LoadingText="Carregando Departamentos...">
                            </ajaxToolkit:CascadingDropDown>
                            <asp:CustomValidator ID="ctvDepartamento" ErrorMessage="Departamento não informado"
                                runat="server" CssClass="asterisco" ValidationGroup="ValidaCombos" ClientValidationFunction="ValidaDepartamento">*</asp:CustomValidator>
                        </td>
                        <td class="tituloCampo" style="text-align: right; width: 15%;">
                            <asp:Button ID="btnIniciarAssoc" runat="server" Text="Iniciar Associação" ValidationGroup="ValidaCombos"
                                CssClass="botaoCadastro" OnClick="btnIniciarAssoc_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br style="line-height: 7px" />
        <asp:UpdatePanel ID="updPerfil" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnIniciarAssoc" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnGravar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnCancelar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnGravaTermino" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnCancelaTermino" EventName="Click" />
            </Triggers>
            <ContentTemplate>
                <div id="dvCategoriaFuncional" runat="server" style="overflow: auto; display: none;
                    width: 95%; text-align: left;">
                    <table border="0" style="width: 100%; margin-right: auto; margin-left: auto;" cellpadding="10"
                        cellspacing="0" class="fundotabelain">
                        <tr>
                            <td style="width: 100%; line-height: 10px;" class="fundoTitulo">
                                ::Associação de Categoria Funcional
                            </td>
                        </tr>
                        <tr>
                            <td class="tituloCampo" style="text-align: left; width: 100%;">
                                <table border="0" cellpadding="4" cellspacing="0" style="width: 100%;">
                                    <tr>
                                        <td class="tituloCampo" style="text-align: left; width: 20%;">
                                            Categoria Funcional
                                        </td>
                                        <td class="tituloCampo" style="text-align: left; width: 80%;">
                                            <asp:DropDownList ID="ddlCategoriaFuncional" runat="server" CssClass="formNovo" Width="300px">
                                            </asp:DropDownList>
                                            <asp:CustomValidator ID="ctvCategoriaFuncional" ErrorMessage="Categoria Funcional não informada"
                                                runat="server" CssClass="asterisco" ValidationGroup="ValidaCadastro" ClientValidationFunction="ValidaCategoriaFuncional">*</asp:CustomValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tituloCampo" style="text-align: left; width: 20%;">
                                            Início de Atuação
                                        </td>
                                        <td class="tituloCampo" style="text-align: left; width: 80%;">
                                            <asp:TextBox ID="tbxDataInicio" CssClass="formNovo" runat="server" Width="85px"></asp:TextBox>
                                            <img id="ImgBtnCalendario1" causesvalidation="False" style="cursor: hand" src="<%=caminhoAplicacao%>Imagens/Calendar_scheduleHS.png" />
                                            <ajaxToolkit:MaskedEditExtender ID="mskDataInicio" runat="server" AcceptNegative="Left"
                                                DisplayMoney="Left" ErrorTooltipEnabled="false" Mask="99/99/9999" MaskType="Date"
                                                MessageValidatorTip="false" TargetControlID="tbxDataInicio">
                                            </ajaxToolkit:MaskedEditExtender>
                                            <ajaxToolkit:MaskedEditValidator ID="mkvDataInicio" runat="server" ControlExtender="mskDataInicio"
                                                ControlToValidate="tbxDataInicio" Display="Dynamic" InvalidValueBlurredMessage="*"
                                                CssClass="asterisco">*</ajaxToolkit:MaskedEditValidator>
                                            <ajaxToolkit:CalendarExtender ID="extcalendario" runat="server" BehaviorID="calendariodatainicio"
                                                Format="dd/MM/yyyy" OnClientShowing="RedimensionaCalendarioDataInicio" PopupButtonID="ImgBtnCalendario1"
                                                PopupPosition="BottomLeft" TargetControlID="tbxDataInicio">
                                            </ajaxToolkit:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="rfvDataInicio" runat="server" ControlToValidate="tbxDataInicio"
                                                CssClass="asterico" ErrorMessage="Informe a Data de Início da Atuação" ValidationGroup="ValidaCadastro">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tituloCampo" style="text-align: left; width: 100%;">
                                <div id="dvGridCategoria" runat="server" style="overflow: auto; display: block; width: 98%;
                                    text-align: left; height: 280px;">
                                    <cc1:RDCGrid CssClass="fundotabelain" ID="grdCategoria" ShowPageDetails="True" runat="server"
                                        AutoGenerateColumns="False" CellPadding="1" CellSpacing="3" GridLines="None"
                                        Width="100%" BorderColor="Black" BorderWidth="1px" OnPageIndexChanging="grdCategoria_PageIndexChanging"
                                        OnRowDataBound="grdCategoria_RowDataBound" OnRowCommand="grdCategoria_RowCommand"
                                        AllowPaging="True" MultiSelection="False" ShowHeaderCheckBoxColumn="False" ShowOptionColumn="False">
                                        <HeaderStyle CssClass="classtd" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="T&#233;rmino">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgEditar" runat="server" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="5%" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Categoria Funcional">
                                                <HeaderStyle Width="45%" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Data In&#237;cio Atua&#231;&#227;o">
                                                <HeaderStyle Width="25%" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Data T&#233;rmino Atua&#231;&#227;o">
                                                <HeaderStyle Width="25%" />
                                                <ItemStyle HorizontalAlign="Center" Font-Bold="True" ForeColor="#C00000" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="CodUsuarioDepartamentoCategoria" Visible="False"></asp:BoundField>
                                        </Columns>
                                    </cc1:RDCGrid>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="dvGridAssociacao" runat="server" style="overflow: auto; display: none; width: 95%;
                    text-align: left;">
                    <table border="0" style="width: 100%; margin-right: auto; margin-left: auto;" cellpadding="10"
                        cellspacing="0" class="fundotabelain">
                        <tr>
                            <td style="width: 100%; text-align: left; line-height: 10px; background-color: #FEE8BA;"
                                class="tituloCampo" colspan="3">
                                ::Associação de Perfil
                            </td>
                        </tr>
                        <tr>
                            <td class="tituloCampo" style="text-align: left; width: 45%;">
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                    <tr>
                                        <td class="tituloCampo" style="text-align: left; width: 100%;">
                                            <table border="0" cellpadding="0" cellspacing="0" style="width: 270px;">
                                                <tr>
                                                    <td class="tituloCampo" style="text-align: left; width: 80%;">
                                                        Perfil(s) Disponível(is)
                                                    </td>
                                                    <td class="tituloCampo" style="text-align: right; width: 20%;">
                                                        <asp:ImageButton ID="btnImgFuncionalidade" runat="server" ImageUrl="~/Imagens/Icone_Pasta_Visualizar.gif"
                                                            OnClick="btnImgFuncionalidade_Click" ToolTip="Visualizar funcionalidades do Perfil selecionado"
                                                            Style="cursor: hand;" OnClientClick="return ValidarPerfilSelecionado();" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tituloCampo" style="text-align: left; width: 100%;">
                                            <asp:ListBox ID="lbxAssociar" runat="server" CssClass="formNovo" SelectionMode="Multiple"
                                                Width="270px" Height="190px" OnDblClick="btnAssociar_Click();"></asp:ListBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="tituloCampo" style="text-align: center; width: 10%;">
                                <table border="0" style="width: 100%" cellpadding="0" cellspacing="5">
                                    <tr>
                                        <td class="tituloCampo" style="text-align: center; width: 100%;">
                                            <input class="botaoNovo" id="btnAssociar" style="width: 48px" onclick="btnAssociar_Click();"
                                                type="button" value=">" name="btnAssociar" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tituloCampo" style="text-align: center; width: 100%;">
                                            <input class="botaoNovo" id="btnRetirar" style="width: 48px" onclick="btnRetirar_Click();"
                                                type="button" value="<" name="btnRetirar" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tituloCampo" style="text-align: center; width: 100%;">
                                            <input class="botaoNovo" id="btnAllAssociar" style="width: 48px" onclick="btnAllAssociar_Click();"
                                                type="button" value=">>" name="btnAllAssociar" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tituloCampo" style="text-align: center; width: 100%;">
                                            <input class="botaoNovo" id="btnAllRetirar" style="width: 48px" onclick="btnAllRetirar_Click();"
                                                type="button" value="<<" name="btnAllRetirar" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="tituloCampo" style="text-align: right; width: 45%;">
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 250px;">
                                    <tr>
                                        <td class="tituloCampo" style="text-align: left; width: 100%;">
                                            <br />
                                            <br style="line-height: 5px;" />
                                            Perfil(s) Associado(s)
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tituloCampo" style="text-align: right; width: 100%;">
                                            <asp:ListBox ID="lbxAssociados" runat="server" CssClass="formNovo" SelectionMode="Multiple"
                                                Width="270px" Height="190px" OnDblClick="btnRetirar_Click();"></asp:ListBox>
                                            <asp:CustomValidator ID="ctvAssociados" ErrorMessage="Perfil(s) não associado(s)"
                                                runat="server" CssClass="asterisco" ValidationGroup="ValidaCadastro" ClientValidationFunction="ValidaAssociados">*</asp:CustomValidator>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br style="line-height: 7px" />
        <asp:UpdatePanel ID="updBotoes" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnIniciarAssoc" EventName="Click" />
            </Triggers>
            <ContentTemplate>
                <table border="0" style="width: 95%; margin-right: auto; margin-left: auto;" cellpadding="0"
                    cellspacing="0">
                    <tr>
                        <td align="left" style="width: 34%">
                            <asp:Button runat="server" ID="btnVoltar" CssClass="botaoNovo" Text="Voltar" Width="120px"
                                OnClick="btnVoltar_Click" />
                        </td>
                        <td align="left" style="width: 33%; text-align: center;">
                            <asp:Button runat="server" ID="btnCancelar" CssClass="botaoNovo" Text="Cancelar"
                                Width="120px" OnClick="btnCancelar_Click" />
                        </td>
                        <td align="left" style="width: 34%; text-align: right;">
                            <asp:HiddenField ID="hdnListaPerfil" runat="server" />
                            <asp:Button runat="server" ID="btnGravar" CssClass="botaoNovo" Text="Gravar" Width="120px"
                                OnClientClick="return ItensAssociados();" OnClick="btnGravar_Click" ValidationGroup="ValidaCadastro" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <input id="hndTargetControl" runat="server" type="hidden" />
    <input id="hndTargetControlTermino" runat="server" type="hidden" />
    <input id="hndTargetControlCategoriaFuncionalFuncionario" runat="server" type="hidden" />
    <input id="hdnPerfilSelecionado" runat="server" type="hidden" />
    <input id="hdnNomePerfilSelecionado" runat="server" type="hidden" />
    <input id="hdnFuncionarioSelecionado" runat="server" type="hidden" />
    <input id="hdnNomeFuncionarioSelecionado" runat="server" type="hidden" />
    <ajaxToolkit:ModalPopupExtender ID="mpeFuncionalidade" runat="server" PopupControlID="PNL"
        TargetControlID="hndTargetControl" BehaviorID="mpeFuncionalidadeID" BackgroundCssClass="modalBackground"
        CancelControlID="CancelButton" DropShadow="true" />
    <asp:Panel ID="PNL" runat="server" Style="cursor: move; background-color: #DDDDDD;
        border: solid 1px Gray; color: Black">
        <div style="text-align: center; width: 500px; height: 400px; padding: 5px 5px 5px 5px;">
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%;">
                <tr>
                    <td style="text-align: left;">
                        <div id="dvFuncionalidades" style="overflow: auto; display: block; width: 490px;
                            height: 345px;" class="fundotabelain">
                            <asp:UpdatePanel ID="updFuncionalidade" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnImgFuncionalidade" EventName="Click" />
                                </Triggers>
                                <ContentTemplate>
                                    <br style="line-height: 5px;" />
                                    <asp:Label ID="lblPerfil" runat="server" Text="" Font-Bold="true" Font-Names="Verdana"
                                        Font-Size="12px" ForeColor="#BA622B">
                                    </asp:Label>
                                    <br />
                                    <br style="line-height: 7px;" />
                                    <cc1:RDCGrid CssClass="fundotabelain" ID="grdFuncionalidade" runat="server" AutoGenerateColumns="False"
                                        CellPadding="1" CellSpacing="3" GridLines="None" Width="470px" BorderColor="Black"
                                        BorderWidth="1px" OnRowDataBound="grdFuncionalidade_RowDataBound">
                                        <HeaderStyle CssClass="classtd" />
                                        <Columns>
                                            <asp:BoundField HeaderText="Funcionalidade">
                                                <ItemStyle CssClass="classtdleft" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" Width="90%" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Ativa">
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                        </Columns>
                                    </cc1:RDCGrid>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; width: 490px; height: 45px;">
                        <asp:Button ID="CancelButton" runat="server" Text="Voltar" CssClass="botaoNovo" Width="100px" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="mpeDataTermino" runat="server" PopupControlID="pnlDataTermino"
        TargetControlID="hndTargetControlTermino" BehaviorID="mpeDataTerminoID" BackgroundCssClass="modalBackground"
        DropShadow="true" />
    <asp:Panel ID="pnlDataTermino" runat="server" Style="cursor: move; background-color: #DDDDDD;
        border: solid 1px Gray; color: Black" TabIndex="5">
        <div style="text-align: center; width: 400px; height: 160px; padding: 5px 5px 5px 5px">
            <table border="0" cellpadding="3" cellspacing="3" style="width: 100%;" class="fundotabela">
                <tr>
                    <td class="fundoTitulo" style="height: 10%;" colspan="2">
                        ::Término da Atuação
                    </td>
                </tr>
                <tr>
                    <td style="height: 80%;" valign="top" colspan="2">
                        <table border="0" cellpadding="5" cellspacing="5" style="width: 100%">
                            <tr>
                                <td style="width: 40%; font-weight: bold;">
                                    Categoria Funcional:
                                </td>
                                <td style="width: 60%;">
                                    <asp:Label ID="lblDescCategoriaFuncional" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 40%; font-weight: bold;">
                                    Data Início:
                                </td>
                                <td style="width: 60%;">
                                    <asp:Label ID="lblDataInicio" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 40%; font-weight: bold;">
                                    Data Término:
                                </td>
                                <td style="width: 60%;">
                                    <asp:TextBox ID="tbxDataTermino" CssClass="formNovo" runat="server" Width="85px"></asp:TextBox>
                                    <img id="ImgBtnCalendario2" causesvalidation="False" style="cursor: hand" src="<%=caminhoAplicacao%>Imagens/Calendar_scheduleHS.png" />
                                    <ajaxToolkit:MaskedEditExtender ID="mskDataTermino" runat="server" AcceptNegative="Left"
                                        DisplayMoney="Left" ErrorTooltipEnabled="false" Mask="99/99/9999" MaskType="Date"
                                        MessageValidatorTip="false" TargetControlID="tbxDataTermino">
                                    </ajaxToolkit:MaskedEditExtender>
                                    <ajaxToolkit:MaskedEditValidator ID="msdDataTermino" runat="server" ControlExtender="mskDataTermino"
                                        ControlToValidate="tbxDataInicio" Display="Dynamic" InvalidValueBlurredMessage="*"
                                        CssClass="asterisco">*</ajaxToolkit:MaskedEditValidator>
                                    <ajaxToolkit:CalendarExtender ID="extDataTermino" runat="server" BehaviorID="calendariodatatermino"
                                        Format="dd/MM/yyyy" OnClientShowing="RedimensionaCalendarioDataTermino" PopupButtonID="ImgBtnCalendario2"
                                        PopupPosition="BottomLeft" TargetControlID="tbxDataTermino">
                                    </ajaxToolkit:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="rqvDataTermino" runat="server" ControlToValidate="tbxDataTermino"
                                        CssClass="asterisco" ErrorMessage="Informe a Data de Término da Atuação" ValidationGroup="ValidaDadosTermino">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; width: 100%; height: 10%;">
                        <asp:UpdatePanel ID="updCancelaTermino" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Button ID="btnCancelaTermino" runat="server" Text="Voltar" CssClass="botaoNovo"
                                    Width="90px" TabIndex="5" OnClick="btnCancelaTermino_Click" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td style="text-align: right; width: 100%; height: 10%;">
                        <asp:UpdatePanel ID="updGravaTermino" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Button ID="btnGravaTermino" runat="server" Text="Gravar" CssClass="botaoNovo"
                                    Width="90px" ValidationGroup="ValidaDadosTermino" OnClick="btnGravaTermino_Click" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="mpeCategoriaFuncionalFuncionario" runat="server"
        PopupControlID="pnlCategoriaFuncionalFuncionario" TargetControlID="hndTargetControlCategoriaFuncionalFuncionario"
        BehaviorID="mpeFuncionarioID" BackgroundCssClass="modalBackground" CancelControlID="btnFechar"
        DropShadow="true" />
    <asp:Panel ID="pnlCategoriaFuncionalFuncionario" runat="server" Style="cursor: move;
        background-color: #DDDDDD; border: solid 1px Gray; color: Black">
        <div style="text-align: center; width: 500px; height: 400px; padding: 5px 5px 5px 5px;">
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%;">
                <tr>
                    <td style="text-align: left;">
                        <div id="dvCategoriasFuncionais" style="overflow: auto; display: block; width: 490px;
                            height: 345px;" class="fundotabelain">
                            <asp:UpdatePanel ID="updCategoriasFuncionais" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="imbCategoriaFuncionalFuncionario" EventName="Click" />
                                </Triggers>
                                <ContentTemplate>
                                    <br style="line-height: 5px;" />
                                    <asp:Label ID="lblFuncionario" runat="server" Text="" Font-Bold="true" Font-Names="Verdana"
                                        Font-Size="12px" ForeColor="#BA622B">
                                    </asp:Label>
                                    <br />
                                    <br style="line-height: 7px;" />
                                    <asp:Table ID="tblCategoriasFuncionais" runat="server" CssClass="fundotabelain" CellPadding="3"
                                        CellSpacing="0" BorderWidth="1px" BorderColor="Black">
                                    </asp:Table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center; width: 490px; height: 45px;">
                        <asp:Button ID="btnFechar" runat="server" Text="Voltar" CssClass="botaoNovo" Width="100px" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <asp:ValidationSummary ID="vlsSumarioAssociacao" runat="server" CssClass="asterisco"
        ShowMessageBox="true" ShowSummary="false" ValidationGroup="ValidaCombos" />
    <asp:ValidationSummary ID="vlsSumarioAssociados" runat="server" CssClass="asterisco"
        ShowMessageBox="true" ShowSummary="false" ValidationGroup="ValidaCadastro" />
    <asp:ValidationSummary ID="vlsTermino" runat="server" CssClass="asterisco" ShowMessageBox="true"
        ShowSummary="false" ValidationGroup="ValidaDadosTermino" />
</asp:Content>
