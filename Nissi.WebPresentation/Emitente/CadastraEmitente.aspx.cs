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
using System.Globalization;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Web.SessionState;
using System.IO;
using Nissi.Model;
using Nissi.Business;
#endregion

public partial class CadastraEmitente : BasePage
{
    private const string key_Imagem = "Imagem";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Pesquisar();
        }
    }


    private void Load_Picture()
    {
        byte[] ImagemLogo = (byte[])ViewState[key_Imagem];
        if (ImagemLogo == null)
        {
            imgImagem.ImageUrl = "";
            imgImagem.AlternateText = "(Nenhuma imagem carregada)";
        }
        else
        {
            string sVarCache = "Imagem";
            Cache[sVarCache] = ImagemLogo;
            imgImagem.ImageUrl ="~/GeraImagem.aspx?Variavel_Cache=" + sVarCache;
            imgImagem.AlternateText = "";
            updImage.Update();
        }
    }

    private void limparImagem()
    {
        imgImagem.ImageUrl = "";
        imgImagem.AlternateText = "(Nenhuma imagem carregada)";
        ViewState.Clear();
           

    }

    protected void btnCarregarImagem_Click(object sender, EventArgs e)
    {

        //btnCarregarImagem.Attributes.Add("onclick", "return ValidaArquivoImagem();");
        if ((upFileUp.PostedFile == null) || (upFileUp.PostedFile.ContentLength == 0)
            || !upFileUp.PostedFile.ContentType.StartsWith("image/"))
            ExecutarScript(updBotoes,new StringBuilder("alert('Informe um arquivo de imagem válido'); "));

        else
        {
            mpeTransIncluir.Show();
            System.IO.Stream Input = upFileUp.PostedFile.InputStream;
            // Inicializa o buffer			
            byte[] ImagemLogo = new byte[Input.Length];
            // Lê a imagem do arquivo          			 
            Input.Read(ImagemLogo, 0, Convert.ToInt32(Input.Length));
            // Joga no ViewState
            ViewState[key_Imagem] =  ImagemLogo;
            string sVarCache = "Imagem";
            Cache[sVarCache] = ImagemLogo;
            imgImagem.ImageUrl ="~/GeraImagem.aspx?Variavel_Cache=" + sVarCache; 
            imgImagem.AlternateText = "";
            updImage.Update();
        }

        // Carrega no controle
        
      // Load_Picture();
        
      
    }

      #region Propriedades
    public EmitenteVO DadosEmitente
        {
            set
            {
                hdfCodEmitente.Value = value.CodEmitente.ToString();
                //txtCelular.Text = Mascara("TEL", value.Celular);
                CEPVO identCep = new CEPVO();
                identCep.CodCep = value.Cep.CodCep;
                identCep = new CEP().Listar(identCep);
                Endereco1.preencheCep(identCep,new ArrayList());
                txtTelefone.Text = Mascara("TEL", value.Telefone);
                tbxFax.Text = Mascara("TEL", value.Fax);
                txtCNPJ.Text = Mascara("CNPJ", value.CNPJ);
                Endereco1.Complemento = value.Complemento;
                txtCNAE.Text = value.CNAE;
                txtInscricaoEstadual.Text = value.InscricaoEstadual;
                txtNomeFantasia.Text = value.NomeFantasia;
                Endereco1.Numero = value.Numero.ToString();
                tbxEmail.Text = value.Email;
                
                txtRazaoSocial.Text = value.RazaoSocial;                
                ViewState[key_Imagem] = value.Image;
                Load_Picture();
            
            }
            get
            {
                EmitenteVO identEmitenteVO = new EmitenteVO();
                identEmitenteVO.CodEmitente = hdfCodEmitente.Value != "" ? Convert.ToInt32(hdfCodEmitente.Value.Replace(",", "")) : int.MinValue;
                identEmitenteVO.Cep.CodCep = Endereco1.CodCep.ToString().Replace("-", "");
                identEmitenteVO.Logradouro = Endereco1.Logradouro;
                identEmitenteVO.Cep.Cidade.CodCidade = Convert.ToInt32(Endereco1.CodCidade);
                identEmitenteVO.Cep.Cidade.UF.CodUF = Endereco1.CodUF;
                identEmitenteVO.Complemento = Endereco1.Complemento;
                identEmitenteVO.Numero =Convert.ToInt32(Endereco1.Numero);
                identEmitenteVO.CNPJ = txtCNPJ.Text.Replace(".", "").Replace("-", "").Replace("/", "");
                identEmitenteVO.InscricaoEstadual = txtInscricaoEstadual.Text.Replace(".", "").Replace("-", "").Replace("/", "");
                if (!string.IsNullOrEmpty(txtTelefone.Text))
                    identEmitenteVO.Telefone = txtTelefone.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "").Trim();
                if (!string.IsNullOrEmpty(tbxFax.Text))
                    identEmitenteVO.Fax = tbxFax.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "").Trim();
                identEmitenteVO.Email = tbxEmail.Text;
                identEmitenteVO.RazaoSocial = txtRazaoSocial.Text;
                identEmitenteVO.NomeFantasia = txtNomeFantasia.Text;
                identEmitenteVO.CNAE = txtCNAE.Text;
                if (ViewState[key_Imagem]!= null) 
                identEmitenteVO.Image = (byte[])ViewState[key_Imagem];
                return identEmitenteVO;
            }

        }
        #endregion

    #region Eventos

    #region btnPesquisar_Click
    protected void btnPesquisar_Click(object sender, EventArgs e)
    {
        Pesquisar();
        tblConsulta.Style.Add("class", "fundotabela");
    }
    #endregion

    #region btnIncluir_Click
    protected void btnIncluir_Click(object sender, EventArgs e)
    {
        hdfTipoAcao.Value = "Incluir";
        Master.PosicionarFoco(txtRazaoSocial);
        LimparCampos();
        mpeTransIncluir.Show();
    }
    #endregion

    #region btnCancelar_Click
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        mpeTransIncluir.Hide();    
    }
    #endregion

    #region btnVoltar_Click
    protected void btnVoltar_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Default.aspx");
    }
    #endregion

    #region btnSalvar_Click
    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        if (hdfTipoAcao.Value == "Incluir")
        { 
           hdfCodEmitente.Value =  new Emitente().Incluir(DadosEmitente).ToString(); }
        else
        { new Emitente().Alterar(DadosEmitente); }

            Pesquisar();
            mpeTransIncluir.Hide();
    }
    #endregion

    #endregion

    #region Métodos

    private void LimparCampos() 
    {
        txtCNPJ.Text =
        txtInscricaoEstadual.Text =
        txtTelefone.Text =
        txtTelefone.Text = 
        hdfCodPessoa.Value =
        txtRazaoSocial.Text =
        txtNomeFantasia.Text =
        hdfCodEmitente.Value = "";
        Endereco1.limpaCampos();
        DestroiValorSessao("TipoAcao");
        string sVarCache = "Imagem";
        Cache[sVarCache] = "";
        imgImagem.ImageUrl = "";
        mpeTransIncluir.Hide();
    }

    #endregion

    #region Métodos da Grid
    protected void grdListaResultado_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    protected void grdListaResultado_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        EmitenteVO identEmitente = new EmitenteVO();

        identEmitente.CodEmitente = int.Parse(e.CommandArgument.ToString());

        //Módulo de exclusão
        if (e.CommandName == "Excluir")
        {
            //Excluir
            new Emitente().Excluir(identEmitente);

            //Atualizar Lista
            Pesquisar();
        }
        else if (e.CommandName == "Editar")  //Módulo de alteração
        {
            //ArmazenaValorSessao("TipoAcao", "Editar");
            hdfTipoAcao.Value = "Editar";
            DadosEmitente = new Emitente().Listar(identEmitente)[0];

            //Alimentar campos para edição
            mpeTransIncluir.Show();
        }

    }

    private void Pesquisar()
    {
        EmitenteVO identEmitente = new EmitenteVO();
        grdListaResultado.DataSource = new Emitente().Listar(identEmitente);
        grdListaResultado.DataBind();
    }

    protected void grdListaResultado_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            EmitenteVO tempEmitente =(EmitenteVO) e.Row.DataItem;

            e.Row.Cells[1].Text = tempEmitente.RazaoSocial;
            e.Row.Cells[2].Text = tempEmitente.NomeFantasia;
            e.Row.Cells[3].Text = Mascara("CNPJ", tempEmitente.CNPJ);
            e.Row.Cells[4].Text = tempEmitente.InscricaoEstadual;
            e.Row.Cells[5].Text = Mascara("TEL",tempEmitente.Telefone);

            #region Botão Editar
            ImageButton imgEditar = (ImageButton)e.Row.FindControl("imgEditar");
            imgEditar.ImageUrl = caminhoAplicacao + @"Imagens\editar.png";
            imgEditar.CommandArgument = tempEmitente.CodEmitente.ToString();
            imgEditar.CommandName = "Editar";
            imgEditar.Style.Add("cursor", "hand");
            imgEditar.ToolTip = "Editar dados da Emitente [" + tempEmitente.RazaoSocial.Trim() + "]";
            #endregion

            #region Botão Excluir
            ImageButton imgExcluir = (ImageButton)e.Row.FindControl("imgExcluir");
            imgExcluir.ImageUrl = caminhoAplicacao + @"Imagens\exclusao_Canc.png";
            imgExcluir.CommandArgument = tempEmitente.CodEmitente.ToString();
            imgExcluir.CommandName = "Excluir";
            imgExcluir.Attributes["onclick"] = "return confirm('Confirmar exclusão do Cliente [" + tempEmitente.RazaoSocial.Trim() + "]?');";
            imgExcluir.Style.Add("cursor", "hand");
            imgExcluir.ToolTip = "Excluir Cliente [" + tempEmitente.RazaoSocial.Trim() + "]";
            #endregion

            if (e.Row.RowState == DataControlRowState.Normal)
                e.Row.CssClass = "FundoLinha1";
            else if (e.Row.RowState == DataControlRowState.Alternate)
                e.Row.CssClass = "FundoLinha2";

        
        }

    }

    #endregion

    protected void btnLimparImagem_Click(object sender, EventArgs e)
    {
        limparImagem();
        updImage.Update();
    }


}




