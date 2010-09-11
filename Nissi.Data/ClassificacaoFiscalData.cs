using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.Model;
using System.Data;

namespace Nissi.DataAccess
{
    public class ClassificacaoFiscalData: NissiBaseData
    {
        /// <summary>
        /// Método para executar a proc pr_selecionar_classificacaofiscal 
        /// </summary>
        public List<ClassificacaoFiscalVO> Listar(ClassificacaoFiscalVO identClassificacaoFiscal)
        {
            OpenCommand("pr_selecionar_classificacaofiscal");
            try
            {
                // Parâmetros de entrada
                AddInParameter("@CodClassificacaoFiscal", DbType.Int32, identClassificacaoFiscal.CodClassificacaoFiscal);
                if (!string.IsNullOrEmpty(identClassificacaoFiscal.Letra))
                    AddInParameter("@Letra", DbType.String, identClassificacaoFiscal.Letra);
                if (!string.IsNullOrEmpty(identClassificacaoFiscal.Numero))
                    AddInParameter("@Numero", DbType.String, identClassificacaoFiscal.Numero);

                List<ClassificacaoFiscalVO> lstClassificacaofiscalVO = new List<ClassificacaoFiscalVO>();

                IDataReader dr = ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        ClassificacaoFiscalVO classificacaofiscalVO = new ClassificacaoFiscalVO();

                        classificacaofiscalVO.CodClassificacaoFiscal = GetReaderValue<int?>(dr, "CodClassificacaoFiscal");
                        classificacaofiscalVO.Letra = GetReaderValue<string>(dr, "Letra");
                        classificacaofiscalVO.Numero = GetReaderValue<string>(dr, "Numero");
                        classificacaofiscalVO.DataCadastro = GetReaderValue<DateTime?>(dr, "DataCadastro");
                        classificacaofiscalVO.UsuarioInc = GetReaderValue<int?>(dr, "UsuarioInc");
                        classificacaofiscalVO.DataAlteracao = GetReaderValue<DateTime?>(dr, "DataAlteracao");
                        classificacaofiscalVO.UsuarioAlt = GetReaderValue<int?>(dr, "UsuarioAlt");

                        lstClassificacaofiscalVO.Add(classificacaofiscalVO);
                    }
                }
                finally
                {
                    dr.Close();
                }

                return lstClassificacaofiscalVO;
            }
            finally
            {
                CloseCommand();
            }
        }


        // ------------------------------------------------------------------------- // 
        /// <summary>
        /// Método para executar a proc pr_incluir_classificacaofiscal 
        /// </summary>
        public int Incluir(ClassificacaoFiscalVO identClassificacaoFiscal)
        {
            OpenCommand("pr_incluir_classificacaofiscal",true);
            int retorno = 0;
            try
            {
                // Parâmetros de entrada
                AddInParameter("@Letra", DbType.String, identClassificacaoFiscal.Letra);
                AddInParameter("@Numero", DbType.String, identClassificacaoFiscal.Numero);
                AddInParameter("@UsuarioInc", DbType.Int32,identClassificacaoFiscal.UsuarioInc);
                ExecuteNonQuery();
                retorno = GetReturnValue();
                return retorno;
            }
            finally
            {
                CloseCommand();
            }
        }


        // ------------------------------------------------------------------------- // 
        /// <summary>
        /// Método para executar a proc pr_alterar_classificacaofiscal 
        /// </summary>
        public void Alterar(ClassificacaoFiscalVO identClassificacaoFiscal)
        {
            OpenCommand("pr_alterar_classificacaofiscal");
            try
            {
                // Parâmetros de entrada
                AddInParameter("@CodClassificacaoFiscal", DbType.Int32, identClassificacaoFiscal.CodClassificacaoFiscal);
                AddInParameter("@Letra", DbType.String, identClassificacaoFiscal.Letra);
                AddInParameter("@Numero", DbType.String, identClassificacaoFiscal.Numero);
                AddInParameter("@UsuarioAlt", DbType.Int32, identClassificacaoFiscal.UsuarioAlt);

                ExecuteNonQuery();
            }
            finally
            {
                CloseCommand();
            }
        }


        // ------------------------------------------------------------------------- // 
        /// <summary>
        /// Método para executar a proc pr_excluir_classificacaofiscal 
        /// </summary>
        public void Excluir(ClassificacaoFiscalVO identClassificacaoFiscal)
        {
            OpenCommand("pr_excluir_classificacaofiscal");
            try
            {
                // Parâmetros de entrada
                AddInParameter("@CodClassificacaoFiscal", DbType.Int32, identClassificacaoFiscal.CodClassificacaoFiscal);

                ExecuteNonQuery();
            }
            finally
            {
                CloseCommand();
            }
        }


        // ------------------------------------------------------------------------- // 



    

    }
}
