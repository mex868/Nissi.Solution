using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.Model;
using Nissi.Repositorio;

namespace Nissi.Business
{
    public class ResistenciaTracao
    {
        #region Métodos de Listagem
        public List<ResistenciaTracaoVO> Listar()
        {
            return new ResistenciaTracaoRepositorio().Listar();
        }
        public ResistenciaTracaoVO ListarPorCodigo(int codResistenciaTracao)
        {
            return new ResistenciaTracaoRepositorio().ListarPorCodigo(codResistenciaTracao);
        }
        #endregion
        #region Método de Inclusão
        public int Incluir(int codMateriaPrima, int codBitola)
        {
            return new ResistenciaTracaoRepositorio().Incluir(codMateriaPrima, codBitola);
        }
        #endregion
        #region Método de Alteração
        public void Alterar(int codResistenciaTracao, int codMateriaPrima, int codBitola)
        {
            new ResistenciaTracaoRepositorio().Alterar(codResistenciaTracao, codMateriaPrima, codBitola);
        }
        #endregion
        #region Método de Exclusão
        public void Excluir(int codResistenciaTracao)
        {
            new ResistenciaTracaoRepositorio().Excluir(codResistenciaTracao);
        }
        #endregion
    }
}
