using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Nissi.Model;

namespace Nissi.DataAccess
{
    public class MensagemNFData : NissiBaseData
    {
        /// <summary>
        /// Método para executar a proc pr_selecionar_mensagagemnf 
        /// </summary>
        public List<MensagemNFVO> Listar(MensagemNFVO identMensagemNF)
        {
            OpenCommand("pr_selecionar_mensagagemnf");
            try
            {
                // Parâmetros de entrada
                if (identMensagemNF.CodMensagemNF > 0)
                    AddInParameter("@CodMensagemNF", DbType.Int32, identMensagemNF.CodMensagemNF);
                if (!string.IsNullOrEmpty(identMensagemNF.Descricao))
                    AddInParameter("@Descricao", DbType.String, identMensagemNF.Descricao);

                List<MensagemNFVO> lstMensagemNFVO = new List<MensagemNFVO>();

                IDataReader dr = ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        MensagemNFVO tempMensagemNF = new MensagemNFVO();

                        tempMensagemNF.CodMensagemNF = GetReaderValue<int?>(dr, "CodMensagemNF");
                        tempMensagemNF.Descricao = GetReaderValue<string>(dr, "Descricao");
                        tempMensagemNF.UsuarioInc = GetReaderValue<int?>(dr, "UsuarioInc");
                        tempMensagemNF.DataCadastro = GetReaderValue<DateTime?>(dr, "DataCadastro");
                        tempMensagemNF.UsuarioAlt = GetReaderValue<int?>(dr, "UsuarioAlt");
                        tempMensagemNF.DataAlteracao = GetReaderValue<DateTime?>(dr, "DataAlteracao");

                        lstMensagemNFVO.Add(tempMensagemNF);
                    }
                }
                finally
                {
                    dr.Close();
                }

                return lstMensagemNFVO;
            }
            finally
            {
                CloseCommand();
            }
        }


        // ------------------------------------------------------------------------- // 

        /// <summary>
        /// Método para executar a proc pr_incluir_mensagemnf 
        /// </summary>
        public int Incluir(MensagemNFVO identMensagemNF, int codUsuarioOperacao)
        {
            OpenCommand("pr_incluir_mensagemnf",true);
            try
            {
                // Parâmetros de entrada
                AddInParameter("@Descricao", DbType.String, identMensagemNF.Descricao);
                AddInParameter("@UsuarioInc", DbType.Int32, codUsuarioOperacao);
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
        /// Método para executar a proc pr_alterar_mensagemnf 
        /// </summary>
        public void Alterar(MensagemNFVO identMensagemNF, int codUsuarioOperacao)
        {
            OpenCommand("pr_alterar_mensagemnf");
            try
            {
                // Parâmetros de entrada
                AddInParameter("@CodMensagemNF", DbType.Int32, identMensagemNF.CodMensagemNF);
                AddInParameter("@Descricao", DbType.String, identMensagemNF.Descricao);
                AddInParameter("@UsuarioAlt", DbType.Int32, codUsuarioOperacao);
                ExecuteNonQuery();
   
            }
            finally
            {
                CloseCommand();
            }

           
        }


        // ------------------------------------------------------------------------- // 

        /// <summary>
        /// Método para executar a proc pr_excluir_mensagemnf 
        /// </summary>
        public void Excluir(int codMensagemNF, int codUsuarioOperacao)
        {
            OpenCommand("pr_excluir_mensagemnf");
            try
            {
                // Parâmetros de entrada
                AddInParameter("@CodMensagemNF", DbType.Int32, codMensagemNF);

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
