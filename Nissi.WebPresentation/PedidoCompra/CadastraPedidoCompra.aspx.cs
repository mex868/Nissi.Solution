using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using Nissi.Model;
using Nissi.Business;
using Nissi.Util;
using System.ServiceModel.Web;


public partial class CadastrarPedidoCompra : BasePage
{
    #region Propriedades
    #region DadosRazaoSocial
    private FornecedorVO DadosRazaoSocial
    {
        set {
            if (value.CodPessoa > 0)
            {
                value.Cep = PreencherCamposCEP(value.Cep);
                hdfCodigoFornecedor.Value = value.CodRef;
                hdfIdRazaoSocial.Value = value.CodPessoa.ToString();
                txtRazaoSocial.Text = value.RazaoSocial;
                txtCEP.Text = value.Cep.CodCep;
                txtCNPJ.Text = value.CNPJ;
                txtInscricaoEstatual.Text = value.InscricaoEstadual;
                txtEndereco.Text = value.Cep.NomEndereco +
                                                    (value.Numero.Trim().Equals("") ? "" : ", " + value.Numero) +
                                                    (value.Complemento.Trim().Equals("") ? "" : " - " + value.Complemento);
                txtBairro.Text = value.Cep.Bairro.NomBairro;
                txtMunicipio.Text = value.Cep.Cidade.NomCidade;
                txtUF.Text = value.Cep.Cidade.UF.CodUF;
                txtFoneFax.Text = value.Telefone + (value.Fax.Trim().Equals("") ? " " : " / " + value.Fax);
                txtContato.Text = value.Contato;
                txtEmail.Text = value.Email;
                txtSite.Text = value.Site;
            }
        }
        get
        {
            FornecedorVO FornecedorVO = new FornecedorVO();
            if (!string.IsNullOrEmpty(hdfIdRazaoSocial.Value))
                FornecedorVO.CodPessoa = Convert.ToInt32(hdfIdRazaoSocial.Value);
            return FornecedorVO;
        }
    }
    #endregion
    #region DadosPedidoCompra
    private PedidoCompraVO DadosPedidoCompra
    {
        set
        {
            hdfCodPedidoCompra.Value = value.CodPedidoCompra.ToString();
            txtPedidoCompra.Text = value.CodPedidoCompra.ToString().PadLeft(8,'0');
            if (value.DataEmissao != null)
            txtEmissao.Text = value.DataEmissao.ToString("dd/MM/yyyy");
            ddlCondicoesPgto.SelectedValue = value.FormaPgto.CodFormaPgto.ToString();
            ddlTipoRetirada.SelectedValue = value.TipoRetirada.ToString();
            if(value.FuncionarioComprador != null)
            {
                hdfCodComprador.Value = value.FuncionarioComprador.CodPessoa.ToString();
                txtComprador.Text = value.FuncionarioComprador.Nome;
            }
            if(value.FuncionarioAprovador != null)
            {
                hdfCodAprovador.Value = value.FuncionarioAprovador.CodPessoa.ToString();
                txtAprovador.Text = value.FuncionarioAprovador.Nome;
            }
            txtCondicoesFornecimento.Text = value.CondicaoFornecimento;
            txtObservacao.Text = value.Observacao;
            txtDataPrazoEntrega.Text = value.DataPrazoEntrega.ToString();
            DadosRazaoSocial = value.Fornecedor;
            grdProduto.DataSource = value.ItemPedidoCompraVo;
            grdProduto.DataBind();
        }
        get 
        {
            PedidoCompraVO identPedidoCompraVo = new PedidoCompraVO();
            ItemPedidoCompraVO[] lstitemPedidoCompraVO = (ItemPedidoCompraVO[])Session["lstItemPedidoCompra"];
            List<ItemPedidoCompraVO> newlstitemPedidoCompraVO = new List<ItemPedidoCompraVO>(lstitemPedidoCompraVO);
            if (!string.IsNullOrEmpty(hdfCodPedidoCompra.Value))
                identPedidoCompraVo.CodPedidoCompra = int.Parse(hdfCodPedidoCompra.Value);
            if (!string.IsNullOrEmpty(hdfIdRazaoSocial.Value))
                identPedidoCompraVo.Fornecedor.CodPessoa = int.Parse(hdfIdRazaoSocial.Value);
            txtPedidoCompra.Text = identPedidoCompraVo.CodPedidoCompra.ToString().PadLeft(8, '0');
            if (!string.IsNullOrEmpty(txtEmissao.Text))
                identPedidoCompraVo.DataEmissao = Convert.ToDateTime(txtEmissao.Text);
            if (!string.IsNullOrEmpty(txtDataPrazoEntrega.Text))
                identPedidoCompraVo.DataPrazoEntrega = Convert.ToDateTime(txtDataPrazoEntrega.Text);
            if (ddlCondicoesPgto.SelectedValue != null)
                identPedidoCompraVo.FormaPgto.CodFormaPgto = short.Parse(ddlCondicoesPgto.SelectedValue);
            identPedidoCompraVo.TipoRetirada = short.Parse(ddlTipoRetirada.SelectedValue);
            identPedidoCompraVo.FuncionarioComprador.CodPessoa = hdfCodComprador.Value != ""? int.Parse(hdfCodComprador.Value):0;
            identPedidoCompraVo.FuncionarioAprovador.CodPessoa = hdfCodAprovador.Value != ""? int.Parse(hdfCodAprovador.Value):0;
            identPedidoCompraVo.CondicaoFornecimento = txtCondicoesFornecimento.Text;
            identPedidoCompraVo.Observacao = txtObservacao.Text;
            identPedidoCompraVo.ItemPedidoCompraVo = newlstitemPedidoCompraVO;
            return identPedidoCompraVo; 
        }
    }
    #endregion
    #region DadosPedidoCompraInsumo
    private PedidoCompraInsumoVO DadosPedidoCompraInsumo
    {
        set
        {
            hdfCodPedidoCompra.Value = value.CodPedidoCompraInsumo.ToString();
            txtPedidoCompra.Text = value.CodPedidoCompraInsumo.ToString().PadLeft(8, '0');
            if (value.DataEmissao != null)
                txtEmissao.Text = value.DataEmissao.ToString("dd/MM/yyyy");
            ddlCondicoesPgto.SelectedValue = value.FormaPgto.CodFormaPgto.ToString();
            ddlTipoRetirada.SelectedValue = value.TipoRetirada.ToString();
            if (value.FuncionarioComprador != null)
            {
                hdfCodComprador.Value = value.FuncionarioComprador.CodPessoa.ToString();
                txtComprador.Text = value.FuncionarioComprador.Nome;
            }
            if (value.FuncionarioAprovador != null)
            {
                hdfCodAprovador.Value = value.FuncionarioAprovador.CodPessoa.ToString();
                txtAprovador.Text = value.FuncionarioAprovador.Nome;
            }
            txtCondicoesFornecimento.Text = value.CondicaoFornecimento;
            txtObservacao.Text = value.Observacao;
            txtDataPrazoEntrega.Text = value.DataPrazoEntrega.ToString();
            DadosRazaoSocial = value.Fornecedor;
            grdProdutoInsumo.DataSource = value.ItemPedidoCompraInsumoVo;
            grdProdutoInsumo.DataBind();
        }
        get
        {
            PedidoCompraInsumoVO identPedidoCompraInsumoVO = new PedidoCompraInsumoVO();
            ItemPedidoCompraInsumoVO[] lstItemPedidoCompraInsumoVO = (ItemPedidoCompraInsumoVO[])Session["lstItemPedidoCompraInsumo"];
            List<ItemPedidoCompraInsumoVO> newlstItemPedidoCompraInsumoVO = new List<ItemPedidoCompraInsumoVO>(lstItemPedidoCompraInsumoVO);
            if (!string.IsNullOrEmpty(hdfCodPedidoCompra.Value))
                identPedidoCompraInsumoVO.CodPedidoCompraInsumo = int.Parse(hdfCodPedidoCompra.Value);
            if (!string.IsNullOrEmpty(hdfIdRazaoSocial.Value))
                identPedidoCompraInsumoVO.Fornecedor.CodPessoa = int.Parse(hdfIdRazaoSocial.Value);
            txtPedidoCompra.Text = identPedidoCompraInsumoVO.CodPedidoCompraInsumo.ToString().PadLeft(8, '0');
            if (!string.IsNullOrEmpty(txtEmissao.Text))
                identPedidoCompraInsumoVO.DataEmissao = Convert.ToDateTime(txtEmissao.Text);
            if (!string.IsNullOrEmpty(txtDataPrazoEntrega.Text))
                identPedidoCompraInsumoVO.DataPrazoEntrega = Convert.ToDateTime(txtDataPrazoEntrega.Text);
            if (ddlCondicoesPgto.SelectedValue != null)
                identPedidoCompraInsumoVO.FormaPgto.CodFormaPgto = short.Parse(ddlCondicoesPgto.SelectedValue);
            identPedidoCompraInsumoVO.TipoRetirada = short.Parse(ddlTipoRetirada.SelectedValue);
            identPedidoCompraInsumoVO.FuncionarioComprador.CodPessoa = hdfCodComprador.Value != "" ? int.Parse(hdfCodComprador.Value) : 0;
            identPedidoCompraInsumoVO.FuncionarioAprovador.CodPessoa = hdfCodAprovador.Value != "" ? int.Parse(hdfCodAprovador.Value) : 0;
            identPedidoCompraInsumoVO.CondicaoFornecimento = txtCondicoesFornecimento.Text;
            identPedidoCompraInsumoVO.Observacao = txtObservacao.Text;
            identPedidoCompraInsumoVO.ItemPedidoCompraInsumoVo = newlstItemPedidoCompraInsumoVO;
            return identPedidoCompraInsumoVO;
        }
    }
    #endregion
    #endregion
    #region Métodos Privados
    #region PreencherCamposCEP
    //Todo: Está função poderia ser feita no próprio FornecedorData.cs
    //ver com Max
    private CEPVO PreencherCamposCEP(CEPVO lCEPVO)
    {
        if (!string.IsNullOrEmpty(lCEPVO.CodCep))
        {
            CEPVO lCEP = new CEP().Listar(lCEPVO);
            lCEPVO.Bairro = lCEP.Bairro;
            lCEPVO.Cidade = lCEP.Cidade;
            lCEPVO.NomEndereco = lCEP.NomEndereco;
            lCEPVO.TipoLogradouro = lCEP.TipoLogradouro;
        }

        return lCEPVO;
    }   
    #endregion
    #region LimparCampos
    private void LimparCampos()
    {
        Session.Remove("ItemPedidoCompra");
        Session.Remove("AcaoProduto");
    }
    #endregion
    #region CarregarCombos
    private void CarregarCombos()
    {
        Geral.CarregarDDL(ref ddlCondicoesPgto, new FormaPgto().Listar().ToArray(), "CodFormaPgto", "Descricao");
        Geral.CarregarDDL(ref ddlMateriaPrima, new MateriaPrima().Listar().ToArray(), "CodMateriaPrima", "Descricao");
        Geral.CarregarDDL(ref ddlBitola, new Bitola().Listar().ToArray(), "CodBitola", "Bitola");
        Geral.CarregarDDL(ref ddlUnidade, new Unidade().Listar(new UnidadeVO()).ToArray(), "CodUnidade", "TipoUnidade");
        Geral.CarregarDDL(ref ddlUnidadeInsumo, new Unidade().Listar(new UnidadeVO()).ToArray(), "CodUnidade", "TipoUnidade");
        Geral.CarregarDDL(ref ddlProdutoInsumo, ProdutoInsumo.Listar().ToArray(),"CodProdutoInsumo","Descricao");
    }   
    #endregion
    #region CreatePedido
    private int CreatePedido(TypePedido typePedido)
    {
        int codPedido = 0;
        switch (typePedido)
        {
            case TypePedido.Compra:
                codPedido = new PedidoCompra().Incluir(DadosPedidoCompra.Fornecedor.CodPessoa,
                                                             DadosPedidoCompra.DataEmissao,
                                                             DadosPedidoCompra.DataPrazoEntrega,
                                                             DadosPedidoCompra.TipoRetirada,
                                                             DadosPedidoCompra.FormaPgto.CodFormaPgto,
                                                             DadosPedidoCompra.CondicaoFornecimento,
                                                             DadosPedidoCompra.Observacao,
                                                             DadosPedidoCompra.FuncionarioAprovador.CodPessoa,
                                                             DadosPedidoCompra.FuncionarioComprador.CodPessoa, UsuarioAtivo.CodFuncionario.Value,
                                                             DadosPedidoCompra.ItemPedidoCompraVo, typePedido);
                break;
            case TypePedido.CompraInsumo:
                codPedido = new PedidoCompra().Incluir(DadosPedidoCompraInsumo.Fornecedor.CodPessoa,
                                                             DadosPedidoCompraInsumo.DataEmissao,
                                                             DadosPedidoCompraInsumo.DataPrazoEntrega,
                                                             DadosPedidoCompraInsumo.TipoRetirada,
                                                             DadosPedidoCompraInsumo.FormaPgto.CodFormaPgto,
                                                             DadosPedidoCompraInsumo.CondicaoFornecimento,
                                                             DadosPedidoCompraInsumo.Observacao,
                                                             DadosPedidoCompraInsumo.FuncionarioAprovador.CodPessoa,
                                                             DadosPedidoCompraInsumo.FuncionarioComprador.CodPessoa, UsuarioAtivo.CodFuncionario.Value,
                                                             DadosPedidoCompraInsumo.ItemPedidoCompraInsumoVo, typePedido);
                break;
        }
        return codPedido;
    }

    #endregion
    #region AlterPedido
    private int AlterPedido(TypePedido typePedido)
    {
        int codPedido = 0;
        switch (typePedido)
        {
            case TypePedido.Compra:
                new PedidoCompra().Alterar(DadosPedidoCompra.CodPedidoCompra,
                                           DadosPedidoCompra.Fornecedor.CodPessoa,
                                           DadosPedidoCompra.DataEmissao,
                                           DadosPedidoCompra.DataPrazoEntrega,
                                           DadosPedidoCompra.TipoRetirada,
                                           DadosPedidoCompra.FormaPgto.CodFormaPgto,
                                           DadosPedidoCompra.CondicaoFornecimento,
                                           DadosPedidoCompra.Observacao,
                                           DadosPedidoCompra.FuncionarioAprovador.CodPessoa,
                                           DadosPedidoCompra.FuncionarioComprador.CodPessoa, UsuarioAtivo.CodFuncionario.Value,
                                           DadosPedidoCompra.ItemPedidoCompraVo, typePedido);
                codPedido = DadosPedidoCompra.CodPedidoCompra;
                break;
            case TypePedido.CompraInsumo:
                new PedidoCompra().Alterar(DadosPedidoCompraInsumo.CodPedidoCompraInsumo,
                                           DadosPedidoCompraInsumo.Fornecedor.CodPessoa,
                                           DadosPedidoCompraInsumo.DataEmissao,
                                           DadosPedidoCompraInsumo.DataPrazoEntrega,
                                           DadosPedidoCompraInsumo.TipoRetirada,
                                           DadosPedidoCompraInsumo.FormaPgto.CodFormaPgto,
                                           DadosPedidoCompraInsumo.CondicaoFornecimento,
                                           DadosPedidoCompraInsumo.Observacao,
                                           DadosPedidoCompraInsumo.FuncionarioAprovador.CodPessoa,
                                           DadosPedidoCompraInsumo.FuncionarioComprador.CodPessoa, UsuarioAtivo.CodFuncionario.Value,
                                           DadosPedidoCompraInsumo.ItemPedidoCompraInsumoVo, typePedido);
                codPedido = DadosPedidoCompraInsumo.CodPedidoCompraInsumo;
                break;
        }
        return codPedido;
    }

    #endregion
    #region LimparCamposItemPedidoCompra
    private void LimparCamposItemPedidoCompra()
    {
        hdfTipoAcaoItemPedidoCompra.Value = "Incluir";
        hdfCodMateriaPrima.Value =
            hdfCodBitola.Value =
            txtNorma.Text =
            txtBitola.Text =
            txtQtde.Text =
            ddlMateriaPrima.Text =
            ddlBitola.Text =
            ddlUnidade.Text =
            txtResistenciaTracao.Text =
            txtEspecificacao.Text =
            txtIPI.Text =
            txtValorUnit.Text = string.Empty;

    }
    #endregion
    #region LimparCamposItemPedidoCompraInsumo
    private void LimparCamposItemPedidoCompraInsumo()
    {
        hdfTipoAcaoItemPedidoCompraInsumo.Value = "Incluir";

        txtQtdInsumo.Text =
        ddlProdutoInsumo.Text =
        ddlUnidadeInsumo.Text =
        txtIpiInsumo.Text =
        txtValorInsumo.Text = string.Empty;

    }    
    #endregion
    #endregion
    #region Eventos
    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            CarregarCombos();
            //cria a referência a variável que vai ser armazenada na Session
            List<ItemPedidoCompraVO> lstItemPedidoCompra = new List<ItemPedidoCompraVO>();
            List<ItemPedidoCompraInsumoVO> lstItemPedidoCompraInsumo = new List<ItemPedidoCompraInsumoVO>();
            //cria referência que vai resgatar o valor da Session
            PedidoCompraVO identPedidoCompra = new PedidoCompraVO();
            PedidoCompraInsumoVO identPedidoCompraInsumo = new PedidoCompraInsumoVO();
            hdfTipoAcao.Value = Request.QueryString["acao"] ?? "";
            if (hdfTipoAcao.Value.Equals("Incluir"))
            {
                txtEmissao.Text = DateTime.Now.ToString("dd/MM/yyyy");
                btnVoltar.Enabled = false;
                txtCondicoesFornecimento.Text = "ENVIAR CERTIFICADO DE MAT. PRIMA";
                txtObservacao.Text = "NÃO ACEITAREMOS MATERIAL EM VÁRIOS ROLOS, POIS TEMOS MUITA PERDA";
            }
            else
            if (hdfTipoAcao.Value.Equals("Editar"))
            {
                int codPedidoCompra = Convert.ToInt32(Request.QueryString["CodPedidoCompra"]);
                switch (Request.QueryString["Tipo"])
                {
                    case "0":
                        identPedidoCompra = new PedidoCompra().ListarTudo(codPedidoCompra);
                        DadosPedidoCompra = identPedidoCompra;
                        lstItemPedidoCompra = identPedidoCompra.ItemPedidoCompraVo;
                        hdfTipoPedido.Value = "0";
                    break;
                    case "1":
                    identPedidoCompraInsumo = new PedidoCompra().ListarTudoInsumo(codPedidoCompra);
                        DadosPedidoCompraInsumo = identPedidoCompraInsumo;
                        lstItemPedidoCompraInsumo = identPedidoCompraInsumo.ItemPedidoCompraInsumoVo;
                        hdfTipoPedido.Value = "1";
                        break;
                }
                btnVoltar.Enabled = true;
                btnSalvar.Enabled = true;
            }

            //btnIncluirProduto.Attributes.Add("onclick", "ChamaPopup();");
            //criar Session para armazenar valores do grid dos Itens da Nota Fiscal
            //este grid só salvará quando salvar a Nota Fiscal inteira
            Session.Add("lstItemPedidoCompra", lstItemPedidoCompra.ToArray());
            Session.Add("lstItemPedidoCompraInsumo", lstItemPedidoCompraInsumo.ToArray());
            //criar ViewState para armazenar valores do grid das Duplicatas
            //este grid só salvará quando salvar a Nota Fiscal inteira
            Master.PosicionarFoco(txtEmissao);
        }
        ExecutarScript(updDados, new StringBuilder("OcultarBotaoCarregarValores();"));      
    }
    #endregion

    #region btnSalvar_Click
    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        if (grdProduto.Rows.Count > 0 || grdProdutoInsumo.Rows.Count > 0)
        {
            int codPedidoCompra = 0;
            if (hdfTipoAcao.Value.Equals("Incluir"))
            {
                codPedidoCompra = CreatePedido((TypePedido)int.Parse(hdfTipoPedido.Value));
            }
            else
            {
                codPedidoCompra = AlterPedido((TypePedido)int.Parse(hdfTipoPedido.Value));
            }

            Response.Redirect("ListaPedidoCompra.aspx?CodPedidoCompra=" + codPedidoCompra);
        }
        else
            MensagemCliente(updDados, "Nenhum item foi associado ao pedido de compra!");
    }    
    #endregion

    #region btnCancelar_Click
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        LimparCampos();
        Response.Redirect("ListaPedidoCompra.aspx");
    }
    #endregion

    #region Métodos do Grid Materia Prima
    protected void grdProduto_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    protected void grdProduto_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (!e.CommandName.Equals("Page"))
        {
            //forma de pegar o index do datagrid
            GridViewRow row = (GridViewRow) (((ImageButton) e.CommandSource).NamingContainer);
            int linha = row.RowIndex;
            string[] codigos = e.CommandArgument.ToString().Split('|');
            int codMateriaPrima = Convert.ToInt32(codigos[0]);
            int codItemPedidoCompra = Convert.ToInt32(codigos[1]);
            //armazena em viewstate a linha selecionada para posterior atualização
            ViewState["LinhaSelecionadaItemPedidoCompra"] = codMateriaPrima;
            ItemPedidoCompraVO[] lstItemPedidoCompra = (ItemPedidoCompraVO[]) Session["lstItemPedidoCompra"];
            List<ItemPedidoCompraVO> newlstItemPedidoCompra = new List<ItemPedidoCompraVO>(lstItemPedidoCompra);

            if (e.CommandName == "Editar")
            {
                hdfTipoAcaoItemPedidoCompra.Value = "Editar";


                var item =
                    newlstItemPedidoCompra.Where(
                        r =>
                        r.MateriaPrimaVo.CodMateriaPrima == codMateriaPrima &&
                        r.BitolaVo.CodBitola == codItemPedidoCompra).Select(r => r)
                        .FirstOrDefault();
                //busca no vo os valores para a linha selecionada

                //atribui aos campos da tela para alteração
                ddlMateriaPrima.SelectedValue = item.MateriaPrimaVo.CodMateriaPrima.ToString();
                ddlBitola.SelectedValue = item.BitolaVo.CodBitola.ToString();
                ddlUnidade.SelectedValue = item.UnidadeVo.CodUnidade.ToString();
                txtResistenciaTracao.Text = item.ResistenciaTracao.ToString();
                txtEspecificacao.Text = item.Especificacao;
                txtQtde.Text = item.Qtd.ToString();
                txtIPI.Text = item.Ipi.ToString();
                txtValorUnit.Text = item.Valor.ToString();
            }
            else if (e.CommandName == "Excluir")
            {
                newlstItemPedidoCompra.RemoveAt(linha);
                if (codItemPedidoCompra != 0)
                    new PedidoCompra().ExcluirItem(codItemPedidoCompra);
                grdProduto.DataSource = newlstItemPedidoCompra;
                grdProduto.DataBind();
                updProduto.Update();
                //atualiza lstItemPedidoCompra
                Session["lstItemPedidoCompra"] = newlstItemPedidoCompra.ToArray();
                ExecutarScript(updItens, new StringBuilder("showItens();"));
            }
        }
    }

    private decimal? TotalGeral = 0;
    protected void grdProduto_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ItemPedidoCompraVO identItemPedidoCompra = (ItemPedidoCompraVO)e.Row.DataItem;
            string descricao = identItemPedidoCompra.MateriaPrimaVo.NormaVo.Descricao + "/" + identItemPedidoCompra.MateriaPrimaVo.NormaVo.Revisao;
            if (identItemPedidoCompra.MateriaPrimaVo.ClasseTipoVo != null)
                descricao += identItemPedidoCompra.MateriaPrimaVo.ClasseTipoVo.Descricao;
            e.Row.Cells[1].Text = descricao;
            e.Row.Cells[2].Text = identItemPedidoCompra.BitolaVo.Bitola.ToString();
            e.Row.Cells[3].Text = identItemPedidoCompra.Qtd.ToString();
            e.Row.Cells[4].Text = identItemPedidoCompra.UnidadeVo.TipoUnidade;
            e.Row.Cells[5].Text = identItemPedidoCompra.ResistenciaTracao.ToString();
            e.Row.Cells[6].Text = identItemPedidoCompra.Especificacao;
            e.Row.Cells[7].Text = identItemPedidoCompra.Ipi.ToString();
            e.Row.Cells[8].Text = identItemPedidoCompra.Valor.ToString();
            e.Row.Cells[9].Text = identItemPedidoCompra.CalcTotalItem.ToString();
            TotalGeral += identItemPedidoCompra.CalcTotalItem;

            #region Botão Editar
            ImageButton imgEditarFatura = (ImageButton)e.Row.FindControl("imgEditar");
            imgEditarFatura.ImageUrl = caminhoAplicacao + @"Imagens\editar.png";
            //imgEditarFatura.CommandArgument = identItemPedidoCompra.CodItemPedidoCompra.ToString();
            imgEditarFatura.CommandArgument = identItemPedidoCompra.MateriaPrimaVo.CodMateriaPrima + "|" + identItemPedidoCompra.BitolaVo.CodBitola;
            imgEditarFatura.CommandName = "Editar";
            imgEditarFatura.Style.Add("cursor", "hand");
            imgEditarFatura.ToolTip = "Editar dados do Produto [" + descricao + "]";
            imgEditarFatura.Attributes["onclick"] = "showItens();";
            #endregion

            #region Botão Excluir
            ImageButton imgExcluirFatura = (ImageButton)e.Row.FindControl("imgExcluir");
            imgExcluirFatura.ImageUrl = caminhoAplicacao + @"Imagens\exclusao_Canc.png";
            //imgExcluirFatura.CommandArgument = identItemPedidoCompra.CodItemPedidoCompra.ToString();
            imgExcluirFatura.CommandArgument = identItemPedidoCompra.MateriaPrimaVo.CodMateriaPrima + "|" + identItemPedidoCompra.CodItemPedidoCompra;
            imgExcluirFatura.CommandName = "Excluir";
            imgExcluirFatura.Attributes["onclick"] = "return confirm('Confirmar exclusão do Produto [" + descricao + "]?');";
            imgExcluirFatura.Style.Add("cursor", "hand");
            imgExcluirFatura.ToolTip = "Excluir Produto";
            #endregion

            if (e.Row.RowState == DataControlRowState.Normal)
                e.Row.CssClass = "FundoLinha1";
            else if (e.Row.RowState == DataControlRowState.Alternate)
                e.Row.CssClass = "FundoLinha2";
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[4].Text = "Total";
            e.Row.Cells[5].Text = "dos";
            e.Row.Cells[6].Text = "Produtos:";
            //e.Row.Cells[9].Attributes.Add("align", "left");
            e.Row.Cells[9].Attributes.Add("align", "right");
            e.Row.Cells[9].Text = TotalGeral.ToString();
        }

    }

    #endregion

    #region btnIncluirProduto_Click
    protected void btnIncluirProduto_Click(object sender, EventArgs e)
    {

    }    
    #endregion

    #region CustomValidator1_ServerValidate
    protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    {

    }    
    #endregion

    #region btnVoltar_Click
    protected void btnVoltar_Click(object sender, EventArgs e)
    {
        var opcao = Request.QueryString["opcao"] ?? string.Empty;
        var valor = Request.QueryString["valor"] ?? string.Empty;
        var campo = Request.QueryString["campo"] ?? string.Empty;
        Response.Redirect("ListaPedidoCompra.aspx?valor=" + valor + "&opcao=" + opcao + "&campo=" + campo);
    }   
    #endregion

    #region btnCarregarValores_Click
    protected void btnCarregarValores_Click(object sender, EventArgs e)
    {
        FornecedorVO identFornecedor = new FornecedorVO();
        //Todo: Depois do tratamento na procedure, remover a linha abaixo
        identFornecedor.CodPessoa = Convert.ToInt32(hdfIdRazaoSocial.Value);
        DadosRazaoSocial = new Fornecedor().Listar(identFornecedor).First();
        //ExecutarScript(updItens, new StringBuilder("showItens();"));
        //ExecutarScript(updItensInsumo, new StringBuilder("showItensInsumo();"));
    }   
    #endregion

    #region btnSalvarItem_Click
    protected void btnSalvarItem_Click(object sender, EventArgs e)
    {
        //será incluido no grid de ICMS manualmente (não incluirá no banco ainda)
        //pois só deverá ser incluido no banco quando salvar o produto

        //armazena em viewstate a linha selecionada para posterior atualização
        ItemPedidoCompraVO[] lstItemPedidoCompra = (ItemPedidoCompraVO[])Session["lstItemPedidoCompra"];
        List<ItemPedidoCompraVO> newlstItemPedidoCompra = new List<ItemPedidoCompraVO>(lstItemPedidoCompra);
        int codMateriaPrima = 0;
        try
        {

            codMateriaPrima = int.Parse(ddlMateriaPrima.SelectedValue);

        }
        catch (Exception)
        {
            MensagemCliente(updItens, "Escolha um valor na lista para a Matéria Prima!");
            return;
        }

        int codBitola = 0;
        try
        {
            codBitola = int.Parse(ddlBitola.SelectedValue);
        }
        catch (Exception)
        {
            MensagemCliente(updItens, "Escolha um valor na lista para a Bitola!");
            return;
        }
        int codUnidade = 0;
        try
        {
            codUnidade = int.Parse(ddlUnidade.SelectedValue);
        }
        catch (Exception)
        {
            MensagemCliente(updItens, "Escolha um valor na lista para a Unidade!");
            return;
        }

        decimal qtde = !string.IsNullOrEmpty(txtQtde.Text) ? decimal.Parse(txtQtde.Text) : 0;
        decimal valor = !string.IsNullOrEmpty(txtValorUnit.Text) ? decimal.Parse(txtValorUnit.Text) : 0;
        string norma = ddlMateriaPrima.SelectedItem.Text;
        decimal bitola = !string.IsNullOrEmpty(ddlBitola.SelectedItem.Text) ? decimal.Parse(ddlBitola.SelectedItem.Text) : 0;
        string unidade = ddlUnidade.SelectedItem.Text;
        decimal resistenciaTracao = !string.IsNullOrEmpty(txtResistenciaTracao.Text) ? decimal.Parse(txtResistenciaTracao.Text) : 0;
        string especificacao = txtEspecificacao.Text;
        decimal Ipi = !string.IsNullOrEmpty(txtIPI.Text) ? decimal.Parse(txtIPI.Text) : 0;

        //se for edição de ICMS, atualizar o list
        if (hdfTipoAcaoItemPedidoCompra.Value.Equals("Incluir"))
        {
            /************************************************************************
            Se a ação for inclusão, simplesmente verifica se o ítem já foi cadastrado
            se já for, exibe mensagem e não inclui o ítem
            /***********************************************************************/
            ItemPedidoCompraVO result = newlstItemPedidoCompra.Find(
            delegate(ItemPedidoCompraVO bk)
                {
                    return bk.MateriaPrimaVo.CodMateriaPrima == codMateriaPrima && bk.BitolaVo.CodBitola == codBitola;
                }
            );
            if (result != null)
            {

                MensagemCliente(updItens, "Material já cadastrada!");
                return;
            }
            /************************************************************************/
            /************************************************************************/

            //senão, incluir novo ítem no list
            ItemPedidoCompraVO lstItemPedidoCompraAux = new ItemPedidoCompraVO();

            lstItemPedidoCompraAux.MateriaPrimaVo.CodMateriaPrima = codMateriaPrima;
            lstItemPedidoCompraAux.MateriaPrimaVo.NormaVo.Descricao = norma;
            lstItemPedidoCompraAux.BitolaVo.CodBitola = codBitola;
            lstItemPedidoCompraAux.BitolaVo.Bitola = bitola;
            lstItemPedidoCompraAux.UnidadeVo.CodUnidade = codUnidade;
            lstItemPedidoCompraAux.UnidadeVo.TipoUnidade = unidade;
            lstItemPedidoCompraAux.ResistenciaTracao = resistenciaTracao;
            lstItemPedidoCompraAux.Especificacao = especificacao;
            lstItemPedidoCompraAux.Ipi = Ipi;
            lstItemPedidoCompraAux.Qtd = qtde;
            lstItemPedidoCompraAux.Valor = valor;

            newlstItemPedidoCompra.Add(lstItemPedidoCompraAux);
        }
        else
        {
            /************************************************************************
            Se a ação for alteração, verifica se o ítem já está cadastrado, se já estiver
            será impedido, desde que não seja ele mesmo
            /***********************************************************************/
            int linha = Convert.ToInt32(ViewState["LinhaSelecionadaItemPedidoCompra"]);
            if (linha != codMateriaPrima)
            {
                var item1 =
                    newlstItemPedidoCompra.Where(r => r.MateriaPrimaVo.CodMateriaPrima == codMateriaPrima && r.BitolaVo.CodBitola == codBitola).Select(r => r).
                        FirstOrDefault();
                if (item1 != null)
                {
                    MensagemCliente(updItens, "Material já cadastrada!");
                    return;
                }
            }

            /************************************************************************
            Atualiza o item do grid        
            /***********************************************************************/
            var item =
                newlstItemPedidoCompra.Where(r => r.MateriaPrimaVo.CodMateriaPrima == linha && r.BitolaVo.CodBitola == codBitola).Select(r => r).
                    FirstOrDefault();
            item.MateriaPrimaVo.CodMateriaPrima = codMateriaPrima;
            item.MateriaPrimaVo.NormaVo.Descricao = norma;
            item.BitolaVo.CodBitola = codBitola;
            item.BitolaVo.Bitola = bitola;
            item.UnidadeVo.CodUnidade = codUnidade;
            item.UnidadeVo.TipoUnidade = unidade;
            item.ResistenciaTracao = resistenciaTracao;
            item.Especificacao = especificacao;
            item.Ipi = Ipi;
            item.Qtd = qtde;
            item.Valor = valor;
            //sai do for
        }
        grdProduto.DataSource = newlstItemPedidoCompra;
        grdProduto.DataBind();
        //atualiza viewstate
        Session["lstItemPedidoCompra"] = newlstItemPedidoCompra.ToArray();
        LimparCamposItemPedidoCompra();
        Master.PosicionarFoco(txtNorma);
        ExecutarScript(updItens, new StringBuilder("showItens();"));
    }
    
    #endregion


    #region ddlBitola_SelectedIndexChanged
    //protected void ddlBitola_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    var codMateriaPrima = ddlMateriaPrima.SelectedValue;
    //    if (string.IsNullOrEmpty(codMateriaPrima))
    //        codMateriaPrima = "0";
    //    var codBitola = ddlBitola.SelectedValue;
    //    if (string.IsNullOrEmpty(codBitola))
    //        codBitola = "0";
    //    var resistenciaTracao = new MateriaPrima().ListarResistenciaTracao(int.Parse(codMateriaPrima),
    //                                                                     int.Parse(codBitola));
    //    if (resistenciaTracao != null)
    //    {
    //        txtResistenciaTracao.Text = resistenciaTracao.Maximo.ToString();
    //    }
    //    else
    //    {
    //        txtResistenciaTracao.Text = string.Empty;
    //    }
    //}    
    #endregion

    #region btnSalvarInsumo_Click
    protected void btnSalvarInsumo_Click(object sender, EventArgs e)
    {
        //será incluido no grid de ICMS manualmente (não incluirá no banco ainda)
        //pois só deverá ser incluido no banco quando salvar o produto

        //armazena em viewstate a linha selecionada para posterior atualização
        ItemPedidoCompraInsumoVO[] lstItemPedidoCompraInsumo = (ItemPedidoCompraInsumoVO[])Session["lstItemPedidoCompraInsumo"];
        List<ItemPedidoCompraInsumoVO> newlstItemPedidoCompraInsumo = new List<ItemPedidoCompraInsumoVO>(lstItemPedidoCompraInsumo);
        int codProdutoInsumo = 0;
        try
        {

            codProdutoInsumo = int.Parse(ddlProdutoInsumo.SelectedValue);

        }
        catch (Exception)
        {
            MensagemCliente(updItensInsumo, "Escolha um valor na lista para o Produto!");
            return;
        }

        int codUnidade = 0;
        try
        {
            codUnidade = int.Parse(ddlUnidadeInsumo.SelectedValue);
        }
        catch (Exception)
        {
            MensagemCliente(updItensInsumo, "Escolha um valor na lista para a Unidade!");
            return;
        }

        decimal qtde = !string.IsNullOrEmpty(txtQtdInsumo.Text) ? decimal.Parse(txtQtdInsumo.Text) : 0;
        decimal valor = !string.IsNullOrEmpty(txtValorInsumo.Text) ? decimal.Parse(txtValorInsumo.Text) : 0;
        string descricao = ddlProdutoInsumo.SelectedItem.Text;
        string unidade = ddlUnidadeInsumo.SelectedItem.Text;
        decimal Ipi = !string.IsNullOrEmpty(txtIpiInsumo.Text) ? decimal.Parse(txtIpiInsumo.Text) : 0;

        //se for edição de ICMS, atualizar o list
        if (hdfTipoAcaoItemPedidoCompraInsumo.Value.Equals("Incluir"))
        {
            /************************************************************************
            Se a ação for inclusão, simplesmente verifica se o ítem já foi cadastrado
            se já for, exibe mensagem e não inclui o ítem
            /***********************************************************************/
            ItemPedidoCompraInsumoVO result = newlstItemPedidoCompraInsumo.Find(
            delegate(ItemPedidoCompraInsumoVO bk)
            {
                return bk.ProdutoInsumoVo.CodProdutoInsumo == codProdutoInsumo;
            }
            );
            if (result != null)
            {

                MensagemCliente(updItensInsumo, "Produto já cadastrada!");
                return;
            }
            /************************************************************************/
            /************************************************************************/

            //senão, incluir novo ítem no list
            ItemPedidoCompraInsumoVO lstItemPedidoCompraInsumoAux = new ItemPedidoCompraInsumoVO();

            lstItemPedidoCompraInsumoAux.ProdutoInsumoVo.CodProdutoInsumo = codProdutoInsumo;
            lstItemPedidoCompraInsumoAux.ProdutoInsumoVo.Descricao = descricao;
            lstItemPedidoCompraInsumoAux.UnidadeVo.CodUnidade = codUnidade;
            lstItemPedidoCompraInsumoAux.UnidadeVo.TipoUnidade = unidade;
            lstItemPedidoCompraInsumoAux.Ipi = Ipi;
            lstItemPedidoCompraInsumoAux.Qtd = qtde;
            lstItemPedidoCompraInsumoAux.Valor = valor;

            newlstItemPedidoCompraInsumo.Add(lstItemPedidoCompraInsumoAux);
        }
        else
        {
            /************************************************************************
            Se a ação for alteração, verifica se o ítem já está cadastrado, se já estiver
            será impedido, desde que não seja ele mesmo
            /***********************************************************************/
            int linha = Convert.ToInt32(ViewState["LinhaSelecionadaItemPedidoCompraInsumo"]);
            if (linha != codProdutoInsumo)
            {
                var item1 =
                    newlstItemPedidoCompraInsumo.Where(r => r.ProdutoInsumoVo.CodProdutoInsumo == codProdutoInsumo).Select(r => r).
                        FirstOrDefault();
                if (item1 != null)
                {
                    MensagemCliente(updItensInsumo, "Produto já cadastrada!");
                    return;
                }
            }

            /************************************************************************
            Atualiza o item do grid        
            /***********************************************************************/
            var item =
                newlstItemPedidoCompraInsumo.Where(r => r.ProdutoInsumoVo.CodProdutoInsumo == linha).Select(r => r).
                    FirstOrDefault();
            item.ProdutoInsumoVo.CodProdutoInsumo = codProdutoInsumo;
            item.ProdutoInsumoVo.Descricao = descricao;
            item.UnidadeVo.CodUnidade = codUnidade;
            item.UnidadeVo.TipoUnidade = unidade;
            item.Ipi = Ipi;
            item.Qtd = qtde;
            item.Valor = valor;
            //sai do for
        }
        grdProdutoInsumo.DataSource = newlstItemPedidoCompraInsumo;
        grdProdutoInsumo.DataBind();
        //atualiza viewstate
        Session["lstItemPedidoCompraInsumo"] = newlstItemPedidoCompraInsumo.ToArray();
        LimparCamposItemPedidoCompraInsumo();
        Master.PosicionarFoco(ddlProdutoInsumo);
        ExecutarScript(updItensInsumo, new StringBuilder("showItensInsumo();"));
    }
   
    #endregion

    #region grdProdutoInsumo_PageIndexChanging
    protected void grdProdutoInsumo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }    
    #endregion

    #region grdProdutoInsumo_RowCommand
    protected void grdProdutoInsumo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (!e.CommandName.Equals("Page"))
        {
            //forma de pegar o index do datagrid
            GridViewRow row = (GridViewRow) (((ImageButton) e.CommandSource).NamingContainer);
            int linha = row.RowIndex;
            string[] codigos = e.CommandArgument.ToString().Split('|');
            int codProdutoInsumo = Convert.ToInt32(codigos[0]);
            int codItemPedidoCompraInsumo = Convert.ToInt32(codigos[1]);
            //armazena em viewstate a linha selecionada para posterior atualização
            ViewState["LinhaSelecionadaItemPedidoCompraInsumo"] = codProdutoInsumo;
            ItemPedidoCompraInsumoVO[] lstItemPedidoCompraInsumo =
                (ItemPedidoCompraInsumoVO[]) Session["lstItemPedidoCompraInsumo"];
            List<ItemPedidoCompraInsumoVO> newlstItemPedidoCompraInsumo =
                new List<ItemPedidoCompraInsumoVO>(lstItemPedidoCompraInsumo);

            if (e.CommandName == "Editar")
            {
                hdfTipoAcaoItemPedidoCompraInsumo.Value = "Editar";


                var item =
                    newlstItemPedidoCompraInsumo.Where(
                        r =>
                        r.ProdutoInsumoVo.CodProdutoInsumo == codProdutoInsumo &&
                        r.CodItemPedidoCompraInsumo == codItemPedidoCompraInsumo).Select(r => r)
                        .FirstOrDefault();
                //busca no vo os valores para a linha selecionada

                //atribui aos campos da tela para alteração
                ddlProdutoInsumo.SelectedValue = item.ProdutoInsumoVo.CodProdutoInsumo.ToString();
                ddlUnidadeInsumo.SelectedValue = item.UnidadeVo.CodUnidade.ToString();
                txtQtdInsumo.Text = item.Qtd.ToString();
                txtIpiInsumo.Text = item.Ipi.ToString();
                txtValorInsumo.Text = item.Valor.ToString();
            }
            else if (e.CommandName == "Excluir")
            {
                newlstItemPedidoCompraInsumo.RemoveAt(linha);
                if (codItemPedidoCompraInsumo != 0)
                    new PedidoCompra().ExcluirItem(codItemPedidoCompraInsumo);
                grdProduto.DataSource = newlstItemPedidoCompraInsumo;
                grdProduto.DataBind();
                updProduto.Update();
                //atualiza lstItemPedidoCompraInsumo
                Session["lstItemPedidoCompraInsumo"] = newlstItemPedidoCompraInsumo.ToArray();
                ExecutarScript(updItensInsumo, new StringBuilder("showItensInsumo();"));
            }
        }
    }
    #endregion

    #region grdProdutoInsumo_RowDataBound
    private decimal? TotalGeralInsumo = 0;
    protected void grdProdutoInsumo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ItemPedidoCompraInsumoVO identItemPedidoCompraInsumo = (ItemPedidoCompraInsumoVO)e.Row.DataItem;
            e.Row.Cells[1].Text = identItemPedidoCompraInsumo.ProdutoInsumoVo.CodProdutoInsumo.ToString().PadLeft(4, '0');
            string descricao = identItemPedidoCompraInsumo.ProdutoInsumoVo.Descricao;
            e.Row.Cells[2].Text = descricao;
            e.Row.Cells[3].Text = identItemPedidoCompraInsumo.Qtd.ToString();
            e.Row.Cells[4].Text = identItemPedidoCompraInsumo.UnidadeVo.TipoUnidade;
            e.Row.Cells[5].Text = identItemPedidoCompraInsumo.Ipi.ToString();
            e.Row.Cells[6].Text = identItemPedidoCompraInsumo.Valor.ToString();
            e.Row.Cells[7].Text = identItemPedidoCompraInsumo.CalcTotalItem.ToString();
            TotalGeralInsumo += identItemPedidoCompraInsumo.CalcTotalItem;

            #region Botão Editar
            ImageButton imgEditarFatura = (ImageButton)e.Row.FindControl("imgEditar");
            imgEditarFatura.ImageUrl = caminhoAplicacao + @"Imagens\editar.png";
            //imgEditarFatura.CommandArgument = identItemPedidoCompraInsumo.CodItemPedidoCompraInsumo.ToString();
            imgEditarFatura.CommandArgument = identItemPedidoCompraInsumo.ProdutoInsumoVo.CodProdutoInsumo + "|" + identItemPedidoCompraInsumo.CodItemPedidoCompraInsumo;
            imgEditarFatura.CommandName = "Editar";
            imgEditarFatura.Style.Add("cursor", "hand");
            imgEditarFatura.ToolTip = "Editar dados do Produto [" + descricao + "]";
            imgEditarFatura.Attributes["onclick"] = "showItensInsumo();";
            #endregion

            #region Botão Excluir
            ImageButton imgExcluirFatura = (ImageButton)e.Row.FindControl("imgExcluir");
            imgExcluirFatura.ImageUrl = caminhoAplicacao + @"Imagens\exclusao_Canc.png";
            //imgExcluirFatura.CommandArgument = identItemPedidoCompraInsumo.CodItemPedidoCompraInsumo.ToString();
            imgExcluirFatura.CommandArgument = identItemPedidoCompraInsumo.ProdutoInsumoVo.CodProdutoInsumo + "|" + identItemPedidoCompraInsumo.CodItemPedidoCompraInsumo;
            imgExcluirFatura.CommandName = "Excluir";
            imgExcluirFatura.Attributes["onclick"] = "return confirm('Confirmar exclusão do Produto [" + descricao + "]?');";
            imgExcluirFatura.Style.Add("cursor", "hand");
            imgExcluirFatura.ToolTip = "Excluir Produto";
            #endregion

            if (e.Row.RowState == DataControlRowState.Normal)
                e.Row.CssClass = "FundoLinha1";
            else if (e.Row.RowState == DataControlRowState.Alternate)
                e.Row.CssClass = "FundoLinha2";
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[4].Text = "Total";
            e.Row.Cells[5].Text = "dos";
            e.Row.Cells[6].Text = "Produtos:";
            //e.Row.Cells[9].Attributes.Add("align", "left");
            e.Row.Cells[7].Attributes.Add("align", "right");
            e.Row.Cells[7].Text = TotalGeralInsumo.ToString();
        }

    }
    #endregion

    protected void hdfTipoPedido_ValueChanged(object sender, EventArgs e)
    {
        hdfTipoPedido.Value = hdfTipoPedido.Value;
    }
    #endregion

    protected void ddlProdutoInsumo_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnAtualizarMateriaPrima_Click(object sender, EventArgs e)
    {
        Geral.CarregarDDL(ref ddlMateriaPrima, new MateriaPrima().Listar().ToArray(), "CodMateriaPrima", "Descricao");
        Geral.CarregarDDL(ref ddlBitola, new Bitola().Listar().ToArray(), "CodBitola", "Bitola");
        hdfTipoAcaoItemPedidoCompra.Value = "hiddenNo";
    }

    protected void btnAtualizarProdutoInsumo_Click(object sender, EventArgs e)
    {
        Geral.CarregarDDL(ref ddlUnidadeInsumo, new Unidade().Listar(new UnidadeVO()).ToArray(), "CodUnidade", "TipoUnidade");
        Geral.CarregarDDL(ref ddlProdutoInsumo, ProdutoInsumo.Listar().ToArray(), "CodProdutoInsumo", "Descricao");
        hdfTipoAcaoItemPedidoCompraInsumo.Value = "hiddenNo";
    }



}
