using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.Model;
using System.Data;

namespace Nissi.DataAccess
{
    public class CFOPData: NissiBaseData
    {
        /// <summary>
        /// Método para executar a proc pr_selecionar_cfop 
        /// </summary>
        public List<CFOPVO> Listar(CFOPVO identCFOP)
        {
            OpenCommand("pr_selecionar_cfop");
            try
            {
                // Parâmetros de entrada
                if (identCFOP.CodCFOP > 0)
                AddInParameter("@CodCFOP", DbType.Int32,identCFOP.CodCFOP);
                if (!string.IsNullOrEmpty(identCFOP.CFOP))
                AddInParameter("@CFOP", DbType.String, identCFOP.CFOP);
                if (!string.IsNullOrEmpty(identCFOP.NaturezaOperacao))
                AddInParameter("@NaturezaOperacao", DbType.String, identCFOP.NaturezaOperacao);

                List<CFOPVO> lstCfopVO = new List<CFOPVO>();

                IDataReader dr = ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        CFOPVO cfopVO = new CFOPVO();

                        cfopVO.CodCFOP = GetReaderValue<int?>(dr, "CodCFOP");
                        cfopVO.CFOP = GetReaderValue<string>(dr, "CFOP");
                        cfopVO.NaturezaOperacao = GetReaderValue<string>(dr, "NaturezaOperacao");
                        cfopVO.UsuarioInc = GetReaderValue<int?>(dr, "UsuarioInc");
                        cfopVO.DataCadastro = GetReaderValue<DateTime?>(dr, "DataCadastro");
                        cfopVO.UsuarioAlt = GetReaderValue<int?>(dr, "UsuarioAlt");
                        cfopVO.DataAlteracao = GetReaderValue<DateTime?>(dr, "DataAlteracao");

                        lstCfopVO.Add(cfopVO);
                    }
                }
                finally
                {
                    dr.Close();
                }

                return lstCfopVO;
            }
            finally
            {
                CloseCommand();
            }
        }
        // ------------------------------------------------------------------------- // 
        /// <summary>
        /// Método para executar a proc pr_incluir_cfop 
        /// </summary>
        public int Incluir(CFOPVO identCFOP,int usuarioAtivo)
        {
            OpenCommand("pr_incluir_cfop",true);
            try
            {
                // Parâmetros de entrada
                AddInParameter("@CFOP", DbType.String, identCFOP.CFOP);
                AddInParameter("@NaturezaOperacao", DbType.String, identCFOP.NaturezaOperacao);
                AddInParameter("@UsuarioInc", DbType.Int32, usuarioAtivo);

                ExecuteNonQuery();
                int retorno = GetReturnValue();
                return retorno;
            }
            finally
            {
                CloseCommand();
            }
        }
        // ------------------------------------------------------------------------- // 
        /// <summary>
        /// Método para executar a proc pr_alterar_cfop 
        /// </summary>
        public void Alterar(CFOPVO identCFOP)
        {
            OpenCommand("pr_alterar_cfop");
            try
            {
                // Parâmetros de entrada
                AddInParameter("@CodCFOP", DbType.Int32, identCFOP.CodCFOP);
                AddInParameter("@CFOP", DbType.String, identCFOP.CFOP);
                AddInParameter("@NaturezaOperacao", DbType.String, identCFOP.NaturezaOperacao);
                AddInParameter("@UsuarioAlt", DbType.Int32, identCFOP.UsuarioAlt);

                ExecuteNonQuery();
            }
            finally
            {
                CloseCommand();
            }
        }
        // ------------------------------------------------------------------------- //
        /// <summary>
        /// Método para executar a proc pr_excluir_cfop
        /// </summary>
        /// <param name="codCFOP"></param>
        public void Excluir(int codCFOP)
        {
            OpenCommand("pr_excluir_cfop");
            try
            {
                // Parâmetros de entrada
                AddInParameter("@CodCFOP", DbType.Int32, codCFOP);
                ExecuteNonQuery();
            }
            finally
            {
                CloseCommand();
            }

        }

    }
}
