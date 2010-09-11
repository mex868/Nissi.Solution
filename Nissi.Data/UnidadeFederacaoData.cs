using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.Model;
using System.Data;

namespace Nissi.DataAccess
{
    public class UnidadeFederacaoData: NissiBaseData
    {
        #region Métodos de Listagem
        public List<UnidadeFederacaoVO> Listar()
        {
            OpenCommand("pr_selecionar_unidadefederacao");
            try
            {
                IDataReader dr = ExecuteReader();

                List<UnidadeFederacaoVO> listaUF = new List<UnidadeFederacaoVO>();

                try
                {
                    while (dr.Read())
                    {
                        UnidadeFederacaoVO identUF = new UnidadeFederacaoVO();
                        identUF.CodUF = GetReaderValue<string>(dr, "CodUF");
                        identUF.NomUF = GetReaderValue<string>(dr, "NomUF");
                        listaUF.Add(identUF);
                    }
                }
                finally
                {
                    dr.Close();
                }

                return listaUF;
            }
            finally
            {
                CloseCommand();
            }
        }
        #endregion
    }
}
