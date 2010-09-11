using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Text;
using System.Web;
using Nissi.Model;
using Nissi.DataAccess;

namespace Nissi.Business
{
    public class Transportadora : NissiBaseBusiness
    {
        #region Métodos de Listagem
        /// <summary>
        /// Método para executar a proc pr_selecionar_transportadora - Parametros:
        /// Objeto: identTransportadora,
        /// Valores: identTransportadora.CodPessoa,
        /// identTransportadora.CNPJ,
        /// identTransportadora.RazaoSocial,
        /// identTransportadora.CodTransportadora,
        /// identTransportadora.NomeFantasia.
        /// Se for passado null no valores ele lista todos os dados
        /// </summary>
        public List<TransportadoraVO> Listar(TransportadoraVO identTransportadora)
        {
            return new TransportadoraData().Listar(identTransportadora);
        }
        /// <summary>
        /// Método para executar a proc pr_selecionar_transportadora
        /// </summary>
        public List<TransportadoraVO> Listar()
        {
            return new TransportadoraData().Listar();
        }
        /// <summary>
        /// Listagem das localidades cadastradas exceto as localidades por sala 
        /// </summary>
        /// <param name="listaLocalidadesPorSalas"></param>
        /// <returns></returns>
        public List<TransportadoraVO> ListarExcetoTransportadoraCliente(List<TransportadoraVO> listaTransportadoraPorCliente)
        {
            //Listagem de todas as localidades do sistema, uso de sessão para evitar várias chamadas ao banco
            List<TransportadoraVO> listaTransportadora;
            if (HttpContext.Current.Session["lTransportadora"] != null)
                listaTransportadora = (List<TransportadoraVO>)HttpContext.Current.Session["lTransportadora"];
            else
                listaTransportadora = new TransportadoraData().Listar();

            //Nova Lista
            List<TransportadoraVO> novaLista = new List<TransportadoraVO>();

            bool encontrou = false;

            //Looping em todos as funcionalidades do sistema
            foreach (TransportadoraVO iTransportadora in listaTransportadora)
            {
                //Looping nas funcionalidades do perfil
                foreach (TransportadoraVO jTransportadora in listaTransportadoraPorCliente)
                {
                    //Caso encontre a localidade da sala na lista de todas as localidades
                    if (iTransportadora.CodTransportadora == jTransportadora.CodTransportadora)
                    {
                        encontrou = true;
                        break;
                    }
                }
                //Caso a localidade da sala não esteja na lista de todas as localidades então inclui na nova lista
                if (!encontrou)
                    novaLista.Add(iTransportadora);
                else
                    encontrou = false;
            }

            return novaLista;
        }


        /// <summary>
        /// Geração de string XML com lista de Transportadoras associadas
        /// </summary>
        /// <param name="arrListaFuncionalidade"></param>
        /// <returns></returns>
        public string GerarXmlListaTransportadora(string[] arrListaTransportadora)
        {
            StringBuilder xmlLista = new StringBuilder();
            xmlLista.Append("<root>");

            for (int i = 0; i < arrListaTransportadora.Length - 1; i++)
            {
                xmlLista.Append("<Transportadora>");
                xmlLista.Append("<codtransportadora>");
                xmlLista.Append(arrListaTransportadora[i].ToString());
                xmlLista.Append("</codtransportadora>");
                xmlLista.Append("</Transportadora>");
            }

            xmlLista.Append("</root>");
            return xmlLista.ToString();
        }


        // ------------------------------------------------------------------------- // 
        #endregion
        #region Métodos de Inclusão
        /// <summary>
        /// Método para executar a proc pr_incluir_transportadora 
        /// Objeto/Parametros: (identTransportadora,CodUsuarioOperacao)
        /// Valores: identTransportadora.Cep.CodCep,
        /// identTransportadora.RazaoSocial,
        /// identTransportadora.NomeFantasia,
        /// identTransportadora.Tipo,
        /// identTransportadora.CNPJ,
        /// identTransportadora.InscricaoEstadual,
        /// identTransportadora.Numero,
        /// identTransportadora.Complemento,
        /// identTransportadora.Telefone,
        ///identTransportadora.Fax,
        ///identTransportadora.Celular,
        ///identTransportadora.Contato,
        ///identTransportadora.Email,
        ///identTransportadora.Site,
        ///identTransportadora.Observacao,
        ///identTransportadora.Custo,
        ///codUsuarioOperacao,
        ///identTransportadora.Ativo,
        /// identTransportadora.IndPessoaTipo
        /// </summary>
        public int Incluir(TransportadoraVO identTransportadora, int codUsuarioOperacao)
        {
          return  new TransportadoraData().Incluir(identTransportadora, codUsuarioOperacao);
        }


        // ------------------------------------------------------------------------- // 
        #endregion
        #region Métodos de Alteração
        /// <summary>
        /// Método para executar a proc pr_alterar_transportadora 
        ///Objeto/Parametros: (identTransportadora,CodUsuarioOperacao)
        /// Valores:identTransportadora.CodTransportadora,
        /// identTransportadora.CodPessoa,
        /// identTransportadora.Cep.CodCep,
        /// identTransportadora.RazaoSocial,
        /// identTransportadora.NomeFantasia,
        /// identTransportadora.Tipo,
        /// identTransportadora.CNPJ,
        /// identTransportadora.InscricaoEstadual,
        /// identTransportadora.Numero,
        /// identTransportadora.Complemento,
        /// identTransportadora.Telefone,
        /// identTransportadora.Fax,
        /// identTransportadora.Celular,
        /// identTransportadora.Contato,
        /// identTransportadora.Email,
        /// identTransportadora.Site,
        /// identTransportadora.Observacao,
        /// identTransportadora.Custo,
        /// codUsuarioOperacao,
        /// identTransportadora.Ativo,
        /// identTransportadora.IndPessoaTipo
        /// </summary>
        public void Alterar(TransportadoraVO identTransportadora, int codUsuarioOperacao)
        {
            new TransportadoraData().Alterar(identTransportadora, codUsuarioOperacao);
        }
        #endregion
        #region Métodos de Exclusão
        // ------------------------------------------------------------------------- // 

        /// <summary>
        /// Método para executar a proc pr_excluir_transportadora
        /// Objeto/Parametros: (codPessoa)
        /// </summary>
        public void Excluir(int? codPessoa)
        {
            new TransportadoraData().Excluir(codPessoa);
        }


        // ------------------------------------------------------------------------- // 


        #endregion
    }
}
