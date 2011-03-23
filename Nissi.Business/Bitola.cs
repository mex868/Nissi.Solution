using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.Model;
using Nissi.Repositorio;

namespace Nissi.Business
{
    public class Bitola
    {
        #region Métodos de Listagem
        public List<BitolaVO> Listar()
        {
            return new BitolaRepositorio().Listar();
        }
        public BitolaVO ListarPorCodigo(int codigoBitola)
        {
            return new BitolaRepositorio().ListarPorCodigo(codigoBitola);

        }
        public List<BitolaVO> ListarPorBitola(decimal bitola)
        {
            return new BitolaRepositorio().ListarPorBitola(bitola);
        }
        #endregion
        #region Métodos de Inclusão
        public int Incluir(decimal bitola, int codUsuarioInc)
        {
            return new BitolaRepositorio().Incluir(bitola, codUsuarioInc);
        }
        #endregion
        #region Método de Alteração
        public void Alterar(int codBitola, decimal bitola, int codUsuarioAlt)
        {
            new BitolaRepositorio().Alterar(codBitola, bitola, codUsuarioAlt);
        }
        #endregion
        #region Método de Exclusão
        public void Excluir(int codBitola, int codUsuarioInc)
        {
            new BitolaRepositorio().Excluir(codBitola, codUsuarioInc);
        }
        #endregion
    }
}
