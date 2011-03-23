#region Using
using System;
using System.Text;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using Nissi.Model;
using Nissi.Business;
using System.Globalization;
using System.Collections.Generic;
using Nissi.Util;

#endregion

    public partial class CadastraMateriaPrima : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //Pesquisar();
                //updGrid.Update();
                if (Request.QueryString["popup"] != null && Request.QueryString["popup"].ToString() == "sim")
                {
                    //ArmazenaValorSessao("TipoAcao", "Incluir");
                    hdfTipoAcao.Value = "Incluir";
                    hdfCadastroPopup.Value = "sim";
                    this.Master.InibirTopo();
                    CarregarCombos();
                    //cria referência a variável que vai ser armazenada no ViewState
                    List<ComposicaoMateriaPrimaVO> lstComposicaoMateriaPrima = new List<ComposicaoMateriaPrimaVO>();
                    grdComposicaoMateriaPrima.DataSource = lstComposicaoMateriaPrima;
                    grdComposicaoMateriaPrima.DataBind();
                    //este grid só salvará quando salvar a Matéria Prima inteira
                    ViewState.Add("lstComposicaoMateriaPrima", lstComposicaoMateriaPrima.ToArray());
                    //cria referência a variável que vai ser armazenada no ViewState
                    List<ResistenciaTracaoVO> lstResistenciaTracao = new List<ResistenciaTracaoVO>();
                    grdResistenciaTracao.DataSource = lstResistenciaTracao;
                    grdResistenciaTracao.DataBind();
                    //este grid só salvará quando salvar a Matéria Prima inteira
                    ViewState.Add("lstResistenciaTracao", lstResistenciaTracao.ToArray());
                    TabContainer1.ActiveTabIndex = 0;
                    Master.PosicionarFoco(ddlNorma);
                    mpeIncluirMateriaPrima.Show();
                }
            }
        }

        #region Propriedades
        public MateriaPrimaVO DadosMateriaPrima
        {
            set
            {
                if (value.CodMateriaPrima > 0)
                    hdfCodMateriaPrima.Value = value.CodMateriaPrima.ToString();
                ddlNorma.SelectedValue = value.NormaVo.CodNorma.ToString();
                if (value.ClasseTipoVo != null)
                    ddlClasseTipo.SelectedValue = value.ClasseTipoVo.CodClasseTipo.ToString();
                else
                {
                    ddlClasseTipo.SelectedIndex = 0;
                }
                //setar valores do icms
                grdComposicaoMateriaPrima.DataSource = value.ComposicaoMateriaPrimaVos;
                grdComposicaoMateriaPrima.DataBind();
                ViewState["lstComposicaoMateriaPrima"] = value.ComposicaoMateriaPrimaVos.ToArray();

                //setar valores do icms
                grdResistenciaTracao.DataSource = value.ResistenciaTracaoVos;
                grdResistenciaTracao.DataBind();
                ViewState["lstResistenciaTracao"] = value.ResistenciaTracaoVos.ToArray();

            }
            get
            {
                MateriaPrimaVO identMateriaPrima = new MateriaPrimaVO();
                identMateriaPrima.NormaVo.CodNorma = !string.IsNullOrEmpty(ddlNorma.SelectedValue)? int.Parse(ddlNorma.SelectedValue): 0;
                if (ddlClasseTipo.SelectedValue != "0" && !string.IsNullOrEmpty(ddlClasseTipo.SelectedValue))
                    identMateriaPrima.ClasseTipoVo.CodClasseTipo = int.Parse(ddlClasseTipo.SelectedValue);
                else
                    identMateriaPrima.ClasseTipoVo.CodClasseTipo = null;
                if (!string.IsNullOrEmpty(hdfCodMateriaPrima.Value))
                    identMateriaPrima.CodMateriaPrima = Convert.ToInt16(hdfCodMateriaPrima.Value);
                //setar valores do icms
                ComposicaoMateriaPrimaVO[] lstComposicaoMateriaPrima = (ComposicaoMateriaPrimaVO[])ViewState["lstComposicaoMateriaPrima"];
                List<ComposicaoMateriaPrimaVO> newlstComposicaoMateriaPrima = new List<ComposicaoMateriaPrimaVO>(lstComposicaoMateriaPrima);
                identMateriaPrima.ComposicaoMateriaPrimaVos = newlstComposicaoMateriaPrima;
                //setar valores do icms
                ResistenciaTracaoVO[] lstResistenciaTracao = (ResistenciaTracaoVO[])ViewState["lstResistenciaTracao"];
                List<ResistenciaTracaoVO> newlstResistenciaTracao = new List<ResistenciaTracaoVO>(lstResistenciaTracao);
                identMateriaPrima.ResistenciaTracaoVos = newlstResistenciaTracao;
                return identMateriaPrima;
            }
        }


        #endregion
        #region Métodos
        private void Pesquisar()
        {
            int codigo;
            string descricao = string.Empty ;
            List<MateriaPrimaVO> lstMateriaPrima = new List<MateriaPrimaVO>(); 
            if (!string.IsNullOrEmpty(hdfCodigo.Value) && !hdfCodigo.Value.Equals("null"))
            {
                
                codigo = Convert.ToInt32(hdfCodigo.Value);
                if (rbNorma.Checked)
                {
                    var materiaPrima = new MateriaPrima().ListarPorCodigo(codigo);
                    lstMateriaPrima.Add(materiaPrima);
                }
                else
                {
                    lstMateriaPrima = new MateriaPrima().ListarPorClasseTipo(codigo);
                }
                
                grdListaResultado.DataSource = lstMateriaPrima;
                grdListaResultado.DataBind();
                grdListaResultado.Visible = true;
            }
            else
            {
                if ((hdfTipoAcao.Value == "Incluir" || hdfTipoAcao.Value == "Editar" || hdfTipoAcao.Value == "IncluirItem") && (!string.IsNullOrEmpty(hdfCodMateriaPrima.Value)))
                {
                    if (!string.IsNullOrEmpty(hdfCodMateriaPrima.Value))
                    {
                       codigo = Convert.ToInt32(hdfCodMateriaPrima.Value);
                       var materiaPrima = new MateriaPrima().ListarPorCodigo(codigo);
                       lstMateriaPrima.Add(materiaPrima);
                    }
                    LimparCampos();
                }
                else
                {
                    descricao = txtNorma.Text;
                }

                lstMateriaPrima = new MateriaPrima().ListarPorNorma(descricao);
                if (lstMateriaPrima.Count > 0)
                {
                    grdListaResultado.DataSource = lstMateriaPrima;
                    grdListaResultado.DataBind();
                    grdListaResultado.Visible = true;
                }
                else
                {
                    grdListaResultado.Visible = false;
                }
                LimparCampos();
            }
        }

        private void LimparCampos()
        {
            hdfCodMateriaPrima.Value = "";
            ddlNorma.SelectedIndex =
            ddlClasseTipo.SelectedIndex = 0;
        }
        private void CarregarCombos()
        {
            Geral.CarregarDDL(ref ddlNorma, new Norma().Listar().ToArray(), "CodNorma", "Descricao");
            Geral.CarregarDDL(ref ddlClasseTipo, new ClasseTipo().Listar().ToArray(), "CodClasseTipo", "Descricao");
            Geral.CarregarDDL(ref ddlBitola, new Bitola().Listar().ToArray(),"CodBitola", "Bitola");
            ddlClasseTipo.Items.Insert(0,"");
        }
        #endregion
        #region Métodos da Grid

        protected void grdListaResultado_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void grdListaResultado_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (!e.CommandName.Equals("Page"))
            {
                MateriaPrimaVO identMateriaPrima = new MateriaPrimaVO();
                identMateriaPrima.CodMateriaPrima = Convert.ToInt16(e.CommandArgument);
                if (e.CommandName == "Excluir")
                {
                    new MateriaPrima().Excluir(Convert.ToInt32(identMateriaPrima.CodMateriaPrima), UsuarioAtivo.CodFuncionario.Value);
                    Pesquisar();
                }
                else if (e.CommandName == "Editar")
                {
                    hdfTipoAcao.Value = "Editar";
                    hdfTipoAcaoComposicaoMateriaPrima.Value = "Incluir";
                    hdfTipoAcaoResistenciaTracao.Value = "Incluir";
                    CarregarCombos();
                    DadosMateriaPrima = new MateriaPrima().ListarTudo(identMateriaPrima.CodMateriaPrima);
                    Master.PosicionarFoco(ddlNorma);
                    TabContainer1.ActiveTabIndex = 0;
                    mpeIncluirMateriaPrima.Show();
                }

            }
        }

        protected void grdListaResultado_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                MateriaPrimaVO tempMateriaPrima = (MateriaPrimaVO)e.Row.DataItem;

                e.Row.Cells[1].Text = tempMateriaPrima.CodMateriaPrima.ToString();
                string descricao = tempMateriaPrima.NormaVo.Descricao + "/" + tempMateriaPrima.NormaVo.Revisao;
                if (tempMateriaPrima.ClasseTipoVo != null)
                    descricao = tempMateriaPrima.NormaVo.Descricao + "/" + tempMateriaPrima.NormaVo.Revisao +
                                tempMateriaPrima.ClasseTipoVo.Descricao;
                e.Row.Cells[2].Text = descricao;
                #region Botão Editar
                ImageButton imgEditar = (ImageButton)e.Row.FindControl("imgEditar");
                //imgEditar.ImageUrl = caminhoAplicacao + @"Imagens\editar.png";
                imgEditar.CommandArgument = tempMateriaPrima.CodMateriaPrima.ToString();
                imgEditar.CommandName = "Editar";
                imgEditar.Style.Add("cursor", "hand");
                imgEditar.ToolTip = "Editar dados do Matéria Prima [" + descricao + "]";
                #endregion

                #region Botão Excluir
                ImageButton imgExcluir = (ImageButton)e.Row.FindControl("imgExcluir");
               // imgExcluir.ImageUrl = caminhoAplicacao + @"Imagens\exclusao_Canc.png";
                imgExcluir.CommandArgument = tempMateriaPrima.CodMateriaPrima.ToString();
                imgExcluir.CommandName = "Excluir";
                imgExcluir.Attributes["onclick"] = "return confirm('Confirmar exclusão do Matéria Prima [" + descricao + "]?');";
                imgExcluir.Style.Add("cursor", "hand");
                imgExcluir.ToolTip = "Excluir Matéria Prima [" + descricao + "]";
                #endregion

                if (e.Row.RowState == DataControlRowState.Normal)
                    e.Row.CssClass = "FundoLinha1";
                else if (e.Row.RowState == DataControlRowState.Alternate)
                    e.Row.CssClass = "FundoLinha2";

            }
        }
        #endregion
        #region Métodos dos Botões


        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Default.aspx");
        }
        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (hdfTipoAcao.Value == "Incluir")
            {
                new MateriaPrima().Incluir(DadosMateriaPrima.ClasseTipoVo.CodClasseTipo, DadosMateriaPrima.NormaVo.CodNorma, UsuarioAtivo.CodFuncionario.Value, DadosMateriaPrima.ComposicaoMateriaPrimaVos, DadosMateriaPrima.ResistenciaTracaoVos );

            }
            else
            {
                new MateriaPrima().Alterar(DadosMateriaPrima.CodMateriaPrima, DadosMateriaPrima.ClasseTipoVo.CodClasseTipo, DadosMateriaPrima.NormaVo.CodNorma, UsuarioAtivo.CodFuncionario.Value, DadosMateriaPrima.ComposicaoMateriaPrimaVos, DadosMateriaPrima.ResistenciaTracaoVos);
            }

            if (hdfCadastroPopup.Value != "sim")
            {
                mpeIncluirMateriaPrima.Hide();
                LimparCampos();
                Pesquisar();
                updGrid.Update();
            }
            else
            {
                ExecutarScript(new StringBuilder("window.close()"));
            }

        }

        protected void btnIncluir_Click(object sender, EventArgs e)
        {
            hdfTipoAcao.Value = "Incluir";
            hdfTipoAcaoComposicaoMateriaPrima.Value = "Incluir";
            hdfTipoAcaoResistenciaTracao.Value = "Incluir";
            LimparCampos();
            CarregarCombos();
            //cria referência a variável que vai ser armazenada no ViewState
            List<ComposicaoMateriaPrimaVO> lstComposicaoMateriaPrima = new List<ComposicaoMateriaPrimaVO>();
            grdComposicaoMateriaPrima.DataSource = lstComposicaoMateriaPrima;
            grdComposicaoMateriaPrima.DataBind();
            //este grid só salvará quando salvar a Matéria Prima inteira
            ViewState.Add("lstComposicaoMateriaPrima", lstComposicaoMateriaPrima.ToArray());
            //cria referência a variável que vai ser armazenada no ViewState
            List<ResistenciaTracaoVO> lstResistenciaTracao = new List<ResistenciaTracaoVO>();
            grdResistenciaTracao.DataSource = lstResistenciaTracao;
            grdResistenciaTracao.DataBind();
            //este grid só salvará quando salvar a Matéria Prima inteira
            ViewState.Add("lstResistenciaTracao", lstResistenciaTracao.ToArray());
            TabContainer1.ActiveTabIndex = 0;
            Master.PosicionarFoco(ddlNorma);
            mpeIncluirMateriaPrima.Show();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            if (hdfCadastroPopup.Value != "sim")
            {
                LimparCampos();
                mpeIncluirMateriaPrima.Hide();

                List<ComposicaoMateriaPrimaVO> newlstComposicaoMateriaPrima = new List<ComposicaoMateriaPrimaVO>();
                grdComposicaoMateriaPrima.DataSource = newlstComposicaoMateriaPrima;
                grdComposicaoMateriaPrima.DataBind();
                ViewState.Add("lstComposicaoMateriaPrima", newlstComposicaoMateriaPrima.ToArray());

                List<ResistenciaTracaoVO> newlstResistenciaTracao = new List<ResistenciaTracaoVO>();
                grdResistenciaTracao.DataSource = newlstResistenciaTracao;
                grdResistenciaTracao.DataBind();
                ViewState.Add("lstResistenciaTracao", newlstResistenciaTracao.ToArray());

            }
            else
                ExecutarScript(new StringBuilder("window.close()"));

        }
        #endregion
        #region  Grid Composicao Quimica
        protected void grdComposicaoMateriaPrima_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void grdComposicaoMateriaPrima_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (!e.CommandName.Equals("Page"))
            {
                //forma de pegar o index do datagrid
                GridViewRow row = (GridViewRow) (((ImageButton) e.CommandSource).NamingContainer);
                int linha = row.RowIndex;
                //armazena em viewstate a linha selecionada para posterior atualização
                ViewState["LinhaSelecionadaComposicaoMateriaPrima"] = Convert.ToInt32(e.CommandArgument);
                int codComposicaoMateriaPrima = Convert.ToInt32(e.CommandArgument);
                ComposicaoMateriaPrimaVO[] lstComposicaoMateriaPrima =
                    (ComposicaoMateriaPrimaVO[]) ViewState["lstComposicaoMateriaPrima"];
                List<ComposicaoMateriaPrimaVO> newlstComposicaoMateriaPrima =
                    new List<ComposicaoMateriaPrimaVO>(lstComposicaoMateriaPrima);

                if (e.CommandName == "Editar")
                {
                    hdfTipoAcaoComposicaoMateriaPrima.Value = "Editar";
                    btnIncluirComposicaoMateriaPrima.Visible = false;
                    divCadastroComposicaoMateriaPrima.Visible = true;
                    divgrdComposicaoMateriaPrima.Visible = false;
                    //busca no vo os valores para a linha selecionada

                    //var item = new ComposicaoMateriaPrimaRepositorio().ListarPorCodigo(codMateriaPrima);
                    ////atribui aos campos da tela para alteração
                    //txtBitMin.Text = item.BitolaMinima.ToString();
                    //txtBitMax.Text = item.BitolaMaxima.ToString();
                    //txtCMin.Text = item.CMinimo.ToString();
                    //txtCMax.Text = item.CMaximo.ToString();
                    //txtSiMin.Text = item.SiMinimo.ToString();
                    //TxtSiMax.Text = item.SiMaximo.ToString();
                    //txtMnMin.Text = item.MnMinimo.ToString();
                    //txtMnMax.Text = item.MnMaximo.ToString();
                    //txtPMin.Text = item.PMinimo.ToString();
                    //txtPMax.Text = item.PMaximo.ToString();
                    //txtSMin.Text = item.SMinimo.ToString();
                    //txtSMax.Text = item.SMaximo.ToString();
                    //txtCrMin.Text = item.CrMinimo.ToString();
                    //txtCrMax.Text = item.CrMaximo.ToString();
                    //txtNiMin.Text = item.NiMinimo.ToString();
                    //txtNiMax.Text = item.NiMaximo.ToString();
                    //txtMoMin.Text = item.MoMinimo.ToString();
                    //txtMoMax.Text = item.MoMaximo.ToString();
                    //txtCuMin.Text = item.CuMinimo.ToString();
                    //txtCuMax.Text = item.CuMaximo.ToString();
                    //txtTiMin.Text = item.TiMinimo.ToString();
                    //txtTiMax.Text = item.TiMaximo.ToString();
                    //txtN2Min.Text = item.N2Minimo.ToString();
                    //txtN2Max.Text = item.N2Maximo.ToString();
                    //txtCoMin.Text = item.CoMinimo.ToString();
                    //txtCoMax.Text = item.CoMaximo.ToString();
                    //txtAlMin.Text = item.AlMinimo.ToString();
                    //txtAlMax.Text = item.AlMaximo.ToString();
                    //txtZnMin.Text = item.ZnMinimo.ToString();
                    //txtZnMax.Text = item.ZnMaximo.ToString();
                    //txtSnMin.Text = item.SnMinimo.ToString();
                    //txtSnMax.Text = item.SnMaximo.ToString();
                    //txtPbMin.Text = item.PbMinimo.ToString();
                    //txtPbMax.Text = item.PbMaximo.ToString();
                    //txtFeMin.Text = item.FeMinimo.ToString();
                    //txtFeMax.Text = item.FeMaximo.ToString();
                    //exibe panel com os controles de ComposicaoMateriaPrima
                    //divCadastroComposicaoMateriaPrima.Visible = true;
                    updComposicaoMateriaPrima.Update();
                }
                else if (e.CommandName == "Excluir")
                {
                    if (codComposicaoMateriaPrima > 0)
                        new MateriaPrima().ExcluirComposicaoMateriaPrima(codComposicaoMateriaPrima);
                    newlstComposicaoMateriaPrima.RemoveAt(linha);

                    grdComposicaoMateriaPrima.DataSource = newlstComposicaoMateriaPrima;
                    grdComposicaoMateriaPrima.DataBind();
                    updComposicaoMateriaPrima.Update();

                    //atualiza viewstate
                    ViewState["lstComposicaoMateriaPrima"] = newlstComposicaoMateriaPrima.ToArray();
                }
            }
        }

        protected void grdComposicaoMateriaPrima_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ComposicaoMateriaPrimaVO tempComposicaoMateriaPrima = (ComposicaoMateriaPrimaVO)e.Row.DataItem;
                e.Row.Cells[1].Text = tempComposicaoMateriaPrima.BitolaMinima+"-"+tempComposicaoMateriaPrima.BitolaMaxima;
                e.Row.Cells[2].Text = tempComposicaoMateriaPrima.CMinimo + "-" + tempComposicaoMateriaPrima.CMaximo;
                e.Row.Cells[3].Text = tempComposicaoMateriaPrima.SiMinimo + "-" + tempComposicaoMateriaPrima.SiMaximo;
                e.Row.Cells[4].Text = tempComposicaoMateriaPrima.MnMinimo + "-" + tempComposicaoMateriaPrima.MnMaximo;
                e.Row.Cells[5].Text = tempComposicaoMateriaPrima.PMinimo + "-" + tempComposicaoMateriaPrima.PMaximo;
                e.Row.Cells[6].Text = tempComposicaoMateriaPrima.SMinimo + "-" + tempComposicaoMateriaPrima.SMaximo;
                e.Row.Cells[7].Text = tempComposicaoMateriaPrima.CrMinimo + "-" + tempComposicaoMateriaPrima.CrMaximo;
                e.Row.Cells[8].Text = tempComposicaoMateriaPrima.NiMinimo + "-" + tempComposicaoMateriaPrima.NiMaximo;
                e.Row.Cells[9].Text = tempComposicaoMateriaPrima.MoMinimo + "-" + tempComposicaoMateriaPrima.MoMaximo;
                e.Row.Cells[10].Text = tempComposicaoMateriaPrima.CuMinimo + "-" + tempComposicaoMateriaPrima.CuMaximo;
                e.Row.Cells[11].Text = tempComposicaoMateriaPrima.TiMinimo + "-" + tempComposicaoMateriaPrima.TiMaximo;
                e.Row.Cells[12].Text = tempComposicaoMateriaPrima.N2Minimo + "-" + tempComposicaoMateriaPrima.N2Maximo;
                e.Row.Cells[13].Text = tempComposicaoMateriaPrima.CoMinimo + "-" + tempComposicaoMateriaPrima.CoMaximo;
                e.Row.Cells[14].Text = tempComposicaoMateriaPrima.AlMinimo + "-" + tempComposicaoMateriaPrima.AlMaximo;
                e.Row.Cells[15].Text = tempComposicaoMateriaPrima.ZnMinimo + "-" + tempComposicaoMateriaPrima.ZnMaximo;
                e.Row.Cells[16].Text = tempComposicaoMateriaPrima.SnMinimo + "-" + tempComposicaoMateriaPrima.SnMaximo;
                e.Row.Cells[17].Text = tempComposicaoMateriaPrima.PbMinimo + "-" + tempComposicaoMateriaPrima.PbMaximo;
                e.Row.Cells[18].Text = tempComposicaoMateriaPrima.FeMinimo + "-" + tempComposicaoMateriaPrima.FeMaximo;


                #region Botão Editar
                ImageButton imgEditar = (ImageButton)e.Row.FindControl("imgEditar");
                imgEditar.ImageUrl = caminhoAplicacao + @"Imagens\editar.png";
                imgEditar.CommandArgument = tempComposicaoMateriaPrima.CodComposicaoMateriaPrima.ToString();
                imgEditar.CommandName = "Editar";
                imgEditar.Style.Add("cursor", "hand");
                imgEditar.ToolTip = "Editar dados do Composição Química";
                #endregion

                #region Botão Excluir
                ImageButton imgExcluir = (ImageButton)e.Row.FindControl("imgExcluir");
                imgExcluir.ImageUrl = caminhoAplicacao + @"Imagens\exclusao_Canc.png";
                imgExcluir.CommandArgument = tempComposicaoMateriaPrima.CodComposicaoMateriaPrima.ToString();
                imgExcluir.CommandName = "Excluir";
                imgExcluir.Attributes["onclick"] = "return confirm('Confirmar exclusão do Composição Química?');";
                imgExcluir.Style.Add("cursor", "hand");
                imgExcluir.ToolTip = "Excluir Composição Química";
                #endregion

                if (e.Row.RowState == DataControlRowState.Normal)
                    e.Row.CssClass = "FundoLinha1";
                else if (e.Row.RowState == DataControlRowState.Alternate)
                    e.Row.CssClass = "FundoLinha2";
            }
        }
        #endregion
        #region Grid Resistencia Tração
        protected void grdResistenciaTracao_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            
        }

        protected void grdResistenciaTracao_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (!e.CommandName.Equals("Page"))
            {
                //forma de pegar o index do datagrid
                GridViewRow row = (GridViewRow) (((ImageButton) e.CommandSource).NamingContainer);
                int linha = row.RowIndex;
                string[] codigos = e.CommandArgument.ToString().Split('|');
                int codResistenciaTracao = Convert.ToInt32(codigos[0]);
                int codBitola = Convert.ToInt32(codigos[1]);
                //armazena em viewstate a linha selecionada para posterior atualização
                ViewState["LinhaSelecionadaResistenciaTracao"] = codBitola;
                ResistenciaTracaoVO[] lstResistenciaTracao = (ResistenciaTracaoVO[]) ViewState["lstResistenciaTracao"];
                List<ResistenciaTracaoVO> newlstResistenciaTracao = new List<ResistenciaTracaoVO>(lstResistenciaTracao);

                if (e.CommandName == "Editar")
                {
                    hdfTipoAcaoResistenciaTracao.Value = "Editar";


                    var item =
                        newlstResistenciaTracao.Where(r => r.CodResistenciaTracao == codResistenciaTracao).Select(r => r)
                            .FirstOrDefault();
                    //busca no vo os valores para a linha selecionada

                    //atribui aos campos da tela para alteração
                    ddlBitola.SelectedValue = item.Bitola.CodBitola.ToString();
                    txtTolerancia.Text = item.Tolerancia.ToString();
                    txtMinimo.Text = item.Minimo.ToString();
                }
                else if (e.CommandName == "Excluir")
                {
                    if (codResistenciaTracao > 0)
                        new MateriaPrima().ExcluirResistenciaTracao(codResistenciaTracao);
                    newlstResistenciaTracao.RemoveAt(linha);

                    grdResistenciaTracao.DataSource = newlstResistenciaTracao;
                    grdResistenciaTracao.DataBind();
                    updResistenciaTracao.Update();
                    //atualiza viewstate
                    ViewState["lstResistenciaTracao"] = newlstResistenciaTracao.ToArray();
                }
            }
        }

        protected void grdResistenciaTracao_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ResistenciaTracaoVO identResistenciaTracao = (ResistenciaTracaoVO)e.Row.DataItem;
                e.Row.Cells[1].Text = identResistenciaTracao.Bitola.Bitola.ToString();
                e.Row.Cells[2].Text = identResistenciaTracao.Tolerancia.ToString();
                e.Row.Cells[3].Text = identResistenciaTracao.Minimo.ToString();
                e.Row.Cells[4].Text = identResistenciaTracao.Maximo.ToString();

                #region Botão Editar
                ImageButton imgEditarResistenciaTracao = (ImageButton)e.Row.FindControl("imgEditarResistenciaTracao");
                imgEditarResistenciaTracao.ImageUrl = caminhoAplicacao + @"Imagens\editar.png";
                imgEditarResistenciaTracao.CommandArgument = identResistenciaTracao.CodResistenciaTracao+"|"+identResistenciaTracao.Bitola.CodBitola;
                imgEditarResistenciaTracao.CommandName = "Editar";
                imgEditarResistenciaTracao.Style.Add("cursor", "hand");
                imgEditarResistenciaTracao.ToolTip = "Editar dados da Resistência a Tração";
                #endregion

                #region Botão Excluir   
                ImageButton imgExcluirResistenciaTracao = (ImageButton)e.Row.FindControl("imgExcluirResistenciaTracao");
                imgExcluirResistenciaTracao.ImageUrl = caminhoAplicacao + @"Imagens\exclusao_Canc.png";
                imgExcluirResistenciaTracao.CommandArgument = identResistenciaTracao.CodResistenciaTracao + "|" + identResistenciaTracao.Bitola.CodBitola;
                imgExcluirResistenciaTracao.CommandName = "Excluir";
                imgExcluirResistenciaTracao.Attributes["onclick"] = "return confirm('Confirmar exclusão da Resistência a Tração?');";
                imgExcluirResistenciaTracao.Style.Add("cursor", "hand");
                imgExcluirResistenciaTracao.ToolTip = "Excluir Resistência a Tração";
                #endregion

                if (e.Row.RowState == DataControlRowState.Normal)
                    e.Row.CssClass = "FundoLinha1";
                else if (e.Row.RowState == DataControlRowState.Alternate)
                    e.Row.CssClass = "FundoLinha2";
            }
        }
        #endregion
        protected void btnIncluirComposicaoMateriaPrima_Click(object sender, EventArgs e)
        {
            //será incluido no grid de ComposicaoMateriaPrima manualmente (não incluirá no banco ainda)
            //pois só deverá ser incluido no banco quando salvar o produto

            ComposicaoMateriaPrimaVO[] lstComposicaoMateriaPrima = (ComposicaoMateriaPrimaVO[])ViewState["lstComposicaoMateriaPrima"];
            List<ComposicaoMateriaPrimaVO> newlstComposicaoMateriaPrima = new List<ComposicaoMateriaPrimaVO>(lstComposicaoMateriaPrima);

            //se for edição de ComposicaoMateriaPrima, atualizar o list
            if (hdfTipoAcaoComposicaoMateriaPrima.Value.Equals("Incluir"))
            {
                //senão, incluir novo ítem no list
                ComposicaoMateriaPrimaVO lstComposicaoMateriaPrimaAux = new ComposicaoMateriaPrimaVO();
                lstComposicaoMateriaPrimaAux.BitolaMinima = !string.IsNullOrEmpty(txtBitMin.Text) ? decimal.Parse(txtBitMin.Text) : 0;
                lstComposicaoMateriaPrimaAux.BitolaMaxima = !string.IsNullOrEmpty(txtBitMax.Text) ? decimal.Parse(txtBitMax.Text) : 0;
                lstComposicaoMateriaPrimaAux.CMinimo = !string.IsNullOrEmpty(txtCMin.Text) ? decimal.Parse(txtCMin.Text) : 0;
                lstComposicaoMateriaPrimaAux.CMaximo = !string.IsNullOrEmpty(txtCMax.Text) ? decimal.Parse(txtCMax.Text) : 0;
                lstComposicaoMateriaPrimaAux.SiMinimo = !string.IsNullOrEmpty(txtSiMin.Text) ? decimal.Parse(txtSiMin.Text) : 0;
                lstComposicaoMateriaPrimaAux.SiMaximo = !string.IsNullOrEmpty(TxtSiMax.Text) ? decimal.Parse(TxtSiMax.Text) : 0;
                lstComposicaoMateriaPrimaAux.MnMinimo = !string.IsNullOrEmpty(txtMnMin.Text) ? decimal.Parse(txtMnMin.Text) : 0;
                lstComposicaoMateriaPrimaAux.MnMaximo = !string.IsNullOrEmpty(txtMnMax.Text) ? decimal.Parse(txtMnMax.Text) : 0;
                lstComposicaoMateriaPrimaAux.PMinimo = !string.IsNullOrEmpty(txtPMin.Text) ? decimal.Parse(txtPMin.Text) : 0;
                lstComposicaoMateriaPrimaAux.PMaximo = !string.IsNullOrEmpty(txtPMax.Text) ? decimal.Parse(txtPMax.Text) : 0;
                lstComposicaoMateriaPrimaAux.SMinimo = !string.IsNullOrEmpty(txtSMin.Text) ? decimal.Parse(txtSMin.Text) : 0;
                lstComposicaoMateriaPrimaAux.SMaximo = !string.IsNullOrEmpty(txtSMax.Text) ? decimal.Parse(txtSMax.Text) : 0;
                lstComposicaoMateriaPrimaAux.CrMinimo = !string.IsNullOrEmpty(txtCrMin.Text) ? decimal.Parse(txtCrMin.Text) : 0;
                lstComposicaoMateriaPrimaAux.CrMaximo = !string.IsNullOrEmpty(txtCrMax.Text) ? decimal.Parse(txtCrMax.Text) : 0;
                lstComposicaoMateriaPrimaAux.NiMinimo = !string.IsNullOrEmpty(txtNiMin.Text) ? decimal.Parse(txtNiMin.Text) : 0;
                lstComposicaoMateriaPrimaAux.NiMaximo = !string.IsNullOrEmpty(txtNiMax.Text) ? decimal.Parse(txtNiMax.Text) : 0;
                lstComposicaoMateriaPrimaAux.MoMinimo = !string.IsNullOrEmpty(txtMoMin.Text) ? decimal.Parse(txtMoMin.Text) : 0;
                lstComposicaoMateriaPrimaAux.MoMaximo = !string.IsNullOrEmpty(txtMoMax.Text) ? decimal.Parse(txtMoMax.Text) : 0;
                lstComposicaoMateriaPrimaAux.CuMinimo = !string.IsNullOrEmpty(txtCuMin.Text) ? decimal.Parse(txtCuMin.Text) : 0;
                lstComposicaoMateriaPrimaAux.CuMaximo = !string.IsNullOrEmpty(txtCuMax.Text) ? decimal.Parse(txtCuMax.Text) : 0;
                lstComposicaoMateriaPrimaAux.TiMinimo = !string.IsNullOrEmpty(txtTiMin.Text) ? decimal.Parse(txtTiMin.Text) : 0;
                lstComposicaoMateriaPrimaAux.TiMaximo = !string.IsNullOrEmpty(txtTiMax.Text) ? decimal.Parse(txtTiMax.Text) : 0;
                lstComposicaoMateriaPrimaAux.N2Minimo = !string.IsNullOrEmpty(txtN2Min.Text) ? decimal.Parse(txtN2Min.Text) : 0;
                lstComposicaoMateriaPrimaAux.N2Maximo = !string.IsNullOrEmpty(txtN2Max.Text) ? decimal.Parse(txtN2Max.Text) : 0;
                lstComposicaoMateriaPrimaAux.CoMinimo = !string.IsNullOrEmpty(txtCoMin.Text) ? decimal.Parse(txtCoMin.Text) : 0;
                lstComposicaoMateriaPrimaAux.CoMaximo = !string.IsNullOrEmpty(txtCoMax.Text) ? decimal.Parse(txtCoMax.Text) : 0;
                lstComposicaoMateriaPrimaAux.AlMinimo = !string.IsNullOrEmpty(txtAlMin.Text) ? decimal.Parse(txtAlMin.Text) : 0;
                lstComposicaoMateriaPrimaAux.AlMaximo = !string.IsNullOrEmpty(txtAlMax.Text) ? decimal.Parse(txtAlMax.Text) : 0;
                lstComposicaoMateriaPrimaAux.ZnMinimo = !string.IsNullOrEmpty(txtZnMin.Text) ? decimal.Parse(txtZnMin.Text) : 0;
                lstComposicaoMateriaPrimaAux.ZnMaximo = !string.IsNullOrEmpty(txtZnMax.Text) ? decimal.Parse(txtZnMax.Text) : 0;
                lstComposicaoMateriaPrimaAux.SnMinimo = !string.IsNullOrEmpty(txtSnMin.Text) ? decimal.Parse(txtSnMin.Text) : 0;
                lstComposicaoMateriaPrimaAux.SnMaximo = !string.IsNullOrEmpty(txtSnMax.Text) ? decimal.Parse(txtSnMax.Text) : 0;
                lstComposicaoMateriaPrimaAux.PbMinimo = !string.IsNullOrEmpty(txtPbMin.Text) ? decimal.Parse(txtPbMin.Text) : 0;
                lstComposicaoMateriaPrimaAux.PbMaximo = !string.IsNullOrEmpty(txtPbMax.Text) ? decimal.Parse(txtPbMax.Text) : 0;
                lstComposicaoMateriaPrimaAux.FeMinimo = !string.IsNullOrEmpty(txtFeMin.Text) ? decimal.Parse(txtFeMin.Text) : 0;
                lstComposicaoMateriaPrimaAux.FeMaximo = !string.IsNullOrEmpty(txtFeMax.Text) ? decimal.Parse(txtFeMax.Text) : 0;
                newlstComposicaoMateriaPrima.Add(lstComposicaoMateriaPrimaAux);
            }
            else
            {
                int linha = Convert.ToInt32(ViewState["LinhaSelecionadaComposicaoMateriaPrima"]);
                var query = newlstComposicaoMateriaPrima.Where(c => c.CodComposicaoMateriaPrima == linha).Select(c => c);
                var item = query.FirstOrDefault();
                item.BitolaMinima = !string.IsNullOrEmpty(txtBitMin.Text) ? decimal.Parse(txtBitMin.Text) : 0;
                item.BitolaMaxima = !string.IsNullOrEmpty(txtBitMax.Text) ? decimal.Parse(txtBitMax.Text) : 0;
                item.CMinimo = !string.IsNullOrEmpty(txtCMin.Text) ? decimal.Parse(txtCMin.Text) : 0;
                item.CMaximo = !string.IsNullOrEmpty(txtCMax.Text) ? decimal.Parse(txtCMax.Text) : 0;
                item.SiMinimo = !string.IsNullOrEmpty(txtSiMin.Text) ? decimal.Parse(txtSiMin.Text) : 0;
                item.SiMaximo = !string.IsNullOrEmpty(TxtSiMax.Text) ? decimal.Parse(TxtSiMax.Text) : 0;
                item.MnMinimo = !string.IsNullOrEmpty(txtMnMin.Text) ? decimal.Parse(txtMnMin.Text) : 0;
                item.MnMaximo = !string.IsNullOrEmpty(txtMnMax.Text) ? decimal.Parse(txtMnMax.Text) : 0;
                item.PMinimo = !string.IsNullOrEmpty(txtPMin.Text) ? decimal.Parse(txtPMin.Text) : 0;
                item.PMaximo = !string.IsNullOrEmpty(txtPMax.Text) ? decimal.Parse(txtPMax.Text) : 0;
                item.SMinimo = !string.IsNullOrEmpty(txtSMin.Text) ? decimal.Parse(txtSMin.Text) : 0;
                item.SMaximo = !string.IsNullOrEmpty(txtSMax.Text) ? decimal.Parse(txtSMax.Text) : 0;
                item.CrMinimo = !string.IsNullOrEmpty(txtCrMin.Text) ? decimal.Parse(txtCrMin.Text) : 0;
                item.CrMaximo = !string.IsNullOrEmpty(txtCrMax.Text) ? decimal.Parse(txtCrMax.Text) : 0;
                item.NiMinimo = !string.IsNullOrEmpty(txtNiMin.Text) ? decimal.Parse(txtNiMin.Text) : 0;
                item.NiMaximo = !string.IsNullOrEmpty(txtNiMax.Text) ? decimal.Parse(txtNiMax.Text) : 0;
                item.MoMinimo = !string.IsNullOrEmpty(txtMoMin.Text) ? decimal.Parse(txtMoMin.Text) : 0;
                item.MoMaximo = !string.IsNullOrEmpty(txtMoMax.Text) ? decimal.Parse(txtMoMax.Text) : 0;
                item.CuMinimo = !string.IsNullOrEmpty(txtCuMin.Text) ? decimal.Parse(txtCuMin.Text) : 0;
                item.CuMaximo = !string.IsNullOrEmpty(txtCuMax.Text) ? decimal.Parse(txtCuMax.Text) : 0;
                item.TiMinimo = !string.IsNullOrEmpty(txtTiMin.Text) ? decimal.Parse(txtTiMin.Text) : 0;
                item.TiMaximo = !string.IsNullOrEmpty(txtTiMax.Text) ? decimal.Parse(txtTiMax.Text) : 0;
                item.N2Minimo = !string.IsNullOrEmpty(txtN2Min.Text) ? decimal.Parse(txtN2Min.Text) : 0;
                item.N2Maximo = !string.IsNullOrEmpty(txtN2Max.Text) ? decimal.Parse(txtN2Max.Text) : 0;
                item.CoMinimo = !string.IsNullOrEmpty(txtCoMin.Text) ? decimal.Parse(txtCoMin.Text) : 0;
                item.CoMaximo = !string.IsNullOrEmpty(txtCoMax.Text) ? decimal.Parse(txtCoMax.Text) : 0;
                item.AlMinimo = !string.IsNullOrEmpty(txtAlMin.Text) ? decimal.Parse(txtAlMin.Text) : 0;
                item.AlMaximo = !string.IsNullOrEmpty(txtAlMax.Text) ? decimal.Parse(txtAlMax.Text) : 0;
                item.ZnMinimo = !string.IsNullOrEmpty(txtZnMin.Text) ? decimal.Parse(txtZnMin.Text) : 0;
                item.ZnMaximo = !string.IsNullOrEmpty(txtZnMax.Text) ? decimal.Parse(txtZnMax.Text) : 0;
                item.SnMinimo = !string.IsNullOrEmpty(txtSnMin.Text) ? decimal.Parse(txtSnMin.Text) : 0;
                item.SnMaximo = !string.IsNullOrEmpty(txtSnMax.Text) ? decimal.Parse(txtSnMax.Text) : 0;
                item.PbMinimo = !string.IsNullOrEmpty(txtPbMin.Text) ? decimal.Parse(txtPbMin.Text) : 0;
                item.PbMaximo = !string.IsNullOrEmpty(txtPbMax.Text) ? decimal.Parse(txtPbMax.Text) : 0;
                item.FeMinimo = !string.IsNullOrEmpty(txtFeMin.Text) ? decimal.Parse(txtFeMin.Text) : 0;
                item.FeMaximo = !string.IsNullOrEmpty(txtFeMax.Text) ? decimal.Parse(txtFeMax.Text) : 0;
            }
            grdComposicaoMateriaPrima.DataSource = newlstComposicaoMateriaPrima;
            grdComposicaoMateriaPrima.DataBind();


            //atualiza viewstate
            ViewState["lstComposicaoMateriaPrima"] = newlstComposicaoMateriaPrima.ToArray();

            LimparCamposComposicaoMateriaPrima();
            btnIncluirComposicaoMateriaPrima.Visible = true;
            divCadastroComposicaoMateriaPrima.Visible = false;
            divgrdComposicaoMateriaPrima.Visible = true;
        }
        private void LimparCamposComposicaoMateriaPrima()
        {
                txtBitMin.Text =
                txtBitMax.Text =
                txtCMin.Text =
                txtCMax.Text =
                txtSiMin.Text =
                TxtSiMax.Text =
                txtMnMin.Text =
                txtMnMax.Text =
                txtPMin.Text =
                txtPMax.Text =
                txtSMin.Text =
                txtSMax.Text =
                txtCrMin.Text =
                txtCrMax.Text =
                txtNiMin.Text =
                txtNiMax.Text =
                txtMoMin.Text =
                txtMoMax.Text =
                txtCuMin.Text =
                txtCuMax.Text =
                txtTiMin.Text =
                txtTiMax.Text =
                txtN2Min.Text =
                txtN2Max.Text =
                txtCoMin.Text =
                txtCoMax.Text =
                txtAlMin.Text =
                txtAlMax.Text =
                txtZnMin.Text =
                txtZnMax.Text =
                txtSnMin.Text =
                txtSnMax.Text =
                txtPbMin.Text =
                txtPbMax.Text =
                txtFeMin.Text =
                txtFeMax.Text = string.Empty;
                hdfTipoAcaoComposicaoMateriaPrima.Value = "Incluir";
        }

        protected void btnIncluirResistenciaTracao_Click(object sender, EventArgs e)
        {
            //será incluido no grid de ICMS manualmente (não incluirá no banco ainda)
            //pois só deverá ser incluido no banco quando salvar o produto

            ResistenciaTracaoVO[] lstResistenciaTracao = (ResistenciaTracaoVO[])ViewState["lstResistenciaTracao"];
            List<ResistenciaTracaoVO> newlstResistenciaTracao = new List<ResistenciaTracaoVO>(lstResistenciaTracao);
            decimal tolerancia = !string.IsNullOrEmpty(txtTolerancia.Text)? decimal.Parse(txtTolerancia.Text):0;
            decimal minimo = !string.IsNullOrEmpty(txtMinimo.Text) ? decimal.Parse(txtMinimo.Text) : 0;
            decimal maximo = !string.IsNullOrEmpty(txtMaximo.Text) ? decimal.Parse(txtMaximo.Text) : 0;

            //se for edição de ICMS, atualizar o list
            if (hdfTipoAcaoResistenciaTracao.Value.Equals("Incluir"))
            {
                /************************************************************************
                Se a ação for inclusão, simplesmente verifica se o ítem já foi cadastrado
                se já for, exibe mensagem e não inclui o ítem
                /***********************************************************************/
                ResistenciaTracaoVO result = newlstResistenciaTracao.Find(
                delegate(ResistenciaTracaoVO bk)
                {
                    return bk.Bitola.CodBitola == int.Parse(ddlBitola.SelectedValue);
                }
                );
                if (result != null)
                {

                    MensagemCliente("Bitola já cadastrada!");
                    return;
                }
                /************************************************************************/
                /************************************************************************/

                //senão, incluir novo ítem no list
                ResistenciaTracaoVO lstResistenciaTracaoAux = new ResistenciaTracaoVO();
                if (!ddlBitola.SelectedValue.Trim().Equals(""))
                {
                    lstResistenciaTracaoAux.Bitola.CodBitola = int.Parse(ddlBitola.SelectedValue);
                    lstResistenciaTracaoAux.Bitola.Bitola = decimal.Parse(ddlBitola.SelectedItem.Text);
                }
                lstResistenciaTracaoAux.Tolerancia = tolerancia;
                lstResistenciaTracaoAux.Minimo = minimo;
                lstResistenciaTracaoAux.Maximo = maximo;
                newlstResistenciaTracao.Add(lstResistenciaTracaoAux);
            }
            else
            {
                /************************************************************************
                Se a ação for alteração, verifica se o ítem já está cadastrado, se já estiver
                será impedido, desde que não seja ele mesmo
                /***********************************************************************/
                int linha = Convert.ToInt32(ViewState["LinhaSelecionadaResistenciaTracao"]);
                if (linha != int.Parse(ddlBitola.SelectedValue))
                {
                    var item1 =
                        newlstResistenciaTracao.Where(r => r.Bitola.CodBitola == int.Parse(ddlBitola.SelectedValue)).Select(r => r).
                            FirstOrDefault();
                    if (item1 != null)
                    {
                        MensagemCliente("Bitola já cadastrada!");
                        return;
                    }
                }

                /************************************************************************
                Atualiza o item do grid        
                /***********************************************************************/
                var item =
                    newlstResistenciaTracao.Where(r => r.Bitola.CodBitola == linha).Select(r => r).
                        FirstOrDefault();
                item.Bitola.CodBitola = int.Parse(ddlBitola.SelectedValue);
                item.Bitola.Bitola = decimal.Parse(ddlBitola.SelectedItem.Text);
                item.Tolerancia = tolerancia;
                item.Minimo = minimo;
                item.Maximo = maximo;
                //sai do for
            }
            grdResistenciaTracao.DataSource = newlstResistenciaTracao;
            grdResistenciaTracao.DataBind();
            //atualiza viewstate
            ViewState["lstResistenciaTracao"] = newlstResistenciaTracao.ToArray();
            LimparCamposResistenciaTracao();
            Master.PosicionarFoco(ddlBitola);
            btnIncluir.Text = "Incluir";
        }
        private void LimparCamposResistenciaTracao()
        {
            ddlBitola.SelectedIndex = 0;
            txtTolerancia.Text =
                txtMinimo.Text =
                txtMaximo.Text = String.Empty;
            hdfTipoAcaoResistenciaTracao.Value = "Incluir";
        }

        protected void btngrdIncluirComposicaoMateriaPrima_Click(object sender, EventArgs e)
        {
            divCadastroComposicaoMateriaPrima.Visible = true;
            divgrdComposicaoMateriaPrima.Visible = false;
            hdfTipoAcaoComposicaoMateriaPrima.Value = "Incluir";
            Master.PosicionarFoco(txtBitMin);
            btnIncluirComposicaoMateriaPrima.Visible = false;
            TabContainer1.ActiveTabIndex = 0;
            LimparCamposComposicaoMateriaPrima();
        }

        protected void btnCancelarComposicaoMateriaPrima_Click(object sender, EventArgs e)
        {
            btnIncluirComposicaoMateriaPrima.Visible = true;
            divCadastroComposicaoMateriaPrima.Visible = false;
            divgrdComposicaoMateriaPrima.Visible = true;
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            Pesquisar();
        }
    }
