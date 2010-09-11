using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nissi.Business;
using Nissi.Model;



namespace Nissi.WebPresentation
{
    public partial class NFEParametros : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Pesquisar();
              
            }
        }

        private void Pesquisar()
        {
            List<ParametroVO> lParametro = new Parametro().Listar();
            if (lParametro.Count > 0)
            {
                DadosParametro = (ParametroVO)lParametro[0];
                ModoAlterar = true;
            }
            else
            {
                ModoAlterar = false;
            }
        }

        private void Limpar()
        {
            txtCancelamento.Text =
            txtCertificado.Text =
            txtDanfeInfo.Text =
            txtInutilizacao.Text =
            txtModelo.Text =
            txtProc.Text =
            txtRecepcao.Text =
            txtRetRecepcao.Text =
            txtStatuS.Text =
            lblDataPacket.Text =
            lblDataPacketForm.Text =
            lblLogoDanfe.Text =
            lblPathPrincipal.Text =
            lblSchema.Text =
            txtProtocolo.Text = "";
            ddlAmbiente.SelectedValue = 
            ddlSerie.SelectedValue = 
            ddlTipoDanfe.SelectedValue = "0";
            ddlTrace.SelectedValue = "-1";
           chkTotalCFOP.Checked = false;
 
        }


        #region Propriedades
        public ParametroVO DadosParametro
        {
            set
            {
                txtCancelamento.Text = value.NFeCancelamento;
                txtCertificado.Text = value.NoSerieCertificado;
                txtDanfeInfo.Text = value.DanfeInfo;
                txtInutilizacao.Text = value.NFeInutilizacao;
                txtModelo.Text = value.Modelo;
                txtProc.Text = value.VerProc;
                txtRecepcao.Text = value.NFeRecepcao;
                txtRetRecepcao.Text = value.NFeRetRecepcao;
                txtStatuS.Text = value.NFeStatusServico;
                ddlAmbiente.SelectedValue = value.Ambiente;
                ddlSerie.SelectedValue = value.Serie;
                ddlTipoDanfe.SelectedValue = value.TipoDanfe;
                ddlTrace.SelectedValue = value.AtivaTrace;
                chkTotalCFOP.Checked = value.TotalizarCfop == "0" ? false : true;
                lblDataPacket.Text = value.DataPacket;
                lblDataPacketForm.Text = value.DataPacketFormSeg;
                lblLogoDanfe.Text = value.DanfeLogo;
                lblPathPrincipal.Text = value.PathPrincipal;
                lblSchema.Text = value.Schemas;
                txtProtocolo.Text = value.NFeConsultaProtocolo;
                
            }

            
            get
            {
                ParametroVO identParametro = new ParametroVO();
                identParametro.Ambiente             = ddlAmbiente.SelectedValue;
                identParametro.AtivaTrace           = ddlTrace.SelectedValue;
                identParametro.DanfeInfo            = txtDanfeInfo.Text;
                identParametro.TipoDanfe            = ddlTipoDanfe.SelectedValue;
                identParametro.Modelo               = txtModelo.Text;
                identParametro.NFeCancelamento      = txtCancelamento.Text;
                identParametro.NFeConsultaProtocolo = txtProtocolo.Text;
                identParametro.NFeInutilizacao      = txtInutilizacao.Text;
                identParametro.NFeRecepcao          = txtRecepcao.Text;
                identParametro.NFeRetRecepcao       = txtRetRecepcao.Text;
                identParametro.NFeStatusServico     = txtStatuS.Text;
                identParametro.NoSerieCertificado   = txtCertificado.Text;
                identParametro.Serie                = ddlSerie.SelectedValue;
                identParametro.TotalizarCfop        = Convert.ToString(chkTotalCFOP.Checked == true ? 1 : 0);
                identParametro.VerProc              = txtProc.Text;
                if (hdfTipoAcao.Value == "Alterar")
                {
                    if (!string.IsNullOrEmpty(lblDataPacket.Text))
                    {
                        identParametro.DataPacket = lblDataPacket.Text;
                    }
                    else if (!string.IsNullOrEmpty(fileDataPacket.FileName.ToString()))
                    {
                        identParametro.DataPacket = fileDataPacket.PostedFile.FileName.ToString();
                    }


                    if (!string.IsNullOrEmpty(lblDataPacketForm.Text))
                    {
                        identParametro.DataPacketFormSeg = lblDataPacketForm.Text;
                    }
                    else{ 
                        if (!string.IsNullOrEmpty(fileDataPacketForm.FileName.ToString()))                    
                            identParametro.DataPacketFormSeg = fileDataPacketForm.PostedFile.FileName.ToString();
                     }


                    if (!string.IsNullOrEmpty(lblLogoDanfe.Text))
                    {
                        identParametro.DanfeLogo = lblLogoDanfe.Text;
                    }
                    else if (!string.IsNullOrEmpty(fileLogoDanfe.FileName.ToString()))
                    {
                        identParametro.DanfeLogo = fileLogoDanfe.PostedFile.FileName.ToString();
                    }


                    if (!string.IsNullOrEmpty(lblPathPrincipal.Text))
                    {
                        identParametro.PathPrincipal = lblPathPrincipal.Text;
                    }
                    else if (!string.IsNullOrEmpty(filePathPrincipal.FileName.ToString()))
                    {
                        identParametro.PathPrincipal = filePathPrincipal.PostedFile.FileName.ToString();
                    }


                    if (!string.IsNullOrEmpty(lblSchema.Text))
                    {
                        identParametro.Schemas = lblSchema.Text;
                    }
                    else if (!string.IsNullOrEmpty(fileSchema.FileName.ToString()))
                    {
                        identParametro.Schemas = fileSchema.PostedFile.FileName.ToString();
                    }


                }
                else
                {
                    identParametro.DanfeLogo = fileLogoDanfe.PostedFile.FileName.ToString();
                    identParametro.DataPacket = fileDataPacket.PostedFile.FileName.ToString();
                    identParametro.DataPacketFormSeg = fileDataPacketForm.PostedFile.FileName.ToString();
                    identParametro.PathPrincipal = filePathPrincipal.PostedFile.FileName.ToString();
                    identParametro.Schemas = fileSchema.PostedFile.FileName.ToString();
                }
                
                return identParametro;
            }
        }
        #endregion



        private int Salvar()
        {
           int retorno = new Parametro().Incluir(DadosParametro);
           return retorno;
        }
        /*protected void ddlAmbiente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAmbiente.SelectedValue == "1")
            {
                txtRecepcao.Text = "https://nfe.fazenda.sp.gov.br/nfeweb/services/nferecepcao2.asmx";
                txtRetRecepcao.Text = "https://nfe.fazenda.sp.gov.br/nfeweb/services/nferetrecepcao2.asmx";
                txtCancelamento.Text = "https://nfe.fazenda.sp.gov.br/nfeweb/services/nfecancelamento2.asmx";
                txtInutilizacao.Text = "https://nfe.fazenda.sp.gov.br/nfeweb/services/nfeinutilizacao2.asmx";
                txtProtocolo.Text = "https://nfe.fazenda.sp.gov.br/nfeweb/services/nfeconsulta2.asmx";
                txtStatuS.Text = "https://nfe.fazenda.sp.gov.br/nfeweb/services/nfestatusservico2.asmx";
            }
            else if(ddlAmbiente.SelectedValue == "2")
            {
                txtRecepcao.Text = "https://homologacao.nfe.fazenda.sp.gov.br/nfeweb/services/nferecepcao.asmx";
                txtRetRecepcao.Text = "https://homologacao.nfe.fazenda.sp.gov.br/nfeweb/services/nferetrecepcao.asmx";
                txtCancelamento.Text = "https://homologacao.nfe.fazenda.sp.gov.br/nfeweb/services/nfecancelamento.asmx";
                txtInutilizacao.Text = "https://homologacao.nfe.fazenda.sp.gov.br/nfeweb/services/nfeinutilizacao.asmx";
                txtProtocolo.Text = "https://homologacao.nfe.fazenda.sp.gov.br/nfeweb/services/nfeconsulta.asmx";
                txtStatuS.Text = "https://homologacao.nfe.fazenda.sp.gov.br/nfeweb/services/nfestatusservico.asmx";
            }
        }*/

        private bool enModoAlterar;
        public bool ModoAlterar
        {
            set {
                enModoAlterar = value;
                    
                if (value)
                {
                    btnSalvar.Text = "Alterar";
                    hdfTipoAcao.Value = "Alterar";
                    btnExcluir.Visible = true;
                }
                else
                {
                    btnSalvar.Text = "Salvar";
                    hdfTipoAcao.Value = "Incluir";
                    btnExcluir.Visible = false;
                }
            }
            get { return enModoAlterar; }
        }



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
                new Parametro().Incluir(DadosParametro);
                ModoAlterar = true;
                Pesquisar();
                
            }
            else
            { 
                new Parametro().Alterar(DadosParametro);
                ModoAlterar = true;
                Pesquisar();
            }

        }
        #endregion

        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            new Parametro().Excluir();
            Pesquisar();
            Limpar();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Default.aspx");
        }

    }
}
