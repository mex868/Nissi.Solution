using System;
using AjaxControlToolkit;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections.Generic;
using Nissi.Business;
using Nissi.Model;


namespace Nissi.WebPresentation.App_Code
{
    /// <summary>
    /// Métodos de Listagem para o controle CascadingDropDownList
    /// </summary>
    [WebService(Namespace = "Nissi.Services")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.Web.Script.Services.ScriptService]

    public class CascadingWS : System.Web.Services.WebService
    {
        #region Construtor
        public CascadingWS()
        {
        }
        #endregion

        #region Listagem de UF
        [WebMethod]
        public CascadingDropDownNameValue[] ListaUF(string knownCategoryValues, string category)
        {
            List<CascadingDropDownNameValue> valoresCombo = new List<CascadingDropDownNameValue>();

            try
            {
                System.Diagnostics.Debug.WriteLine("(CascadingDropDown)Lista UF");

                List<UnidadeFederacaoVO> listaUF;

                if (category.Equals("UF"))
                    listaUF = new UnidadeFederacao().Listar();
                else
                    listaUF = new List<UnidadeFederacaoVO>();

                UnidadeFederacaoVO identUF = new UnidadeFederacaoVO();

                foreach (UnidadeFederacaoVO tempUF in listaUF)
                {
                    if (tempUF.CodUF != "SP")
                        valoresCombo.Add(new CascadingDropDownNameValue(tempUF.CodUF, tempUF.CodUF));
                    else
                    {
                        identUF = new UnidadeFederacaoVO();
                        identUF.CodUF = tempUF.CodUF;
                        identUF.NomUF = tempUF.NomUF;
                    }
                }

                if (!string.IsNullOrEmpty(identUF.CodUF))
                    valoresCombo.Insert(0, new CascadingDropDownNameValue(identUF.CodUF, identUF.CodUF));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Erro Listar UF: " + ex.Message);
            }

            return valoresCombo.ToArray();
        }
        #endregion

        #region Listagem de Cidade
        [WebMethod]
        public CascadingDropDownNameValue[] ListaCidade(string knownCategoryValues, string category)
        {
            List<CascadingDropDownNameValue> valoresCombo = new List<CascadingDropDownNameValue>();

            try
            {
                Cidade cidade = new Cidade();
                System.Diagnostics.Debug.WriteLine("(CascadingDropDown)Lista Cidade");
                string[] categoryValues = knownCategoryValues.Split(':', ';');
                string codUF = categoryValues[1].ToString();

                List<CidadeVO> listaCidade;
                UnidadeFederacaoVO identUF = new UnidadeFederacaoVO();
                identUF.CodUF = codUF;

                if (category.Equals("cidade"))
                    listaCidade = new Cidade().Listar(identUF);
                else
                    listaCidade = new List<CidadeVO>();

                CidadeVO identCidadeSP = new CidadeVO();

                foreach (CidadeVO identCidade in listaCidade)
                {
                    if (identCidade.NomCidade.Equals("São Paulo"))
                    {
                        identCidadeSP.CodCidade = identCidade.CodCidade;
                        identCidadeSP.NomCidade = identCidade.NomCidade;
                        identCidadeSP.UF.CodUF = identCidade.UF.CodUF;
                    }
                    else
                        valoresCombo.Add(new CascadingDropDownNameValue(identCidade.NomCidade, identCidade.CodCidade.ToString()));
                }

                if (!string.IsNullOrEmpty(identCidadeSP.NomCidade))
                    valoresCombo.Insert(0, new CascadingDropDownNameValue(identCidadeSP.NomCidade, identCidadeSP.CodCidade.ToString()));

                return valoresCombo.ToArray();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Erro Listar Cidade: " + ex.Message);
            }

            return valoresCombo.ToArray();
        }

        #endregion

        #region Listagem de Cidade Opção Todos
        [WebMethod]
        public CascadingDropDownNameValue[] ListaCidadeOpcaoTodos(string knownCategoryValues, string category)
        {
            List<CascadingDropDownNameValue> valoresCombo = new List<CascadingDropDownNameValue>();

            try
            {
                Cidade cidade = new Cidade();
                System.Diagnostics.Debug.WriteLine("(CascadingDropDown)Lista Cidade Opcao Todos");
                string[] categoryValues = knownCategoryValues.Split(':', ';');
                string codUF = categoryValues[1].ToString();

                List<CidadeVO> listaCidade;
                UnidadeFederacaoVO identUF = new UnidadeFederacaoVO();
                identUF.CodUF = codUF;

                if (category.Equals("cidade"))
                    listaCidade = new Cidade().Listar(identUF);
                else
                    listaCidade = new List<CidadeVO>();

                CidadeVO identCidadeSP = new CidadeVO();

                foreach (CidadeVO identCidade in listaCidade)
                {
                    if (identCidade.NomCidade.Equals("São Paulo"))
                    {
                        identCidadeSP.CodCidade = identCidade.CodCidade;
                        identCidadeSP.NomCidade = identCidade.NomCidade;
                        identCidadeSP.UF.CodUF = identCidade.UF.CodUF;
                    }
                    else
                        valoresCombo.Add(new CascadingDropDownNameValue(identCidade.NomCidade, identCidade.CodCidade.ToString()));
                }

                if (!string.IsNullOrEmpty(identCidadeSP.NomCidade))
                    valoresCombo.Insert(0, new CascadingDropDownNameValue(identCidadeSP.NomCidade, identCidadeSP.CodCidade.ToString()));

                //Opção Todos
                if (listaCidade.Count > 0)
                {
                    identCidadeSP.CodCidade = 0;
                    identCidadeSP.NomCidade = "(Todos)";
                    identCidadeSP.UF.CodUF = identUF.CodUF;
                    valoresCombo.Insert(0, new CascadingDropDownNameValue(identCidadeSP.NomCidade, identCidadeSP.CodCidade.ToString()));
                }

                return valoresCombo.ToArray();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Erro Listar Cidade Opção Todos: " + ex.Message);
            }

            return valoresCombo.ToArray();
        }
        #endregion

    }
}