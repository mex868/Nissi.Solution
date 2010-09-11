using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Nissi.Model;

namespace Nissi.DataAccess
{
    public class PerfilAcessoData: NissiBaseData
    {

        #region Método de Seleção
        /// <summary>
        /// Método para executar a proc pr_selecionar_perfilacesso 
        /// </summary>
        public List<PerfilAcessoVO> Listar(PerfilAcessoVO perfilAcessoVO)
        {
            OpenCommand("pr_selecionar_perfilacesso");
            try
            {
                // Parâmetros de entrada
                AddInParameter("@CodPerfilAcesso", DbType.Int16,perfilAcessoVO.CodPerfilAcesso);
                AddInParameter("@NomPerfilAcesso", DbType.String, perfilAcessoVO.CodPerfilAcesso);
                AddInParameter("@DescPerfilAcesso", DbType.String, perfilAcessoVO.DescPerfilAcesso);

                List<PerfilAcessoVO> lstPerfilacessoVO = new List<PerfilAcessoVO>();

                IDataReader dr = ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        PerfilAcessoVO perfilacesso = new PerfilAcessoVO();

                        perfilacesso.CodPerfilAcesso = GetReaderValue<short?>(dr, "CodPerfilAcesso");
                        perfilacesso.NomPerfilAcesso = GetReaderValue<string>(dr, "NomPerfilAcesso");
                        perfilacesso.DescPerfilAcesso = GetReaderValue<string>(dr, "DescPerfilAcesso");
                        perfilacesso.DataCadastro = GetReaderValue<DateTime>(dr, "DataCadastro");
                        perfilacesso.UsuarioInc = GetReaderValue<int>(dr, "UsuarioInc");
                        perfilacesso.DataAlteracao = GetReaderValue<DateTime>(dr, "DataAlteracao");
                        perfilacesso.UsuarioAlt = GetReaderValue<int>(dr, "UsuarioAlt");
                        perfilacesso.Ativo = GetReaderValue<bool>(dr, "Ativo");

                        lstPerfilacessoVO.Add(perfilacesso);
                    }
                }
                finally
                {
                    dr.Close();
                }

                return lstPerfilacessoVO;
            }
            finally
            {
                CloseCommand();
            }
        }
        #endregion

        // ------------------------------------------------------------------------- // 


        /// <summary>
        /// Método para incluir um registro na tabela PerfilAcesso 
        /// </summary>
        #region Métodos de Inclusão
        public void Incluir(PerfilAcessoVO perfilAcessoVO, int codUsuarioOperacao)
        {
            OpenCommand("pr_incluir_PerfilAcesso", true);
            try
            {
                // Parâmetros de entrada
                AddInParameter("@NomPerfilAcesso", DbType.String, perfilAcessoVO.NomPerfilAcesso);
                AddInParameter("@DescPerfilAcesso", DbType.String, perfilAcessoVO.DescPerfilAcesso);
                AddInParameter("@UsuarioInc", DbType.Int32, perfilAcessoVO.UsuarioInc);
                AddInParameter("@Ativo", DbType.Boolean, perfilAcessoVO.Ativo);
                ExecuteNonQuery();
            }
            finally
            {
                CloseCommand();
            }
        }
        #endregion

        // ------------------------------------------------------------------------- // 

        /// <summary>
        /// Método para alterar um registro na tabela  PerfilAcesso 
        /// </summary>
        #region Métodos de Alteração
        public void Alterar(PerfilAcessoVO perfilAcessoVO, int codUsuarioOperacao)
        {
            OpenCommand("pr_alterar_PerfilAcesso");
            try
            {
                // Parâmetros de entrada
                AddInParameter("@CodPerfilAcesso", DbType.Int16, perfilAcessoVO.CodPerfilAcesso);
                AddInParameter("@NomPerfilAcesso", DbType.String, perfilAcessoVO.NomPerfilAcesso);
                AddInParameter("@DescPerfilAcesso", DbType.String, perfilAcessoVO.DescPerfilAcesso);
                AddInParameter("@UsuarioAlt", DbType.Int32, perfilAcessoVO.UsuarioAlt);
                AddInParameter("@Ativo", DbType.Boolean, perfilAcessoVO.Ativo);
                ExecuteNonQuery();
            }
            finally
            {
                CloseCommand();
            }
        }
        #endregion

        // ------------------------------------------------------------------------- // 

        /// <summary>
        /// Método para excluir um registro na tabela  PerfilAcesso 
        /// </summary>
        #region Métodos de Exclusão
        public void Excluir(short? codPerfilAcesso, int codUsuarioOperacao)
        {
            OpenCommand("pr_excluir_PerfilAcesso");
            try
            {
                // Parâmetros de entrada
                AddInParameter("@CodPerfilAcesso", DbType.Int16, codPerfilAcesso);
                AddInParameter("@CodUsuarioOperacao", DbType.Int32, codUsuarioOperacao);

                ExecuteNonQuery();
            }
            finally
            {
                CloseCommand();
            }
        }
        #endregion

        // ------------------------------------------------------------------------- // 


    }
}
