using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Nissi.Model;

namespace Nissi.DataAccess
{
   public class BancoData:NissiBaseData
   {
  #region Métodos de Listagem
        /// <summary>
        /// Listagem de Bancos
        /// </summary>
        /// <returns></returns>
        public List<BancoVO> Lista()
        {
            OpenCommand("pr_selecionar_banco");
            try
            {
                IDataReader dr = ExecuteReader();

                List<BancoVO> listaBanco = new List<BancoVO>();

                try
                {
                    while (dr.Read())
                    {
                        BancoVO identBanco = new BancoVO();
                        identBanco.CodBanco = GetReaderValue<int?>(dr, "CodBanco");
                        identBanco.CodCompensacao = GetReaderValue<int?>(dr, "CodCompensacao");
                        identBanco.Banco = GetReaderValue<string>(dr, "Banco");

                        listaBanco.Add(identBanco);
                    }
                }
                finally
                {
                    dr.Close();
                }

                return listaBanco;
            }
            finally
            {
                CloseCommand();
            }
        }
        #endregion
   }
}
