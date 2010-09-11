using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.Model;
using Nissi.DataAccess;

namespace Nissi.Business
{
    public class CEP: NissiBaseBusiness
    {
        /// <summary>
        /// Listagem de CEP
        /// Objeto/Parametros: (identCEP)
        /// Valores: identCEP.CodCep(Obrigatório)
        /// </summary>
        /// <param name="identCEP"></param>
        /// <returns></returns>
        public CEPVO Listar(CEPVO identCEP)
        {
            return new CEPData().Lista(identCEP);
        }

        /// <summary>
        /// Pesquisa Fonética de Logradouro
        /// Objeto/Parametros: (identCEP)
        /// Valores: identCEP.Cidade.CodCidade(Obrigatório),
        /// identCEP.NomEndereco(Obrigatório)
        /// </summary>
        /// <param name="identCEP"></param>
        /// <returns></returns>
        public List<CEPVO> ListarPorLogradouro(CEPVO identCEP)
        {
            return new CEPData().ListaPorLogradouro(identCEP);
        }
        /// <summary>
        /// Inclui Cep
        /// Valores:identCEP.CodCep, identCEP.Cidade.CodCidade,
        /// identCEP.NomEndereco),identCEP.NomEndereco,identCEP.NomTipoLogradouro
        /// identCEP.UF.CodUF 
        /// </summary>
        /// <param name="identCep"></param>
        public void Incluir(CEPVO identCep)
        {
            new CEPData().Incluir(identCep);
        }
                /// <summary>
        /// Método para executar a proc pr_alterar_cep 
        /// </summary>
        public void Alterar(CEPVO identCEP)
        {
            new CEPData().Alterar(identCEP);
        }
        /// <summary>
	    /// Método para executar a proc pr_excluir_cep 
	    /// </summary>
        public void Excluir(CEPVO identCEP)
        {
            new CEPData().Excluir(identCEP);
        }
    }
}
