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
    }
}
