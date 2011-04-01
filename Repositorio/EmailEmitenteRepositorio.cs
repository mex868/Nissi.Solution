using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.Model;

namespace Nissi.Repositorio
{
    public class EmailEmitenteRepositorio
    {
        private readonly RepositorioDataContext _repositorioDataContext;
        public EmailEmitenteRepositorio()
        {
            _repositorioDataContext = new RepositorioDataContext();
        }


        #region Métodos de Listagem
        public List<EmailEmitenteVO> Listar()
        {
            var query = from e in _repositorioDataContext.EmailEmitentes
                        select new EmailEmitenteVO()
                        {
                            CodEmailEmitente = e.CodEmailEmitente,
                            Email = e.Email,
                            Tipo = e.Tipo != null ? (TypePedido)e.Tipo : TypePedido.Compra
                        };
            var emailEmitente = new List<EmailEmitenteVO>();
            if (query.Count() > 0)
                emailEmitente = query.ToList();
            return emailEmitente;
        }

        public EmailEmitenteVO ListarPorCodigo(int codigoEmailEmitente)
        {
            var query = from e in _repositorioDataContext.EmailEmitentes
                        where e.CodEmailEmitente == codigoEmailEmitente
                        select new EmailEmitenteVO()
                        {
                            CodEmailEmitente = e.CodEmailEmitente,
                            Email = e.Email,
                            Tipo = e.Tipo != null ? (TypePedido)e.Tipo : TypePedido.Compra,
                            DataCadastro = e.DataCadastro,
                            DataAlteracao = e.DataAlteracao,
                            UsuarioInc = e.UsuarioInc,
                            UsuarioAlt = e.UsuarioAlt
                        };

            var emailEmitenteVO = query.FirstOrDefault();
            return emailEmitenteVO;

        }

        public List<EmailEmitenteVO> ListarPorTipo(int tipo)
        {
            var query = from e in _repositorioDataContext.EmailEmitentes
                        where e.Tipo == tipo
                        select new EmailEmitenteVO()
                        {
                            CodEmailEmitente = e.CodEmailEmitente,
                            Email = e.Email,
                            Tipo = e.Tipo != null ? (TypePedido)e.Tipo : TypePedido.Compra,
                            DataCadastro = e.DataCadastro,
                            DataAlteracao = e.DataAlteracao,
                            UsuarioInc = e.UsuarioInc,
                            UsuarioAlt = e.UsuarioAlt
                        };

            var emailEmitente = new List<EmailEmitenteVO>();
            if (query.Count() > 0)
                emailEmitente = query.ToList();
            return emailEmitente;
        }
        public List<EmailEmitenteVO> ListarPorEmailEmitente(string emailEmitente)
        {
            var query = from e in _repositorioDataContext.EmailEmitentes
                        where e.Email == emailEmitente
                        select new EmailEmitenteVO()
                        {
                            CodEmailEmitente = e.CodEmailEmitente,
                            Email = e.Email,
                            Tipo = e.Tipo != null ? (TypePedido)e.Tipo : TypePedido.Compra,
                            DataCadastro = e.DataCadastro,
                            DataAlteracao = e.DataAlteracao,
                            UsuarioInc = e.UsuarioInc,
                            UsuarioAlt = e.UsuarioAlt
                        };
            var lstEmailEmitenteVO = new List<EmailEmitenteVO>();
            if (query.Count() > 0)
                lstEmailEmitenteVO = query.ToList();
            return lstEmailEmitenteVO;

        }
        #endregion
        #region Métodos de Inclusão
        public int Incluir(string emailEmitente, int tipo, int codUsuarioInc)
        {
            var identEmailEmitente = new EmailEmitente()
            {
                Email = emailEmitente,
                Tipo = tipo,
                DataCadastro = DateTime.Now,
                UsuarioInc = codUsuarioInc
            };
            _repositorioDataContext.EmailEmitentes.InsertOnSubmit(identEmailEmitente);
            _repositorioDataContext.SubmitChanges();
            return identEmailEmitente.CodEmailEmitente;
        }
        #endregion
        #region Método de Alteração
        public void Alterar(int codEmailEmitente, string emailEmitente, int tipo, int codUsuarioAlt)
        {
            IQueryable<EmailEmitente> query = from e in _repositorioDataContext.EmailEmitentes
                                       where e.CodEmailEmitente == codEmailEmitente
                                       select e;
            var identEmailEmitente = query.FirstOrDefault();
            identEmailEmitente.Email = emailEmitente;
            identEmailEmitente.Tipo = tipo;
            identEmailEmitente.DataAlteracao = DateTime.Now;
            identEmailEmitente.UsuarioAlt = codUsuarioAlt;
            _repositorioDataContext.SubmitChanges();
        }
        #endregion
        #region Método de Exclusão
        public void Excluir(int codEmailEmitente, int codUsuarioInc)
        {
            IQueryable<EmailEmitente> query = from e in _repositorioDataContext.EmailEmitentes
                                       where e.CodEmailEmitente == codEmailEmitente
                                       select e;
            if (query.Count() > 0)
            {
                var identEmailEmitente = query.FirstOrDefault();
                _repositorioDataContext.EmailEmitentes.DeleteOnSubmit(identEmailEmitente);
                _repositorioDataContext.SubmitChanges();
            }
        }
        #endregion        
    }
}
