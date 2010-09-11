using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.Model;
using System.Data;

namespace Nissi.DataAccess
{
    public class EmitenteData : NissiBaseData
    {
        #region Métodos de Listagem
        public List<EmitenteVO> Lista(EmitenteVO identEmitente)
        {
            OpenCommand("pr_selecionar_emitente");
            try
            {
                if (identEmitente.CodEmitente > 0)
                    AddInParameter("CodEmitente", DbType.Int32, identEmitente.CodEmitente);
                List<EmitenteVO> lEmitente = new List<EmitenteVO>();
                IDataReader dr = ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        EmitenteVO tempEmitente = new EmitenteVO();
                        tempEmitente.CodEmitente = GetReaderValue<int?>(dr, "CodEmitente");
                        tempEmitente.RazaoSocial = GetReaderValue<string>(dr, "RazaoSocial");
                        tempEmitente.NomeFantasia = GetReaderValue<string>(dr, "NomeFantasia");
                        tempEmitente.CNPJ = GetReaderValue<string>(dr, "CNPJ");
                        tempEmitente.InscricaoEstadual = GetReaderValue<string>(dr, "InscricaoEstadual");
                        tempEmitente.Telefone = GetReaderValue<string>(dr, "Telefone");
                        tempEmitente.CNAE = GetReaderValue<string>(dr, "CNAE");
                        tempEmitente.Cep.CodCep = GetReaderValue<string>(dr, "CodCep");
                        tempEmitente.Cep.Cidade.CodCidade = GetReaderValue<int?>(dr, "CodCidade");
                        tempEmitente.Logradouro = GetReaderValue<string>(dr, "Logradouro");
                        tempEmitente.Pais = GetReaderValue<string>(dr, "Pais");
                        tempEmitente.Numero = GetReaderValue<int?>(dr, "Numero");
                        tempEmitente.Complemento = GetReaderValue<string>(dr, "Complemento");
                        tempEmitente.Cep.Cidade.UF.CodUF = GetReaderValue<string>(dr, "CodUF");
                        tempEmitente.Image = GetReaderValue<byte[]>(dr, "Logo");
                        tempEmitente.Fax = GetReaderValue<string>(dr, "Fax");
                        tempEmitente.Email = GetReaderValue<string>(dr, "Email");
                        ListarCep(ref tempEmitente);
                        lEmitente.Add(tempEmitente);

                    }
                }
                finally
                {
                    dr.Close();
                }
                return lEmitente;
            }
            finally
            {
                CloseCommand();
            }
        }
        private void ListarCep(ref EmitenteVO tempEmitente)
        {
            CEPVO tempCep = new CEPData().Lista(tempEmitente.Cep);
            tempEmitente.Cep = tempCep;
        }
        #endregion

        #region Métodos de Inclusão
        public int Inclui(EmitenteVO identEmitente)
        {
            OpenCommand("pr_incluir_emitente", true);
            try
            {
                int codEmitente = int.MinValue;
                AddInParameter("@RazaoSocial", DbType.String, identEmitente.RazaoSocial);
                AddInParameter("@NomeFantasia", DbType.String, identEmitente.NomeFantasia);
                AddInParameter("@CNPJ", DbType.String, identEmitente.CNPJ);
                AddInParameter("@InscricaoEstadual", DbType.String, identEmitente.InscricaoEstadual);
                AddInParameter("@InscricaoMunicipal", DbType.String, identEmitente.InscricaoMunicipal);
                AddInParameter("@CNAE", DbType.String, identEmitente.CNAE);
                AddInParameter("@InscricaoEstadualSub", DbType.String, identEmitente.InscricaoEstadualSub);
                AddInParameter("@CodCep", DbType.String, identEmitente.Cep.CodCep);
                AddInParameter("@Logradouro", DbType.String, identEmitente.Logradouro);
                AddInParameter("@Complemento", DbType.String, identEmitente.Complemento);
                AddInParameter("@Numero", DbType.Int32, identEmitente.Numero);
                AddInParameter("@Pais", DbType.String, "Brasil");
                AddInParameter("@CodUF", DbType.String, identEmitente.Cep.Cidade.UF.CodUF);
                AddInParameter("@CodCidade", DbType.Int32, identEmitente.Cep.Cidade.CodCidade);
                AddInParameter("@Telefone", DbType.String, identEmitente.Telefone);
                AddInParameter("@Logo", DbType.Binary, identEmitente.Image);
                AddInParameter("@Fax", DbType.String, identEmitente.Fax);
                AddInParameter("@Email", DbType.String, identEmitente.Email);
                ExecuteNonQuery();
                codEmitente = GetReturnValue();
                return codEmitente;
            }
            finally
            {
                CloseCommand();
            }
        }

        #endregion

        #region Métodos de Exclusão
        public void Exclui(EmitenteVO identEmitente)
        {
            OpenCommand("pr_excluir_emitente");
            try
            {
                AddInParameter("CodEmitente", DbType.Int32, identEmitente.CodEmitente);
                ExecuteNonQuery();

            }
            finally
            {
                CloseCommand();
            }
        }


        #endregion

        #region Métodos de Alteração
        public void Altera(EmitenteVO identEmitente)
        {
            OpenCommand("pr_alterar_emitente");
            try
            {
                AddInParameter("@CodEmitente", DbType.String, identEmitente.CodEmitente);
                AddInParameter("@RazaoSocial", DbType.String, identEmitente.RazaoSocial);
                AddInParameter("@NomeFantasia", DbType.String, identEmitente.NomeFantasia);
                AddInParameter("@CNPJ", DbType.String, identEmitente.CNPJ);
                AddInParameter("@InscricaoEstadual", DbType.String, identEmitente.InscricaoEstadual);
                AddInParameter("@InscricaoMunicipal", DbType.String, identEmitente.InscricaoMunicipal);
                AddInParameter("@CNAE", DbType.String, identEmitente.CNAE);
                AddInParameter("@InscricaoEstadualSub", DbType.String, identEmitente.InscricaoEstadualSub);
                AddInParameter("@CodCep", DbType.String, identEmitente.Cep.CodCep);
                AddInParameter("@Logradouro", DbType.String, identEmitente.Logradouro);
                AddInParameter("@Complemento", DbType.String, identEmitente.Complemento);
                AddInParameter("@Numero", DbType.Int32, identEmitente.Numero);
                AddInParameter("@Pais", DbType.String, "Brasil");
                AddInParameter("@CodUF", DbType.String, identEmitente.Cep.Cidade.UF.CodUF);
                AddInParameter("@CodCidade", DbType.Int32, identEmitente.Cep.Cidade.CodCidade);
                AddInParameter("@Telefone", DbType.String, identEmitente.Telefone);
                AddInParameter("@Logo", DbType.Binary, identEmitente.Image);
                AddInParameter("@Fax", DbType.String, identEmitente.Fax);
                AddInParameter("@Email", DbType.String, identEmitente.Email);
                ExecuteNonQuery();
            }
            finally
            {
                CloseCommand();
            }
        }

        #endregion

    }
}
