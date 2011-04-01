using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Caching;
using Nissi.Model;
using Nissi.Business;
using Nissi.Util;
using System.Globalization;

public partial class CadastraEntradaEstoque : BasePage
{
    #region Propriedades
    private FornecedorVO DadosRazaoSocial
    {
        set {
            if (value.CodPessoa > 0)
            {
                value.Cep = PreencherCamposCep(value.Cep);
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
    }

    private EntradaEstoqueVO DadosEntradaEstoque
    {
        set
        {
            hdfCodEntradaEstoque.Value = value.CodEntradaEstoque.ToString();
            txtEntradaEstoque.Text = value.CodEntradaEstoque.ToString().PadLeft(8,'0');
            txtPedidoCompra.Text = value.PedidoCompra.CodPedidoCompra.ToString().PadLeft(8, '0');
            txtNotaFiscal.Text = value.NotaFiscal.ToString().PadLeft(8, '0');
            if (value.DataEmissao != null)
            txtEmissao.Text = value.DataEmissao.ToString("dd/MM/yyyy");
            if (value.DataEntrada != null)
                txtEntrega.Text = value.DataEntrada.ToString("dd/MM/yyyy");
            if (value.DataNotaFiscal != null)
                txtDataNotaFiscal.Text = value.DataNotaFiscal.ToString("dd/MM/yyyy");
            ddlTipoRetirada.SelectedValue = value.PedidoCompra.TipoRetirada.ToString();
            if (value.PedidoCompra.FuncionarioComprador != null)
                txtComprador.Text = value.PedidoCompra.FuncionarioComprador.Nome;
            if (value.PedidoCompra.FuncionarioAprovador != null)
                txtAprovador.Text = value.PedidoCompra.FuncionarioAprovador.Nome;
            txtCondicoesFornecimento.Text = value.PedidoCompra.CondicaoFornecimento;
            txtObservacao.Text = value.PedidoCompra.Observacao;
            DadosRazaoSocial = value.Fornecedor;
            grdProduto.DataSource = value.Itens;
            grdProduto.DataBind();
        }
        get 
        {
            EntradaEstoqueVO identEntradaEstoqueVo = new EntradaEstoqueVO();
            ItemEntradaEstoqueVO[] lstitemEntradaEstoqueVO = (ItemEntradaEstoqueVO[])Session["lstItemEntradaEstoque"];
            List<ItemEntradaEstoqueVO> newlstitemEntradaEstoqueVO = new List<ItemEntradaEstoqueVO>(lstitemEntradaEstoqueVO);
            if (!string.IsNullOrEmpty(hdfCodEntradaEstoque.Value))
                identEntradaEstoqueVo.CodEntradaEstoque = int.Parse(hdfCodEntradaEstoque.Value);
            identEntradaEstoqueVo.PedidoCompra.CodPedidoCompra = int.Parse(txtPedidoCompra.Text);
            if (!string.IsNullOrEmpty(hdfIdRazaoSocial.Value))
                identEntradaEstoqueVo.Fornecedor.CodPessoa = int.Parse(hdfIdRazaoSocial.Value);
            txtEntradaEstoque.Text = identEntradaEstoqueVo.CodEntradaEstoque.ToString().PadLeft(8, '0');
            if (!string.IsNullOrEmpty(txtEmissao.Text))
                identEntradaEstoqueVo.DataEmissao = Convert.ToDateTime(txtEmissao.Text);
            if (!string.IsNullOrEmpty(txtEntrega.Text))
                identEntradaEstoqueVo.DataEntrada = Convert.ToDateTime(txtEntrega.Text);
            if (!string.IsNullOrEmpty(txtDataNotaFiscal.Text))
                identEntradaEstoqueVo.DataNotaFiscal = Convert.ToDateTime(txtDataNotaFiscal.Text);
            if (!string.IsNullOrEmpty(txtNotaFiscal.Text))
                identEntradaEstoqueVo.NotaFiscal = txtNotaFiscal.Text;
            identEntradaEstoqueVo.Itens = newlstitemEntradaEstoqueVO;
            return identEntradaEstoqueVo; 
        }
    }

    private EntradaEstoqueInsumoVO DadosEntradaEstoqueInsumo
    {
        set
        {
            hdfCodEntradaEstoque.Value = value.CodEntradaEstoque.ToString();
            txtEntradaEstoque.Text = value.CodEntradaEstoque.ToString().PadLeft(8, '0');
            txtPedidoCompra.Text = value.PedidoCompra.CodPedidoCompra.ToString().PadLeft(8, '0');
            txtNotaFiscal.Text = value.NotaFiscal.ToString().PadLeft(8, '0');
            if (value.DataEmissao != null)
                txtEmissao.Text = value.DataEmissao.ToString("dd/MM/yyyy");
            if (value.DataEntrada != null)
                txtEntrega.Text = value.DataEntrada.ToString("dd/MM/yyyy");
            if (value.DataNotaFiscal != null)
                txtDataNotaFiscal.Text = value.DataNotaFiscal.ToString("dd/MM/yyyy");
            ddlTipoRetirada.SelectedValue = value.PedidoCompra.TipoRetirada.ToString();
            if (value.PedidoCompra.FuncionarioComprador != null)
                txtComprador.Text = value.PedidoCompra.FuncionarioComprador.Nome;
            if (value.PedidoCompra.FuncionarioAprovador != null)
                txtAprovador.Text = value.PedidoCompra.FuncionarioAprovador.Nome;
            txtCondicoesFornecimento.Text = value.PedidoCompra.CondicaoFornecimento;
            txtObservacao.Text = value.PedidoCompra.Observacao;
            DadosRazaoSocial = value.Fornecedor;
            grdProdutoInsumo.DataSource = value.Itens;
            grdProdutoInsumo.DataBind();
        }
        get
        {
            EntradaEstoqueInsumoVO identEntradaEstoqueInsumoVo = new EntradaEstoqueInsumoVO();
            ItemEntradaEstoqueInsumoVO[] lstitemEntradaEstoqueInsumoVO = (ItemEntradaEstoqueInsumoVO[])Session["lstItemEntradaEstoqueInsumo"];
            List<ItemEntradaEstoqueInsumoVO> newlstitemEntradaEstoqueInsumoVO = new List<ItemEntradaEstoqueInsumoVO>(lstitemEntradaEstoqueInsumoVO);
            if (!string.IsNullOrEmpty(hdfCodEntradaEstoque.Value))
                identEntradaEstoqueInsumoVo.CodEntradaEstoque = int.Parse(hdfCodEntradaEstoque.Value);
            identEntradaEstoqueInsumoVo.PedidoCompra.CodPedidoCompra = int.Parse(txtPedidoCompra.Text);
            if (!string.IsNullOrEmpty(hdfIdRazaoSocial.Value))
                identEntradaEstoqueInsumoVo.Fornecedor.CodPessoa = int.Parse(hdfIdRazaoSocial.Value);
            txtEntradaEstoque.Text = identEntradaEstoqueInsumoVo.CodEntradaEstoque.ToString().PadLeft(8, '0');
            if (!string.IsNullOrEmpty(txtEmissao.Text))
                identEntradaEstoqueInsumoVo.DataEmissao = Convert.ToDateTime(txtEmissao.Text);
            if (!string.IsNullOrEmpty(txtEntrega.Text))
                identEntradaEstoqueInsumoVo.DataEntrada = Convert.ToDateTime(txtEntrega.Text);
            if (!string.IsNullOrEmpty(txtDataNotaFiscal.Text))
                identEntradaEstoqueInsumoVo.DataNotaFiscal = Convert.ToDateTime(txtDataNotaFiscal.Text);
            if (!string.IsNullOrEmpty(txtNotaFiscal.Text))
                identEntradaEstoqueInsumoVo.NotaFiscal = txtNotaFiscal.Text;
            identEntradaEstoqueInsumoVo.Itens = newlstitemEntradaEstoqueInsumoVO;
            return identEntradaEstoqueInsumoVo;
        }
    }
    #region DadosPedidoCompra
    private PedidoCompraVO DadosPedidoCompra
    {
        set
        {
            hdfPedidoCompra.Value = value.CodPedidoCompra.ToString();
            txtPedidoCompra.Text = value.CodPedidoCompra.ToString().PadLeft(8, '0');
            ddlTipoRetirada.SelectedValue = value.TipoRetirada.ToString();
            if (value.FuncionarioComprador != null)
                txtComprador.Text = value.FuncionarioComprador.Nome;
            if (value.FuncionarioAprovador != null)
                txtAprovador.Text = value.FuncionarioAprovador.Nome;
            txtCondicoesFornecimento.Text = value.CondicaoFornecimento;
            txtObservacao.Text = value.Observacao;
            DadosRazaoSocial = value.Fornecedor;
        }
     }
    #endregion
    #endregion

    #region Eventos
        
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //cria a referência a variável que vai ser armazenada na Session
            List<ItemEntradaEstoqueVO> lstItemEntradaEstoque = new List<ItemEntradaEstoqueVO>();
            List<ItemEntradaEstoqueInsumoVO> lstItemEntradaEstoqueInsumo = new List<ItemEntradaEstoqueInsumoVO>();
            //cria referência que vai resgatar o valor da Session
            EntradaEstoqueVO identEntradaEstoque = new EntradaEstoqueVO();
            EntradaEstoqueInsumoVO identEntradaEstoqueInsumo = new EntradaEstoqueInsumoVO();
            hdfTipoAcao.Value = Request.QueryString["acao"] ?? "";
            if (hdfTipoAcao.Value.Equals("Incluir"))
            {
                txtEmissao.Text = DateTime.Now.ToString("dd/MM/yyyy");
                btnVoltar.Enabled = false;
            }
            else
            if (hdfTipoAcao.Value.Equals("Editar"))
            {
                int codEntradaEstoque = Convert.ToInt32(Request.QueryString["CodEntradaEstoque"]);
                switch (Request.QueryString["Tipo"])
                {
                    case "0":
                        identEntradaEstoque = new EntradaEstoque().ListarTudo(codEntradaEstoque);
                        DadosEntradaEstoque = identEntradaEstoque;
                        lstItemEntradaEstoque = identEntradaEstoque.Itens;
                        hdfTipoPedido.Value = "0";
                        break;
                    case "1":
                        identEntradaEstoqueInsumo = new EntradaEstoque().ListarTudoInsumo(codEntradaEstoque);
                        DadosEntradaEstoqueInsumo = identEntradaEstoqueInsumo;
                        lstItemEntradaEstoqueInsumo = identEntradaEstoqueInsumo.Itens;
                        hdfTipoPedido.Value = "1";
                        break;
                }
                btnVoltar.Enabled = true;
                //btnSalvar.Enabled = false;
            }
            CarregarCombosInsumo();
            //btnIncluirProduto.Attributes.Add("onclick", "ChamaPopup();");
            //criar Session para armazenar valores do grid dos Itens da Nota Fiscal
            //este grid só salvará quando salvar a Nota Fiscal inteira
            Session.Add("lstItemEntradaEstoque", lstItemEntradaEstoque.ToArray());
            Session.Add("lstItemEntradaEstoqueInsumo", lstItemEntradaEstoqueInsumo.ToArray());
            //criar ViewState para armazenar valores do grid das Duplicatas
            //este grid só salvará quando salvar a Nota Fiscal inteira
            Master.PosicionarFoco(txtPedidoCompra);
        }
        ExecutarScript(updDados, new StringBuilder("OcultarBotaoCarregarValores();"));     
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        if (grdProduto.Rows.Count > 0 || grdProdutoInsumo.Rows.Count > 0)
        {

            int codEntradaEstoque = 0;
            if (hdfTipoAcao.Value.Equals("Incluir"))
            {
                codEntradaEstoque = CreateEntrada((TypePedido)int.Parse(hdfTipoPedido.Value));
            }
            else
            {
                codEntradaEstoque = AlterEntrada((TypePedido)int.Parse(hdfTipoPedido.Value));
            }

            Response.Redirect("ListaEntradaEstoque.aspx?CodEntradaEstoque=" + codEntradaEstoque);
        }
        else
            MensagemCliente(updDados, "Não foi associado nenhuma matéria prima a entrada de estoque!");
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        LimparCampos();
        Response.Redirect("ListaEntradaEstoque.aspx");
    }

    #endregion

    #region Métodos Privados
    //Todo: Está função poderia ser feita no próprio FornecedorData.cs
    //ver com Max
    private static CEPVO PreencherCamposCep(CEPVO lCEPVO)
    {
        if (!string.IsNullOrEmpty(lCEPVO.CodCep))
        {
            CEPVO lCep = new CEP().Listar(lCEPVO);
            lCEPVO.Bairro = lCep.Bairro;
            lCEPVO.Cidade = lCep.Cidade;
            lCEPVO.NomEndereco = lCep.NomEndereco;
            lCEPVO.TipoLogradouro = lCep.TipoLogradouro;
        }

        return lCEPVO;
    }

    private void LimparCampos()
    {
        Session.Remove("ItemEntradaEstoque");
        Session.Remove("AcaoProduto");
    }

    private void Load_Pdf()
    {
        byte[] pdfBytes = (byte[])ViewState[key_Pdf];
        if (pdfBytes == null)
        {
            lkbArquivoPdf.Text = "(Nenhum arquivo carregado)";
        }
        else
        {
            string sVarCache = "PDF";
            Cache[sVarCache] = pdfBytes;
            lkbArquivoPdf.Text = "(Arquivo carregado)";
        }
    }

    private void CarregarCombos()
    {
        Geral.CarregarDDL(ref ddlMateriaPrima, new MateriaPrima().Listar().ToArray(), "CodMateriaPrima", "Descricao");
        Geral.CarregarDDL(ref ddlBitola, new Bitola().Listar().ToArray(), "CodBitola", "Bitola");
        Geral.CarregarDDL(ref ddlUnidade, new Unidade().Listar(new UnidadeVO()).ToArray(),"CodUnidade","TipoUnidade");
    }
    private void CarregarCombosInsumo()
    {
        Geral.CarregarDDL(ref ddlUnidadeInsumo, new Unidade().Listar(new UnidadeVO()).ToArray(), "CodUnidade", "TipoUnidade");
        Geral.CarregarDDL(ref ddlProdutoInsumo, ProdutoInsumo.Listar().ToArray(), "CodProdutoInsumo", "Descricao");        
    }

    #region CreatePedido
    private int CreateEntrada(TypePedido typePedido)
    {
        int codEntradaEstoque = 0;
        switch (typePedido)
        {
            case TypePedido.Compra:
                codEntradaEstoque = new EntradaEstoque().Incluir(DadosEntradaEstoque.Fornecedor.CodPessoa,
                                                             DadosEntradaEstoque.PedidoCompra.CodPedidoCompra,
                                                             DadosEntradaEstoque.DataNotaFiscal,
                                                             DadosEntradaEstoque.DataEntrada,
                                                             DadosEntradaEstoque.NotaFiscal,
                                                             UsuarioAtivo.CodFuncionario.Value,
                                                             DadosEntradaEstoque.Itens, typePedido);
                break;
            case TypePedido.CompraInsumo:
                codEntradaEstoque = new EntradaEstoque().Incluir(DadosEntradaEstoqueInsumo.Fornecedor.CodPessoa,
                                                             DadosEntradaEstoqueInsumo.PedidoCompra.CodPedidoCompra,
                                                             DadosEntradaEstoqueInsumo.DataNotaFiscal,
                                                             DadosEntradaEstoqueInsumo.DataEntrada,
                                                             DadosEntradaEstoqueInsumo.NotaFiscal,
                                                             UsuarioAtivo.CodFuncionario.Value,
                                                             DadosEntradaEstoqueInsumo.Itens, typePedido);
                break;
        }
        return codEntradaEstoque;
    }

    #endregion
    #region AlterPedido
    private int AlterEntrada(TypePedido typePedido)
    {
        int codPedido = 0;
        switch (typePedido)
        {
            case TypePedido.Compra:
                new EntradaEstoque().Alterar(DadosEntradaEstoqueInsumo.CodEntradaEstoque, 
                                                             DadosEntradaEstoque.Fornecedor.CodPessoa,
                                                             DadosEntradaEstoque.PedidoCompra.CodPedidoCompra,
                                                             DadosEntradaEstoque.DataNotaFiscal,
                                                             DadosEntradaEstoque.DataEntrada,
                                                             DadosEntradaEstoque.NotaFiscal,
                                                             UsuarioAtivo.CodFuncionario.Value,
                                                             DadosEntradaEstoque.Itens, typePedido);
                codPedido = DadosEntradaEstoqueInsumo.CodEntradaEstoque;
                break;
            case TypePedido.CompraInsumo:
                new EntradaEstoque().Alterar(DadosEntradaEstoqueInsumo.CodEntradaEstoque, 
                                                             DadosEntradaEstoqueInsumo.Fornecedor.CodPessoa,
                                                             DadosEntradaEstoqueInsumo.PedidoCompra.CodPedidoCompra,
                                                             DadosEntradaEstoqueInsumo.DataNotaFiscal,
                                                             DadosEntradaEstoqueInsumo.DataEntrada,
                                                             DadosEntradaEstoqueInsumo.NotaFiscal,
                                                             UsuarioAtivo.CodFuncionario.Value,
                                                             DadosEntradaEstoqueInsumo.Itens, typePedido);
                codPedido = DadosEntradaEstoqueInsumo.CodEntradaEstoque;
                break;
        }
        return codPedido;
    }

    #endregion
    #endregion

    
    #region Métodos do Grid Produto
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
            int codItemEntradaEstoque = Convert.ToInt32(codigos[1]);
            //armazena em viewstate a linha selecionada para posterior atualização
            ViewState["LinhaSelecionadaItemEntradaEstoque"] = codMateriaPrima;
            ItemEntradaEstoqueVO[] lstItemEntradaEstoque = (ItemEntradaEstoqueVO[]) Session["lstItemEntradaEstoque"];
            List<ItemEntradaEstoqueVO> newlstItemEntradaEstoque = new List<ItemEntradaEstoqueVO>(lstItemEntradaEstoque);

            if (e.CommandName == "Editar")
            {
                hdfTipoAcaoItem.Value = "Editar";


                var item =
                    newlstItemEntradaEstoque.Where(
                        r =>
                        r.MateriaPrimaVo.CodMateriaPrima == codMateriaPrima &&
                        r.BitolaVo.CodBitola == codItemEntradaEstoque).Select(r => r)
                        .FirstOrDefault();
                //busca no vo os valores para a linha selecionada
                CarregarCombos();
                //atribui aos campos da tela para alteração
                ddlMateriaPrima.SelectedValue = item.MateriaPrimaVo.CodMateriaPrima.ToString();
                ddlBitola.SelectedValue = item.BitolaVo.CodBitola.ToString();
                ddlUnidade.SelectedValue = item.UnidadeVo.CodUnidade.ToString();
                txtEspecificacao.Text = item.Especificacao;
                txtQtdePedidoCompra.Text = item.QtdPedidoCompra.ToString();
                txtQtde.Text = item.Qtd.ToString();
                txtValorUnit.Text = item.Valor.ToString();
                txtLote.Text = item.Lote.ToString();
                txtCertificado.Text = item.Certificado;
                txtCorrida.Text = item.Corrida;
                txtC.Text = item.C.ToString();
                txtSi.Text = item.Si.ToString();
                txtMn.Text = item.Mn.ToString();
                txtP.Text = item.P.ToString();
                txtS.Text = item.S.ToString();
                txtCr.Text = item.Cr.ToString();
                txtNi.Text = item.Ni.ToString();
                txtMo.Text = item.Mo.ToString();
                txtCu.Text = item.Cu.ToString();
                txtTi.Text = item.Ti.ToString();
                txtN2.Text = item.N2.ToString();
                txtCo.Text = item.Co.ToString();
                txtAl.Text = item.Al.ToString();
                txtResistenciaTracao.Text = item.Resistencia.ToString();
                txtDureza.Text = item.Dureza.ToString();
                txtNota.Text = item.Nota.ToString();
                txtPedidoCompraItem.Text = txtPedidoCompra.Text;
                txtFornecedor.Text = txtRazaoSocial.Text;
                txtData.Text = txtEmissao.Text;
                txtNotaFiscalItem.Text = txtNotaFiscal.Text;
                txtDataNotaFiscalItem.Text = txtDataNotaFiscal.Text;
                txtIPI.Text = item.Ipi.ToString();
                Master.PosicionarFoco(txtLote);
                ViewState[key_Pdf] = item.CertificadoScanneado;
                Load_Pdf();
                var lstComposicaoMateriaPrima = new List<ComposicaoMateriaPrimaVO>();
                if (!string.IsNullOrEmpty(ddlMateriaPrima.SelectedValue))
                    lstComposicaoMateriaPrima =
                        new MateriaPrima().ListarComposicaoMateriaPrima(item.MateriaPrimaVo.CodMateriaPrima);
                Session.Add("ComposicaoMateriaPrima", lstComposicaoMateriaPrima.ToArray());
                var resistenciaTracao = new ResistenciaTracaoVO();
                if (!string.IsNullOrEmpty(ddlMateriaPrima.SelectedValue))
                    resistenciaTracao =
                        new MateriaPrima().ListarResistenciaTracao(item.MateriaPrimaVo.CodMateriaPrima,
                                                                   item.BitolaVo.CodBitola);
                var lstResistenciaTracao = new List<ResistenciaTracaoVO>();
                lstResistenciaTracao.Add(resistenciaTracao);
                Session.Add("ResistenciaTracao", lstResistenciaTracao.ToArray());
                mpeIncluirItem.Show();
            }
            else if (e.CommandName == "Excluir")
            {
                newlstItemEntradaEstoque.RemoveAt(linha);
                grdProduto.DataSource = newlstItemEntradaEstoque;
                grdProduto.DataBind();
                updProduto.Update();
                //atualiza lstItemEntradaEstoque
                Session["lstItemEntradaEstoque"] = newlstItemEntradaEstoque.ToArray();
            }
        }

    }

    private decimal? TotalGeral = 0;
    protected void grdProduto_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ItemEntradaEstoqueVO identItemEntradaEstoque = (ItemEntradaEstoqueVO)e.Row.DataItem;
            e.Row.Cells[1].Text = identItemEntradaEstoque.Lote.ToString();      
            string descricao = identItemEntradaEstoque.MateriaPrimaVo.NormaVo.Descricao + "/" + identItemEntradaEstoque.MateriaPrimaVo.NormaVo.Revisao;
            if (identItemEntradaEstoque.MateriaPrimaVo.ClasseTipoVo != null)
                descricao += identItemEntradaEstoque.MateriaPrimaVo.ClasseTipoVo.Descricao;
                e.Row.Cells[2].Text = descricao;
            e.Row.Cells[3].Text = identItemEntradaEstoque.BitolaVo.Bitola.ToString();
            e.Row.Cells[4].Text = identItemEntradaEstoque.QtdPedidoCompra.ToString();
            e.Row.Cells[5].Text = identItemEntradaEstoque.Qtd.ToString();
            e.Row.Cells[6].Text = identItemEntradaEstoque.UnidadeVo.TipoUnidade;
            e.Row.Cells[7].Text = identItemEntradaEstoque.Resistencia.ToString();
            e.Row.Cells[8].Text = identItemEntradaEstoque.Especificacao;
            e.Row.Cells[9].Text = identItemEntradaEstoque.Ipi.ToString();
            e.Row.Cells[10].Text = identItemEntradaEstoque.Valor.ToString();
            e.Row.Cells[11].Text = identItemEntradaEstoque.CalcTotalItem.ToString();
            TotalGeral += identItemEntradaEstoque.CalcTotalItem;

            #region Botão Editar
            ImageButton imgEditarFatura = (ImageButton)e.Row.FindControl("imgEditar");
            imgEditarFatura.ImageUrl = caminhoAplicacao + @"Imagens\editar.png";
            //imgEditarFatura.CommandArgument = identItemEntradaEstoque.CodItemEntradaEstoque.ToString();
            imgEditarFatura.CommandArgument = identItemEntradaEstoque.MateriaPrimaVo.CodMateriaPrima+"|"+identItemEntradaEstoque.BitolaVo.CodBitola;
            imgEditarFatura.CommandName = "Editar";
            imgEditarFatura.Style.Add("cursor", "hand");
            imgEditarFatura.ToolTip = "Editar dados do Produto ["+descricao+"]";
            #endregion

            #region Botão Excluir
            ImageButton imgExcluirFatura = (ImageButton)e.Row.FindControl("imgExcluir");
            imgExcluirFatura.ImageUrl = caminhoAplicacao + @"Imagens\exclusao_Canc.png";
            //imgExcluirFatura.CommandArgument = identItemEntradaEstoque.CodItemEntradaEstoque.ToString();
            imgExcluirFatura.CommandArgument = identItemEntradaEstoque.MateriaPrimaVo.CodMateriaPrima.ToString();
            imgExcluirFatura.CommandName = "Excluir";
            imgExcluirFatura.Attributes["onclick"] = "return confirm('Confirmar exclusão do Produto ["+descricao+"]?');";
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
            e.Row.Cells[7].Text = "Total";
            e.Row.Cells[8].Text = "dos";
            e.Row.Cells[9].Text = "Produtos:";
            e.Row.Cells[11].Attributes.Add("align", "right");
            e.Row.Cells[11].Text = TotalGeral.ToString();
        }

    }
        
    #endregion
    
    
    protected void btnIncluirProduto_Click(object sender, EventArgs e)
    {
        
    }

    private void LimparCamposItemEntradaEstoque()
    {
        hdfTipoAcaoItem.Value = "Incluir";
        hdfCodMateriaPrima.Value =
        hdfCodBitola.Value =
        txtLote.Text =
        txtCertificado.Text =
        txtCorrida.Text =
        txtBitola.Text =
        txtQtdePedidoCompra.Text =
        txtQtde.Text =
        txtValorUnit.Text =
        txtC.Text =
        txtSi.Text =
        txtMn.Text =
        txtP.Text =
        txtS.Text =
        txtCr.Text =
        txtNi.Text =
        txtMo.Text =
        txtCu.Text =
        txtTi.Text =
        txtN2.Text =
        txtCo.Text =
        txtAl.Text =
        txtResistenciaTracao.Text =
        txtDureza.Text =
        ddlMateriaPrima.SelectedValue =
        ddlBitola.SelectedValue =
        ddlUnidade.SelectedValue =
        txtEspecificacoes.Text =
        txtIPI.Text =
        txtNota.Text = string.Empty;
        lkbArquivoPdf.Text = "(Nenhum arquivo carregado)";
        Cache[key_Pdf] = "";
        ViewState.Clear();
        Session.Remove("ComposicaoMateriaPrima");
        Session.Remove("ResistenciaTracao");
    }

    protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    {

    }

    protected void btnVoltar_Click(object sender, EventArgs e)
    {
        var opcao = Request.QueryString["opcao"] ?? string.Empty;
        var valor = Request.QueryString["valor"] ?? string.Empty;
        var campo = Request.QueryString["campo"] ?? string.Empty;
        Response.Redirect("ListaEntradaEstoque.aspx?valor="+valor+"&opcao="+opcao+"&campo="+campo);
    }

    protected void btnCarregarValores_Click(object sender, EventArgs e)
    {
        FornecedorVO identFornecedor = new FornecedorVO();
        //Todo: Depois do tratamento na procedure, remover a linha abaixo
        identFornecedor.CodPessoa = Convert.ToInt32(hdfIdRazaoSocial.Value);
        DadosRazaoSocial = new Fornecedor().Listar(identFornecedor).First();
        ExecutarScript(updCadastroItem, new StringBuilder("showItens();"));
    }

    protected void btnSalvarItem_Click(object sender, EventArgs e)
    {

    }

    protected void btnLimparImagem_Click(object sender, EventArgs e)
    {
        lkbArquivoPdf.Text = "(Nenhum arquivo carregado)";
        ViewState.Clear();
    }

    private const string key_Pdf = "PDF";
    protected void btnCarregarImagem_Click(object sender, EventArgs e)
    {
        //btnCarregarImagem.Attributes.Add("onclick", "return ValidaArquivoImagem();");
        if ((upFileUp.PostedFile == null) || (upFileUp.PostedFile.ContentLength == 0)
            || !upFileUp.PostedFile.ContentType.Contains("pdf"))
        {
            mpeIncluirItem.Show();
            MensagemCliente(updBotoes, "Informe um arquivo de pdf válido");          
        }
        else
        {
            mpeIncluirItem.Show(); 
            Stream Input = upFileUp.PostedFile.InputStream;
            // Inicializa o buffer			
            byte[] certificadoPdf = new byte[Input.Length];
            // Lê a imagem do arquivo          			 
            Input.Read(certificadoPdf, 0, Convert.ToInt32(Input.Length));
            // Joga no ViewState
            ViewState[key_Pdf] = certificadoPdf;
            lkbArquivoPdf.Text = "(Arquivo carregado)";
            string sVarCache = "PDF";
            Cache[sVarCache] = certificadoPdf;
        }
    }

    protected void btnNovoItem_Click(object sender, EventArgs e)
    {
        mpeIncluirItem.Show();
        hdfTipoAcaoItem.Value = "Incluir";
        txtPedidoCompraItem.Text = txtPedidoCompra.Text;
        txtFornecedor.Text = txtRazaoSocial.Text;
        txtData.Text = txtEmissao.Text;
        txtNotaFiscalItem.Text = txtNotaFiscal.Text;
        txtDataNotaFiscalItem.Text = txtDataNotaFiscal.Text;
        CarregarCombos();
        Master.PosicionarFoco(txtLote);
    }

    protected void btnCancelarItem_Click(object sender, EventArgs e)
    {
        mpeIncluirItem.Hide();
        LimparCamposItemEntradaEstoque();
    }

    protected void btnIncluirItem_Click(object sender, EventArgs e)
    {
        //será incluido no grid de ICMS manualmente (não incluirá no banco ainda)
        //pois só deverá ser incluido no banco quando salvar o produto

        //armazena em viewstate a linha selecionada para posterior atualização
        ItemEntradaEstoqueVO[] lstItemEntradaEstoque = (ItemEntradaEstoqueVO[])Session["lstItemEntradaEstoque"];
        List<ItemEntradaEstoqueVO> newlstItemEntradaEstoque = new List<ItemEntradaEstoqueVO>(lstItemEntradaEstoque);
        int codMateriaPrima = 0;
        try
        {
            codMateriaPrima = int.Parse(ddlMateriaPrima.SelectedValue);
        }
        catch (Exception)
        {
            MensagemCliente(updCadastroItem, "Escolha um valor na lista para a Matéria Prima!");
            return;
        }

        int codBitola = 0;
        try
        {
            codBitola = int.Parse(ddlBitola.SelectedValue);
        }
        catch (Exception)
        {
            MensagemCliente(updCadastroItem, "Escolha um valor na lista para a Bitola!");
            return;
        }

        decimal qtde = !string.IsNullOrEmpty(txtQtde.Text) ? decimal.Parse(txtQtde.Text) : 0;
        decimal valor = !string.IsNullOrEmpty(txtValorUnit.Text) ? decimal.Parse(txtValorUnit.Text) : 0;
        string norma = ddlMateriaPrima.SelectedItem.Text;
        decimal bitola = !string.IsNullOrEmpty(ddlBitola.SelectedItem.Text) ? decimal.Parse(ddlBitola.SelectedItem.Text) : 0;
        string unidade = ddlUnidade.SelectedItem.Text;
        string especificacao = txtEspecificacao.Text;
        decimal Ipi = !string.IsNullOrEmpty(txtIPI.Text) ? decimal.Parse(txtIPI.Text) : 0;
        //se for edição de ICMS, atualizar o list
        if (hdfTipoAcaoItem.Value.Equals("Incluir"))
        {
            /************************************************************************
            Se a ação for inclusão, simplesmente verifica se o ítem já foi cadastrado
            se já for, exibe mensagem e não inclui o ítem
            /***********************************************************************/
            ItemEntradaEstoqueVO result = newlstItemEntradaEstoque.Find(
            delegate(ItemEntradaEstoqueVO bk)
            {
                return bk.MateriaPrimaVo.CodMateriaPrima == codMateriaPrima && bk.BitolaVo.CodBitola == codBitola;
            }
            );
            if (result != null)
            {

                MensagemCliente(updCadastroItem, "Material já cadastrado!");
                return;
            }
            /************************************************************************/
            /************************************************************************/

            //senão, incluir novo ítem no list
            ItemEntradaEstoqueVO lstItemEntradaEstoqueAux = new ItemEntradaEstoqueVO();
            lstItemEntradaEstoqueAux.Lote = int.Parse(txtLote.Text);
            lstItemEntradaEstoqueAux.Certificado = txtCertificado.Text;
            lstItemEntradaEstoqueAux.Corrida = txtCorrida.Text;
            lstItemEntradaEstoqueAux.MateriaPrimaVo.CodMateriaPrima = codMateriaPrima;
            lstItemEntradaEstoqueAux.MateriaPrimaVo.NormaVo.Descricao = norma;
            lstItemEntradaEstoqueAux.BitolaVo.CodBitola = codBitola;
            lstItemEntradaEstoqueAux.BitolaVo.Bitola = bitola;
            lstItemEntradaEstoqueAux.Qtd = qtde;
            lstItemEntradaEstoqueAux.UnidadeVo.TipoUnidade = unidade;
            lstItemEntradaEstoqueAux.Especificacao = especificacao;
            lstItemEntradaEstoqueAux.Ipi = Ipi;
            lstItemEntradaEstoqueAux.Valor = valor;
            lstItemEntradaEstoqueAux.C = !string.IsNullOrEmpty(txtC.Text) ? decimal.Parse(txtC.Text) : 0;
            lstItemEntradaEstoqueAux.Si = !string.IsNullOrEmpty(txtSi.Text) ? decimal.Parse(txtSi.Text) : 0;
            lstItemEntradaEstoqueAux.Mn = !string.IsNullOrEmpty(txtMn.Text) ? decimal.Parse(txtMn.Text) : 0;
            lstItemEntradaEstoqueAux.P = !string.IsNullOrEmpty(txtP.Text) ? decimal.Parse(txtP.Text) : 0;
            lstItemEntradaEstoqueAux.S = !string.IsNullOrEmpty(txtS.Text) ? decimal.Parse(txtS.Text) : 0;
            lstItemEntradaEstoqueAux.Cr = !string.IsNullOrEmpty(txtCr.Text) ? decimal.Parse(txtCr.Text) : 0;
            lstItemEntradaEstoqueAux.Ni = !string.IsNullOrEmpty(txtNi.Text) ? decimal.Parse(txtNi.Text) : 0;
            lstItemEntradaEstoqueAux.Mo = !string.IsNullOrEmpty(txtMo.Text) ? decimal.Parse(txtMo.Text) : 0;
            lstItemEntradaEstoqueAux.Cu = !string.IsNullOrEmpty(txtCu.Text) ? decimal.Parse(txtCu.Text) : 0;
            lstItemEntradaEstoqueAux.Ti = !string.IsNullOrEmpty(txtTi.Text) ? decimal.Parse(txtTi.Text) : 0;
            lstItemEntradaEstoqueAux.N2 = !string.IsNullOrEmpty(txtN2.Text) ? decimal.Parse(txtN2.Text) : 0;
            lstItemEntradaEstoqueAux.Co = !string.IsNullOrEmpty(txtCo.Text) ? decimal.Parse(txtCo.Text) : 0;
            lstItemEntradaEstoqueAux.Al = !string.IsNullOrEmpty(txtAl.Text) ? decimal.Parse(txtAl.Text) : 0;
            lstItemEntradaEstoqueAux.Resistencia = !string.IsNullOrEmpty(txtResistenciaTracao.Text) ? decimal.Parse(txtResistenciaTracao.Text) : 0;
            lstItemEntradaEstoqueAux.Dureza = !string.IsNullOrEmpty(txtDureza.Text) ? decimal.Parse(txtDureza.Text) : 0;
            lstItemEntradaEstoqueAux.Nota = !string.IsNullOrEmpty(txtNota.Text) ? decimal.Parse(txtNota.Text) : 0;
            lstItemEntradaEstoqueAux.CertificadoScanneado = (byte[]) ViewState[key_Pdf];

            newlstItemEntradaEstoque.Add(lstItemEntradaEstoqueAux);
        }
        else
        {
            /************************************************************************
            Se a ação for alteração, verifica se o ítem já está cadastrado, se já estiver
            será impedido, desde que não seja ele mesmo
            /***********************************************************************/
            int linha = Convert.ToInt32(ViewState["LinhaSelecionadaItemEntradaEstoque"]);
            if (linha != codMateriaPrima)
            {
                var item1 =
                    newlstItemEntradaEstoque.Where(r => r.MateriaPrimaVo.CodMateriaPrima == codMateriaPrima && r.BitolaVo.CodBitola == codBitola).Select(r => r).
                        FirstOrDefault();
                if (item1 != null)
                {
                    MensagemCliente(updCadastroItem, "Material já cadastrado!");
                    return;
                }
            }

            /************************************************************************
            Atualiza o item do grid        
            /***********************************************************************/
            var item =
                newlstItemEntradaEstoque.Where(r => r.MateriaPrimaVo.CodMateriaPrima == linha && r.BitolaVo.CodBitola == codBitola).Select(r => r).
                    FirstOrDefault();
            item.MateriaPrimaVo.CodMateriaPrima = codMateriaPrima;
            item.MateriaPrimaVo.NormaVo.Descricao = norma;
            item.BitolaVo.CodBitola = codBitola;
            item.BitolaVo.Bitola = bitola;
            item.Lote = int.Parse(txtLote.Text);
            item.Certificado = txtCertificado.Text;
            item.Corrida = txtCorrida.Text;
            item.Qtd = qtde;
            item.UnidadeVo.TipoUnidade = unidade;
            item.Especificacao = especificacao;
            item.Ipi = Ipi;
            item.Valor = valor;
            item.C = !string.IsNullOrEmpty(txtC.Text) ? decimal.Parse(txtC.Text) : 0;
            item.Si = !string.IsNullOrEmpty(txtSi.Text) ? decimal.Parse(txtSi.Text) : 0;
            item.Mn = !string.IsNullOrEmpty(txtMn.Text) ? decimal.Parse(txtMn.Text) : 0;
            item.P = !string.IsNullOrEmpty(txtP.Text) ? decimal.Parse(txtP.Text) : 0;
            item.S = !string.IsNullOrEmpty(txtS.Text) ? decimal.Parse(txtS.Text) : 0;
            item.Cr = !string.IsNullOrEmpty(txtCr.Text) ? decimal.Parse(txtCr.Text) : 0;
            item.Ni = !string.IsNullOrEmpty(txtNi.Text) ? decimal.Parse(txtNi.Text) : 0;
            item.Mo = !string.IsNullOrEmpty(txtMo.Text) ? decimal.Parse(txtMo.Text) : 0;
            item.Cu = !string.IsNullOrEmpty(txtCu.Text) ? decimal.Parse(txtCu.Text) : 0;
            item.Ti = !string.IsNullOrEmpty(txtTi.Text) ? decimal.Parse(txtTi.Text) : 0;
            item.N2 = !string.IsNullOrEmpty(txtN2.Text) ? decimal.Parse(txtN2.Text) : 0;
            item.Co = !string.IsNullOrEmpty(txtCo.Text) ? decimal.Parse(txtCo.Text) : 0;
            item.Al = !string.IsNullOrEmpty(txtAl.Text) ? decimal.Parse(txtAl.Text) : 0;
            item.Resistencia = !string.IsNullOrEmpty(txtResistenciaTracao.Text) ? decimal.Parse(txtResistenciaTracao.Text) : 0;
            item.Dureza = !string.IsNullOrEmpty(txtDureza.Text) ? decimal.Parse(txtDureza.Text) : 0;
            item.Nota = !string.IsNullOrEmpty(txtNota.Text) ? decimal.Parse(txtNota.Text) : 0;
            item.CertificadoScanneado = (byte[]) ViewState[key_Pdf];

            //sai do for
        }
        grdProduto.DataSource = newlstItemEntradaEstoque;
        grdProduto.DataBind();
        //atualiza viewstate
        Session["lstItemEntradaEstoque"] = newlstItemEntradaEstoque.ToArray();
        LimparCamposItemEntradaEstoque();        
        mpeIncluirItem.Hide();
    }

    protected void btnCarregaValoresPedidoCompra_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtPedidoCompra.Text))
        {
            PedidoCompraVO pedidoCompraVo = new PedidoCompra().ListarTudoEstoque(int.Parse(txtPedidoCompra.Text));
            if (pedidoCompraVo == null)
                MensagemCliente(updDados,"Nenhum Pedido de Compra foi encontrado com esse número!");
            else
            {
                DadosPedidoCompra = pedidoCompraVo;
                switch (pedidoCompraVo.Tipo)
                {
                    case TypePedido.Compra:
                        hdfTipoPedido.Value = "0";
                        grdProduto.DataSource = ConvertToItemEntradaEstoque(pedidoCompraVo.ItemPedidoCompraVo);
                        grdProduto.DataBind();
                        break;
                    case TypePedido.CompraInsumo:
                        hdfTipoPedido.Value = "1";
                        grdProdutoInsumo.DataSource = ConvertToItemEntradaEstoqueInsumo(pedidoCompraVo.ItemPedidoCompraVo);
                        grdProdutoInsumo.DataBind();
                        break;
                }
            }
        }
        else
        {
            MensagemCliente(updDados,"Número do Pedido de Compra não foi preenchido!");
        }
    }

    private List<ItemEntradaEstoqueVO> ConvertToItemEntradaEstoque(List<ItemPedidoCompraVO> itemPedidoCompraVos)
    {
        List<ItemEntradaEstoqueVO> itemEntradaEstoqueVos = new List<ItemEntradaEstoqueVO>();
        foreach (var itemPedidoCompraVo in itemPedidoCompraVos)
        {
            var itemEntradaEstoque = new ItemEntradaEstoqueVO();
            itemEntradaEstoque.MateriaPrimaVo = itemPedidoCompraVo.MateriaPrimaVo;
            itemEntradaEstoque.BitolaVo = itemPedidoCompraVo.BitolaVo;
            itemEntradaEstoque.QtdPedidoCompra = itemPedidoCompraVo.Saldo;
            itemEntradaEstoque.Valor = itemPedidoCompraVo.Valor;
            itemEntradaEstoque.Resistencia = itemPedidoCompraVo.ResistenciaTracao;
            itemEntradaEstoque.Especificacao = itemPedidoCompraVo.Especificacao;
            itemEntradaEstoque.UnidadeVo.CodUnidade = itemPedidoCompraVo.UnidadeVo.CodUnidade;
            itemEntradaEstoque.UnidadeVo.TipoUnidade = itemPedidoCompraVo.UnidadeVo.TipoUnidade;
            itemEntradaEstoque.Ipi = itemPedidoCompraVo.Ipi;
            itemEntradaEstoqueVos.Add(itemEntradaEstoque);
        }
        Session["lstItemEntradaEstoque"] = itemEntradaEstoqueVos.ToArray();
        return itemEntradaEstoqueVos;
    }
    private List<ItemEntradaEstoqueInsumoVO> ConvertToItemEntradaEstoqueInsumo(List<ItemPedidoCompraVO> itemPedidoCompraVos)
    {
        List<ItemEntradaEstoqueInsumoVO> itemEntradaEstoqueVos = new List<ItemEntradaEstoqueInsumoVO>();
        foreach (var itemPedidoCompraVo in itemPedidoCompraVos)
        {
            var itemEntradaEstoque = new ItemEntradaEstoqueInsumoVO();
            itemEntradaEstoque.ProdutoInsumoVo = new ProdutoInsumoVO()
                                                     {
                                                         CodProdutoInsumo = itemPedidoCompraVo.MateriaPrimaVo.CodMateriaPrima,
                                                         Descricao = itemPedidoCompraVo.MateriaPrimaVo.Descricao                                                        
                                                     };
            itemEntradaEstoque.QtdPedidoCompra = itemPedidoCompraVo.Saldo;
            itemEntradaEstoque.Valor = itemPedidoCompraVo.Valor;
            itemEntradaEstoque.Especificacao = itemPedidoCompraVo.Especificacao;
            itemEntradaEstoque.UnidadeVo.CodUnidade = itemPedidoCompraVo.UnidadeVo.CodUnidade;
            itemEntradaEstoque.UnidadeVo.TipoUnidade = itemPedidoCompraVo.UnidadeVo.TipoUnidade;
            itemEntradaEstoque.Ipi = itemPedidoCompraVo.Ipi;
            itemEntradaEstoque.CertificadoScanneado = new byte[0];
            itemEntradaEstoqueVos.Add(itemEntradaEstoque);
        }
        Session["lstItemEntradaEstoqueInsumo"] = itemEntradaEstoqueVos.ToArray();
        return itemEntradaEstoqueVos;
    }
    protected void lkbArquivoPdf_Click(object sender, EventArgs e)
    {
        Response.Redirect("GerarPDF.aspx?Variavel_Cache=PDF");
    }



    protected void ddlMateriaPrima_SelectedIndexChanged(object sender, EventArgs e)
    {
        var lstComposicaoMateriaPrima = new List<ComposicaoMateriaPrimaVO>();
        if (!string.IsNullOrEmpty(ddlMateriaPrima.SelectedValue))
         lstComposicaoMateriaPrima = new MateriaPrima().ListarComposicaoMateriaPrima(int.Parse(ddlMateriaPrima.SelectedValue));
        Session.Add("ComposicaoMateriaPrima",lstComposicaoMateriaPrima.ToArray());
    }

    protected void ddlBitola_SelectedIndexChanged(object sender, EventArgs e)
    {
        string mensagem = string.Empty;
        decimal value = 0;
        if (!string.IsNullOrEmpty(ddlBitola.SelectedValue))
        {
            if (Session["ComposicaoMateriaPrima"] != null)
            {
                value = decimal.Parse(ddlBitola.SelectedItem.Text);
                //armazena em viewstate a linha selecionada para posterior atualização
                ComposicaoMateriaPrimaVO[] lstComposicaoMateriaPrimaVos = (ComposicaoMateriaPrimaVO[])Session["ComposicaoMateriaPrima"];
                List<ComposicaoMateriaPrimaVO> newlstComposicaoMateriaPrimaVos = new List<ComposicaoMateriaPrimaVO>(lstComposicaoMateriaPrimaVos);
                bool verificado = false; 
                foreach (var newlstComposicaoMateriaPrimaVo in newlstComposicaoMateriaPrimaVos)
                {
                    if (0 != newlstComposicaoMateriaPrimaVo.BitolaMaxima && value < newlstComposicaoMateriaPrimaVo.BitolaMinima)
                    {
                        if (verificado == false)
                        {
                            mensagem = newlstComposicaoMateriaPrimaVo.BitolaMinima + " - " + newlstComposicaoMateriaPrimaVo.BitolaMaxima;
                            verificado = true;
                        }
                    }
                    else
                    {
                        if (value > newlstComposicaoMateriaPrimaVo.BitolaMaxima && verificado == false)
                        {
                            mensagem = newlstComposicaoMateriaPrimaVo.BitolaMinima + " - " + newlstComposicaoMateriaPrimaVo.BitolaMaxima;
                        }
                        else
                        {
                            verificado = true;
                            mensagem = string.Empty;
                        }
                    }
                }
                if (!string.IsNullOrEmpty(mensagem))
                {
                    spanBi.Attributes.Add("title", mensagem);
                    spanBi.Style.Add("display", "inline");
                    ddlBitola.BackColor = Color.Yellow;
                }
                else
                {
                    spanBi.Style.Add("display", "none");
                    ddlBitola.BackColor = Color.White;
                }
            }
            if (!string.IsNullOrEmpty(ddlMateriaPrima.SelectedValue))
            {
                var resistenciaTracao = new ResistenciaTracaoVO();
                if (!string.IsNullOrEmpty(ddlMateriaPrima.SelectedValue))
                    resistenciaTracao =
                        new MateriaPrima().ListarResistenciaTracao(int.Parse(ddlMateriaPrima.SelectedValue),
                                                                   int.Parse(ddlBitola.SelectedValue));
                var lstResistenciaTracao = new List<ResistenciaTracaoVO>();
                lstResistenciaTracao.Add(resistenciaTracao);
                Session.Add("ResistenciaTracao", lstResistenciaTracao.ToArray());
            }
        }
    }

    protected void txtAl_TextChanged(object sender, EventArgs e)
    {
        string mensagem = string.Empty;
        decimal value = 0;
        if (!string.IsNullOrEmpty(((TextBox)sender).Text))
        {
            if (Session["ComposicaoMateriaPrima"] != null)
            {
                value = decimal.Parse(((TextBox)sender).Text);
                //armazena em viewstate a linha selecionada para posterior atualização
                ComposicaoMateriaPrimaVO[] lstComposicaoMateriaPrimaVos = (ComposicaoMateriaPrimaVO[])Session["ComposicaoMateriaPrima"];
                List<ComposicaoMateriaPrimaVO> newlstComposicaoMateriaPrimaVos = new List<ComposicaoMateriaPrimaVO>(lstComposicaoMateriaPrimaVos);
                bool verificado = false; 
                foreach (var newlstComposicaoMateriaPrimaVo in newlstComposicaoMateriaPrimaVos)
                {
                    if (0 != newlstComposicaoMateriaPrimaVo.AlMaximo)
                    {
                        if (value < newlstComposicaoMateriaPrimaVo.AlMinimo)
                        {

                            if (verificado == false)
                            {
                                mensagem = newlstComposicaoMateriaPrimaVo.AlMinimo + " - " +
                                           newlstComposicaoMateriaPrimaVo.AlMaximo;
                                verificado = true;
                            }
                        }
                        else
                        {
                            if (value > newlstComposicaoMateriaPrimaVo.AlMaximo && verificado == false)
                            {
                                mensagem = newlstComposicaoMateriaPrimaVo.AlMinimo + " - " +
                                           newlstComposicaoMateriaPrimaVo.AlMaximo;
                            }
                            else
                            {
                                verificado = true;
                                mensagem = string.Empty;
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(mensagem))
                {
                    spanAl.Attributes.Add("title", mensagem);
                    spanAl.Style.Add("display", "inline");
                    ((TextBox)sender).BackColor = Color.Yellow;
                }
                else
                {
                    spanAl.Style.Add("display", "none");
                    ((TextBox)sender).BackColor = Color.White;
                }
            }
        }
        txtC.Focus();
    }


    protected void txtC_TextChanged(object sender, EventArgs e)
    {
        string mensagem = string.Empty;
        decimal value = 0;
        if (!string.IsNullOrEmpty(((TextBox)sender).Text))
        {
            if (Session["ComposicaoMateriaPrima"] != null)
            {
                value = decimal.Parse(((TextBox)sender).Text);
                //armazena em viewstate a linha selecionada para posterior atualização
                ComposicaoMateriaPrimaVO[] lstComposicaoMateriaPrimaVos = (ComposicaoMateriaPrimaVO[])Session["ComposicaoMateriaPrima"];
                List<ComposicaoMateriaPrimaVO> newlstComposicaoMateriaPrimaVos = new List<ComposicaoMateriaPrimaVO>(lstComposicaoMateriaPrimaVos);
                bool verificado = false; 
                foreach (var newlstComposicaoMateriaPrimaVo in newlstComposicaoMateriaPrimaVos)
                {
                    if (0 != newlstComposicaoMateriaPrimaVo.CMaximo)
                    {
                        if (value < newlstComposicaoMateriaPrimaVo.CMinimo)
                        {

                            if (verificado == false)
                            {
                                mensagem = newlstComposicaoMateriaPrimaVo.CMinimo + " - " +
                                           newlstComposicaoMateriaPrimaVo.CMaximo;
                                verificado = true;
                            }
                        }
                        else
                        {
                            if (value > newlstComposicaoMateriaPrimaVo.CMaximo && verificado == false)
                            {
                                mensagem = newlstComposicaoMateriaPrimaVo.CMinimo + " - " +
                                           newlstComposicaoMateriaPrimaVo.CMaximo;
                            }
                            else
                            {
                                verificado = true;
                                mensagem = string.Empty;
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(mensagem))
                {
                    spanC.Attributes.Add("title", mensagem);
                    spanC.Style.Add("display", "inline");
                    ((TextBox) sender).BackColor = Color.Yellow;
                }
                else
                {
                    spanC.Style.Add("display", "none");
                    ((TextBox)sender).BackColor = Color.White;
                }
            }
        }
        txtSi.Focus();
    }

    protected void txtSi_TextChanged(object sender, EventArgs e)
    {
        string mensagem = string.Empty;
        decimal value = 0;
        if (!string.IsNullOrEmpty(((TextBox)sender).Text))
        {
            if (Session["ComposicaoMateriaPrima"] != null)
            {
                value = decimal.Parse(((TextBox)sender).Text);
                //armazena em viewstate a linha selecionada para posterior atualização
                ComposicaoMateriaPrimaVO[] lstComposicaoMateriaPrimaVos = (ComposicaoMateriaPrimaVO[])Session["ComposicaoMateriaPrima"];
                List<ComposicaoMateriaPrimaVO> newlstComposicaoMateriaPrimaVos = new List<ComposicaoMateriaPrimaVO>(lstComposicaoMateriaPrimaVos);
                bool verificado = false; 
                foreach (var newlstComposicaoMateriaPrimaVo in newlstComposicaoMateriaPrimaVos)
                {
                    if (0 != newlstComposicaoMateriaPrimaVo.SiMaximo)
                    {
                        if (value < newlstComposicaoMateriaPrimaVo.SiMinimo)
                        {
                            if (verificado == false)
                            {
                                mensagem = newlstComposicaoMateriaPrimaVo.SiMinimo + " - " +
                                           newlstComposicaoMateriaPrimaVo.SiMaximo;
                                verificado = true;
                            }
                        }
                        else
                        {
                            if (value > newlstComposicaoMateriaPrimaVo.SiMaximo && verificado == false)
                            {
                                mensagem = newlstComposicaoMateriaPrimaVo.SiMinimo + " - " +
                                           newlstComposicaoMateriaPrimaVo.SiMaximo;
                            }
                            else
                            {
                                verificado = true;
                                mensagem = string.Empty;
                            }
                        }
                    }
                }

                if (!string.IsNullOrEmpty(mensagem))
                {
                    spanSi.Attributes.Add("title", mensagem);
                    spanSi.Style.Add("display", "inline");
                    ((TextBox)sender).BackColor = Color.Yellow;
                }
                else
                {
                    spanSi.Style.Add("display", "none");
                    ((TextBox)sender).BackColor = Color.White;
                }
            }
        }
        txtMn.Focus();
    }

    protected void txtMn_TextChanged(object sender, EventArgs e)
    {
        string mensagem = string.Empty;
        decimal value = 0;
        if (!string.IsNullOrEmpty(((TextBox)sender).Text))
        {
            if (Session["ComposicaoMateriaPrima"] != null)
            {
                value = decimal.Parse(((TextBox)sender).Text);
                //armazena em viewstate a linha selecionada para posterior atualização
                ComposicaoMateriaPrimaVO[] lstComposicaoMateriaPrimaVos = (ComposicaoMateriaPrimaVO[])Session["ComposicaoMateriaPrima"];
                List<ComposicaoMateriaPrimaVO> newlstComposicaoMateriaPrimaVos = new List<ComposicaoMateriaPrimaVO>(lstComposicaoMateriaPrimaVos);
                bool verificado = false;
                foreach (var newlstComposicaoMateriaPrimaVo in newlstComposicaoMateriaPrimaVos)
                {

                    if (0 != newlstComposicaoMateriaPrimaVo.MnMaximo)
                    {
                        if (value < newlstComposicaoMateriaPrimaVo.MnMinimo)
                        {

                            if (verificado == false)
                            {
                                mensagem = newlstComposicaoMateriaPrimaVo.MnMinimo + " - " +
                                           newlstComposicaoMateriaPrimaVo.MnMaximo;
                                verificado = true;
                            }
                        }
                        else
                        {
                            if (value > newlstComposicaoMateriaPrimaVo.MnMaximo && verificado == false)
                            {
                                mensagem = newlstComposicaoMateriaPrimaVo.MnMinimo + " - " +
                                           newlstComposicaoMateriaPrimaVo.MnMaximo;
                            }
                            else
                            {
                                verificado = true;
                                mensagem = string.Empty;
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(mensagem))
                {
                    spanMn.Attributes.Add("title", mensagem);
                    spanMn.Style.Add("display", "inline");
                    ((TextBox)sender).BackColor = Color.Yellow;
                }
                else
                {
                    spanMn.Style.Add("display", "none");
                    ((TextBox)sender).BackColor = Color.White;
                }
            }
        }
        txtP.Focus();
    }

    protected void txtP_TextChanged(object sender, EventArgs e)
    {
        string mensagem = string.Empty;
        decimal value = 0;
        if (!string.IsNullOrEmpty(((TextBox)sender).Text))
        {
            if (Session["ComposicaoMateriaPrima"] != null)
            {
                value = decimal.Parse(((TextBox)sender).Text);
                //armazena em viewstate a linha selecionada para posterior atualização
                ComposicaoMateriaPrimaVO[] lstComposicaoMateriaPrimaVos = (ComposicaoMateriaPrimaVO[])Session["ComposicaoMateriaPrima"];
                List<ComposicaoMateriaPrimaVO> newlstComposicaoMateriaPrimaVos = new List<ComposicaoMateriaPrimaVO>(lstComposicaoMateriaPrimaVos);
                bool verificado = false; 
                foreach (var newlstComposicaoMateriaPrimaVo in newlstComposicaoMateriaPrimaVos)
                {
                    if (0 != newlstComposicaoMateriaPrimaVo.PMaximo)
                    {
                        if (value < newlstComposicaoMateriaPrimaVo.PMinimo)
                        {

                            if (verificado == false)
                            {
                                mensagem = newlstComposicaoMateriaPrimaVo.PMinimo + " - " +
                                           newlstComposicaoMateriaPrimaVo.PMaximo;
                                verificado = true;
                            }
                        }
                        else
                        {
                            if (value > newlstComposicaoMateriaPrimaVo.PMaximo && verificado == false)
                            {
                                mensagem = newlstComposicaoMateriaPrimaVo.PMinimo + " - " +
                                           newlstComposicaoMateriaPrimaVo.PMaximo;
                            }
                            else
                            {
                                verificado = true;
                                mensagem = string.Empty;
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(mensagem))
                {
                    spanP.Attributes.Add("title", mensagem);
                    spanP.Style.Add("display", "inline");
                    ((TextBox)sender).BackColor = Color.Yellow;
                }
                else
                {
                    spanP.Style.Add("display", "none");
                    ((TextBox)sender).BackColor = Color.White;
                }
            }
        }
        txtS.Focus();
    }

    protected void txtS_TextChanged(object sender, EventArgs e)
    {
        string mensagem = string.Empty;
        decimal value = 0;
        if (!string.IsNullOrEmpty(((TextBox)sender).Text))
        {
            if (Session["ComposicaoMateriaPrima"] != null)
            {
                value = decimal.Parse(((TextBox)sender).Text);
                //armazena em viewstate a linha selecionada para posterior atualização
                ComposicaoMateriaPrimaVO[] lstComposicaoMateriaPrimaVos = (ComposicaoMateriaPrimaVO[])Session["ComposicaoMateriaPrima"];
                List<ComposicaoMateriaPrimaVO> newlstComposicaoMateriaPrimaVos = new List<ComposicaoMateriaPrimaVO>(lstComposicaoMateriaPrimaVos);
                bool verificado = false; 
                foreach (var newlstComposicaoMateriaPrimaVo in newlstComposicaoMateriaPrimaVos)
                {

                    if (0 != newlstComposicaoMateriaPrimaVo.SMaximo)
                    {
                        if (value < newlstComposicaoMateriaPrimaVo.SMinimo)
                        {

                            if (verificado == false)
                            {
                                mensagem = newlstComposicaoMateriaPrimaVo.SMinimo + " - " +
                                           newlstComposicaoMateriaPrimaVo.SMaximo;
                                verificado = true;
                            }
                        }
                        else
                        {
                            if (value > newlstComposicaoMateriaPrimaVo.SMaximo && verificado == false)
                            {
                                mensagem = newlstComposicaoMateriaPrimaVo.SMinimo + " - " +
                                           newlstComposicaoMateriaPrimaVo.SMaximo;
                            }
                            else
                            {
                                verificado = true;
                                mensagem = string.Empty;
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(mensagem))
                {
                    spanS.Attributes.Add("title", mensagem);
                    spanS.Style.Add("display", "inline");
                    ((TextBox)sender).BackColor = Color.Yellow;
                }
                else
                {
                    spanS.Style.Add("display", "none");
                    ((TextBox)sender).BackColor = Color.White;
                }
            }
        }
        txtCr.Focus();
    }

    protected void txtCr_TextChanged(object sender, EventArgs e)
    {
        string mensagem = string.Empty;
        decimal value = 0;
        if (!string.IsNullOrEmpty(((TextBox)sender).Text))
        {
            if (Session["ComposicaoMateriaPrima"] != null)
            {
                value = decimal.Parse(((TextBox)sender).Text);
                //armazena em viewstate a linha selecionada para posterior atualização
                ComposicaoMateriaPrimaVO[] lstComposicaoMateriaPrimaVos = (ComposicaoMateriaPrimaVO[])Session["ComposicaoMateriaPrima"];
                List<ComposicaoMateriaPrimaVO> newlstComposicaoMateriaPrimaVos = new List<ComposicaoMateriaPrimaVO>(lstComposicaoMateriaPrimaVos);
                bool verificado = false; 
                foreach (var newlstComposicaoMateriaPrimaVo in newlstComposicaoMateriaPrimaVos)
                {
                    if (0 != newlstComposicaoMateriaPrimaVo.CrMaximo)
                    {
                        if (value < newlstComposicaoMateriaPrimaVo.CrMinimo)
                        {

                            if (verificado == false)
                            {
                                mensagem = newlstComposicaoMateriaPrimaVo.CrMinimo + " - " +
                                           newlstComposicaoMateriaPrimaVo.CrMaximo;
                                verificado = true;
                            }
                        }
                        else
                        {
                            if (value > newlstComposicaoMateriaPrimaVo.CrMaximo && verificado == false)
                            {
                                mensagem = newlstComposicaoMateriaPrimaVo.CrMinimo + " - " +
                                           newlstComposicaoMateriaPrimaVo.CrMaximo;
                            }
                            else
                            {
                                verificado = true;
                                mensagem = string.Empty;
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(mensagem))
                {
                    spanCr.Attributes.Add("title", mensagem);
                    spanCr.Style.Add("display", "inline");
                    ((TextBox)sender).BackColor = Color.Yellow;
                }
                else
                {
                    spanCr.Style.Add("display", "none");
                    ((TextBox)sender).BackColor = Color.White;
                }
            }
        }
        txtNi.Focus();
    }

    protected void txtNi_TextChanged(object sender, EventArgs e)
    {
        string mensagem = string.Empty;
        decimal value = 0;
        if (!string.IsNullOrEmpty(((TextBox)sender).Text))
        {
            if (Session["ComposicaoMateriaPrima"] != null)
            {
                value = decimal.Parse(((TextBox)sender).Text);
                //armazena em viewstate a linha selecionada para posterior atualização
                ComposicaoMateriaPrimaVO[] lstComposicaoMateriaPrimaVos = (ComposicaoMateriaPrimaVO[])Session["ComposicaoMateriaPrima"];
                List<ComposicaoMateriaPrimaVO> newlstComposicaoMateriaPrimaVos = new List<ComposicaoMateriaPrimaVO>(lstComposicaoMateriaPrimaVos);
                bool verificado = false;
                foreach (var newlstComposicaoMateriaPrimaVo in newlstComposicaoMateriaPrimaVos)
                {

                    if (0 != newlstComposicaoMateriaPrimaVo.NiMaximo)
                    {
                        if (value < newlstComposicaoMateriaPrimaVo.NiMinimo)
                        {

                            if (verificado == false)
                            {
                                mensagem = newlstComposicaoMateriaPrimaVo.NiMinimo + " - " +
                                           newlstComposicaoMateriaPrimaVo.NiMaximo;
                                verificado = true;
                            }
                        }
                        else
                        {
                            if (value > newlstComposicaoMateriaPrimaVo.NiMaximo && verificado == false)
                            {
                                mensagem = newlstComposicaoMateriaPrimaVo.NiMinimo + " - " +
                                           newlstComposicaoMateriaPrimaVo.NiMaximo;
                            }
                            else
                            {
                                verificado = true;
                                mensagem = string.Empty;
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(mensagem))
                {
                    spanNi.Attributes.Add("title", mensagem);
                    spanNi.Style.Add("display", "inline");
                    ((TextBox)sender).BackColor = Color.Yellow;
                }
                else
                {
                    spanNi.Style.Add("display", "none");
                    ((TextBox)sender).BackColor = Color.White;
                }
            }
        }
        txtMo.Focus();
    }

    protected void txtMo_TextChanged(object sender, EventArgs e)
    {
        string mensagem = string.Empty;
        decimal value = 0;
        if (!string.IsNullOrEmpty(((TextBox)sender).Text))
        {
            if (Session["ComposicaoMateriaPrima"] != null)
            {
                value = decimal.Parse(((TextBox)sender).Text);
                //armazena em viewstate a linha selecionada para posterior atualização
                ComposicaoMateriaPrimaVO[] lstComposicaoMateriaPrimaVos = (ComposicaoMateriaPrimaVO[])Session["ComposicaoMateriaPrima"];
                List<ComposicaoMateriaPrimaVO> newlstComposicaoMateriaPrimaVos = new List<ComposicaoMateriaPrimaVO>(lstComposicaoMateriaPrimaVos);
                bool verificado = false; 
                foreach (var newlstComposicaoMateriaPrimaVo in newlstComposicaoMateriaPrimaVos)
                {

                    if (0 != newlstComposicaoMateriaPrimaVo.MoMaximo)
                    {
                        if (value < newlstComposicaoMateriaPrimaVo.MoMinimo)
                        {

                            if (verificado == false)
                            {
                                mensagem = newlstComposicaoMateriaPrimaVo.MoMinimo + " - " +
                                           newlstComposicaoMateriaPrimaVo.MoMaximo;
                                verificado = true;
                            }
                        }
                        else
                        {
                            if (value > newlstComposicaoMateriaPrimaVo.MoMaximo && verificado == false)
                            {
                                mensagem = newlstComposicaoMateriaPrimaVo.MoMinimo + " - " +
                                           newlstComposicaoMateriaPrimaVo.MoMaximo;
                            }
                            else
                            {
                                verificado = true;
                                mensagem = string.Empty;
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(mensagem))
                {
                    spanMo.Attributes.Add("title", mensagem);
                    spanMo.Style.Add("display", "inline");
                    ((TextBox)sender).BackColor = Color.Yellow;
                }
                else
                {
                    spanMo.Style.Add("display", "none");
                    ((TextBox)sender).BackColor = Color.White;
                }
            }
        }
        txtCu.Focus();
    }

    protected void txtCu_TextChanged(object sender, EventArgs e)
    {
        string mensagem = string.Empty;
        decimal value = 0;
        if (!string.IsNullOrEmpty(((TextBox)sender).Text))
        {
            if (Session["ComposicaoMateriaPrima"] != null)
            {
                value = decimal.Parse(((TextBox)sender).Text);
                //armazena em viewstate a linha selecionada para posterior atualização
                ComposicaoMateriaPrimaVO[] lstComposicaoMateriaPrimaVos = (ComposicaoMateriaPrimaVO[])Session["ComposicaoMateriaPrima"];
                List<ComposicaoMateriaPrimaVO> newlstComposicaoMateriaPrimaVos = new List<ComposicaoMateriaPrimaVO>(lstComposicaoMateriaPrimaVos);
                bool verificado = false; 
                foreach (var newlstComposicaoMateriaPrimaVo in newlstComposicaoMateriaPrimaVos)
                {
                    if (0 != newlstComposicaoMateriaPrimaVo.CuMaximo)
                    {
                        if (value < newlstComposicaoMateriaPrimaVo.CuMinimo)
                        {

                            if (verificado == false)
                            {
                                mensagem = newlstComposicaoMateriaPrimaVo.CuMinimo + " - " +
                                           newlstComposicaoMateriaPrimaVo.CuMaximo;
                                verificado = true;
                            }
                        }
                        else
                        {
                            if (value > newlstComposicaoMateriaPrimaVo.CuMaximo && verificado == false)
                            {
                                mensagem = newlstComposicaoMateriaPrimaVo.CuMinimo + " - " +
                                           newlstComposicaoMateriaPrimaVo.CuMaximo;
                            }
                            else
                            {
                                verificado = true;
                                mensagem = string.Empty;
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(mensagem))
                {
                    spanCu.Attributes.Add("title", mensagem);
                    spanCu.Style.Add("display", "inline");
                    ((TextBox)sender).BackColor = Color.Yellow;
                }
                else
                {
                    spanCu.Style.Add("display", "none");
                    ((TextBox)sender).BackColor = Color.White;
                }
            }
        }
        txtTi.Focus();
    }

    protected void txtTi_TextChanged(object sender, EventArgs e)
    {
        string mensagem = string.Empty;
        decimal value = 0;
        if (!string.IsNullOrEmpty(((TextBox)sender).Text))
        {
            if (Session["ComposicaoMateriaPrima"] != null)
            {
                value = decimal.Parse(((TextBox)sender).Text);
                //armazena em viewstate a linha selecionada para posterior atualização
                ComposicaoMateriaPrimaVO[] lstComposicaoMateriaPrimaVos = (ComposicaoMateriaPrimaVO[])Session["ComposicaoMateriaPrima"];
                List<ComposicaoMateriaPrimaVO> newlstComposicaoMateriaPrimaVos = new List<ComposicaoMateriaPrimaVO>(lstComposicaoMateriaPrimaVos);
                bool verificado = false; 
                foreach (var newlstComposicaoMateriaPrimaVo in newlstComposicaoMateriaPrimaVos)
                {
                    if (0 != newlstComposicaoMateriaPrimaVo.TiMaximo)
                    {
                        if (value < newlstComposicaoMateriaPrimaVo.TiMinimo)
                        {

                            if (verificado == false)
                            {
                                mensagem = newlstComposicaoMateriaPrimaVo.TiMinimo + " - " +
                                           newlstComposicaoMateriaPrimaVo.TiMaximo;
                                verificado = true;
                            }
                        }
                        else
                        {
                            if (value > newlstComposicaoMateriaPrimaVo.CMaximo && verificado == false)
                            {
                                mensagem = newlstComposicaoMateriaPrimaVo.TiMinimo + " - " +
                                           newlstComposicaoMateriaPrimaVo.TiMaximo;
                            }
                            else
                            {
                                verificado = true;
                                mensagem = string.Empty;
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(mensagem))
                {
                    spanTi.Attributes.Add("title", mensagem);
                    spanTi.Style.Add("display", "inline");
                    ((TextBox)sender).BackColor = Color.Yellow;
                }
                else
                {
                    spanTi.Style.Add("display", "none");
                    ((TextBox)sender).BackColor = Color.White;
                }
            }
        }
        txtN2.Focus();
    }

    protected void txtN2_TextChanged(object sender, EventArgs e)
    {
        string mensagem = string.Empty;
        decimal value = 0;
        if (!string.IsNullOrEmpty(((TextBox)sender).Text))
        {
            if (Session["ComposicaoMateriaPrima"] != null)
            {
                value = decimal.Parse(((TextBox)sender).Text);
                //armazena em viewstate a linha selecionada para posterior atualização
                ComposicaoMateriaPrimaVO[] lstComposicaoMateriaPrimaVos = (ComposicaoMateriaPrimaVO[])Session["ComposicaoMateriaPrima"];
                List<ComposicaoMateriaPrimaVO> newlstComposicaoMateriaPrimaVos = new List<ComposicaoMateriaPrimaVO>(lstComposicaoMateriaPrimaVos);
                bool verificado = false; 
                foreach (var newlstComposicaoMateriaPrimaVo in newlstComposicaoMateriaPrimaVos)
                {
                    if (0 != newlstComposicaoMateriaPrimaVo.N2Maximo)
                    {
                        if (value < newlstComposicaoMateriaPrimaVo.N2Minimo)
                        {

                            if (verificado == false)
                            {
                                mensagem = newlstComposicaoMateriaPrimaVo.N2Minimo + " - " +
                                           newlstComposicaoMateriaPrimaVo.N2Maximo;
                                verificado = true;
                            }
                        }
                        else
                        {
                            if (value > newlstComposicaoMateriaPrimaVo.CMaximo && verificado == false)
                            {
                                mensagem = newlstComposicaoMateriaPrimaVo.N2Minimo + " - " +
                                           newlstComposicaoMateriaPrimaVo.N2Maximo;
                            }
                            else
                            {
                                verificado = true;
                                mensagem = string.Empty;
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(mensagem))
                {
                    spanN2.Attributes.Add("title", mensagem);
                    spanN2.Style.Add("display", "inline");
                    ((TextBox)sender).BackColor = Color.Yellow;
                }
                else
                {
                    spanN2.Style.Add("display", "none");
                    ((TextBox)sender).BackColor = Color.White;
                }
            }
        }
        txtCo.Focus();
    }

    protected void txtCo_TextChanged(object sender, EventArgs e)
    {
        string mensagem = string.Empty;
        decimal value = 0;
        if (!string.IsNullOrEmpty(((TextBox)sender).Text))
        {
            if (Session["ComposicaoMateriaPrima"] != null)
            {
                value = decimal.Parse(((TextBox)sender).Text);
                //armazena em viewstate a linha selecionada para posterior atualização
                ComposicaoMateriaPrimaVO[] lstComposicaoMateriaPrimaVos = (ComposicaoMateriaPrimaVO[])Session["ComposicaoMateriaPrima"];
                List<ComposicaoMateriaPrimaVO> newlstComposicaoMateriaPrimaVos = new List<ComposicaoMateriaPrimaVO>(lstComposicaoMateriaPrimaVos);
                bool verificado = false; 
                foreach (var newlstComposicaoMateriaPrimaVo in newlstComposicaoMateriaPrimaVos)
                {
                    if (0 != newlstComposicaoMateriaPrimaVo.CoMaximo)
                    {
                        if (value < newlstComposicaoMateriaPrimaVo.CoMinimo)
                        {

                            if (verificado == false)
                            {
                                mensagem = newlstComposicaoMateriaPrimaVo.CoMinimo + " - " +
                                           newlstComposicaoMateriaPrimaVo.CoMaximo;
                                verificado = true;
                            }
                        }
                        else
                        {
                            if (value > newlstComposicaoMateriaPrimaVo.CoMaximo && verificado == false)
                            {
                                mensagem = newlstComposicaoMateriaPrimaVo.CoMinimo + " - " +
                                           newlstComposicaoMateriaPrimaVo.CoMaximo;
                            }
                            else
                            {
                                verificado = true;
                                mensagem = string.Empty;
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(mensagem))
                {
                    spanCo.Attributes.Add("title", mensagem);
                    spanCo.Style.Add("display", "inline");
                    ((TextBox)sender).BackColor = Color.Yellow;
                }
                else
                {
                    spanCo.Style.Add("display", "none");
                    ((TextBox)sender).BackColor = Color.White;
                }
            }
        }
        txtResistenciaTracao.Focus();
    }

    protected void txtResistenciaTracao_TextChanged(object sender, EventArgs e)
    {
        string mensagem = string.Empty;
        decimal value = 0;
        if (!string.IsNullOrEmpty(((TextBox)sender).Text))
        {
            if (Session["ResistenciaTracao"] != null)
            {
                value = decimal.Parse(((TextBox)sender).Text);
                //armazena em viewstate a linha selecionada para posterior atualização
                ResistenciaTracaoVO[] lstResistenciaTracao = (ResistenciaTracaoVO[])Session["ResistenciaTracao"];
                List<ResistenciaTracaoVO> newlstResistenciaTracaoVos = new List<ResistenciaTracaoVO>(lstResistenciaTracao);
                bool verificado = false;
                foreach (var newlstresistenciaTracaoVo in newlstResistenciaTracaoVos)
                {
                    if (0 != newlstresistenciaTracaoVo.Maximo)
                    {
                        if (value < newlstresistenciaTracaoVo.Minimo)
                        {

                            if (verificado == false)
                            {
                                mensagem = newlstresistenciaTracaoVo.Minimo + " - " +
                                           newlstresistenciaTracaoVo.Maximo;
                                verificado = true;
                            }
                        }
                        else
                        {
                            if (value > newlstresistenciaTracaoVo.Maximo && verificado == false)
                            {
                                mensagem = newlstresistenciaTracaoVo.Minimo + " - " +
                                           newlstresistenciaTracaoVo.Maximo;
                            }
                            else
                            {
                                verificado = true;
                                mensagem = string.Empty;
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(mensagem))
                {
                    spanRt.Attributes.Add("title", mensagem);
                    spanRt.Style.Add("display", "inline");
                    ((TextBox)sender).BackColor = Color.Yellow;
                }
                else
                {
                    spanRt.Style.Add("display", "none");
                    ((TextBox)sender).BackColor = Color.White;
                }
            }
        }
        txtDureza.Focus();
    }

    protected void hdfTipoPedido_ValueChanged(object sender, EventArgs e)
    {
        hdfTipoPedido.Value = hdfTipoPedido.Value;
    }

    #region Metodos do Produto Insumo

    #region btnSalvarInsumo_Click
    protected void btnSalvarInsumo_Click(object sender, EventArgs e)
    {
        //será incluido no grid de ICMS manualmente (não incluirá no banco ainda)
        //pois só deverá ser incluido no banco quando salvar o produto

        //armazena em viewstate a linha selecionada para posterior atualização
        ItemEntradaEstoqueInsumoVO[] lstItemEntradaEstoqueInsumo = (ItemEntradaEstoqueInsumoVO[])Session["lstItemEntradaEstoqueInsumo"];
        List<ItemEntradaEstoqueInsumoVO> newlstItemEntradaEstoqueInsumo = new List<ItemEntradaEstoqueInsumoVO>(lstItemEntradaEstoqueInsumo);
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
        int lote = !string.IsNullOrEmpty(txtLoteInsumo.Text) ? int.Parse(txtLoteInsumo.Text) : 0;
        decimal qtde = !string.IsNullOrEmpty(txtQtdEntregueInsumo.Text) ? decimal.Parse(txtQtdEntregueInsumo.Text) : 0;
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
            ItemEntradaEstoqueInsumoVO result = newlstItemEntradaEstoqueInsumo.Find(
            delegate(ItemEntradaEstoqueInsumoVO bk)
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
            ItemEntradaEstoqueInsumoVO lstItemItemEntradaEstoqueInsumoAux = new ItemEntradaEstoqueInsumoVO();

            lstItemItemEntradaEstoqueInsumoAux.ProdutoInsumoVo.CodProdutoInsumo = codProdutoInsumo;
            lstItemItemEntradaEstoqueInsumoAux.Lote = lote;
            lstItemItemEntradaEstoqueInsumoAux.ProdutoInsumoVo.Descricao = descricao;
            lstItemItemEntradaEstoqueInsumoAux.UnidadeVo.CodUnidade = codUnidade;
            lstItemItemEntradaEstoqueInsumoAux.UnidadeVo.TipoUnidade = unidade;
            lstItemItemEntradaEstoqueInsumoAux.Ipi = Ipi;
            lstItemItemEntradaEstoqueInsumoAux.Qtd = qtde;
            lstItemItemEntradaEstoqueInsumoAux.Valor = valor;
            lstItemItemEntradaEstoqueInsumoAux.CertificadoScanneado = new byte[0];
            newlstItemEntradaEstoqueInsumo.Add(lstItemItemEntradaEstoqueInsumoAux);
        }
        else
        {
            /************************************************************************
            Se a ação for alteração, verifica se o ítem já está cadastrado, se já estiver
            será impedido, desde que não seja ele mesmo
            /***********************************************************************/
            int linha = Convert.ToInt32(ViewState["LinhaSelecionadaItemEntradaEstoqueInsumo"]);
            if (linha != codProdutoInsumo)
            {
                var item1 =
                    newlstItemEntradaEstoqueInsumo.Where(r => r.ProdutoInsumoVo.CodProdutoInsumo == codProdutoInsumo).Select(r => r).
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
                newlstItemEntradaEstoqueInsumo.Where(r => r.ProdutoInsumoVo.CodProdutoInsumo == linha).Select(r => r).
                    FirstOrDefault();
            item.ProdutoInsumoVo.CodProdutoInsumo = codProdutoInsumo;
            item.Lote = lote;
            item.ProdutoInsumoVo.Descricao = descricao;
            item.UnidadeVo.CodUnidade = codUnidade;
            item.UnidadeVo.TipoUnidade = unidade;
            item.Ipi = Ipi;
            item.Qtd = qtde;
            item.Valor = valor;
            item.CertificadoScanneado = new byte[0];
            //sai do for
        }
        grdProdutoInsumo.DataSource = newlstItemEntradaEstoqueInsumo;
        grdProdutoInsumo.DataBind();
        //atualiza viewstate
        Session["lstItemEntradaEstoqueInsumo"] = newlstItemEntradaEstoqueInsumo.ToArray();
        LimparCamposItemPedidoCompraInsumo();
        Master.PosicionarFoco(ddlProdutoInsumo);
        ExecutarScript(updItensInsumo, new StringBuilder("showItensInsumo();"));
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
            int codItemEntradaEstoqueInsumo = Convert.ToInt32(codigos[1]);
            //armazena em viewstate a linha selecionada para posterior atualização
            ViewState["LinhaSelecionadaItemEntradaEstoqueInsumo"] = codProdutoInsumo;
            ItemEntradaEstoqueInsumoVO[] lstItemEntradaEstoqueInsumo =
                (ItemEntradaEstoqueInsumoVO[]) Session["lstItemEntradaEstoqueInsumo"];
            List<ItemEntradaEstoqueInsumoVO> newlstItemEntradaEstoqueInsumo =
                new List<ItemEntradaEstoqueInsumoVO>(lstItemEntradaEstoqueInsumo);

            if (e.CommandName == "Editar")
            {
                hdfTipoAcaoItemPedidoCompraInsumo.Value = "Editar";


                var item =
                    newlstItemEntradaEstoqueInsumo.Where(
                        r =>
                        r.ProdutoInsumoVo.CodProdutoInsumo == codProdutoInsumo &&
                        r.CodItemEntradaEstoqueInsumo == codItemEntradaEstoqueInsumo).Select(r => r)
                        .FirstOrDefault();
                //busca no vo os valores para a linha selecionada

                //atribui aos campos da tela para alteração
                txtLoteInsumo.Text = item.Lote.ToString();
                ddlProdutoInsumo.SelectedValue = item.ProdutoInsumoVo.CodProdutoInsumo.ToString();
                ddlUnidadeInsumo.SelectedValue = item.UnidadeVo.CodUnidade.ToString();
                txtQtdInsumo.Text = item.QtdPedidoCompra.ToString();
                txtIpiInsumo.Text = item.Ipi.ToString();
                txtValorInsumo.Text = item.Valor.ToString();
                Master.PosicionarFoco(txtLoteInsumo);
            }
            else if (e.CommandName == "Excluir")
            {
                newlstItemEntradaEstoqueInsumo.RemoveAt(linha);
                if (codItemEntradaEstoqueInsumo != 0)
                    new EntradaEstoque().ExcluirItem(codItemEntradaEstoqueInsumo);
                grdProduto.DataSource = newlstItemEntradaEstoqueInsumo;
                grdProduto.DataBind();
                updProduto.Update();
                //atualiza lstItemPedidoCompraInsumo
                Session["lstItemEntradaEstoqueInsumo"] = newlstItemEntradaEstoqueInsumo.ToArray();
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
            ItemEntradaEstoqueInsumoVO idenItemEntradaEstoqueInsumo = (ItemEntradaEstoqueInsumoVO)e.Row.DataItem;
            e.Row.Cells[1].Text = idenItemEntradaEstoqueInsumo.Lote.ToString();
            string descricao = idenItemEntradaEstoqueInsumo.ProdutoInsumoVo.Descricao;
            e.Row.Cells[2].Text = descricao;
            e.Row.Cells[3].Text = idenItemEntradaEstoqueInsumo.QtdPedidoCompra.ToString();
            e.Row.Cells[4].Text = idenItemEntradaEstoqueInsumo.Qtd.ToString();
            e.Row.Cells[5].Text = idenItemEntradaEstoqueInsumo.UnidadeVo.TipoUnidade;
            e.Row.Cells[6].Text = idenItemEntradaEstoqueInsumo.Ipi.ToString();
            e.Row.Cells[7].Text = idenItemEntradaEstoqueInsumo.Valor.ToString();
            e.Row.Cells[8].Text = idenItemEntradaEstoqueInsumo.CalcTotalItem.ToString();
            TotalGeralInsumo += idenItemEntradaEstoqueInsumo.CalcTotalItem;

            #region Botão Editar
            ImageButton imgEditarFatura = (ImageButton)e.Row.FindControl("imgEditar");
            imgEditarFatura.ImageUrl = caminhoAplicacao + @"Imagens\editar.png";
            //imgEditarFatura.CommandArgument = identItemPedidoCompraInsumo.CodItemPedidoCompraInsumo.ToString();
            imgEditarFatura.CommandArgument = idenItemEntradaEstoqueInsumo.ProdutoInsumoVo.CodProdutoInsumo + "|" + idenItemEntradaEstoqueInsumo.CodItemEntradaEstoqueInsumo;
            imgEditarFatura.CommandName = "Editar";
            imgEditarFatura.Style.Add("cursor", "hand");
            imgEditarFatura.ToolTip = "Editar dados do Produto [" + descricao + "]";
            imgEditarFatura.Attributes["onclick"] = "showItensInsumo();";
            #endregion

            #region Botão Excluir
            ImageButton imgExcluirFatura = (ImageButton)e.Row.FindControl("imgExcluir");
            imgExcluirFatura.ImageUrl = caminhoAplicacao + @"Imagens\exclusao_Canc.png";
            //imgExcluirFatura.CommandArgument = identItemPedidoCompraInsumo.CodItemPedidoCompraInsumo.ToString();
            imgExcluirFatura.CommandArgument = idenItemEntradaEstoqueInsumo.ProdutoInsumoVo.CodProdutoInsumo + "|" + idenItemEntradaEstoqueInsumo.CodItemEntradaEstoqueInsumo;
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
            e.Row.Cells[8].Attributes.Add("align", "right");
            e.Row.Cells[8].Text = TotalGeralInsumo.ToString();
        }

    }
    #endregion
    #endregion


}
