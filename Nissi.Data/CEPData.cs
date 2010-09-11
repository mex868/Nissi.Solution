using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.Model;
using System.Data;

namespace Nissi.DataAccess
{
    public class CEPData : NissiBaseData
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
                        identCEPTemp.Cidade.CodIBGE = GetReaderValue<string>(dr, "CodIBGE");
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
                        identCEPTemp.Cidade.CodIBGE = GetReaderValue<string>(dr, "CodIBGE");

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

        #region Método de Inclusão
        public void Incluir(CEPVO identCEP)
        {
            OpenCommand("pr_incluir_cep");
            try
            {
                AddInParameter("@CodCEP", DbType.String, identCEP.CodCep);
                AddInParameter("@NomLogrCEP", DbType.String, identCEP.NomEndereco);
                AddInParameter("@NomTipoLog", DbType.String, identCEP.TipoLogradouro.NomTipoLogradouro);
                AddInParameter("@CodUsuInc", DbType.Int32, 1);
                AddInParameter("@CodCidade", DbType.Int32, identCEP.Cidade.CodCidade);
                AddInParameter("@NomBairro", DbType.String, identCEP.Bairro.NomBairro);
                AddInParameter("@CodUF", DbType.String, identCEP.Cidade.UF.CodUF);
                ExecuteNonQuery();
            }
            finally
            {
                CloseCommand();
            }

        }
        #endregion
        #region Método de Alteração
        /// <summary>
        /// Método para executar a proc pr_alterar_cep 
        /// </summary>
        public void Alterar(CEPVO identCEP)
        {
            OpenCommand("pr_alterar_cep");
            try
            {
                // Parâmetros de entrada
                AddInParameter("@CodCEP", DbType.AnsiStringFixedLength, identCEP.CodCep);
                AddInParameter("@NomLogrCEP", DbType.AnsiString, identCEP.NomEndereco);
                AddInParameter("@NomTipoLog", DbType.AnsiString, identCEP.TipoLogradouro);
                AddInParameter("@CodUsuAlt", DbType.Int32, 1);
                AddInParameter("@CodCidade", DbType.Int32, identCEP.Cidade.CodCidade);
                AddInParameter("@NomBairro", DbType.AnsiString, identCEP.Bairro.NomBairro);
                AddInParameter("@CodUF", DbType.AnsiStringFixedLength, identCEP.Cidade.UF.CodUF);

                ExecuteNonQuery();
            }
            finally
            {
                CloseCommand();
            }
        }


        // ------------------------------------------------------------------------- // 
        #endregion
        #region Método de Exclusão
        	    /// <summary>
	    /// Método para executar a proc pr_excluir_cep 
	    /// </summary>
	    public void Excluir(CEPVO identCEP)
	    {	
		    OpenCommand("pr_excluir_cep");
		    try
		    {
			    // Parâmetros de entrada
			    AddInParameter("@CodCEP", DbType.AnsiString, identCEP.CodCep);
                        
			    ExecuteNonQuery();
		    }            
		    finally
		    {
			    CloseCommand();
		    }				
	    }


     // ------------------------------------------------------------------------- // 
        #endregion
    }

}
