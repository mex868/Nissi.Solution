using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.Model;
using Nissi.Repositorio;

namespace Nissi.Business
{
    public class Norma
    {
        #region Métodos de Listagem
        public List<NormaVO> Listar()
        {
            return new NormaRepositorio().Listar();
        }
        public NormaVO ListarPorCodigo(int codNorma)
        {
            return new NormaRepositorio().ListarPorCodigo(codNorma);
        }
        public List<MateriaPrimaVO> ListarPorNorma(string norma)
        {
            return new NormaRepositorio().ListarPorNorma(norma);
        }
        #endregion
        #region Método de Inclusão
        public int Incluir(string descricao, int? revisao, int codUsuarioInc)
        {
            return new NormaRepositorio().Incluir(descricao, revisao, codUsuarioInc);
        }
        #endregion
        #region Método de Alteração
        public void Alterar(int codNorma, string descricao, int? revisao, int codUsuarioAlt)
        {
            new NormaRepositorio().Alterar(codNorma, descricao, revisao, codUsuarioAlt);
        }
        #endregion
        #region Método de Exclusão
        public void Excluir(int codNorma, int codUsuarioExc)
        {
            new NormaRepositorio().Excluir(codNorma, codUsuarioExc);
        }
        #endregion
    }
}
