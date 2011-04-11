using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nissi.Model;

namespace Nissi.Repositorio
{
    public class NormaRepositorio
    {
        private readonly RepositorioDataContext _repositorioDataContext;
        public NormaRepositorio()
        {
            _repositorioDataContext = new RepositorioDataContext();
        }
        #region Métodos de Listagem
        public List<NormaVO> Listar()
        {
            var query = from n in _repositorioDataContext.Normas
                        select new NormaVO()
                                   {
                                       CodNorma = n.CodNorma,
                                       Descricao = n.Descricao,
                                       Revisao = n.Revisao,
                                       DataCadastro = n.DataCadastro,
                                       DataAlteracao = n.DataAlteracao,
                                       UsuarioInc = n.UsuarioInc,
                                       UsuarioAlt = n.UsuarioAlt
                                   };
            var lstNorma = new List<NormaVO>();
            if (query.Count() > 0)
                lstNorma = query.ToList();
            return lstNorma;
        }
        public NormaVO ListarPorCodigo(int codNorma)
        {
            var query = from n in _repositorioDataContext.Normas
                        where n.CodNorma == codNorma
                        select new NormaVO()
                        {
                            CodNorma = n.CodNorma,
                            Descricao = n.Descricao,
                            Revisao = n.Revisao,
                            DataCadastro = n.DataCadastro,
                            DataAlteracao = n.DataAlteracao,
                            UsuarioInc = n.UsuarioInc,
                            UsuarioAlt = n.UsuarioAlt
                        };
            var norma = query.FirstOrDefault();
            return norma;
        }

        public List<MateriaPrimaVO> ListarPorNorma(string norma)
        {
            var query = from m in _repositorioDataContext.MateriaPrimas
                        join n in _repositorioDataContext.Normas
                            on m.CodNorma equals (n.CodNorma)
                        where n.Descricao.Contains(norma)
                        select new MateriaPrimaVO()
                                   {
                                       CodMateriaPrima = m.CodMateriaPrima,
                                       NormaVo = new NormaVO()
                                                     {
                                                         Descricao = n.Descricao,
                                                         Revisao = n.Revisao
                                                     },
                                       ClasseTipoVo = (from ct in _repositorioDataContext.ClasseTipos
                                                       where ct.CodClasseTipo == m.CodClasseTipo
                                                       select new ClasseTipoVO()
                                                                  {
                                                                      CodClasseTipo = ct.CodClasseTipo,
                                                                      Descricao = ct.Descricao
                                                                  }).FirstOrDefault(),
                                   };
            var lstMateriaPrima = new List<MateriaPrimaVO>();
            if (query.Count() > 0)
                lstMateriaPrima = query.ToList();
            return lstMateriaPrima;
        }

        #endregion
        #region Método de Inclusão
        public int Incluir(string descricao, int? revisao, int codUsuarioInc)
        {
            var norma = new Norma()
                            {
                                Descricao = descricao,
                                Revisao = revisao,
                                DataCadastro = DateTime.Now,
                                UsuarioInc = codUsuarioInc
                            };
            _repositorioDataContext.Normas.InsertOnSubmit(norma);
            _repositorioDataContext.SubmitChanges();
            return norma.CodNorma;
        }
        #endregion
        #region Método de Alteração
        public void Alterar(int codNorma, string descricao, int? revisao, int codUsuarioAlt)
        {
            IQueryable<Norma> query = from n in _repositorioDataContext.Normas
                                      where n.CodNorma == codNorma
                                      select n;
            var norma = query.FirstOrDefault();
            norma.Descricao = descricao;
            norma.Revisao = revisao;
            norma.DataAlteracao = DateTime.Now;
            norma.UsuarioAlt = codUsuarioAlt;
            _repositorioDataContext.SubmitChanges();
        }
        #endregion
        #region Método de Exclusão
        public void Excluir(int codNorma, int codUsuarioExc)
        {
            IQueryable<Norma> query = from n in _repositorioDataContext.Normas
                                      where n.CodNorma == codNorma
                                      select n;
            var norma = query.FirstOrDefault();
            _repositorioDataContext.Normas.DeleteOnSubmit(norma);
            _repositorioDataContext.SubmitChanges();
        }
        #endregion
    }
}
