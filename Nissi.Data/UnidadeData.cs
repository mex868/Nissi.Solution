using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.Model;
using System.Data;

namespace Nissi.DataAccess
{
    public class UnidadeData:NissiBaseData
    {
        #region Métodos de Listagem
        /// <summary>
        /// Método para listar os produtos
        /// </summary>
        /// <returns></returns>
        public List<UnidadeVO> Lista(UnidadeVO identUnidade)
        {
            OpenCommand("pr_selecionar_unidade");
            try
            {
                if (identUnidade.CodUnidade > 0)
                    AddInParameter("CodUnidade", DbType.Int32, identUnidade.CodUnidade);
                IDataReader dr = ExecuteReader();
                List<UnidadeVO> lUnidade = new List<UnidadeVO>();

                try
                {
                    while (dr.Read())
                    {
                        UnidadeVO tempUnidade = new UnidadeVO();
                        tempUnidade.CodUnidade= GetReaderValue<int?>(dr, "CodUnidade");
                        tempUnidade.TipoUnidade = GetReaderValue<string>(dr, "Unidade");
                        tempUnidade.Descricao = GetReaderValue<string>(dr, "Descricao");
                        
                        lUnidade.Add(tempUnidade);
                    }
                }
                finally
                {
                    dr.Close();
                }

                return lUnidade;
            }
            finally
            {
                CloseCommand();
            }
        }
        #endregion
        #region Método de Inclusão
        /// <summary>
        /// Método para excluir um produto
        /// </summary>
        /// <param name="identProduto">passar Unidade, descrição, usuarioInc</param>
        public void Inclui(UnidadeVO identUnidade)
        {
            OpenCommand("pr_incluir_unidade");
            try
            {
                AddInParameter("Unidade", DbType.String, identUnidade.TipoUnidade);
                AddInParameter("Descricao", DbType.String, identUnidade.Descricao);
                AddInParameter("UsuarioInc", DbType.Int32, 1);
                ExecuteNonQuery();
            }
            finally
            {
                CloseCommand();
            }
        }
        #endregion

        #region Métodos de Alteração
        /// <summary>
        /// Método para executar a proc pr_alterar_Unidade
        /// Objeto/Parametros: (identUnidade, codUsuarioOperacao)
        /// Valore: identUnidade.CodUnidade,
        ///identUnidade.Unidade
        ///identUnidade.Descricao
        ///codUsuarioOperacao
        /// </summary>
        /// <param name="identProduto"></param>
        public void Altera(UnidadeVO identUnidade)
        {
            OpenCommand("pr_alterar_unidade");
            try
            {
                AddInParameter("CodUnidade", DbType.Int32, identUnidade.CodUnidade);
                AddInParameter("Unidade", DbType.String, identUnidade.TipoUnidade);
                AddInParameter("Descricao", DbType.String, identUnidade.Descricao);
                AddInParameter("UsuarioAlt", DbType.Int32, 1);
                ExecuteNonQuery();
            }
            finally
            {
                CloseCommand();
            }
        }
        #endregion

        #region Método de Exlusão
        /// <summary>
        /// Método de Exclusão da unidade
        /// </summary>
        /// <param name="identUnidade">passar codUnidade</param>
        public void Exclui(UnidadeVO identUnidade)
        {
            OpenCommand("pr_excluir_unidade");
            try
            {
                AddInParameter("CodUnidade", DbType.Int32, identUnidade.CodUnidade);
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
