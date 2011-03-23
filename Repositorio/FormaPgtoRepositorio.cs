using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.Model;

namespace Nissi.Repositorio
{
    public class FormaPgtoRepositorio
    {
        private readonly RepositorioDataContext _repositorioDataContext;
        public FormaPgtoRepositorio()
        {
            _repositorioDataContext = new RepositorioDataContext();
        }
        #region Métodos de Listagem
        public List<FormaPgtoVO> Listar()
        {
            var query = from f in _repositorioDataContext.FormaPgtos
                        select new FormaPgtoVO()
                                   {
                                       CodFormaPgto = f.CodFormaPgto,
                                       Descricao = f.Descricao,
                                       Parcelas = f.Parcelas,
                                       Intervalo = f.Intervalo,
                                       DataCadastro = f.DataCadastro,
                                       UsuarioInc = f.UsuarioInc,
                                       DataAlteracao = f.DataAlteracao,
                                       UsuarioAlt = f.UsuarioAlt
                                   };
            var lstFormaPgto = new List<FormaPgtoVO>();
            if (query.Count() > 0)
                lstFormaPgto = query.ToList();
            return lstFormaPgto;

        }
        public FormaPgtoVO ListarPorCodigo(int codFormaPgto)
        {
            var query = from f in _repositorioDataContext.FormaPgtos
                        select new FormaPgtoVO()
                        {
                            CodFormaPgto = f.CodFormaPgto,
                            Descricao = f.Descricao,
                            Parcelas = f.Parcelas,
                            Intervalo = f.Intervalo,
                            DataCadastro = f.DataCadastro,
                            UsuarioInc = f.UsuarioInc,
                            DataAlteracao = f.DataAlteracao,
                            UsuarioAlt = f.UsuarioAlt
                        };
            var formaPgto = query.FirstOrDefault();
            return formaPgto;
        }
        #endregion
        #region Método de Insclusão
        public int Incluir(string descricao, short? parcelas, short? intervalo, int codUsuarioInc)
        {
            var formaPgto = new FormaPgto()
            {
                Descricao = descricao,
                Parcelas = parcelas,
                Intervalo = intervalo,
                DataCadastro = DateTime.Now,
                UsuarioInc = codUsuarioInc,
            };
            _repositorioDataContext.FormaPgtos.InsertOnSubmit(formaPgto);
            _repositorioDataContext.SubmitChanges();
            return formaPgto.CodFormaPgto;
        }
        #endregion
        #region Método de Alteração
        public void Alterar(int codFormaPgto, string descricao, short? parcelas, short? intervalo, int codUsuarioAlt )
        {
            IQueryable<FormaPgto> query = from f in _repositorioDataContext.FormaPgtos
                                          where f.CodFormaPgto == codFormaPgto
                                          select f;
            var formaPgto = query.FirstOrDefault();
            formaPgto.Descricao = descricao;
            formaPgto.Parcelas = parcelas;
            formaPgto.Intervalo = intervalo;
            formaPgto.DataAlteracao = DateTime.Now;
            formaPgto.UsuarioAlt = codUsuarioAlt;
            _repositorioDataContext.SubmitChanges();
        }
        #endregion
        #region Método de Exclusão
        public void Excluir(int codFormaPgto)
        {
            IQueryable<FormaPgto> query = from f in _repositorioDataContext.FormaPgtos
                                          where f.CodFormaPgto == codFormaPgto
                                          select f;
            var formaPgto = query.FirstOrDefault();
            _repositorioDataContext.FormaPgtos.DeleteOnSubmit(formaPgto);
            _repositorioDataContext.SubmitChanges();
        }
        #endregion
    }
}
