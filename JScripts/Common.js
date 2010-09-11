//--------------------------------------------------------------------------------
//Criado por...: Alexandre Maximiano - 20/07/2009
//Objetivo.....: Abre a aplicação em uma nova janela, não redimensionável e sem menus e barras.
//--------------------------------------------------------------------------------
function CommonAbrirTelaCheia(especificarLargura, especificarAltura, url) {
    if(!window.TelaCheia) {

        var intLargura;
        var intAltura;

        if (especificarLargura == null)
            intLargura = screen.availWidth;
        else
            intLargura = especificarLargura;

        if (especificarAltura == null)
            intAltura = screen.availHeight;
        else
            intAltura = especificarAltura;

        intLargura -= 11;
        intAltura -= 36;

        if (url == null)
            url = document.location.href;

        var wnd = window.open(url, "_blank", 
            "scrollbars=yes,toolbar=no,location=no,directories=no,status=no,menubar=no,resizable=no,copyhistory=no,width="+intLargura+",height="+intAltura+",top=0,left=0");

        if(navigator.appVersion.indexOf("MSIE 7.0")!= -1)
            window.open("","_self","");
        else
            window.opener = self;
            
   wnd.TelaCheia = true;
        window.close();        
     
    }
}

//Esta função transforma, no evento keyup, o valor digitado em UpperCase 
function ConverterCaixaAlta() 
{
	var caracter;
	var str;
	caracter = String.fromCharCode(window.event.keyCode);
	caracter = caracter.toUpperCase();	
	if (isNaN(caracter))
		window.event.keyCode = caracter.charCodeAt();
}

// Permite somente a digitacao de números
function OnlyNumbers() {
    if (event.keyCode != 8) {
        var caracter;
        caracter = String.fromCharCode(window.event.keyCode);

        if (isNaN(caracter) == false && caracter != ' ')
            window.event.keyCode = caracter.charCodeAt();
        else
            window.event.keyCode = '';
    }

}


function MontaComboTbx(objIn, objOut)
{

	if (objIn.value == "")
	{
		return;
	}
	
	objOut.value = objIn.value
	if (objOut.value == "")
	{
		objIn.focus();
		return;
	}
}

function HabilitaTextBox(valor) {
    $get('<%=txbPesquisa.ClientID%>').disabled = valor;
    if (valor == true) {
        $get('<%=txbPesquisa.ClientID%>').style.backgroundColor = '#F5F5F5';
    }
    else {
        $get('<%=txbPesquisa.ClientID%>').style.backgroundColor = 'white';
        $get('<%=txbPesquisa.ClientID%>').focus();
    }
}
//***************************************************
//Mascaras e validações
//***************************************************

//valida telefone
function ValidaTelefone(tel)
{
    exp = /\(\d{2}\)\ \d{4}\-\d{4}/
    if (!exp.test(tel.value))
        alert('Numero de Telefone Invalido!');
}

//valida CEP
function ValidaCep(cep)
{
    exp = /\d{2}\.\d{3}\-\d{3}/
    if (!exp.test(cep.value))
        alert('Numero de Cep Invalido!');
}

//Função Para Validar CPF
function Verifica_CPF(objNum) {
    var retorno = true;
    exp = /\.|\-|\//g
    var CPF = objNum.value.toString().replace(exp, ""); // Recebe o valor digitado no campo

// Aqui começa a checagem do CPF
    var POSICAO, I, SOMA, DV, DV_INFORMADO;
    var DIGITO = new Array(10);
        DV_INFORMADO = CPF.substr(9, 2); // Retira os dois últimos dígitos do número informado

// Desemembra o número do CPF na array DIGITO
    for (I=0; I<=8; I++) 
    {
        DIGITO[I] = CPF.substr( I, 1);
    }

// Calcula o valor do 10º dígito da verificação
    POSICAO = 10;
    SOMA = 0;
   for (I=0; I<=8; I++) 
    {
      SOMA = SOMA + DIGITO[I] * POSICAO;
      POSICAO = POSICAO - 1;
   }
    DIGITO[9] = SOMA % 11;
   if (DIGITO[9] < 2) 
    {
        DIGITO[9] = 0;
    }
   else
    {
       DIGITO[9] = 11 - DIGITO[9];
    }

    // Calcula o valor do 11º dígito da verificação
    POSICAO = 11;
    SOMA = 0;
   for (I=0; I<=9; I++) 
    {
      SOMA = SOMA + DIGITO[I] * POSICAO;
      POSICAO = POSICAO - 1;
    }
    DIGITO[10] = SOMA % 11;
   if (DIGITO[10] < 2) 
    {
        DIGITO[10] = 0;
    }
   else 
    {
        DIGITO[10] = 11 - DIGITO[10];
    }

    // Verifica se os valores dos dígitos verificadores conferem
    DV = DIGITO[9] * 10 + DIGITO[10];
   if (DV != DV_INFORMADO) 
    {
      /*objNum.value = '';
      objNum.focus();*/
      retorno = false;
    }

    return retorno;
}

//valida o CNPJ digitado
function ValidarCNPJ(ObjCnpj) {

    var cnpj = ObjCnpj.value;
    var valida = new Array(6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2);
    var dig1 = new Number;
    var dig2 = new Number;
    exp = /\.|\-|\//g
    cnpj = cnpj.toString().replace(exp, "");
    var digito = new Number(eval(cnpj.charAt(12) + cnpj.charAt(13)));

    for (i = 0; i < valida.length; i++) {
        dig1 += (i > 0 ? (cnpj.charAt(i - 1) * valida[i]) : 0);
        dig2 += cnpj.charAt(i) * valida[i];
    }
    dig1 = (((dig1 % 11) < 2) ? 0 : (11 - (dig1 % 11)));
    dig2 = (((dig2 % 11) < 2) ? 0 : (11 - (dig2 % 11)));

    if (((dig1 * 10) + dig2) != digito)
    // alert('CNPJ Invalido!');
        return false;
    else
    return true;
}

//Função para a formatação dos campos
function Mascara(tipo, campo, teclaPress) {

    if (window.event) {
        var tecla = teclaPress.keyCode;
    }
    else {
        tecla = teclaPress.which;
    }
    var s = new String(campo.value);

    // Remove todos os caracteres à seguir:  ( ) / - . e espaço, para tratar a string denovo.
    s = s.replace(/(\.|\(|\)|\/|\-| )+/g, '');
    tam = s.length + 1; if (tecla != 9 && tecla != 8) {
        switch (tipo) {
            case 'CPF':
                if (tam > 3 && tam < 7) campo.value = s.substr(0, 3) + '.' + s.substr(3, tam);
                if (tam >= 7 && tam < 10)
                    campo.value = s.substr(0, 3) + '.' + s.substr(3, 3) + '.' + s.substr(6, tam - 6);
                if (tam >= 10 && tam < 12)
                    campo.value = s.substr(0, 3) + '.' + s.substr(3, 3) + '.' + s.substr(6, 3) + '-' + s.substr(9, tam - 9);
                break;
                
            case 'CNPJ':
                if (tam > 2 && tam < 6)
                    campo.value = s.substr(0, 2) + '.' + s.substr(2, tam);
                if (tam >= 6 && tam < 9) campo.value = s.substr(0, 2) + '.' + s.substr(2, 3) + '.' + s.substr(5, tam - 5);
                if (tam >= 9 && tam < 13)
                    campo.value = s.substr(0, 2) + '.' + s.substr(2, 3) + '.' + s.substr(5, 3) + '/' + s.substr(8, tam - 8);
                if (tam >= 13 && tam < 15)
                    campo.value = s.substr(0, 2) + '.' + s.substr(2, 3) + '.' + s.substr(5, 3) + '/' + s.substr(8, 4) + '-' + s.substr(12, tam - 12);
                break;
                
            case 'TEL':
                if (tam > 2 && tam < 4) campo.value = '(' + s.substr(0, 2) + ') ' + s.substr(2, tam);
                if (tam >= 7 && tam < 11) campo.value = '(' + s.substr(0, 2) + ') ' + s.substr(2, 4) + '-' + s.substr(6, tam - 6);
                break;
                
            case 'DATA':
                if (tam > 2 && tam < 4)
                    campo.value = s.substr(0, 2) + '/' + s.substr(2, tam);
                if (tam > 4 && tam < 11) campo.value = s.substr(0, 2) + '/' + s.substr(2, 2) + '/' + s.substr(4, tam - 4);
                break;
                
            case 'CEP':
                if (tam > 5 && tam < 7) campo.value = s.substr(0, 5) + '-' + s.substr(5, tam);
                break;
        } 
    } 
}
 //--->Função para verificar se o valor digitado é número <---
function digitos(event) {
    if (window.event) {
        key = event.keyCode;
    }
    if (key != 8 || key != 13 || key < 48 || key > 57)
        return (((key > 47) && (key < 58)) || (key == 8) || (key == 13));
     return true;
 }
 
function ValidarCPFCNPJ(theCPF) 
{ 
  if (theCPF.value == "") 
  { 
    alert("Campo inválido."); 
    theCPF.focus(); 
    return (false); 
  } 
  if (((theCPF.value.length == 11) && (theCPF.value == 11111111111) || (theCPF.value == 22222222222) || (theCPF.value == 33333333333) || (theCPF.value == 44444444444) || (theCPF.value == 55555555555) || (theCPF.value == 66666666666) || (theCPF.value == 77777777777) || (theCPF.value == 88888888888) || (theCPF.value == 99999999999) || (theCPF.value == 00000000000))) 
  { 
    alert("Dados inválido."); 
    theCPF.focus(); 
    return (false); 
  } 


  if (!((theCPF.value.length == 11) || (theCPF.value.length == 14))) 
  { 
    alert("Dados inválido."); 
    theCPF.focus(); 
    return (false); 
  } 

  var checkOK = "0123456789"; 
  var checkStr = theCPF.value; 
  var allValid = true; 
  var allNum = ""; 
  for (i = 0;  i < checkStr.length;  i++) 
  { 
    ch = checkStr.charAt(i); 
    for (j = 0;  j < checkOK.length;  j++) 
      if (ch == checkOK.charAt(j)) 
        break; 
    if (j == checkOK.length) 
    { 
      allValid = false; 
      break; 
    } 
    allNum += ch; 
  } 
  if (!allValid) 
  { 
    alert("Favor preencher somente com dígitos o campo CPF/CNPJ."); 
    theCPF.focus(); 
    return (false); 
  } 

  var chkVal = allNum; 
  var prsVal = parseFloat(allNum); 
  if (chkVal != "" && !(prsVal > "0")) 
  { 
    alert("CPF zerado !"); 
    theCPF.focus(); 
    return (false); 
  } 

if (theCPF.value.length == 11) 
{ 
  var tot = 0; 

  for (i = 2;  i <= 10;  i++) 
    tot += i * parseInt(checkStr.charAt(10 - i)); 

  if ((tot * 10 % 11 % 10) != parseInt(checkStr.charAt(9))) 
  { 
    alert("Dados inválido."); 
    theCPF.focus(); 
    return (false); 
  } 
  
  tot = 0; 
  
  for (i = 2;  i <= 11;  i++) 
    tot += i * parseInt(checkStr.charAt(11 - i)); 

  if ((tot * 10 % 11 % 10) != parseInt(checkStr.charAt(10))) 
  { 
    alert("Dados inválido."); 
    theCPF.focus(); 
    return (false); 
  } 
} 
else 
{ 
  var tot  = 0; 
  var peso = 2; 
  
  for (i = 0;  i <= 11;  i++) 
  { 
    tot += peso * parseInt(checkStr.charAt(11 - i)); 
    peso++; 
    if (peso == 10) 
    { 
        peso = 2; 
    } 
  } 

  if ((tot * 10 % 11 % 10) != parseInt(checkStr.charAt(12))) 
  { 
    alert("Dados inválido."); 
    theCPF.focus(); 
    return (false); 
  } 
  
  tot  = 0; 
  peso = 2; 
  
  for (i = 0;  i <= 12;  i++) 
  { 
    tot += peso * parseInt(checkStr.charAt(12 - i)); 
    peso++; 
    if (peso == 10) 
    { 
        peso = 2; 
    } 
  } 

  if ((tot * 10 % 11 % 10) != parseInt(checkStr.charAt(13))) 
  { 
    alert("Dados inválido."); 
    theCPF.focus(); 
    return (false); 
  } 
} 
  return(true); 
} 
 

 function fnTrocaLista(objOrigem, objDestino, blnTodos) {
     var j = objOrigem.length;
     for (var i = 0; i < j; i++) {
         if (objOrigem.options[i].selected == true || blnTodos) {
             var vValor = objOrigem.options[i].value;
             var vTexto = objOrigem.options[i].text;
             if (!fnItemListaExiste(vValor, objDestino)) {
                 fnAdicionaElementoCombo(objDestino, vValor, vTexto);
                 objOrigem.remove(i);
                 j--;
                 i--;
             }
         }
     }
 }
 function fnItemListaExiste(strValue, objList) {
     var existe = false;
     for (var i = 0; i < objList.options.length; i++) {
         if (strValue == objList.options[i].value) {
             existe = true;
             break;
         }
     }
     return existe;
 }

 function fnAdicionaElementoCombo(objCombo, value, text) {
     var objOption = objCombo.parentElement.document.createElement('OPTION');

     //Retirar pontos que se referem aos níveis, quando necessário
     var strNovoTexto = text.toString();

     objOption.text = strNovoTexto.toString();
     objOption.value = value;
     objCombo.add(objOption);
 }

 //Testa o grupo de validacao e retorna se o grupo é valido
 //Quando usa o OnClientClick de um aspbutton o mesmo não efetua a validação, caso houver necessidade
 //de validar a tela usar essa função
 function Common_TestaValidacao(ValidationGroup) {
     var i;

     for (i = 0; i < Page_Validators.length; i++) {
         if (ValidationGroup == '' || Page_Validators[i].validationGroup == ValidationGroup) {
             Page_Validators[i].isvalid = Page_Validators[i].evaluationfunction(Page_Validators[i]);
             if (!Page_Validators[i].isvalid)
                 return false;
         }
     }

     return true;
 }

 /***  FUNÇÕES DE AUTOCOMPLETE ***/
 /* Deixa em negrito as palavras relacionadas a busca*/
function ClientPopulated(source, eventArgs) {
     if (source._currentPrefix != null) {
         var list = source.get_completionList();
         var search = source._currentPrefix.toLowerCase();

         for (var i = 0; i < list.childNodes.length; i++) {
             var text = list.childNodes[i].innerHTML;
             var index = text.toLowerCase().indexOf(search.toLowerCase());

             if (index != -1) {
                 var value = text.substring(0, index);
                 value += '<span class="AutoComplete_ListItemHighlightText">';
                 value += text.substr(index, search.length);
                 value += '</span>';
                 value += text.substring(index + search.length);

                 list.childNodes[i].innerHTML = value;
             }
         }
     }
 }
