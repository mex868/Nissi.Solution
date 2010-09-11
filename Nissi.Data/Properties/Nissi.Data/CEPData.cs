using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.Model;
using System.Data;

namespace Nissi.DataAccess
{
    public class CEPData: NissiBaseData
    {
        #region Métodos de Listagem
        /// <summary>
        /// Listagem de CEP
        /// Objeto/Parametros: (identCEP)
        /// Valores: identCEP.CodCep
        /// </summary>
        /// <param name="identCEP"></param>
        /// <returns></returns>
        public CEPVO Lista(CEPVO identCEP)
        {
            OpenCommand("pr_selecionar_cep");

            try
            {
                AddInParameter("CodCEP", DbType.String, identCEP.CodCep);

                CEPVO identCEPTemp = new CEPVO();

                IDataReader dr = ExecuteReader();

                try
                {
                    if (dr.Read())
                    {
                        identCEPTemp.CodCep = GetReaderValue<string>(dr, "CodCep");
                        identCEPTemp.NomEndereco = GetReaderValue<string>(dr, "NomLogr");
                        identCEPTemp.Bairro.CodBairro = GetReaderValue<int?>(dr, "CodBairro");
                        identCEPTemp.Bairro.NomBairro = GetReaderValue<string>(dr, "NomBairro");
                        identCEPTemp.Cidade.CodCidade = GetReaderValue<int>(dr, "CodCidade");
                        identCEPTemp.Cidade.NomCidade = GetReaderValue<string>(dr, "NomCidade");
                        identCEPTemp.Cidade.UF.CodUF = GetReaderValue<string>(dr, "CodUF");
                    }
                }
                finally
                {
                    dr.Close();
                }

                return identCEPTemp;
            }
            finally
            {
                CloseCommand();
            }
        }

        /// <summary>
        /// Pesquisa Fonética de Logradouro
        /// Objeto/Parametros: (identCEP)
        /// Valores: identCEP.Cidade.CodCidade(Obrigatório),
        /// identCEP.NomEndereco(Obrigatório)
        /// </summary>
        /// <param name="identCEP"></param>
        /// <returns></returns>
        public List<CEPVO> ListaPorLogradouro(CEPVO identCEP)

        {
            OpenCommand("pr_selecionar_cep_pesquisa_fonetica");

            try
            {
                AddInParameter("CodCidade", DbType.Int32, identCEP.Cidade.CodCidade);
                AddInParameter("ListaCodFon", DbType.String, identCEP.NomEndereco);

                List<CEPVO> listaCEP = new List<CEPVO>();

                IDataReader dr = ExecuteReader();

                try
                {
                    while (dr.Read())
                    {
                        CEPVO identCEPTemp = new CEPVO();

                        identCEPTemp.CodCep = GetReaderValue<string>(dr, "CodCep");
                        identCEPTemp.NomEndereco = GetReaderValue<string>(dr, "NomLogr");
                        identCEPTemp.Bairro.CodBairro = GetReaderValue<int?>(dr, "CodBairro");
                        identCEPTemp.Bairro.NomBairro = GetReaderValue<string>(dr, "NomBairro");
                        identCEPTemp.Cidade.CodCidade = GetReaderValue<int>(dr, "CodCidade");
                        identCEPTemp.Cidade.NomCidade = GetReaderValue<string>(dr, "NomCidade");
                        identCEPTemp.Cidade.UF.CodUF = GetReaderValue<string>(dr, "CodUF");

                        listaCEP.Add(identCEPTemp);
                    }
                }
                finally
                {
                    dr.Close();
                }

                return listaCEP;
            }
            finally
            {
                CloseCommand();
            }
        }
        #endregion
    }
}
