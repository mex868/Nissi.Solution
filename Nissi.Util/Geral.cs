using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

namespace Nissi.Util
{

    /// <summary>
    /// Classe para métodos gerais
    /// </summary>
    public static class Geral
    {
        /// <summary>
        /// Método geral para carregar DropDownList
        /// </summary>
        /// <param name="ddl">DropDownList passar por referência</param>
        /// <param name="lista">Array com dados para carregar na DropDownList</param>
        /// <param name="strDataValueField">campo do List que será o DataValueField da DropDownList</param>
        /// <param name="strDataTextField">campo do List que será o DataTextField da DropDownList</param>
        public static void CarregarDDL(ref DropDownList ddl, Array lista, string strDataValueField, string strDataTextField)
        {
           if (lista.Length > 0)
           {
               ddl.DataSource = lista;
               ddl.DataValueField = strDataValueField;
               ddl.DataTextField = strDataTextField;
               ddl.DataBind();
               ddl.Items.Insert(0, new ListItem("(Selecione)", string.Empty));
           }
        }

    }
}