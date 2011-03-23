using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nissi.Business;
using Nissi.Model;
using System.Text;


namespace Nissi.WebPresentation.Relatorios
{
    public partial class EmitirRelatorios : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarCombos();
            }

        }

        private void CarregarCombos()
        {

                //Carrega a combo com o uf
                ddlUF.DataSource = new UnidadeFederacao().Listar();
                ddlUF.DataTextField = "CodUF";
                ddlUF.DataValueField = "CodUF";
                ddlUF.DataBind();
                ddlUF.Items.Insert(0, new ListItem("Todos", ""));

                ddlRegiao.Items.Add(new ListItem("Notas Fiscais no Estado", "1"));
                ddlRegiao.Items.Add(new ListItem("Notas Fiscais Fora do Estado", "2"));
                ddlRegiao.DataBind();

            
          
        }
    }
}