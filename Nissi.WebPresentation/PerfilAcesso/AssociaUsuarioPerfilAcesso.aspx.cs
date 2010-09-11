#region Usings
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Collections.Generic;
using System.Text;
#endregion

public partial class Administracao_Usuario_AssociaUsuarioPerfilAcesso : BasePage
{
  /*  #region Variaveis Globais
    protected string caminhoAplicacao = ConfigurationManager.AppSettings["CGA.SIDAP.CaminhoAplicacao"].ToString();
    #endregion

    #region PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Caso tenha QueryString de Categoria Funcional, então habilita o modo de associação
            if (Request.QueryString["CategoriaFuncional"] != null &&
                Request.QueryString["CategoriaFuncional"].ToString().Equals("S"))
            {
                hdnCategoriaFuncional.Value = "S";
                lblTitulo.Text = "Associação Usuário x Categoria Funcional";
                imbCategoriaFuncionalFuncionario.Visible = true;
            }
            else
            {
                hdnCategoriaFuncional.Value = "N";
                lblTitulo.Text = "Associação Usuário x Perfil";
                imbCategoriaFuncionalFuncionario.Visible = false;
            }

            HabilitaAssociacao(false);
            //Apagar: Implementação de Localidade
            //CarregarDepartamento();
            //Apagar
        }
    }
    #endregion

    #region Carregar Listas
    private void CarregarPerfilAcesso()
    {
        int codUsuario = int.Parse(cddUsuario.SelectedValue.Split(':')[0]);
        int codDepartamento = int.Parse(ddlDepartamento.SelectedValue.ToString());

        //Listagem de perfis do usuário
        List<PerfilAcessoVO> listaPerfilUsuario = new PerfilAcesso().ListarPerfilAcessoPorUsuario(codUsuario, codDepartamento);

        //Listagem de todos os perfis do sistema
        List<PerfilAcessoVO> listaPerfil = new PerfilAcesso().ListarPerfilAcessoExcetoPerfilUsuario(UsuarioAtivo.NaturezaProcesso,
                                                                                                    listaPerfilUsuario);

        //ListBox "Perfis Disponíveis
        lbxAssociar.DataSource = listaPerfil;
        lbxAssociar.DataTextField = "NomPerfilAcesso";
        lbxAssociar.DataValueField = "CodPerfilAcesso";
        lbxAssociar.DataBind();

        //ListBox "Perfis Associados"
        lbxAssociados.DataSource = listaPerfilUsuario;
        lbxAssociados.DataTextField = "NomPerfilAcesso";
        lbxAssociados.DataValueField = "CodPerfilAcesso";
        lbxAssociados.DataBind();
    }

    private void CarregarDepartamento()
    {
        ddlDepartamento.DataSource = new Departamento().ListaDepartamento(UsuarioAtivo.NaturezaProcesso);
        ddlDepartamento.DataTextField = "DescDepartamento";
        ddlDepartamento.DataValueField = "CodDepartamento";
        ddlDepartamento.DataBind();
        ddlDepartamento.Items.Insert(0, new ListItem("(Selecione)", string.Empty));
    }

    private void CarregarCategoriaFuncional()
    {
        //Popular dropdownlist com todas as categorias
        ddlCategoriaFuncional.DataSource = new CategoriaFuncional().Listar();
        ddlCategoriaFuncional.DataTextField = "DescCategoriaFuncional";
        ddlCategoriaFuncional.DataValueField = "CodCategoriaFuncional";
        ddlCategoriaFuncional.DataBind();
        ddlCategoriaFuncional.Items.Insert(0, new ListItem("(Selecione)", string.Empty));
    }
    #endregion

    #region Funções auxiliares
    private void HabilitaAssociacao(bool habilita)
    {
        cddFuncionario.Enabled =
        cddUsuario.Enabled =
        ddlDepartamento.Enabled =
        btnIniciarAssoc.Enabled = !habilita;
        btnGravar.Visible =
        btnCancelar.Visible = habilita;

        if (habilita)
        {
            if (hdnCategoriaFuncional.Value.Equals("S"))
            {
                dvGridAssociacao.Style.Add("display", "none");
                dvCategoriaFuncional.Style.Add("display", "block");
                CarregarCategoriaFuncional();
                CarregarGridCategoriaUsuarioDepartamento();
                tbxDataInicio.Text = string.Empty;
                this.Master.SetarFoco(ddlCategoriaFuncional);
            }
            else
            {
                dvGridAssociacao.Style.Add("display", "block");
                dvCategoriaFuncional.Style.Add("display", "none");
                CarregarPerfilAcesso();
                this.Master.SetarFoco(lbxAssociar);
            }
        }
        else
        {
            dvGridAssociacao.Style.Add("display", "none");
            dvCategoriaFuncional.Style.Add("display", "none");
        }
    }
    #endregion

    #region Eventos dos Botões
    protected void btnIniciarAssoc_Click(object sender, EventArgs e)
    {
        HabilitaAssociacao(true);
    }

    protected void btnGravar_Click(object sender, EventArgs e)
    {
        //Usuário selecionado
        int codUsuario = int.Parse(cddUsuario.SelectedValue.Split(':')[0]);

        //Departamento selecionado
        int codDepartamento = int.Parse(ddlDepartamento.SelectedValue.ToString());

        //Caso seja Inclusão de Categoria Funcional
        if (hdnCategoriaFuncional.Value.Equals("S"))
        {
            //VO com dados de inclusão
            UsuarioDepartamentoCategoriaVO identUsuDeptCat = new UsuarioDepartamentoCategoriaVO();
            identUsuDeptCat.Usuario.CodUsuario = codUsuario;
            identUsuDeptCat.Departamento.CodDepartamento = codDepartamento;
            identUsuDeptCat.CategoriaFuncional.CodCategoriaFuncional = short.Parse(ddlCategoriaFuncional.SelectedValue);
            identUsuDeptCat.DataInicioAtuacao = Convert.ToDateTime(tbxDataInicio.Text);

            //Inclusão da Categoria
            string mensagem = new UsuarioDepartamentoCategoria().Incluir(identUsuDeptCat, UsuarioAtivo);

            if (!string.IsNullOrEmpty(mensagem))
                MensagemCliente(mensagem);
            else
                CarregarGridCategoriaUsuarioDepartamento();
        }
        else //Caso seja Inclusão de Perfil de Acesso
        {
            //String Xml dos Perfis selecionados
            string xmlListaPerfil = new PerfilAcesso().GerarXmlListaPerfil(hdnListaPerfil.Value.Split('|'));

            //Associação de perfis
            new PerfilAcesso().AssociarPerfilUsuarioDepartamento(codUsuario, codDepartamento, xmlListaPerfil, UsuarioAtivo);

            HabilitaAssociacao(false);
        }
    }

    protected void btnVoltar_Click(object sender, EventArgs e)
    {
        Response.Redirect("../../Default.aspx");
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        HabilitaAssociacao(false);
    }

    protected void btnImgFuncionalidade_Click(object sender, ImageClickEventArgs e)
    {
        if (CarregarFuncionalidade())
            mpeFuncionalidade.Show();
        else
            MensagemCliente("Este Perfil não possui Funcionalidades!");
    }

    protected void imbCategoriaFuncionalFuncionario_Click(object sender, ImageClickEventArgs e)
    {
        if (CarregarCategoriaFuncionalFuncionario())
            mpeCategoriaFuncionalFuncionario.Show();
        else
            MensagemCliente("Este Funcionário não possui Categorias Funcionais associadas a ele.");
    }

    protected void btnGravaTermino_Click(object sender, EventArgs e)
    {
        //Código do registro selecionado
        int codUsuarioDepartamentoCategoria = Convert.ToInt32(RecuperaValorSessao("CodUsuarioDepartamentoCategoria"));

        UsuarioDepartamentoCategoriaVO identUsuarioDepartamentoCategoria = new UsuarioDepartamentoCategoriaVO();
        identUsuarioDepartamentoCategoria.CodUsuarioDepartamentoCategoria = codUsuarioDepartamentoCategoria;
        identUsuarioDepartamentoCategoria.DataTerminoAtuacao = Convert.ToDateTime(tbxDataTermino.Text);

        //Registro da data de término
        new UsuarioDepartamentoCategoria().RegistrarTermino(identUsuarioDepartamentoCategoria, UsuarioAtivo);

        DestroiValorSessao("CodUsuarioDepartamentoCategoria");

        //Ocultar popup de término
        mpeDataTermino.Hide();

        //Atualizar Grid
        CarregarGridCategoriaUsuarioDepartamento();
    }

    protected void btnCancelaTermino_Click(object sender, EventArgs e)
    {
        //Ocultar popup de término
        mpeDataTermino.Hide();

        //Atualizar Grid
        CarregarGridCategoriaUsuarioDepartamento();
    }
    #endregion

    #region Métodos do RDCGrid grdFuncionalidade
    protected void grdFuncionalidade_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            FuncionalidadeSistemaVO identFuncionalidade = (FuncionalidadeSistemaVO)e.Row.DataItem;

            e.Row.Cells[0].Text = identFuncionalidade.DescFuncSistema;
            e.Row.Cells[1].Text = (identFuncionalidade.IndAtivo.Equals("S")) ? "Sim" : "Não";

            if (e.Row.RowState == DataControlRowState.Normal)
                e.Row.CssClass = "FundoLinha1";
            else if (e.Row.RowState == DataControlRowState.Alternate)
                e.Row.CssClass = "FundoLinha2";
        }
    }

    private bool CarregarFuncionalidade()
    {
        PerfilAcessoVO identPerfil = new PerfilAcessoVO();
        identPerfil.CodPerfilAcesso = short.Parse(hdnPerfilSelecionado.Value);
        List<FuncionalidadeSistemaVO> listaFuncionalidade = new Funcionalidade().ListarPorPerfil(identPerfil);

        if (listaFuncionalidade.Count > 0)
        {
            lblPerfil.Text = "Perfil: " + hdnNomePerfilSelecionado.Value;
            grdFuncionalidade.DataSource = listaFuncionalidade;
            grdFuncionalidade.DataBind();
            return true;
        }
        else
            return false;
    }
    #endregion

    #region Métodos do Table tblCategoriasFuncionais

    private bool CarregarCategoriaFuncionalFuncionario()
    {
        FuncionarioVO identFuncionario = new FuncionarioVO();
        identFuncionario.CodFunc = int.Parse(hdnFuncionarioSelecionado.Value);

        UsuarioDepartamentoCategoria oUsuarioDepartamentoCategoria = new UsuarioDepartamentoCategoria();
        List<UsuarioDepartamentoCategoriaVO> lUsuarioDepartamentoCategoriaVO;
        lUsuarioDepartamentoCategoriaVO = oUsuarioDepartamentoCategoria.ListarPorFuncionario(identFuncionario);

        if (lUsuarioDepartamentoCategoriaVO.Count > 0)
        {
            lblFuncionario.Text = "Funcionario: " + hdnNomeFuncionarioSelecionado.Value;
            MontaTabelaCategoriasFuncionais(lUsuarioDepartamentoCategoriaVO);
            return true;
        }
        else
            return false;

    }
    #endregion

    #region Eventos do RDCGrid grdCategoria
    protected void grdCategoria_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //Necessário para funcionamento da paginação, vide observação do componente
    }

    protected void grdCategoria_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            UsuarioDepartamentoCategoriaVO identUsuDeptCat = (UsuarioDepartamentoCategoriaVO)e.Row.DataItem;

            #region Botão Editar
            ImageButton imgEditar = (ImageButton)e.Row.FindControl("imgEditar");
            if (identUsuDeptCat.DataTerminoAtuacao != DateTime.MinValue)
                imgEditar.Visible = false;
            else
            {
                imgEditar.Visible = true;
                imgEditar.ImageUrl = caminhoAplicacao + @"Imagens\RegistroData.png";
                imgEditar.CommandArgument = identUsuDeptCat.CodUsuarioDepartamentoCategoria.ToString();
                imgEditar.CommandName = "Editar";
                imgEditar.Style.Add("cursor", "hand");
                imgEditar.ToolTip = "Definir TÉRMINO desta Categoria Funcional";
            }
            #endregion

            e.Row.Cells[1].Text = identUsuDeptCat.CategoriaFuncional.DescCategoriaFuncional;
            e.Row.Cells[2].Text = identUsuDeptCat.DataInicioAtuacao.ToShortDateString();
            e.Row.Cells[3].Text = (identUsuDeptCat.DataTerminoAtuacao != DateTime.MinValue) ? identUsuDeptCat.DataTerminoAtuacao.ToShortDateString() : string.Empty;
            e.Row.Cells[4].Text = identUsuDeptCat.CodUsuarioDepartamentoCategoria.ToString();

            if (e.Row.RowState == DataControlRowState.Normal)
                e.Row.CssClass = "FundoLinha1";
            else if (e.Row.RowState == DataControlRowState.Alternate)
                e.Row.CssClass = "FundoLinha2";
        }
    }

    private void CarregarGridCategoriaUsuarioDepartamento()
    {
        //Popular grid com categorias do usuário
        UsuarioDepartamentoCategoriaVO identUsuDeptCat = new UsuarioDepartamentoCategoriaVO();
        identUsuDeptCat.Usuario.CodUsuario = int.Parse(cddUsuario.SelectedValue.Split(':')[0]);
        identUsuDeptCat.Departamento.CodDepartamento = int.Parse(ddlDepartamento.SelectedValue.ToString());
        List<UsuarioDepartamentoCategoriaVO> listaUsuDeptCat = new UsuarioDepartamentoCategoria().Listar(identUsuDeptCat);
        grdCategoria.DataSource = listaUsuDeptCat;
        grdCategoria.DataBind();
    }

    protected void grdCategoria_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        UsuarioDepartamentoCategoriaVO identUsuDeptCat = new UsuarioDepartamentoCategoriaVO();
        identUsuDeptCat.CodUsuarioDepartamentoCategoria = int.Parse(e.CommandArgument.ToString());

        //Módulo Informe de Término
        if (e.CommandName == "Editar")
        {
            //Sessão para uso na gravação do término
            ArmazenaValorSessao("CodUsuarioDepartamentoCategoria", identUsuDeptCat.CodUsuarioDepartamentoCategoria.ToString());

            mpeDataTermino.Show();

            this.Master.SetarFoco(tbxDataTermino);

            //Retornar dados do registro selecionado
            identUsuDeptCat = new UsuarioDepartamentoCategoria().ListarPorCodigo(Convert.ToInt32(e.CommandArgument));
            ExecutarScript(new StringBuilder(" PreencherCamposTermino('" + identUsuDeptCat.CategoriaFuncional.DescCategoriaFuncional +
                                "', '" + identUsuDeptCat.DataInicioAtuacao.ToShortDateString() + "'); "));
        }
    }
    #endregion

    #region " MontaTabelaCategoriasFuncionais "

    private void MontaTabelaCategoriasFuncionais(List<UsuarioDepartamentoCategoriaVO> lUsuarioDepartamentoCategoriaVO)
    {
        //Determinar Headers verticais e horizontais
        //Lista de Usuários
        List<UsuarioVO> lUsuarioVO = new List<UsuarioVO>();
        int countUsuarios;
        //Lista de Localidades
        List<LocalidadeVO> lLocalidadeVO = new List<LocalidadeVO>();
        int countLocalidades;

        foreach (UsuarioDepartamentoCategoriaVO tempUsuarioDepartamentoCategoriaVO in lUsuarioDepartamentoCategoriaVO)
        {
            UsuarioVO tempUsuario = tempUsuarioDepartamentoCategoriaVO.Usuario;
            LocalidadeVO tempLocalidade = tempUsuarioDepartamentoCategoriaVO.Departamento.Localidade;
            DepartamentoVO tempDepartamento = tempUsuarioDepartamentoCategoriaVO.Departamento;
            CategoriaFuncionalVO tempCategoriaFuncional = tempUsuarioDepartamentoCategoriaVO.CategoriaFuncional;

            //Verifica Usuario
            if (!lUsuarioVO.Exists(delegate(UsuarioVO x) { return x.CodUsuario == tempUsuario.CodUsuario; }))
            {
                //Adiciona Usuario
                lUsuarioVO.Add(tempUsuario);
            }

            //Verifica Localidade
            if (!lLocalidadeVO.Exists(delegate(LocalidadeVO x) { return x.CodLocalidade == tempLocalidade.CodLocalidade; }))
            {
                //Adiciona Departamento
                tempLocalidade.lDepartamento.Add(tempDepartamento);
                //Adiciona Localidade
                lLocalidadeVO.Add(tempLocalidade);
            }
            else
            {
                //Verifica Departamento
                tempLocalidade = lLocalidadeVO.Find(delegate(LocalidadeVO x)
                    { return x.CodLocalidade == tempLocalidade.CodLocalidade; });
                if (!tempLocalidade.lDepartamento.Exists(delegate(DepartamentoVO x)
                    { return x.CodDepartamento == tempDepartamento.CodDepartamento; }))
                {
                    //Adiciona Departamento
                    tempLocalidade.lDepartamento.Add(tempDepartamento);
                }
            }
        }

        //Contagem
        countUsuarios = lUsuarioVO.Count;
        countLocalidades = lLocalidadeVO.Count;

        //Monta Table
        TableRow tr;
        TableCell tc;

        //Cabeçalho
        tr = new TableRow();
        tc = new TableCell();

        tc.Text = "&nbsp;";
        tc.CssClass = "";
        tc.Width = Unit.Pixel(250);

        tr.Cells.Add(tc);

        foreach (UsuarioVO identUsuarioVO in lUsuarioVO)
        {
            tc = new TableCell();

            tc.Text = identUsuarioVO.DescLogin.ToUpper();
            tc.CssClass = "";
            tc.Font.Bold = true;
            tc.HorizontalAlign = HorizontalAlign.Center;
            tc.Width = Unit.Pixel(150);

            tr.Cells.Add(tc);
        }
        tblCategoriasFuncionais.Rows.Add(tr);



        //Localidades
        foreach (LocalidadeVO identLocalidadeVO in lLocalidadeVO)
        {
            tr = new TableRow();
            tc = new TableCell();
            tc.ColumnSpan = countUsuarios + 1;
            tc.Height = Unit.Pixel(1);
            tc.BackColor = System.Drawing.Color.Black;
            tr.Cells.Add(tc);
            tblCategoriasFuncionais.Rows.Add(tr);

            tr = new TableRow();
            tc = new TableCell();

            tc.Text = identLocalidadeVO.NomLocalidade.ToUpper();
            tc.CssClass = "tituloCampo";
            tc.HorizontalAlign = HorizontalAlign.Left;

            tr.Cells.Add(tc);

            for (int i = 0; i < countUsuarios; i++)
            {
                tc = new TableCell();

                tc.Text = "&nbsp;";
                tc.CssClass = "fundoCelCampo";

                tr.Cells.Add(tc);
            }

            tblCategoriasFuncionais.Rows.Add(tr);

            //Departamentos
            foreach (DepartamentoVO identDepartamentoVO in identLocalidadeVO.lDepartamento)
            {

                tr = new TableRow();
                tc = new TableCell();
                tc.ColumnSpan = countUsuarios + 1;
                tc.Height = Unit.Pixel(1);
                tc.BackColor = System.Drawing.Color.LightGray;
                tr.Cells.Add(tc);
                tblCategoriasFuncionais.Rows.Add(tr);

                tr = new TableRow();
                tc = new TableCell();

                tc.Text = identDepartamentoVO.DescDepartamento;
                tc.Font.Bold = true;
                tc.CssClass = "";
                tc.HorizontalAlign = HorizontalAlign.Right;

                tr.Cells.Add(tc);

                //Categoria Funcional
                foreach (UsuarioVO identUsuarioVO in lUsuarioVO)
                {
                    tc = new TableCell();
                    tc.CssClass = "";

                    List<UsuarioDepartamentoCategoriaVO> templUsuarioDepartamentoCategoriaVO;
                    templUsuarioDepartamentoCategoriaVO = lUsuarioDepartamentoCategoriaVO.FindAll(delegate(UsuarioDepartamentoCategoriaVO x)
                        {
                            return x.Usuario.CodUsuario == identUsuarioVO.CodUsuario &&
                                x.Departamento.CodDepartamento == identDepartamentoVO.CodDepartamento;
                        });

                    if (templUsuarioDepartamentoCategoriaVO.Count > 0)
                    {
                        foreach (UsuarioDepartamentoCategoriaVO tempUsuarioDepartamentoCategoriaVO in templUsuarioDepartamentoCategoriaVO)
                        {
                            tc.Text += tempUsuarioDepartamentoCategoriaVO.CategoriaFuncional.DescCategoriaFuncional + "<br>";
                        }
                        tc.Text.PadRight(4);
                        tc.HorizontalAlign = HorizontalAlign.Center;
                    }
                    else
                    {
                        tc.Text = "&nbsp;";
                    }


                    tr.Cells.Add(tc);
                }

                tblCategoriasFuncionais.Rows.Add(tr);



            }
        }


    }

    #endregion*/
}
