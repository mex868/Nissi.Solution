using System;
using System.Collections.Generic;
using Nissi.Model;
using System.Data;

namespace Nissi.DataAccess
{
    public class TipoLogradouroData : NissiBaseData
    {
        #region Método de Listagem
        /// <summary>
        /// Método para executar a proc pr_selecionar_nomtipolog 
        /// Objeto/Parâmetros: (codTipoFornecimento)
        /// </summary>
        public List<TipoLogradouroVO> Listar()
        {
            OpenCommand("pr_selecionar_nomtipolog");
            try
            {
                List<TipoLogradouroVO> lstTipoLogradouroVO = new List<TipoLogradouroVO>();

                IDataReader dr = ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        TipoLogradouroVO tipoLogradouroVO = new TipoLogradouroVO();
                        tipoLogradouroVO.CodNomTipoLogradouro = GetReaderValue<int?>(dr, "CodNomTipoLog");
                        tipoLogradouroVO.NomTipoLogradouro = GetReaderValue<string>(dr, "NomTipoLog");

                        lstTipoLogradouroVO.Add(tipoLogradouroVO);
                    }
                }
                finally
                {
                    dr.Close();
                }
                return lstTipoLogradouroVO;
            }
            finally
            {
                CloseCommand();
            }
        }
        #endregion
    }
}
