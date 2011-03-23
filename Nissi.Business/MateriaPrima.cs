using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.Model;
using Nissi.Repositorio;

namespace Nissi.Business
{
    public class MateriaPrima
    {
        #region Métodos de Listagem
        public MateriaPrimaVO ListarPorCodigo(int codigoMateriaPrima)
        {
            return new MateriaPrimaRepositorio().ListarPorCodigo(codigoMateriaPrima);
        }
        public List<MateriaPrimaVO> ListarPorNorma(string descricao)
        {
            return new MateriaPrimaRepositorio().ListarPorNorma(descricao);
        }

        public List<MateriaPrimaVO> ListarPorClasseTipo(int codClasseTipo)
        {
            return new MateriaPrimaRepositorio().ListarPorClasseTipo(codClasseTipo);
        }
        public List<MateriaPrimaVO> Listar()
        {
            return new MateriaPrimaRepositorio().Listar();
        }
        public MateriaPrimaVO ListarTudo(int codMateriaPrima)
        {
            return new MateriaPrimaRepositorio().ListarTudo(codMateriaPrima);
        }
        public ResistenciaTracaoVO ListarResistenciaTracao(int codMateriaPrima, int codBitola)
        {
            return new ResistenciaTracaoRepositorio().ListarPorCodigoBitola(codMateriaPrima, codBitola);
        }
        public List<ComposicaoMateriaPrimaVO> ListarComposicaoMateriaPrima(int codMateriaPrima)
        {
            return new ComposicaoMateriaPrimaRepositorio().ListarPorCodigo(codMateriaPrima);
        }
       #endregion
        #region Método de Inclusão
        public int Incluir(int? codClasseTipo, int codNorma, int usuarioInc, List<ComposicaoMateriaPrimaVO> lstComposicaoMateriaPrima, List<ResistenciaTracaoVO> lstResistenciaTracao)
        {
            return new MateriaPrimaRepositorio().Incluir(codClasseTipo, codNorma, usuarioInc, lstComposicaoMateriaPrima, lstResistenciaTracao); 
        }
        #endregion
        #region Métodos de Alteração
        public void Alterar(int codMateriaPrima, int? codClasseTipo, int codNorma, int usuarioAlt, List<ComposicaoMateriaPrimaVO> lstComposicaoMateriaPrima, List<ResistenciaTracaoVO> lstResistenciaTracao)
        {
            new MateriaPrimaRepositorio().Alterar(codMateriaPrima, codClasseTipo, codNorma, usuarioAlt, lstComposicaoMateriaPrima, lstResistenciaTracao);
        }
        #endregion
        #region Método de Exclusao
        public void Excluir(int codMateriaPrima, int codUsuarioExc)
        {
            new MateriaPrimaRepositorio().Excluir(codMateriaPrima, codUsuarioExc);
        }
        #endregion
        #region Método de Exclusão ComposicaoMateriaPrima
        public void ExcluirComposicaoMateriaPrima(int codComposicaoMateriaPrima)
        {
            new ComposicaoMateriaPrimaRepositorio().Excluir(codComposicaoMateriaPrima);
        }
        #endregion
        #region Método de Exclusão ResistenciaTracao
        public void ExcluirResistenciaTracao(int codResistenciaTracao)
        {
            new ResistenciaTracaoRepositorio().Excluir(codResistenciaTracao);
        }
        #endregion

    }
}
