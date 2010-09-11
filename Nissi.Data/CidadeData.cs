using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Nissi.Model;

namespace Nissi.DataAccess
{
    public class CidadeData: NissiBaseData
    {
        #region Métodos de Listagem
        /// <summary>
        /// Listagem de Cidades por UF.
        /// Objeto/Parâmetros: (identUF)
        /// Valores: identUF.CodUF
        /// Se for passado null no valores ele lista todos os dados
        /// </summary>
        /// <param name="identUF">identUF.CodUF(opcional)</param>
        /// <returns></returns>
        public List<CidadeVO> Lista(UnidadeFederacaoVO identUF)
        {
            OpenCommand("pr_selecionar_cidade");

            try
            {
                if (!string.IsNullOrEmpty(identUF.CodUF))
                    AddInParameter("CodUF", DbType.String, identUF.CodUF);

                IDataReader dr = ExecuteReader();

                List<CidadeVO> listaCidade = new List<CidadeVO>();

                try
                {
                    while (dr.Read())
                    {
                        CidadeVO identCidade = new CidadeVO();
                        identCidade.CodCidade = GetReaderValue<int>(dr, "CodCidade");
                        identCidade.CodIBGE = GetReaderValue<string>(dr, "CodIBGE");
                        identCidade.NomCidade = GetReaderValue<string>(dr, "NomCidade");

                        if (!string.IsNullOrEmpty(identUF.CodUF))
                            identCidade.UF.CodUF = identUF.CodUF;
                        else
                            identCidade.UF.CodUF = GetReaderValue<string>(dr, "CodUF");

                        listaCidade.Add(identCidade);
                    }
                }
                finally
                {
                    dr.Close();
                }

                return listaCidade;
            }
            finally
            {
                CloseCommand();
            }
        }
        #endregion
    }
}
