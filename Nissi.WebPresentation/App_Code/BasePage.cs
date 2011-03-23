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
using System.ComponentModel;


/// <summary>
/// Summary description for BasePage
/// </summary>
public class BasePage : Page
{
    #region Constantes
    public const short perfilAdmForum = 1;
    public const short perfilSTI = 40;
    public const short perfilAdmSis = 44;
    protected string caminhoAplicacao = ConfigurationManager.AppSettings["Nissi.CaminhoAplicacao"].ToString();


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
    /// Executa um alert em javascript
    /// </summary>
    /// <returns></returns>
    public void MensagemCliente(UpdatePanel Controle,string Mensagem)
    {
        ScriptManager.RegisterStartupScript(Controle, typeof(UpdatePanel), "script", " window.alert('" + Mensagem + "'); ", true);
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

    /// <summary>
    /// Executa um script
    /// </summary>
    /// <returns></returns>
    public void ExecutarScript(UpdatePanel Controle,StringBuilder scriptJs)
    {
        ScriptManager.RegisterStartupScript(Controle, typeof(UpdatePanel), "script", scriptJs.ToString(), true);
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
    private FuncionarioVO _mUsuarioAtivo;
    public FuncionarioVO UsuarioAtivo
    {
        get
        {
            _mUsuarioAtivo = new FuncionarioVO();
            _mUsuarioAtivo = (FuncionarioVO) RecuperaValorSessao("UsuarioAtivo");

            if (_mUsuarioAtivo == null)
                Response.Redirect("~/login.aspx");

            return _mUsuarioAtivo;
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
    ///
    /// formata um valor sobre uma mascara
    ///
    /// valor a formatar
    /// no formato ex.:##/##/#### ou ##.###,##
    /// valor formatado
    public static string Formatar( string valor, string mascara )
    {
        StringBuilder dado = new StringBuilder();
        // remove caracteres nao numericos
        foreach ( char c in valor )
        {
            if ( Char.IsNumber(c) )
            dado.Append(c);
        }
        int indMascara = mascara.Length;
        int indCampo = dado.Length;
        for (; indCampo > 0 && indMascara > 0; )
        {
            if ( mascara[--indMascara] == '#' )
            indCampo--;
        }
        StringBuilder saida = new StringBuilder();
        for (; indMascara < mascara.Length; indMascara++)
        {
            saida.Append( ( mascara[indMascara] == '#' ) ? dado[indCampo++] : mascara[indMascara] );
        }
        return saida.ToString();
    }

    //Função para a formatação dos campos
    public static string Mascara(string Tipo,string campo)
    {
        switch (Tipo)
        {
            case "CNPJ":
                campo = campo.Replace("-", "").Replace("/", "").Replace(".", "");
                campo = Formatar(campo, "##.###.###/####-##");
                break;
            case "CPF":
                campo = campo.Replace("-", "").Replace("/", "").Replace(".", "");
                campo = Formatar(campo, "###.###.###-##");
                break;
            case "TEL":
                if (!string.IsNullOrEmpty(campo))
                    campo = campo.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                if (!string.IsNullOrEmpty(campo) && campo.Length == 10)
                {
                    campo = "(" + campo.Substring(0, 2) + ") " + campo.Substring(2, 4) + '-' + campo.Substring(6, 4);
                }
                break;
            case "CEP":
                campo = campo.Replace("-", "");
                campo = Formatar(campo, "#####-###");
                break;
            case "DATA":
                campo = campo.Replace("/", "");
                campo = Formatar(campo, "##/##/####");
                break;
        }
        return campo;
    }
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

    /*public string FormatarCnpj(string cnpj)
    {
        MaskedTextProvider mtpCnpj = new MaskedTextProvider(@"00\.000\.000/0000-00");

        mtpCnpj.Set(cnpj);

        return cnpj.ToString();

    }*/
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

    /// <summary>
    /// Obtêm a largura da imagem de assinatura digitalizada.
    /// </summary>
    public static int Largura_Img_Assinatura()
    {
        string sLargura;
        sLargura = ConfigurationManager.AppSettings["Nissi.Largura_Assinatura"];
        if (sLargura == "" || sLargura == null)
            return 300;
        else
            return Convert.ToInt32(sLargura);
    }

    /// <summary>
    /// Obtêm a altura da imagem de assinatura digitalizada.
    /// </summary>		
    public static int Altura_Img_Assinatura()
    {
        string sAltura;
        sAltura = ConfigurationManager.AppSettings["Nissi.Altura_Assinatura"];
        if (sAltura == "" || sAltura == null)
            return 60;
        else
            return Convert.ToInt32(sAltura);
    }
    #region Método que Carrega os Clientes
    [System.Web.Services.WebMethod]
    public static string[] GetNames(string prefixText)
    {
        ClienteVO identCliente = new ClienteVO();
        //Todo: Depois do tratamento na procedure, remover a linha abaixo
        identCliente.IndPessoaTipo = null;
        identCliente.RazaoSocial = prefixText;
        List<ClienteVO> lCliente = new Cliente().ListaClientePorNome(identCliente);

        List<string> items = new List<string>();
        foreach (ClienteVO item in lCliente)
        {
            items.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(
                item.RazaoSocial, item.CodPessoa.ToString()));
        }
        return items.ToArray();
    }
    #endregion
    #region Método que Carrega os Clientes pelo Nome Fantasia
    [System.Web.Services.WebMethod]
    public static string[] GetNamesFantasy(string prefixText)
    {
        ClienteVO identCliente = new ClienteVO();
        //Todo: Depois do tratamento na procedure, remover a linha abaixo
        identCliente.IndPessoaTipo = null;
        identCliente.NomeFantasia = prefixText;
        List<ClienteVO> lCliente = new Cliente().ListaClientePorNomeFantasia(identCliente);

        List<string> items = new List<string>();
        foreach (ClienteVO item in lCliente)
        {
            items.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(
                item.RazaoSocial, item.CodPessoa.ToString()));
        }
        return items.ToArray();
    }
    #endregion
    #region Método que Carrega os Fornecedor
    [System.Web.Services.WebMethod]
    public static string[] GetFornecedor(string prefixText)
    {
        FornecedorVO identFornecedor = new FornecedorVO();
        //Todo: Depois do tratamento na procedure, remover a linha abaixo
        identFornecedor.RazaoSocial = prefixText;
        List<FornecedorVO> lFornecedor = new Fornecedor().ListaFornecedorPorNome(identFornecedor);

        List<string> items = new List<string>();
        foreach (FornecedorVO item in lFornecedor)
        {
            items.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(
                item.RazaoSocial, item.CodPessoa.ToString()));
        }
        return items.ToArray();
    }
    #endregion
    #region Método que Carrega os Fornecedor pelo Nome Fantasia
    [System.Web.Services.WebMethod]
    public static string[] GetFornecedorFantasia(string prefixText)
    {
        FornecedorVO identForneced = new FornecedorVO();
        //Todo: Depois do tratamento na procedure, remover a linha abaixo
        identForneced.IndPessoaTipo = null;
        identForneced.NomeFantasia = prefixText;
        List<FornecedorVO> lfornecedor = new Fornecedor().ListaFornecedorNomeFantasia(identForneced);

        List<string> items = new List<string>();
        foreach (FornecedorVO item in lfornecedor)
        {
            items.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(
                item.RazaoSocial, item.CodPessoa.ToString()));
        }
        return items.ToArray();
    }
    #endregion
    #region Método que Carrega os Transportadora
    [System.Web.Services.WebMethod]
    public static string[] GetTransportadora(string prefixText)
    {
        TransportadoraVO identTransportadoraVo = new TransportadoraVO();
        //Todo: Depois do tratamento na procedure, remover a linha abaixo
        identTransportadoraVo.RazaoSocial = prefixText;
        List<TransportadoraVO> ltransportadora = new Transportadora().ListaTransportadoraPorNome(identTransportadoraVo);

        List<string> items = new List<string>();
        foreach (TransportadoraVO item in ltransportadora)
        {
            items.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(
                item.RazaoSocial, item.CodPessoa.ToString()));
        }
        return items.ToArray();
    }
    #endregion
    #region Método que Carrega os Transportadora pelo Nome Fantasia
    [System.Web.Services.WebMethod]
    public static string[] GetTransortadoraFantasia(string prefixText)
    {
        TransportadoraVO identTransportadoraVo = new TransportadoraVO();
        //Todo: Depois do tratamento na procedure, remover a linha abaixo
        identTransportadoraVo.NomeFantasia = prefixText;
        List<TransportadoraVO> ltransportadora = new Transportadora().ListaTransportadoraNomeFantasia(identTransportadoraVo);

        List<string> items = new List<string>();
        foreach (TransportadoraVO item in ltransportadora)
        {
            items.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(
                item.RazaoSocial, item.CodPessoa.ToString()));
        }
        return items.ToArray();
    }
    #endregion
    #region Método que Carrega os Produto
    [System.Web.Services.WebMethod]
    public static string[] GetProduto(string prefixText)
    {
        ProdutoVO identProdutoVo = new ProdutoVO();
        //Todo: Depois do tratamento na procedure, remover a linha abaixo
        identProdutoVo.Descricao = prefixText;
        List<ProdutoVO> lProduto = new Produto().ListaProdutoPorNome(identProdutoVo);

        List<string> items = new List<string>();
        foreach (ProdutoVO item in lProduto)
        {
            items.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(
                item.Descricao, item.CodProduto.ToString()));
        }
        return items.ToArray();
    }
    #endregion
    #region Método que Carrega os Produto Insumo
    [System.Web.Services.WebMethod]
    public static string[] GetProdutoInsumo(string prefixText)
    {
        //Todo: Depois do tratamento na procedure, remover a linha abaixo
        List<ProdutoInsumoVO> lProduto = ProdutoInsumo.ListarPorDescricao(prefixText);

        List<string> items = new List<string>();
        foreach (ProdutoInsumoVO item in lProduto)
        {
            items.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(
                item.Descricao, item.CodProdutoInsumo.ToString()));
        }
        return items.ToArray();
    }
    #endregion
    #region Método que Carrega os Funcionarios
    [System.Web.Services.WebMethod]
    public static string[] GetFuncionario(string prefixText)
    {
        FuncionarioVO identFuncionarioVo = new FuncionarioVO();
        //Todo: Depois do tratamento na procedure, remover a linha abaixo
        identFuncionarioVo.Nome = prefixText;
        List<FuncionarioVO> lFuncionario = new Funcionario().ListaFuncionarioPorNome(identFuncionarioVo);

        List<string> items = new List<string>();
        foreach (FuncionarioVO item in lFuncionario)
        {
            items.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(
                item.Nome, item.CodFuncionario.ToString()));
        }
        return items.ToArray();
    }
    #endregion
    #region Método que Carrega a Norma
    [System.Web.Services.WebMethod]
    public static string[] GetNorma(string prefixText)
    {
        MateriaPrimaVO identMateriaPrimaVo = new MateriaPrimaVO();
        //Todo: Depois do tratamento na procedure, remover a linha abaixo
        identMateriaPrimaVo.NormaVo.Descricao = prefixText;
        List<MateriaPrimaVO> lNorma = new Norma().ListarPorNorma(identMateriaPrimaVo.NormaVo.Descricao);

        List<string> items = new List<string>();
        foreach (MateriaPrimaVO item in lNorma)
        {
            string descricao = item.NormaVo.Descricao + "/" + item.NormaVo.Revisao;
            if (item.ClasseTipoVo != null)
                descricao += item.ClasseTipoVo.Descricao;
            items.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(
                descricao, item.CodMateriaPrima.ToString()));
        }
        return items.ToArray();
    }
    #endregion
    #region Método que Carrega a Classe/Tipo
    [System.Web.Services.WebMethod]
    public static string[] GetClasseTipo(string prefixText)
    {
        MateriaPrimaVO identMateriaPrimaVo = new MateriaPrimaVO();
        //Todo: Depois do tratamento na procedure, remover a linha abaixo
        identMateriaPrimaVo.ClasseTipoVo.Descricao = prefixText;
        List<ClasseTipoVO> lstClasseTipo = new ClasseTipo().ListarPorClasseTipo(identMateriaPrimaVo.ClasseTipoVo.Descricao);

        List<string> items = new List<string>();
        foreach (ClasseTipoVO item in lstClasseTipo)
        {
            items.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(
                item.Descricao, item.CodClasseTipo.ToString()));
        }
        return items.ToArray();
    }
    #endregion
    #region Método que Carrega a Bitola
    [System.Web.Services.WebMethod]
    public static string[] GetBitola(string prefixText)
    {
        BitolaVO identBitolaVo = new BitolaVO();
        //Todo: Depois do tratamento na procedure, remover a linha abaixo
        identBitolaVo.Bitola = decimal.Parse(prefixText);
        List<BitolaVO> lstBitolaVo = new Bitola().ListarPorBitola(identBitolaVo.Bitola);

        List<string> items = new List<string>();
        foreach (BitolaVO item in lstBitolaVo)
        {
            items.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(
                item.Bitola.ToString(), item.CodBitola.ToString()));
        }
        return items.ToArray();
    }

    #endregion
    #region Método que Carrega o Prazo de Entrega
    [System.Web.Services.WebMethod]
    public string[] GetPrazoEntrega(string prefixText)
    {
        PrazoEntregaVO identPrazoEntregaVo = new PrazoEntregaVO();
        //Todo: Depois do tratamento na procedure, remover a linha abaixo
        identPrazoEntregaVo.Descricao = prefixText;
        List<PrazoEntregaVO> lstPrazoEntregaVo = new PrazoEntrega().ListarPorDescricao(prefixText);

        List<string> items = new List<string>();
        foreach (PrazoEntregaVO item in lstPrazoEntregaVo)
        {
            items.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(
                item.Descricao, item.CodPrazoEntrega.ToString()));
        }
        return items.ToArray();
    }
    #endregion
    #region Método que Carrega o Condicoes de Pagamento
    [System.Web.Services.WebMethod]
    public string[] GetCondicoesPgto(string prefixText)
    {
        FormaPgtoVO identFormaPgtoVo = new FormaPgtoVO();
        //Todo: Depois do tratamento na procedure, remover a linha abaixo
        identFormaPgtoVo.Descricao = prefixText;
        List<FormaPgtoVO> lstFormaPgtoVo = new FormaPgto().ListarPorDescricao(prefixText);

        List<string> items = new List<string>();
        foreach (FormaPgtoVO item in lstFormaPgtoVo)
        {
            items.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(
                item.Descricao, item.CodFormaPgto.ToString()));
        }
        return items.ToArray();
    }
    #endregion
    #region Método que Carrega a Email
    [System.Web.Services.WebMethod]
    public static string[] GetEmail(string prefixText)
    {
        EmailEmitenteVO identEmailVo = new EmailEmitenteVO();
        //Todo: Depois do tratamento na procedure, remover a linha abaixo
        identEmailVo.Email = prefixText;
        List<EmailEmitenteVO> lstEmailVo = EmailEmitente.ListarPorEmailEmitente(identEmailVo.Email);

        List<string> items = new List<string>();
        foreach (EmailEmitenteVO item in lstEmailVo)
        {
            items.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(
                item.Email, item.CodEmailEmitente.ToString()));
        }
        return items.ToArray();
    }

    #endregion

}
