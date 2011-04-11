using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.Repositorio;
using Nissi.Model;

namespace Nissi.Business
{
    public class ClasseTipo
    {
        #region Métodos de Listagem
        public List<ClasseTipoVO> Listar()
        {
            return new ClasseTipoRepositorio().Listar();
        }
        public ClasseTipoVO ListarPorCodigo(int codClasseTipo)
        {
            return new ClasseTipoRepositorio().ListarPorCodigo(codClasseTipo);
        }
        public List<ClasseTipoVO> ListarPorClasseTipo(string classeTipo)
        {
            return new ClasseTipoRepositorio().ListarPorClasseTipo(classeTipo);
        }
        #endregion
        #region Método de Inclusão
        public int Incluir(string descricao, int codUsuarioInc)
        {
            return new ClasseTipoRepositorio().Incluir(descricao, codUsuarioInc);
        }
        #endregion
        #region Método de Alteração
        public void Alterar(int codClasseTipo, string descricao, int codUsuarioAlt)
        {
            new ClasseTipoRepositorio().Alterar(codClasseTipo, descricao, codUsuarioAlt);
        }
        #endregion
        #region Método de Exclusão
        public void Excluir(int codClasseTipo, int codUsuarioExc)
        {
            new ClasseTipoRepositorio().Excluir(codClasseTipo, codUsuarioExc);
        }
        #endregion
    }
}
