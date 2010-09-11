using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using Nissi.Model;
using Nissi.Business;
using Nissi.Util;


/// <summary>
/// Summary description for BasePage
/// </summary>
public class BasePage : Page
{
    #region Constantes
    public const short perfilAdmForum = 1;
    public const short perfilSTI = 40;
    public const short perfilAdmSis = 44;
    #endregion

    protected override void OnPreRender(EventArgs e)
    {
        StringBuilder strScript = new StringBuilder();

        FuncionarioVO tempUser = (FuncionarioVO)HttpContext.Current.Session["UsuarioAtivo"];

        if (tempUser != null)
        {
            if (!UsuarioAtivo.ModoDesenvolvedor)
            {
                strScript.Append(" document.onkeydown = function(){ ");

                //Bloqueia o F3
                strScript.Append(" if (event.keyCode == 114){ event.keyCode = 0; return false; } ");

                //Bloqueia o F11
                strScript.Append(" if (event.keyCode == 122){ event.keyCode = 0; return false; } ");

                // Bloqueia o Crtl+N
                strScript.Append(" if (event.keyCode == 78 & event.ctrlKey == true){ event.keyCode = 0; return false; } ");

                // Bloqueia o Crtl+O
                strScript.Append(" if (event.keyCode == 79 & event.ctrlKey == true){ event.keyCode = 0; return false; } ");

                // Bloqueia o Crtl+P
                strScript.Append(" if (event.keyCode == 80 & event.ctrlKey == true){ event.keyCode = 0; return false; } ");

                // Bloqueia o Crtl+E
                strScript.Append(" if (event.keyCode == 69 & event.ctrlKey == true){ event.keyCode = 0; return false; } ");

                // Bloqueia o Crtl+I
                strScript.Append(" if (event.keyCode == 73 & event.ctrlKey == true){ event.keyCode = 0; return false; } ");

                // Bloqueia o Crtl+H
                strScript.Append(" if (event.keyCode == 72 & event.ctrlKey == true){ event.keyCode = 0; return false; } ");

                //Bloqueia o F5
                strScript.Append(" if (event.keyCode == 116){ event.keyCode = 0; return false; } ");

                // Bloqueia o ESC
                strScript.Append(" if (event.keyCode == 27){ event.keyCode = 0; return false; } ");

                //Transforma o Backspace em <- mais Del
                strScript.Append(" if (document.activeElement.type != 'file'){ if (event.keyCode == 8){ event.keyCode = 37 + 46; return true; } } } ");

                //Bloqueia o botão direito do mouse
                strScript.Append(" ; document.oncontextmenu = function(){ return false; } ");

                ScriptManager.RegisterStartupScript(this, typeof(string), "scriptDesenvolvedor", strScript.ToString(), true);
            }
        }

        base.OnPreRender(e);
    }

    #region Inicializacao
    public BasePage()
    { }
    #endregion

    #region ControleCliente
    /// <summary>
    /// Executa um alert em javascript
    /// </summary>
    /// <returns></returns>
    public void MensagemCliente(string Mensagem)
    {
        ScriptManager.RegisterStartupScript(this, typeof(string), "script", " window.alert('" + Mensagem + "'); ", true);
    }
    /// <summary>
    /// Executa um alert em javascript e redireciona para uma pagina
    /// </summary>
    /// <returns></returns>
    public void MensagemCliente(string Mensagem, string PaginaRedirecionamento)
    {
        ScriptManager.RegisterStartupScript(this, typeof(string),
               "script", " window.alert('" + Mensagem + "'); window.location.href = '" + PaginaRedirecionamento + "'; ", true);
    }
    /// <summary>
    /// Executa um alert em javascript e outro script qualquer.
    /// </summary>
    /// <param name="Mensagem"></param>
    /// <param name="scriptJs"></param>
    public void MensagemCliente(string Mensagem, StringBuilder scriptJs)
    {
        ScriptManager.RegisterStartupScript(this, typeof(string),
                   "script", " window.alert('" + Mensagem + "'); " + scriptJs.ToString(), true);
    }
    /// <summary>
    /// Executa um script
    /// </summary>
    /// <returns></returns>
    public void ExecutarScript(StringBuilder scriptJs)
    {
        ScriptManager.RegisterStartupScript(this, typeof(string), "script", scriptJs.ToString(), true);
    }
    #endregion

    #region ControleDeSessao
    public void ArmazenaValorSessao(string Chave, object Valor)
    {
        Session.Add(Chave, Valor);
    }
    public object RecuperaValorSessao(string Chave)
    {
        object tempObjeto = Session[Chave];

        if (tempObjeto != null)
            ArmazenaValorSessao(Chave, tempObjeto);

        return tempObjeto;
    }
    public void DestroiValorSessao(string Chave)
    {
        Session.Remove(Chave);
    }
    #endregion Sessao

    #region Função para igualar de classe
    /// <summary>
    /// Iguala duas classes sem criar referência
    /// </summary>
    /// <returns></returns>
    public void IgualarClasse(Object ClasseOrigem, out Object ClasseDestino)
    {
        Object tempclasseorigem;

        ClasseDestino = null;

        try
        {
            //Serializacao necessaria para solucionar o problema de inserir a referencia do objeto
            //ao inves do seu valor
            BinaryFormatter Serializar = new BinaryFormatter();
            MemoryStream memoria = new MemoryStream();

            Serializar.Serialize(memoria, ClasseOrigem);
            memoria.Position = 0;
            tempclasseorigem = Serializar.Deserialize(memoria);
        }
        catch
        {
            tempclasseorigem = ClasseDestino;
        }

        ClasseDestino = tempclasseorigem;
    }
    #endregion

    #region Propriedades
    private FuncionarioVO mUsuarioAtivo;
    public FuncionarioVO UsuarioAtivo
    {
        get
        {
            mUsuarioAtivo = new FuncionarioVO();
            mUsuarioAtivo = (FuncionarioVO) RecuperaValorSessao("UsuarioAtivo");

            if (mUsuarioAtivo == null)
                Response.Redirect("~/login.aspx");

            return mUsuarioAtivo;
        }
        set
        {
            ArmazenaValorSessao("UsuarioAtivo", value);
        }
    }
    #endregion

    #region Funções Auxiliares
    /*
    /// <summary>
    /// Armazena a ViewState na sessão para diminuir o tamanho da pagina
    /// </summary>
    protected override PageStatePersister PageStatePersister
    {
        get
        {
            return new SessionPageStatePersister(this);
        }
    }
    */

    /// <summary>
    /// Verifica se o usuário possui o perfil informado via parâmetro
    /// </summary>
    /// <param name="codPerfil"></param>
    /// <returns>True=Acesso concedido, False=Acesso negado</returns>
    public bool VerificarPerfilAcesso(short codPerfil)
    {
        bool possuiPerfil = false;

        foreach (PerfilAcessoVO identPerfil in UsuarioAtivo.Perfils)
        {
            if (identPerfil.CodPerfilAcesso == codPerfil)
            {
                possuiPerfil = true;
                break;
            }
        }

        return possuiPerfil;
    }
    #endregion

    #region LoadPageStateFromPersistenceMedium
    protected override object LoadPageStateFromPersistenceMedium()
    {
        string vState = this.Request.Form["__VSTATE"];
        Byte[] bytes = Convert.FromBase64String(vState);
        bytes = Zip.Decompress(bytes);
        LosFormatter format = new LosFormatter();
        return format.Deserialize(Convert.ToBase64String(bytes));
    }
    #endregion

    #region SavePageStateToPersistenceMedium
    protected override void SavePageStateToPersistenceMedium(object state)
    {
        LosFormatter format = new LosFormatter();
        StringWriter sw = new StringWriter();
        format.Serialize(sw, state);
        string vw = sw.ToString();
        byte[] bytes = Convert.FromBase64String(vw);
        bytes = Zip.Compress(bytes);
        string vwFinal = Convert.ToBase64String(bytes);
        ScriptManager.RegisterHiddenField(Page, "__VSTATE", vwFinal);
    }
    #endregion
}
