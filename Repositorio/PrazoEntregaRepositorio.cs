using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.Model;

namespace Nissi.Repositorio
{
    public class PrazoEntregaRepositorio
    {
        private readonly RepositorioDataContext _repositorioDataContext;
        public PrazoEntregaRepositorio()
        {
            _repositorioDataContext = new RepositorioDataContext();
        }
        #region Método de Listagem
        public List<PrazoEntregaVO> Listar()
        {
            var query = from p in _repositorioDataContext.PrazoEntregas
                        select new PrazoEntregaVO()
                                   {
                                       CodPrazoEntrega = p.CodPrazoEntrega,
                                       Descricao = p.Descricao,
                                       Dias = p.Dias,
                                       DataCadastro = p.DataCadastro,
                                       UsuarioInc = p.UsuarioInc,
                                       DataAlteracao = p.DataAlteracao,
                                       UsuarioAlt = p.UsuarioAlt
                                   };
            var lstPrazoEntrega = new List<PrazoEntregaVO>();
            if (query.Count() > 0)
                lstPrazoEntrega = query.ToList();
            return lstPrazoEntrega;
        }
        public PrazoEntregaVO ListarPorCodigo(int codPrazoEntrega)
        {
            var query = from p in _repositorioDataContext.PrazoEntregas
                        where p.CodPrazoEntrega == codPrazoEntrega
                        select new PrazoEntregaVO()
                        {
                            CodPrazoEntrega = p.CodPrazoEntrega,
                            Descricao = p.Descricao,
                            Dias = p.Dias,
                            DataCadastro = p.DataCadastro,
                            UsuarioInc = p.UsuarioInc,
                            DataAlteracao = p.DataAlteracao,
                            UsuarioAlt = p.UsuarioAlt
                        };
            var prazoEntrega = query.FirstOrDefault();
            return prazoEntrega;
        }

        public List<PrazoEntregaVO> ListarPorDescricao(string descricao)
        {
            var query = from p in _repositorioDataContext.PrazoEntregas
                        where p.Descricao.Contains(descricao)
                        select new PrazoEntregaVO()
                                   {
                                       CodPrazoEntrega = p.CodPrazoEntrega,
                                       Descricao = p.Descricao,
                                       Dias = p.Dias,
                                       DataCadastro = p.DataCadastro,
                                       UsuarioInc = p.UsuarioInc,
                                       DataAlteracao = p.DataAlteracao,
                                       UsuarioAlt = p.UsuarioAlt
                                   };
            var lstPrazoEntrega = new List<PrazoEntregaVO>();
            if (query.Count() > 0)
                lstPrazoEntrega = query.ToList();
            return lstPrazoEntrega;
        }
        #endregion
        #region Método de Inclusão
        public int Incluir(string descricao, int dias, int codUsuarioInc)
        {
            var prazoentrega = new PrazoEntrega()
                                   {
                                       Descricao = descricao,
                                       Dias = dias,
                                       DataCadastro = DateTime.Now,
                                       UsuarioInc = codUsuarioInc
                                   };
            _repositorioDataContext.PrazoEntregas.InsertOnSubmit(prazoentrega);
            _repositorioDataContext.SubmitChanges();
            return prazoentrega.CodPrazoEntrega;
        }
        #endregion
        #region Método de Alteração
        public void Alterar(int codPrazoEntrega, string descricao, int dias, int codUsuarioAlt)
        {
            IQueryable<PrazoEntrega> query = from p in _repositorioDataContext.PrazoEntregas
                                             where p.CodPrazoEntrega == codPrazoEntrega
                                             select p;
            var prazoEntrega = query.FirstOrDefault();
            prazoEntrega.Descricao = descricao;
            prazoEntrega.Dias = dias;
            prazoEntrega.DataAlteracao = DateTime.Now;
            prazoEntrega.UsuarioAlt = codUsuarioAlt;
            _repositorioDataContext.SubmitChanges();
        }
        #endregion
        #region Método de Exclusão
        public void Excluir(int codPrazoEntrega)
        {
            IQueryable<PrazoEntrega> query = from p in _repositorioDataContext.PrazoEntregas
                                             where p.CodPrazoEntrega == codPrazoEntrega
                                             select p;
            var prazoEntrega = query.FirstOrDefault();
            _repositorioDataContext.PrazoEntregas.DeleteOnSubmit(prazoEntrega);
            _repositorioDataContext.SubmitChanges();
        }
        #endregion
    }
}
