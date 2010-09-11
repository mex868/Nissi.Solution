using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Nissi.Business
{
    /// <summary>
    /// Gera C�digos e Nomes Foneticos baseados em strings recebidas
    /// </summary>
    public class Fonetica
    {
        #region "Construtores"

        /// <summary>
        /// Construtor padr�o da Classe Fon�tica.
        /// </summary>
        public Fonetica()
        {
        }
        #endregion


        #region "M�todos P�blicos"
        public string NomeFonetico(string strValor)
        {
            return RetornaNomeFonetico(strValor.Trim());
        }

        public string Gerar(string strValor)
        {
            return RetornaCodigoFonetico(strValor.Trim());
        }

        #endregion

        #region "M�todos Privados"
        /// <summary>
        /// Retorna o Nome Fon�tica da string recebida em strNome
        /// Antes de ser gerado o c�digo fon�tico, esse m�todo se encarrega de gerar um nome fon�tico
        /// criando palavras semelhantes apenas trocando ou eliminando letras
        /// Ex: Thereza = Teresa
        /// </summary>
        private string RetornaNomeFonetico(string strNome)
        {
            StringBuilder stbNomeFonetico = new StringBuilder("", 100); //ao final representar� o nome em sua forma fon�tica
            StringBuilder stbNomeTemp = new StringBuilder(strNome.ToUpper(), 100); //usado para manipula��o do nome fon�tico em cada looping

            //string strEspeciais = @"""!#$%-*+'_/:;<=>?@[\],^_`{|}~�()���.";

            //Substitui caracteres especiais por espa�os 
            for (int i = 0; i < stbNomeTemp.Length; i++)
            {
                // char chr = stbNomeTemp[i];
                // UnicodeEncoding.				

                int c = (int)stbNomeTemp[i];

                if ((c <= 47) || (c >= 58 && c <= 64)
                    || (c >= 91 && c <= 96) || (c >= 123 && c <= 191)
                    || c == 197 || c == 198 || c == 208 || c == 215
                    || c == 216 || c == 221 || c == 222 || c == 223
                    || c == 229 || c == 230 || c == 240 || c == 247
                    || c == 248 || c == 253 || c == 254 || c > 255)
                    stbNomeFonetico.Append(' ');
                else
                    stbNomeFonetico.Append(stbNomeTemp[i]);

                //		if ((byte) c <= 47 || strEspeciais.IndexOf(c) != -1)
                //			stbNomeFonetico.Append(" ");
                //		else
                //			stbNomeFonetico.Append(c);
            }

            //Caso nao sobre nada para fonetizar ser� calculado sobre 'A'
            if (stbNomeFonetico.ToString().Trim().Length == 0)
                stbNomeFonetico.Append('A');

            //Retira espa�os duplicados
            stbNomeTemp.Remove(0, stbNomeTemp.Length);
            stbNomeTemp.Append(stbNomeFonetico.ToString().Trim());
            stbNomeFonetico.Remove(0, stbNomeFonetico.Length);
            stbNomeFonetico.Append(stbNomeTemp[0]);
            for (int i = 1; i < stbNomeTemp.Length; i++)
            {
                if (!(stbNomeTemp[i] == ' ' && stbNomeTemp[i - 1] == ' '))
                {
                    stbNomeFonetico.Append(stbNomeTemp[i]);
                }
            }

            //Retira acentua��o do nome completo e troca Y por I.
            stbNomeTemp.Remove(0, stbNomeTemp.Length);
            stbNomeTemp.Append(stbNomeFonetico.ToString());
            stbNomeFonetico.Remove(0, stbNomeFonetico.Length);

            stbNomeTemp.Replace('�', 'A');
            stbNomeTemp.Replace('�', 'A');
            stbNomeTemp.Replace('�', 'A');
            stbNomeTemp.Replace('�', 'A');
            stbNomeTemp.Replace('�', 'A');

            stbNomeTemp.Replace('�', 'E');
            stbNomeTemp.Replace('�', 'E');
            stbNomeTemp.Replace('�', 'E');
            stbNomeTemp.Replace('�', 'E');
            stbNomeTemp.Replace('&', 'E');

            stbNomeTemp.Replace('�', 'I');
            stbNomeTemp.Replace('�', 'I');
            stbNomeTemp.Replace('�', 'I');
            stbNomeTemp.Replace('�', 'I');
            stbNomeTemp.Replace('Y', 'I');
            stbNomeTemp.Replace('�', 'I');

            stbNomeTemp.Replace('�', 'O');
            stbNomeTemp.Replace('�', 'O');
            stbNomeTemp.Replace('�', 'O');
            stbNomeTemp.Replace('�', 'O');
            stbNomeTemp.Replace('�', 'O');

            stbNomeTemp.Replace('�', 'U');
            stbNomeTemp.Replace('�', 'U');
            stbNomeTemp.Replace('�', 'U');
            stbNomeTemp.Replace('�', 'U');

            stbNomeTemp.Replace('�', 'C');
            stbNomeTemp.Replace('�', 'N');

            stbNomeTemp.Append(' ');

            /*
                        for (int i = 0; i < stbNomeTemp.Length; i++)
                        {
                            switch(stbNomeTemp[i])
                            {
                                case '�':
                                case '�':
                                case '�':
                                case '�':
                                case '�':
                                    stbNomeFonetico.Append('A');
                                    break;
                                case '�':
                                case '�':
                                case '�':
                                case '�':
                                    stbNomeFonetico.Append('E');
                                    break;
                                case '�':
                                case '�':
                                case '�':
                                case '�':
                                case 'Y':
                                    stbNomeFonetico.Append('I');
                                    break;
                                case '�':
                                case '�':
                                case '�':
                                case '�':
                                case '�':
                                    stbNomeFonetico.Append('O');
                                    break;
                                case '�':
                                case '�':
                                case '�':
                                case '�':
                                    stbNomeFonetico.Append('U');
                                    break;
                                case '�':
                                    stbNomeFonetico.Append('C');
                                    break;
                                case '�':
                                    stbNomeFonetico.Append('N');
                                    break;
                                default:
                                    stbNomeFonetico.Append(stbNomeTemp[i]);
                                    break;
                            }
                        }

                        //Troca "&" por "E"
                        stbNomeTemp.Remove(0,stbNomeTemp.Length);
                        stbNomeTemp.Append(stbNomeFonetico.ToString());
                        stbNomeFonetico.Remove(0,stbNomeFonetico.Length);
                        for (int i = 0; i < stbNomeTemp.Length; i++)
                        {
                            if (stbNomeTemp[i].CompareTo('&') == 0)
                                stbNomeFonetico.Append('E');
                            else
                                stbNomeFonetico.Append(stbNomeTemp[i]);
                        }

                        //Monta o array com o nome completo quebrado em itens do array para cada parte do nome
                        stbNomeTemp.Remove(0,stbNomeTemp.Length);
                        stbNomeTemp.Append(stbNomeFonetico.ToString() + " ");
                        stbNomeFonetico.Remove(0,stbNomeFonetico.Length);
            */

            //o nome fon�tico dividido em partes ex: LUIZ AVILA = "LUIZ", "AVILA"
            ArrayList alNomeCompleto = new ArrayList();

            //Armazena as partes do nome no array;
            for (int i = 0; i < stbNomeTemp.Length; i++)
            {
                if (!stbNomeTemp[i].CompareTo(' ').Equals(0))
                    stbNomeFonetico.Append(stbNomeTemp[i]);
                else
                {
                    alNomeCompleto.Add(stbNomeFonetico.ToString());
                    stbNomeFonetico.Remove(0, stbNomeFonetico.Length);
                }
            }

            //Array de letras que poder�o ser extra�das do nome fon�tico de acordo com a combina��o
            string[] arrEspecial = new string[6] { "A", "C", "D", "L", "S", "T" };
            //Array de preposi��es que deve ser extra�das do nome fon�tico
            string[] arrRemocao = new string[17] { "DA", "DAS", "DE", "DO", "DOS", "E", "ESPOLIO", "INVENTARIANTE", "INVENTARIO", "LIMITADA", "LT", "LTD", "LTDA", "OUTRA", "OUTRAS", "OUTRO", "OUTROS" };
            //Array com as poss�veis combina��es de letras que devem ser extra�das do nome fon�tico.
            string[] arrAuxiliar = new string[] { "LT", "LTD", "LTDA", "SA", "SC" };
            int intIndex = 0; //Usado para controlar o ind�ce do array
            int intInicio = 0; //Usado para marcar o in�cio do range
            int intFim = 0; //Usado para marcar o fim do range
            object objElement; //Objeto a ser analisado

            //Usado para manipular as poss�veis combina��es de letras
            StringBuilder stbAuxiliar = new StringBuilder();

            alNomeCompleto.Add("FIMDOARRAY");

            IEnumerator ienumNomeCompleto = alNomeCompleto.GetEnumerator();

            ArrayList alNomeTratado = (ArrayList)alNomeCompleto.Clone();

            while (ienumNomeCompleto.MoveNext())
            {
                intIndex += 1;

                objElement = ienumNomeCompleto.Current;

                if ((Array.BinarySearch(arrRemocao, objElement) > -1))
                {
                    alNomeTratado.Remove(objElement);
                    intIndex -= 1;
                    stbAuxiliar.Remove(0, stbAuxiliar.Length);
                }
                else
                {
                    if ((Array.BinarySearch(arrEspecial, objElement) < 0))
                        stbAuxiliar.Remove(0, stbAuxiliar.Length);
                    else
                    {
                        stbAuxiliar.Append(objElement);

                        if ((Array.BinarySearch(arrAuxiliar, stbAuxiliar.ToString()) > -1))
                            intFim = intIndex - 1;
                        else
                        {
                            if (intFim > 0)
                                stbAuxiliar.Remove(stbAuxiliar.Length - 1, 1);
                            else
                            {
                                if (stbAuxiliar.Length > 1)
                                    stbAuxiliar.Remove(0, stbAuxiliar.Length - 1);

                                intInicio = intIndex - 1;
                            }
                        }
                    }
                }

                if ((stbAuxiliar.Length.Equals(0) && intFim > 0))
                {
                    alNomeTratado.RemoveRange(intInicio, (intFim - intInicio) + 1);
                    intIndex = intIndex - ((intFim - intInicio) + 1);
                    intInicio = 0;
                    intFim = 0;
                }

                if (objElement.Equals("FIMDOARRAY"))
                {
                    stbAuxiliar.Remove(0, stbAuxiliar.Length);
                    alNomeTratado.Remove(objElement);

                    if (alNomeTratado.Count == 0)
                    {
                        alNomeTratado = (ArrayList)alNomeCompleto.Clone();
                        alNomeTratado.Remove(objElement);
                    }
                }
            }

            //Efetua o algor�timo fon�tico para cada nome do nome completo
            stbNomeFonetico.Remove(0, stbNomeFonetico.Length);
            for (int i = 0; i < alNomeTratado.Count; i++)
            {
                stbNomeTemp.Remove(0, stbNomeTemp.Length);
                stbNomeTemp.Append(alNomeTratado[i].ToString());

                //Troca �O por AM no final do nome
                if (stbNomeTemp.ToString().EndsWith("AO"))
                    stbNomeTemp.Replace('O', 'M', stbNomeTemp.Length - 1, 1);

                //Troca BAPTI para BATI no in�cio do nome
                stbNomeTemp.Replace("BAPTI", "BATI");

                // if (stbNomeTemp.ToString().StartsWith("BAPTI"))
                //	stbNomeTemp.Remove(2,1);

                //Troca OPTI para OTI
                stbNomeTemp.Replace("OPTI", "OTI");

                //Palavras terminadas em RD, remover a letra D.
                if ((stbNomeTemp.Length > 3) && (stbNomeTemp.ToString().EndsWith("RD")))
                    stbNomeTemp.Remove(stbNomeTemp.Length - 1, 1);

                //Substitui GEO, JEO, GIO, JIO, GYO e JYO por JO
                stbNomeTemp.Replace("GEO", "JO");
                stbNomeTemp.Replace("JEO", "JO");
                stbNomeTemp.Replace("GIO", "JO");
                stbNomeTemp.Replace("JIO", "JO");
                stbNomeTemp.Replace("GYO", "JO");
                stbNomeTemp.Replace("JYO", "JO");

                //Considera "EIA" = "EA" (Exemplo: Andreia = Andrea)
                stbNomeTemp.Replace("EIA", "EA");

                //Aplica as regras fon�ticas do H. Quando n�o possui regra, elimina o H

                for (int z = 0; z < stbNomeTemp.Length; z++)
                {
                    bool blnRemoveH = true;
                    if ((stbNomeTemp[z] == 'H') && (stbNomeTemp.Length > 1))
                    {
                        if (z > 0)
                        {
                            switch (stbNomeTemp[z - 1])
                            {
                                case 'C':
                                    {
                                        if (z == stbNomeTemp.Length - 1)
                                        {
                                            stbNomeTemp.Replace('H', 'K', z, 1);
                                            stbNomeTemp.Append('E');
                                            blnRemoveH = false;
                                        }
                                        else
                                        {
                                            if ("AEIOUY".IndexOf(stbNomeTemp[z + 1]) > -1)
                                                stbNomeTemp.Replace('C', 'X', z - 1, 1);
                                        }
                                        break;
                                    }
                                case 'S':
                                    {
                                        stbNomeTemp.Replace('S', 'X', z - 1, 1);
                                        break;
                                    }
                                case 'L':
                                    {
                                        stbNomeTemp.Replace('H', 'I', z, 1);
                                        blnRemoveH = false;
                                        break;
                                    }
                                case 'P':
                                    {
                                        stbNomeTemp.Replace('P', 'F', z - 1, 1);
                                        break;
                                    }
                            }
                        }

                        if (blnRemoveH) stbNomeTemp.Remove(z, 1);
                    }
                }

                //Palavras terminadas em consoantes
                string strUltimaLetra = stbNomeTemp[stbNomeTemp.Length - 1].ToString();
                if ("BCDFGJKPQTV".IndexOf(strUltimaLetra) > -1)
                    stbNomeTemp.Insert(stbNomeTemp.Length, 'E');

                //Regra fon�tica para S no in�cio do nome
                //Caso a pr�xima letra seja uma consoante, acrescente a letra E antes do S. Ex.: Stenio = Estenio
                if ((stbNomeTemp[0] == 'S') && (stbNomeTemp.Length > 2))
                    if ("AEIOUY".IndexOf(stbNomeTemp[1].ToString(), 0, 6) == -1)
                        stbNomeTemp.Insert(0, 'I');

                //Elimina letras duplicadas
                char chrLetra;
                for (int z = 0; z < stbNomeTemp.Length - 1; z++)
                {
                    chrLetra = stbNomeTemp[z];
                    if (chrLetra == stbNomeTemp[z + 1])
                    {
                        stbNomeTemp.Remove(z, 1);
                        z -= 1;
                    }
                }

                //Aplica as regras foneticas para C, G, L, P, Q, S, N, W e Z
                for (int z = 0; z < stbNomeTemp.Length; z++)
                {
                    switch (stbNomeTemp[z])
                    {
                        /*****Regra fon�tica para C*****
                         * Para a letra C existem tr�s regras definidas. 
                         * 1 - Quando a pr�xima letra for E, I ou Y o C tem som de S - Ex.: Celio = Selio , Cicero = Sisero
                         * 2 - Quando a proxima letra for K ou T o som do C � despresado. Ex.: Packal = Pakal, Pictolomeu = Pitolomeu)
                         * 3 - Quando a pr�xima letra for S o som do C tem semelhan�a ao X. Ex.: Alecsandro = Alexsandro
                         * */

                        case 'C':
                            if (z < stbNomeTemp.Length - 1)
                            {
                                if ("EIY".IndexOf(stbNomeTemp[z + 1]) > -1)
                                    stbNomeTemp.Replace('C', 'S', z, 1);
                                else if ("TK".IndexOf(stbNomeTemp[z + 1]) > -1)
                                    stbNomeTemp.Remove(z, 1);
                                else if (stbNomeTemp[z + 1].ToString() == "S")
                                    stbNomeTemp.Replace('C', 'X', z, 1);
                            }
                            continue;

                        //Regra fon�tica para G
                        case 'G':
                            if (z < stbNomeTemp.Length - 1)
                            {
                                if ("EIY".IndexOf(stbNomeTemp[z + 1]) > -1)
                                {
                                    stbNomeTemp.Replace('G', 'J', z, 1);
                                }
                                else if (stbNomeTemp[z + 1] == 'U')
                                {
                                    if (stbNomeTemp.Length > z + 2)
                                    {
                                        if ("AEIOUY".IndexOf(stbNomeTemp[z + 2]) > -1)
                                        {
                                            stbNomeTemp.Remove(z + 1, 1);
                                        }
                                    }
                                    else
                                    {
                                        stbNomeTemp.Remove(z + 1, 1);
                                    }
                                }
                                else if (stbNomeTemp[z + 1] == 'N')
                                {
                                    stbNomeTemp.Remove(z, 1); //Exemplo: Ignacio = Inacio
                                }
                            }
                            continue;

                        //*****Regra fon�tica para L*****
                        case 'L':
                            /*O L como primeira letra da palavra n�o demanda substitui��o fon�tica
                            Apenas para os casos em que o L est� no meio ou no final da palavra
                            ele poder� exercer um outro valor fon�tico como 'U' */
                            if (z > 0)
                            {
                                if (z == stbNomeTemp.Length - 1) //L � a �ltima letra
                                {
                                    if ("AEIOUY".IndexOf(stbNomeTemp[z - 1]) > -1) //Letra anterior � uma vogal
                                        stbNomeTemp.Replace('L', 'U', z, 1);
                                }
                                else //L est� no meio da palavra
                                {
                                    if ("AEIOUY".IndexOf(stbNomeTemp[z - 1]) > -1) //Letra anterior � uma vogal
                                        if ("AEIOUY".IndexOf(stbNomeTemp[z + 1]) == -1) //Letra posterios � uma consoante
                                            stbNomeTemp.Replace('L', 'U', z, 1);
                                }
                            }
                            continue;

                        //Regra fon�tica para P
                        case 'P':
                            if (z < stbNomeTemp.Length - 1)
                            {
                                if (stbNomeTemp[z + 1] == 'C')
                                    stbNomeTemp.Replace('C', 'I', z + 1, 1);
                            }
                            continue;

                        /*****Regra fon�tica para Q******
                         * Para a letra Q ser� considerado apenas quando a pr�xima letra for U, eliminando-a.
                         * Todas as letras Q's tem o mesmo valor fon�tico de C e K
                         * */
                        case 'Q':
                            if (z < stbNomeTemp.Length - 1)
                                if (stbNomeTemp[z + 1] == 'U')
                                    if (stbNomeTemp.Length > z + 2)
                                        if ("EIY".IndexOf(stbNomeTemp[z + 2]) > -1)
                                        {
                                            stbNomeTemp.Remove(z + 1, 1);
                                            z -= 1;
                                        }

                            continue;

                        /********Regra fon�tica para S*****
                        No Caso da letra S, ser�o considerados os casos SC e SW.
                        No primeiro o C ser� eliminado, figurando apenas o som do S. Ex. Escenios = Esenios
                        No segundo caso o W passa a ter som de U. Ex.: Swing = Suing
                        Para o c�lculo fon�tico, o C e o K possuem o mesmo valor*/
                        case 'S':
                            if (z < stbNomeTemp.Length - 1)
                            {
                                if (stbNomeTemp[z + 1] == 'C')
                                {
                                    if (stbNomeTemp.Length > z + 2)
                                    {
                                        if ("EI".IndexOf(stbNomeTemp[z + 2]) > -1)
                                        {
                                            stbNomeTemp.Remove(z + 1, 1);
                                            z -= 1;
                                        }
                                    }
                                }
                            }
                            else if ((stbNomeTemp.Length > 3) && (z == stbNomeTemp.Length - 1))
                                stbNomeTemp.Remove(z, 1);

                            continue;

                        /*****Regra fon�tica para N*****
                         * Para a letra N, o valor fon�tico ser� de M, a menos que a pr�xima letra seja uma vogal
                         * */
                        case 'N':
                            if (z < stbNomeTemp.Length - 1)
                            {
                                if ("AEIOUY".IndexOf(stbNomeTemp[z + 1]) == -1)
                                    stbNomeTemp.Replace('N', 'M', z, 1);
                            }
                            else
                                stbNomeTemp.Replace('N', 'M', z, 1);

                            continue;

                        /*****Regra fon�tica para W*****
                         *A letra W possui som de V quando existe uma vogal em seguida e som de U nos outros casos
                         **/
                        case 'W':
                            if (z < stbNomeTemp.Length - 1)
                            {
                                if ("AEIOUY".IndexOf(stbNomeTemp[z + 1]) == -1)
                                    stbNomeTemp.Replace('W', 'U', z, 1);
                            }
                            continue;

                        case 'Z':
                            if ((stbNomeTemp.Length > 3) && (z == stbNomeTemp.Length - 1))
                                stbNomeTemp.Remove(z, 1);

                            continue;

                        default:
                            continue;
                    }
                }

                /*****Regras de fon�tica para encontro de consoantes*****
                 * Para o encontro de consoantes, ser� considerado o som da consoante, acrescentando a letra
                 * I no meio. Ex.: Pta = Pita. Bta = Bita. Como a letra E e I tem o mesmo valor fon�tico, 
                 * consoantes com som de E no final tamb�m ter�o o mesmo valor
                 * */
                for (int z = 0; z < stbNomeTemp.Length - 1; z++)
                {
                    if ("0123456789".IndexOf(stbNomeTemp[z]) == -1)
                        if ("AEIOUYRLMNSX0".IndexOf(stbNomeTemp[z]) == -1)
                            if ("LRAEIOUY".IndexOf(stbNomeTemp[z + 1]) == -1)
                                stbNomeTemp.Insert(z + 1, 'I');
                }

                //Considera "OU" = "U" (Exemplo: Lourdes = Lurdes)
                stbNomeTemp.Replace("OU", "U");

                //Monta o nome fonetico final
                if (i == 0)
                    stbNomeFonetico.Append(stbNomeTemp.ToString());
                else
                    stbNomeFonetico.Append(" " + stbNomeTemp.ToString());
            }

            return stbNomeFonetico.ToString();

        }

        /// <summary>
        /* Para cada letra existe um valor fon�tico. Ap�s a convers�o do nome para nome fon�tico
         * esse m�todo se encarrega de gerar o c�digo fon�tico, substituindo sua string fon�tica
         * por um c�digo fon�tico*/
        /// </summary>
        private string RetornaCodigoFonetico(string strNome)
        {
            string[] arrFonemas = new string[26] {/*a*/"0", /*b*/"1", /*c*/"2", /*d*/"3", /*e*/"4", /*f*/"5", 
													 /*g*/"D", /*h*/"0", /*i*/"4", /*j*/"6", /*k*/"2", /*l*/"7",
													 /*m*/"8", /*n*/"9", /*o*/"A", /*p*/"1", /*q*/"2", /*r*/"B",
													 /*s*/"C", /*t*/"3", /*u*/"A", /*v*/"5", /*w*/"5", /*x*/"C",
													 /*y*/"4", /*z*/"C"};

            string strNomeFonetico = RetornaNomeFonetico(strNome);

            StringBuilder stbCodigoFonetico = new StringBuilder("", 100);

            ASCIIEncoding ascii = new ASCIIEncoding();
            byte[] bytCodASC = ascii.GetBytes(strNomeFonetico);

            for (int i = 0; i < strNomeFonetico.Length; i++)
            {
                if (strNomeFonetico[i] == ' ')
                    stbCodigoFonetico.Append(' ');
                else if ((bytCodASC[i] > 64) && (bytCodASC[i] < 91))
                    stbCodigoFonetico.Append(arrFonemas[bytCodASC[i] - 65]);
                else
                    stbCodigoFonetico.Append(strNomeFonetico[i]);
            }

            string[] arrCodigosFoneticos = stbCodigoFonetico.ToString().Split(" ".ToCharArray());
            string strCodigoFonetico = "";
            string strAuxiliar = stbCodigoFonetico.ToString();
            int intContador;

            for (intContador = 0; intContador <= arrCodigosFoneticos.Length - 1; intContador++)
            {
                strCodigoFonetico += arrCodigosFoneticos[intContador] + " ";
            }
            return strCodigoFonetico.Trim();
        }

        #endregion

    }
}
