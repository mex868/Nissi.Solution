using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.Model;
using Nissi.Repositorio;

namespace Nissi.Business
{
    public class PrazoEntrega
    {
        public List<PrazoEntregaVO> Listar()
        {
            return new PrazoEntregaRepositorio().Listar();
        }
        public PrazoEntregaVO ListarPorCodigo(int codigo)
        {
            return new PrazoEntregaRepositorio().ListarPorCodigo(codigo);
        }
        public List<PrazoEntregaVO> ListarPorDescricao(string descricao)
        {
            return new PrazoEntregaRepositorio().ListarPorDescricao(descricao);
        }
        #region Método de Inclusão
        public int Incluir(string descricao, int dias, int codUsuarioInc)
        {
           return new PrazoEntregaRepositorio().Incluir(descricao, dias, codUsuarioInc);
        }
        #endregion
        #region Método de Alteração
        public void Alterar(int codPrazoEntrega, string descricao, int dias, int codUsuarioAlt)
        {
            new PrazoEntregaRepositorio().Alterar(codPrazoEntrega, descricao, dias, codUsuarioAlt);
        }
        #endregion
        #region Método de Exclusão
        public void Excluir(int codPrazoEntrega)
        {
            new PrazoEntregaRepositorio().Excluir(codPrazoEntrega);
        }
        #endregion
    }
}
